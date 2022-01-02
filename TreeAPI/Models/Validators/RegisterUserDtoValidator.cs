using System.Linq;
using FluentValidation;
using TreeAPI.Context;
using TreeAPI.Dtos;

namespace TreeAPI.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public RegisterUserDtoValidator(TreeDbContext dbContext)
        {
            RuleFor(x => x.Mail).EmailAddress().NotEmpty();

            RuleFor(x => x.Password).MinimumLength(6);
            RuleFor(x => x.ConfirmPassowrd).Equal(e => e.Password).WithMessage("Wpisano dwa inne hasła");



            RuleFor(x => x.Mail)
            .Custom((value, context) => {
                var emailInUse = dbContext.Users.Any(x => x.Mail == value);
                if(emailInUse)
                {
                    context.AddFailure("Mail", "Ten email jest już używany");
                }
            });

            
            RuleFor(x => x.Name)
            .Custom((value, context) => {
                var nameInUse = dbContext.Users.Any(x => x.Name == value);
                if(nameInUse)
                {
                    context.AddFailure("Name", "Ta nazwa użytkownika jest już wykorzystywana");
                }
            });
        }
    }
}