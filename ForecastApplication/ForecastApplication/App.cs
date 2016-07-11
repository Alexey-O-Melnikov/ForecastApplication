using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ForecastApplication
{
    public class App : Application
    {
        public static IRepository repository;
        public static IValidator validator;
        public static ApiOpenweathermapWorker apiWorker;
        public static int userId = 0;
        public static string cityName = "";
        public App()
        {
            validator = new Validator();
            repository = new RepositoryLocalStorage(new LocalStorage());
            apiWorker = new ApiOpenweathermapWorker();
            //repository = new Repository("ForecastApplication.db");
            //MainPage = new NavigationPage(new AuthPage(repository));
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
