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
using System.Windows.Shapes;


namespace SIMS_Booking.UI.View.Guide
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : UserControl
    {
        public CreateTour()
        {
            InitializeComponent();
        }

































        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
                    
            if (!int.TryParse(textBox.Text, out int result))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;
            
        }

        private void TextBox_LostFocusINT(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (int.TryParse(textBox.Text, out int result))
                textBox.Background = Brushes.White;
            else
                textBox.Background = Brushes.Red;

        }
    }
}
