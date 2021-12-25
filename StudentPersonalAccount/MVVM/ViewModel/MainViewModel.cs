using Microsoft.Toolkit.Mvvm.ComponentModel;
using StudentPersonalAccount.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPersonalAccount.MVVM
{
    internal class MainViewModel : ObservableObject
    {
        public ProfileViewModel ProfileVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            ProfileVM = new ProfileViewModel();
        }
    }
}
