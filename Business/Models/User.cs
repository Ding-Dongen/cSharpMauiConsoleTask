using System.Text.Json.Serialization;

namespace Business.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int PhoneNumber { get; set; } = 0; 
        public string Address { get; set; } = string.Empty;
        public int ZipCode { get; set; } = 0;
        public string City { get; set; } = string.Empty;



        
        public User() { }

        [JsonConstructor]
        public User(string name, string lastname, string email, string password)
        {
            Name = name;
            LastName = lastname;
            Email = email;
            Password = password;
            PhoneNumber = 0;
            Address = string.Empty;
            ZipCode = 0;
            City = string.Empty;
        }
    }
}
