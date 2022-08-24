using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting; 

namespace VIsitorPlacementTests.LogicTests;



//Todo: Snelle test om of list correct werken volgens C#, mag weg.
[TestClass]
public class PlacementTest
{
    
    
    [TestMethod]
    public void FilterTest()
    {
        List<int> list2 = new List<int>(){1,2};
        
        Test(list2);
        
        Assert.AreEqual(3, list2.Count);
    }

    public void Test(List<int> list)
    { 
        list.Add(3);
    }
}