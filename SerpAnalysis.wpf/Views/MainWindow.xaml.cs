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
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SerpAnalysis.wpf.ViewModels;

namespace SerpAnalysis.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModelBase Vm { get; }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = Vm = Ioc.Default.GetService<MainViewModel>();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void Ellipse_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Ellipse_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Ellipse_MouseDown(this, null);
            }
        }
    }
}
