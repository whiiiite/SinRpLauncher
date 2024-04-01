using Launcher.Classes;
using Launcher.Handlers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Launcher.Models
{
    public class ProfilesModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        static private List<string> LanguageText = LanguagesTexts.AllWords;
        private  string _UserProfilesText   = LanguageText[15];
        private  string _CancelBtnText      = LanguageText[11];
        private  string _DeleteBtnText      = LanguageText[12];
        private  string _AddBtnText         = LanguageText[13];
        private  string _PickBtnText        = LanguageText[14];

        public ProfilesModel() { ResetModel(); }

        public string UserProfilesText  { get { return _UserProfilesText;   }   set { _UserProfilesText = value; OnPropertyChanged();   } }
        public string CancelBtnText     { get { return _CancelBtnText;      }   set { _CancelBtnText = value; OnPropertyChanged();      } }
        public string DeleteBtnText     { get { return _DeleteBtnText;      }   set { _DeleteBtnText = value; OnPropertyChanged();      } }
        public string AddBtnText        { get { return _AddBtnText;         }   set { _AddBtnText = value; OnPropertyChanged();         } }
        public string PickBtnText       { get { return _PickBtnText;        }   set { _PickBtnText = value; OnPropertyChanged();        } }

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ResetModel()
        {
            LanguageText = LanguagesTexts.AllWords;

            UserProfilesText   = LanguageText[15];
            CancelBtnText      = LanguageText[11];
            DeleteBtnText      = LanguageText[12];
            AddBtnText         = LanguageText[13];
            PickBtnText        = LanguageText[14];
        }
    }
}
