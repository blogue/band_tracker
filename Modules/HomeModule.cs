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
    }
  }

}
