using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabFood.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        void openMenu(object sender, EventArgs args)
        {
            Navigation.PushAsync(new MenuPage());
        }
        void openBooking(object sender, EventArgs args)
        {
            Navigation.PushAsync(new BookPage());
        }
        void openAbout(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AboutPage());
        }
    }
}
