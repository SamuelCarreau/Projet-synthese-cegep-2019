using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParentEspoir.Application.Test
{
    public class CustomerDescriptionTestBase : TestBase
    {
        protected ParentEspoirDbContext _context;

        public CustomerDescriptionTestBase()
        {
            _context = GetDbContext();

            PopulateTestingData();
        }

        private void PopulateTestingData()
        {
            _context.AddRange(new Sex { Name = "homme" }, new Sex { Name = "femme" }, new Sex { Name = "autre" });
            _context.AddRange(new Parent { Name = "pere" }, new Parent { Name = "mere" });
            _context.AddRange(new MaritalStatus { Name = "marie" }, new MaritalStatus { Name = "conjoint de fait" }, new MaritalStatus { Name = "celibataire" }, new MaritalStatus { Name = "separe" });
            _context.AddRange(new CitizenStatus { Name = "citoyen canadien" }, new CitizenStatus { Name = "resident temporaire" }, new CitizenStatus { Name = "immigrant" });
            _context.AddRange(new FamilyType { Name = "biologique" }, new FamilyType { Name = "reconstitue" }, new FamilyType { Name = "separe" });
            _context.AddRange(new Language { Name = "francais" }, new Language { Name = "anglais" }, new Language { Name = "espagnol" }, new Language { Name = "mandarin" }, new Language { Name = "arabe" }, new Language { Name = "japonais" });
            _context.AddRange(new HomeType { Name = "maison unifamiliale" }, new HomeType { Name = "appartement" }, new HomeType { Name = "hlm" }, new HomeType { Name = "condo" }, new HomeType { Name = "autre" });
            _context.AddRange(new TransportType { Name = "voiture" }, new TransportType { Name = "camion" }, new TransportType { Name = "transport en commun" }, new TransportType { Name = "velo" }, new TransportType { Name = "autre" });
            _context.AddRange(new Schooling { Name = "primaire" }, new Schooling { Name = "secondaire" }, new Schooling { Name = "collegiale" }, new Schooling { Name = "universitaire baccalaureat" }, new Schooling { Name = "universitaire maitrise" }, new Schooling { Name = "universitaire doctorat" }, new Schooling { Name = "autre" });
            _context.AddRange(new IncomeSource { Name = "travail" }, new IncomeSource { Name = "aide-social" }, new IncomeSource { Name = "chomage" }, new IncomeSource { Name = "autre" });
            _context.AddRange(new Availability { Name = "matin 8:00 - 10:00" }, new Availability { Name = "matin 10:00 - 12:00" }, new Availability { Name = "jour 12:00 - 14:00" }, new Availability { Name = "jour 14:00 - 16:00" }, new Availability { Name = "jour 16:00 - 18:00" }, new Availability { Name = "soir 18:00 - 20:00" }, new Availability { Name = "soir 20:00 - 22:00" });
            _context.AddRange(new YearlyIncome { Name = "1" }, new YearlyIncome { Name = "2" }, new YearlyIncome { Name = "3" }, new YearlyIncome { Name = "4" }, new YearlyIncome { Name = "5" }, new YearlyIncome { Name = "6" }, new YearlyIncome { Name = "7" });
            _context.AddRange(new LegalCustody { Name = "avec droit d'acces" }, new LegalCustody { Name = "sans droit d'acces" }, new LegalCustody { Name = "garde partagee" });
            _context.AddRange(new ChildrenAgeBracket { Name = "1" }, new ChildrenAgeBracket { Name = "2" }, new ChildrenAgeBracket { Name = "3" }, new ChildrenAgeBracket { Name = "4" }, new ChildrenAgeBracket { Name = "5" }, new ChildrenAgeBracket { Name = "6" });
            _context.AddRange(new SkillToDevelop { Name = "Habilete parental" }, new SkillToDevelop { Name = "Habilete perso et social" }, new SkillToDevelop { Name = "Saines habitude de vie" });
            _context.AddRange(new SocialService { Name = "Hopitaux" }, new SocialService { Name = "CLSC" }, new SocialService { Name = "organisme communautaire" }, new SocialService { Name = "services prives" }, new SocialService { Name = "des proches" }, new SocialService { Name = "du reseau social" });

            _context.AddRange(new SupportGroup { Name = "Cegep de sainte-foy" }, new SupportGroup { Name = "Universite Laval" }, new SupportGroup { Name = "Hopital regional" }, new SupportGroup { Name = "Centre des femmes" }, new SupportGroup { Name = "autre" });
            _context.AddRange(new ReferenceType { Name = "Ecole" }, new ReferenceType { Name = "Hopital" }, new ReferenceType { Name = "CLSC" }, new ReferenceType { Name = "Autre" });
            _context.AddRange(new HeardOfUsFrom { Name = "reseau sociaux" }, new HeardOfUsFrom { Name = "moteur de recherche" }, new HeardOfUsFrom { Name = "journaux" }, new HeardOfUsFrom { Name = "connaissance" }, new HeardOfUsFrom { Name = "professionnel de la sante" }, new HeardOfUsFrom { Name = "autre" });

            _context.SaveChanges();
        }

        protected Customer CreateCustomer(string customerFirstName)
        {
            var customer = new Customer
            {
                FirstName = customerFirstName,
                LastName = "Legrand",
                Address = "516 rue Saint-Luc",
                PostalCode = "G1N2V3",
                City = "Quebec",
                Province = "Quebec",
                Country = "Canada",
                DateOfBirth = new DateTime(1986, 11, 25),
                HeardOfUsFrom = _context.HeardOfUsFroms.Find(3),
                Phone = "418-271-3856",
                SecondaryPhone = "418-895-5996",
                ReferenceBy = _context.ReferenceTypes.Find(2),
                SupportGroup = _context.SupportGroups.Find(4)
            };
            customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

            _context.Add(customer);
            _context.SaveChanges();
            return _context.Customers.Include(c => c.CustomerActivations).Where(c => c.FirstName.Equals(customerFirstName)).Single();
        }
    }
}
