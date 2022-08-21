using System.Collections.Generic;

namespace VisitorPlacementTool2.Competition;

public class Organisation
{
    public string Name { get; }
    public List<Competition> Events { get; set; }

    public Organisation(string name, List<Competition> Events)
    {
        Name = name;
        this.Events = Events;
    }

    public void AddAreaToStage()
    {
    }
}