using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Operations.User.Queries.UserQuery
{
    public class UserQueryValidator : Validator<UserQuery>
    {
        public UserQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull()
                .WithMessage("A user id must be given");
        }
    }
}