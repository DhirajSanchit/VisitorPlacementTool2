using System;
using System.Collections.Generic;
using VisitorPlacementTool2.Track;

namespace VisitorPlacementTool2.Competition
{
    
    /// <summary>
    /// Class represents an event. Competition is used, because "Event" is a reserved class etc. in C#.
    ///
    /// According to the case:
    /// -a list of areas for the event is created.
    /// -Visitors should have registered to the event before a date 
    /// -Event should hold a maximum of visitors, based on a permit.
    /// </summary>
    public class Competition
    {
        public DateTime CompetitionDate { get; set; }
        public List<Area> Areas { get; set; }
        public DateTime RegisterDeadline { get; set; }
        public int MaxVisitorAmount { get; set; }

        public Competition()
        {
            
        }
        
        //Constructor for the competition.
        //Takes a date for input, a deadline for registration, and a maximum amount of visitors used for the simulation.
        public Competition(DateTime competitionDate, DateTime registerDeadline, int maxVisitorAmount)
        {
            AreaGenerator areaGenerator = new AreaGenerator();
            Areas =  areaGenerator.GenerateAreas(maxVisitorAmount);
            CompetitionDate = competitionDate; 
            RegisterDeadline = registerDeadline;
            MaxVisitorAmount = maxVisitorAmount;
        }

        //Returns a total number of seats generated for the event.
        public int GetNumberOfSeats()
        {
            int count = 0;
            foreach( var area in Areas)
            {
                count+= area.GetNumberOfSeats();
                
            }
            return count;
        }


        //Methods below are from previous attempts of the project.
        //Saved for reference
        //Todo: Remove code below when not needed anymore.

        //returns all occupied seats
        // private List<Seat> OccupiedSeats(List<Area> areas)
        // {
        //     List<Seat> OccupiedSeats = new List<Seat>();
        //     foreach (var area in areas)
        //     {
        //         foreach (var row in area.Rows)
        //         {
        //             foreach (var seat in row.Seats)
        //             {
        //                 if (seat.IsOccupied())
        //                 {
        //                     OccupiedSeats.Add(seat);
        //                 }
        //             }
        //         }
        //     }
        //
        //     return OccupiedSeats;
        // }

        //todo do we need this?
        // private int OccupiedSeatsRow2ndAnd3rd(Area area)
        // {
        // var occupiedSeats = 0;
        // foreach (Seat seat in area.Seats)
        // {
        //     if (seat.InFirstRow == false && seat.Occupied == true)
        //     {
        //         occupiedSeats++;
        //     }
        // }
        // return occupiedSeats;
        // }

        //todo do we need this?
        // public void AdultGroupSeatOccupation(List<Area> Areas, Group.Group group, int totalMembersInGroup, List<Visitor> SeatedVisitors)
        // {
        //     foreach (var area in Areas)
        //     {
        //         if (totalMembersInGroup <= area.RowLength)
        //         {
        //             foreach (Visitor visitor in group.Visitors)
        //             {
        //                 if (visitor.SeatCode == null)
        //                 {
        //                     foreach (Seat seat in area.Seats)
        //                     {
        //                         //Check if occupied or first row is free
        //                         if (seat.InFirstRow == false && seat.Occupied == false)
        //                         {
        //                             seat.Visitor = visitor;
        //                             seat.Occupied = true;
        //                             visitor.SeatCode = seat.SeatCode;
        //                             SeatedVisitors.Add(visitor);
        //                             break;
        //                         }
        //                     }
        //                 }
        //             }
        //         }
        //         else if (totalMembersInGroup <= (area.RowLength + area.RowLength))
        //         {
        //             foreach (Visitor visitor in group.Visitors)
        //             {
        //                 foreach (Seat seat in area.Seats)
        //                 {
        //                     //Check if occupied or first row is free
        //                     if (seat.InFirstRow == false && seat.Occupied == false)
        //                     {
        //                         seat.Visitor = visitor;
        //                         seat.Occupied = true;
        //                         visitor.SeatCode = seat.SeatCode;
        //                         SeatedVisitors.Add(visitor);
        //                         break;
        //                     }
        //                 }
        //             }
        //         }
        //         break;
        //     }
        // }

        //todo do we need this?
        // public void GroupWithMinorsOccupation(List<Area> areas, Group.Group group, int TotalMembersInGroup,
        //     List<Visitor> SeatedVisitors)
        // {
        //     foreach (var area in areas)
        //     {
        //         if (TotalMembersInGroup <= area.RowLength)
        //         {
        //             foreach (Visitor visitor in group.Visitors)
        //             {
        //                 foreach (Seat seat in area.Seats)
        //                 {
        //                     if (seat.Occupied == false && seat.InFirstRow)
        //                     {
        //                         seat.Visitor = visitor;
        //                         seat.Occupied = true;
        //                         visitor.SeatCode = seat.SeatCode;
        //                         SeatedVisitors.Add(visitor);
        //                         break;
        //                     }
        //                 }
        //             }
        //
        //             break;
        //         }
        //         else
        //         {
        //             //Parent and kid condition
        //         }
        //     }
        // }

        //todo do we need this?
        // public int FirstRowOccupied(List<Seat> OccupiedSeats)
        // {
        //     int firstRowOccupied = 0;
        //     foreach (var seat in OccupiedSeats)
        //     {
        //         if (seat.InFirstRow)
        //         {
        //             firstRowOccupied++;
        //         }
        //     }
        //
        //     return firstRowOccupied;
        // }

        //todo do we need this?
        // public int LastRowOccupied(List<Seat> occupiedSeats)
        // {
        //     int firstRowOccupied = 0;
        //     foreach (var seat in occupiedSeats)
        //     {
        //         if (!seat.InFirstRow)
        //         {
        //             firstRowOccupied++;
        //         }
        //     }
        //
        //     return firstRowOccupied;
        // }



        //todo refactor this
        // Checks above 
        // public List<Visitor> PlaceSeat(Group.Group group, List<Area> areas)
        // {
        //     List<Visitor> seatedVisitors = new List<Visitor>();
        //     // hier geeft ik de bezoeker/groep een stoel(en).
        //     //check hoe groot de groep is
        //     var occupiedSeats = OccupiedSeats(areas);
        //
        //     //hoeveel minderjarige zitten er in de groep:
        //     var totalMembersInGroup = group.CountVisitors(group.Visitors);
        //     var amountOfMinors = group.CountMinors(group.Visitors);
        //
        //     // er zijn nog geen stoelen bezet:
        //     if (occupiedSeats.Count == 0)
        //     {
        //         if (group.AmountofMinors == 0)
        //         {
        //             AdultGroupSeatOccupation(areas, group, totalMembersInGroup, seatedVisitors);
        //         }
        //
        //         // er zijn kinderen onder 12 aanwezig
        //         if (amountOfMinors > 0)
        //         {
        //             GroupWithMinorsOccupation(areas, group, totalMembersInGroup, seatedVisitors);
        //         }
        //     }
        //
        //     // er zijn al verschillende stoelen bezet:
        //     if (occupiedSeats.Count > 0)
        //     {
        //         if (group.AmountOfAge == 0)
        //         {
        //             foreach (var area in areas)
        //             {
        //                 var occupation = OccupiedSeatsRow2ndAnd3rd(area);
        //                 var rowLength = area.Seats.Count / area.rowCount;
        //                 var availableSeats = area.Seats.Count - rowLength - occupation;
        //
        //                 if (totalMembersInGroup <= availableSeats)
        //                 {
        //                     foreach (var visitor in group.Visitors)
        //                     {
        //                         foreach (Seat seat in area.Seats)
        //                         {
        //                             //kijken of er niemand op de stoel zit en kijken for het de eerste rij is.
        //                             if (seat.InFirstRow == false && seat.Occupied == false)
        //                             {
        //                                 seat.Visitor = visitor;
        //                                 seat.Occupied = true;
        //                                 visitor.SeatCode = seat.SeatCode;
        //                                 seatedVisitors.Add(visitor);
        //                                 break;
        //                             }
        //                         }
        //                     }
        //
        //                     break;
        //                 }
        //             }
        //         }
        //
        //         if (group.AmountOfAge > 0)
        //         {
        //         }
        //     }
        //
        //     return seatedVisitors;
        // }
    }
}