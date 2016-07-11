using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    public class LocalStorage
    {
        public List<User> Users { get; set; }
        public List<Favorit> Favorites { get; set; }

        public LocalStorage()
        {
            Users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Login = "q",
                    Email = "alex@mel.ru",
                    Password = "q"
                },
                new User
                {
                    UserId = 2,
                    Login = "Qwerty",
                    Email = "qqq123@gmail.com",
                    Password = "1qaz!QAZ"
                },
            };

            Favorites = new List<Favorit>
            {
                new Favorit
                {
                    CityId = 535121,
                    UserId = 1,
                },
                new Favorit
                {
                    CityId = 524901,
                    UserId = 1,
                },
                new Favorit
                {
                    CityId = 2643743,
                    UserId = 1,
                },
                new Favorit
                {
                    CityId = 2122311,
                    UserId = 1,
                },
                new Favorit
                {
                    CityId = 823678,
                    UserId = 1,
                },
                new Favorit
                {
                    CityId = 2950159,
                    UserId = 2,
                },
                new Favorit
                {
                    CityId = 472045,
                    UserId = 2,
                },
                new Favorit
                {
                    CityId = 524901,
                    UserId = 2,
                },
            };
        }
    }
}
