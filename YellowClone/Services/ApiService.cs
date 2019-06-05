using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using YellowClone.Interfaces;
using YellowClone.Models;

namespace YellowClone.Services
{
    public class ApiService : IApi
    {
        private static ApiService _instance;
        public static ApiService Instance = _instance ?? (_instance = new ApiService());
        private readonly IApi _api;

        private ApiService()
        {
            _api = RestService.For<IApi>("https://yellow.getsandbox.com");
        }

        public async Task<Account> GetAccount()
        {
            return await _api.GetAccount();
        }

        public async Task<IList<MainMenu>> GetMenus()
        {
            return await _api.GetMenus();
        }

        public async Task<Wallet> GetWallet()
        {
            return await _api.GetWallet();
        }

        public async Task<IList<Trip>> GetTrips()
        {
            return await _api.GetTrips();
        }
    }
}
