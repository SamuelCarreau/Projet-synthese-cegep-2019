using FluentValidation;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class CreateWorkshopTypeCommandValidator : AbstractValidator<CreateWorkshopTypeCommand>
    {
        public CreateWorkshopTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom est requis");

            RuleFor(a => a.Name)
                .MaximumLength(WorkshopTypeConstant.NAME_MAX_LENGHT)
                .WithMessage($"Le nom doit contenir moins de {WorkshopTypeConstant.NAME_MAX_LENGHT} caractères");

            RuleFor(a => a.Name)
                .Must(n => string.IsNullOrWhiteSpace(n) == false &&
                context.WorkshopTypes
                .Where(c => c.IsDelete == false)
                .Any(c => n.ToUpper().Trim() == c.Name.ToUpper().Trim())
                == false).WithMessage("Le nom existe déjà");

            RuleFor(a => a.Code)
                .NotEmpty()
                .WithMessage("Le code est requis");

            RuleFor(a => a.Code)
                .MaximumLength(WorkshopTypeConstant.CODE_MAX_LENGHT)
                .WithMessage($"Le nom doit contenir moins de {WorkshopTypeConstant.CODE_MAX_LENGHT} caractères");

        }
    }
}