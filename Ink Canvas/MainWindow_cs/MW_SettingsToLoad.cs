﻿using Ink_Canvas.Helpers;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using File = System.IO.File;

namespace Ink_Canvas {
    public partial class MainWindow : Window {
        private void LoadSettings(bool isStartup = false) {
            AppVersionTextBlock.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try {
                if (File.Exists(App.RootPath + settingsFileName)) {
                    try {
                        string text = File.ReadAllText(App.RootPath + settingsFileName);
                        Settings = JsonConvert.DeserializeObject<Settings>(text);
                    } catch { }
                } else {
                    BtnResetToSuggestion_Click(null, null);
                }
            } catch (Exception ex) {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }
            // Startup
            if (isStartup) {
                CursorIcon_Click(null, null);
            }
            try {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\Ink Canvas Annotation.lnk")) {
                    ToggleSwitchRunAtStartup.IsOn = true;
                }
            } catch (Exception ex) {
                LogHelper.WriteLogToFile(ex.ToString(), LogHelper.LogType.Error);
            }
            if (Settings.Startup != null) {
                if (isStartup) {
                    if (Settings.Automation.AutoDelSavedFiles) {
                        DelAutoSavedFiles.DeleteFilesOlder(Settings.Automation.AutoSavedStrokesLocation, Settings.Automation.AutoDelSavedFilesDaysThreshold);
                    }
                    if (Settings.Startup.IsFoldAtStartup) {
                        FoldFloatingBar_MouseUp(Fold_Icon, null);
                    }
                }
                if (Settings.Startup.IsEnableNibMode) {
                    ToggleSwitchEnableNibMode.IsOn = true;
                    ToggleSwitchBoardEnableNibMode.IsOn = true;
                    BoundsWidth = Settings.Advanced.NibModeBoundsWidth;
                } else {
                    ToggleSwitchEnableNibMode.IsOn = false;
                    ToggleSwitchBoardEnableNibMode.IsOn = false;
                    BoundsWidth = Settings.Advanced.FingerModeBoundsWidth;
                }
                if (Settings.Startup.IsAutoUpdate) {
                    ToggleSwitchIsAutoUpdate.IsOn = true;
                    AutoUpdate();
                }
                // ToggleSwitchIsAutoUpdateWithSilence.Visibility = Settings.Startup.IsAutoUpdate ? Visibility.Visible : Visibility.Collapsed;
                if (Settings.Startup.IsAutoUpdateWithSilence) {
                    ToggleSwitchIsAutoUpdateWithSilence.IsOn = true;
                }
                AutoUpdateTimePeriodBlock.Visibility = Settings.Startup.IsAutoUpdateWithSilence ? Visibility.Visible : Visibility.Collapsed;

                AutoUpdateWithSilenceTimeComboBox.InitializeAutoUpdateWithSilenceTimeComboBoxOptions(AutoUpdateWithSilenceStartTimeComboBox, AutoUpdateWithSilenceEndTimeComboBox);
                AutoUpdateWithSilenceStartTimeComboBox.SelectedItem = Settings.Startup.AutoUpdateWithSilenceStartTime;
                AutoUpdateWithSilenceEndTimeComboBox.SelectedItem = Settings.Startup.AutoUpdateWithSilenceEndTime;

                ToggleSwitchFoldAtStartup.IsOn = Settings.Startup.IsFoldAtStartup;
            } else {
                Settings.Startup = new Startup();
            }
            // Appearance
            if (Settings.Appearance != null) {
                if (!Settings.Appearance.IsEnableDisPlayNibModeToggler) {
                    NibModeSimpleStackPanel.Visibility = Visibility.Collapsed;
                    BoardNibModeSimpleStackPanel.Visibility = Visibility.Collapsed;
                } else {
                    NibModeSimpleStackPanel.Visibility = Visibility.Visible;
                    BoardNibModeSimpleStackPanel.Visibility = Visibility.Visible;
                }
                //if (Settings.Appearance.IsColorfulViewboxFloatingBar) // 浮动工具栏背景色
                //{
                //    LinearGradientBrush gradientBrush = new LinearGradientBrush();
                //    gradientBrush.StartPoint = new Point(0, 0);
                //    gradientBrush.EndPoint = new Point(1, 1);
                //    GradientStop blueStop = new GradientStop(Color.FromArgb(0x95, 0x80, 0xB0, 0xFF), 0);
                //    GradientStop greenStop = new GradientStop(Color.FromArgb(0x95, 0xC0, 0xFF, 0xC0), 1);
                //    gradientBrush.GradientStops.Add(blueStop);
                //    gradientBrush.GradientStops.Add(greenStop);
                //    EnableTwoFingerGestureBorder.Background = gradientBrush;
                //    BorderFloatingBarMainControls.Background = gradientBrush;
                //    BorderFloatingBarMoveControls.Background = gradientBrush;
                //    BorderFloatingBarExitPPTBtn.Background = gradientBrush;

                //    ToggleSwitchColorfulViewboxFloatingBar.IsOn = true;
                //} else {
                //    EnableTwoFingerGestureBorder.Background = (Brush)FindResource("FloatBarBackground");
                //    BorderFloatingBarMainControls.Background = (Brush)FindResource("FloatBarBackground");
                //    BorderFloatingBarMoveControls.Background = (Brush)FindResource("FloatBarBackground");
                //    BorderFloatingBarExitPPTBtn.Background = (Brush)FindResource("FloatBarBackground");

                //    ToggleSwitchColorfulViewboxFloatingBar.IsOn = false;
                //}
                if (Settings.Appearance.ViewboxFloatingBarScaleTransformValue != 0) // 浮动工具栏 UI 缩放 85%
                {
                    double val = Settings.Appearance.ViewboxFloatingBarScaleTransformValue;
                    ViewboxFloatingBarScaleTransform.ScaleX = (val > 0.5 && val < 1.25) ? val : val <= 0.5 ? 0.5 : val >= 1.25 ? 1.25 : 1;
                    ViewboxFloatingBarScaleTransform.ScaleY = (val > 0.5 && val < 1.25) ? val : val <= 0.5 ? 0.5 : val >= 1.25 ? 1.25 : 1;
                    ViewboxFloatingBarScaleTransformValueSlider.Value = val;
                }

                ComboBoxUnFoldBtnImg.SelectedIndex = Settings.Appearance.UnFoldButtonImageType;
                if (Settings.Appearance.UnFoldButtonImageType==0)
                {
                    RightUnFoldBtnImgChevron.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/new-icons/unfold-chevron.png"));
                    RightUnFoldBtnImgChevron.Width = 14;
                    RightUnFoldBtnImgChevron.Height = 14;
                    RightUnFoldBtnImgChevron.RenderTransform = new RotateTransform(180);
                    LeftUnFoldBtnImgChevron.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/new-icons/unfold-chevron.png"));
                    LeftUnFoldBtnImgChevron.Width = 14;
                    LeftUnFoldBtnImgChevron.Height = 14;
                    LeftUnFoldBtnImgChevron.RenderTransform = null;
                } else if (Settings.Appearance.UnFoldButtonImageType ==1)
                {
                    RightUnFoldBtnImgChevron.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/new-icons/pen-white.png"));
                    RightUnFoldBtnImgChevron.Width = 18;
                    RightUnFoldBtnImgChevron.Height = 18;
                    RightUnFoldBtnImgChevron.RenderTransform = null;
                    LeftUnFoldBtnImgChevron.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/new-icons/pen-white.png"));
                    LeftUnFoldBtnImgChevron.Width = 18;
                    LeftUnFoldBtnImgChevron.Height = 18;
                    LeftUnFoldBtnImgChevron.RenderTransform = null;
                }

                if (Settings.Appearance.IsShowQuickPanel)
                {
                    ToggleSwitchEnableQuickPanel.IsOn = true;
                } else
                {
                    ToggleSwitchEnableQuickPanel.IsOn = false;
                }
                if (Settings.Appearance.EnableViewboxBlackBoardScaleTransform) // 画板 UI 缩放 80%
                {
                    ViewboxBlackboardLeftSideScaleTransform.ScaleX = 0.8;
                    ViewboxBlackboardLeftSideScaleTransform.ScaleY = 0.8;
                    ViewboxBlackboardCenterSideScaleTransform.ScaleX = 0.8;
                    ViewboxBlackboardCenterSideScaleTransform.ScaleY = 0.8;
                    ViewboxBlackboardRightSideScaleTransform.ScaleX = 0.8;
                    ViewboxBlackboardRightSideScaleTransform.ScaleY = 0.8;

                    ToggleSwitchEnableViewboxBlackBoardScaleTransform.IsOn = true;
                } else {
                    ViewboxBlackboardLeftSideScaleTransform.ScaleX = 1;
                    ViewboxBlackboardLeftSideScaleTransform.ScaleY = 1;
                    ViewboxBlackboardCenterSideScaleTransform.ScaleX = 1;
                    ViewboxBlackboardCenterSideScaleTransform.ScaleY = 1;
                    ViewboxBlackboardRightSideScaleTransform.ScaleX = 1;
                    ViewboxBlackboardRightSideScaleTransform.ScaleY = 1;

                    ToggleSwitchEnableViewboxBlackBoardScaleTransform.IsOn = false;
                }
                if (Settings.Appearance.IsTransparentButtonBackground) {
                    BtnExit.Background = new SolidColorBrush(StringToColor("#7F909090"));
                } else {
                    if (BtnSwitchTheme.Content.ToString() == "深色") {
                        //Light
                        BtnExit.Background = new SolidColorBrush(StringToColor("#FFCCCCCC"));
                    } else {
                        //Dark
                        BtnExit.Background = new SolidColorBrush(StringToColor("#FF555555"));
                    }
                }
                if (Settings.Appearance.EnableTimeDisplayInWhiteboardMode==true)
                {
                    ToggleSwitchEnableTimeDisplayInWhiteboardMode.IsOn = true;
                } else
                {
                    ToggleSwitchEnableTimeDisplayInWhiteboardMode.IsOn = false;
                }
                SystemEvents_UserPreferenceChanged(null, null);
            } else {
                Settings.Appearance = new Appearance();
            }
            // PowerPointSettings
            if (Settings.PowerPointSettings != null) {
                PptNavigationBtn.Visibility = Settings.PowerPointSettings.IsShowPPTNavigation ? Visibility.Visible : Visibility.Collapsed;
                ToggleSwitchShowButtonPPTNavigation.IsOn = Settings.PowerPointSettings.IsShowPPTNavigation;
                ToggleSwitchShowBottomPPTNavigationPanel.IsOn = Settings.PowerPointSettings.IsShowBottomPPTNavigationPanel;
                ToggleSwitchShowSidePPTNavigationPanel.IsOn = Settings.PowerPointSettings.IsShowSidePPTNavigationPanel;
                if (Settings.PowerPointSettings.PowerPointSupport) {
                    ToggleSwitchSupportPowerPoint.IsOn = true;
                    timerCheckPPT.Start();
                } else {
                    ToggleSwitchSupportPowerPoint.IsOn = false;
                    timerCheckPPT.Stop();
                }
                if (Settings.PowerPointSettings.IsShowCanvasAtNewSlideShow) {
                    ToggleSwitchShowCanvasAtNewSlideShow.IsOn = true;
                } else {
                    ToggleSwitchShowCanvasAtNewSlideShow.IsOn = false;
                }
                if (Settings.PowerPointSettings.IsEnableTwoFingerGestureInPresentationMode) {
                    ToggleSwitchEnableTwoFingerGestureInPresentationMode.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerGestureInPresentationMode.IsOn = false;
                }
                if (Settings.PowerPointSettings.IsEnableFingerGestureSlideShowControl) {
                    ToggleSwitchEnableFingerGestureSlideShowControl.IsOn = true;
                } else {
                    ToggleSwitchEnableFingerGestureSlideShowControl.IsOn = false;
                }


                if (Settings.PowerPointSettings.IsAutoSaveStrokesInPowerPoint) {
                    ToggleSwitchAutoSaveStrokesInPowerPoint.IsOn = true;
                } else {
                    ToggleSwitchAutoSaveStrokesInPowerPoint.IsOn = false;
                }

                if (Settings.PowerPointSettings.IsNotifyPreviousPage) {
                    ToggleSwitchNotifyPreviousPage.IsOn = true;
                } else {
                    ToggleSwitchNotifyPreviousPage.IsOn = false;
                }

                if (Settings.PowerPointSettings.IsNotifyHiddenPage) {
                    ToggleSwitchNotifyHiddenPage.IsOn = true;
                } else {
                    ToggleSwitchNotifyHiddenPage.IsOn = false;
                }

                if (Settings.PowerPointSettings.IsNotifyAutoPlayPresentation)
                {
                    ToggleSwitchNotifyAutoPlayPresentation.IsOn = true;
                }
                else
                {
                    ToggleSwitchNotifyAutoPlayPresentation.IsOn = false;
                }
                if (Settings.PowerPointSettings.IsSupportWPS) {
                    ToggleSwitchSupportWPS.IsOn = true;
                } else {
                    ToggleSwitchSupportWPS.IsOn = false;
                }
                if (Settings.PowerPointSettings.IsAutoSaveScreenShotInPowerPoint) {
                    ToggleSwitchAutoSaveScreenShotInPowerPoint.IsOn = true;
                } else {
                    ToggleSwitchAutoSaveScreenShotInPowerPoint.IsOn = false;
                }
            } else {
                Settings.PowerPointSettings = new PowerPointSettings();
            }
            // Gesture
            if (Settings.Gesture != null) {
                if (Settings.Gesture.IsEnableMultiTouchMode) {
                    ToggleSwitchEnableMultiTouchMode.IsOn = true;
                } else {
                    ToggleSwitchEnableMultiTouchMode.IsOn = false;
                }
                if (Settings.Gesture.IsEnableTwoFingerZoom) {
                    ToggleSwitchEnableTwoFingerZoom.IsOn = true;
                    BoardToggleSwitchEnableTwoFingerZoom.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerZoom.IsOn = false;
                    BoardToggleSwitchEnableTwoFingerZoom.IsOn = false;
                }
                if (Settings.Gesture.IsEnableTwoFingerTranslate) {
                    ToggleSwitchEnableTwoFingerTranslate.IsOn = true;
                    BoardToggleSwitchEnableTwoFingerTranslate.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerTranslate.IsOn = false;
                    BoardToggleSwitchEnableTwoFingerTranslate.IsOn = false;
                }
                if (Settings.Gesture.IsEnableTwoFingerRotation) {
                    ToggleSwitchEnableTwoFingerRotation.IsOn = true;
                    BoardToggleSwitchEnableTwoFingerRotation.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerRotation.IsOn = false;
                    BoardToggleSwitchEnableTwoFingerRotation.IsOn = false;
                }
                if (Settings.Gesture.AutoSwitchTwoFingerGesture) {
                    ToggleSwitchAutoSwitchTwoFingerGesture.IsOn = true;
                } else {
                    ToggleSwitchAutoSwitchTwoFingerGesture.IsOn = false;
                }
                if (Settings.Gesture.IsEnableTwoFingerRotation) {
                    ToggleSwitchEnableTwoFingerRotation.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerRotation.IsOn = false;
                }
                if (Settings.Gesture.IsEnableTwoFingerRotationOnSelection) {
                    ToggleSwitchEnableTwoFingerRotationOnSelection.IsOn = true;
                } else {
                    ToggleSwitchEnableTwoFingerRotationOnSelection.IsOn = false;
                }
                if (Settings.Gesture.AutoSwitchTwoFingerGesture) {
                    if (Topmost) {
                        ToggleSwitchEnableTwoFingerTranslate.IsOn = false;
                        BoardToggleSwitchEnableTwoFingerTranslate.IsOn = false;
                        Settings.Gesture.IsEnableTwoFingerTranslate = false;
                        if (!isInMultiTouchMode) ToggleSwitchEnableMultiTouchMode.IsOn = true;
                    } else {
                        ToggleSwitchEnableTwoFingerTranslate.IsOn = true;
                        BoardToggleSwitchEnableTwoFingerTranslate.IsOn = true;
                        Settings.Gesture.IsEnableTwoFingerTranslate = true;
                        if (isInMultiTouchMode) ToggleSwitchEnableMultiTouchMode.IsOn = false;
                    }
                }
                CheckEnableTwoFingerGestureBtnColorPrompt();
            } else {
                Settings.Gesture = new Gesture();
            }
            // Canvas
            if (Settings.Canvas != null) {
                drawingAttributes.Height = Settings.Canvas.InkWidth;
                drawingAttributes.Width = Settings.Canvas.InkWidth;

                InkWidthSlider.Value = Settings.Canvas.InkWidth * 2;
                HighlighterWidthSlider.Value = Settings.Canvas.HighlighterWidth;

                ComboBoxHyperbolaAsymptoteOption.SelectedIndex = (int)Settings.Canvas.HyperbolaAsymptoteOption;

                if (Settings.Canvas.UsingWhiteboard) {
                    GridBackgroundCover.Background = new SolidColorBrush(StringToColor("#FFF2F2F2"));
                } else {
                    GridBackgroundCover.Background = new SolidColorBrush(StringToColor("#FF1F1F1F"));
                }

                if (Settings.Canvas.IsShowCursor) {
                    ToggleSwitchShowCursor.IsOn = true;
                    inkCanvas.ForceCursor = true;
                } else {
                    ToggleSwitchShowCursor.IsOn = false;
                    inkCanvas.ForceCursor = false;
                }

                ComboBoxPenStyle.SelectedIndex = Settings.Canvas.InkStyle;
                BoardComboBoxPenStyle.SelectedIndex = Settings.Canvas.InkStyle;

                ComboBoxEraserSize.SelectedIndex = Settings.Canvas.EraserSize;
                ComboBoxEraserSizeFloatingBar.SelectedIndex = Settings.Canvas.EraserSize;

                if (Settings.Canvas.ClearCanvasAndClearTimeMachine==true) {
                    ToggleSwitchClearCanvasAndClearTimeMachine.IsOn = true;
                } else
                {
                    ToggleSwitchClearCanvasAndClearTimeMachine.IsOn = false;
                }

                if (Settings.Canvas.EraserShapeType==0)
                {
                    double k = 1;
                    switch (Settings.Canvas.EraserSize)
                    {
                        case 0:
                            k = 0.5;
                            break;
                        case 1:
                            k = 0.8;
                            break;
                        case 3:
                            k = 1.25;
                            break;
                        case 4:
                            k = 1.8;
                            break;
                    }
                    inkCanvas.EraserShape = new EllipseStylusShape(k * 90, k * 90);
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                    
                } else if (Settings.Canvas.EraserShapeType == 1)
                {
                    double k = 1;
                    switch (Settings.Canvas.EraserSize)
                    {
                        case 0:
                            k = 0.7;
                            break;
                        case 1:
                            k = 0.9;
                            break;
                        case 3:
                            k = 1.2;
                            break;
                        case 4:
                            k = 1.6;
                            break;
                    }
                    inkCanvas.EraserShape = new RectangleStylusShape(k * 90 * 0.6, k * 90);
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                }

                CheckEraserTypeTab();

                if (Settings.Canvas.HideStrokeWhenSelecting) {
                    ToggleSwitchHideStrokeWhenSelecting.IsOn = true;
                } else {
                    ToggleSwitchHideStrokeWhenSelecting.IsOn = false;
                }
                if (Settings.Canvas.FitToCurve)
                {
                    ToggleSwitchFitToCurve.IsOn = true;
                    drawingAttributes.FitToCurve = true;
                }
                else
                {
                    ToggleSwitchFitToCurve.IsOn = false;
                    drawingAttributes.FitToCurve = false;
                }
            } else {
                Settings.Canvas = new Canvas();
            }
            // Advanced
            if (Settings.Advanced != null) {
                TouchMultiplierSlider.Value = Settings.Advanced.TouchMultiplier;
                FingerModeBoundsWidthSlider.Value = Settings.Advanced.FingerModeBoundsWidth;
                NibModeBoundsWidthSlider.Value = Settings.Advanced.NibModeBoundsWidth;
                if (Settings.Advanced.IsLogEnabled) {
                    ToggleSwitchIsLogEnabled.IsOn = true;
                } else {
                    ToggleSwitchIsLogEnabled.IsOn = false;
                }
                if (Settings.Advanced.IsSecondConfimeWhenShutdownApp) {
                    ToggleSwitchIsSecondConfimeWhenShutdownApp.IsOn = true;
                } else {
                    ToggleSwitchIsSecondConfimeWhenShutdownApp.IsOn = false;
                }
                if (Settings.Advanced.EraserBindTouchMultiplier) {
                    ToggleSwitchEraserBindTouchMultiplier.IsOn = true;
                } else {
                    ToggleSwitchEraserBindTouchMultiplier.IsOn = false;
                }

                if (Settings.Advanced.IsSpecialScreen) {
                    ToggleSwitchIsSpecialScreen.IsOn = true;
                } else {
                    ToggleSwitchIsSpecialScreen.IsOn = false;
                }
                TouchMultiplierSlider.Visibility = ToggleSwitchIsSpecialScreen.IsOn ? Visibility.Visible : Visibility.Collapsed;

                ToggleSwitchIsQuadIR.IsOn = Settings.Advanced.IsQuadIR;
            } else {
                Settings.Advanced = new Advanced();
            }
            // InkToShape
            if (Settings.InkToShape != null) {
                if (Settings.InkToShape.IsInkToShapeEnabled) {
                    ToggleSwitchEnableInkToShape.IsOn = true;
                } else {
                    ToggleSwitchEnableInkToShape.IsOn = false;
                }
                if (Settings.InkToShape.IsInkToShapeNoFakePressureRectangle)
                {
                    ToggleSwitchEnableInkToShapeNoFakePressureRectangle.IsOn = true;
                }
                else
                {
                    ToggleSwitchEnableInkToShapeNoFakePressureRectangle.IsOn = false;
                }
                if (Settings.InkToShape.IsInkToShapeNoFakePressureTriangle)
                {
                    ToggleSwitchEnableInkToShapeNoFakePressureTriangle.IsOn = true;
                }
                else
                {
                    ToggleSwitchEnableInkToShapeNoFakePressureTriangle.IsOn = false;
                }
                if (Settings.InkToShape.IsInkToShapeTriangle)
                {
                    ToggleCheckboxEnableInkToShapeTriangle.IsChecked = true;
                } else
                {
                    ToggleCheckboxEnableInkToShapeTriangle.IsChecked= false;
                }
                if (Settings.InkToShape.IsInkToShapeRectangle)
                {
                    ToggleCheckboxEnableInkToShapeRectangle.IsChecked = true;
                }
                else
                {
                    ToggleCheckboxEnableInkToShapeRectangle.IsChecked = false;
                }
                if (Settings.InkToShape.IsInkToShapeRounded)
                {
                    ToggleCheckboxEnableInkToShapeRounded.IsChecked = true;
                }
                else
                {
                    ToggleCheckboxEnableInkToShapeRounded.IsChecked = false;
                }
            } else {
                Settings.InkToShape = new InkToShape();
            }
            // RandSettings
            if (Settings.RandSettings != null) {
            } else {
                Settings.RandSettings = new RandSettings();
            }
            // Automation
            if (Settings.Automation != null) {
                StartOrStoptimerCheckAutoFold();
                if (Settings.Automation.IsAutoFoldInEasiNote) {
                    ToggleSwitchAutoFoldInEasiNote.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInEasiNote.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInEasiCamera) {
                    ToggleSwitchAutoFoldInEasiCamera.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInEasiCamera.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInEasiNote3C) {
                    ToggleSwitchAutoFoldInEasiNote3C.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInEasiNote3C.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInEasiNote5C)
                {
                    ToggleSwitchAutoFoldInEasiNote5C.IsOn = true;
                }
                else
                {
                    ToggleSwitchAutoFoldInEasiNote5C.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInSeewoPincoTeacher) {
                    ToggleSwitchAutoFoldInSeewoPincoTeacher.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInSeewoPincoTeacher.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInHiteTouchPro) {
                    ToggleSwitchAutoFoldInHiteTouchPro.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInHiteTouchPro.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInHiteCamera) {
                    ToggleSwitchAutoFoldInHiteCamera.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInHiteCamera.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInWxBoardMain) {
                    ToggleSwitchAutoFoldInWxBoardMain.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInWxBoardMain.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInOldZyBoard) {
                    ToggleSwitchAutoFoldInOldZyBoard.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInOldZyBoard.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInMSWhiteboard) {
                    ToggleSwitchAutoFoldInMSWhiteboard.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInMSWhiteboard.IsOn = false;
                }
                if (Settings.Automation.IsAutoFoldInPPTSlideShow) {
                    ToggleSwitchAutoFoldInPPTSlideShow.IsOn = true;
                } else {
                    ToggleSwitchAutoFoldInPPTSlideShow.IsOn = false;
                }
                if (Settings.Automation.IsAutoKillEasiNote || Settings.Automation.IsAutoKillPptService) {
                    timerKillProcess.Start();
                } else {
                    timerKillProcess.Stop();
                }

                if (Settings.Automation.IsAutoKillEasiNote) {
                    ToggleSwitchAutoKillEasiNote.IsOn = true;
                } else {
                    ToggleSwitchAutoKillEasiNote.IsOn = false;
                }
                if (Settings.Automation.IsAutoKillPptService) {
                    ToggleSwitchAutoKillPptService.IsOn = true;
                } else {
                    ToggleSwitchAutoKillPptService.IsOn = false;
                }
                if (Settings.Automation.IsAutoSaveStrokesAtClear) {
                    ToggleSwitchAutoSaveStrokesAtClear.IsOn = true;
                } else {
                    ToggleSwitchAutoSaveStrokesAtClear.IsOn = false;
                }
                if (Settings.Automation.IsSaveScreenshotsInDateFolders) {
                    ToggleSwitchSaveScreenshotsInDateFolders.IsOn = true;
                } else {
                    ToggleSwitchSaveScreenshotsInDateFolders.IsOn = false;
                }
                if (Settings.Automation.IsAutoSaveStrokesAtScreenshot) {
                    ToggleSwitchAutoSaveStrokesAtScreenshot.IsOn = true;
                } else {
                    ToggleSwitchAutoSaveStrokesAtScreenshot.IsOn = false;
                }
                SideControlMinimumAutomationSlider.Value = Settings.Automation.MinimumAutomationStrokeNumber;

                AutoSavedStrokesLocation.Text = Settings.Automation.AutoSavedStrokesLocation;
                ToggleSwitchAutoDelSavedFiles.IsOn = Settings.Automation.AutoDelSavedFiles;
                ComboBoxAutoDelSavedFilesDaysThreshold.Text = Settings.Automation.AutoDelSavedFilesDaysThreshold.ToString();
            } else {
                Settings.Automation = new Automation();
            }
            // auto align
            if (BtnPPTSlideShowEnd.Visibility == Visibility.Visible) {
                ViewboxFloatingBarMarginAnimation(60);
            } else {
                ViewboxFloatingBarMarginAnimation(100, true);
            }
        }
    }
}