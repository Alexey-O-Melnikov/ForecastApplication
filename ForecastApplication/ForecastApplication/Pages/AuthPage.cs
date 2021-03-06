﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForecastApplication
{
    class AuthPage : ContentPage
    {
        private Entry login_Entry;
        private Entry password_Entry;
        public AuthPage()
        {
            Initialize();
        }

        private void Initialize()
        {
            Title = "Sign In";
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
            App.userId = App.repository.GetUserId(login_Entry.Text, password_Entry.Text);

            if(App.userId == 0)
               await DisplayAlert("Error", "Login or password incorrect", "Ok");
            else
               await Navigation.PopAsync();
        }

        //Регистрация
        private async void CheckIn_Btn_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CheckInPage());
        }
    }
}
