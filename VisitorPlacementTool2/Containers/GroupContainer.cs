using System.Collections.Generic;
using VisitorPlacementTool2.Group;

namespace VisitorPlacementTool2.Containers;

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
}