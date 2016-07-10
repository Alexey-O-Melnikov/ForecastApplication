using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForecastApplication
{
    class CheckInPage : ContentPage
    {
        private Entry login_Entry;
        Entry password_Entry;
        Entry email_Entry;
        public CheckInPage()
        {
            Initialize();
        }
        private void Initialize()
        {
            Title = "Check In";
            login_Entry = new Entry() { Placeholder = "Login" };
            password_Entry = new Entry() { Placeholder = "Password", IsPassword = true };
            email_Entry = new Entry() { Placeholder = "Email" };

            Button checkIn_Btn = new Button() { Text = "Check In" };
            checkIn_Btn.Clicked += CheckIn_Btn_Click;

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(login_Entry);
            stackLayout.Children.Add(password_Entry);
            stackLayout.Children.Add(email_Entry);
            stackLayout.Children.Add(checkIn_Btn);

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }

        private async void CheckIn_Btn_Click(object sender, EventArgs e)
        {
            string message = App.repository.CreateNewUser(login_Entry.Text, password_Entry.Text, email_Entry.Text);

            App.userId = App.repository.GetUserId(login_Entry.Text, password_Entry.Text);

            if (message != "")
            {
                await DisplayAlert("Error", message, "Ok");
            }
            else
            {
                await Navigation.PopToRootAsync();
            }
        }
    }
}
