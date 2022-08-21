using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Group;

namespace VIsitorPlacementTests.LogicTests;

[TestClass]
public class GroupGeneratorTest
{
    [TestMethod]
    public void GenerateVisitorGroup()
    {
        //arrange
        var groupGenerator = new GroupGenerator();
        var id = 1;
        //act
        var result = groupGenerator.GenerateVisitorGroup(id);
        //assert
        Assert.IsNotNull(result.GetVisitors());
        foreach (var visitor in result.GetVisitors())
        {
            Assert.IsNotNull(visitor.Name);
            Assert.IsNotNull(visitor.DateOfBirth);
        }
    }

    [TestMethod]
    public void GenerateMaxVisitorGroups()
    {
        //arrange
        var maxVisitors = 250;
        var groupGenerator = new GroupGenerator();
        //act
        var results = groupGenerator.GenerateMaxVisitorGroups(maxVisitors);
        //assert
        Assert.IsNotNull(results);
        foreach (var result in results)
        {
            Assert.IsTrue(result.GetVisitors().Count > 0);
        }
    }
}