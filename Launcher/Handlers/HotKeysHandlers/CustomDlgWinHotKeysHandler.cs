using Launcher;
using Launcher.BaseClasses;
using Launcher.Interfaces;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace SinRpLauncher.Handlers.HotKeysHandlers
{
    public class CustomDlgWinHotKeysHandler : BaseHandler, IHotKeysHandler, IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }

            // Call base class implementation.
            base.Dispose(disposing);
        }


        CustomDialogWindow apw;

        public CustomDlgWinHotKeysHandler(CustomDialogWindow apw)
        {
            this.apw = apw;
        }

        public void HandleHotKeys(KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                apw.Close();
            }
            if (e.Key == Key.Enter)
            {
                apw.DialogResult = true;
            }
        }
    }
}
