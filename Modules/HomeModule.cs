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
      Get["/venues/{id}"] = parameters =>
      {
        Venue selectedVenue = Venue.Find(parameters.id);
        return View["venue.cshtml", selectedVenue];
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
        return View["band.cshtml", selectedBand];
      };
    }
  }

}
