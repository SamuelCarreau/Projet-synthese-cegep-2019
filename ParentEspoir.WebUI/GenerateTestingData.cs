using ParentEspoir.Application;
using ParentEspoir.Domain.Constants;
using ParentEspoir.Domain.Entities;
using ParentEspoir.Domain.Enums;
using ParentEspoir.Infrastructure;
using ParentEspoir.Persistence;
using System;
using System.Diagnostics;
using System.Linq;

namespace ParentEspoir.WebUI
{
    public static class GenerateTestingData
    {
        private static Random _randEngine = new Random();

        public static void Generate(ParentEspoirDbContext context)
        {
            GenerateProfilOption(context);
            GenerateCustomers(context);
            GenerateWorkshopsData(context);
        }

        private static void GenerateWorkshopsData(ParentEspoirDbContext context)
        {
            if (context.WorkshopTypes.Count() == 0)
            {
                context.AddRange(new WorkshopType { Name = "VIS-HPP" }, new WorkshopType { Name = "VIS-HPS" }, new WorkshopType { Name = "ISP-PPA" });

                context.SaveChanges();
            }

            if (context.Sessions.Count() == 0)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (i == 5 && j >= 3)
                        {

                        }
                        else
                        {
                            DateTime startDate = new DateTime();
                            switch ((Season)j)
                            {
                                case (Season.Winter):
                                    startDate = new DateTime(2014 + i, SessionConstant.WINTER_START_MONTH, SessionConstant.WINTER_START_DAY);
                                    break;
                                case (Season.Spring):
                                    startDate = new DateTime(2014 + i, SessionConstant.SPRING_START_MONTH, SessionConstant.SPRING_START_DAY);
                                    break;
                                case (Season.Summer):
                                    startDate = new DateTime(2014 + i, SessionConstant.SUMMER_START_MONTH, SessionConstant.SUMMER_START_DAY);
                                    break;
                                case (Season.Fall):
                                    startDate = new DateTime(2014 + i, SessionConstant.FALL_START_MONTH, SessionConstant.FALL_START_DAY);
                                    break;
                            }
                            context.Add(new Session
                            {
                                Season = (Season)j,
                                Year = 2014 + i,
                                StartDate = startDate
                            });
                        }
                    }
                }

                context.SaveChanges();

                foreach (var session in context.Sessions)
                {
                    for (int i = 0; i < _randEngine.Next(4, 10); i++)
                    {
                        var name = GetRandomWorkshopName();

                        var x = ((int)session.Season) - 1 == 0 ? 4 : ((int)session.Season) - 1;

                        context.Add(new Workshop
                        {
                            EndDate = new DateTime(session.Year, x * 3, 28),
                            Session = session,
                            StartDate = new DateTime(session.Year, (x * 3) - 2, 28),
                            WorkshopDescription = GetWorkshopDescription(name),
                            WorkshopName = name,
                            WorkshopTypeId = context.WorkshopTypes.ToArray()[_randEngine.Next(3)].Id
                        });
                    }

                    context.SaveChanges();
                }

                foreach (var workshop in context.Workshops)
                {
                    for (int i = 0; i < _randEngine.Next(4, 10); i++)
                    {
                        context.Add(new Seance
                        {
                            SeanceDate = workshop.StartDate + TimeSpan.FromDays(1 + i * 10),
                            SeanceDescription = "Séance de travail et d'apprentissage",
                            SeanceName = $"Scéance {i + 1}",
                            SeanceTimeSpan = TimeSpan.FromHours(_randEngine.Next(1, 4)),
                            Workshop = workshop
                        });
                    }
                }

                context.SaveChanges();

                for (int i = 0; i < _randEngine.Next(1, 10); i++)
                {
                    foreach (var seance in context.Seances)
                    {
                        if (seance.SeanceDate < DateTime.Now)
                        {
                            ParticipationStatus? status = null;
                            TimeSpan nbHourLate = new TimeSpan(0,0,0);

                            if (_randEngine.Next() % 3 == 0)
                            {
                                status = ParticipationStatus.Absent;
                                nbHourLate = seance.SeanceTimeSpan;
                            }
                            else if (_randEngine.Next() % 3 == 0)
                            {
                                status = ParticipationStatus.Late;
                                nbHourLate = new TimeSpan(_randEngine.Next(2) + 1, 0, 0);
                            }
                            else
                            {
                                status = ParticipationStatus.Present;
                            }

                            context.Add(new Participant
                            {
                                Customer = GetRandomCustomer(context),
                                Status = status,
                                NbHourLate = nbHourLate,
                                Seance = seance,
                                WorkshopId = seance.WorkshopId
                            });
                        }
                        else
                        {
                            context.Add(new Participant
                            {
                                Customer = GetRandomCustomer(context),
                                Seance = seance,
                                WorkshopId = seance.WorkshopId
                            });
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        private static Customer[] _customers;

        private static Customer GetRandomCustomer(ParentEspoirDbContext context)
        {
            Debug.Assert(_customers != null || context.Customers.Count() > 0);

            if (_customers == null)
            {
                _customers = context.Customers.ToArray();
            }

            return _customers[_randEngine.Next(_customers.Length)];
        }

        private static string GetRandomWorkshopName()
        {
            var names = new string[] { "Rencontre d'orientation", "Psy de base et le codeveloppement", "S'ouvrire à la vie en santé", "Droits et devoirs des parents", "De la dépendance à mon épanoussement", "Pouvoir agir du je au nous" };
            return names[_randEngine.Next(names.Length)];
        }

        private static string GetWorkshopDescription(string workshopName)
        {
            switch (workshopName)
            {
                case "Rencontre d'orientation":
                    return "Pour tous les parents et futures parents. Tout le réseau peut référer des parents. Individuel";
                case "Psy de base et le codeveloppement":
                    return "Pour tous les parents et futures parents. Les collaborateurs possibles sont : Martine Sauvageux, Coach en CoDev du centre St-Pierre de MTL. Groupe ouvert";
                case "S'ouvrire à la vie en santé":
                    return "Pour tous les parents et futures parents. Collaborateurs : Stagiaires de l'UQTR en sc.infirmier. Groupe Fermé";
                default:
                    return "Atelier d'aide";
            }
        }

        private static void GenerateProfilOption(ParentEspoirDbContext context)
        {
            if (context.ReferenceTypes.ToList().Count() == 0)
            {
                context.AddRange(new Sex { Name = "isDelete", IsDelete = true }, new Sex { Name = "homme" }, new Sex { Name = "femme" }, new Sex { Name = "autre" });
                context.AddRange(new Parent { Name = "pere" }, new Parent { Name = "mere" });
                context.AddRange(new MaritalStatus { Name = "marie" }, new MaritalStatus { Name = "conjoint de fait" }, new MaritalStatus { Name = "celibataire" }, new MaritalStatus { Name = "separe" });
                context.AddRange(new CitizenStatus { Name = "citoyen canadien" }, new CitizenStatus { Name = "resident temporaire" }, new CitizenStatus { Name = "immigrant" });
                context.AddRange(new FamilyType { Name = "biologique" }, new FamilyType { Name = "reconstitue" }, new FamilyType { Name = "separe" });
                context.AddRange(new Language { Name = "francais" }, new Language { Name = "anglais" }, new Language { Name = "espagnol" }, new Language { Name = "mandarin" }, new Language { Name = "arabe" }, new Language { Name = "japonais" });
                context.AddRange(new HomeType { Name = "maison unifamiliale" }, new HomeType { Name = "appartement" }, new HomeType { Name = "hlm" }, new HomeType { Name = "condo" }, new HomeType { Name = "autre" });
                context.AddRange(new TransportType { Name = "voiture" }, new TransportType { Name = "camion" }, new TransportType { Name = "transport en commun" }, new TransportType { Name = "velo" }, new TransportType { Name = "autre" });
                context.AddRange(new Schooling { Name = "primaire" }, new Schooling { Name = "secondaire" }, new Schooling { Name = "collegiale" }, new Schooling { Name = "universitaire baccalaureat" }, new Schooling { Name = "universitaire maitrise" }, new Schooling { Name = "universitaire doctorat" }, new Schooling { Name = "autre" });
                context.AddRange(new IncomeSource { Name = "travail" }, new IncomeSource { Name = "aide-social" }, new IncomeSource { Name = "chomage" }, new IncomeSource { Name = "autre" });
                context.AddRange(new Availability { Name = "matin 8:00 - 10:00" }, new Availability { Name = "matin 10:00 - 12:00" }, new Availability { Name = "jour 12:00 - 14:00" }, new Availability { Name = "jour 14:00 - 16:00" }, new Availability { Name = "jour 16:00 - 18:00" }, new Availability { Name = "soir 18:00 - 20:00" }, new Availability { Name = "soir 20:00 - 22:00" });
                context.AddRange(new YearlyIncome { Name = "moins de 10 000 $" }, new YearlyIncome { Name = "Entre 10 000 $ et 25 000 $" }, new YearlyIncome { Name = "Entre 25 000 $ et 45 000 $" }, new YearlyIncome { Name = "Entre 45 000 $ et 55 000 $" }, new YearlyIncome { Name = "Entre 55 000 $ et 75 000 $" }, new YearlyIncome { Name = "Entre 75 000 $ et 100 000 $" }, new YearlyIncome { Name = "Plus de 100 000 $" });
                context.AddRange(new LegalCustody { Name = "avec droit d'acces" }, new LegalCustody { Name = "sans droit d'acces" }, new LegalCustody { Name = "garde partagee" });
                context.AddRange(new ChildrenAgeBracket { Name = "Moins de 3 ans" }, new ChildrenAgeBracket { Name = "Entre 3 et 5 ans" }, new ChildrenAgeBracket { Name = "Entre 5 et 8 ans" }, new ChildrenAgeBracket { Name = "Entre 8 et 10 ans" }, new ChildrenAgeBracket { Name = "Entre 10 et 13 ans" }, new ChildrenAgeBracket { Name = "Entre 13 et 15 ans" }, new ChildrenAgeBracket { Name = "Entre 15 et 18 ans" });
                context.AddRange(new SkillToDevelop { Name = "Habilete parental" }, new SkillToDevelop { Name = "Habilete perso et social" }, new SkillToDevelop { Name = "Saines habitude de vie" });
                context.AddRange(new SocialService { Name = "Hopitaux" }, new SocialService { Name = "CLSC" }, new SocialService { Name = "organisme communautaire" }, new SocialService { Name = "services prives" }, new SocialService { Name = "des proches" }, new SocialService { Name = "du reseau social" });

                context.AddRange(new SupportGroup { Name = "Cegep de sainte-foy" }, new SupportGroup { Name = "Universite Laval" }, new SupportGroup { Name = "Hopital regional" }, new SupportGroup { Name = "Centre des femmes" }, new SupportGroup { Name = "isDelete", IsDelete = true });
                context.AddRange(new ReferenceType { Name = "Ecole" }, new ReferenceType { Name = "Hopital" }, new ReferenceType { Name = "CLSC" }, new ReferenceType { Name = "isDelete", IsDelete = true });
                context.AddRange(new HeardOfUsFrom { Name = "reseau sociaux" }, new HeardOfUsFrom { Name = "moteur de recherche" }, new HeardOfUsFrom { Name = "journaux" }, new HeardOfUsFrom { Name = "connaissance" }, new HeardOfUsFrom { Name = "professionnel de la sante" }, new HeardOfUsFrom { Name = "isDelete", IsDelete = true });

                context.SaveChanges();
            }
        }

        private static void GenerateCustomers(ParentEspoirDbContext context)
        {
            if (!context.Customers.Any())
            {
                for (int i = 0; i < 1; i++)
                {
                    string[] names = new string[]
                    {
                        "Mary;Poppins",
                        "John;Malkovitch",
                        "Alfred;Leblanc",
                        "Foo;Bar",
                        "Samuel;Carreau",
                        "Jean-Francois;Beaudet",
                        "Miguel;Deslandes",
                        "Tommy;D'Amours",
                        "Frederic;Jacques",
                        "Thomas;Calderon",
                        "Steven;Franklin",
                        "Hammett;Leach",
                        "Neve;Sears",
                        "Britanni;York",
                        "Devin;Fry",
                        "Baker;Walker",
                        "Deacon;Colon",
                        "Timon;Coleman",
                        "Henry;Holcomb",
                        "Simon;Dotson",
                        "Jana;Gill",
                        "Hamilton;Pace",
                        "Graiden;Spears",
                        "April;Hansen",
                        "Channing;Cervantes",
                        "Jorden;Estrada",
                        "Desiree;Fowler",
                        "Jennifer;Hayden",
                        "Roanna;Roach",
                        "Hall;Lowe",
                        "Nevada;Craft",
                        "Howard;Aguirre",
                        "Darrel;Bishop",
                        "Omar;Becker",
                        "Keely;Mcmahon",
                        "Echo;Brewer",
                        "Price;Baldwin",
                        "Nora;Rodriguez",
                        "Louis;Kim",
                        "Macy;Rhodes",
                        "Sacha;Irwin",
                        "Zorita;Copeland",
                        "Anastasia;Sweeney",
                        "August;Flynn",
                        "Ila;Mclaughlin",
                        "Ginger;Kelly",
                        "Murphy;Pollard",
                        "Kylee;Fields",
                        "Aphrodite;Hughes",
                        "Kiara;Ray",
                        "Claire;Tillman",
                        "Octavius;Ball",
                        "Rashad;Butler",
                        "Noelle;Wolfe",
                        "Aquila;Ortiz",
                        "Selma;Rose",
                        "Selma;Rose",
                        "Elliott;Cleveland",
                        "Gary;Melton",
                        "Seth;Dawson",
                        "George;Shelton",
                        "Carly;Mayo",
                        "Martena;Mason",
                        "Blythe;Spence",
                        "Sage;Browning",
                        "Finn;Kelly",
                        "Keegan;Savage",
                        "Nola;Reed",
                        "Farrah;Fuller",
                        "Aquila;Tyler",
                        "Charles;Duffy",
                        "Meredith;Moses",
                        "Harper;Carey",
                        "Indira;Deleon",
                        "Jonah;Bradshaw",
                        "Tate;Mcknight",
                        "Farrah;Carroll",
                        "Brielle;Joyce",
                        "Avram;Waller",
                        "Denise;Avery",
                        "Camilla;Schroeder",
                        "Chantale;Bryant",
                        "Orli;Wynn",
                        "Lila;Padilla",
                        "Shelley;Chang",
                        "Jacqueline;Valentine",
                        "Jescie;Poole",
                        "Avye;Singleton",
                        "Shannon;Preston",
                        "Cullen;Lawson",
                        "Richard;Decker",
                        "Vance;Black",
                        "Maite;Church",
                        "Yuli;Avery",
                        "Dacey;Bowman",
                        "Cullen;Barnett",
                        "Bree;Orr",
                        "Aidan;Garcia",
                        "Peter;Wall",
                        "Harriet;Nicholson",
                        "Sasha;Conrad",
                        "Ivy;Cole",
                        "Aquila;Bernard",
                        "Dalton;Salinas",
                        "Rhonda;Alford",
                        "Veda;Shaw",
                        "Guy;Mcguire",
                        "Josiah;Floyd",
                        "Raven;Fry",
                        "Rooney;Hester"
                    };

                    foreach (var name in names)
                    {
                        var customer = new Customer
                        {
                            FileNumber = CustomerFileNumberCreator.CreateFileNumberAsync(context, new MachineDateTime()),
                            CustomerDescription = new CustomerDescription(),
                            IsDelete = false,
                            FirstName = name.Split(';')[0],
                            LastName = name.Split(';')[1],
                            NormalizedName = StringNormalizer.Normalize(name.Split(';')[0] + name.Split(';')[1]),
                            Address = "516 rue Saint-Luc",
                            PostalCode = "G1N2V3",
                            City = "Quebec",
                            Province = "Quebec",
                            Country = "Canada",
                            DateOfBirth = new DateTime(1986, 11, 25),
                            HeardOfUsFrom = context.HeardOfUsFroms.Find(3),
                            Phone = "418-271-3856",
                            SecondaryPhone = "418-895-5996",
                            ReferenceBy = context.ReferenceTypes.Find(2),
                            SupportGroup = context.SupportGroups.Find(4),
                            CreationDate = DateTime.Now
                        };
                        customer.CustomerActivations.Add(new CustomerActivation { IsActive = true, IsActiveSince = DateTime.Now });

                        context.Add(customer);

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}

