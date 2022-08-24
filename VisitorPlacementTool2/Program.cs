using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using VisitorPlacementTool2.Competition;
using VisitorPlacementTool2.Containers;
using VisitorPlacementTool2.Group;
using VisitorPlacementTool2.Track;
using VisitorPlacementTool2.Visitors;


namespace VisitorPlacementTool2;

public class Program
{
    public static void Main(string[] args)
    {
        //Code below creates an event based by the requirements of the user.
        
        //Max visitors based on permit
        var maxVisitors = MaxVisitors();
        
        //Date the event is based on
        var competitionDate = CompetitionDate();
        
        //Deadline users have had to be registered by;
        var registerDeadline = RegisterDeadline(competitionDate);
        
        //Its possible that more visitors want have registered than the permit allows
        var actualVisitors = ActualVisitors();
        
        //Generaates groups
        GroupGenerator groupGenerator = new GroupGenerator();

        //Contains the rejected visitors
        VisitorContainer visitorContainer = new();
        
        //Container that saves the generated groups
        GroupContainer groupContainer = new GroupContainer
        (
            //Creates a max amount of groups based on the actual ammount of visitors  
            groupGenerator.GenerateMaxVisitorGroups(actualVisitors)
        );

        //Intantiates a competition based on predefined values;
        var competition = new Competition.Competition(competitionDate, registerDeadline, maxVisitors);

        //Filter visitorgroups on register date and composition
        
        //TODO: Wrap in functions
        foreach (var visitorGroup in groupContainer.GetGroups())
        {   
            //Prepare list for visitors to be rejected and removed
            List<Visitor> ToBeRemoved = new();

            //Adults have to be present
            var hasAdults = false;
            
            //Check if the visitors in the group who have registered on time
            //TODO: FILTER
            //TODO: Extract method date filter, adult filter


            foreach (var visitor in visitorGroup.GetVisitors())
            {
                if (FilterRegistrationDeadline(visitor, competition, ToBeRemoved, visitorContainer))
                {
                    hasAdults = true;
                }

                //Now check on the visitor-groups that remain if they contain adults
                //Check for each visitor group that remains if there are adults in it
                //Do they not contain adults? Add them to the ToBeRemoved List with the reason of rejection
            
                //Compare the visitorslists against ToBeRemoved, if they are in ToBeRemoved, remove them from the visitorgroup
            
                //Remove the empty groups from the groupcontainer
                //Place the remaining groups.
            
                //TODO: Extract method adult filter
                // foreach (var visitor in visitorGroup.GetVisitors())
            }
            
            //Collected all the visitors that have been filtered and 
            actualVisitors-= ToBeRemoved.Count;
            
            foreach (var visitor in ToBeRemoved)
            {
                // remove them from visitors that are allowed to enter
                visitorGroup.RemoveVisitor(visitor);
            }
            
            if (!hasAdults)
            {
                ToBeRemoved.Clear();
                FilterByHasAdults(visitorGroup, ToBeRemoved, visitorContainer); 
                actualVisitors -= ToBeRemoved.Count;
            }
        }
        
        //Overgebleven bezoekers aan het einde weigeren op op capactiteit.
        if (actualVisitors > competition.GetNumberOfSeats())
        {
            List<Visitor> visitors = new();
            foreach (var group in groupContainer.GetGroups())
            {
                foreach (var visitor in group.GetVisitors())
                { 
                    visitors.Add(visitor);
                }
            } 
            visitors.Sort((x, y) => x.RegisteredTime.CompareTo(y.RegisteredTime));
            
            for (int i = 0; visitors.Count > competition.GetNumberOfSeats(); i++)
            { 
                //return last visitor in list
                var visitor = visitors[visitors.Count-1];
                visitorContainer.RejectVisitor(visitor, "Overcapacity");

                var group = groupContainer.GetGroupById(visitor.GroupId);
                group.RemoveVisitor(visitor);

                var capactiyRemovedVisitors = new List<Visitor>();
                FilterByHasAdults(group, capactiyRemovedVisitors, visitorContainer);
                
                foreach (var visitorToBeRemoved in capactiyRemovedVisitors)
                {
                    visitors.Remove(visitorToBeRemoved);
                }
                //remove visitor from list
                visitors.Remove(visitor);
            }
        }
        
        List<VisitorGroup> sortedGroups = new(); 
        
        //Loop through all the groups
        competition.SortAreas();
        groupContainer.SortGroups(competitionDate);

        //Creates the layout
        LogVenue(competition);
    }

    

    private static void FilterByHasAdults(VisitorGroup visitorGroup, List<Visitor> ToBeRemoved, VisitorContainer visitorContainer)
    {
        foreach (var vistor in visitorGroup.GetVisitors())
        {
            ToBeRemoved.Add(vistor);
        }

        foreach (var visitor in ToBeRemoved)
        {
            // remove them from visitors that are allowed to enter
            visitorGroup.RemoveVisitor(visitor);
            visitorContainer.RejectVisitor(visitor, "Group contains no adults");
        }
    }


    //Refactor: remove dependecy to competition
    public static bool FilterRegistrationDeadline(Visitor visitor, Competition.Competition competition, List<Visitor> toBeRemoved ,VisitorContainer visitorContainer)
    {
        if (visitor.RegisteredTime > competition.RegisterDeadline)
        {
            //Have not met the deadline, remove
            toBeRemoved.Add(visitor);
                    
            //Save the the removed visitors with reason of rejection
            visitorContainer.RejectVisitor(visitor, "Registration was too late");
        }

        else 
        {
          if (visitor.IsAnAdult(competition.CompetitionDate))
          {
              return true;
          }
        }

        return false;
    }

    //Asks for the Actual visitors that have shown up
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

    //Asks for the deadline users have to register by
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

    //Asks for the date the event is based on
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

    //Asks for the max amount of visitors allowed
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

    //Creates the layout of the venue
    private static void LogVenue(Competition.Competition competition)
    {
        //Layout has to be generated based on with and length
        foreach (var area in competition.Areas)
        {
            //Calculate the amount of rows
            var rowsCount = area.Rows.Count;
            
            //calculate the row width based on the amount of seats
            var rowWidth = area.Rows[0].Seats.Count;

            //prints out the layout to the console in nested for loops for a rectangular layout
            for (var i = 0; i < rowWidth; i++)
            {
                for (var j = 0; j < rowsCount; j++)
                {
                    if (area.Rows[j].Seats[i].IsOccupied())
                    {
                        //Prints out the placed visitors with the area and seatcode and the occupant.
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