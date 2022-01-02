using System.ComponentModel.DataAnnotations;

namespace TreeAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string Name { get; set; }
        [Required(ErrorMessage="Mail jest wymagany")]
        public string  Mail { get; set; }
        public string Role { get; set; }
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
    }
}