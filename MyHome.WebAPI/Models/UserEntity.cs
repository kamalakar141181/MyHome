namespace MyHome.WebAPI.Models
{
    public class UserEntity : AuditEntity
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailID { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
