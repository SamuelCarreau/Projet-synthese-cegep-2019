using System;
using System.Text;

namespace ParentEspoir.Domain.Constants
{
    public static class NoteConstant
    {
        public static int NAME_MAX_LENGHT = 50;
        public static string NAME_MAX_LENGHT_ERROR = $"Doit contenir {NAME_MAX_LENGHT} caractere au maximum";
        public static int NAME_MIN_LENGHT = 4;
        public static string NAME_MIN_LENGHT_ERROR = $"Doit contenir {NAME_MIN_LENGHT} caractere au minimum";
        public static int BODY_MIN_LENGHT = 15;

        public static string MAX_DATE_ERROR ="Ne peut pas être dans le futur";

        //REGEX
        
        public const string MATCH_ONLY_ALPHANUMERIC_SPACE = @"^([\u00c0-\u01ffa-zA-Z'\-\s])+$";
        public const string MATCH_ONLY_ALPHANUMERIC_SPACE_ERROR = "Doit contenir que des lettres, chiffre ou espace";
        public const string MATCH_ONLY_ALPHANUMERIC = "^[a-zA-Z0-9]+$";
        public const string MATCH_ONLY_ALPHA_SPACE = "^[a-zA-Z ]+$";
        public const string MATCH_ONLY_ALPHA = "^[a-zA-Z]+$";

        public const string ERROR_ISREQUIRED = "Une valeur est requise";
    }
}