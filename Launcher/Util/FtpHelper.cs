using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ED;

namespace Launcher.Classes
{
    /// <summary>
    /// Class for help work with ftp protocol and define some resources.
    /// </summary>
    public class FtpHelper
    {
        #region dllimport c++
        [DllImport("f_info.dll")]
        private extern static IntPtr f_ip();        // ftp ip
        [DllImport("f_info.dll")]
        private extern static IntPtr ai_lg();       // login to ftp
        [DllImport("f_info.dll")]
        private extern static IntPtr ai_pw();       // psw to ftp
        [DllImport("f_info.dll")]
        private extern static IntPtr f_mn_fd();     // main folder of launcher data in ftp
        [DllImport("f_info.dll")]
        private extern static IntPtr f_l_dr_fd();   // folder for download launcher in ftp
        //[DllImport("f_info.dll")]
        //private extern static IntPtr f_l_g();       // folder with game data
        #endregion // dllimport c++


        // DO NOT change names of vars to obvious names like 'FTP'
        #region Constants
        /// <summary>
        /// Contains url to FTP server
        /// </summary>
        public static string F_URL { get { return EncDecInfo.Get_S(Utils.GetUncypher(f_ip())); } }

        /// <summary>
        /// Contains login for FTP server
        /// </summary>
        public static string? F_L_NAME { get { return EncDecInfo.Get_S(Utils.GetUncypher(ai_lg())); } }

        /// <summary>
        /// Contains password for FTP server
        /// </summary>
        public static string? F_PW { get { return EncDecInfo.Get_S(Utils.GetUncypher(ai_pw())); } }

        /// <summary>
        /// Return name of directory that contains all data about launcher. Also update files etc.
        /// </summary>
        public static string F_Ln_Dt_Dr { get { return EncDecInfo.Get_S(Utils.GetUncypher(f_mn_fd())); } }

        /// <summary>
        /// Return folder name in FTP server that contains files of launcher
        /// </summary>
        public static string F_Ln_Fs_Dr { get { return EncDecInfo.Get_S(Utils.GetUncypher(f_l_dr_fd())); } }

        ///// <summary>
        ///// Return folder name in FTP server. That contains game data
        ///// </summary>
        //public static string F_L_G { get { return EncDecInfo.Get_S(Utils.GetUncypher(f_l_g())); } }

        /// <summary>
        /// Return file name on FTP server. That contains string with version of Launcher
        /// </summary>
        public static string F_Vr_Fl { get { return "ver.txt"; } }

        /// <summary>
        /// Return file name on FTP server. That contains string with hash of game
        /// </summary>
        public static string HashOfGame { get { return "hash_game.txt"; } }
        #endregion // Constants


        #region Methods
        public static Stream GetStreamFileFtp(string urlFtp, NetworkCredential creds)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlFtp);
            request.Timeout = 2500;
            request.Method = WebRequestMethods.Ftp.DownloadFile; 
            request.Credentials = creds;
            request.KeepAlive = false;

            FtpWebResponse response = null;
            Task task = Task.Run(() => { try { response = (FtpWebResponse)request.GetResponse(); } catch { } });
            Utils.TaskTimeIsTimedOut(task, Convert.ToUInt32(request.Timeout));

            Stream responseStream = response.GetResponseStream(); // get stream of file (data)
            return responseStream;
        }
        #endregion // Methods
    }
}
