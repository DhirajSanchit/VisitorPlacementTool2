using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Track;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class StageTest
{
    [TestMethod]
    public void Constructor()
    {
        //arrange
        var areas = new List<Area>();
        //act
        var stage = new Stage(areas);
        //assert
        Assert.IsNotNull(stage);
    }

    [TestMethod]
    public void GetArea()
    {
        //arrange
        var areas = new List<Area>();
        areas.Add(new Area("A",3,4));
        areas.Add(new Area("B",4,5));
        areas.Add(new Area("C",6,3));
        var stage = new Stage(areas);
        //act
        var result = stage.GetArea("B");
        //
        Assert.AreEqual("B", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetNonContainedArea()
    {
        //arrange
        var areas = new List<Area>();
        areas.Add(new Area("A",3,4));
        areas.Add(new Area("B",4,5));
        areas.Add(new Area("C",6,3));
        var stage = new Stage(areas);
        //act
        var result = stage.GetArea("Q");
        //assert
        Assert.Fail();
    }

    [TestMethod]
    public void GetAllAreas()
    {
        //arrange
        var areas = new List<Area>();
        areas.Add(new Area("A",3,4));
        areas.Add(new Area("B",4,5));
        areas.Add(new Area("C",6,3));
        var stage = new Stage(areas);
        //act
        var results = stage.GetAllAreas();
        //assert
        Assert.AreEqual(3, results.Count);
        foreach (var result in results)
        {
            Assert.IsNotNull(result.Name);
            Assert.IsTrue(result.Rows.Count > 2);
            Assert.IsTrue(result.Rows[0].Seats.Count > 2);
        }
    }
}