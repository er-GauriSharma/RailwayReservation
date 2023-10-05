
namespace Railway_Reservation.Repository
{
    public interface IEmailInterface
    {
        public void SendEmail(string toEmail, string subject, string body);
    }
}
