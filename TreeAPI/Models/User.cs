namespace TreeAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string  Mail { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}