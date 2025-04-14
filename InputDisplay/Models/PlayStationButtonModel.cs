using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InputDisplay.Models;

public class PlayStationButtonModel : INotifyPropertyChanged {
    private Visibility _visibility;

    public Visibility ButtonVisibility {
        get => _visibility;
        set {
            if (_visibility != value) {
                _visibility = value;
                NotifyPropertyChanged();
            }
        }
    }

    public PlayStationButtonModel() {
        _visibility = Visibility.Hidden;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}