using System.Windows.Input;

namespace Memo.Core
{
    /// <summary>
    /// The settings menu state, as a ViewModel
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties

        public TextEntryViewModel Name { get; set; }

        public TextEntryViewModel Username { get; set; }

        public TextEntryViewModel Password { get; set; }

        public TextEntryViewModel Email { get; set; }

        #endregion

        #region Public Commands

        public ICommand OpenCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
        }

        #endregion

        public void Open()
        {
            IoC.Application.SettingsMenuVisible = true;
        }

        public void Close()
        {
            IoC.Application.SettingsMenuVisible = false;
        }
    }
}
