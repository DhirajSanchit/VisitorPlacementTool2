using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Competition;
using VisitorPlacementTool2.Containers;
using VisitorPlacementTool2.Group;
using VisitorPlacementTool2.Visitors;

namespace VIsitorPlacementTests.LogicTests;

[TestClass]
public class FilterTests
{
    [TestMethod]
    public void Filter()
    {
        
    }
    
    
    [TestMethod]
    public static void SortByAmountOfMinors_InContainer_HighestAmountFirstLowestLast()
    {
        var vis = new List<Visitor>();
        VisitorGroup group1 = new VisitorGroup(vis);
        
        //arrange  
        var groupGenerator = new GroupGenerator();
        var date = new DateTime(2022, 12, 12);
        List<VisitorGroup> list = groupGenerator.GenerateMaxVisitorGroups(100);

        //act
        GroupContainer _container = new(list);
        _container.SortGroups(date);
        var groupa = list.First().AmountOfKids(date);
        var groupb = list.Last().AmountOfKids(date);
        
        //assert
        Assert.IsTrue(groupa > groupb, "The list is not sorted by amount of kids");
    }
}