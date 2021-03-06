﻿using MahApps.Metro.Controls;
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
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using Caliburn.Micro;
using SlavApp.Minion.Plugin;
using System.Threading;
using SlavApp.Minion.ViewModels; 

namespace SlavApp.Minion.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : MetroWindow
    {
        public MainWindowView()
        {
            InitializeComponent();
        }

        public Task<ProgressDialogController> OpenProgress()
        {
            return this.ShowProgressAsync("Please wait...", string.Empty);
        }
    }
}
