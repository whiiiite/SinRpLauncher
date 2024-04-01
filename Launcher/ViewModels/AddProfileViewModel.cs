namespace Launcher.ViewModels
{
    internal class AddProfileViewModel
    {
        Models.AddProfileModel _model = new Models.AddProfileModel();

        public string[] SourceServers => _model.SourceServers();

        public string AddButtonProfileText => _model.AddProfileButtonText;
        public string ProfNameFieldText => _model.ProfNameFieldText;
        public string ServerFieldText => _model.ServerFieldText;
        public string NickNameFieldText => _model.NickNameFieldText;
    }
}
