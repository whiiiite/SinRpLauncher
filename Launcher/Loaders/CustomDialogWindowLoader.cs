using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Launcher.Classes;
using Launcher.Handlers;
using Launcher.Interfaces;
using Microsoft.Win32.SafeHandles;

namespace Launcher.Loaders
{
    public class CustomDialogWindowLoader : IWindowLoader, IDisposable
    {
        // To detect redundant calls
        private bool _disposedValue;

        // Instantiate a SafeHandle instance.
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _safeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }


        DialogWindow cdw;

        public CustomDialogWindowLoader(CustomDialogWindow cdw)
        {
            this.cdw = cdw;
        }

        public void LoadWindow()
        {
            List<string> l = LanguagesTexts.AllWords;
            Utils.SetCursorsToControls(cdw);
            cdw.AgreeButton.Content = l[22];
            cdw.NoButton.Content = l[23];
            cdw.OkButton.Content = l[34];
            cdw.CancelButton.Content = l[11];
        }
    }
}
