using Nancy;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BandTracker.Objects;

namespace BandTracker.Objects
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => View["index.cshtml"];

      Get["/venues"] = _ =>
      {
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/venues/add"] = _ => View["venue_new.cshtml"];
      Post["/venues"] = _ =>
      {
        Venue newVenue = new Venue(Request.Form["name"]);
        newVenue.Save();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Delete["/venues/deleted"] = _ =>
      {
        Venue.DeleteAll();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };
      Get["/venues/{id}"] = parameters =>
      {
        Venue selectedVenue = Venue.Find(parameters.id);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("venue", selectedVenue);
        model.Add("bands", allBands);
        return View["venue.cshtml", model];
      };
      Post["/venues/{id}"] = parameters =>
      {
        Venue selectedVenue = Venue.Find(parameters.id);
        int bandId = Request.Form["band"];
        selectedVenue.AddBand(bandId);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("venue", selectedVenue);
        model.Add("bands", allBands);
        return View["venue.cshtml", model];
      };
      Patch["/venues/{id}"] = parameters =>
      {
        Venue selectedVenue = Venue.Find(parameters.id);
        string newName = Request.Form["newName"];
        selectedVenue.Update(newName);
        List<Band> allBands = Band.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("venue", selectedVenue);
        model.Add("bands", allBands);
        return View["venue.cshtml", model];
      };
      Delete["/venues"] = _ =>
      {
        Venue selectedVenue = Venue.Find(Request.Form["venueId"]);
        selectedVenue.Delete();
        List<Venue> allVenues = Venue.GetAll();
        return View["venues.cshtml", allVenues];
      };

      Get["/bands"] = _ =>
      {
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/bands/add"] = _ => View["band_new.cshtml"];
      Post["/bands"] = _ =>
      {
        Band newBand = new Band(Request.Form["name"]);
        newBand.Save();
        List<Band> allBands = Band.GetAll();
        return View["bands.cshtml", allBands];
      };
      Get["/bands/{id}"] = parameters =>
      {
        Band selectedBand = Band.Find(parameters.id);
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("band", selectedBand);
        model.Add("venues", allVenues);
        return View["band.cshtml", model];
      };
      Post["/bands/{id}"] = parameters =>
      {
        int bandId = parameters.id;
        Band selectedBand = Band.Find(bandId);
        Venue addVenueToBand = Venue.Find(Request.Form["venue"]);
        addVenueToBand.AddBand(bandId);
        List<Venue> allVenues = Venue.GetAll();
        Dictionary<string, object> model = new Dictionary<string, object>{};
        model.Add("band", selectedBand);
        model.Add("venues", allVenues);
        return View["band.cshtml", model];
      };
    }
  }

}
