using FabFood.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FabFood
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate();
    }

    public class App : Application
    {
        public static IAuthenticate Authenticator { get; private set; }
        public static void Init(IAuthenticate authenticator)
        {
            Authenticator = authenticator;
        }

        public static MasterDetailPage MasterDetailPage;

        public App()
        {
            // The root page of your application
            var content = new MainPage();
            MasterDetailPage = new MasterDetailPage
            {
                Master = new MenuPage(),
                Detail = new NavigationPage(content)
            };
            MainPage = MasterDetailPage;

            //MainPage = new NavigationPage(content);

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        
    }
}
