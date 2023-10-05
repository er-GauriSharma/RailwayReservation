using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation.Model
{
    public class Booking
    {
        [Key]
        public int Booking_Id { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }
        public string Train_Name { get; set; }
        public string Departure_City { get; set; }
        public string Arrival_City { get; set; }
        public DateTime Date_Time { get; set; }
        public int Seat_Booked { get; set; }
        public string Class { get; set; }
        public long Total_Fare { get; set; }
        public string Payment_Status { get; set; }
    }
    public class TrainSearch
    {
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string Class { get; set; }
        public int NoOfSeats { get; set; }
    }
}
