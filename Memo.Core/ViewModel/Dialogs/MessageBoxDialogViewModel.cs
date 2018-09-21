namespace Memo.Core
{
    /// <summary>
    /// Details for a message box/dialog box
    /// </summary>
    public class MessageBoxDialogViewModel : BaseDialogViewModel
    {
        /// <summary>
        /// The message to display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The text to use for the "OK" button
        /// </summary>
        public string OkText { get; set; }
    }
}
