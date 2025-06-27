using Ink_Canvas.Helpers;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using File = System.IO.File;
using System.Windows.Media;
using System.Windows.Ink;
using System.Windows.Media.Imaging;
using System.Windows.Interop;
using Hardcodet.Wpf.TaskbarNotification;
using OSVersionExtension;

namespace Ink_Canvas {
    public partial class MainWindow : Window {
        #region Behavior

        private void ToggleSwitchSupportPowerPoint_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;

            Settings.PowerPointSettings.PowerPointSupport = ToggleSwitchSupportPowerPoint.IsOn;
            SaveSettingsToFile();

            if (Settings.PowerPointSettings.PowerPointSupport)
                timerCheckPPT.Start();
            else
                timerCheckPPT.Stop();
        }

        private void ToggleSwitchShowCanvasAtNewSlideShow_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;

            Settings.PowerPointSettings.IsShowCanvasAtNewSlideShow = ToggleSwitchShowCanvasAtNewSlideShow.IsOn;
            SaveSettingsToFile();
        }

        #endregion

        #region Startup

        #endregion

        #region Appearance

        //private void ToggleSwitchIsColorfulViewboxFloatingBar_Toggled(object sender, RoutedEventArgs e) {
        //    if (!isLoaded) return;
        //    Settings.Appearance.IsColorfulViewboxFloatingBar = ToggleSwitchColorfulViewboxFloatingBar.IsOn;
        //    SaveSettingsToFile();
        //}

        //[Obsolete]
        //private void ToggleSwitchShowButtonPPTNavigation_OnToggled(object sender, RoutedEventArgs e) {
        //    if (!isLoaded) return;
        //    Settings.PowerPointSettings.IsShowPPTNavigation = ToggleSwitchShowButtonPPTNavigation.IsOn;
        //    var vis = Settings.PowerPointSettings.IsShowPPTNavigation ? Visibility.Visible : Visibility.Collapsed;
        //    PPTLBPageButton.Visibility = vis;
        //    PPTRBPageButton.Visibility = vis;
        //    PPTLSPageButton.Visibility = vis;
        //    PPTRSPageButton.Visibility = vis;
        //    SaveSettingsToFile();
        //}

        //[Obsolete]
        //private void ToggleSwitchShowBottomPPTNavigationPanel_OnToggled(object sender, RoutedEventArgs e) {
        //    if (!isLoaded) return;
        //    Settings.PowerPointSettings.IsShowBottomPPTNavigationPanel = ToggleSwitchShowBottomPPTNavigationPanel.IsOn;
        //    if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible)
        //        //BottomViewboxPPTSidesControl.Visibility = Settings.PowerPointSettings.IsShowBottomPPTNavigationPanel
        //        //    ? Visibility.Visible
        //        //    : Visibility.Collapsed;
        //    SaveSettingsToFile();
        //}

        //[Obsolete]
        //private void ToggleSwitchShowSidePPTNavigationPanel_OnToggled(object sender, RoutedEventArgs e) {
        //    if (!isLoaded) return;
        //    Settings.PowerPointSettings.IsShowSidePPTNavigationPanel = ToggleSwitchShowSidePPTNavigationPanel.IsOn;
        //    if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) {
        //        LeftSidePanelForPPTNavigation.Visibility = Settings.PowerPointSettings.IsShowSidePPTNavigationPanel
        //            ? Visibility.Visible
        //            : Visibility.Collapsed;
        //        RightSidePanelForPPTNavigation.Visibility = Settings.PowerPointSettings.IsShowSidePPTNavigationPanel
        //            ? Visibility.Visible
        //            : Visibility.Collapsed;
        //    }

        //    SaveSettingsToFile();
        //}

        private void ToggleSwitchShowPPTButton_OnToggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.ShowPPTButton = ToggleSwitchShowPPTButton.IsOn;
            SaveSettingsToFile();
            UpdatePPTBtnDisplaySettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void ToggleSwitchEnablePPTButtonPageClickable_OnToggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.EnablePPTButtonPageClickable = ToggleSwitchEnablePPTButtonPageClickable.IsOn;
            SaveSettingsToFile();
        }

        private void CheckboxEnableLBPPTButton_IsCheckChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTButtonsDisplayOption.ToString();
            char[] c = str.ToCharArray();
            c[0] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTButtonsDisplayOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxEnableRBPPTButton_IsCheckChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTButtonsDisplayOption.ToString();
            char[] c = str.ToCharArray();
            c[1] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTButtonsDisplayOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxEnableLSPPTButton_IsCheckChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTButtonsDisplayOption.ToString();
            char[] c = str.ToCharArray();
            c[2] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTButtonsDisplayOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxEnableRSPPTButton_IsCheckChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTButtonsDisplayOption.ToString();
            char[] c = str.ToCharArray();
            c[3] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTButtonsDisplayOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxSPPTDisplayPage_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTSButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[0] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTSButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxSPPTHalfOpacity_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTSButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[1] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTSButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxSPPTBlackBackground_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTSButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[2] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTSButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxBPPTDisplayPage_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTBButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[0] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTBButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxBPPTHalfOpacity_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTBButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[1] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTBButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void CheckboxBPPTBlackBackground_IsCheckChange(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            var str = Settings.PowerPointSettings.PPTBButtonsOption.ToString();
            char[] c = str.ToCharArray();
            c[2] = (bool)((CheckBox)sender).IsChecked ? '2' : '1';
            Settings.PowerPointSettings.PPTBButtonsOption = int.Parse(new string(c));
            SaveSettingsToFile();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnStyleSettingsStatus();
            UpdatePPTBtnPreview();
        }

        private void PPTButtonLeftPositionValueSlider_ValueChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.PPTLSButtonPosition = (int)PPTButtonLeftPositionValueSlider.Value;
            UpdatePPTBtnSlidersStatus();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            SliderDelayAction.DebounceAction(2000, null, SaveSettingsToFile);
            UpdatePPTBtnPreview();
        }

        private void UpdatePPTBtnSlidersStatus() {
            if (PPTButtonLeftPositionValueSlider.Value <= -500 || PPTButtonLeftPositionValueSlider.Value >= 500) {
                if (PPTButtonLeftPositionValueSlider.Value >= 500) {
                    PPTBtnLSPlusBtn.IsEnabled = false;
                    PPTBtnLSPlusBtn.Opacity = 0.5;
                    PPTButtonLeftPositionValueSlider.Value = 500;
                } else if (PPTButtonLeftPositionValueSlider.Value <= -500) {
                    PPTBtnLSMinusBtn.IsEnabled = false;
                    PPTBtnLSMinusBtn.Opacity = 0.5;
                    PPTButtonLeftPositionValueSlider.Value = -500;
                }
            }
            else
            {
                PPTBtnLSPlusBtn.IsEnabled = true;
                PPTBtnLSPlusBtn.Opacity = 1;
                PPTBtnLSMinusBtn.IsEnabled = true;
                PPTBtnLSMinusBtn.Opacity = 1;
            }

            if (PPTButtonRightPositionValueSlider.Value <= -500 || PPTButtonRightPositionValueSlider.Value >= 500)
            {
                if (PPTButtonRightPositionValueSlider.Value >= 500)
                {
                    PPTBtnRSPlusBtn.IsEnabled = false;
                    PPTBtnRSPlusBtn.Opacity = 0.5;
                    PPTButtonRightPositionValueSlider.Value = 500;
                }
                else if (PPTButtonRightPositionValueSlider.Value <= -500)
                {
                    PPTBtnRSMinusBtn.IsEnabled = false;
                    PPTBtnRSMinusBtn.Opacity = 0.5;
                    PPTButtonRightPositionValueSlider.Value = -500;
                }
            }
            else
            {
                PPTBtnRSPlusBtn.IsEnabled = true;
                PPTBtnRSPlusBtn.Opacity = 1;
                PPTBtnRSMinusBtn.IsEnabled = true;
                PPTBtnRSMinusBtn.Opacity = 1;
            }
        }

        private void PPTBtnLSPlusBtn_Clicked(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            PPTButtonLeftPositionValueSlider.Value++;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTLSButtonPosition = (int)PPTButtonLeftPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnLSMinusBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonLeftPositionValueSlider.Value--;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTLSButtonPosition = (int)PPTButtonLeftPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnLSSyncBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonRightPositionValueSlider.Value = PPTButtonLeftPositionValueSlider.Value;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTRSButtonPosition = (int)PPTButtonLeftPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnLSResetBtn_Clicked(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            PPTButtonLeftPositionValueSlider.Value = 0;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTLSButtonPosition = 0;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnRSPlusBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonRightPositionValueSlider.Value++;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTRSButtonPosition = (int)PPTButtonRightPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnRSMinusBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonRightPositionValueSlider.Value--;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTRSButtonPosition = (int)PPTButtonRightPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnRSSyncBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonLeftPositionValueSlider.Value = PPTButtonRightPositionValueSlider.Value;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTLSButtonPosition = (int)PPTButtonRightPositionValueSlider.Value;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private void PPTBtnRSResetBtn_Clicked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            PPTButtonRightPositionValueSlider.Value = 0;
            UpdatePPTBtnSlidersStatus();
            Settings.PowerPointSettings.PPTRSButtonPosition = 0;
            SaveSettingsToFile();
            UpdatePPTBtnPreview();
        }

        private DelayAction SliderDelayAction = new DelayAction();

        private void PPTButtonRightPositionValueSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.PowerPointSettings.PPTRSButtonPosition = (int)PPTButtonRightPositionValueSlider.Value;
            UpdatePPTBtnSlidersStatus();
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) UpdatePPTBtnDisplaySettingsStatus();
            SliderDelayAction.DebounceAction(2000,null, SaveSettingsToFile);
            UpdatePPTBtnPreview();
        }

        private void UpdatePPTBtnPreview() {
            //new BitmapImage(new Uri("pack://application:,,,/Resources/new-icons/unfold-chevron.png"));
            var bopt = Settings.PowerPointSettings.PPTBButtonsOption.ToString();
            char[] boptc = bopt.ToCharArray();
            if (boptc[1] == '2') {
                PPTBtnPreviewLB.Opacity = 0.5;
                PPTBtnPreviewRB.Opacity = 0.5;
            } else {
                PPTBtnPreviewLB.Opacity = 1;
                PPTBtnPreviewRB.Opacity = 1;
            }

            if (boptc[2] == '2') {
                PPTBtnPreviewLB.Source =
                    new BitmapImage(
                        new Uri("pack://application:,,,/Resources/PresentationExample/bottombar-dark.png"));
                PPTBtnPreviewRB.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Resources/PresentationExample/bottombar-dark.png"));
            } else {
                PPTBtnPreviewLB.Source =
                    new BitmapImage(
                        new Uri("pack://application:,,,/Resources/PresentationExample/bottombar-white.png"));
                PPTBtnPreviewRB.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Resources/PresentationExample/bottombar-white.png"));
            }

            var sopt = Settings.PowerPointSettings.PPTSButtonsOption.ToString();
            char[] soptc = sopt.ToCharArray();
            if (soptc[1] == '2')
            {
                PPTBtnPreviewLS.Opacity = 0.5;
                PPTBtnPreviewRS.Opacity = 0.5;
            }
            else
            {
                PPTBtnPreviewLS.Opacity = 1;
                PPTBtnPreviewRS.Opacity = 1;
            }

            if (soptc[2] == '2')
            {
                PPTBtnPreviewLS.Source =
                    new BitmapImage(
                        new Uri("pack://application:,,,/Resources/PresentationExample/sidebar-dark.png"));
                PPTBtnPreviewRS.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Resources/PresentationExample/sidebar-dark.png"));
            }
            else
            {
                PPTBtnPreviewLS.Source =
                    new BitmapImage(
                        new Uri("pack://application:,,,/Resources/PresentationExample/sidebar-white.png"));
                PPTBtnPreviewRS.Source = new BitmapImage(
                    new Uri("pack://application:,,,/Resources/PresentationExample/sidebar-white.png"));
            }

            var dopt = Settings.PowerPointSettings.PPTButtonsDisplayOption.ToString();
            char[] doptc = dopt.ToCharArray();

            if (Settings.PowerPointSettings.ShowPPTButton) {
                PPTBtnPreviewLB.Visibility = doptc[0] == '2' ? Visibility.Visible : Visibility.Collapsed;
                PPTBtnPreviewRB.Visibility = doptc[1] == '2' ? Visibility.Visible : Visibility.Collapsed;
                PPTBtnPreviewLS.Visibility = doptc[2] == '2' ? Visibility.Visible : Visibility.Collapsed;
                PPTBtnPreviewRS.Visibility = doptc[3] == '2' ? Visibility.Visible : Visibility.Collapsed;
            } else {
                PPTBtnPreviewLB.Visibility = Visibility.Collapsed;
                PPTBtnPreviewRB.Visibility = Visibility.Collapsed;
                PPTBtnPreviewLS.Visibility = Visibility.Collapsed;
                PPTBtnPreviewRS.Visibility = Visibility.Collapsed;
            }
            
            PPTBtnPreviewRSTransform.Y = -(Settings.PowerPointSettings.PPTRSButtonPosition * 0.5);
            PPTBtnPreviewLSTransform.Y = -(Settings.PowerPointSettings.PPTLSButtonPosition * 0.5);
        }

        #endregion

        #region Canvas

        #endregion

        #region Automation

        private void StartOrStoptimerCheckAutoFold() {
            if (Settings.Automation.IsEnableAutoFold)
                timerCheckAutoFold.Start();
            else
                timerCheckAutoFold.Stop();
        }

        private void ToggleSwitchAutoFoldInEasiNote_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiNote = ToggleSwitchAutoFoldInEasiNote.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInEasiNoteIgnoreDesktopAnno_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiNoteIgnoreDesktopAnno =
                ToggleSwitchAutoFoldInEasiNoteIgnoreDesktopAnno.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoFoldInEasiCamera_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiCamera = ToggleSwitchAutoFoldInEasiCamera.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInEasiNote3_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiNote3 = ToggleSwitchAutoFoldInEasiNote3.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInEasiNote3C_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiNote3C = ToggleSwitchAutoFoldInEasiNote3C.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInEasiNote5C_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInEasiNote5C = ToggleSwitchAutoFoldInEasiNote5C.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInSeewoPincoTeacher_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInSeewoPincoTeacher = ToggleSwitchAutoFoldInSeewoPincoTeacher.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInHiteTouchPro_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInHiteTouchPro = ToggleSwitchAutoFoldInHiteTouchPro.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInHiteLightBoard_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInHiteLightBoard = ToggleSwitchAutoFoldInHiteLightBoard.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInHiteCamera_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInHiteCamera = ToggleSwitchAutoFoldInHiteCamera.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInWxBoardMain_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInWxBoardMain = ToggleSwitchAutoFoldInWxBoardMain.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInOldZyBoard_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInOldZyBoard = ToggleSwitchAutoFoldInOldZyBoard.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInMSWhiteboard_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInMSWhiteboard = ToggleSwitchAutoFoldInMSWhiteboard.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInAdmoxWhiteboard_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInAdmoxWhiteboard = ToggleSwitchAutoFoldInAdmoxWhiteboard.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInAdmoxBooth_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInAdmoxBooth = ToggleSwitchAutoFoldInAdmoxBooth.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInQPoint_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInQPoint = ToggleSwitchAutoFoldInQPoint.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInYiYunVisualPresenter_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInYiYunVisualPresenter = ToggleSwitchAutoFoldInYiYunVisualPresenter.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInMaxHubWhiteboard_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInMaxHubWhiteboard = ToggleSwitchAutoFoldInMaxHubWhiteboard.IsOn;
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoFoldInPPTSlideShow_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoFoldInPPTSlideShow = ToggleSwitchAutoFoldInPPTSlideShow.IsOn;
            if (Settings.Automation.IsAutoFoldInPPTSlideShow)
            {
                SettingsPPTInkingAndAutoFoldExplictBorder.Visibility = Visibility.Visible;
                SettingsShowCanvasAtNewSlideShowStackPanel.Opacity = 0.5;
                SettingsShowCanvasAtNewSlideShowStackPanel.IsHitTestVisible = false;
            } else {
                SettingsPPTInkingAndAutoFoldExplictBorder.Visibility = Visibility.Collapsed;
                SettingsShowCanvasAtNewSlideShowStackPanel.Opacity = 1;
                SettingsShowCanvasAtNewSlideShowStackPanel.IsHitTestVisible = true;
            }
            SaveSettingsToFile();
            StartOrStoptimerCheckAutoFold();
        }

        private void ToggleSwitchAutoKillPptService_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillPptService = ToggleSwitchAutoKillPptService.IsOn;
            SaveSettingsToFile();

            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillEasiNote_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillEasiNote = ToggleSwitchAutoKillEasiNote.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillHiteAnnotation_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillHiteAnnotation = ToggleSwitchAutoKillHiteAnnotation.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillVComYouJiao_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillVComYouJiao = ToggleSwitchAutoKillVComYouJiao.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillSeewoLauncher2DesktopAnnotation_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation = ToggleSwitchAutoKillSeewoLauncher2DesktopAnnotation.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillInkCanvas_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillInkCanvas = ToggleSwitchAutoKillInkCanvas.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillICA_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillICA = ToggleSwitchAutoKillICA.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchAutoKillIDT_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Automation.IsAutoKillIDT = ToggleSwitchAutoKillIDT.IsOn;
            SaveSettingsToFile();
            if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService ||
                Settings.Automation.IsAutoKillHiteAnnotation || Settings.Automation.IsAutoKillInkCanvas
                || Settings.Automation.IsAutoKillICA || Settings.Automation.IsAutoKillIDT || Settings.Automation.IsAutoKillVComYouJiao
                || Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation)
                timerKillProcess.Start();
            else
                timerKillProcess.Stop();
        }

        private void ToggleSwitchSaveScreenshotsInDateFolders_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsSaveScreenshotsInDateFolders = ToggleSwitchSaveScreenshotsInDateFolders.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoSaveStrokesAtScreenshot_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoSaveStrokesAtScreenshot = ToggleSwitchAutoSaveStrokesAtScreenshot.IsOn;
            ToggleSwitchAutoSaveStrokesAtClear.Header =
                ToggleSwitchAutoSaveStrokesAtScreenshot.IsOn ? "清屏时自动截图并保存墨迹" : "清屏时自动截图";
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoSaveStrokesAtClear_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.IsAutoSaveStrokesAtClear = ToggleSwitchAutoSaveStrokesAtClear.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoSaveStrokesInPowerPoint_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsAutoSaveStrokesInPowerPoint = ToggleSwitchAutoSaveStrokesInPowerPoint.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchNotifyPreviousPage_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsNotifyPreviousPage = ToggleSwitchNotifyPreviousPage.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchNotifyHiddenPage_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsNotifyHiddenPage = ToggleSwitchNotifyHiddenPage.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchNotifyAutoPlayPresentation_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsNotifyAutoPlayPresentation = ToggleSwitchNotifyAutoPlayPresentation.IsOn;
            SaveSettingsToFile();
        }

        private void SideControlMinimumAutomationSlider_ValueChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.MinimumAutomationStrokeNumber = (int)SideControlMinimumAutomationSlider.Value;
            SaveSettingsToFile();
        }

        private void AutoSavedStrokesLocationTextBox_TextChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.AutoSavedStrokesLocation = AutoSavedStrokesLocation.Text;
            SaveSettingsToFile();
        }

        private void AutoSavedStrokesLocationButton_Click(object sender, RoutedEventArgs e) {
            var folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            folderBrowser.ShowDialog();
            if (folderBrowser.SelectedPath.Length > 0) AutoSavedStrokesLocation.Text = folderBrowser.SelectedPath;
        }

        private void SetAutoSavedStrokesLocationToDiskDButton_Click(object sender, RoutedEventArgs e) {
            AutoSavedStrokesLocation.Text = @"D:\Ink Canvas";
        }

        private void SetAutoSavedStrokesLocationToDocumentFolderButton_Click(object sender, RoutedEventArgs e) {
            AutoSavedStrokesLocation.Text =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Ink Canvas";
        }

        private void ToggleSwitchAutoDelSavedFiles_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.AutoDelSavedFiles = ToggleSwitchAutoDelSavedFiles.IsOn;
            SaveSettingsToFile();
        }

        private void
            ComboBoxAutoDelSavedFilesDaysThreshold_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (!isLoaded) return;
            Settings.Automation.AutoDelSavedFilesDaysThreshold =
                int.Parse(((ComboBoxItem)ComboBoxAutoDelSavedFilesDaysThreshold.SelectedItem).Content.ToString());
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoSaveScreenShotInPowerPoint_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsAutoSaveScreenShotInPowerPoint =
                ToggleSwitchAutoSaveScreenShotInPowerPoint.IsOn;
            SaveSettingsToFile();
        }

        #endregion

        #region Gesture

        private void ToggleSwitchEnableFingerGestureSlideShowControl_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsEnableFingerGestureSlideShowControl =
                ToggleSwitchEnableFingerGestureSlideShowControl.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchAutoSwitchTwoFingerGesture_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Gesture.AutoSwitchTwoFingerGesture = ToggleSwitchAutoSwitchTwoFingerGesture.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableTwoFingerZoom_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            if (sender == ToggleSwitchEnableTwoFingerZoom)
                BoardToggleSwitchEnableTwoFingerZoom.IsOn = ToggleSwitchEnableTwoFingerZoom.IsOn;
            else
                ToggleSwitchEnableTwoFingerZoom.IsOn = BoardToggleSwitchEnableTwoFingerZoom.IsOn;
            Settings.Gesture.IsEnableTwoFingerZoom = ToggleSwitchEnableTwoFingerZoom.IsOn;
            CheckEnableTwoFingerGestureBtnColorPrompt();
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableMultiTouchMode_Toggled(object sender, RoutedEventArgs e) {
            //if (!isLoaded) return;
            if (sender == ToggleSwitchEnableMultiTouchMode)
                BoardToggleSwitchEnableMultiTouchMode.IsOn = ToggleSwitchEnableMultiTouchMode.IsOn;
            else
                ToggleSwitchEnableMultiTouchMode.IsOn = BoardToggleSwitchEnableMultiTouchMode.IsOn;
            if (ToggleSwitchEnableMultiTouchMode.IsOn) {
                if (!isInMultiTouchMode) {
                    inkCanvas.StylusDown += MainWindow_StylusDown;
                    inkCanvas.StylusMove += MainWindow_StylusMove;
                    inkCanvas.StylusUp += MainWindow_StylusUp;
                    inkCanvas.TouchDown += MainWindow_TouchDown;
                    inkCanvas.TouchDown -= Main_Grid_TouchDown;
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    inkCanvas.Children.Clear();
                    isInMultiTouchMode = true;
                }
            } else {
                if (isInMultiTouchMode) {
                    inkCanvas.StylusDown -= MainWindow_StylusDown;
                    inkCanvas.StylusMove -= MainWindow_StylusMove;
                    inkCanvas.StylusUp -= MainWindow_StylusUp;
                    inkCanvas.TouchDown -= MainWindow_TouchDown;
                    inkCanvas.TouchDown += Main_Grid_TouchDown;
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                    inkCanvas.Children.Clear();
                    isInMultiTouchMode = false;
                }
            }

            Settings.Gesture.IsEnableMultiTouchMode = ToggleSwitchEnableMultiTouchMode.IsOn;
            CheckEnableTwoFingerGestureBtnColorPrompt();
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableTwoFingerTranslate_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            if (sender == ToggleSwitchEnableTwoFingerTranslate)
                BoardToggleSwitchEnableTwoFingerTranslate.IsOn = ToggleSwitchEnableTwoFingerTranslate.IsOn;
            else
                ToggleSwitchEnableTwoFingerTranslate.IsOn = BoardToggleSwitchEnableTwoFingerTranslate.IsOn;
            Settings.Gesture.IsEnableTwoFingerTranslate = ToggleSwitchEnableTwoFingerTranslate.IsOn;
            CheckEnableTwoFingerGestureBtnColorPrompt();
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableTwoFingerRotation_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;

            if (sender == ToggleSwitchEnableTwoFingerRotation)
                BoardToggleSwitchEnableTwoFingerRotation.IsOn = ToggleSwitchEnableTwoFingerRotation.IsOn;
            else
                ToggleSwitchEnableTwoFingerRotation.IsOn = BoardToggleSwitchEnableTwoFingerRotation.IsOn;
            Settings.Gesture.IsEnableTwoFingerRotation = ToggleSwitchEnableTwoFingerRotation.IsOn;
            Settings.Gesture.IsEnableTwoFingerRotationOnSelection = ToggleSwitchEnableTwoFingerRotationOnSelection.IsOn;
            CheckEnableTwoFingerGestureBtnColorPrompt();
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableTwoFingerGestureInPresentationMode_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.PowerPointSettings.IsEnableTwoFingerGestureInPresentationMode =
                ToggleSwitchEnableTwoFingerGestureInPresentationMode.IsOn;
            SaveSettingsToFile();
        }

        #endregion

        #region Reset

        public static void SetSettingsToRecommendation() {
            var AutoDelSavedFilesDays = Settings.Automation.AutoDelSavedFiles;
            var AutoDelSavedFilesDaysThreshold = Settings.Automation.AutoDelSavedFilesDaysThreshold;
            Settings = new Settings();
            Settings.Advanced.IsSpecialScreen = true;
            Settings.Advanced.IsQuadIR = false;
            Settings.Advanced.TouchMultiplier = 0.3;
            Settings.Advanced.NibModeBoundsWidth = 5;
            Settings.Advanced.FingerModeBoundsWidth = 20;
            Settings.Advanced.EraserBindTouchMultiplier = true;
            Settings.Advanced.IsLogEnabled = true;
            Settings.Advanced.IsSecondConfirmWhenShutdownApp = false;
            Settings.Advanced.IsEnableEdgeGestureUtil = false;
            Settings.Advanced.EdgeGestureUtilOnlyAffectBlackboardMode = false;
            Settings.Advanced.IsEnableFullScreenHelper = false;
            Settings.Advanced.IsEnableForceFullScreen = false;
            Settings.Advanced.IsEnableDPIChangeDetection = false;
            Settings.Advanced.IsEnableResolutionChangeDetection = false;

            Settings.Appearance.IsEnableDisPlayNibModeToggler = false;
            Settings.Appearance.IsColorfulViewboxFloatingBar = false;
            Settings.Appearance.ViewboxFloatingBarScaleTransformValue = 1;
            Settings.Appearance.EnableViewboxBlackBoardScaleTransform = false;
            Settings.Appearance.IsTransparentButtonBackground = true;
            Settings.Appearance.IsShowExitButton = true;
            Settings.Appearance.IsShowEraserButton = true;
            Settings.Appearance.IsShowHideControlButton = false;
            Settings.Appearance.IsShowLRSwitchButton = false;
            Settings.Appearance.IsShowModeFingerToggleSwitch = true;
            Settings.Appearance.IsShowQuickPanel = true;
            Settings.Appearance.Theme = 0;
            Settings.Appearance.EnableChickenSoupInWhiteboardMode = true;
            Settings.Appearance.EnableTimeDisplayInWhiteboardMode = true;
            Settings.Appearance.ChickenSoupSource = 1;
            Settings.Appearance.ViewboxFloatingBarOpacityValue = 1.0;
            Settings.Appearance.ViewboxFloatingBarOpacityInPPTValue = 1.0;
            Settings.Appearance.EnableTrayIcon = true;

            Settings.Automation.IsAutoFoldInEasiNote = true;
            Settings.Automation.IsAutoFoldInEasiNoteIgnoreDesktopAnno = true;
            Settings.Automation.IsAutoFoldInEasiCamera = true;
            Settings.Automation.IsAutoFoldInEasiNote3C = false;
            Settings.Automation.IsAutoFoldInEasiNote3 = false;
            Settings.Automation.IsAutoFoldInEasiNote5C = true;
            Settings.Automation.IsAutoFoldInSeewoPincoTeacher = false;
            Settings.Automation.IsAutoFoldInHiteTouchPro = false;
            Settings.Automation.IsAutoFoldInHiteCamera = false;
            Settings.Automation.IsAutoFoldInWxBoardMain = false;
            Settings.Automation.IsAutoFoldInOldZyBoard = false;
            Settings.Automation.IsAutoFoldInMSWhiteboard = false;
            Settings.Automation.IsAutoFoldInAdmoxWhiteboard = false;
            Settings.Automation.IsAutoFoldInAdmoxBooth = false;
            Settings.Automation.IsAutoFoldInQPoint = false;
            Settings.Automation.IsAutoFoldInYiYunVisualPresenter = false;
            Settings.Automation.IsAutoFoldInMaxHubWhiteboard = false;
            Settings.Automation.IsAutoFoldInPPTSlideShow = false;
            Settings.Automation.IsAutoKillPptService = false;
            Settings.Automation.IsAutoKillEasiNote = false;
            Settings.Automation.IsAutoKillVComYouJiao = false;
            Settings.Automation.IsAutoKillInkCanvas = false;
            Settings.Automation.IsAutoKillICA = false;
            Settings.Automation.IsAutoKillIDT = true;
            Settings.Automation.IsAutoKillSeewoLauncher2DesktopAnnotation = false;
            Settings.Automation.IsSaveScreenshotsInDateFolders = false;
            Settings.Automation.IsAutoSaveStrokesAtScreenshot = true;
            Settings.Automation.IsAutoSaveStrokesAtClear = true;
            Settings.Automation.IsAutoClearWhenExitingWritingMode = false;
            Settings.Automation.MinimumAutomationStrokeNumber = 0;
            Settings.Automation.AutoDelSavedFiles = AutoDelSavedFilesDays;
            Settings.Automation.AutoDelSavedFilesDaysThreshold = AutoDelSavedFilesDaysThreshold;

            //Settings.PowerPointSettings.IsShowPPTNavigation = true;
            //Settings.PowerPointSettings.IsShowBottomPPTNavigationPanel = false;
            //Settings.PowerPointSettings.IsShowSidePPTNavigationPanel = true;
            Settings.PowerPointSettings.PowerPointSupport = true;
            Settings.PowerPointSettings.IsShowCanvasAtNewSlideShow = false;
            Settings.PowerPointSettings.IsNoClearStrokeOnSelectWhenInPowerPoint = true;
            Settings.PowerPointSettings.IsShowStrokeOnSelectInPowerPoint = false;
            Settings.PowerPointSettings.IsAutoSaveStrokesInPowerPoint = true;
            Settings.PowerPointSettings.IsAutoSaveScreenShotInPowerPoint = true;
            Settings.PowerPointSettings.IsNotifyPreviousPage = false;
            Settings.PowerPointSettings.IsNotifyHiddenPage = false;
            Settings.PowerPointSettings.IsEnableTwoFingerGestureInPresentationMode = false;
            Settings.PowerPointSettings.IsEnableFingerGestureSlideShowControl = false;
            Settings.PowerPointSettings.IsSupportWPS = true;

            Settings.Canvas.InkWidth = 2.5;
            Settings.Canvas.IsShowCursor = false;
            Settings.Canvas.InkStyle = 0;
            Settings.Canvas.HighlighterWidth = 20;
            Settings.Canvas.EraserSize = 1;
            Settings.Canvas.EraserType = 0;
            Settings.Canvas.EraserShapeType = 1;
            Settings.Canvas.HideStrokeWhenSelecting = false;
            Settings.Canvas.ClearCanvasAndClearTimeMachine = false;
            Settings.Canvas.FitToCurve = true;
            Settings.Canvas.UsingWhiteboard = false;
            Settings.Canvas.HyperbolaAsymptoteOption = 0;

            Settings.Gesture.AutoSwitchTwoFingerGesture = true;
            Settings.Gesture.IsEnableTwoFingerTranslate = true;
            Settings.Gesture.IsEnableTwoFingerZoom = false;
            Settings.Gesture.IsEnableTwoFingerRotation = false;
            Settings.Gesture.IsEnableTwoFingerRotationOnSelection = false;

            Settings.InkToShape.IsInkToShapeEnabled = true;
            Settings.InkToShape.IsInkToShapeNoFakePressureRectangle = false;
            Settings.InkToShape.IsInkToShapeNoFakePressureTriangle = false;
            Settings.InkToShape.IsInkToShapeTriangle = true;
            Settings.InkToShape.IsInkToShapeRectangle = true;
            Settings.InkToShape.IsInkToShapeRounded = true;


            Settings.Startup.IsEnableNibMode = false;
            Settings.Startup.IsAutoUpdate = true;
            Settings.Startup.IsAutoUpdateWithSilence = true;
            Settings.Startup.AutoUpdateWithSilenceStartTime = "18:20";
            Settings.Startup.AutoUpdateWithSilenceEndTime = "07:40";
            Settings.Startup.IsFoldAtStartup = false;
        }

        private void BtnResetToSuggestion_Click(object sender, RoutedEventArgs e) {
            try {
                isLoaded = false;
                SetSettingsToRecommendation();
                SaveSettingsToFile();
                LoadSettings();
                isLoaded = true;

                ToggleSwitchRunAtStartup.IsOn = true;
            }
            catch { }

            ShowNotification("设置已重置为默认推荐设置~");
        }

        private async void SpecialVersionResetToSuggestion_Click() {
            await Task.Delay(1000);
            try {
                isLoaded = false;
                SetSettingsToRecommendation();
                Settings.Automation.AutoDelSavedFiles = true;
                Settings.Automation.AutoDelSavedFilesDaysThreshold = 15;
                SetAutoSavedStrokesLocationToDiskDButton_Click(null, null);
                SaveSettingsToFile();
                LoadSettings();
                isLoaded = true;
            }
            catch { }
        }

        #endregion

        #region Ink To Shape

        private void ToggleSwitchEnableInkToShape_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeEnabled = ToggleSwitchEnableInkToShape.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableInkToShapeNoFakePressureTriangle_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeNoFakePressureTriangle =
                ToggleSwitchEnableInkToShapeNoFakePressureTriangle.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchEnableInkToShapeNoFakePressureRectangle_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeNoFakePressureRectangle =
                ToggleSwitchEnableInkToShapeNoFakePressureRectangle.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleCheckboxEnableInkToShapeTriangle_CheckedChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeTriangle = (bool)ToggleCheckboxEnableInkToShapeTriangle.IsChecked;
            SaveSettingsToFile();
        }

        private void ToggleCheckboxEnableInkToShapeRectangle_CheckedChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeRectangle = (bool)ToggleCheckboxEnableInkToShapeRectangle.IsChecked;
            SaveSettingsToFile();
        }

        private void ToggleCheckboxEnableInkToShapeRounded_CheckedChanged(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.InkToShape.IsInkToShapeRounded = (bool)ToggleCheckboxEnableInkToShapeRounded.IsChecked;
            SaveSettingsToFile();
        }

        #endregion

        #region Advanced

        private void ToggleSwitchIsSpecialScreen_OnToggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsSpecialScreen = ToggleSwitchIsSpecialScreen.IsOn;
            TouchMultiplierSlider.Visibility =
                ToggleSwitchIsSpecialScreen.IsOn ? Visibility.Visible : Visibility.Collapsed;
            SaveSettingsToFile();
        }

        private void TouchMultiplierSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (!isLoaded) return;
            Settings.Advanced.TouchMultiplier = e.NewValue;
            SaveSettingsToFile();
        }

        private void BorderCalculateMultiplier_TouchDown(object sender, TouchEventArgs e) {
            var args = e.GetTouchPoint(null).Bounds;
            double value;
            if (!Settings.Advanced.IsQuadIR) value = args.Width;
            else value = Math.Sqrt(args.Width * args.Height); //四边红外

            TextBlockShowCalculatedMultiplier.Text = (5 / (value * 1.1)).ToString();
        }

        private void ToggleSwitchIsEnableFullScreenHelper_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsEnableFullScreenHelper = ToggleSwitchIsEnableFullScreenHelper.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsEnableEdgeGestureUtil_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsEnableEdgeGestureUtil = ToggleSwitchIsEnableEdgeGestureUtil.IsOn;
            if (OSVersion.GetOperatingSystem() >= OSVersionExtension.OperatingSystem.Windows10) EdgeGestureUtil.DisableEdgeGestures(new WindowInteropHelper(this).Handle, ToggleSwitchIsEnableEdgeGestureUtil.IsOn);
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsEnableForceFullScreen_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsEnableForceFullScreen = ToggleSwitchIsEnableForceFullScreen.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsEnableDPIChangeDetection_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Advanced.IsEnableDPIChangeDetection = ToggleSwitchIsEnableDPIChangeDetection.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsEnableResolutionChangeDetection_Toggled(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.Advanced.IsEnableResolutionChangeDetection = ToggleSwitchIsEnableResolutionChangeDetection.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchEraserBindTouchMultiplier_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.EraserBindTouchMultiplier = ToggleSwitchEraserBindTouchMultiplier.IsOn;
            SaveSettingsToFile();
        }

        private void NibModeBoundsWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (!isLoaded) return;
            Settings.Advanced.NibModeBoundsWidth = (int)e.NewValue;

            if (Settings.Startup.IsEnableNibMode)
                BoundsWidth = Settings.Advanced.NibModeBoundsWidth;
            else
                BoundsWidth = Settings.Advanced.FingerModeBoundsWidth;

            SaveSettingsToFile();
        }

        private void FingerModeBoundsWidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            if (!isLoaded) return;
            Settings.Advanced.FingerModeBoundsWidth = (int)e.NewValue;

            if (Settings.Startup.IsEnableNibMode)
                BoundsWidth = Settings.Advanced.NibModeBoundsWidth;
            else
                BoundsWidth = Settings.Advanced.FingerModeBoundsWidth;

            SaveSettingsToFile();
        }

        private void ToggleSwitchIsQuadIR_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsQuadIR = ToggleSwitchIsQuadIR.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsLogEnabled_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsLogEnabled = ToggleSwitchIsLogEnabled.IsOn;
            SaveSettingsToFile();
        }

        private void ToggleSwitchIsSecondConfimeWhenShutdownApp_Toggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.Advanced.IsSecondConfirmWhenShutdownApp = ToggleSwitchIsSecondConfimeWhenShutdownApp.IsOn;
            SaveSettingsToFile();
        }

        #endregion

        #region RandSettings

        private void ToggleSwitchDisplayRandWindowNamesInputBtn_OnToggled(object sender, RoutedEventArgs e) {
            if (!isLoaded) return;
            Settings.RandSettings.DisplayRandWindowNamesInputBtn = ToggleSwitchDisplayRandWindowNamesInputBtn.IsOn;
            SaveSettingsToFile();
        }

        private void RandWindowOnceCloseLatencySlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.RandSettings.RandWindowOnceCloseLatency = RandWindowOnceCloseLatencySlider.Value;
            SaveSettingsToFile();
        }

        private void RandWindowOnceMaxStudentsSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (!isLoaded) return;
            Settings.RandSettings.RandWindowOnceMaxStudents = (int)RandWindowOnceMaxStudentsSlider.Value;
            SaveSettingsToFile();
        }

        #endregion

        public static void SaveSettingsToFile() {
            var text = JsonConvert.SerializeObject(Settings, Formatting.Indented);
            try {
                File.WriteAllText(App.RootPath + settingsFileName, text);
            }
            catch { }
        }

        private void SCManipulationBoundaryFeedback(object sender, ManipulationBoundaryFeedbackEventArgs e) {
            e.Handled = true;
        }

        private void HyperlinkSourceToICCRepository_Click(object sender, RoutedEventArgs e) {
            Process.Start("https://gitea.bliemhax.com/kriastans/InkCanvasForClass");
            HideSubPanels();
        }

        private void HyperlinkSourceToPresentRepository_Click(object sender, RoutedEventArgs e) {
            Process.Start("https://github.com/ChangSakura/Ink-Canvas");
            HideSubPanels();
        }

        private void HyperlinkSourceToOringinalRepository_Click(object sender, RoutedEventArgs e) {
            Process.Start("https://github.com/WXRIW/Ink-Canvas");
            HideSubPanels();
        }
    }
}