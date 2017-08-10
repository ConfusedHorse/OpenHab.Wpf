using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHab.Wpf.CrossCutting.Events;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class SitemapViewModel : ViewModelBase
    {

        #region Fields

        private readonly RestContext _restContext;
        private bool _isEnabled;
        private bool _isBusy;
        private Sitemap _sitemap;

        #endregion

        public SitemapViewModel(RestContext restContext)
        {
            _restContext = restContext;
            _restContext.ConnectionChanged += RestContextOnConnectionChanged;
        }

        #region Properties

        public Sitemap Sitemap
        {
            get => _sitemap;
            set
            {
                _sitemap = value;
                RaisePropertyChanged();
            }
        }

        public Homepage Homepage => Sitemap?.Homepage;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        private void RestContextOnConnectionChanged(RestContext sender, ServerConnectionChangedEventArgs args)
        {
            IsEnabled = args.ConnectionEstablished;
            if (IsEnabled) LoadSitemapsAsync();
        }

        private async void LoadSitemapsAsync()
        {
            if (IsBusy) return;
            IsBusy = true;

            Sitemap = await Task.Run(() => _restContext.Client.SitemapService.GetDefaultSitemap());
            //TODO implement extension .ToViewModel(this Sitemap sitemap);

            IsBusy = false;
        }

        #endregion
    }
}
