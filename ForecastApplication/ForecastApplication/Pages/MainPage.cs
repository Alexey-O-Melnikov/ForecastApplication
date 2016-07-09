using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Windows.UI.Xaml.Controls;
using Xamarin.Forms;


namespace ForecastApplication
{
    class MainPage : ContentPage
    {
        private Entry city_Entry;
        public MainPage()
        {
            Initialize();
        }

        private void Initialize()
        {
            Button signIn_Btn = new Button() { Text = "Sign In" };
            signIn_Btn.Clicked += SignIn_Btn_Click;
            Button signOut_Btn = new Button() { Text = "Sign Out" };
            signOut_Btn.Clicked += SignOut_Btn_Click;

            Button favorites_Btn = new Button() { Text = "Favorites" };
            favorites_Btn.Clicked += Favorites_Btn_Click;
            city_Entry = new Entry() { Placeholder = "city" };
            Button citySearch_Btn = new Button() { Text = "Search" };
            citySearch_Btn.Clicked += CitySearch_Btn_Click;

            Picker picker = new Picker() { Title = "Favorites" };
            picker.Items.Add("Lipetsk");
            picker.Items.Add("Moscow");
            picker.Items.Add("Voronez");
            picker.Items.Add("Elec");

            StackLayout stackLayout = new StackLayout();
            if(App.userId == 0)
            {
                stackLayout.Children.Add(signIn_Btn);
            }
            else
            {
                stackLayout.Children.Add(signOut_Btn);
                stackLayout.Children.Add(favorites_Btn);
            }
            stackLayout.Children.Add(city_Entry);
            stackLayout.Children.Add(citySearch_Btn);
            //stackLayout.Children.Add(picker);

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }

        private void SignOut_Btn_Click(object sender, EventArgs e)
        {
            App.userId = 0;
            Initialize();
        }

        private async void SignIn_Btn_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AuthPage());
        }

        private async void Favorites_Btn_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavoritesPage());
        }

        private void CitySearch_Btn_Click(object sender, EventArgs e)
        {
            ApiOpenweathermapWorker apiWorker = new ApiOpenweathermapWorker();
        }
    }
}
