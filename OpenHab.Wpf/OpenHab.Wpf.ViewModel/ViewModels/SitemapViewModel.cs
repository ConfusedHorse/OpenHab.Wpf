using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using OpenHab.Wpf.CrossCutting.Context;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class SitemapViewModel : ViewModelBase
    {
        private RestContext _restContext;

        #region Fields



        #endregion

        #region Properties

        public ObservableCollection<Sitemap> Sitemaps { get; set; }

        #endregion

        public SitemapViewModel(RestContext restContext)
        {
            _restContext = restContext;
        }

        #region Public Methods

        

        #endregion

        #region Private Methods

        

        #endregion
    }
}
