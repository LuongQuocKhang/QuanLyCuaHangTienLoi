﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Quan_Ly_Ban_Hang.View.Quản_lý_hóa_đơn_bán_hàng
{
    /// <summary>
    /// Interaction logic for QuanLyHoaDonBH.xaml
    /// </summary>
    public partial class QuanLyHoaDonBH : Window
    {
        public QuanLyHoaDonBH()
        {
            InitializeComponent();
        }

        private void txbSoluong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
