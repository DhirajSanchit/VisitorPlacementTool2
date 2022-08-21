using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Track;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class AreaTest
{
    [TestMethod]
    public void EmptyConstructor()
    {
        //arrange
        //act
        var area = new Area();
        //assert  
        Assert.IsNotNull(area);
        Assert.IsNotNull(area.Rows);
    }

    [TestMethod]
    public void Constructor()
    {
        //arrange
        var areaName = "A";
        var rowAmount = 4;
        var rowLength = 3;
        //act
        var area = new Area(areaName,rowAmount,rowLength);
        //assert
        Assert.IsNotNull(area);
        Assert.AreEqual("A",area.Name);
        Assert.AreEqual(4, area.Rows.Count);
        Assert.AreEqual(3, area.Rows[0].Seats.Count);
    }
}