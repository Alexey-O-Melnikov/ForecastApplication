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
        private IRepository repository;
        private Entry login_Entry;
        private Entry password_Entry;
        public AuthPage(IRepository repository)
        {
            this.repository = repository;
            Initialize();
        }

        private void Initialize()
        {
            Button checkIn_Btn = new Button() { Text = "Check In" };
            checkIn_Btn.Clicked += CheckIn_Btn_Click;

            login_Entry = new Entry() { Placeholder = "Login" };

            password_Entry = new Entry() { Placeholder = "Password", IsPassword = true};

            Button signIn_Btn = new Button() { Text = "Sign In" };
            signIn_Btn.Clicked += SignIn_Btn_Click;

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(checkIn_Btn);
            stackLayout.Children.Add(login_Entry);
            stackLayout.Children.Add(password_Entry);
            stackLayout.Children.Add(signIn_Btn);

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }


        //Вход
        private async void SignIn_Btn_Click(object sender, EventArgs e)
        {
            int userId = repository.GetUserId(login_Entry.Text, password_Entry.Text);

            if(userId == 0)
               await DisplayAlert("Error", "Login or password incorrect", "Ok");
            else
               await Navigation.PushAsync(new MainPage(repository));
        }

        //Регистрация
        private void CheckIn_Btn_Click(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new CheckInPage(repository));
        }
    }
}
