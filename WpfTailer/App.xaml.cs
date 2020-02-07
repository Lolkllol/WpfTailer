using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.Platforms.Wpf.Views;

namespace WpfTailer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        public App()
        {
            this.RegisterSetupType<MvxWpfSetup<Core.App>>();
        }
    }
}
