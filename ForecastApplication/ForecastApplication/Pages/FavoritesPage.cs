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

        private void Initialize()
        {
            Title = "Favorites";
            Button addCity_Btn = new Button() { Text = "Added city" };

            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(addCity_Btn);



            ScrollView scrollView = new ScrollView() { Content = stackLayout };

            this.Content = scrollView;

        }
    }

}
