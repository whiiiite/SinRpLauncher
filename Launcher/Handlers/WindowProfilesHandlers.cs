using System;
using System.Collections.Generic;
using Launcher.BaseClasses;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using CDialogWindow;
using Launcher.Extentions;
using SinRpLauncher.Util;

namespace Launcher.Handlers
{
    public class WindowProfilesHandlers : BaseHandler, IDisposable
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


        WindowProfiles wp;

        public WindowProfilesHandlers(WindowProfiles wp)
        {
            this.wp = wp;
        }

        public void AddProfileButtonClickHnd(WindowProfiles wp)
        {
            bool resultAddProfile = wp.AddProfile();
            DialogWindow customDialogWindow = new DialogWindow();
            string mainText;
            string titleText;
            if (resultAddProfile)
            {
                mainText = LanguagesTexts.DescriptionAccountAddedSuccess;
                titleText = LanguagesTexts.AllWords[36];
            }
            else
            {
                mainText = LanguagesTexts.DescriptionAccountWasNotAdded;
                titleText = LanguagesTexts.ErrorWord;
            }
            customDialogWindow.SetLanguage(InfoClass.InterfaceLang);
            customDialogWindow.ShowCustomDialogWindow(mainText, titleText, DialogWindowButtons.Ok);
        }


        public void PickProfileButtonClickHnd(WindowProfiles wp)
        {
            if (wp.PickedProfileContainer != null)
            {
                wp.PickProfile();
            }
            wp.DialogResult = true;
        }


        public void ProfileContainerMouseDwnHnd(WindowProfiles wp, object sender)
        {
            ProfileContainer profCont = (ProfileContainer)sender;

            if (wp.PickedProfileContainer != null && profCont != null)
            {
                wp.SetSelectedProfileColor(Brushes.Transparent, wp.PickedProfileContainer);
            }

            if (profCont != null)
            {
                wp.PickedProfileContainer = profCont;
                wp.SetSelectedProfileColor(Brushes.Gray, wp.PickedProfileContainer);
                wp.PickProfileButton.IsEnabled = true;
                wp.DeleteProfileButton.IsEnabled = true;
            }
        }


        /// <summary>
        /// Clear grid and dictionary with profiles. And display new
        /// </summary>
        /// <param name="pathToJson"></param>
        /// <param name="profsContainers"></param>
        /// <param name="ProfContainGrid"></param>
        public void UpdateProfilesData(string pathToJson, Dictionary<string, ProfileContainer> profsContainers, Grid ProfContainGrid)
        {
            foreach (var pr in profsContainers)
                ProfContainGrid.Children.Remove(pr.Value);
            profsContainers.Clear();
            wp.SetProfilesList(pathToJson);
        }


        public void DeleteProfileButtonClickHnd(WindowProfiles wp)
        {
            wp.DeleteProfile();
            DialogWindow msgBox = new DialogWindow();
            msgBox.SetLanguage(InfoClass.InterfaceLang);  
            string message = "Профиль успешно удален";
            msgBox.ShowCustomDialogWindow(message, "Удаление аккаунта", DialogWindowButtons.Ok);
        }


        public void ButtonEnterHnd(object sender, ThemesHandler _th)
        {
            _th.ButtonMouseEnter((Button)sender);
        }


        public void ButtonLeaveHnd(object sender, ThemesHandler _th)
        {
            _th.ButtonMouseLeave((Button)sender);
        }


        public void PickColorControlHnd(object sender, WindowProfiles wp)
        {
            if (sender is not MenuItem)
                return;

            string cPath = Directory.GetCurrentDirectory() + '\\' +
                           PathRoots.DataDirectory + '\\' + PathRoots.ProfilesWndColorThemeDatFile;

            // we need got menuitem
            MenuItem menuItem = (MenuItem)sender;
            using (ThemesHandler tHnd = new ThemesHandler())
            {
                tHnd.PickControlColors(menuItem); // pick color for control
                tHnd.SetNewProfileWindowColorsThemeToXML(cPath, wp);
            }
        }
    }
}
