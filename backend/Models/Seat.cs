namespace backend.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string SeatNo { get; set; }
        public bool IsBooked { get; set; }

        public Bus? Bus { get; set; }
    }
}
