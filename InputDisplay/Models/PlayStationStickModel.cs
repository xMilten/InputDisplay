using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InputDisplay.Models;

public class PlayStationStickModel : INotifyPropertyChanged {
    private int _posX;
    private int _posY;

    public int DefaultX { get; }
    public int DefaultY { get; }

    public int PosX {
        get => _posX;
        set {
            if (_posX != value) {
                _posX = value;
                NotifyPropertyChanged();
            }
        }
    }

    public int PosY {
        get => _posY;
        set {
            if (_posY != value) {
                _posY = value;
                NotifyPropertyChanged();
            }
        }
    }

    public PlayStationStickModel(int defaultX, int defaultY) {
        DefaultX = defaultX;
        DefaultY = defaultY;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}