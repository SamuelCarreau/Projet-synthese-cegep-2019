using System;
using System.Text;

namespace ParentEspoir.Domain.Constants
{
    public static class CustomerConstant
    {
        public static int FIRST_NAME_MAX_LENGHT = 50;
        public static int LAST_NAME_MAX_LENGHT = 50;
        public static int ADDRESS_MAX_LENGHT = 75;
        public static int CITY_MAX_LENGHT = 75;
        public static int POSTAL_CODE_MAX_LENGHT = 20;
        public static int PROVINCE_MAX_LENGHT = 75;
        public static int COUNTRY_MAX_LENGHT = 75;
        public static int PHONE_MAX_LENGHT = 20;
        public static int SECONDARY_PHONE_MAX_LENGHT = 20;

        public static string ERROR_MESSAGE_FIRST_NAME = $"Le prénom doit contenir moins que {CustomerConstant.FIRST_NAME_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_LAST_NAME = $"Le nom doit contenir moins que {CustomerConstant.LAST_NAME_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_DATEOFBIRTH_AFTER_1900 = "La date de naissance doit être après le 01-01-1900";
        public static string ERROR_MESSAGE_DATEOFBIRTH_BEFORE_NOW = "La date doit être plus petit que la date courante";
        public static string ERROR_MESSAGE_ADDRESS = $"L'adresse doit contenir moins que {CustomerConstant.ADDRESS_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_CITY = $"La ville doit contenir moins que {CustomerConstant.CITY_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_POSTAL_CODE = $"Le code postale doit contenir moins que {CustomerConstant.POSTAL_CODE_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_PROVINCE = $"La province doit contenir moins que {CustomerConstant.PROVINCE_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_COUNTRY = $"Le pays doit contenir moins que {CustomerConstant.COUNTRY_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_PHONE = $"Le numéro de téléphone ne doit pas être vide et doit être plus petit que {CustomerConstant.PHONE_MAX_LENGHT} caractères";
        public static string ERROR_MESSAGE_SECONDARY_PHONE = $"Le second numéro de téléphone ne doit pas être vide et doit être plus petit que {CustomerConstant.SECONDARY_PHONE_MAX_LENGHT} caractères";
    }
}