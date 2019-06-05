using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using YellowClone.Models;

namespace YellowClone.Interfaces
{
    public interface IApi
    {
        [Get("/account")]
        Task<Account> GetAccount();

        [Get("/menu")]
        Task<IList<MainMenu>> GetMenus();

        [Get("/wallet")]
        Task<Wallet> GetWallet();

        [Get("/trips")]
        Task<IList<Trip>> GetTrips();
    }
}
