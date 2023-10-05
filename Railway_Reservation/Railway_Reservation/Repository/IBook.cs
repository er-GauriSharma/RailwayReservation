using Railway_Reservation.Data;
using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public class IBooking : IBookInterface
    {
        private DbConnection context;
        public IBooking(DbConnection context)
        {
            this.context = context;
        }

        public int Book(int id, int noOfSeats, int userID)
        {
            Train train = context.Trains.Find(id);
            if (train == null)
            {
                return 1;
            }
            else
            {
                if (train.Seat_Available >= noOfSeats)
                {
                    long TotalFare = train.Fare * noOfSeats;
                    Booking booking = new Booking
                    {
                        TrainId = id,
                        UserId = userID,
                        Train_Name = train.Train_Name,
                        Departure_City = train.Departure_City,
                        Arrival_City = train.Arrival_City,
                        Date_Time = train.Date_Time,
                        Seat_Booked = noOfSeats,
                        Class = train.Class,
                        Total_Fare = TotalFare,
                        Payment_Status = "Payment Pending"
                    };
                    context.Trip.Add(booking);
                    context.SaveChanges();
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
        }

        public List<Booking> GetTrainList(int userId)
        {
            var bookings = context.Trip.Where(u => u.UserId == userId);
            return bookings.ToList();
        }
        public bool Cancel(int id)
        {
            Booking bookings = context.Trip.Find(id);
            if (bookings == null)
            {
                return false;
            }
            else
            {
                var train = context.Trains.Find(bookings.TrainId);
                context.Trip.Remove(bookings);
                context.SaveChanges();
                train.Seat_Available += bookings.Seat_Booked;
                context.SaveChanges();
                return true;
            }
        }
        public int Checkout(int id, int Payment)
        {
            var booking = context.Trip.Find(id);
            if (booking == null)
            {
                return 1;
            }
            else
            {
                var train = context.Trains.Find(booking.TrainId);
                if (train.Seat_Available >= booking.Seat_Booked)
                {
                    if (Payment == booking.Total_Fare)
                    {
                        booking.Payment_Status = "Payment Successful";
                        train.Seat_Available -= booking.Seat_Booked;
                        context.SaveChanges();
                        return 2;
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 4;
                }
            }
        }

        public List<Booking> GetTrainsList(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
