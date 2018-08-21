using System.Collections.Generic;
using System.Windows.Input;

namespace Memo.Core
{
    /// <summary>
    /// A view model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The chat thread items for the list
        /// </summary>
        public List<ChatMessageListItemViewModel> Items { get; set; }

        /// <summary>
        /// Shows the attachment menu if true (and hides if false)
        /// </summary>
        public bool AttachmentMenuVisible { get; set; }

        /// <summary>
        /// The view model for the attachment menu
        /// </summary>
        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command for when the Attach button is clicked
        /// </summary>
        public ICommand AttachmentButtonCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatMessageListViewModel()
        {
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Shows/hides the attachment popup menu when the attachment button is clicked
        /// </summary>
        public void AttachmentButton()
        {
            AttachmentMenuVisible ^= true;
        }

        #endregion
    }
}
