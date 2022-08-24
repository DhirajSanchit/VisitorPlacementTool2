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
    
    
    //Method for getting the visitorgroup with the highest priority:
    
    public void SortGroups(DateTime competitionDate)
    
    {   
         
        //Sort the groups by the amount of kids in a group
        Groups.Sort((x, y) => y.AmountOfKids(competitionDate).CompareTo(x.AmountOfKids(competitionDate)));
    
        //Categorization list
        var sizeCategory = new List<VisitorGroup>();
            
        //LastSize amount of groups with the same amount of kids for comparing the next group
        var lastSize = Groups[0].AmountOfKids(competitionDate);
        List<VisitorGroup> sortedGroups = new();
 
        //Check the groups
        foreach (var group in Groups)
        {
            //If amount of kids is the same as the last group, add the group to the same category
            if (group.AmountOfKids(competitionDate) != lastSize)
            {
                //Add the group to the category and sort it
                //For example: if the last group had 4 kids, the next group will have 3 kids, the this group will be added
                sizeCategory.Sort((x, y) => x.GetVisitors().Count.CompareTo(y.GetVisitors().Count));
                sortedGroups.AddRange(sizeCategory);
                    
                //Empty the list to start for the new size category
                sizeCategory = new();
                
                //Reset the index for the new size category
                lastSize = group.AmountOfKids(competitionDate);
            }

            //Add the category to the list.
            sizeCategory.Add(group);
        }
          
        //After the last iteration, the range remains in the list, so we add it to the sorted list
        sizeCategory.Sort((x, y) => x.GetVisitors().Count.CompareTo(y.GetVisitors().Count));
        sortedGroups.AddRange(sizeCategory);
        
        //Reassign the sorted groups to in container
        Groups = sortedGroups;
    }
}