using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Database database = Database.GetInstance();

            /* REMARKS:

                * Your task is to create five LINQ queries for a flights database.
                * You can create the queries in any order you want.
                * You are obligated to use LINQ query expressions syntax.
                * Dot notation is allowed only if it is necessary.
                * You can use aggregation methods like Sum, Min or Count etc, when required.
                * You cannot process data on your own - with for or foreach etc.
                * When asked for limited output (three top rows) you may use Take method.
                * You should create anonymous class to output/project data from your query.

                * Licenses Table - Stores information about all valid licenses: (Category,ValidSince,ExpirationDate).
                * People Table - Stores information about all flight staff: (Name,Surname,LicenseID).
                * Aircrafts Table - Stores information about all available planes: (RegistrationNumber,Brand,Weight,Capacity).
                * Airports Table - Stores information about all operational airports: (Country,City,CodeIATA,CodeICAO).
                * Flights Table - Stores information about all flights already taken: (FlightNumber,AircraftID,AirportOriginID,AirportDestinationID,Duration).
                * Crews Table - Stores information about all crew members: (FlightID,PersonID,Role,Salary).
            */

            /* STAGE_1 (0.5 Pts)
                * List Surname and Name for all People present in the database.
                * Entries should be sorted Ascending by Surname and then Descending by Name.
            */
            {
                Console.WriteLine("--------------- STAGE_1 (0.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var people = from p in database.People
                                 orderby p.Surname, p.Name descending
                                 select new { p.Surname, p.Name };

                    foreach (var p in people)
                        Console.WriteLine($" {p.Surname}, {p.Name}");
                }

                

                Console.WriteLine();
            }


            /* STAGE_2 (0.5 Pts)
                * List Brand, Weight, TotalWeight for 3 Airplanes with greatest TotalWeight.
                * Entries should be sorted Descending by TotalWeight.
                * REMARK:
                    * TotalWeight is sum of Weight of the Airplane and its Capacity multiplied by 70 kilograms (default weight of a single person).
                    * Use LET statement to compute TotalWeight only once and reuse its value.
                    * Weight of an Airplane is given in Tons, TotalWeight should be outputed in Kilograms.
            */
            {
                Console.WriteLine("--------------- STAGE_2 (0.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var planes = (from p in database.Aircrafts
                                 let TotalWeight = p.Weight * 1000 + p.Capacity * 70
                                 orderby TotalWeight descending
                                 select new { p.Brand, p.Weight, TotalWeight }).Take(3);

                    foreach (var p in planes)
                        Console.WriteLine($" {p.Brand}, Weight {p.Weight} Tons - Total {p.TotalWeight} Kilograms");
                }

                Console.WriteLine();
            }


            /* STAGE_3 (1.0 Pts)
                * List Brand and RegistrationNumber for all Airplanes that have less than 2 Flights.
                * Entries should be sorted Ascending by its Brand.
                * REMARK:
                    * Use JOIN .. INTO statement and Count method to group entries and calculate number of Flights taken by an Airplane.
            */
            {
                Console.WriteLine("--------------- STAGE_3 (1.0 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var planes = from p in database.Aircrafts
                                 join f in database.Flights on p.ID equals f.AircraftID into l
                                 where (l.Count() < 2)
                                 orderby p.Brand
                                 select new { p.Brand, p.Registration };
                                 

                    foreach (var p in planes)                        
                            Console.WriteLine($" {p.Brand}, {p.Registration}");


                }

                Console.WriteLine();
            }

            /* STAGE_4 (1.5 Pts)
                * List CodeIATA,AverageDuration,FlightsNo for all Airports. Where:
                    * AverageDuration represents average Duration of all Departure Flights from given Airport.
                    * FlightsNo represents overall amount of Departure Flights from given Airport.
                * REMARKS:
                    * Use GROUP statement to group entries.
                    * Use Average and Count methods to calculate average Duration in minutes and the amount of Departure Flights from an Airport.
            */
            {
                Console.WriteLine("--------------- STAGE_4 (1.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var airports = from a in database.Airports
                                   join f in database.Flights
                                   on a.ID equals f.AirportDestinationID
                                   group f by a.CodeIATA into l
                                   orderby l.Key
                                   select new { CodeIATA = l.Key, avgduration = l.Average(f => f.Duration.TotalMinutes), fcount = l.Count() };

                    foreach (var a in airports)
                        Console.WriteLine($"{a.CodeIATA}({a.fcount} Arrival Flights) - {a.avgduration} Minutes");                    
                    
                }

                Console.WriteLine();
            }


            /* STAGE_5 (1.5 Pts)
                * List Surname,Name,AircraftCategory of those People that didn't attend any flight.
                * Entries should be sorted Ascending by Surname and Name.
                * REMARKS:
                    * Use JOIN .. INTO statement and Count method to group entries and calculate number of Flights taken by a Person.
            */
            {
                Console.WriteLine("--------------- STAGE_5 (1.5 Pts) ---------------");

                /* IMPLEMENTATION HERE */
                {
                    var people = from p in database.People
                                 join c in database.Crews
                                 on p.ID equals c.PersonID into crew
                                 where crew.Count() == 0
                                 select new { p.Surname, p.Name, p.LicenseID } into x
                                 join l in database.Licenses
                                 on x.LicenseID equals l.ID
                                 orderby x.Surname, x.Name descending
                                 select new { x.Surname, x.Name, l.AircraftCategory };

                    foreach (var p in people)
                        Console.WriteLine($"{p.Surname}, {p.Name}, {p.AircraftCategory} License");
                }

                Console.WriteLine();
            }
        }
    }
}
