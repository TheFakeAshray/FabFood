﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Settings;
using Microsoft.WindowsAzure.MobileServices;

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
        bool authenticated = false;
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

            if (authenticated == true)
            {
                //Hide the Sign-in Button
                this.loginButton.IsVisible = false;
            }
        }

        async void loginButton_Clicked(object sender, EventArgs args)
        {
            if (App.Authenticator != null)
            {
                authenticated = await App.Authenticator.Authenticate();
            }
            if (authenticated == true)
            {
                this.loginButton.IsVisible = false;
                CrossSettings.Current.AddOrUpdateValue("user", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.UserId);
                CrossSettings.Current.AddOrUpdateValue("token", AzureManager.AzureManagerInstance.AzureClient.CurrentUser.MobileServiceAuthenticationToken);
            }
        }
    }
}
