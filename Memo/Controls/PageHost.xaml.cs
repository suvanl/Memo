using Memo.Core;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Memo
{
    /// <summary>
    /// Interaction logic for PageHost.xaml
    /// </summary>
    public partial class PageHost : UserControl
    {
        #region Dependency Property

        /// <summary>
        /// The page to show in the PageHost
        /// </summary>
        public BasePage CurrentPage
        {
            get => (BasePage)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        // Using a DependencyProperty as the backing store for CurrentPage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(nameof(CurrentPage), typeof(BasePage), typeof(PageHost), new UIPropertyMetadata(CurrentPagePropertyChanged));

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PageHost()
        {
            InitializeComponent();

            // If in design mode, show the current page as the dependency property does not fire
            if (DesignerProperties.GetIsInDesignMode(this))
                NewPage.Content = (BasePage)new ApplicationPageValueConverter().Convert(IoC.Application.CurrentPage);
        }

        #endregion

        #region Property Changed Events

        /// <summary>
        /// Called when the <see cref="CurrentPage"/> value has changed
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void CurrentPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Gets frames
            var newPageFrame = (d as PageHost).NewPage;
            var oldPageFrame = (d as PageHost).OldPage;

            // Stores the current page's content as the old page
            var oldPageContent = newPageFrame.Content;

            // Removes current page from new page frame
            newPageFrame.Content = null;

            // Moves previous page into old page frame
            oldPageFrame.Content = oldPageContent;

            // Animates previous page out when the Loaded event fires
            if (oldPageContent is BasePage oldPage)
            {
                // Animates out old page
                oldPage.ShouldAnimateOut = true;

                Task.Delay((int)(oldPage.SlideSeconds * 1000)).ContinueWith((t) =>
                {
                    // Removes the old page when back on the UI thread
                    Application.Current.Dispatcher.Invoke(() => oldPageFrame.Content = null);
                });
            }

            // Sets new page content
            newPageFrame.Content = e.NewValue;
        }

        #endregion
    }
}
