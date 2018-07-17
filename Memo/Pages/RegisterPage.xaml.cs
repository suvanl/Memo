using Memo.Core;
using System.Security;

namespace Memo
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : BasePage<RegisterViewModel>, IHavePassword
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The secure password for the login page
        /// </summary>
        public SecureString SecurePassword => PasswordText.SecurePassword;
    }
}
