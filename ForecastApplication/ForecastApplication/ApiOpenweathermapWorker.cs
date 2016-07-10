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
        private dynamic forecast;
        public async Task<bool> CreatWorker(string cityName = "", int cityId = 0)
        {
            forecast = await GetForecast(cityName, cityId);
            return true;
        }
        public async Task<dynamic> GetForecast(string cityName = "", int cityId = 0)
        {
            if (cityName == "" && cityId == 0)
                return null;

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

        public string GetCityName()
        {
            return forecast?.city.name ?? "Error";
        }

        public string GetCounry()
        {
            return forecast?.city.country ?? "Error";
        }

        public string GetIcon()
        {
            return @"http://api.openweathermap.org/img/w/" + forecast?.list[0].weather[0].icon + ".png" ?? "Error";
        }

        public string GetWeatherMain()
        {
            return forecast?.list[0].weather[0].main;
        }

        public DateTime GetDateTime()
        {
            return (DateTime)forecast?.list[0].dt_txt;
        }

        public double GetTemp()
        {
            double zeroKelvin = -273.15;
            return (double)forecast?.list[0].main.temp + zeroKelvin;
        }

        public double GetWindSpeed()
        {
            return (double)forecast?.list[0].wind.speed;
        }

        public double GetWindDirectiond()
        {
            return (double)forecast?.list[0].wind.deg;
        }

        public string GetCloudiness()
        {
            return forecast?.list[0].weather[0].description ?? "Error";
        }

        public double GetPressure()
        {
            return (double)forecast?.list[0].main.pressure;
        }

        public double GetHumidity()
        {
            return (double)forecast?.list[0].main.humidity;
        }

        public string GetSunrise()
        {
            return "error";
        }

        public string GetSunset()
        {
            return "error";
        }

        public double GetCoordsLon()
        {
            return (double)forecast?.city.coord.lon;
        }

        public double GetCoordsLat()
        {
            return (double)forecast?.city.coord.lat;
        }
    }
}
