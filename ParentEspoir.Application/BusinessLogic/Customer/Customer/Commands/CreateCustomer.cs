using FluentValidation;
using MediatR;
using ParentEspoir.Common;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Infrastructure;
using ParentEspoir.Persistence;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ParentEspoir.Application
{
    public class CreateCustomerCommand : IRequest
    {
        public CustomerModel Model { get; set; }
    }

    public class CreateCustomerModelValidator : AbstractValidator<CreateCustomerCommand>
    {
        private const string REQUIRED_ERROR_MESSAGE = "Ce champs est requis.";
        private const string NAME_VALIDATION_REGEX = @"^([\u00c0-\u01ffa-zA-Z'\-])+$";
        private const string NAME_ERROR_MESSAGE = "Le nom saisie n'est pas valide.";

        public CreateCustomerModelValidator(ParentEspoirDbContext context)
        {
            RuleFor(c => c.Model.Id).Must(x => x == null);

            RuleFor(c => c.Model.FirstName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.LastName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.DateOfBirth).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.CityName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.Address).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.PostalCodeName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.ProvinceName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.CountryName).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.Phone).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            RuleFor(c => c.Model.SecondaryPhone).NotNull().WithMessage(REQUIRED_ERROR_MESSAGE);
            
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

            RuleFor(c => c.Model.PostalCodeName).Must(x => x == null || (IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.POSTAL_CODE_MAX_LENGHT))
                .WithMessage(CustomerConstant.ERROR_MESSAGE_POSTAL_CODE);

            RuleFor(c => c.Model.ProvinceName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.PROVINCE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_PROVINCE);

            RuleFor(c => c.Model.CountryName).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.COUNTRY_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_COUNTRY);

            RuleFor(c => c.Model.Phone).Must(x => IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.PHONE_MAX_LENGHT)
                .WithMessage(CustomerConstant.ERROR_MESSAGE_PHONE);

            RuleFor(c => c.Model.SecondaryPhone).Must(x => x == null || (IsNotNullOrWhiteSpace(x) && x.Length <= CustomerConstant.SECONDARY_PHONE_MAX_LENGHT))
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

    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Unit>
    {
        private readonly ParentEspoirDbContext _context;
        private readonly IDateTime _time; 

        public CreateCustomerCommandHandler(ParentEspoirDbContext context, IDateTime time)
        {
            _context = context;
            _time = time;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            int fileNumber = CustomerFileNumberCreator.CreateFileNumberAsync(_context, _time);

            Customer customer = new Customer
            {
                CustomerDescription = new CustomerDescription(),
                FileNumber = fileNumber,
                CreationDate = _time.Now,
                SuppressionDate = null,
                FirstName = request.Model.FirstName,
                LastName = request.Model.LastName,
                NormalizedName = StringNormalizer.Normalize(request.Model.FirstName + request.Model.LastName),
                DateOfBirth = request.Model.DateOfBirth,
                Address = request.Model.Address,
                PostalCode = request.Model.PostalCodeName,
                City = request.Model.CityName,
                Province = request.Model.ProvinceName,
                Country = request.Model.CountryName,
                Phone = request.Model.Phone,
                SecondaryPhone = request.Model.SecondaryPhone,
                SupportGroup = (request.Model.SupportGroupId == null) ? null : _context.SupportGroups.Find(request.Model.SupportGroupId),
                ReferenceBy = (request.Model.ReferenceById == null) ? null : _context.ReferenceTypes.Find(request.Model.ReferenceById),
                HeardOfUsFrom = (request.Model.HeardOfUsFromId == null) ? null : _context.HeardOfUsFroms.Find(request.Model.HeardOfUsFromId),
                InscriptionDate = request.Model.InscriptionDate
            };

            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });
            _context.Add(customer);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
