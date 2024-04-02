using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Assignment_4
{

    public class Seat
    {
        public int Row { get; set; }
        public SeatLabel Label { get; set; }
        public bool IsBooked { get; set; }
        public Passenger Passenger { get; set; }

        public Seat(int row, SeatLabel label)
        {
            Row = row;
            Label = label;
            IsBooked = false;
            Passenger = null;
        }

        public override string ToString()
        {
            return $"{Row} {Label}";
        }
    }

    public class Passenger
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public SeatPreference Preference { get; set; }
        public Seat BookedSeat { get; set; }

        public Passenger(string firstName, string lastName, SeatPreference preference)
        {
            FirstName = firstName;
            LastName = lastName;
            Preference = preference;
            BookedSeat = null;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    public enum SeatPreference
    {
        WindowSeat,
        AisleSeat
    }

    public class Flight
    {
        public readonly List<List<Seat>> SeatingChart;

        public Flight()
        {
            SeatingChart = new List<List<Seat>>();

            for (int i = 1; i <= 12; i++)
            {
                var row = new List<Seat>();
                for (SeatLabel label = SeatLabel.A; label <= SeatLabel.D; label++)
                {
                    row.Add(new Seat(i, label));
                }
                SeatingChart.Add(row);
            }
        }

        public Seat BookSeat(Passenger passenger)
        {
            foreach (var row in SeatingChart)
            {
                foreach (var seat in row)
                {
                    if (!seat.IsBooked && (passenger.Preference == SeatPreference.Window && (seat.Label == SeatLabel.A || seat.Label == SeatLabel.D) ||
                                            passenger.Preference == SeatPreference.Aisle && (seat.Label == SeatLabel.B || seat.Label == SeatLabel.C)))
                    {
                        seat.IsBooked = true;
                        seat.Passenger = passenger;
                        passenger.BookedSeat = seat;
                        return seat;
                    }
                }
            }
            return null;
        }

        public void PrintSeatingChart()
        {
            foreach (var row in SeatingChart)
            {
                foreach (var seat in row)
                {
                    if (seat.IsBooked)
                        Console.Write($"{seat.Passenger.FirstName[0]}{seat.Passenger.LastName[0]} ");
                    else
                        Console.Write($"{seat} ");
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Flight plane = new Flight();

            while (true)
            {
                Console.WriteLine("Please enter 1 to book a ticket.");
                Console.WriteLine("Please enter 2 to see seating chart.");
                Console.WriteLine("Please enter 3 to exit the application.");

                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        Console.WriteLine("Please enter the passenger's first name:");
                        string firstName = Console.ReadLine();
                        Console.WriteLine("Please enter the passenger's last name:");
                        string lastName = Console.ReadLine();
                        Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter for first available seat:");
                        string preferenceInput = Console.ReadLine();

                        SeatPreference preference = SeatPreference.Window;

                        if (preferenceInput == "2")
                            preference = SeatPreference.Aisle;

                        Passenger passenger = new Passenger(firstName, lastName, preference);
                        Seat bookedSeat = plane.BookSeat(passenger);

                        if (bookedSeat != null)
                            Console.WriteLine($"The seat {bookedSeat} has been booked.");
                        else
                            Console.WriteLine("Sorry, Flight is fully booked.");

                        break;

                    case "2":
                        plane.PrintSeatingChart();
                        break;

                    case "3":
                        Console.WriteLine("Exit the application.");
                        return;

                    default:
                        Console.WriteLine("Invalid input. Please enter a valid input.");
                        break;
                }
            }
        }
    }

}
