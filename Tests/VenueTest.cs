using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BandTracker
{
  public class VenueTrackerTest : IDisposable
  {
    public VenueTrackerTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=band_tracker_test;Integrated Security=SSPI;";
    }

    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }
    [Fact]
    public void Venue_DatabaseEmpty()
    {
      //Arrange, Act
      int result = Venue.GetAll().Count;
      //Assert
      Assert.Equal(0, result);
    }
    [Fact]
    public void Venue_SavesToDatabase()
    {
      //Arrange
      Venue newVenue = new Venue("The Rainbow Room");
      List<Venue> expectedResult = new List<Venue>{newVenue};
      //Act
      newVenue.Save();
      List<Venue> result = Venue.GetAll();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Venue_SavesToDatabaseWithId()
    {
      //Arrange
      Venue newVenue = new Venue("The Rainbow Room");
      List<Venue> allVenues = new List<Venue>{newVenue};
      //Act
      newVenue.Save();
      List<Venue> savedVenue = Venue.GetAll();
      int result = savedVenue[0].GetId();
      int expectedResult = allVenues[0].GetId();
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Venue_FindVenueInDatabase()
    {
      //Arrange
      Venue expectedResult = new Venue("The Rainbow Room");
      //Act
      expectedResult.Save();
      Venue result = Venue.Find(expectedResult.GetId());
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
     public void Venue_Delete_DeletesInstanceOfVenue()
     {
       Venue firstVenue = new Venue("The Rainbow Room");
       firstVenue.Save();
       Venue secondVenue = new Venue("Madison Square Garden");
       secondVenue.Save();

       firstVenue.Delete();

       List<Venue> expectedResult = new List<Venue>{secondVenue};
       List<Venue> actualResult = Venue.GetAll();
       Assert.Equal(expectedResult, actualResult);
     }
    [Fact]
    public void Venue_Update_UpdatesVenue()
    {
      //Arrange
      Venue firstVenue = new Venue("The Rainbow Room");
      firstVenue.Save();
      //Act
      firstVenue.Update("Madison Square Garden");
      Venue resultVenue = Venue.Find(firstVenue.GetId());
      string result = resultVenue.GetName();
      string expectedResult = "Madison Square Garden";
      //Assert
      Assert.Equal(expectedResult, result);
    }
    [Fact]
    public void Venue_AddBandToVenue()
    {
      //Arrange
      Venue testVenue = new Venue("The Rainbow Room");
      testVenue.Save();
      Band firstBand = new Band("The Beatles");
      firstBand.Save();
      Band secondBand = new Band("The Who");
      secondBand.Save();
      Band thirdBand = new Band("Miley Cyrus");
      thirdBand.Save();
      //Act
      testVenue.AddBand(firstBand.GetId());
      testVenue.AddBand(thirdBand.GetId());
      List<Band> expectedResult = new List<Band>{firstBand, thirdBand};
      List<Band> result = testVenue.GetBands();
      //Assert
      Assert.Equal(expectedResult, result);
    }
  }
}
