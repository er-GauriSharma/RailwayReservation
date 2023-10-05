using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public interface IAdminInterface
    {
        Admin Authenticate(string phoneNo, string password);
    }
}
