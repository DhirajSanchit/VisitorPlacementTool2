using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

public class Row
{
    
    public int Number { get; set; }
    public bool Occupied { get; set; }
    public List<Seat> Seats { get; set; }
    
    public Row(int length, int RowNr)
    {
        Number = RowNr;
        Seats = new List<Seat>();
        for (int i = 0; i < length; i++)
        {
            Seats.Add(new Seat(i+1));
        }
    }
}