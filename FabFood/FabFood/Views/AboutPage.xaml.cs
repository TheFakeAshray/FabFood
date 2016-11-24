using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace FabFood.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var position = new Position(-36.852228, 174.768799); // Latitude, Longitude
            
            MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromMiles(1)));
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = position,
                Label = "FabFoods",
                Address = "Above Shadows Obviously ;)"
            };
            MyMap.Pins.Add(pin);
        }
    }
}
