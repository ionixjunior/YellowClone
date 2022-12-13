using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.Enums;
using YellowClone.Interfaces;
using YellowClone.Models;
using YellowClone.Views.Wallet;
using YellowClone.Views.Trips;
using YellowClone.Views.Terms;
using YellowClone.Views.HelpCenter;
using YellowClone.Views.Promotion;

namespace YellowClone.ViewModels
{
    public class MenuViewModel : BaseViewModel, IAsyncInitialization
    {
        public Task Initialization { get; }

        private readonly INavigation _navigation;
        private readonly IApi _api;

        private Account _account;
        public Account Account
        {
            get => _account;
            set => RaiseIfPropertyChanged(ref _account, value);
        }

        private IList<MainMenu> _menus;
        public IList<MainMenu> Menus
        {
            get => _menus;
            set => RaiseIfPropertyChanged(ref _menus, value);
        }

        private Wallet _wallet;
        public Wallet Wallet
        {
            get => _wallet;
            set => RaiseIfPropertyChanged(ref _wallet, value);
        }

        public Command<MainMenuType> NavigateToCommand { get; private set; }

        public MenuViewModel(INavigation navigation, IApi api)
        {
            _navigation = navigation;
            _api = api;

            NavigateToCommand = new Command<MainMenuType>(async (type) => await NavigateToAsync(type));
            Initialization = InitializationAsync();
        }

        private async Task InitializationAsync()
        {
            await LoadAccountAsync();
            await LoadMenusAsync();
            await LoadWalletAsync();
        }

        private async Task LoadAccountAsync()
        {
            try
            {
                Account = await _api.GetAccount();
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task LoadMenusAsync()
        {
            try
            {
                var menus = await _api.GetMenus();
                Menus = new List<MainMenu>(menus);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task LoadWalletAsync()
        {
            try
            {
                Wallet = await _api.GetWallet();
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task NavigateToAsync(MainMenuType type)
        {
            System.Diagnostics.Debug.WriteLine(type);

            var mainPage = Application.Current.MainPage as NavigationPage;
            var masterDetail = mainPage.Navigation.NavigationStack.FirstOrDefault() as FlyoutPage;
            masterDetail.IsPresented = false;

            switch (type)
            {
                case MainMenuType.MyWallet:
                    await _navigation.PushAsync(new MyWalletView());
                    break;
                case MainMenuType.MyTrips:
                    await _navigation.PushAsync(new MyTripsView());
                    break;
                case MainMenuType.TermsConditions:
                    await _navigation.PushAsync(new TermsView());
                    break;
                case MainMenuType.HelpCenter:
                    await _navigation.PushAsync(new HelpCenterView());
                    break;
                case MainMenuType.Promotions:
                    await _navigation.PushAsync(new PromotionsView());
                    break;
            }
        }
    }
}
