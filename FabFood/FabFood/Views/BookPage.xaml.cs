using FabFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabFood.Views
{
    public partial class BookPage : ContentPage
    {
        private int peopleValue { get; set; }

        public BookPage()
        {
            InitializeComponent();
            this.peopleValue = 1;
            peopleNumber.Text = this.peopleValue.ToString();
        }
        void peopleOrPerson(object sender, EventArgs args)
        {
            if (this.peopleValue == 1)
            {
                peoplePerson.Text = "Person";
            }
            else
            {
                peoplePerson.Text = "People";
            }
        }
        void stepUp_Clicked(object sender, EventArgs args)
        {
            if (this.peopleValue < 10)
            {
                this.peopleValue += 1;
            }
            peopleNumber.Text = this.peopleValue.ToString();
            this.peopleOrPerson(sender, args);
        }
        void stepDown_Clicked(object sender, EventArgs args)
        {
            if (this.peopleValue > 1)
            {
                this.peopleValue -= 1;
            }
            peopleNumber.Text = this.peopleValue.ToString();
            this.peopleOrPerson(sender, args);
        }


        async void postBooking(object sender, EventArgs args)
        {
            Bookings book = new Bookings()
            {
                NumberOfPeople = this.peopleValue,
                ExtraDetails = "NoDetailsForNow",
                Time = timePicker.Time.ToString(),
                Date = datePicker.Date
            };
            await AzureManager.AzureManagerInstance.AddBookings(book);
        }
        //public static MobileServiceClient MobileService =
        //    new MobileServiceClient(
        //    "https://fabfood.azurewebsites.net"
        //);
        //async void call(object sender, EventArgs args)
        //{

        //TodoItem item = new TodoItem { Text = "Awesome item" };
        //    await MobileService.GetTable<TodoItem>().InsertAsync(item);
        //}
    }
}
