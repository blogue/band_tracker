using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace BandTracker
{
  public class Band
  {
    private int _id;
    private string _name;

    public Band(string name, int Id=0)
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

    public static List<Band> GetAll()
    {
      List<Band> allBands = new List<Band>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int bandId = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        Band newBand = new Band(bandName, bandId);
        allBands.Add(newBand);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allBands;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM bands;", conn);
      cmd.ExecuteNonQuery();
    }

    public override bool Equals(System.Object otherBand)
     {
       if(!(otherBand is Band))
       {
         return false;
       }
       else
       {
         Band newBand = (Band) otherBand;
         bool bandIdEquality = _id == newBand.GetId();
         bool bandNameEquality = _name == newBand.GetName();
         return (bandIdEquality && bandNameEquality);
       }
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO bands (name) OUTPUT INSERTED.id VALUES (@BandName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@BandName";
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
    public static Band Find(int bandId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlDataReader rdr = null;

      SqlCommand cmd = new SqlCommand("SELECT * FROM bands WHERE id = @BandId;", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = bandId;
      cmd.Parameters.Add(bandIdParameter);

      int foundBandId = 0;
      string foundBandName = null;

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        foundBandId = rdr.GetInt32(0);
        foundBandName = rdr.GetString(1);
      }
      Band foundBand = new Band(foundBandName, foundBandId);

      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();

      return foundBand;
    }
    public List<Venue> GetVenues()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT venues.* FROM bands JOIN venues_bands ON (venues_bands.band_id = bands.id) JOIN venues ON (venues_bands.venue_id = venues.id) WHERE band_id = @BandId;", conn);

      SqlParameter bandIdParameter = new SqlParameter();
      bandIdParameter.ParameterName = "@BandId";
      bandIdParameter.Value = this.GetId();
      cmd.Parameters.Add(bandIdParameter);

      rdr = cmd.ExecuteReader();

      List<Venue> foundVenues = new List<Venue>{};

      while(rdr.Read())
      {
        int foundVenueId = rdr.GetInt32(0);
        string foundVenueName = rdr.GetString(1);
        Venue foundVenue = new Venue(foundVenueName, foundVenueId);
        foundVenues.Add(foundVenue);
      }

      if(rdr!=null) rdr.Close();
      if(conn!=null) conn.Close();

      return foundVenues;
    }
  }
}
