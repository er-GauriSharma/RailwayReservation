using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public interface IUserInterface
    {
        void Add(User user);
        List<User> GetUserList();
        User GetUser(int id);
        void Update(int id, User user);
        void Delete(int id);
        User Authenticate(string phoneNo, string password);
    }
}
