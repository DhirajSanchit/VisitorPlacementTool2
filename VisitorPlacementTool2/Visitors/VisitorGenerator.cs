using System;
using System.Collections.Generic;

namespace VisitorPlacementTool2.Visitors;

/// <summary>
/// Generates a random list of Visitors
/// </summary>

public class VisitorGenerator
{
    private Random _generator = new Random();
    
    //Generates visitors with random names and ages and assigns a groupid to each visitor
    public List<Visitor> GenerateVisitors(int amount, int groupid)
    {
        //negative amount will throw exception
        if (amount <= 0)
        {
            throw new IndexOutOfRangeException("Index for amount is lower than or equal to 0");
        }
        var visitors = new List<Visitor>();
        
        
        //Keep generating random names and ages until the amount of visitors is reached
        for (var i = 0; i < amount; i++)
        {
            
            //Generate random date of birth
            //Highly accurate dates are ouf of scope for this project, but this is a good start.
            //Leap years are not included in this calculation, every value should give a valid and practical date.
            var randomDateOfBirth = new DateTime(_generator.Next(1940, 2020), _generator.Next(1, 12), _generator.Next(1, 28));
            
            //Generate random date of registration
            var date = new DateTime(2022, _generator.Next(1, 12), _generator.Next(1, 28));

            var visitor = new Visitor
            (
                //assigns a random name for example: "Visitor1", "Visitor2", "Visitor3" etc.
                "Visitor " + i,
                randomDateOfBirth,
                date
                );
            //assigns a groupid to each visitor
            visitor.GroupId = groupid;
            
            //adds the visitor to the list of visitors generated
            visitors.Add(visitor);
        }
        return visitors;
    }
}

 