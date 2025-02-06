using System.Windows;
using System.Windows.Controls;

namespace UIUpdateServiceProject
{
    public static class TabControlHelper
    {
        public static readonly DependencyProperty IsTabSelectionEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsTabSelectionEnabled",
                typeof(bool),
                typeof(TabControlHelper),
                new PropertyMetadata(true, OnIsTabSelectionEnabledChanged));

        public static readonly DependencyProperty PreviousSelectedIndexProperty =
            DependencyProperty.RegisterAttached(
                "PreviousSelectedIndex",
                typeof(int),
                typeof(TabControlHelper),
                new PropertyMetadata(0));

        public static bool GetIsTabSelectionEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTabSelectionEnabledProperty);
        }

        public static void SetIsTabSelectionEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTabSelectionEnabledProperty, value);
        }

        public static int GetPreviousSelectedIndex(DependencyObject obj)
        {
            return (int)obj.GetValue(PreviousSelectedIndexProperty);
        }

        public static void SetPreviousSelectedIndex(DependencyObject obj, int value)
        {
            obj.SetValue(PreviousSelectedIndexProperty, value);
        }

        private static void OnIsTabSelectionEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TabControl tabControl)
            {
                //if ((bool)e.NewValue)
                //{
                //    tabControl.SelectionChanged -= TabControl_SelectionChanged;
                //}
                //else
                //{
                    tabControl.SelectionChanged += TabControl_SelectionChanged;
                //}
            }
        }

        private static void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                bool isSelectionEnabled = GetIsTabSelectionEnabled(tabControl);
                if (isSelectionEnabled)
                {
                    // 강제로 이전 Index로 복원
                    int previousIndex = GetPreviousSelectedIndex(tabControl);
                    tabControl.SelectedIndex = previousIndex; // **이전 Index로 되돌림**
                    e.Handled = true;
                    return;
                }
                else
                {
                    // 새로운 선택된 인덱스를 저장 (변경 허용된 경우)
                    SetPreviousSelectedIndex(tabControl, tabControl.SelectedIndex);
                }
            }
        }
    }
}