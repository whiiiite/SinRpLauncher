using Launcher.Classes;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Launcher.Handlers
{
    /// <summary>
    /// Class for contains some text in picked language interface.
    /// <para>Like descriptions, labels, etc.</para>
    /// </summary>
    public class LanguagesTexts : InfoClass // in info class protected fields
    {


        /// <summary>
        /// Contains all words and sequences
        /// </summary>
        public static List<string> AllWords
        {
            get { return InfoClass.LanguageText; }
        }


        /// <summary>
        /// Return Update word in language
        /// </summary>
        public static string UpdateWord
        {
            get { return InfoClass.LanguageText[0]; }
        }


        /// <summary>
        /// Return text when internet connection ok and all updates is installed
        /// </summary>
        public static string SubDescriprtionHasInternetAndAllUpdates
        {
            get { return InfoClass.LanguageText[1]; }
        }


        /// <summary>
        /// Return string when in-et connection ok but updates don't installed
        /// </summary>
        public static string SubDescriprtionHasInternetNotUpdated
        {
            get { return InfoClass.LanguageText[2]; }
        }


        /// <summary>
        /// Return string when user has not in-et connection
        /// </summary>
        public static string SubDescriprtionNotHasInternet
        {
            get { return InfoClass.LanguageText[3]; }
        }


        /// <summary>
        /// Return string of description when game isnt downloaded
        /// </summary>
        public static string SubDescriptionGameIsntDownloaded
        {
            get { return InfoClass.LanguageText[4]; }
        }


        /// <summary>
        /// Return profiles word
        /// </summary>
        public static string ProfilesWord
        {
            get { return InfoClass.LanguageText[5]; }
        }


        /// <summary>
        /// Return cabinet word
        /// </summary>
        public static string CabinetWord
        {
            get { return InfoClass.LanguageText[6]; }
        }


        /// <summary>
        /// Return forum word
        /// </summary>
        public static string ForumWord
        {
            get { return InfoClass.LanguageText[7]; }
        }


        /// <summary>
        /// Return Tech Support word
        /// </summary>
        public static string TechSupWord
        {
            get { return InfoClass.LanguageText[8]; }
        }


        /// <summary>
        /// Return string of description when got error while get data from server 
        /// </summary>
        public static string DescriptionErrorWhileGetData
        {
            get { return InfoClass.LanguageText[30]; }
        }


        public static string DescriptionMaybeNoInet
        {
            get { return InfoClass.LanguageText[31]; }
        }


        public static string DescriptionHaveGoodPlay
        {
            get { return InfoClass.LanguageText[40]; }
        }


        public static string DescriptionWarningUpdateFiles
        {
            get { return InfoClass.LanguageText[42]; }
        }


        public static string ErrorWord
        {
            get { return InfoClass.LanguageText[32]; }
        }


        public static string DescriptionWrongNickname
        {
            get { return InfoClass.LanguageText[33].Replace('|', '\n'); }
        }


        public static string DescriptionUpdateFilesLauncher
        {
            get { return InfoClass.LanguageText[35]; }
        }


        public static string DescriptionAccountAddedSuccess
        {
            get { return InfoClass.LanguageText[37]; }
        }


        public static string DescriptionAccountWasNotAdded
        {
            get { return InfoClass.LanguageText[38]; }
        }


        public static string DescriptionDownloadGame
        {
            get { return InfoClass.LanguageText[43]; }
        }


        public static string DescriptionDownloadInProcess
        {
            get { return InfoClass.LanguageText[44]; }
        }


        public static string DescriptionDownloadStopped
        {
            get { return InfoClass.LanguageText[45]; }
        }


        public static string DescriptionDownloadComplete
        {
            get { return InfoClass.LanguageText[46]; }
        }
    }
}
