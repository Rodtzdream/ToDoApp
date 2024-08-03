using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators;

public class ChangeToDoItemDtoValidator : AbstractValidator<ChangeToDoItemDto>
{
    public ChangeToDoItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
