using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class BandTrackerTest : IDisposable
  {
    public BandTrackerTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Band.DeleteAll();
    }
    [Fact]
    public void Band_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact] public void Band_SavesToDatabase()
    {
      //Arrange
      Band newBand = new Band("The Sensational Seven");
      List<Band> expectedResult = new List<Band>{newBand};
      //Act
      newBand.Save();
      List<Band> result = Band.GetAll();
      //Assert
      Assert.Equal(expectedResult, result);

    }
  }
}
