using InputDisplay.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InputDisplay.ViewModels;

public class GamepadViewModel : INotifyPropertyChanged {
    private string _buttons;

    public string Buttons {
        get => _buttons;
        set {
            if (_buttons != value) {
                _buttons = value;
                NotifyPropertyChanged();
            }
        }
    }

    public List<PlayStationButtonModel> PlayStationButtons { get; }
    public List<PlayStationStickModel> PlayStationSticks { get; }

    public GamepadViewModel() {
        PlayStationButtons = new List<PlayStationButtonModel>();

        for (int i = 0; i < 16; i++) {
            PlayStationButtons.Add(new PlayStationButtonModel());
        }

        PlayStationSticks = new List<PlayStationStickModel>() {
            new PlayStationStickModel(111, 139),
            new PlayStationStickModel(240, 139)
        };
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}