using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ForecastApplication
{
    public class Validator : IValidator
    {
        private bool IsValid(string value, string pattern)
        {
            Regex check = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
            bool valid = false;

            if (string.IsNullOrEmpty(value))
                valid = false;
            else
                valid = check.IsMatch(value);

            return valid;
        }
        public string IsValidCityName(string cityName)
        {
            string message = "";
            string pattern = @"/^.*[^A-zА-яЁё].*$/";

            if(!IsValid(cityName, pattern))
                message += "Use only letters.\n\r";

            return message;
        }

        public string IsValidEmail(string email)
        {
            string message = "";
            string pattern = @"/^([\w\._]+)@\1\.([a-z]{2,6}\.?)$/";

            if(IsValid(email, pattern))
                message = "Not valid Email.\n\r";
            return message;
        }

        public string IsValidLogin(string login)
        {
            string message = "";
            string pattern = @"/^[A-z0-9_-]{3,49}$/";

            if (IsValid(login, pattern))
                message = "Login should consist of letters, numbers, underscores and hyphens. Limit 49 characters.\n\r";

            return message;
        }

        public string IsValidPass(string password)
        {
            string message = "";
            string pattern = @"/^{8,49}$/";

            if(IsValid(password, pattern))
                message += "The password must be between 8 and 49 characters.\n\r";

            return message;
        }
    }
}
