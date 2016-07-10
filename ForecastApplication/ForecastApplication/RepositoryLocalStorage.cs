using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    public class RepositoryLocalStorage : IRepository
    {
        private LocalStorage storage;
        public RepositoryLocalStorage(LocalStorage storage)
        {
            this.storage = storage;
        }

        public string CreateNewUser(string login, string password, string email)
        {
            string message = "";

            message += App.validator.IsValidLogin(login);
            if (message == "" && ChekedLogin(login))
                message += "Login is already in use.\n\r";

            message += App.validator.IsValidEmail(email);
            if (message == "" && CheckedEmail(email))
                message += "Email is already in use.\n\r";

            message += App.validator.IsValidPass(password);

            if (message == "")
            {
                storage.Users.Add(
                    new User(login, password, email));
            }

            return message;
        }

        private bool CheckedEmail(string email)
        {
            var check = from user in storage.Users
                        where user.Email == email
                        select user;

            return check.Count() == 0 ? false : true;
        }

        private bool ChekedLogin(string login)
        {
            var check = from user in storage.Users
                        where user.Login == login
                        select user;

            return check.Count() == 0 ? false : true;
        }

        private bool CheckedCityInFavorites(int userId, int cityId)
        {
            var check = from favorit in storage.Favorites
                        where favorit.UserId == userId && favorit.CityId == cityId
                        select favorit;

            return check.Count() == 0 ? false : true;
        }

        public string AddCityInFavorites(int userId, int cityId)
        {
            if (CheckedCityInFavorites(userId, cityId))
                return "City already added to favorites";

            storage.Favorites.Add(
                new Favorit(userId, cityId));

            return "";
        }

        public string DeleteCityOfFavorites(int userId, int cityId)
        {
            if (CheckedCityInFavorites(userId, cityId))
                storage.Favorites.Remove(
                    new Favorit(userId, cityId));

            return "City remove from ravorites";
        }

        public List<int> GetListFavorites(int userId)
        {
            var favorites = from favorit in storage.Favorites
                            where favorit.UserId == userId
                            select favorit.CityId;

            return favorites.ToList();
        }

        public int GetUserId(string login, string password)
        {
            var userId = from user in storage.Users
                         where user.Login == login && user.Password == password
                         select user.UserId;

            return userId.FirstOrDefault();
        }
    }
}
