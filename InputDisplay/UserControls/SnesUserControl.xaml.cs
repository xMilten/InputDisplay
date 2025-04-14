using InputDisplay.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InputDisplay.UserControls;
/// <summary>
/// Interaction logic for SnesUserControl.xaml
/// </summary>
public partial class SnesUserControl : UserControl {
    public GamepadViewModel _gamepad;

    public SnesUserControl() {
        InitializeComponent();

        _gamepad = new GamepadViewModel();

        DataContext = _gamepad;
    }
}
