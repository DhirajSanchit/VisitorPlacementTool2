using System;
using System.Collections.Generic;
using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Group;

public class GroupGenerator
{
    private Random
        _random = new Random(); //todo create random in constructor to use one random instead of making a new one on every call?
    //GroupGenerator is a class that generates groups of visitors


    //todo Review beneath.
    //Needs to generate an amount of groups, then fill those groups with visitors.
    //Function should only create a single group and should be called multiple times for multiple groups.


    ////Should this class also be responsible for generating visitors?
    /// amit: not directly, visitor generator should be called.
    //Pre determine which visitors are placed in group or pick out random?

    //-Create a group with group size.
    //-Fill group with visitors until group size threshold is reached.
    //-If visitor amount != group size => throw exception?

    //-Create a group with group size.

    //Generates a single group with visitors and returns it.
    public VisitorGroup GenerateVisitorGroup(int id)
    { 
        VisitorGenerator visitorGenerator = new();
        VisitorGroup visitorGroup = new VisitorGroup
        (
            visitorGenerator.GenerateVisitors(_random.Next(1, 11), id)
        );
        visitorGroup.SetId(id);
        //generate a random number of visitors and add them to the group
        return visitorGroup;
    }

    //TODO: Remove code below, not used
    // public List<VisitorGroup> GenerateVisitorGroups(int amountOfGroups)
    // {
    //     if (amountOfGroups <= 0)
    //     {
    //         throw new IndexOutOfRangeException("Amount of groups must be higher than zero.");
    //     }
    //
    //     List<VisitorGroup> visitorGroups = new List<VisitorGroup>();
    //     for (int i = 0; i < amountOfGroups; i++)
    //     {
    //         visitorGroups.Add(GenerateVisitorGroup());
    //     }
    //
    //     return visitorGroups;
    // }

    public List<VisitorGroup> GenerateMaxVisitorGroups(int maxVisitors)
    {
        List<VisitorGroup> visitorGroups = new List<VisitorGroup>();
        int amount = 0;
        int index = 0;
        while (amount < maxVisitors)
        {
            var group = GenerateVisitorGroup(index);
            amount += group.GetVisitors().Count;

            if (amount > maxVisitors)
            {
                break;
            }

            visitorGroups.Add(group);
            index++;
        }

        return visitorGroups;
    }
}