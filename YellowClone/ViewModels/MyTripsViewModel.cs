using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using YellowClone.Interfaces;
using YellowClone.Models;
using YellowClone.Views.Trips;

namespace YellowClone.ViewModels
{
    public class MyTripsViewModel : BaseViewModel, IAsyncInitialization
    {
        public Task Initialization { get; }

        private readonly IApi _api;
        private readonly INavigation _navigation;

        public ObservableCollection<Trip> Trips { get; private set; }

        public Command<Trip> GoToDetailsCommand { get; private set; }

        public MyTripsViewModel(INavigation navigation, IApi api)
        {
            _navigation = navigation;
            _api = api;

            Trips = new ObservableCollection<Trip>();

            GoToDetailsCommand = new Command<Trip>(
                async (trip) => await GoToDetailsAsync(trip)
            );

            Initialization = InitializationAsync();
        }

        private async Task InitializationAsync()
        {
            await LoadTripsAsync();
        }

        private async Task LoadTripsAsync()
        {
            try
            {
                var trips = await _api.GetTrips();

                foreach (var trip in trips)
                    Trips.Add(trip);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
        }

        private async Task GoToDetailsAsync(Trip trip)
        {
            await _navigation.PushAsync(new TripDetailView(trip));
        }
    }
}
