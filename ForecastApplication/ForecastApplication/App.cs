using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ForecastApplication
{
    public class App : Application
    {
        public App()
        {
            IValidator validator = new Validator();
            IRepository repository = new Repository(validator);
            MainPage = new AuthPage(repository);
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
