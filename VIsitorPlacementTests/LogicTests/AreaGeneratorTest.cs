using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Track;

namespace VIsitorPlacementTests.LogicTests;

[TestClass]
public class AreaGeneratorTest
{
    [TestMethod]
    public void GenerateAreaTest()
    {
        //arrange
        var areaName = "A";
        var areaGenerator = new AreaGenerator();
        //act
        var result= areaGenerator.GenerateArea(areaName);
        //assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Rows.Count > 0);
        foreach (var row in result.Rows)
        {
            Assert.IsTrue(row.Seats.Count >= 3);
            Assert.IsTrue(row.Seats.Count <= 10);
        }
        Assert.AreEqual("A", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GenerateAreaWithEmptyName()
    {
        //arrange
        var areaName = "";
        var areaGenerator = new AreaGenerator();
        //act
        var result= areaGenerator.GenerateArea(areaName);
        //assert
        Assert.Fail();
    }
    
    [TestMethod]
    public void GenerateName()
    {
        //arrange
        var areaGenerator = new AreaGenerator();
        //act
        for (int i = 0; i < 100; i++)
        {
            var result = areaGenerator.GenerateName(i);
            //assert
            Console.WriteLine(result);
        }
        //assert
        // Assert.Fail();
    }
}