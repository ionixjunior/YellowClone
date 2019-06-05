namespace YellowClone.ViewModels
{
    public class TermsViewModel : BaseViewModel
    {
        private string _link;
        public string Link
        {
            get => _link;
            set => RaiseIfPropertyChanged(ref _link, value);
        }

        public TermsViewModel()
        {
            Link = "https://www.yellow.app/termos-de-uso/";
        }
    }
}
