using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Venue
  {
    private int _id;
    private string _name;

    public Venue(string name, int Id=0)
    {
      _name = name;
      _id = Id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public static List<Venue> GetAll()
    {
      List<Venue> allVenues = new List<Venue>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int venueId = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        Venue newVenue = new Venue(venueName, venueId);
        allVenues.Add(newVenue);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allVenues;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues; DELETE FROM venues_bands;", conn);
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherVenue)
     {
       if(!(otherVenue is Venue))
       {
         return false;
       }
       else
       {
         Venue newVenue = (Venue) otherVenue;
         bool venueIdEquality = _id == newVenue.GetId();
         bool venueNameEquality = _name == newVenue.GetName();
         return (venueIdEquality && venueNameEquality);
       }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues (name) OUTPUT INSERTED.id VALUES (@VenueName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@VenueName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);

      rdr=cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();
    }
    public static Venue Find(int venueId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM venues WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = venueId;
      cmd.Parameters.Add(venueIdParameter);

      int foundVenueId = 0;
      string foundVenueName = null;

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundVenueId = rdr.GetInt32(0);
        foundVenueName = rdr.GetString(1);
      }
      Venue foundVenue = new Venue(foundVenueName, foundVenueId);

      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();

      return foundVenue;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM venues WHERE id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      cmd.ExecuteNonQuery();

      if(conn!=null) conn.Close();
    }
    public void Update(string newName)
     {
       _name = newName;

       SqlConnection conn = DB.Connection();
       conn.Open();

       SqlCommand cmd = new SqlCommand("UPDATE venues SET name=@NewName WHERE id=@VenueId;", conn);

       SqlParameter venueIdParameter = new SqlParameter();
       venueIdParameter.ParameterName = "@VenueId";
       venueIdParameter.Value = this.GetId();
       cmd.Parameters.Add(venueIdParameter);

       SqlParameter nameParameter = new SqlParameter();
       nameParameter.ParameterName = "@NewName";
       nameParameter.Value = newName;
       cmd.Parameters.Add(nameParameter);

       cmd.ExecuteNonQuery();

       if(conn!=null) conn.Close();
     }
    public void AddBand(int bandId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO venues_bands (venue_id, band_id) VALUES (@VenueId, @BandId);", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = bandId;
      cmd.Parameters.Add(bandIdParameter);

      cmd.ExecuteNonQuery();

      if(conn != null) conn.Close();
    }
    public List<Band> GetBands()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT bands.* FROM venues JOIN venues_bands ON (venues_bands.venue_id = venues.id) JOIN bands ON (venues_bands.band_id = bands.id) WHERE venue_id = @VenueId;", conn);

      SqlParameter venueIdParameter = new SqlParameter();
      venueIdParameter.ParameterName = "@VenueId";
      venueIdParameter.Value = this.GetId();
      cmd.Parameters.Add(venueIdParameter);

      rdr = cmd.ExecuteReader();

      List<Band> foundBands = new List<Band>{};

      while(rdr.Read())
      {
        int foundBandId = rdr.GetInt32(0);
        string foundBandName = rdr.GetString(1);
        Band foundBand = new Band(foundBandName, foundBandId);
        foundBands.Add(foundBand);
      }

      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();

      return foundBands;
    }
  }
}
