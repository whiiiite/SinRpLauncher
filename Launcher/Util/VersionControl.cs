using System;
using System.IO;
using System.Net;
using System.Windows;

namespace Launcher.Classes
{
    public enum VersionCode : Int32
    {
        Current = 0, IsNotCurrent = 1, Exception = 2, 
    }

    /// <summary>
    /// Class for version control of the launcher (not game)
    /// </summary>
    public static class VersionControl
    {
        /// <summary>
        /// Check Version in server. If versions isnt equals - return VersionCode.IsNotCurrent. Else - VersionCode.Current
        /// </summary>
        /// <returns>VersionCode type value that means is current version of launcher or not</returns>
        public static VersionCode IsCurrentVersion()
        {
            try
            {
                string ftpVerFileUrl = FtpHelper.F_URL + FtpHelper.F_Ln_Dt_Dr; // ftp version file url
                string ftpVerFileName = FtpHelper.F_Vr_Fl; // ftp version file name

                string? username = FtpHelper.F_L_NAME; 
                string? psw = FtpHelper.F_PW;

                NetworkCredential nwkCrd = new NetworkCredential(username, psw);

                string userVer = GetUserVersion().Trim();
                string ftpVer = GetCurrentVersionFromFTP(ftpVerFileUrl, ftpVerFileName, nwkCrd).Trim();


                if (userVer == ftpVer) // if version is current - return true
                {
                    return VersionCode.Current; // true
                }
                return VersionCode.IsNotCurrent; // false by not equals versions
            }
            catch(Exception)
            {
                //MessageBox.Show(e.ToString());
                return VersionCode.Exception; // false by exception
            }
        }


        /// <summary>
        /// Get version of launcher
        /// </summary>
        /// <param name="jkey"></param>
        /// <returns>String with version of launcher</returns>
        private static string GetUserVersion()
        {
            return InfoClass.curVer;
        }


        /// <summary>
        /// Make request to FTP server and get version of launcher
        /// </summary>
        /// <returns>String with actuall version from FTP server</returns>
        public static string GetCurrentVersionFromFTP(string urlFtp, string verFileName, NetworkCredential creds)
        {
            string url = Path.Combine(urlFtp, verFileName);
            using (Stream responseStream = FtpHelper.GetStreamFileFtp(url, creds))
            {
                using (StreamReader sr = new StreamReader(responseStream))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
