using PropertyChanged;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Memo.Core
{
    /// <summary>
    /// A base view model that fires Property Changed events as required
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired whenever any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Fires a <see cref="PropertyChanged"/> event when called
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #region Command Helpers

        /// <summary>
        /// Runs a command if the updatingFlag is not set.
        /// If the flag is true (indicating the function is already running), the action is not run.
        /// If the flag is false (indicating no running function), the action is run.
        /// Once the action has finished (if it ran), the flag is reset to false.
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag which states whether the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
                return;

            updatingFlag.SetPropertyValue(true);

            try
            {
               await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }

        }

        #endregion
    }
}
