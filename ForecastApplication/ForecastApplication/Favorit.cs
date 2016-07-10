using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    [Table("Favorites")]
    public class Favorit
    {
        //[PrimaryKey, AutoIncrement]
        //public int FavoritId { get; set; }
        public int UserId { get; set; }
        public int CityId { get; set; }
        //public User User { get; set; }

        public Favorit() { }
        public Favorit(int userId, int cityId)
        {
            UserId = userId;
            CityId = cityId;
        }
    }
}
