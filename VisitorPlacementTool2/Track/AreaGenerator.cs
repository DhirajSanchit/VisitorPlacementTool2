using System;
using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

/// <summary>
/// Class responsible for generating an random area
/// </summary>
public class AreaGenerator
{
    private Random _random;

    public AreaGenerator()
    {
        _random = new Random();
    }
    
    //Generates areas based on the given parameters: The maximum of visitors allowed on an event
    public List<Area> GenerateAreas(int maxVisitorAmount)
    {
        List<Area> areas = new();
        
        //variable for storing the total amount of seats generated
        var generatedseats = 0;
        
        //for loop for storing the iterations, which the needed Areaname will be based on.
        for (var i = 0; ; i++)
        {
            //Amount of rows that will be generated, minimum of 1, maximum of 3
            var rowAmount = _random.Next(1, 4);
            
            //Amount of seats in a row to be generated, minimum of 3, max of 10
            var rowLength = _random.Next(3, 11);
            
            //Calculates the toal amount of seats generated
            generatedseats += rowAmount*rowLength;
            
            //Within the scope of the given case
            //There should not be more seats generated than the maximum amount of visitors allowed on an event
            //Keep generating until that condition is met
            if (generatedseats <= maxVisitorAmount)
            {
                areas.Add(new Area(GenerateName(i), rowAmount, rowLength));
            }
            else
            {   //stops the for loop if the condition is met
                break;
            }
        }
        return areas;
    }
    
    //Generates a name for the area based on the iteration which should be Alphabetical Letters 
    //For example 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'
    public string GenerateName(int areaIndex)
    {
        if (areaIndex > 25)
        {
            //The index is divided by 26, because there are 26 letters in the alphabet
            //The remainder is added to 64 to get the correct letter because ASCII is used
            var first = (char)(areaIndex / 26 -1 + 65 );
            
            //When a certain amount of areas is reached, a second letter is needed
            //The remainder is added to 64 to get the correct letter because ASCII is used
            //For example: 27 = 'AA', 28 = 'AB', 29 = 'AC', 30 = 'AD', 31 = 'AE', 32 = 'AF', 33 = 'AG', 34 = 'AH',
            //35 = 'AI', 36 = 'AJ'
            var second = (char)(areaIndex % 26 + 65 );
            return $"{first}{second}";
        }
        else
        {
            //Ceates an ASCII character from the index for the first 26 areas with areacode 'A'
            var first = (char)(areaIndex % 26 + 65 );
            return first.ToString();
        }
    }

    
    //Manually generated areas for testing purposes or special cases / needs / scenarios
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