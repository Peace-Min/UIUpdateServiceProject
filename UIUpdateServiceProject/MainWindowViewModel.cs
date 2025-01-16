using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIUpdateServiceProject
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public string Text { get; set; } = "Hello, World!";
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            Task.Run(() =>
            {
                Task.Delay(1000).Wait();

                UIUpdateService.Instance.UpdateField(this, nameof(Text), System.Windows.Media.Brushes.Red);
            });
        }
    }
}
