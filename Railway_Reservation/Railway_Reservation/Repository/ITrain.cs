using Railway_Reservation.Data;
using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public class ITrain :ITraininterface
    {
        private DbConnection context;
        public ITrain(DbConnection context)
        {
            this.context = context;
        }
        public void Add(Train train)
        {
            context.Trains.Add(train);
            context.SaveChanges();
        }

        public IList<Train> Search(string ArrivalCity, string DepartureCity, int NoOfSeats, string Class)
        {
            var trains = context.Trains.Where(u => u.Arrival_City.Equals(ArrivalCity) && u.Departure_City.Equals(DepartureCity) && u.Seat_Available >= NoOfSeats && u.Class.Equals(Class)).ToList();
            return trains;
        }
        public IList<Train> GetTrainsList()
        {
            return context.Trains.ToList();
        }

        public void Delete(int id)
        {
            Train train = context.Trains.Find(id);
            context.Trains.Remove(train);
            context.SaveChanges();
        }

        public void Update(int id, Train t)
        {
            Train train = context.Trains.Find(id);
            if (train != null)
            {
                train.Train_Name = t.Train_Name;
                train.Departure_City = t.Departure_City;
                train.Arrival_City = t.Arrival_City;
                train.Date_Time = t.Date_Time;
                train.Fare = t.Fare;
                train.Seat_Available = t.Seat_Available;
                train.Class = t.Class;
                context.SaveChanges();
            }
        }
    }
}
