using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace Memo
{
    public enum WindowDockPosition
    {
        Undocked,
        Left,
        Right
    }

    /// <summary>
    /// Fixes the issue with Windows of Style <see cref="WindowStyle.None"/> covering the taskbar
    /// </summary>
    public class WindowResizer
    {
        #region Private Members

        /// <summary>
        /// The window to handle the resizing for
        /// </summary>
        private Window mWindow;

        /// <summary>
        /// The last calculated available screen size
        /// </summary>
        private Rect mScreenSize = new Rect();

        /// <summary>
        /// How close to the edge of the screen the window must be, to be detected as being at the edge of the screen
        /// </summary>
        private int mEdgeTolerance = 2;

        /// <summary>
        /// The transform matrix - converts WPF sizes to screen pixels
        /// </summary>
        private Matrix mTransformToDevice;

        /// <summary>
        /// The last screen the window was on
        /// </summary>
        private IntPtr mLastScreen;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition mLastDock = WindowDockPosition.Undocked;

        #endregion

        #region DLL Imports

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        #endregion

        #region Public Events

        public event Action<WindowDockPosition> WindowDockChanged = (dock) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="window">The window to monitor and correctly maximize</param>
        /// <param name="adjustSize">The callback for the host to adjust the maximum available size, if required</param>
        public WindowResizer(Window window)
        {
            mWindow = window;

            // Create transform visual (for converting WPF size to pixel size)
            GetTransform();

            // Listens out for source initialised to setup
            mWindow.SourceInitialized += Window_SourceInitialized;

            // Monitors for edge docking
            mWindow.SizeChanged += Window_SizeChanged;
        }

        #endregion

        #region Initialize

        private void GetTransform()
        {
            var source = PresentationSource.FromVisual(mWindow);

            // Resets the transform to default
            mTransformToDevice = default(Matrix);

            if (source == null)
                return;

            // Gets the new transform object
            mTransformToDevice = source.CompositionTarget.TransformToDevice;
        }

        /// <summary>
        /// Initialize and hook into the windows message pump
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_SourceInitialized(object sender, System.EventArgs e)
        {
            // Gets the handle of this window
            var handle = (new WindowInteropHelper(mWindow)).Handle;
            var handleSource = HwndSource.FromHwnd(handle);

            // Ends if not found
            if (handleSource == null)
                return;

            // Hook into its Windows messages
            handleSource.AddHook(WindowProc);
        }

        #endregion

        #region Edge Docking

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Cannot find positioning until the window transform has been established
            if (mTransformToDevice == default(Matrix))
                return;

            // WPF size
            var size = e.NewSize;

            // Window rectangle
            var top = mWindow.Top;
            var left = mWindow.Left;
            var bottom = top + size.Height;
            var right = left + mWindow.Width;

            // Gets window position/size in device pixels
            var windowTopLeft = mTransformToDevice.Transform(new Point(left, top));
            var windowBottomRight = mTransformToDevice.Transform(new Point(left, top));

            // Checks for docked edges
            var edgedTop = windowTopLeft.Y <= (mScreenSize.Top + mEdgeTolerance);
            var edgedLeft = windowTopLeft.X <= (mScreenSize.Left + mEdgeTolerance);
            var edgedBottom = windowBottomRight.Y >= (mScreenSize.Bottom - mEdgeTolerance);
            var edgedRight = windowBottomRight.X >= (mScreenSize.Right - mEdgeTolerance);

            // Docked position
            var dock = WindowDockPosition.Undocked;

            // Left docking
            if (edgedTop && edgedBottom && edgedTop)
                dock = WindowDockPosition.Left;
            else if (edgedTop && edgedBottom && edgedRight)
                dock = WindowDockPosition.Right;
            else
                dock = WindowDockPosition.Undocked;

            // If dock changed
            if (dock != mLastDock)
                // Informs listeners
                WindowDockChanged(dock);

            // Saves last dock position
            mLastDock = dock;
        }

        #endregion

        #region WindowProc

        /// <summary>
        /// Listens out for all windows messages for this window
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                // Handle the GetMinMaxInfo of the Window
                case 0x0024: // WM_GETMINMAXINFO
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (IntPtr)0;
        }

        #endregion

        /// <summary>
        /// Gets the min/max window size for this window,
        /// correctly accounting for the taskbar size and position
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lParam"></param>
        private void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            // Gets the point position to determine what screen the application is on
            GetCursorPos(out POINT lMousePosition);

            // Gets the primary monitor at cursor pos (0, 0)
            var lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);

            // Tries to get the primary screen's information
            var lPrimaryScreenInfo = new MONITORINFO();

            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
                return;

            // Gets the current screen
            var lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            // Updates the transform if it has changed from the last one
            if (lCurrentScreen != mLastScreen || mTransformToDevice == default(Matrix))
                GetTransform();

            // Stores last known screen
            mLastScreen = lCurrentScreen;

            // Gets min/max structure to fill with info
            var lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Uses the rcWork variable if the current screen is the primary screen, else, uses rcMonitor values
            if (lPrimaryScreen.Equals(lCurrentScreen) == true)
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcWork.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcWork.Right - lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcWork.Bottom - lPrimaryScreenInfo.rcWork.Top;
            }
            else
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcMonitor.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcMonitor.Right - lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcMonitor.Bottom - lPrimaryScreenInfo.rcMonitor.Top;
            }

            // Sets min size
            var minSize = mTransformToDevice.Transform(new Point(mWindow.MinWidth, mWindow.MinHeight));

            lMmi.ptMinTrackSize.X = (int)minSize.X;
            lMmi.ptMinTrackSize.Y = (int)minSize.Y;

            // Stores new size
            mScreenSize = new Rect(lMmi.ptMaxPosition.X, lMmi.ptMaxPosition.Y, lMmi.ptMaxSize.X, lMmi.ptMaxSize.Y);

            // Allows the host to tweak as needed, as the max size has now been established
            Marshal.StructureToPtr(lMmi, lParam, true);
        }
    }

    #region DLL Helper Structures

    enum MonitorOptions : uint
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public Rectangle rcMonitor = new Rectangle();
        public Rectangle rcWork = new Rectangle();
        public int dwFlags = 0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rectangle
    {
        public int Left, Top, Right, Bottom;

        public Rectangle(int left, int top, int right, int bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        /// <summary>
        /// x coordinate of point
        /// </summary>
        public int X;
        /// <summary>
        /// y coordinate of point
        /// </summary>
        public int Y;

        /// <summary>
        /// Constructs a point from coordinates (x, y)
        /// </summary>
        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    #endregion
}