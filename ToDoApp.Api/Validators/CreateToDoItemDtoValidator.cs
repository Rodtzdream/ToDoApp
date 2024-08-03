﻿using FluentValidation;
using ToDoApp.Services.Dtos;

namespace ToDoApp.Api.Validators;

public class CreateToDoItemDtoValidator : AbstractValidator<CreateToDoItemDto>
{
    public CreateToDoItemDtoValidator()
    {
        RuleFor(x => x.Title)
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .MaximumLength(500);

        RuleFor(x => x.DueDate)
            .Must(x => x > DateTime.Now);
    }
}
