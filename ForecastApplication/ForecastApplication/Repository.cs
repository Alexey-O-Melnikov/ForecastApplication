using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;

namespace ForecastApplication
{

    public class Repository : IRepository
    {
        private IValidator validator;
        private SQLiteConnection db;

        public Repository(IValidator validator, string filename)
        {
            this.validator = validator;
            db = DependencyService.Get<ISQLite>().GetConnection(filename);
            db.CreateTable<User>();
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
                message += "Login is already in use.\n\r";

            message += validator.IsValidEmail(email);
            if (message == "" && CheckedEmail(email))
                message += "Email is already in use.\n\r";

            message += validator.IsValidPass(password);

            if (message == "")
            {
                User user = new User();
                user.Login = login;
                user.Password = password;
                user.Email = email;

                db.Insert(user);
            }

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
            var res = from s in db.Table<User>()
                      where s.Login == login && s.Password == password
                      select s.UserId;

            int i = res.FirstOrDefault();

            //var res1 = db.Query<User>("SELECT * FROM Users WHERE Login = " + login + "AND Password = " + password);

            //i = res1.FirstOrDefault().UserId;

            return i;
        }

        public bool CheckedEmail(string email)
        {
            return false;
        }

        public bool ChekedLogin(string login)
        {
            return false;
        }

    }
}
