﻿using EntityFremworkImtahan.ViewsModels;
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

namespace EntityFremworkImtahan.Views.Pages
{
    /// <summary>
    /// Interaction logic for GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        public GuestPage()
        {

            InitializeComponent();
            DataContext = new GuestPageBackEnd();
        }
    }
}
