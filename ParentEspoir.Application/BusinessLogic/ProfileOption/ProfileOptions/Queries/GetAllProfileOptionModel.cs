using ParentEspoir.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public class GetAllProfileOptionModel
    {
        public static string AVAILABILITY_TO_FRENCH = "Disponibilité";
        public static string CHILDREN_AGE_BRACKET_TO_FRENCH = "Tranche d\'âge des enfants";
        public static string CITIZEN_STATUS_TO_FRENCH = "Status citoyen";
        public static string DOCUMENT_TYPE_TO_FRENCH = "Type de document";
        public static string FAMILY_TYPE_TO_FRENCH = "Type de famille";
        public static string HEARD_OF_US_FROM_TO_FRENCH = "À entendu parler de nous par";
        public static string HOME_TYPE_TO_FRENCH = "Type de domicile";
        public static string INCOME_SOURCE_TO_FRENCH = "Source de revenue";
        public static string LANGUAGE_TO_FRENCH = "Langage";
        public static string LEGAL_CUSTODY_TO_FRENCH = "Garde légale";
        public static string MARITAL_STATUSTO_FRENCH = "État civil";
        public static string NOTE_TYPE_TO_FRENCH = "Type de note";
        public static string PARENT_TO_FRENCH = "Parent";
        public static string REFERENCE_TYPE_TO_FRENCH = "Type de référence";
        public static string SCHOOLING_TO_FRENCH = "Étude";
        public static string SEX_TO_FRENCH = "Sexe";
        public static string SKILL_TO_DEVELOP_TO_FRENCH = "Aptitude à développer";
        public static string SOCIAL_SERVICE_TO_FRENCH = "Service social";
        public static string TRANSPORT_TYPE_TO_FRENCH = "Type de tansport";
        public static string VOLUNTEERING_TYPE_TO_FRENCH = "Type de bénévolat";
        public static string YEARLY_INCOME_TO_FRENCH = "Revenu annuel";

        public GetAllProfileOptionModel()
        {
            Options = new Dictionary<string, List<IProfileOption>>(Traductions.Count);
        }

        public void Add(string key, List<IProfileOption> value)
        {
            Options.Add(key, value);
        }

        public List<IProfileOption> this[string key]
        {
            get
            {
                return Options[key];
            }
            set
            {
                Options[key] = value;
            }
        }

        public Dictionary<string, List<IProfileOption>> Options { get; set; }

        public Dictionary<string, string> Traductions { get; set; } = new Dictionary<string, string>()
        {
            { typeof(Availability).Name, AVAILABILITY_TO_FRENCH},
            { typeof(ChildrenAgeBracket).Name, CHILDREN_AGE_BRACKET_TO_FRENCH },
            { typeof(CitizenStatus).Name, CITIZEN_STATUS_TO_FRENCH },
            { typeof(DocumentType).Name, DOCUMENT_TYPE_TO_FRENCH },
            { typeof(FamilyType).Name, FAMILY_TYPE_TO_FRENCH },
            { typeof(HeardOfUsFrom).Name, HEARD_OF_US_FROM_TO_FRENCH},
            { typeof(HomeType).Name, HOME_TYPE_TO_FRENCH },
            { typeof(IncomeSource).Name, INCOME_SOURCE_TO_FRENCH },
            { typeof(Language).Name, LANGUAGE_TO_FRENCH },
            { typeof(LegalCustody).Name, LEGAL_CUSTODY_TO_FRENCH },
            { typeof(MaritalStatus).Name, MARITAL_STATUSTO_FRENCH },
            { typeof(NoteType).Name, NOTE_TYPE_TO_FRENCH },
            { typeof(Parent).Name, PARENT_TO_FRENCH },
            { typeof(ReferenceType).Name, REFERENCE_TYPE_TO_FRENCH },
            { typeof(Schooling).Name, SCHOOLING_TO_FRENCH },
            { typeof(Sex).Name, SEX_TO_FRENCH },
            { typeof(SkillToDevelop).Name, SKILL_TO_DEVELOP_TO_FRENCH },
            { typeof(SocialService).Name, SOCIAL_SERVICE_TO_FRENCH },
            { typeof(TransportType).Name, TRANSPORT_TYPE_TO_FRENCH },
            { typeof(VolunteeringType).Name, VOLUNTEERING_TYPE_TO_FRENCH },
            { typeof(YearlyIncome).Name, YEARLY_INCOME_TO_FRENCH }
        };
    }
}
