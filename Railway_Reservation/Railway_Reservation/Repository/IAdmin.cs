using Railway_Reservation.Model;

namespace Railway_Reservation.Repository

    {
        public class IAdmin : IAdminInterface
        {
            private List<Admin> _admins = new List<Admin>
        {
            new Admin { UserId = 1, PhoneNo = "8077982617", Password = "8077982617" }
        };
            public Admin Authenticate(string PhoneNo, string Pass)
            {
                return _admins.FirstOrDefault(u => u.PhoneNo.Equals(PhoneNo) && u.Password.Equals(Pass));
            }
        }
   }

