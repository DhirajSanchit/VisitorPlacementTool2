using System;
using System.Collections.Generic;

namespace VisitorPlacementTool2.Track;

/// <summary>
/// Purpose of this class is the stage as a whole.
/// </summary>

public class Stage
{
    public List<Area> Areas { get; set; }

    public Stage(List<Area> areas)
    {
        Areas = areas;
    }
    
    public Area GetArea(string areaChar)
    {
        return Areas.Find(a => a.Name == areaChar) ?? throw new ArgumentException("Area not found");
    }
    
    public List<Area> GetAllAreas()
    {
        return Areas;
    }
}   