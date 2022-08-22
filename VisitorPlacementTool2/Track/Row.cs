using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

/// <summary>
/// Class represents a row in an area, containing a collection of seats visitors can be placed in
/// </summary>
public class Row
{
    
    //Row needs to be recognised by its number
    public int Number { get; set; }
    //Row needs to be able to see if the row is full or not
    public bool Occupied { get; set; }
    //Row needs to be able to see how many seats are in the row
    public List<Seat> Seats { get; set; }
    
    //Constructor for the row class, takes a rowlength and number as parameters
    //For example: Row(10, 1) would create a row with 10 seats and number 1
    //Multiple rows can be created with different rowlengths and numbers
    public Row(int length, int RowNr)
    {
        
        Number = RowNr;
        Seats = new List<Seat>();
        
        //Loop to create seats in the row
        for (int i = 0; i < length; i++)
        {
            Seats.Add(new Seat(i+1));
        }
    }
}