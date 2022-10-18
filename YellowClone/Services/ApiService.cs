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
            HttpClient client = null;

#if DEBUG
            client = new HttpClient(GetInsecureHandler());
#else
            client = new HttpClient();
#endif

            client.BaseAddress = new Uri("https://yellow.getsandbox.com");
            _api = RestService.For<IApi>(client);
        }

        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                return true;
            };
            return handler;
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
