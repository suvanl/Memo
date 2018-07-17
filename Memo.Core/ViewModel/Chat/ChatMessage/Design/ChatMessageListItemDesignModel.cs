using System;

namespace Memo.Core
{
    /// <summary>
    /// Design-time data for a <see cref="ChatMessageListItemViewModel"/>
    /// </summary>
    public class ChatMessageListItemDesignModel : ChatMessageListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ChatMessageListItemDesignModel Instance => new ChatMessageListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatMessageListItemDesignModel()
        {
            Initials = "SL";
            SenderName = "Suvan";
            Message = "Here's some design-time visual text";
            ProfilePictureRGB = "3099c5";
            SentByMe = true;
            MessageSentTime = DateTimeOffset.UtcNow;
            MessageReadTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1.3));
        }

        #endregion
    }
}
