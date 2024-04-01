using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Launcher.Classes
{

    /// <summary>
    /// Auto closing window after timeout (using user32.dll)
    /// </summary>
    public class AutoClosingMsgBox
    {
        /// <summary>
        /// Method for find window in thread(like a message box)
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Method for send some message to window
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);


        const int WM_CLOSE = 0x0010; // windows message for close window
        System.Threading.Timer _timeoutTimer; // timer thread
        string _caption; // var for caption

        AutoClosingMsgBox(string text, string caption, int timeout)
        {
            _caption = caption; // set caption
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, Timeout.Infinite); // start timer< when it end - work callback method
            MessageBox.Show(text, caption); // show message box - timer is still working
        }

        /// <summary>
        /// Shows message box and close it after timeout
        /// </summary>
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMsgBox(text, caption, timeout); // Create new instance for show msg box
        }

        /// <summary>
        /// Need for send callback to thread timer.
        /// Find window(for example message box) by caption, and send message to this window for close
        /// (0x0010)
        /// </summary>
        private void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow(null, _caption); // find message box(window) by caption
            if (mbWnd != IntPtr.Zero) // if exists
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero); // send close message to found window
            _timeoutTimer.Dispose();
        }
    }
}
