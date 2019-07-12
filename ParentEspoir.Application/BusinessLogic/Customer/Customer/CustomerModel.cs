using ParentEspoir.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ParentEspoir.Application
{
    public class CustomerModel
    {
        public int? Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int? FileNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? InscriptionDate { get; set; }
        public string Address { get; set; }
        public string PostalCodeName { get; set; }
        public string CityName { get; set; }
        public string ProvinceName { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        public bool IsInscripted
        {
            get => InscriptionDate != null ?
                  InscriptionDate >= DateTime.Now.AddYears(-1) : false;
        }
        public int? SupportGroupId { get; set; }
        public string SupportGroupName { get; set; }
        public int? ReferenceById { get; set; }
        public string ReferenceByName { get; set; }
        public int? HeardOfUsFromId { get; set; }
        public string HeardOfUsFromName { get; set; }

        // if Member:
        public bool IsMember { get; set; }
        public DateTime? IsMemberSince { get; set; }
        //public DateTime? RenewalDate { get; set; }

        public bool HasCustomerDescription { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CustomerModel model &&
                   EqualityComparer<int?>.Default.Equals(Id, model.Id) &&
                   CreationDate == model.CreationDate &&
                   EqualityComparer<int?>.Default.Equals(FileNumber, model.FileNumber) &&
                   FirstName == model.FirstName &&
                   LastName == model.LastName &&
                   EqualityComparer<DateTime?>.Default.Equals(DateOfBirth, model.DateOfBirth) &&
                   Address == model.Address &&
                   PostalCodeName == model.PostalCodeName &&
                   CityName == model.CityName &&
                   ProvinceName == model.ProvinceName &&
                   CountryName == model.CountryName &&
                   Phone == model.Phone &&
                   SecondaryPhone == model.SecondaryPhone &&
                   EqualityComparer<int?>.Default.Equals(SupportGroupId, model.SupportGroupId) &&
                   SupportGroupName == model.SupportGroupName &&
                   EqualityComparer<int?>.Default.Equals(ReferenceById, model.ReferenceById) &&
                   ReferenceByName == model.ReferenceByName &&
                   EqualityComparer<int?>.Default.Equals(HeardOfUsFromId, model.HeardOfUsFromId) &&
                   HeardOfUsFromName == model.HeardOfUsFromName &&
                   IsMember == model.IsMember &&
                   EqualityComparer<DateTime?>.Default.Equals(IsMemberSince, model.IsMemberSince) &&
                   HasCustomerDescription == model.HasCustomerDescription;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(CreationDate);
            hash.Add(FileNumber);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(DateOfBirth);
            hash.Add(Address);
            hash.Add(PostalCodeName);
            hash.Add(CityName);
            hash.Add(ProvinceName);
            hash.Add(CountryName);
            hash.Add(Phone);
            hash.Add(SecondaryPhone);
            hash.Add(SupportGroupId);
            hash.Add(SupportGroupName);
            hash.Add(ReferenceById);
            hash.Add(ReferenceByName);
            hash.Add(HeardOfUsFromId);
            hash.Add(HeardOfUsFromName);
            hash.Add(IsMember);
            hash.Add(IsMemberSince);
            hash.Add(HasCustomerDescription);
            return hash.ToHashCode();
        }

        public static explicit operator CustomerModel(Customer entity)
        {
            var customer = new CustomerModel
            {
                Id = entity.CustomerId,
                CreationDate = entity.CreationDate,
                FileNumber = entity.FileNumber,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                PostalCodeName = entity.PostalCode,
                CityName = entity.City,
                ProvinceName = entity.Province,
                CountryName = entity.Country,
                Phone = entity.Phone,
                SecondaryPhone = entity.SecondaryPhone,
                SupportGroupId = entity.SupportGroup?.SupportGroupId,
                SupportGroupName = entity.SupportGroup?.Name,
                ReferenceById = entity.ReferenceBy?.Id,
                ReferenceByName = entity.ReferenceBy?.Name,
                HeardOfUsFromId = entity.HeardOfUsFrom?.Id,
                HeardOfUsFromName = entity.HeardOfUsFrom?.Name,
                IsMember = (entity.Member != null) ? true : false,
                IsMemberSince = entity.Member?.SubscriptionDate,
                InscriptionDate = entity.InscriptionDate
            };

            return customer;
        }
    }
}
