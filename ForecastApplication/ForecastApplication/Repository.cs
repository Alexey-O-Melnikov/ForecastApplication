using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{

    public class Repository : IRepository
    {
        private IValidator validator;

        public Repository(IValidator validator)
        {
            this.validator = validator;
        }

        public string AddCityInFavorites(int userId, int cityId)
        {
            throw new NotImplementedException();
        }

        public string CreateNewUser(string login, string password, string email)
        {
            string message = "";

            message += validator.IsValidLogin(login);
            if (message == "" && ChekedLogin(login))
                message += "";

            message += validator.IsValidEmail(email);
            if (message == "" && CheckedEmail(email))
                message += "";

            message += validator.IsValidPass(password);

            if (message == "")
            /*запрос*/;

            return message;
        }

        public string DeleteCityOfFavorites(int userId, int cityId)
        {
            throw new NotImplementedException();
        }

        public List<Favorit> GetListFavorites(int userId)
        {
            throw new NotImplementedException();
        }

        public int GetUserId(string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool CheckedEmail(string email)
        {
            throw new NotImplementedException();
        }

        public bool ChekedLogin(string login)
        {
            throw new NotImplementedException();
        }

    }
}
