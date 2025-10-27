namespace backend.Models
{
    public class Bus
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string BusName { get; set; } 
        public string From { get; set; } 
        public string To { get; set; } 
        public DateTime JourneyDate { get; set; }
        public string StartTime { get; set; } 
        public string ArrivalTime { get; set; } 
        public string BoardingPoint { get; set; } 
        public string DroppingPoint { get; set; } 
        public string BusType { get; set; } 
        public decimal Fare { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }

        public int SeatsLeft => TotalSeats - BookedSeats;
        public List<Seat> Seats { get; set; } = new List<Seat>();
    }


   
}
