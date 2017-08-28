using GalaSoft.MvvmLight;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class StatusInfoViewModel : ViewModelBase
    {
        #region Fields

        private string _description;
        private string _status;
        private string _statusDetail;

        private StatusInfo _statusInfo;

        #endregion

        public StatusInfoViewModel(StatusInfo statusInfo)
        {
            Status = statusInfo.Status;
            StatusDetail = statusInfo.StatusDetail;
            Description = statusInfo.Description;

            _statusInfo = statusInfo;
        }

        #region Properties

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        public string StatusDetail
        {
            get => _statusDetail;
            set
            {
                _statusDetail = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        public static StatusInfoViewModel Default => new StatusInfoViewModel(StatusInfo.Default);

        #endregion

        #region Private Methods

        #endregion
    }
}