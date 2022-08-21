using VisitorPlacementTool2.Visitors;

namespace VisitorPlacementTool2.Track;

public class Seat
{
    public int Number { get; set; }
    public Visitor Visitor { get; set; }

    public Seat(int seatNr)
    {
        Number = seatNr;
    }

    public bool PlaceVisitor(Visitor visitor)
    {
        if (IsOccupied()) return false;

        Visitor = visitor;
        return true;
    }

    public bool IsOccupied()
    {
        return !Equals(Visitor, default(Visitor));
    }
}