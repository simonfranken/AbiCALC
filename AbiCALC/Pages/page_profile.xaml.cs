﻿using System;
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

namespace AbiCALC.Pages
{
    /// <summary>
    /// Interaction logic for page_profile.xaml
    /// </summary>
    public partial class page_profile : Page
    {
        public page_profile()
        {
            InitializeComponent();
            DataContext = this;
        }
        public string getName 
        {
            get 
            {
                return $"Hallo {database.current.name}!";
            }
        }
    }
}
