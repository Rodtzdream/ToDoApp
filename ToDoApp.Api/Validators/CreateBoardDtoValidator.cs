using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators;

public class CreateBoardDtoValidator : AbstractValidator<CreateBoardDto>
{
    public CreateBoardDtoValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100);
    }
}
