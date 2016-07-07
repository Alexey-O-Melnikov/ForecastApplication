using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace ForecastApplication
{
    public class ApiOpenweathermapWorker
    {
        private dynamic _forecast;
        public ApiOpenweathermapWorker()
        {
            _forecast = GetForecast();
        }
        public async Task<dynamic> GetForecast(string cityName = "", int cityId = 0)
        {
            string site = @"http://api.openweathermap.org/data/2.5/forecast/city?";
            string isName = "q=";
            string isId = "id=";
            string apiid = @"&APPID=ab2d0601159d10719014d247c5335083";

            string strReq =
                site +
                (cityId == 0 && cityName != "" ? isName + cityName : isId + cityId) +
                apiid;

            dynamic forecast = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strReq);
                var result = await client.GetAsync(strReq);
                if (result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = await result.Content.ReadAsStringAsync();
                    forecast = JsonConvert.DeserializeObject(json);
                }
            }

            return forecast;
        }

        public string GetCounry()
        {
            return @"http://api.openweathermap.org/img/w/" + _forecast.city.country + ".png";
        }

        public string GetIcon()
        {
            return  _forecast.list[0].weather.icon;
        }


        public DateTime GetDateTime()
        {
            return (DateTime)_forecast.list[0].dt_txt;
        }

        public double GetTemp()
        {
            double zeroKelvin = -273.15;
            return (double)_forecast.list[0].main.temp + zeroKelvin;
        }

        public double GetWindSpeed()
        {
            return (double)_forecast.list[0].wind.speed;
        }

        public double GetWindDirectiond()
        {
            return (double)_forecast.list[0].wind.deg;
        }

        public string GetCloudiness()
        {
            return _forecast.list[0].weather.description;
        }

        public double GetPressure()
        {
            return (double)_forecast.list[0].main.pressure;
        }

        public double GetHumidity()
        {
            return (double)_forecast.list[0].main.humidity;
        }

        public string GetSunrise()
        {
            return "error";
        }

        public string GetSunset()
        {
            return "error";
        }

        public string GetCoords()
        {
            return _forecast.city.coord.lon + " " + _forecast.city.coord.lat;
        }
    }
}
