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

namespace NumberingSystem
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
<<<<<<< HEAD
        #region インスタンス変数
        private ViewModel.MainViewModel _bindingSource;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            _bindingSource = new ViewModel.MainViewModel();
            this.DataContext = _bindingSource;
=======
        public MainWindow()
        {
            InitializeComponent();
>>>>>>> d5c52a6b0b5a778621fd75d3499ffd2795657b10
        }
    }
}
