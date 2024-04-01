using System;
using System.Collections.Generic;
using System.Windows.Input;
using Launcher.BaseClasses;
using Launcher.Classes;
using System.Windows.Shapes;
using SinRpLauncher.Classes;
using CDialogWindow;
using Launcher.Extentions;
using SinRpLauncher.Debug;
using SinRpLauncher.Util;
using SinRpLauncher.Handlers.HotKeysHandlers;
using Path = System.IO.Path;

namespace Launcher.Handlers.HotKeysHandlers
{
    public class MainWindowHotKeysHandler : BaseHandler, IHotKeysHandler
    {

        MainWindow mw;
        MainWindowHandlers mwHandlers;

        public MainWindowHotKeysHandler(MainWindow mw, MainWindowHandlers mwh)
        {
            this.mw = mw;
            mwHandlers = mwh;
        }


        public void HandleHotKeys(KeyEventArgs e)
        {
            // handle main hotkeys
            string hotKeysDataPath = Path.Combine(InfoClass.CurrentDir, PathRoots.DataDirectory, PathRoots.HotKeysFile);
            Dictionary<string, List<string>> hkDataDes = Utils.GetAllItemsFromJson<string, List<string>>(hotKeysDataPath);
            if(hkDataDes == null) throw new NullReferenceException("Hot keys got null");

            HotKeyEventArgs hkEventArgs;    
            HotKeyEvent hkEvent;
            foreach (KeyValuePair<string, List<string>> hkpair in hkDataDes)
            {
                hkEventArgs = new HotKeyEventArgs(hkpair.Value);
                if (!string.IsNullOrWhiteSpace(hkEventArgs.EventStr))
                {
                    hkEvent = new HotKeyEvent(hkEventArgs);

                    if (hkEvent.EventIsHappend())
                        HandleOpcodes(hkEventArgs.Opcode);
                }
                else
                {
                    throw new NullReferenceException("Colums of hot keys must be not empty.");
                }
            }



            // debug hkeys
            if ( (e.Key == Key.D && Keyboard.IsKeyDown(Key.LeftCtrl)) && InfoClass.IsDebugMode)
            {
                DebugTools.SwitchBuildText(mw.BuildName);
            }

            if ( (e.Key == Key.D && Keyboard.IsKeyDown(Key.LeftShift)) && InfoClass.IsDebugMode)
            {
                bool isAdmMode = DebugTools.SwitchAdminMode();
                DialogWindow msg = new DialogWindow();
                if (isAdmMode)
                    msg.ShowCustomDialogWindow("Админ режим активирован. Проверки отключены!");
                else
                    msg.ShowCustomDialogWindow("Админ режим деактивирован. Проверки включены!");
            }

            if ((e.Key == Key.D && Keyboard.IsKeyDown(Key.RightShift)) && InfoClass.IsDebugMode)
            {
                DebugTools.OffDebugMode(Opcodes.off_debug_mode);
                DialogWindow msg = new DialogWindow();
                msg.ShowCustomDialogWindow("Дебаг режим отключен.\nДля повторного включения перезагрузите лаунчер");
            }

            if(e.Key == Key.Enter && InfoClass.IsDebugMode && (mw.NickNameBox.Text == Opcodes.DevCmdCallStr))
            {
                DeveloperConsole developerConsole = new DeveloperConsole(mw);
                developerConsole.Show();
            }

            if (e.Key == Key.Enter && InfoClass.IsDebugMode)
            {
                UserDataUtil.NickNameIsValid(mw.NickNameBox.Text);
            }


            // easter
            StringHelper.PushBackCharArray(InfoClass.CodeStr, e.Key.ToString()[0]);
            CheckAndHandleEasterHackCode(InfoClass.CodeStr);
        }


        /// <summary>
        /// Handle operations by codes
        /// </summary>
        /// <param name="opcode"></param>
        /// <param name="mw"></param>
        public async void HandleOpcodes(int opcode)
        {
            int o = opcode; // rename
            if (o == Opcodes.RefreshLauncher)
                await mwHandlers.RefreshCheckConnetionBorderButtonHnd(mw);
            else if (o == Opcodes.ToGame)
                mwHandlers.ToGameButtonClickHnd(mw);
            else if (o == Opcodes.CloseLauncher)
                mwHandlers.CloseButtonHnd();
            else if (o == Opcodes.MinimizeLauncher)
                mwHandlers.MinimizedButtonHnd(mw);
            else if (o == Opcodes.ToSettingsLauncher)
                mwHandlers.SettingsButtonHnd(mw);
            else if (o == Opcodes.ToProfiles)
                mwHandlers.UserProfilesButtonHnd(mw);
            else if (o == Opcodes.ToPresonalCabinetURL)
                Utils.RedirectToSocialMedia(mw.NavBarCabinetButton.Name);
            else if (o == Opcodes.ToForumURL)
                Utils.RedirectToSocialMedia(mw.NavBarForumButton.Name);
            else if (o == Opcodes.ToTechSupportURL)
                Utils.RedirectToSocialMedia(mw.NavBarTechSupportButton.Name);
            else if (o == Opcodes.ToDiscordURL)
                Utils.RedirectToSocialMedia(mw.DiscordButton.Name);
            else if (o == Opcodes.ToYouTubeURL)
                Utils.RedirectToSocialMedia(mw.YoutubeButton.Name);
            else if (o == Opcodes.ToVkURL)
                Utils.RedirectToSocialMedia(mw.VkButton.Name);
            else if (o == Opcodes.ChangeNewsLeft)
                ChangeNewsLeft(mw.CurrentNewsEllipse.Name);
            else if (o == Opcodes.ChangeNewsRight)
                ChangeNewsRight(mw.CurrentNewsEllipse.Name);
            else if (o == Opcodes.FocusNickNameBox)
            {
                mw.NickNameBox.Focus();
                mw.NickNameBox.CaretIndex = mw.NickNameBox.Text.Length == 0 ? 0 : mw.NickNameBox.Text.Length;
            }
        }


        private void ChangeNewsRight(string CurrNewsEllipse)
        {
            switch (CurrNewsEllipse.ToLower())
            {
                case InfoClass.FIVTH_NEWS_IMAGE:
                    break;

                case InfoClass.FIRST_NEWS_IMAGE:
                    GetImage(mw.Scnd2);
                    break;

                case InfoClass.SECOND_NEWS_IMAGE:
                    GetImage(mw.Thrd3);
                    break;

                case InfoClass.THIRD_NEWS_IMAGE:
                    GetImage(mw.Frth4);
                    break;

                case InfoClass.FOUTH_NEWS_IMAGE:
                    GetImage(mw.Fvth5);
                    break;
            }
        }


        private void ChangeNewsLeft(string CurrNewsEllipse)
        {
            switch (CurrNewsEllipse.ToLower())
            {
                case InfoClass.FIRST_NEWS_IMAGE:
                    break;

                case InfoClass.FIVTH_NEWS_IMAGE:
                    GetImage(mw.Frth4);
                    break;

                case InfoClass.FOUTH_NEWS_IMAGE:
                    GetImage(mw.Thrd3);
                    break;

                case InfoClass.THIRD_NEWS_IMAGE:
                    GetImage(mw.Scnd2);
                    break;

                case InfoClass.SECOND_NEWS_IMAGE:
                    GetImage(mw.Frst1);
                    break;
            }
        }


        private void GetImage(Ellipse ellipse)
        {
            mw.CurrentNewsEllipse = Utils.PickNewsToggle(mw.CurrentNewsEllipse, ellipse);
            Utils.SetNewsImage(mw, ellipse.Name);
        }


        /// <summary>
        /// For easter about hacks code like in gta
        /// </summary>
        /// <param name="inputStr"></param>
        private void CheckAndHandleEasterHackCode(char[] inputStr)
        {
            string inputedString = new string(inputStr);
            inputedString = inputedString.Trim('\0');
            if (inputedString.Length > 0)
            {
                foreach (string code in InfoClass.EasterCodes)
                {
                    if (inputedString.EndsWith(code))
                    {
                        DialogWindow dialogWindow = new DialogWindow();
                        dialogWindow.SetLanguage(InfoClass.InterfaceLang);
                        dialogWindow.ShowCustomDialogWindow("Code activated :)", "", DialogWindowButtons.Ok);
                    }
                }
            }
        }
    }
}
