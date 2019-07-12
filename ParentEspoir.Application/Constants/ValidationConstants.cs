using System;
using System.Collections.Generic;
using System.Text;

namespace ParentEspoir.Application
{
    public static class ValidationConstants
    {
        public static string REQUIRED_FIELD_MESSAGE = "ce champs est requis";
        public static string NAME_ERROR_MESSAGE = "Le nom saisie n'est pas valide.";

        // This regex is use for first names and last names
        public static string NAME_VALIDATION_REGEX = @"^([\u00c0-\u01ffa-zA-Z'\-])+$";
    }
}
