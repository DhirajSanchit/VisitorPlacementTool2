using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

/// <summary>
/// this class is used to store the data of the area which visitors can be placed in
/// containing a list of Rows
public class Area
{
    public string Name { get; set; }
    public List<Row> Rows { get; set; }
    
    //Empty Constructor
    public Area()
    {
        Rows = new();
    }

    
    //Constructor that creates a rectangular area with the given number of rows and columns
    public Area(string areaName, int rowAmount, int rowLength)
    {
        Rows = new List<Row>();
        Name = areaName;
        for (int i = 0; i < rowAmount; i++)
        {
            Rows.Add(new Row(rowLength, i+1));
        }

    }

    //Returns the number of rows seats in the area
    public int GetNumberOfSeats()
    {
        int count = 0;
        foreach (var row in Rows)
        {
            count += row.Seats.Count;
        }
        return count;
    }
}