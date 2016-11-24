using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabFood.Views
{
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform<int>(0, 80, 0), 0, 0),
                Children = {
                new MainLink("MainPage"),
                new MainLink("BookPage"),
                new MainLink("AboutPage"),
                new MainLink("ReactionPage")
                },
                Spacing = 0,
                MinimumHeightRequest = 40,
            };
            Title = "Master";
            BackgroundColor = Color.White.WithLuminosity(1);
            Icon = Device.OS == TargetPlatform.iOS ? "menu.png" : null;
        }
    }
}
