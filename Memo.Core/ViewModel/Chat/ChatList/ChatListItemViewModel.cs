namespace Memo.Core
{
    /// <summary>
    /// A view model for each ChatListItem in the overview chat list
    /// </summary>
    public class ChatListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public string Initials { get; set; }

        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if new unread messages exist in chat
        /// </summary>
        public bool NewContentAvailable { get; set; }

        public bool IsSelected { get; set; }
    }
}
