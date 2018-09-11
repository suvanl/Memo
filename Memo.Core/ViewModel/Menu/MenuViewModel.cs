using System.Collections.Generic;

namespace Memo.Core
{
    /// <summary>
    /// A view model for a menu
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// The items in the menu
        /// </summary>
        public List<MenuItemViewModel> Items { get; set; }
    }
}
