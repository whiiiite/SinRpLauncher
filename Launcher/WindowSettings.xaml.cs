using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Launcher.Classes;
using Launcher.Handlers;
using Launcher.Handlers.HotKeysHandlers;
using SinRpLauncher.Settings;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for WindowSettings.xaml
    /// Use as Dialog
    /// </summary>
    public partial class WindowSettings : Window
    {
        private MainWindow _MainWindow;
        private Utils _Utils;
        private Handlers.ThemesHandler _ThemesHandler;
        private Handlers.SettingsWindowHandlers swh;

        public Handlers.ThemesHandler ThemesHandler { get { return _ThemesHandler; } }


        public WindowSettings()
        {
            InitializeComponent();
            _Utils = new Utils();
            _ThemesHandler = new Handlers.ThemesHandler();
            swh = new Handlers.SettingsWindowHandlers(this);
            _MainWindow = null;
        }


        /// <summary>
        /// Constructor for change mainwindow
        /// </summary>
        /// <param name="mw"></param>
        public WindowSettings(ref MainWindow mw)
        {
            InitializeComponent();
            _Utils = new Utils();
            _ThemesHandler = new Handlers.ThemesHandler();
            swh = new Handlers.SettingsWindowHandlers(this);
            _MainWindow = mw;   
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Loaders.SettingsWindowLoader loader = new Loaders.SettingsWindowLoader(this);
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                loader.LoadWindow();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.CloseButtonHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void ApplySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.ApplySettingsButtonHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void CancelSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.CancelSettingsButtonHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.Button_MouseEnterHnd(sender, _ThemesHandler);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.Button_MouseLeaveHnd(sender, _ThemesHandler);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void FileSourceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.FileSourceButton_ClickHnd(this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            SettingsWindowHotKeysHandler hk = new SettingsWindowHotKeysHandler(this);
            string eventMsg = e.Key + " Hotkey used" + " | " + this.Name;
            LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, eventMsg); // write log

            hk.HandleHotKeys(e);
        }


        private void ChangeColorControlMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                swh.ChangeColorControlMenuItem_ClickHnd(sender, this);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }

        private void LanguagesComboxBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender == null) return;
                ComboBox langsComboBox = (ComboBox)sender;
                if (langsComboBox.SelectedItem == null) return;
                swh.LanguagesComboxBox_SelectionChangedHnd(langsComboBox);
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
            }
            catch (System.NullReferenceException ex)
            {
                //LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex, DateTime.Now);
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void SetDefaultThemesButton_Click(object sender, RoutedEventArgs e)
        {
            swh.SetDefaultThemesButton_ClickHnd(_MainWindow);
        }
    }
}
