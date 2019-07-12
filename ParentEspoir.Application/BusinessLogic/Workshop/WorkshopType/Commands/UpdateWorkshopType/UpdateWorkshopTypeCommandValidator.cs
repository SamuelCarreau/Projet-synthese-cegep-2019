using FluentValidation;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System.Linq;

namespace ParentEspoir.Application
{
    public class UpdateWorkshopTypeCommandValidator : AbstractValidator<UpdateWorkshopTypeCommand>
    {

        public UpdateWorkshopTypeCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(y => y)
                .Must(n => (context.WorkshopTypes.SingleOrDefault(e => e.Id == n.Id && e.Name == n.Name) != null) ||
                string.IsNullOrWhiteSpace(n.Name) == false &&
                context.WorkshopTypes
                .Where(a => a.IsDelete == false)
                .Any(w => n.Name.ToUpper().Trim() == w.Name.ToUpper().Trim())
                == false).WithMessage("Le nom existe déjà").OverridePropertyName("Name");

            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Le nom est requis");

            RuleFor(a => a.Name)
                .MaximumLength(WorkshopTypeConstant.NAME_MAX_LENGHT)
                .WithMessage($"Le nom doit contenir moins de {WorkshopTypeConstant.NAME_MAX_LENGHT} caractères");

            RuleFor(a => a.Code)
                .NotEmpty()
                .WithMessage("Le code est requis");

            RuleFor(a => a.Code)
                .MaximumLength(WorkshopTypeConstant.CODE_MAX_LENGHT)
                .WithMessage($"Le nom doit contenir moins de {WorkshopTypeConstant.CODE_MAX_LENGHT} caractères");
        }
    }
}