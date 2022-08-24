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

        
        //This method sorts the areas by the number of seats and categorizes them by seatnumber.
        public void SortAreas()
        
        {
            //Sort the areas based on the total area size
            Areas.Sort((x, y) => y.GetNumberOfSeats().CompareTo(x.GetNumberOfSeats()));
            
            //Categorize the areas based on the seatnumber
            var sizeCategory = new List<Area>();
            
            //Compare the areas based on the seat Amount and save them in a list.
            var lastSize = Areas[0].GetNumberOfSeats();
            List<Area> sortedAreas = new();

            
            foreach (var area in Areas)
            {
                //If the area size is not the same as the last size,
                if (area.GetNumberOfSeats() != lastSize)
                {
                    
                    //Sort the list by rowlength and save it in a new list.
                    sizeCategory.Sort((x, y) => y.Rows[0].Seats.Count.CompareTo(x.Rows[0].Seats.Count));
                    sortedAreas.AddRange(sizeCategory);
                    
                    //Empty the list to start for the new size category
                    sizeCategory = new();
                    lastSize = area.GetNumberOfSeats();
                }

                //Add the area to the list.
                sizeCategory.Add(area);
            }
            
            //Sort the last category and save it in a new list for the last size category.
            sizeCategory.Sort((x, y) => x.Rows[0].Seats.Count.CompareTo(y.Rows[0].Seats.Count));
            sortedAreas.AddRange(sizeCategory);
            
            //Save the sorted areas
            Areas = sortedAreas;
        }

        //Method sorts the areas based by the alphabetical area code:
        //For example A, B, C, D, E...
        //Continued by AA, AB, AC...
        public void SortAreasByName()
        {
            var shortNameAreas = new List<Area>();
            var mediumNameAreas = new List<Area>();
            
            foreach (var area in Areas)
            {
                if (area.Name.Length == 1)
                {
                    shortNameAreas.Add(area);
                }
                else
                {
                    mediumNameAreas.Add(area);
                }
            }
            
            //sort alphabetically by name
            shortNameAreas.Sort((x, y) => x.Name.CompareTo(y.Name));
            mediumNameAreas.Sort((x, y) => x.Name.CompareTo(y.Name));

            Areas = shortNameAreas;
            Areas.AddRange(mediumNameAreas);
        }
    }
}