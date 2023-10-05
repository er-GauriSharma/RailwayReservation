using Railway_Reservation.Data;
using Railway_Reservation.Model;

namespace Railway_Reservation.Repository
{
    public class IUser:IUserInterface
    {
        private DbConnection context;
        public IUser(DbConnection context)
        {
            this.context = context;
        }
        public void Add(User user)
        {
            context.User2.Add(user);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user1 = context.User2.Find(id);
            context.User2.Remove(user1);
            context.SaveChanges();
        }

        public List<User> GetUserList()
        {
            return context.User2.ToList();
        }

        public User GetUser(int id)
        {
            var user1 = context.User2.Find(id);
            return user1;
        }

        public void Update(int id, User user)
        {
            var user1 = context.User2.Find(id);
            if (user1 != null)
            {
                user1.Name = user.Name;
                user1.Email = user.Email;
                user1.PhoneNo = user.PhoneNo;
                user1.Password = user.Password;
                context.SaveChanges();
            }
        }
        public User Authenticate(string PhoneNo, string Pass)
        {
            return context.User2.Where(u => u.PhoneNo.Equals(PhoneNo) && u.Password == Pass).FirstOrDefault();
        }
    }
}
