using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Visitors;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class VisitorTest
{
    [TestMethod]
    public void Constructor()
    {
        //arrange
        var name = "Visitor1";
        var dateOfBirth = new DateTime(2010, 4, 20);
        //act
        var visitor = new Visitor(name, dateOfBirth);
        //assert
        Assert.IsNotNull(visitor);
        Assert.AreEqual(name, visitor.Name);
        Assert.AreEqual(dateOfBirth, visitor.DateOfBirth);
    }

    [TestMethod]
    public void IsAnAdult()
    {
        //arrange
        var name = "Visitor1";
        var dateOfBirth = new DateTime(2003, 4, 20);
        var visitor = new Visitor(name, dateOfBirth);
        //act
        var result = visitor.IsAnAdult(new DateTime(2022,6,12));
        //assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsNotAnAdult()
    {
        //arrange
        var name = "Visitor1";
        var dateOfBirth = new DateTime(2014, 4, 20);
        var visitor = new Visitor(name, dateOfBirth);
        //act
        var result = visitor.IsAnAdult(new DateTime(2022, 6, 21));
        //assert
        Assert.IsFalse(result);
    }
}