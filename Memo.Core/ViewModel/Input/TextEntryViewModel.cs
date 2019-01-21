using System.Windows.Input;

namespace Memo.Core
{
    /// <summary>
    /// The view model for a text entry field to edit a string value
    /// </summary>
    public class TextEntryViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The label to identify what the value is for
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The current saved value
        /// </summary>
        public string OriginalText { get; set; }

        /// <summary>
        /// The current uncommitted edited text
        /// </summary>
        public string EditedText { get; set; }

        /// <summary>
        /// Indicates whether the current field is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Sets the control to edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Exits out of edit mode without saving
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Commits the edits, saves the value and returns to a non-editable state
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TextEntryViewModel()
        {
            // Create commands
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        #endregion

        #region Command Methods

        public void Edit()
        {
            EditedText = OriginalText;

            Editing = true;
        }

        public void Cancel()
        {
            Editing = false;
        }

        public void Save()
        {
            // TODO: implement method to save content
            OriginalText = EditedText;

            Editing = false;
        }

        #endregion
    }
}
