using System.Collections.Generic;
using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Containers;

public class VisitorContainer
{
    private List<Visitor> rejectedVisitors;
    
    public void RejectVisitor(Visitor visitor, string reason)
    {
        visitor.Rejection = reason;
        rejectedVisitors.Add(visitor);
    }
    
    public IReadOnlyCollection<Visitor> GetRejectedVisitors()
    {
        return rejectedVisitors;
    }
}