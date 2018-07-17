using Memo.Core;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace Memo
{
    /// <summary>
    /// The ViewModel for the custom material/flat design window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        private Window mWindow;

        private int mOuterMarginSize = 10;
        private int mWindowRadius = 10;

        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        public double WindowMinimumWidth { get; set; } = 800;
        public double WindowMinimumHeight { get; set; } = 500;

        public bool Borderless => (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked);

        public int ResizeBorder => Borderless ? 0 : 6;

        /// <summary>
        /// The size of the resize border around the window, accounting for the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        /// <summary>
        /// The padding of the inner content of the main window
        /// </summary>
        public Thickness InnerContentPaddingThickness { get; set; } = new Thickness(0);


        public int OuterMarginSize
        {
            get => Borderless ? 0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }

        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        public int WindowRadius
        {
            get => Borderless ? 0 : mWindowRadius;
            set => mWindowRadius = value;
        }

        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        public int TitleHeight { get; set; } = 42;

        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        #endregion

        #region Commands

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window"></param>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // Listens out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fires events for all properties that are affected by a resize taking place
                WindowResized();
            };

            // Create commands
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue
            var resizer = new WindowResizer(mWindow);

            // Listens out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Stores last position
                mDockPosition = dock;

                // Fires resize events
                WindowResized();
            };
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Mouse position, relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so it's a "ToScreen"
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        /// <summary>
        /// If the window is resized to a special position (docked or maximised)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fires events for all properties that are affected by a resize taking place
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        } 

        #endregion
    }
}
