using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

public class Area
{
    public string Name { get; set; }
    public List<Row> Rows { get; set; }
    
    public Area()
    {
        Rows = new();
    }

    public Area(string areaName, int rowAmount, int rowLength)
    {
        Rows = new List<Row>();
        Name = areaName;
        for (int i = 0; i < rowAmount; i++)
        {
            Rows.Add(new Row(rowLength, i+1));
        }

    }

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