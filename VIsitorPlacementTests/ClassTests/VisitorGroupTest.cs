using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Group;
using VisitorPlacementTool2.Visitors;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class VisitorGroupTest
{
    [TestMethod]
    public void Constructor()
    {
        //arrange
        var visitors = new List<Visitor>();
        //act
        var visitorGroup = new VisitorGroup(visitors);
        //assert
        Assert.IsNotNull(visitorGroup);
    }

    [TestMethod]
    public void GetVisitors()
    {
        //arrange
        var visitors = new List<Visitor>();
        visitors.Add(new Visitor("Visitor1", new DateTime(2003,6,9), new DateTime(2022,12,1)));
        visitors.Add(new Visitor("Visitor2", new DateTime(2001,11,13), new DateTime(2022,12,1)));
        visitors.Add(new Visitor("Visitor3", new DateTime(1996, 12, 4), new DateTime(2022,12,1)));
        var visitorGroup = new VisitorGroup(visitors);
        //act
        List<Visitor> results = (List<Visitor>) visitorGroup.GetVisitors();
        //assert
        Assert.AreEqual(3, results.Count);
        for (var i = 0; i < visitors.Count; i++)
        {
            Assert.AreEqual(visitors[i].Name, results[i].Name);
            Assert.AreEqual(visitors[i].DateOfBirth, results[i].DateOfBirth);
        }
    }
}