using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VisitorPlacementTool2.Competition;
using VisitorPlacementTool2.Containers;
using VisitorPlacementTool2.Group;
using VisitorPlacementTool2.Visitors;

namespace VIsitorPlacementTests.LogicTests;

[TestClass]
public class SortingTests
{

    [TestMethod]
    //Tests if visitorgroup in container is added sorted by highest amount of kids
    //then grouped by highest total amount
    public void SortByAmountOfMinors_InContainer_HighestAmountFirstLowestLast()
    {
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
    
    [TestMethod]
    //Tests if visitorgroup in container is added sorted by highest amount of kids
    //then grouped by highest total amount
    public void SortAreaByBiggestSize_InCompetition_HighestAmountFirstLowestLast()
    {
        //arrange  
        
        var groupGenerator = new GroupGenerator();
        var competitionDate = new DateTime(2022, 12, 12);
        var deadline = new DateTime(2022, 11, 12);
        var maxVisitors = 100;
        
        var competition = new Competition(competitionDate, deadline, maxVisitors);
        List<VisitorGroup> list = groupGenerator.GenerateMaxVisitorGroups(100);
        

        //act 
        competition.SortAreas();
        var AreaA = competition.Areas.First().GetNumberOfSeats();
        var AreaB = competition.Areas.Last().GetNumberOfSeats();;

        //assert
        Assert.IsTrue(AreaA > AreaB, "Area's not sorted correctly, biggest isn't first");
    }
    

}
 

