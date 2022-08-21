using System;
using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

public class AreaGenerator
{
    //Generate a Area with a random number of rows and seats
    private Random _random;

    public AreaGenerator()
    {
        _random = new Random();
    }


    public List<Area> GenerateAreas(int maxVisitorAmount)
    {
        List<Area> areas = new();
        var generatedseats = 0;
        for (var i = 0; ; i++)
        {
            var rowAmount = _random.Next(1, 4);
            var rowLength = _random.Next(3, 11);
            generatedseats += rowAmount*rowLength;
            
            if (generatedseats <= maxVisitorAmount)
            {
                areas.Add(new Area(GenerateName(i), rowAmount, rowLength));
            }
            else
            {
                break;
            }
        }
        return areas;
    }
    
    public static string GenerateName(int areaIndex)
    {
        areaIndex += 1;
        if (areaIndex > 26)
        {
            var first = (char)( areaIndex / 26 + 64 );
            var second = (char)( areaIndex % 26 + 64 );
            return $"{first}{second}"; 
            
        }
        else
        {
            var first = (char)( areaIndex % 26 + 64 );
            return first.ToString();
        }
    }

    public Area GenerateArea(string areaName)
    {
        if (string.IsNullOrWhiteSpace(areaName))
        {
            throw new ArgumentException("Area Name cannot be empty.");
        }
        
        Area area = new Area
        (areaName,
            _random.Next(1, 4),
            _random.Next(3, 11)
        );
        //generate a random number of rows and seats and add them to the area
        return area;
    }
    
    
}