using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParentEspoir.Application
{
    public class DeleteSeanceCommandValidator : AbstractValidator<DeleteSeanceCommand>
    {
        public DeleteSeanceCommandValidator(ParentEspoirDbContext context)
        {
            RuleFor(w => w.WorkshopId)
                .Must(id => context.Workshops.Find(id) != null)
                .WithMessage("L'atelier sélectionné est invalide.");

            RuleFor(s => s.SeanceId)
                .Must(id => context.Seances.Find(id) != null &&
                    context.Participants
                .Where(p => p.SeanceId == id && p.IsDelete == false)
                .Any() ==  false)
                .WithMessage("Impossible de supprimer une séance possédant des participants");
        }
    }
}