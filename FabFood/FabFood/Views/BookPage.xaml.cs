using FabFood.Models;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Settings;
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
        string userId = CrossSettings.Current.GetValueOrDefault("user", "");
        bool authenticated = false;
        private List<Bookings> booked;

        private int peopleValue { get; set; }

        public BookPage()
        {
            InitializeComponent();
            this.peopleValue = 1;
            peopleNumber.Text = this.peopleValue.ToString();

            deleteBookingButton.IsVisible = false;

            checkIfBooked();
            
        }
        async void checkIfBooked()
        {
            booked = await AzureManager.AzureManagerInstance.GetBookings();
            foreach (var item in booked)
            {
                if (item.UserId == userId)
                {
                    deleteBookingButton.IsVisible = true;
                }
            }
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
        async void deleteBoooking(object sender, EventArgs args)
        {
            booked = await AzureManager.AzureManagerInstance.GetBookings();
            foreach (var item in booked)
            {
                if (item.UserId == userId)
                {
                    await AzureManager.AzureManagerInstance.DeleteUsersBooking(item);
                    await DisplayAlert("Nice!", "Your booking has been /CANCELED/. Thanks for nothing.", "Sorry Not Sorry.");
                    deleteBookingButton.IsVisible = false;
                }
            }
        }
        async void postBooking(object sender, EventArgs args)
        {
            booked = await AzureManager.AzureManagerInstance.GetBookings();
            bool updated = false;
            if (authenticated == true)
            {
                

                foreach (var item in booked)
                {
                    if (item.UserId == userId)
                    {
                        item.NumberOfPeople = this.peopleValue;
                        item.Time = timePicker.Time.ToString();
                        item.Date = datePicker.Date;
                        item.UserId = userId;
                        await AzureManager.AzureManagerInstance.UpdateUsersBooking(item);
                        await DisplayAlert("Nice!", "Your booking has been /updated/. See you soon!", "kewl.");
                        updated = true;
                    }
                }
                if (!updated)
                {
                    Bookings book = new Bookings()
                    {
                        NumberOfPeople = this.peopleValue,
                        ExtraDetails = "NoDetailsForNow",
                        Time = timePicker.Time.ToString(),
                        Date = datePicker.Date,
                        UserId = userId
                    };
                    await AzureManager.AzureManagerInstance.AddBookings(book);
                    await DisplayAlert("Nice!", "Your booking has been made. See you soon!", "Fine.");
                    deleteBookingButton.IsVisible = true;
                }
                
            }
            else
            {
                await DisplayAlert("Hey Dude!", "You need to sign in!", "OK");
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            string userId = CrossSettings.Current.GetValueOrDefault("user", "");
            string token = CrossSettings.Current.GetValueOrDefault("token", "");
            if (!token.Equals("") && !userId.Equals(""))
            {
                MobileServiceUser user = new MobileServiceUser(userId);
                user.MobileServiceAuthenticationToken = token;
                AzureManager.AzureManagerInstance.AzureClient.CurrentUser = user;

                authenticated = true;
            }
            else
            {
                Navigation.PushAsync(new MainPage());
            }
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
