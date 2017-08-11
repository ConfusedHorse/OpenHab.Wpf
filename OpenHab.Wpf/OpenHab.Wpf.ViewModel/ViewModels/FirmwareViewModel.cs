using GalaSoft.MvvmLight;
using OpenHAB.NetRestApi.Models;

namespace OpenHab.Wpf.ViewModel.ViewModels
{
    public class FirmwareViewModel : ViewModelBase
    {
        #region Fields

        private string _status;
        private string _updatableVersion;

        private Firmware _firmware;

        #endregion

        public FirmwareViewModel(Firmware firmware)
        {
            Status = firmware.Status;
            UpdatableVersion = firmware.UpdatableVersion;

            _firmware = firmware;
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

        public string UpdatableVersion
        {
            get => _updatableVersion;
            set
            {
                _updatableVersion = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion
    }
}