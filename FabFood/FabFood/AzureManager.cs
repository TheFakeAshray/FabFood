using FabFood.Models;
using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabFood
{
    class AzureManager
    {
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Bookings> bookingsTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("https://fabfood.azurewebsites.net/");
            this.bookingsTable = this.client.GetTable<Bookings>();
        }
        public MobileServiceClient AzureClient
        {
            get { return client; }
        }
        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }
                return instance;
            }
        }
        public async Task<List<Bookings>> GetBookings()
        {
            return await this.bookingsTable.ToListAsync();
        }
        public async Task AddBookings(Bookings booking)
        {
            await this.bookingsTable.InsertAsync(booking);
        }
    }
}
