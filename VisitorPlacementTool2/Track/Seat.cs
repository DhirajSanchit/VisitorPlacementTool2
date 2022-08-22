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

    //"places" the visitor in the seat
    public bool PlaceVisitor(Visitor visitor)
    {
        if (IsOccupied()) return false;

        Visitor = visitor;
        return true;
    }
    
    //Checks if the seat is occupied
    public bool IsOccupied()
    {
        return !Equals(Visitor, default(Visitor));
    }
}