using SCA.Shared.Entities.Enums;

namespace SCA.Shared.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirtName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role AcessLevel { get; set; }
    }
}
