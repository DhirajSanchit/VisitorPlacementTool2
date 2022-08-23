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
}