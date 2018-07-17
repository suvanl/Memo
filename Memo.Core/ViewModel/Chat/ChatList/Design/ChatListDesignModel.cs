using System.Collections.Generic;

namespace Memo.Core
{
    /// <summary>
    /// Design-time data for a <see cref="ChatListViewModel"/>
    /// </summary>
    public class ChatListDesignModel : ChatListViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ChatListDesignModel Instance => new ChatListDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListDesignModel()
        {
            Items = new List<ChatListItemViewModel>
            {
                new ChatListItemViewModel
                {
                    Name = "Suvan",
                    Initials = "SL",
                    Message = "Hello, world! This new chat app, Memo, is pretty cool.",
                    ProfilePictureRGB = "3099c5",
                    NewContentAvailable = true
                },
                new ChatListItemViewModel
                {
                    Name = "Lorem",
                    Initials = "LI",
                    Message = "Ipsum dolor sit amet, consectetur adipiscing elit.",
                    ProfilePictureRGB = "ffa800"
                },
                new ChatListItemViewModel
                {
                    Name = "Joe",
                    Initials = "JB",
                    Message = "About to wipe the old server; we need to update it to Windows Server 2016",
                    ProfilePictureRGB = "00c541",
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Name = "Suvan",
                    Initials = "SL",
                    Message = "Hello, world! This new chat app, Memo, is pretty cool.",
                    ProfilePictureRGB = "3099c5"
                },
                new ChatListItemViewModel
                {
                    Name = "Lorem",
                    Initials = "LI",
                    Message = "Ipsum dolor sit amet, consectetur adipiscing elit.",
                    ProfilePictureRGB = "ffa800"
                },
                new ChatListItemViewModel
                {
                    Name = "Joe",
                    Initials = "JB",
                    Message = "Hey, I'm Joe Bloggs",
                    ProfilePictureRGB = "00c541"
                },
                new ChatListItemViewModel
                {
                    Name = "Suvan",
                    Initials = "SL",
                    Message = "Hello, world! This new chat app, Memo, is pretty cool.",
                    ProfilePictureRGB = "3099c5"
                },
                new ChatListItemViewModel
                {
                    Name = "Lorem",
                    Initials = "LI",
                    Message = "Ipsum dolor sit amet, consectetur adipiscing elit.",
                    ProfilePictureRGB = "ffa800"
                },
                new ChatListItemViewModel
                {
                    Name = "Joe",
                    Initials = "JB",
                    Message = "Hey, I'm Joe Bloggs",
                    ProfilePictureRGB = "00c541"
                }
            };
        }

        #endregion
    }
}
