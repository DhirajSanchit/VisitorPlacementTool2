using System.Collections.Generic;
using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Group;

public class VisitorGroup
{
    private List<Visitor> Visitors;
    private int Id; 
    public VisitorGroup(List<Visitor> visitors)
    {
        Visitors = visitors;
    }
    
    public IReadOnlyCollection<Visitor> GetVisitors()
    {
        return Visitors;
    }
    
    public int GetId()
    {
        return Id;
    }
        
    public void SetId(int id)
    {
        Id = id;
    }
    
    public void RemoveVisitor(Visitor visitor)
    {
        Visitors.Remove(visitor);
    }

}