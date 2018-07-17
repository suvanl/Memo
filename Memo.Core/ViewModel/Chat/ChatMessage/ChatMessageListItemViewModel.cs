using System;

namespace Memo.Core
{
    /// <summary>
    /// A view model for each chat message thread item in a chat thread
    /// </summary>
    public class ChatMessageListItemViewModel : BaseViewModel
    {
        /// <summary>
        /// The name of the message sender
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// The latest message in the current chat
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The initials on the default profile picture
        /// </summary>
        public string Initials { get; set; }

        /// <summary>
        /// The RGB values (in hex) for the background colour of the default profile picture
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if the item is currently selected
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// True if the message was sent by the signed-in user
        /// </summary>
        public bool SentByMe { get; set; }

        /// <summary>
        /// The time at which the message was read, or <see cref="DateTimeOffset.MinValue"/> if not read
        /// </summary>
        public DateTimeOffset MessageReadTime { get; set; }

        /// <summary>
        /// True if the message has been read
        /// </summary>
        public bool MessageRead => MessageReadTime > DateTimeOffset.MinValue;

        /// <summary>
        /// The time at which the message was sent
        /// </summary>
        public DateTimeOffset MessageSentTime { get; set; }
    }
}
