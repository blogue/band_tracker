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
      Venue.DeleteAll();
    }
    [Fact]
    public void Band_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Band.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Band_SavesToDatabase()
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
    [Fact]
    public void Band_SavesToDatabaseWithId()
    {
      //Arrange
      Band newBand = new Band("The Sensational Seven");
      List<Band> allBands = new List<Band>{newBand};
      //Act
      newBand.Save();
      List<Band> savedBand = Band.GetAll();
      int result = savedBand[0].GetId();
      int expectedResult = allBands[0].GetId();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Band_FindBandInDatabase()
    {
      //Arrange
      Band expectedResult = new Band("The Sensational Seven");
      //Act
      expectedResult.Save();
      Band result = Band.Find(expectedResult.GetId());
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Band_GetVenues_GetAllVenuesAssociatedWithInstanceOfBand()
    {
      //Arrange
      Band testBand = new Band("The Sensational Seven");
      testBand.Save();
      Venue firstVenue = new Venue("The Rainbow Room");
      firstVenue.Save();
      Venue secondVenue = new Venue("Madison Square Garden");
      secondVenue.Save();
      Venue thirdVenue = new Venue("Mickey's Carwash");
      thirdVenue.Save();
      //Act
      firstVenue.AddBand(testBand.GetId());
      thirdVenue.AddBand(testBand.GetId());
      List<Venue> result = testBand.GetVenues();
      List<Venue> expectedResult = new List<Venue>{firstVenue, thirdVenue};
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
