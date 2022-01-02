using System.Linq;
using FluentValidation;
using TreeAPI.Context;
using TreeAPI.Dtos;

namespace TreeAPI.Models.Validators
{
    public class AddNodeDtoValidator : AbstractValidator<AddNodeDto>
    {
        public AddNodeDtoValidator(TreeDbContext dbContext)
        {
            RuleFor(x => x.Value).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Value).Custom((value, context) => {
                var valueInUse = dbContext.Nodes.Any(x => x.Value == value);
                if(valueInUse)
                {
                    context.AddFailure("Value", "Ta wartość jest już wykorzystywana");
                }
            });

            RuleFor(x => x.ParentId).Custom((value, context) => {
                if(value != null)
                {
                    var parentExist = dbContext.Nodes.Any(x => x.ParentId == value);
                    if(!parentExist)
                    {
                        context.AddFailure("ParentId", "Wprowadzony rodzic nie istnieje");
                    }
                }
            });

        }
    }
}