using InputDisplay.Models;
using InputDisplay.UserControls;
using SharpDX;
using SharpDX.DirectInput;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace InputDisplay;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private PlayStationUserControl _playStationLayout;
    private SnesUserControl _snesLayout;
    private DirectInput _directInput;
    private Joystick _joystick;
    private JoystickState _state;
    private DispatcherTimer _dispatcherTimer;

    public MainWindow() {
        _directInput = new DirectInput();
        _playStationLayout = new PlayStationUserControl();
        //_snesLayout = new SnesUserControl();

        CaptureJoystick();
        Closing += MainWindow_Closing;

        InitializeComponent();

        Content = _playStationLayout;
        //Content = _snesLayout;

        _dispatcherTimer = new DispatcherTimer();
        _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1);
        _dispatcherTimer.Tick += UpdateJoystick;
        _dispatcherTimer.Start();
    }

    private void CaptureJoystick() {
        foreach (DeviceInstance deviceInstance in _directInput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices)) {
            _joystick = new Joystick(_directInput, deviceInstance.InstanceGuid);
            break;
        }

        if (_joystick != null) {
            _joystick.Acquire();
        }
        else {
            /*
            if (MessageBox.Show("Gamepad is not connected", "Gamepad missing", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel) {
                Application.Current.Shutdown();
            }
            */
            CaptureJoystick();
        }
    }

    private void UpdateJoystick(object sender, object e) {
        if (_joystick != null) {
            try {
                _state = _joystick.GetCurrentState();

                var button = _playStationLayout._gamepad;

                ChangeVisibility(_state.Buttons[0], button.PlayStationButtons[0]); // Square
                ChangeVisibility(_state.Buttons[1], button.PlayStationButtons[1]); // Cross
                ChangeVisibility(_state.Buttons[2], button.PlayStationButtons[2]); // Circle
                ChangeVisibility(_state.Buttons[3], button.PlayStationButtons[3]); // Triangle

                ChangeVisibility(_state.Buttons[4], button.PlayStationButtons[4]); // L1
                ChangeVisibility(_state.Buttons[5], button.PlayStationButtons[5]); // R1
                ChangeVisibility(_state.Buttons[6], button.PlayStationButtons[6]); // L2
                ChangeVisibility(_state.Buttons[7], button.PlayStationButtons[7]); // R2

                ChangeVisibility(_state.Buttons[8], button.PlayStationButtons[8]); // Share
                ChangeVisibility(_state.Buttons[9], button.PlayStationButtons[9]); // Options

                ChangeVisibility(_state.Buttons[10], button.PlayStationButtons[10]); // L-Trigger
                ChangeVisibility(_state.Buttons[11], button.PlayStationButtons[11]); // R-Trigger

                var dPad = _state.PointOfViewControllers[0];

                ChangeVisibility(dPad == 31500 || dPad == 0 || dPad == 4500, button.PlayStationButtons[12]); // D-Up
                ChangeVisibility(dPad == 4500 || dPad == 9000 || dPad == 13500, button.PlayStationButtons[13]); // D-Right
                ChangeVisibility(dPad == 13500 || dPad == 18000 || dPad == 22500, button.PlayStationButtons[14]); // D-Down
                ChangeVisibility(dPad == 22500 || dPad == 27000 || dPad == 31500, button.PlayStationButtons[15]); // D-Left

                button.PlayStationSticks[0].PosX = CalculateStick(_state.X, button.PlayStationSticks[0].DefaultX); // L-Stick X-Axis
                button.PlayStationSticks[0].PosY = CalculateStick(_state.Y, button.PlayStationSticks[0].DefaultY); // L-Stick Y-Axis
                button.PlayStationSticks[1].PosX = CalculateStick(_state.Z, button.PlayStationSticks[1].DefaultX); // R-Stick X-Axis
                button.PlayStationSticks[1].PosY = CalculateStick(_state.RotationZ, button.PlayStationSticks[1].DefaultY); // R-Stick Y-Axis

                button.Buttons = _state.ToString();
            }
            catch (SharpDXException ex) {
                //MessageBox.Show(sex.Message);
                _state = null;
                _joystick = null;
                CaptureJoystick();
            }
            catch (Exception ex) {
                //MessageBox.Show(ex.Message);
                _state = null;
                _joystick = null;
                CaptureJoystick();
            }
        }
        else {
            CaptureJoystick();
        }
    }

    private static void ChangeVisibility(bool flag, PlayStationButtonModel button) {
        if (button.ButtonVisibility == Visibility.Hidden && flag) {
            button.ButtonVisibility = Visibility.Visible;
        }
        else if (button.ButtonVisibility == Visibility.Visible && !flag) {
            button.ButtonVisibility = Visibility.Hidden;
        }
    }

    private static int CalculateStick(int axis, int pos) {
        return (int)Math.Round(((double)10 / 32767 * axis) + pos - 10);
    }

    private void MainWindow_Closing(object sender, CancelEventArgs e) {
        _state = null;
        _joystick = null;
    }
}