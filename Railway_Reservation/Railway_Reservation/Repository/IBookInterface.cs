using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public interface IBookInterface
    {
        int Book(int id, int noOfSeats, int userID);
        bool Cancel(int id);
        int Checkout(int id, int Payment);
        List<Booking> GetTrainList(int userId);
    }
}
