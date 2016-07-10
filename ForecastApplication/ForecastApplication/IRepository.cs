using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastApplication
{
    public interface IRepository
    {
        int GetUserId(string login, string password);
        string CreateNewUser(string login, string password, string email);
        List<int> GetListFavorites(int userId);
        string AddCityInFavorites(int userId, int cityId);
        string DeleteCityOfFavorites(int userId, int cityId);
    }
}
