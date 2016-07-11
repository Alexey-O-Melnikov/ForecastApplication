using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;


namespace ForecastApplication
{
    class MainPage : ContentPage
    {
        Entry city_Entry;
        private int cityId = 0;
        public MainPage()
        {
            Initialize();
        }

        private async void Initialize()
        {
            Title = "Forecast";
            Button signIn_Btn = new Button() { Text = "Sign In" };
            signIn_Btn.Clicked += SignIn_Btn_Click;
            Button signOut_Btn = new Button() { Text = "Sign Out" };
            signOut_Btn.Clicked += SignOut_Btn_Click;

            Button favorites_Btn = new Button() { Text = "Favorites" };
            favorites_Btn.Clicked += Favorites_Btn_Click;
            city_Entry = new Entry() { Placeholder = "city" };
            Button citySearch_Btn = new Button() { Text = "Search" };
            citySearch_Btn.Clicked += CitySearch_Btn_Click;

            Button addInFavorites_Btn = new Button() { Text = "Add in favorites" };
            addInFavorites_Btn.Clicked += AddInFavorites_Btn_Click;
            Button delOfFavorites_Btn = new Button() { Text = "Delete of favorites" };
            delOfFavorites_Btn.Clicked += DelOfFavorites_Btn_Click;

            Grid forecastForCity_Grid = null;
            if (!String.IsNullOrWhiteSpace(App.cityName))
            {
                forecastForCity_Grid = await InitializeGrid(App.cityName);
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

            if (!String.IsNullOrWhiteSpace(App.cityName) && App.userId != 0)
            {
                cityId = App.apiWorker.GetCityId();
                stackLayout.Children.Add(
                    App.repository.CheckedCityInFavorites(App.userId, cityId) ?
                        delOfFavorites_Btn :
                        addInFavorites_Btn
                );
            }

            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;
        }

        private WebView InitializeMap()
        {
            WebView webView = new WebView
            {
                Source = new UrlWebViewSource
                {
                    Url = @"http://openweathermap.org/weathermap",
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            return webView;
        }

        private async Task<Grid> InitializeGrid(string cityName = "", int cityId = 0)
        {
            string message = App.validator.IsValidCityName(cityName);
            if(message != "")
            {
                await DisplayAlert("Error", message, "Ok");
                return null;
            }
            await App.apiWorker.CreatWorker(cityName, cityId);

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

            Label cityAndCountry = new Label { Text = App.apiWorker.GetCityName() + ", " + App.apiWorker.GetCounry(), FontSize = 20, FontAttributes = FontAttributes.Bold };
            Label mainForecast = new Label { Text = Math.Round(App.apiWorker.GetTemp(), 1).ToString() + " C", FontSize = 25 };
            Label dateTimeForecast = new Label { Text = "get at " + App.apiWorker.GetDateTime().ToString("yyyy.MM.dd hh:mm") };
            Label weatherMain = new Label { Text = App.apiWorker.GetWeatherMain() };
            Image icon = new Image { Source = App.apiWorker.GetIcon() };

            grid.Children.Add(cityAndCountry,0,0);
            Grid.SetColumnSpan(cityAndCountry, 2);
            grid.Children.Add(icon, 0, 1);
            grid.Children.Add(mainForecast,1,1);
            grid.Children.Add(weatherMain, 0, 2);
            Grid.SetColumnSpan(weatherMain, 2);
            grid.Children.Add(dateTimeForecast,0,3);
            Grid.SetColumnSpan(dateTimeForecast, 2);
            grid.Children.Add(new Label { Text = "Wind"}, 0, 4);
            grid.Children.Add(new Label { Text = Math.Round(App.apiWorker.GetWindSpeed(), 2).ToString() + "m/s\n\rWind direction (" + Math.Round(App.apiWorker.GetWindDirectiond(), 3).ToString() + ")" }, 1, 4);
            grid.Children.Add(new Label { Text = "Cloudiness" }, 0, 5);
            grid.Children.Add(new Label { Text = App.apiWorker.GetCloudiness() }, 1, 5);
            grid.Children.Add(new Label { Text = "Pressure" }, 0, 6);
            grid.Children.Add(new Label { Text = Math.Round(App.apiWorker.GetPressure(), 1).ToString() + "hPa" }, 1, 6);
            grid.Children.Add(new Label { Text = "Humidity" }, 0, 7);
            grid.Children.Add(new Label { Text = Math.Round(App.apiWorker.GetHumidity()).ToString() + "%" }, 1, 7);
            grid.Children.Add(new Label { Text = "Get coords" }, 0, 8);
            grid.Children.Add(new Label { Text = "[" + Math.Round(App.apiWorker.GetCoordsLon(), 2) + ", " + Math.Round(App.apiWorker.GetCoordsLat(), 2) + "]" }, 1, 8);

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
            App.cityName = city_Entry.Text;
            Initialize();
        }

        private void DelOfFavorites_Btn_Click(object sender, EventArgs e)
        {
            string message = App.repository.DeleteCityOfFavorites(App.userId, cityId);
            DisplayAlert(message, App.cityName, "Ok");
            Initialize();
        }

        private void AddInFavorites_Btn_Click(object sender, EventArgs e)
        {
            string message = App.repository.AddCityInFavorites(App.userId, cityId);
            DisplayAlert(message, App.cityName, "Ok");
            Initialize();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Initialize();
        }
    }
}
