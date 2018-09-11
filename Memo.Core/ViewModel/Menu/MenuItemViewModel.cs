namespace Memo.Core
{
    /// <summary>
    /// A view model for a menu item
    /// </summary>
    public class MenuItemViewModel : BaseViewModel
    {
        public string Text { get; set; }

        public IconType Icon { get; set; }

        public MenuItemType Type { get; set; }
    }
}
