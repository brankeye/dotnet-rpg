using System;
using dotnet_rpg.Domain.Enums;
using dotnet_rpg.Service.Contracts.Validation;
using FluentValidation;

namespace dotnet_rpg.Service.Core.Character.DeleteCharacterCommand
{
    public class DeleteCharacterCommandValidator : Validator<DeleteCharacterCommand>
    {
        public DeleteCharacterCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("An id must be given");
        }
    }
}