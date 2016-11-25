using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using static FabFood.App;

namespace FabFood.Droid
{
    [Activity(Label = "FabFood", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate
    {
        private MobileServiceUser user;

        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.FormsMaps.Init(this, bundle);
            CurrentPlatform.Init();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            // Initialize the authenticator before loading the app.
            App.Init(this);
            LoadApplication(new App());
        }
        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                user = await AzureManager.AzureManagerInstance.AzureClient.LoginAsync(this,
                    MobileServiceAuthenticationProvider.Facebook);
                if (user != null)
                {
                    message = string.Format("Hey, you're signed in! Now you can book a table ;)",
                        user.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
    }
}

