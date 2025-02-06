using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIUpdateServiceProject
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool _isButtonEnabled;
        public byte SelectedIndex { get; private set; }
        public bool IsButtonEnabled
        {
            get => _isButtonEnabled;
            set
            {
                _isButtonEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsButtonEnabled)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
        }

        public void UpdateSelectedIndex(string header)
        {
            switch (header)
            {
                case "A":
                    SelectedIndex = 0;
                    break;
                case "B":
                    SelectedIndex = 1;
                    break;
                case "C":
                    SelectedIndex = 2;
                    break;
            }
        }
    }
}
