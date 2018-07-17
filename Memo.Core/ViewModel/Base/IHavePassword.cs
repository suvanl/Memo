using System.Security;

namespace Memo.Core
{
    /// <summary>
    /// An interface for a class that can provide
    /// </summary>
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }
    }
}
