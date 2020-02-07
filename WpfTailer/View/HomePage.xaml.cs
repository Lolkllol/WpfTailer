using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core.ViewModels;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;

namespace WpfTailer.View
{
    [MvxViewFor(typeof(HomePageViewModel))]
    public partial class HomePage : MvxWpfView
    {
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
