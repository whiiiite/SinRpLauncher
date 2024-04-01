using Launcher.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Launcher.ViewModels
{
    public class ProfilesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ProfilesModel _model = new ProfilesModel();
        public ProfilesViewModel() { }
        public ProfilesViewModel(ProfilesModel _m) { _model = _m; }

        public string UserProfilesText  { get { return _model.UserProfilesText; } }
        public string CancelBtnText     { get { return _model.CancelBtnText;    } }
        public string DeleteBtnText     { get { return _model.DeleteBtnText;    } }
        public string AddBtnText        { get { return _model.AddBtnText;       } }
        public string PickBtnText       { get { return _model.PickBtnText;      } }


        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
