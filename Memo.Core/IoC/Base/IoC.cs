using System;
using Ninject;

namespace Memo.Core
{
    /// <summary>
    /// The IoC (Inversion of Control) container for the application
    /// </summary>
    public static class IoC
    {
        #region Public Properties

        /// <summary>
        /// The kernel of the IoC container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// A shortcut to access the <see cref="IUIManager"/>
        /// </summary>
        public static IUIManager UI => IoC.Get<IUIManager>();

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => IoC.Get<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access the <see cref="SettingsViewModel"/>
        /// </summary>
        public static SettingsViewModel Settings => IoC.Get<SettingsViewModel>();

        #endregion

        #region Construction

        /// <summary>
        /// Sets up the IoC container, binds all information required and is ready for use.
        /// NOTE: Must be called as soon as application starts up, to ensure all services can be found.
        /// </summary>
        public static void Setup()
        {
            // Binds all required ViewModels
            BindViewModels();
        }

        /// <summary>
        /// Binds all singleton ViewModels
        /// </summary>
        private static void BindViewModels()
        {
            // Binds to a single instance of ApplicationViewModel
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());

            // Binds to a single instance of SettingsViewModel
            Kernel.Bind<SettingsViewModel>().ToConstant(new SettingsViewModel());
        }

        #endregion

        /// <summary>
        /// Gets a service of the specified type from the IoC
        /// </summary>
        /// <typeparam name="T">The type to get from the IoC</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
