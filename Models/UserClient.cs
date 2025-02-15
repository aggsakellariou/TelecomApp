namespace TelecomApp.Models
{
    public class UserClient
    {
        public required Client Client { get; set; }
        public required User User { get; set; }
        public required Phone Phone { get; set; }
    }
}