using System;
using VisitorPlacementTool2.Containers;
using VisitorPlacementTool2.Group;

namespace VisitorPlacementTool2;

public class Program
{
    public static void Main(string[] args)
    {
        var maxVisitors = MaxVisitors();
        var competitionDate = CompetitionDate();
        var registerDeadline = RegisterDeadline(competitionDate);
        var actualVisitors = ActualVisitors();
        GroupGenerator groupGenerator = new GroupGenerator();

        VisitorContainer visitorContainer = new();
        GroupContainer groupContainer = new GroupContainer
        (
            groupGenerator.GenerateMaxVisitorGroups(actualVisitors)
        );
        
        var competition = new Competition.Competition(competitionDate, registerDeadline, maxVisitors);

        //Telaat
        foreach (var visitorGroup in groupContainer.GetGroups())
        {
            bool HasAdults = false;
             foreach (var visitor in visitorGroup.GetVisitors())
            {
                if (visitor.RegisteredTime > registerDeadline)
                {
                    visitorGroup.RemoveVisitor(visitor);
                    visitorContainer.RejectVisitor(visitor, "Registration was to late");
                }
                
            }
             
        }


        //Weigeren bij teveel bezoekers
        // if (actualVisitors > competition.GetNumberOfSeats())
        // {
        //     
        // }
        
            
        
        
        
        LogVenue(competition);
    }
    
    private static int ActualVisitors()
    {
        Console.WriteLine("How many are trying to visit?");
        var input = Console.ReadLine();
        int output;
        if (int.TryParse(input, out output))
        {
            // do something
            return output;
        }
        Console.WriteLine("Invalid input, please enter a number");
        return ActualVisitors();
    }
    
    //Todo: add error handling for date in past
    private static DateTime RegisterDeadline(DateTime competitionDate)
    {
        Console.WriteLine("How many days in advance should visitors register?");
        var input = Console.ReadLine();
        int output;
        if (int.TryParse(input, out output))
        {
            // do something
            return competitionDate.AddDays(-output);
        }

        Console.WriteLine("Invalid input, please enter a number");
        return RegisterDeadline(competitionDate);
    }

    //Todo: add error handling for date in past
    private static DateTime CompetitionDate()
    {
        Console.WriteLine("What is the date of the competition (dd:mm:yyyy) ?");
        var input = Console.ReadLine();

        try
        {
            return DateTime.Parse(input ?? string.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine("Format could not be read, please try again.");
            return CompetitionDate();
        }
    }

    //TODO: add error handling for negative numbers
    private static int MaxVisitors()
    {
        Console.WriteLine("How many visitors are allowed in the event?");
        var input = Console.ReadLine();
        int output;
        if (int.TryParse(input, out output))
        {
            // do something
            return output;
        }
        
        Console.WriteLine("Invalid input, please enter a number");
        return MaxVisitors();
    }

    private static void LogVenue(Competition.Competition competition)
    {
        foreach (var area in competition.Areas)
        {
            var rowsCount = area.Rows.Count;
            var rowWidth = area.Rows[0].Seats.Count;

            for (var i = 0; i < rowWidth; i++)
            {
                for (var j = 0; j < rowsCount; j++)
                {
                    if (area.Rows[j].Seats[i].IsOccupied())
                    {
                        Console.Write($"[{area.Rows[j].Seats[i].Visitor.GroupId}]");
                    }
                    else
                    {
                        Console.Write("[ ]");
                    }
                }

                Console.Write("\r\n");
            }

            Console.Write("\r\n");
        }
    }
}