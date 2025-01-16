using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;

namespace UIUpdateServiceProject
{
    public class UIUpdateService
    {
        private static UIUpdateService _instance;

        // 싱글톤 인스턴스에 대한 락 오브젝트
        private static readonly object _lock = new object();

        private readonly Dictionary<INotifyPropertyChanged, FrameworkElement> _viewMappings = new Dictionary<INotifyPropertyChanged, FrameworkElement>();

        public static UIUpdateService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new UIUpdateService();
                        }
                    }
                }

                return _instance;
            }
        }

        private UIUpdateService() { }

        public void RegisterView(FrameworkElement view)
        {
            view.Loaded += (s, e) =>
            {
                if (view.DataContext is INotifyPropertyChanged viewModel && !_viewMappings.ContainsKey(viewModel))
                {
                    _viewMappings.Add(viewModel, view);
                }
            };
        }

        public void UpdateField(INotifyPropertyChanged viewModel, string fieldName, Brush color)
        {
            if (_viewMappings.TryGetValue(viewModel, out var view))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    var targetElement = FindElementByBindingPath(view, fieldName);
                    if (targetElement is TextBox textBox)
                    {
                        textBox.Background = color;
                    }
                });
            }
        }

        private FrameworkElement FindElementByBindingPath(DependencyObject parent, string bindingPath)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement element)
                {
                    var bindingExpression = element.GetBindingExpression(TextBox.TextProperty);
                    if (bindingExpression != null && bindingExpression.ParentBinding.Path.Path == bindingPath)
                    {
                        return element;
                    }
                }

                var result = FindElementByBindingPath(child, bindingPath);
                if (result != null)
                    return result;
            }

            return null;
        }
    }
}
