using System;

namespace Memo.Core
{
    /// <summary>
    /// The application state, as a ViewModel
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Chat;

        public bool SideMenuVisible { get; set; } = true;

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;

            SideMenuVisible = page == ApplicationPage.Chat;
        }
    }
}
