namespace Memo.Core
{
    /// <summary>
    /// Design-time data for a <see cref="ChatListItemViewModel"/>
    /// </summary>
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static ChatListItemDesignModel Instance => new ChatListItemDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ChatListItemDesignModel()
        {
            Initials = "SL";
            Name = "Suvan";
            Message = "Hello, world! This new chat app, Memo, is pretty cool.";
            ProfilePictureRGB = "3099c5";
        }

        #endregion

    }
}
