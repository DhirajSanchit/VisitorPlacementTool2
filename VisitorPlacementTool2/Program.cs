using System;
using System.Collections.Generic;
using System.Linq;
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
        var removedGroups = new List<VisitorGroup>();
        //TODO: Wrap in functions
        foreach (var visitorGroup in groupContainer.GetGroups())
        {
            //Prepare list for visitors to be rejected and removed
            List<Visitor> ToBeRemoved = new();

            //Adults have to be present
            var hasAdults = false;

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
            actualVisitors -= ToBeRemoved.Count;

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
                removedGroups.Add(visitorGroup);
            }
            else if (visitorGroup.GetVisitors().Count == 0)
            {
                removedGroups.Add(visitorGroup);
            }
        }

        //Verwijder de lege groepen uit de groepcontainer
        removedGroups.ForEach(group => groupContainer.RemoveGroup(group));


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
                var visitor = visitors[^1];
                visitorContainer.RejectVisitor(visitor, "Overcapacity");

                var group = groupContainer.GetGroupById(visitor.GroupId);
                group.RemoveVisitor(visitor);

                var capactiyRemovedVisitors = new List<Visitor>();
                if (!group.HasAdults(competitionDate))
                {
                    FilterByHasAdults(group, capactiyRemovedVisitors, visitorContainer);
                    groupContainer.RemoveGroup(group);
                }
                else if (group.GetVisitors().Count == 0)
                {
                    groupContainer.RemoveGroup(group);
                }

                foreach (var visitorToBeRemoved in capactiyRemovedVisitors)
                {
                    visitors.Remove(visitorToBeRemoved);
                }

                //remove visitor from list
                visitors.Remove(visitor);
            }
        }


        //Sort all areas
        competition.SortAreas();

        //sorts all the available groups
        groupContainer.SortGroups(competitionDate);
        var leftToPlaceGroups = new List<VisitorGroup>();
        //Place the groups in the areas

        
        foreach (var group in groupContainer.GetGroups())
        {
            var visitors = group.GetVisitors();
            visitors.OrderBy(x => x.DateOfBirth);
            var placed = false;
            var numberOfMinors = group.AmountOfKids(competitionDate);
            foreach (var area in competition.Areas)
            {
                //Check for each area if there is enough space for the iterated group
                //And if there is enough space for the minors in the first row
                if (area.AmountOfSeatsAvailable() >= group.GetVisitors().Count &&
                    area.AmountOfAvailableSeatsInFirstRow() >= numberOfMinors)
                {
                    //Keep track of placed minors and adults
                    var childPositions = new List<Coordinate>();
                    var adultPositions = new List<Coordinate>();

                    //plaat groep in area
                    foreach (var visitor in visitors)
                    {
                        //Check for alle minors in de groep
                        if (!visitor.IsAnAdult(competitionDate))
                        {
                            childPositions.Add(placeChild(area, visitor));
                        }

                        //Visitor is volwassen
                        else
                        {
                            adultPositions.Add(PlaceAdult(adultPositions, childPositions, area, visitor, competitionDate));
                        }
                    }

                    placed = true;
                    break;
                }
            }

            if (!placed)
            {
                //All the groups that can't be placed for now are stored.
                leftToPlaceGroups.Add(group);
            }
        }

        //Now check for the groups that couldn't be placed
        foreach (var visitorGroup in leftToPlaceGroups)
        {
            foreach (var visitor in visitorGroup.GetVisitors())
            {
                var placed = false;
                //Iterate through the areas, check if theres a spot free and place the visitor
                //Open for optimization
                foreach (var area in competition.Areas)
                {
                    //Visitor was placed
                    if (placed)
                    {
                        //Stop checking areas, go to the next visitor
                        break;
                    }
                    
                    //Check if there is space for the visitor on every row of the iterated area
                    foreach (var row in area.Rows)
                    {
                        if (placed)
                        {
                            break;
                        }
                        //Check if there is a seat for the visitor in the row
                        foreach (var seat in row.Seats)
                        {
                            if (!seat.IsOccupied())
                            {
                                seat.PlaceVisitor(visitor);
                                placed = true;
                                break;
                            }
                        }
                    }
                }
            }
        }

        //put areas back in old order
        competition.SortAreasByName();
        
        //Creates the layout
        LogVenue(competition, (List<VisitorGroup>)groupContainer.GetGroups(), visitorContainer.rejectedVisitors);
    }

    //Place an adult 
    private static Coordinate PlaceAdult(List<Coordinate> adultPositions, List<Coordinate> childPositions, Area area,
        Visitor visitor, DateTime competitionDate)
    {
        //if group has children
        if (childPositions.Count > 0 && adultPositions.Count == 0)
        {
            return PlaceFirstAdultWithChildren(childPositions, area, visitor);
        }
        return PlaceOtherAdult(adultPositions, area, visitor, competitionDate);
        
    }

    
    //Start checking if the adult can be placed 
    private static Coordinate PlaceFirstAdultWithChildren(List<Coordinate> childPositions,
        Area area, Visitor visitor)
    {
        //Check for the possibilities of placement next to child:
        foreach (var childPosition in childPositions)
        {
            //Check seat to the left
            if (!area.Rows[childPosition.RowNr].Seats[Math.Max(childPosition.SeatNr - 1, 0)]
                    .IsOccupied())
            {
                area.Rows[childPosition.RowNr].Seats[childPosition.SeatNr - 1].PlaceVisitor(visitor);
                return new Coordinate(childPosition.RowNr, childPosition.SeatNr - 1);
            }

            //Check seat behind
            //Math Min/Max used to prevent out of bounds exception
            if (!area.Rows[Math.Min(childPosition.RowNr + 1, area.Rows.Count - 1)].Seats[childPosition.SeatNr]
                    .IsOccupied())
            {
                area.Rows[childPosition.RowNr + 1].Seats[childPosition.SeatNr]
                    .PlaceVisitor(visitor);
                return new Coordinate(childPosition.RowNr + 1, childPosition.SeatNr);
            }

            //Check seat to the right
            if (!area.Rows[childPosition.RowNr]
                    .Seats[Math.Min(childPosition.SeatNr + 1,
                        area.Rows[childPosition.RowNr].Seats.Count - 1)].IsOccupied())
            {
                area.Rows[childPosition.RowNr]
                    .Seats[childPosition.SeatNr + 1].PlaceVisitor(visitor);
                return new Coordinate(childPosition.RowNr, childPosition.SeatNr + 1);
            }

            //Check seat in front
            //Math max used to prevent out of bounds exception, number between -1 and 0 is always 0
            //If childposition.Rownumber = 0
            if (!area.Rows[Math.Max(childPosition.RowNr - 1, 0)].Seats[childPosition.SeatNr]
                    .IsOccupied())
            {
                area.Rows[childPosition.RowNr - 1].Seats[childPosition.SeatNr].PlaceVisitor(visitor);
                return new Coordinate(childPosition.RowNr - 1, childPosition.SeatNr);
            }
        }

        throw new Exception("No place found for adult");
    }

    
    //First adult is already placed, now place the rest of adults or the group contains no minors
    private static Coordinate PlaceOtherAdult(List<Coordinate> adultPositions, Area area, Visitor visitor, DateTime competitionDate)
    {
        var seatNr = 0;
        if (adultPositions.Count == 0)
        {
            //start last rowindex to the left
            switch (area.Rows.Count)
            {
                case 1:
                    //place adult most left option
                    foreach (var seat in area.Rows[0].Seats)
                    {
                        if (!seat.IsOccupied())
                        {
                            seat.PlaceVisitor(visitor);
                            return new Coordinate(0, seatNr);
                        }

                        seatNr++;
                    }

                    break;
                case 2:

                    foreach (var seat in area.Rows[0].Seats)
                    {
                        if (!seat.IsOccupied())
                        {
                            seat.PlaceVisitor(visitor);
                            return new Coordinate(0, seatNr);
                        }

                        seatNr++;
                    }

                    seatNr = area.Rows[1].Seats.Count - 1;
                    for (; seatNr >= 0; seatNr--)
                    {
                        if (!area.Rows[1].Seats[seatNr].IsOccupied())
                        {
                            area.Rows[1].Seats[seatNr].PlaceVisitor(visitor);
                            return new Coordinate(1, seatNr);
                        }
                    }

                    break;
                case 3:
                    foreach (var seat in area.Rows[0].Seats)
                    {
                        if (!seat.IsOccupied())
                        {
                            seat.PlaceVisitor(visitor);
                            return new Coordinate(0, seatNr);
                        }

                        seatNr++;
                    }

                    seatNr = area.Rows[1].Seats.Count - 1;
                    for (; seatNr >= 0; seatNr--)
                    {
                        if (!area.Rows[1].Seats[seatNr].IsOccupied())
                        {
                            area.Rows[1].Seats[seatNr].PlaceVisitor(visitor);
                            return new Coordinate(1, seatNr);
                        }
                    }

                    seatNr = 0;
                    foreach (var seat in area.Rows[2].Seats)
                    {
                        if (!seat.IsOccupied())
                        {
                            seat.PlaceVisitor(visitor);
                            return new Coordinate(2, seatNr);
                        }

                        seatNr++;
                    }

                    break;
            }
        }
        
        //For every adult that isn't the first adult in the group.
        else
        {   
            //Checks for the number of rows in an area
            switch (area.Rows.Count)
            {   
                //If there is only one row
                //Try to place right of the last adult
                case 1:
                    area.Rows[0].Seats[adultPositions.Last().SeatNr + 1].PlaceVisitor(visitor);
                    return new Coordinate(0, adultPositions.Last().SeatNr + 1);
                case 2:
                    //Check for a seat in 2nd row 
                    foreach (var seat in area.Rows[1].Seats)
                    {
                        if (!seat.IsOccupied())
                        {
                            seat.PlaceVisitor(visitor);
                            return new Coordinate(1, seatNr);
                        }

                        seatNr++;
                    }   
                    
                    //No place in the 2nd frow, check the first row, from right to left
                    seatNr = area.Rows[0].Seats.Count - 1;
                    for (; seatNr >= 0; seatNr--)
                    {
                        if (!area.Rows[0].Seats[seatNr].IsOccupied())
                        {
                            area.Rows[0].Seats[seatNr].PlaceVisitor(visitor);
                            return new Coordinate(0, seatNr);
                        }
                    }

                    break;
                case 3:
                    
                    //For every previous placed adult check for a seat relative to them
                    for (int i = adultPositions.Count - 1; i >= 0; i--)
                    {
                        //Check seat to the left
                        if (!area.Rows[adultPositions[i].RowNr].Seats[Math.Max(adultPositions[i].SeatNr - 1, 0)]
                                .IsOccupied())
                        {
                            area.Rows[adultPositions[i].RowNr].Seats[adultPositions[i].SeatNr - 1]
                                .PlaceVisitor(visitor);
                            return new Coordinate(adultPositions[i].RowNr, adultPositions[i].SeatNr - 1);
                        }

                        //
                        //if not last row
                        if (adultPositions[i].RowNr != 2)
                        {
                            //Check seat behind
                            if (!area.Rows[Math.Min(adultPositions[i].RowNr + 1, 2)].Seats[adultPositions[i].SeatNr]
                                    .IsOccupied())
                            {
                                area.Rows[adultPositions[i].RowNr + 1].Seats[adultPositions[i].SeatNr]
                                    .PlaceVisitor(visitor);
                                return new Coordinate(adultPositions[i].RowNr + 1, adultPositions[i].SeatNr);
                            }

                            //if not first row
                            //Check seat behind to the right, diagonally
                            if (adultPositions[i].RowNr != 0)
                            {
                                if (!area.Rows[Math.Min(adultPositions[i].RowNr + 1, 2)]
                                        .Seats[Math.Min(adultPositions[i].SeatNr + 1,
                                            area.Rows[adultPositions[i].RowNr].Seats.Count - 1)].IsOccupied())
                                {
                                    area.Rows[adultPositions[i].RowNr + 1]
                                        .Seats[adultPositions[i].SeatNr + 1].PlaceVisitor(visitor);
                                    return new Coordinate(adultPositions[i].RowNr + 1, adultPositions[i].SeatNr + 1);
                                }
                            }
                        }

                        //Check seat in front
                        if (!area.Rows[Math.Max(adultPositions[i].RowNr - 1, 0)].Seats[adultPositions[i].SeatNr]
                                .IsOccupied())
                        {
                            area.Rows[adultPositions[i].RowNr - 1].Seats[adultPositions[i].SeatNr]
                                .PlaceVisitor(visitor);
                            return new Coordinate(adultPositions[i].RowNr - 1, adultPositions[i].SeatNr);
                        }

                        //Check seat to the right
                        if (!area.Rows[adultPositions[i].RowNr]
                                .Seats[Math.Min(adultPositions[i].SeatNr + 1,
                                    area.Rows[adultPositions[i].RowNr].Seats.Count - 1)].IsOccupied())
                        {
                            area.Rows[adultPositions[i].RowNr]
                                .Seats[adultPositions[i].SeatNr + 1].PlaceVisitor(visitor);
                            return new Coordinate(adultPositions[i].RowNr, adultPositions[i].SeatNr + 1);
                        }

                        //check 2 seats to the right
                        if (!area.Rows[adultPositions[i].RowNr]
                                .Seats[Math.Min(adultPositions[i].SeatNr + 2,
                                    area.Rows[adultPositions[i].RowNr].Seats.Count - 1)].IsOccupied())
                        {
                            area.Rows[adultPositions[i].RowNr]
                                .Seats[adultPositions[i].SeatNr + 2].PlaceVisitor(visitor);
                            return new Coordinate(adultPositions[i].RowNr, adultPositions[i].SeatNr + 2);
                        }
                    }

                    break;
            }
        }
        
        //if all else fails try and still place the visitor
        foreach (var row in area.Rows)
        {
            foreach (var seat in row.Seats)
            {
                if (!seat.IsOccupied())
                {
                    seat.PlaceVisitor(visitor);
                    return new Coordinate(row.Number, seat.Number);
                }
            }
        }


        throw new Exception($"No seat found for {visitor.GroupId}");
    }

    private static Coordinate placeChild(Area area, Visitor visitor)
    {
        var seatnumber = 0;
        //Kijk of de eerste rij vrij is en plaats als het kan
        foreach (var seat in area.Rows[0].Seats)
        {
            if (!seat.IsOccupied())
            {
                seat.PlaceVisitor(visitor);
                return new Coordinate(0, seatnumber);
            }

            seatnumber++;
        }

        foreach (var seat in area.Rows[1].Seats)
        {
            if (!seat.IsOccupied())
            {
                seat.PlaceVisitor(visitor);
                return new Coordinate(1, seatnumber);
            }

            seatnumber++;
        }
     
        foreach (var seat in area.Rows[2].Seats)
        {
            if (!seat.IsOccupied())
            {
                seat.PlaceVisitor(visitor);
                return new Coordinate(2, seatnumber);
            }

            seatnumber++;
        }
        
        throw new Exception("No seat found");
    }


    private static void FilterByHasAdults(VisitorGroup visitorGroup, List<Visitor> ToBeRemoved,
        VisitorContainer visitorContainer)
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
    public static bool FilterRegistrationDeadline(Visitor visitor, Competition.Competition competition,
        List<Visitor> toBeRemoved, VisitorContainer visitorContainer)
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
    private static void LogVenue(Competition.Competition competition, List<VisitorGroup> groups,
        List<Visitor> rejectedVisitors)
    {
        //Layout has to be generated based on with and length
        foreach (var area in competition.Areas)
        {
            Console.WriteLine($"Area {area.Name}");
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
                        var adultStatus = "C";
                        if (area.Rows[j].Seats[i].Visitor.IsAnAdult(competition.CompetitionDate))
                        {
                            adultStatus = "A";
                        }
                        //Prints out the placed visitors with the area and seatcode and the occupant.
                        Console.Write($"[{area.Rows[j].Seats[i].Visitor.GroupId}-{adultStatus}]");
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

        //Prints out the rejected visitors
        Console.WriteLine("Rejected visitors:");
        foreach (var visitor in rejectedVisitors)
        {
            Console.WriteLine($"{visitor.GroupId} - {visitor.Rejection}");
        }

        //Prints out the visitors that have not been handled correctly
        foreach (var group in groups)
        {
            foreach (var visitor in group.GetVisitors())
            {
                var visitorFound = false;
                foreach (var area in competition.Areas)
                {
                    foreach (var row in area.Rows)
                    {
                        foreach (var seat in row.Seats)
                        {
                            if (seat.Visitor == visitor)
                            {
                                visitorFound = true;
                            }
                        }
                    }
                }

                if (!visitorFound)
                {
                    var visitorRejected = false;
                    foreach (var rejectee in rejectedVisitors)
                    {
                        if (rejectee == visitor)
                        {
                            visitorRejected = true;
                        }
                    }

                    if (!visitorRejected)
                    {
                        Console.WriteLine($"Visitor {visitor.GroupId} was not accounted for");
                    }
                }
            }
        }
    }
}