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

namespace UIUpdateServiceProject
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
        }

        private void TabItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //버튼 상태가 true일 때 이벤트를 처리하지 않음
            if (btnState.IsChecked == true)
            {
                e.Handled = true;
                return;
            }

            //선택 된 TabItem에 따른 ViewModel SelectedIndex 업데이트
            var tabItem = sender as TabItem;
            mainWindowViewModel.UpdateSelectedIndex(tabItem.Header.ToString());

        }
    }
}
