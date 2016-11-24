using FabFood.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FabFood
{
    class MainLink : Button
    {
        public MainLink(string name)
        {
            HeightRequest = 50;
            BackgroundColor = Color.FromHex("e91e63");

            Text = name;
            Command = new Command(o =>
            {
                if (name == "BookPage") { App.MasterDetailPage.Detail = new NavigationPage(new BookPage()); }
                else if (name == "AboutPage") { App.MasterDetailPage.Detail = new NavigationPage(new AboutPage()); }
                else { App.MasterDetailPage.Detail = new NavigationPage(new MainPage()); }

                App.MasterDetailPage.IsPresented = false;
            });
        }
    }
}
