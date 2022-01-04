using System.ComponentModel.DataAnnotations;

namespace TreeAPI.Dtos
{
    public class UserRegisterDto
    {
                public int Id { get; set; }
        
        public string Name { get; set; }
     
        public string  Mail { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword {get; set;}
    }
}