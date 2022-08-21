using System.Collections.Generic;
using VisitorPlacementTool2.Group;

namespace VisitorPlacementTool2.Filter;

public class Filter
{
    //Purpose of this class is to check if the group can be placed in 1 go, or has to be split up int o subgroups
    private List<VisitorGroup> WithMinors;
    private List<VisitorGroup> OnlyAdults;

    public Filter()
    {
        WithMinors = new();
        OnlyAdults = new();
    }

    //Checks if the Visitorsgroup instance can be placed
    public List<VisitorGroup> FilterGroupForMinors(List<VisitorGroup> groups)
    {
        List<VisitorGroup> SortedGroupWithMinors = new List<VisitorGroup>();
        List<VisitorGroup> SortedGroupWithAdults = new List<VisitorGroup>();
        
        //Check each group
        foreach (VisitorGroup group in groups)
        {
            foreach (var Visitor in group.GetVisitors())
            {
                //Count amount of minors
                if(Visitor.IsAnAdult(Visitor.DateOfBirth))
                {
                    //SortedGroupWithMinors.
                }

            }
                     
            {            
                /*//if the biggest group is smaller than the current group, split the biggest group in 2 and place the current group in the smaller group
                //split the biggest group in 2 and place the current group in the smaller group
                VisitorGroup biggestGroup = sortedGroups.Find(x => x.GetSize() == sortedGroups.Max(y => y.GetSize()));
                VisitorGroup smallerGroup = new VisitorGroup();
                if (biggestGroup.GetSize() > group.GetSize())
                {
                    smallerGroup = biggestGroup;
                    biggestGroup = group;
                }
                else
                {
                    smallerGroup = group;
                }
                //split the biggest group in 2 and place the current group in the smaller group
                VisitorGroup newGroup = new VisitorGroup();
                newGroup.AddVisitor(smallerGroup.GetVisitor(0));
                newGroup.AddVisitor(smallerGroup.GetVisitor(1));
                sortedGroups.Add(newGroup);*/
            }
            
            
        }
        return null;
    }
    
    //Loop through the Area seats for the first row and keep count
    // Compare with 
    //
    
    // Count if the amount of consecutive seats
    //
    //
    //
    // is equal to the amount of visitors
    // 
    //  End Summary
    //
    //
    // Return true if the group can be placed
    
    
    
}