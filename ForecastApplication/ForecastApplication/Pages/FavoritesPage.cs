using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ForecastApplication
{
    class FavoritesPage : ContentPage
    {
        public FavoritesPage()
        {
            Initialize();
        }

        private async void Initialize()
        {
            Title = "Favorites";

            StackLayout stackLayout = new StackLayout();

            foreach (var cityId in App.repository.GetListFavorites(App.userId))
            {
                await App.apiWorker.CreatWorker(cityId: cityId);

                Button city_Btn = new Button { Text = App.apiWorker.GetCityName() };
                city_Btn.Clicked += City_Btn_Click;

                stackLayout.Children.Add(city_Btn);
            }

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;

        }

        private void City_Btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            App.cityName = btn.Text;
            Navigation.PopAsync();
        }
    }

}
