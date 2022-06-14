using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using SerpAnalysis.wpf.ViewModels;

namespace SerpAnalysis.Wpf.Views
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

        bool _shown;

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (_shown)
                return;

            _shown = true;

            // Your code here.
            TxtKeyword.SelectAll();
            TxtKeyword.Focus();
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void EllipseMax_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Maximized;
        }

        private void EllipseMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void EllipseClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                EllipseMax_MouseDown(this, null);
            }
        }
    }
}
