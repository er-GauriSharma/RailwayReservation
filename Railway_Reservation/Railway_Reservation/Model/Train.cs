using System.ComponentModel.DataAnnotations;

namespace Railway_Reservation.Model
{
    public class Train
    {
        [Key]
        public int Train_Id { get; set; }

        [Required(ErrorMessage = "Please enter Train_Name")]
        public string Train_Name { get; set; }

        [Required(ErrorMessage = "Please enter Departure City")]
        public string Departure_City { get; set; }

        [Required(ErrorMessage = "Please enter Arrival City")]
        public string Arrival_City { get; set; }

        [Required(ErrorMessage = "Please enter Departure Time and Date")]
        [DataType(DataType.DateTime)]
        public DateTime Date_Time { get; set; }

        [Required(ErrorMessage = "Please enter Train Fare")]
        public int Fare { get; set; }

        [Required(ErrorMessage = "Please enter Available Seats")]
        [Range(1, 40)]
        public int Seat_Available { get; set; }

        [Required(ErrorMessage = "Please enter the class")]
        public string Class { get; set; }
    }

    

}
