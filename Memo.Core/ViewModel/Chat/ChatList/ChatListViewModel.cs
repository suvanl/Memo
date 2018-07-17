using System.Collections.Generic;

namespace Memo.Core
{
    /// <summary>
    /// A view model for the overview chat list
    /// </summary>
    public class ChatListViewModel : BaseViewModel
    {
        public List<ChatListItemViewModel> Items { get; set; }
    }
}
