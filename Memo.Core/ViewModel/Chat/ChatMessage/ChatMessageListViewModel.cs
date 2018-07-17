using System.Collections.Generic;

namespace Memo.Core
{
    /// <summary>
    /// A view model for a chat message thread list
    /// </summary>
    public class ChatMessageListViewModel : BaseViewModel
    {
        public List<ChatMessageListItemViewModel> Items { get; set; }
    }
}
