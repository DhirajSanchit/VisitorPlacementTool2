using System;
using System.Collections.Generic;

namespace VisitorPlacementTool2.Visitors;

/// <summary>
/// Generates a random list of Visitors
/// </summary>

public class VisitorGenerator
{
    private Random _generator = new Random();
    
    public List<Visitor> GenerateVisitors(int amount, int groupid)
    {
        if (amount <= 0)
        {
            throw new IndexOutOfRangeException("Index for amount is lower than or equal to 0");
        }
        var visitors = new List<Visitor>();
        
        //Generate random date of birth
        var randomDateOfBirth = new DateTime(_generator.Next(1940, 2020), _generator.Next(1, 12), _generator.Next(1, 28));
        var date = new DateTime(2022, _generator.Next(1, 12), _generator.Next(1, 28));

        for (var i = 0; i < amount; i++)
        {
            var visitor = new Visitor
            (
                "Visitor " + i,
                randomDateOfBirth,
                date
                );
            visitor.GroupId = groupid;
            visitors.Add(visitor);
        }
        return visitors;
    }
}

 