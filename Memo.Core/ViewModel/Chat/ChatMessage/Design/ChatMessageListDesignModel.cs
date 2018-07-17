using System;
using System.Collections.Generic;

namespace Memo.Core
{
    /// <summary>
    /// Design-time data for a <see cref="ChatMessageListViewModel"/>
    /// </summary>
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ChatMessageListDesignModel Instance => new ChatMessageListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatMessageListDesignModel()
        {
            Items = new List<ChatMessageListItemViewModel>
            {
                new ChatMessageListItemViewModel
                {
                    SenderName = "Joe",
                    Initials = "JB",
                    Message = "About to wipe the old server; we need to update it to Windows Server 2016",
                    ProfilePictureRGB = "00c541",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    SentByMe = false
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Suvan",
                    Initials = "SL",
                    Message = "Ok, let me know when it's done",
                    ProfilePictureRGB = "00c541",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    SentByMe = true
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Joe",
                    Initials = "JB",
                    Message = "Done, the new server is up at 192.168.1.1.\r\nUsername and password are the same as before.",
                    ProfilePictureRGB = "00c541",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    SentByMe = false
                }
            };
        }

        #endregion
    }
}
