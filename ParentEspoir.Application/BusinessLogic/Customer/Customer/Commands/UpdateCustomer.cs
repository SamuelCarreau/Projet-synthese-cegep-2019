using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class UpdateCustomerCommand : IRequest
    {
        public CustomerModel Model { get; set; }
    }

    public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerCommand>
    {
        private const string REQUIRED_ERROR_MESSAGE = "Ce champs est requis";
        private const string NAME_VALIDATION_REGEX = @"^([\u00c0-\u01ffa-zA-Z'\-])+$";
        private const string NAME_ERROR_MESSAGE = "Le nom saisie n'est pas valide.";

        public UpdateCustomerModelValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Model.Id).Must(x => context.Customers.Where(c => c.CustomerId == x && c.IsDelete == false).Any());

            RuleFor(c => c.Model.FirstName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.LastName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.DateOfBirth).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.CityName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.Address).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.ProvinceName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.CountryName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.Phone).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);

            RuleFor(c => c.Model.FirstName).Matches(new Regex(NAME_VALIDATION_REGEX)).WithMessage(NAME_ERROR_MESSAGE);

            RuleFor(c => c.Model.FirstName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.FIRST_NAME_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_FIRST_NAME);

            RuleFor(c => c.Model.LastName).Matches(new Regex(NAME_VALIDATION_REGEX)).WithMessage(NAME_ERROR_MESSAGE);

            RuleFor(c => c.Model.LastName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.LAST_NAME_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_LAST_NAME);

            RuleFor(c => c.Model.DateOfBirth).Must(x => x >= new DateTime(1900, 01, 01))
                .WithMessage(CustomerConstant.ERROR_MESSAGE_DATEOFBIRTH_AFTER_1900);
            RuleFor(c => c.Model.DateOfBirth).Must(x => x <= DateTime.Now)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_DATEOFBIRTH_BEFORE_NOW);

            RuleFor(c => c.Model.Address).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.ADDRESS_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_ADDRESS);

            RuleFor(c => c.Model.CityName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.CITY_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_CITY);

            RuleFor(c => c.Model.PostalCodeName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.POSTAL_CODE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_POSTAL_CODE);

            RuleFor(c => c.Model.ProvinceName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.PROVINCE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_PROVINCE);

            RuleFor(c => c.Model.CountryName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.COUNTRY_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_COUNTRY);

            RuleFor(c => c.Model.Phone).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.PHONE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_PHONE);

            RuleFor(c => c.Model.SecondaryPhone).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.SECONDARY_PHONE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_SECONDARY_PHONE);

            RuleFor(c => c.Model.SupportGroupId).Must(x => x == null || (context.SupportGroups.Where(sp => sp.SupportGroupId == x && sp.IsDelete == false).Any()));
            RuleFor(c => c.Model.ReferenceById).Must(x => x == null || (context.ReferenceTypes.Where(rt => rt.Id == x && rt.IsDelete == false).Any()));
            RuleFor(c => c.Model.HeardOfUsFromId).Must(x => x == null || (context.HeardOfUsFroms.Where(hf => hf.Id == x && hf.IsDelete == false).Any()));
        }

        private bool IsNotNullOrWhiteSpace(string customerProperty)
        {
            return !string.IsNullOrWhiteSpace(customerProperty);
        }
    }

    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;

        public UpdateCustomerCommandHandler(ParentEspoirDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            Customer customer = await _context.Customers
                .Include(c => c.HeardOfUsFrom)
                .Include(c => c.SupportGroup)
                .Include(c => c.ReferenceBy)
                .SingleOrDefaultAsync(c => c.CustomerId == request.Model.Id);

            customer.FirstName = request.Model.FirstName;
            customer.LastName = request.Model.LastName;
            customer.NormalizedName = StringNormalizer.Normalize(request.Model.FirstName + request.Model.LastName);
            customer.DateOfBirth = request.Model.DateOfBirth;
            customer.Address = request.Model.Address;
            customer.PostalCode = request.Model.PostalCodeName;
            customer.City = request.Model.CityName;
            customer.Province = request.Model.ProvinceName;
            customer.Country = request.Model.CountryName;
            customer.Phone = request.Model.Phone;
            customer.SecondaryPhone = request.Model.SecondaryPhone;
            customer.SupportGroup = (request.Model.SupportGroupId == null) ? null : _context.SupportGroups.Find(request.Model.SupportGroupId);
            customer.ReferenceBy = (request.Model.ReferenceById == null) ? null : _context.ReferenceTypes.Find(request.Model.ReferenceById);
            customer.HeardOfUsFrom = (request.Model.HeardOfUsFromId == null) ? null : _context.HeardOfUsFroms.Find(request.Model.HeardOfUsFromId);
            customer.InscriptionDate = request.Model.InscriptionDate;

            _context.Update(customer);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
