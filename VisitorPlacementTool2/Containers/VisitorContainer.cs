using System.Collections.Generic;
using System.Drawing;
using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Containers;


/// <summary>
/// Container class that stores visitors.
/// </summary>
public class VisitorContainer
{
    //Visitor list that stores visitors which are rejected with the reason.
    public List<Visitor> rejectedVisitors;

    public VisitorContainer()
    {
        rejectedVisitors = new();
    }

    //Adds a visitor to the list of rejected visitors with the reason why.
    public void RejectVisitor(Visitor visitor, string reason)
    {
        visitor.Rejection = reason;
        rejectedVisitors.Add(visitor);
    }
    
    //Getter
    public IReadOnlyCollection<Visitor> GetRejectedVisitors()
    {
        return rejectedVisitors;
    }
}