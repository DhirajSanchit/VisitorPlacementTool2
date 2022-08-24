using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Track;
using VisitorPlacementTool2.Visitors;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class SeatTest
{
    [TestMethod]
    public void Constructor()
    {
        //arrange
        var seatNr = 1;
        //act
        var seat = new Seat(seatNr);
        //assert
        Assert.IsNotNull(seat);
        Assert.AreEqual(1, seat.Number);
    }

    [TestMethod]
    public void PlaceVisitor()
    {
        //arrange
        var seatNr = 1;
        var seat = new Seat(seatNr);
        var visitor = new Visitor("Visitor1", new DateTime(2001,12,20), new DateTime(2022,5,21));
        //act
        seat.PlaceVisitor(visitor);
        //assert
        Assert.AreEqual("Visitor1",seat.Visitor.Name);
    }

    [TestMethod]
    public void IsNotOccupied()
    {
        //arrange
        var seatNr = 1;
        var seat = new Seat(seatNr);
        //act
        var result = seat.IsOccupied();
        //assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void IsOccupied()
    {
        //arrange
        var seatNr = 1;
        var seat = new Seat(seatNr);
        var visitor = new Visitor("Visitor1", new DateTime(2001,12,20), new DateTime(2022,5,19));
        //act
        seat.PlaceVisitor(visitor);
        var result = seat.IsOccupied();
        //assert
        Assert.IsTrue(result);
    }
}