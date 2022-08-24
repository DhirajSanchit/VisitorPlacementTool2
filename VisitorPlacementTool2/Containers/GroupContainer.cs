using System;
using System.Collections.Generic;
using VisitorPlacementTool2.Group;

namespace VisitorPlacementTool2.Containers;

/// <summary>
/// Container class for storing the  visitorgroups
/// </summary>


public class GroupContainer
{
    private List<VisitorGroup> Groups;

    public GroupContainer()
    {
        Groups = new List<VisitorGroup>();
    }
    public GroupContainer(List<VisitorGroup> groups)
    {
        Groups = groups;
    }

    public void AddGroup(VisitorGroup group)
    {
        Groups.Add(group);
    }

    public IReadOnlyCollection<VisitorGroup> GetGroups()
    {
        return Groups;
    }
    public void RemoveGroup(VisitorGroup group)
    {
        Groups.Remove(group);
    }
    
    public VisitorGroup GetGroupById(int id)
    {
        foreach (VisitorGroup group in Groups)
        {
            if (group.GetId() == id)
            {
                return group;
            }
        }
        return null;
    }
    
    
    public void SortGroups(DateTime competitionDate)
    
    {   
        // Groups.Sort((x, y) => x.GetId().CompareTo(y.GetId()));
        
        //Sort the groups by the amount of kids in a group
        Groups.Sort((x, y) => y.AmountOfKids(competitionDate).CompareTo(x.AmountOfKids(competitionDate)));
    
        //Categorize the areas based on the seatnumber
        var sizeCategory = new List<VisitorGroup>();
            
        //Compare the areas based on the seat Amount and save them in a list.
        var lastSize = Groups[0].AmountOfKids(competitionDate);
        List<VisitorGroup> sortedGroups = new();
 
        foreach (var group in Groups)
        {
            //If the area size is not the same as the last size,
            if (group.AmountOfKids(competitionDate) != lastSize)
            {
                    
                //Sort the list by rowlength and save it in a new list.
                sizeCategory.Sort((x, y) => x.GetVisitors().Count.CompareTo(y.GetVisitors().Count));
                sortedGroups.AddRange(sizeCategory);
                    
                //Empty the list to start for the new size category
                sizeCategory = new();
                lastSize = group.AmountOfKids(competitionDate);
            }

            //Add the area to the list.
            sizeCategory.Add(group);
        }
            
        //Sort the last category and save it in a new list for the last size category.
        sizeCategory.Sort((x, y) => x.GetVisitors().Count.CompareTo(y.GetVisitors().Count));
        sortedGroups.AddRange(sizeCategory);
            
        //Save the sorted areas
        Groups = sortedGroups;
    }
}