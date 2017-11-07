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

namespace Sample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _bindingSource;

        public MainWindow()
        {
            InitializeComponent();

            // バインディングソースの設定
            _bindingSource = new ViewModel();
            this.DataContext = _bindingSource;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // 追加処理
        }
    }
}
