using System;
using YellowClone.Models;

namespace YellowClone.ViewModels
{
    public class TripDetailViewModel : BaseViewModel
    {
        private Trip _trip;
        public Trip Trip
        {
            get => _trip;
            set => RaiseIfPropertyChanged(ref _trip, value);
        }

        public TripDetailViewModel(Trip trip)
        {
            _trip = trip;
        }
    }
}
