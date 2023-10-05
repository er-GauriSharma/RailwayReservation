using Railway_Reservation.Model;
using Railway_Reservation.Data;

namespace Railway_Reservation.Repository
{
    public interface ITraininterface
    {
        void Add(Train train);
        void Delete(int id);
        void Update(int id, Train train);
        IList<Train> Search(string ArrivalCity, string DepartureCity, int NoOfSeats, string Class);
        IList<Train> GetTrainsList();
    }
}
