using CommunityToolkit.Mvvm.ComponentModel;
using Ink_Canvas.Configuration.Options; // Assuming this was the namespace for options
using Ink_Canvas.Services; // Assuming this was the namespace for ISettingsService

namespace Ink_Canvas.ViewModels { // Original namespace
    public partial class MainWindowViewModel : ObservableObject {
        private readonly ISettingsService _settingsService;

        public MainWindowViewModel(ISettingsService settingsService) {
            _settingsService = settingsService;
        }

        // Appearance Settings
        public bool IsEnableDisPlayNibModeToggler {
            get => _settingsService.AppearanceOptions.IsEnableDisPlayNibModeToggler;
            set {
                if (_settingsService.AppearanceOptions.IsEnableDisPlayNibModeToggler != value) {
                    _settingsService.AppearanceOptions.IsEnableDisPlayNibModeToggler = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public double ViewboxFloatingBarScaleTransformValue {
            get => _settingsService.AppearanceOptions.ViewboxFloatingBarScaleTransformValue;
            set {
                if (_settingsService.AppearanceOptions.ViewboxFloatingBarScaleTransformValue != value) {
                    _settingsService.AppearanceOptions.ViewboxFloatingBarScaleTransformValue = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int FloatingBarImg {
            get => _settingsService.AppearanceOptions.FloatingBarImg;
            set {
                if (_settingsService.AppearanceOptions.FloatingBarImg != value) {
                    _settingsService.AppearanceOptions.FloatingBarImg = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public double ViewboxFloatingBarOpacityValue {
            get => _settingsService.AppearanceOptions.ViewboxFloatingBarOpacityValue;
            set {
                if (_settingsService.AppearanceOptions.ViewboxFloatingBarOpacityValue != value) {
                    _settingsService.AppearanceOptions.ViewboxFloatingBarOpacityValue = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public double ViewboxFloatingBarOpacityInPPTValue {
            get => _settingsService.AppearanceOptions.ViewboxFloatingBarOpacityInPPTValue;
            set {
                if (_settingsService.AppearanceOptions.ViewboxFloatingBarOpacityInPPTValue != value) {
                    _settingsService.AppearanceOptions.ViewboxFloatingBarOpacityInPPTValue = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool EnableViewboxBlackBoardScaleTransform {
            get => _settingsService.AppearanceOptions.EnableViewboxBlackBoardScaleTransform;
            set {
                if (_settingsService.AppearanceOptions.EnableViewboxBlackBoardScaleTransform != value) {
                    _settingsService.AppearanceOptions.EnableViewboxBlackBoardScaleTransform = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int UnFoldButtonImageType {
            get => _settingsService.AppearanceOptions.UnFoldButtonImageType;
            set {
                if (_settingsService.AppearanceOptions.UnFoldButtonImageType != value) {
                    _settingsService.AppearanceOptions.UnFoldButtonImageType = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool IsColorfulViewboxFloatingBar {
            get => _settingsService.AppearanceOptions.IsColorfulViewboxFloatingBar;
            set {
                if (_settingsService.AppearanceOptions.IsColorfulViewboxFloatingBar != value) {
                    _settingsService.AppearanceOptions.IsColorfulViewboxFloatingBar = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool EnableTimeDisplayInWhiteboardMode {
            get => _settingsService.AppearanceOptions.EnableTimeDisplayInWhiteboardMode;
            set {
                if (_settingsService.AppearanceOptions.EnableTimeDisplayInWhiteboardMode != value) {
                    _settingsService.AppearanceOptions.EnableTimeDisplayInWhiteboardMode = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool EnableChickenSoupInWhiteboardMode {
            get => _settingsService.AppearanceOptions.EnableChickenSoupInWhiteboardMode;
            set {
                if (_settingsService.AppearanceOptions.EnableChickenSoupInWhiteboardMode != value) {
                    _settingsService.AppearanceOptions.EnableChickenSoupInWhiteboardMode = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int ChickenSoupSource {
            get => _settingsService.AppearanceOptions.ChickenSoupSource;
            set {
                if (_settingsService.AppearanceOptions.ChickenSoupSource != value) {
                    _settingsService.AppearanceOptions.ChickenSoupSource = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool IsShowQuickPanel {
            get => _settingsService.AppearanceOptions.IsShowQuickPanel;
            set {
                if (_settingsService.AppearanceOptions.IsShowQuickPanel != value) {
                    _settingsService.AppearanceOptions.IsShowQuickPanel = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool EnableTrayIcon {
            get => _settingsService.AppearanceOptions.EnableTrayIcon;
            set {
                if (_settingsService.AppearanceOptions.EnableTrayIcon != value) {
                    _settingsService.AppearanceOptions.EnableTrayIcon = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        // Startup Settings
        public bool IsAutoUpdate {
            get => _settingsService.StartupOptions.IsAutoUpdate;
            set {
                if (_settingsService.StartupOptions.IsAutoUpdate != value) {
                    _settingsService.StartupOptions.IsAutoUpdate = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsAutoUpdateWithSilenceVisible));
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool IsAutoUpdateWithSilence {
            get => _settingsService.StartupOptions.IsAutoUpdateWithSilence;
            set {
                if (_settingsService.StartupOptions.IsAutoUpdateWithSilence != value) {
                    _settingsService.StartupOptions.IsAutoUpdateWithSilence = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(AutoUpdateTimePeriodBlockVisible));
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool IsAutoUpdateWithSilenceVisible => IsAutoUpdate;
        public bool AutoUpdateTimePeriodBlockVisible => IsAutoUpdate && IsAutoUpdateWithSilence;


        public int AutoUpdateWithSilenceStartTime {
            get => TimeToIndexConverter(_settingsService.StartupOptions.AutoUpdateWithSilenceStartTime);
            set {
                string timeValue = IndexToTimeConverter(value);
                if (_settingsService.StartupOptions.AutoUpdateWithSilenceStartTime != timeValue) {
                    _settingsService.StartupOptions.AutoUpdateWithSilenceStartTime = timeValue;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int AutoUpdateWithSilenceEndTime {
            get => TimeToIndexConverter(_settingsService.StartupOptions.AutoUpdateWithSilenceEndTime);
            set {
                string timeValue = IndexToTimeConverter(value);
                if (_settingsService.StartupOptions.AutoUpdateWithSilenceEndTime != timeValue) {
                    _settingsService.StartupOptions.AutoUpdateWithSilenceEndTime = timeValue;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        private int TimeToIndexConverter(string time) {
            if (string.IsNullOrEmpty(time)) return 0;
            var parts = time.Split(':');
            if (parts.Length == 2 && int.TryParse(parts[0], out int hour) && int.TryParse(parts[1], out int minute)) {
                return hour * 2 + (minute == 30 ? 1 : 0);
            }
            return 0;
        }

        private string IndexToTimeConverter(int index) {
            int hour = index / 2;
            int minute = (index % 2) * 30;
            return $"{hour:D2}:{minute:D2}";
        }

        public bool RunAtStartup {
            get => _settingsService.StartupOptions.RunAtStartup;
            set {
                if (_settingsService.StartupOptions.RunAtStartup != value) {
                    _settingsService.StartupOptions.RunAtStartup = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool FoldAtStartup {
            get => _settingsService.StartupOptions.FoldAtStartup;
            set {
                if (_settingsService.StartupOptions.FoldAtStartup != value) {
                    _settingsService.StartupOptions.FoldAtStartup = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool IsEnableNibMode {
            get => _settingsService.StartupOptions.IsEnableNibMode;
            set {
                if (_settingsService.StartupOptions.IsEnableNibMode != value) {
                    _settingsService.StartupOptions.IsEnableNibMode = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        // Canvas Settings
        public bool IsShowCursor {
            get => _settingsService.CanvasOptions.IsShowCursor;
            set {
                if (_settingsService.CanvasOptions.IsShowCursor != value) {
                    _settingsService.CanvasOptions.IsShowCursor = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int EraserSize {
            get => _settingsService.CanvasOptions.EraserSize;
            set {
                if (_settingsService.CanvasOptions.EraserSize != value) {
                    _settingsService.CanvasOptions.EraserSize = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int InkStyle {
            get => _settingsService.CanvasOptions.InkStyle;
            set {
                if (_settingsService.CanvasOptions.InkStyle != value) {
                    _settingsService.CanvasOptions.InkStyle = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool HideStrokeWhenSelecting {
            get => _settingsService.CanvasOptions.HideStrokeWhenSelecting;
            set {
                if (_settingsService.CanvasOptions.HideStrokeWhenSelecting != value) {
                    _settingsService.CanvasOptions.HideStrokeWhenSelecting = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool ClearCanvasAndClearTimeMachine {
            get => _settingsService.CanvasOptions.ClearCanvasAndClearTimeMachine;
            set {
                if (_settingsService.CanvasOptions.ClearCanvasAndClearTimeMachine != value) {
                    _settingsService.CanvasOptions.ClearCanvasAndClearTimeMachine = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public int HyperbolaAsymptoteOption {
            get => (int)_settingsService.CanvasOptions.HyperbolaAsymptoteOption;
            set {
                if ((int)_settingsService.CanvasOptions.HyperbolaAsymptoteOption != value) {
                    _settingsService.CanvasOptions.HyperbolaAsymptoteOption = (OptionalOperation)value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        public bool FitToCurve {
            get => _settingsService.CanvasOptions.FitToCurve;
            set {
                if (_settingsService.CanvasOptions.FitToCurve != value) {
                    _settingsService.CanvasOptions.FitToCurve = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }

        // Advanced Settings
        public bool IsLogEnabled {
            get => _settingsService.AdvancedOptions.IsLogEnabled;
            set {
                if (_settingsService.AdvancedOptions.IsLogEnabled != value) {
                    _settingsService.AdvancedOptions.IsLogEnabled = value;
                    OnPropertyChanged();
                    _settingsService.SaveSettings();
                }
            }
        }
    }
}
