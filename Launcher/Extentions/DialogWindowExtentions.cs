using Launcher;
using CDialogWindow;
using Launcher.Handlers;
using System.Collections.Generic;
using Launcher.Classes;
using SinRpLauncher.Util;

namespace Launcher.Extentions
{
    public static class DialogWindowExtentions
    {
        public static void SetLanguage(this DialogWindow d, Languages lang)
        {
            List<string> words = LanguagesHandler.GetLanguageWords(lang);
            XamlUtil.SetCursorsToControls(d);
            
            d.AgreeButton.Content =     words[22];
            d.NoButton.Content  =       words[23];
            d.OkButton.Content =        words[34];
            d.CancelButton.Content =    words[11];
        }
    }
}
