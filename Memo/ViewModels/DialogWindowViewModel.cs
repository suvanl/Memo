using System.Windows;
using System.Windows.Controls;

namespace Memo
{
    /// <summary>
    /// The ViewModel for the custom material/flat design window
    /// </summary>
    public class DialogWindowViewModel : WindowViewModel
    {
        #region Public Properties

        /// <summary>
        /// The title of the dialog window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The content to be hosted within the dialog
        /// </summary>
        public Control Content { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogWindowViewModel(Window window) : base(window)
        {
            WindowMinimumWidth = 250;
            WindowMinimumHeight = 100;
            TitleHeight = 30;
        }

        #endregion
    }
}
