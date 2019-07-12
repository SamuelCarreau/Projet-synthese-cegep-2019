using FluentValidation;
using FluentValidation.Validators;
using ParentEspoir.Persistence;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using ParentEspoir.Domain.Constants;

namespace ParentEspoir.Application
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {


        public CreateNoteCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.CustomerId).Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());

            RuleFor(c => c.NoteTypeId).Must(x => x == null || (context.NoteTypes.Where(c => c.Id == x && c.IsDelete == false).Any()));

            //NOTE NAME
            RuleFor(c => c.NoteName)
                .Must(x => x != null)
                .WithMessage(NoteConstant.ERROR_ISREQUIRED);
            RuleFor(c => c.NoteName)
                .MaximumLength(NoteConstant.NAME_MAX_LENGHT)
                .WithMessage(NoteConstant.NAME_MAX_LENGHT_ERROR);
            RuleFor(c => c.NoteName)
                .Matches(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE)
                .WithMessage(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE_ERROR);

            //SUPERVISOR NAME
            RuleFor(c => c.SupervisorName)
                .MaximumLength(NoteConstant.NAME_MAX_LENGHT)
                .WithMessage(NoteConstant.NAME_MAX_LENGHT_ERROR);
            RuleFor(c => c.SupervisorName)
                .Matches(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE)
                .WithMessage(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE_ERROR);

            //SUPERVISORT TITLE
            RuleFor(c => c.SupervisorTitle)
                .MaximumLength(NoteConstant.NAME_MAX_LENGHT)
                .WithMessage(NoteConstant.NAME_MAX_LENGHT_ERROR);
            RuleFor(c => c.SupervisorTitle)
                .Matches(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE)
                .WithMessage(NoteConstant.MATCH_ONLY_ALPHANUMERIC_SPACE_ERROR);

            //NOTE BODY
            RuleFor(c => c.Body).Must(x => x != null).WithMessage(NoteConstant.ERROR_ISREQUIRED);

        }
    }
}