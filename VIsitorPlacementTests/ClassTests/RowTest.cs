using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Track;

namespace VIsitorPlacementTests.ClassTests;

[TestClass]
public class RowTest
{
    [TestMethod]
    public void Constructor()
    {
        //arrange
        var length = 3;
        var rowNr = 1;
        //act
        var row = new Row(length, rowNr);
        //assert
        Assert.IsNotNull(row);
        Assert.AreEqual(rowNr, 1);
        foreach (var seat in row.Seats)
        {
            Assert.IsNotNull(seat);
        }
    }
}