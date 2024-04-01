using System;
using System.Windows;
using Launcher.Classes;
using Launcher.Handlers;
using Launcher.Loaders;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for LoadLauncherWindow.xaml
    /// </summary>
    // This window is a start point of all program
    public partial class LoadLauncherWindow : Window
    {
        private Utils _Utils;
        private LauncherFilesSystem _launcherFilesSystem;
        ThemesHandler _ThemeHandler;
        private string _LauncherFilesMessage;

        public LauncherFilesSystem Lfs => _launcherFilesSystem;
        public ThemesHandler ThemeHandler => _ThemeHandler;
        public string LauncherFilesMessage { get { return _LauncherFilesMessage; } set { _LauncherFilesMessage = value; } }

        //public MainWindow LoadMainWindowMeth() => LoadMainWindow();
        public void ChangeLoadTextBlockMeth(string value) => ChangeLoadTextBlock(value);

        public LoadLauncherWindow()
        {
            InitializeComponent();
            _Utils = new Utils();
            _launcherFilesSystem = new LauncherFilesSystem();
            _LauncherFilesMessage = string.Empty;
            _ThemeHandler = new ThemesHandler();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadLauncherWindowLoader loader = new LoadLauncherWindowLoader(this);
                LogsSys.WriteSimpleLog(_Utils.GetAllLogPath(), sender, LogsSys.ConstructMsg(sender, e.RoutedEvent)); // write log
                MainWindow mw = new MainWindow();
                await loader.LoadWindowAsync();
                CloseThis();
                mw.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), sender, ex);
            }
        }


        private void ChangeLoadTextBlock(string value)
        {
            LoadingTextBlock.Text = value;
        }


        private void CloseThis()
        {
            try
            {
                this.Close();
            }
            catch(Exception ex)
            { 
                Utils.ShowMessageBoxErrorIfNotDebug(ex);
                LogsSys.WriteErrorLog(_Utils.GetErrorLogPath(), "null", ex);
            }
        }
    }
}
