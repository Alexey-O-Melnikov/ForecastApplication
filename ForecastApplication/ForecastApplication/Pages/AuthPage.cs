using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForecastApplication
{
    class AuthPage : ContentPage
    {
        public AuthPage()
        {
            Initialize();
        }

        private void Initialize()
        {
            Entry login_Entry = new Entry() { Placeholder = "Login" };
            Entry password_Entry = new Entry() { Placeholder = "Password", IsPassword = true};
            Button addCity_Btn = new Button() { Text = "check in" };

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(login_Entry);
            stackLayout.Children.Add(password_Entry);
            stackLayout.Children.Add(addCity_Btn);

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }
    }
}
