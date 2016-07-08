using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    public interface IValidator
    {
        string IsValidEmail(string email);
        string IsValidPass(string password);
        string IsValidLogin(string login);
        string IsValidCityName(string cityName);
    }
}
