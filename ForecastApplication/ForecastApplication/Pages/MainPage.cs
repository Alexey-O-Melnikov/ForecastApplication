using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace ForecastApplication
{
    class MainPage : ContentPage
    {
        Entry city_Entry;
        private string cityName = "";
        public MainPage()
        {
            Initialize();
        }

        private async void Initialize()
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

            Grid forecastForCity_Grid = null;
            if (!String.IsNullOrWhiteSpace(cityName))
            {
                forecastForCity_Grid = await InitializeGrid(cityName);
            }

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
            if (forecastForCity_Grid != null)
                stackLayout.Children.Add(forecastForCity_Grid);

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }

        private async Task<Grid> InitializeGrid(string cityName = "", int cityId = 0)
        {
            ApiOpenweathermapWorker apiWorker = new ApiOpenweathermapWorker();
            await apiWorker.CreatWorker(cityName, cityId);

            Grid grid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 50 },
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 80 },
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 30 },
                    new RowDefinition { Height = 30 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowSpacing = 5
            };


            Label cityAndCountry = new Label { Text = apiWorker.GetCityName() + ", " + apiWorker.GetCounry(), FontSize = 20, FontAttributes = FontAttributes.Bold };
            Label mainForecast = new Label { Text = Math.Round(apiWorker.GetTemp(), 1).ToString() + " C", FontSize = 25 };
            Label dateTimeForecast = new Label { Text = "get at " + apiWorker.GetDateTime().ToString("yyyy.MM.dd hh:mm") };
            Label weatherMain = new Label { Text = apiWorker.GetWeatherMain() };
            Image icon = new Image { Source = apiWorker.GetIcon() };

            grid.Children.Add(cityAndCountry,0,0);
            Grid.SetColumnSpan(cityAndCountry, 2);
            grid.Children.Add(icon, 0, 1);
            grid.Children.Add(mainForecast,1,1);
            grid.Children.Add(weatherMain, 0, 2);
            Grid.SetColumnSpan(weatherMain, 2);
            grid.Children.Add(dateTimeForecast,0,3);
            Grid.SetColumnSpan(dateTimeForecast, 2);
            grid.Children.Add(new Label { Text = "Wind"}, 0, 4);
            grid.Children.Add(new Label { Text = Math.Round(apiWorker.GetWindSpeed(), 2).ToString() + "m/s\n\rWind direction (" + Math.Round(apiWorker.GetWindDirectiond(), 3).ToString() + ")" }, 1, 4);
            grid.Children.Add(new Label { Text = "Cloudiness" }, 0, 5);
            grid.Children.Add(new Label { Text = apiWorker.GetCloudiness() }, 1, 5);
            grid.Children.Add(new Label { Text = "Pressure" }, 0, 6);
            grid.Children.Add(new Label { Text = Math.Round(apiWorker.GetPressure(), 1).ToString() + "hPa" }, 1, 6);
            grid.Children.Add(new Label { Text = "Humidity" }, 0, 7);
            grid.Children.Add(new Label { Text = Math.Round(apiWorker.GetHumidity()).ToString() + "%" }, 1, 7);
            grid.Children.Add(new Label { Text = "Get coords" }, 0, 8);
            grid.Children.Add(new Label { Text = "[" + Math.Round(apiWorker.GetCoordsLon(), 2) + ", " + Math.Round(apiWorker.GetCoordsLat(), 2) + "]" }, 1, 8);

            return grid;
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
            cityName = city_Entry.Text;
            Initialize();
        }
    }
}
