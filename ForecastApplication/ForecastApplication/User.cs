using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string Login { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [Unique]
        public string Email { get; set; }
    }
}
