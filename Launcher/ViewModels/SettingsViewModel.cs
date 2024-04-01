using Launcher.Models;

namespace Launcher.ViewModels
{
    public class SettingsViewModel
    {
        SettingsModel _model = new SettingsModel();
        public SettingsViewModel() { }
        public SettingsViewModel(SettingsModel _m) { _model = _m; }

        public string FolderIcon        { get { return _model.FolderIcon; } }
        public string GamePath          { get { return _model.GamePath; } }
        public string SubmitBtnText     { get { return _model.SubmitBtnText; } }
        public string CancelBtnText     { get { return _model.CancelBtnText; } }

        public string MainBGPath { get { return _model.MainBGImg; } }

        public string[] LangList        { get { return _model.LangList; } }
    }
}
