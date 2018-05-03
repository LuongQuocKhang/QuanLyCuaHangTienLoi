using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    class DataContextQLTK:BaseViewModel 
    {
        #region list 
        public ObservableCollection<NHANVIEN> ListMaNV { get; set; }
        public ObservableCollection<TAIKHOAN> ListTaiKhoan { get; set; }

        #endregion
        #region command
   
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
       
        #endregion

        public DataContextQLTK()
        {
            LoadInfo();
            Command();
        }
        public void LoadInfo()
        {
            ListMaNV = Load.Instance.Load_Thong_Tin_MaNV();
            ListTaiKhoan = Load.Instance.Load_Thong_Tin_Tai_Khoan();
        }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            AddCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                string tentaikhoan = "";
                string matkhau = "";
                string manv = "";

                foreach (var item in p.Children)
                {

                    if (item is TextBox)
                    {
                        TextBox tb = item as TextBox;
                        switch (tb.Name)
                        {
                            case "txbTaiKhoan":
                                {
                                    tentaikhoan = tb.Text;
                                }
                                break;
                            case "txbMatKhau":
                                {
                                    matkhau = tb.Text;
                                }
                                break;
                        }
                    }
                    if(item is ComboBox)
                    {
                        ComboBox cb = item as ComboBox;
                        switch(cb.Name)
                        {
                            case "cbMaNV":
                                {
                                    manv = cb.Text;
                                }
                                break;
                        }

                    }
                }
                if (string.IsNullOrEmpty(tentaikhoan) || string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(manv)) return;

                TAIKHOAN taikhoan = new TAIKHOAN()
                {
                    TAIKHOAN1 = tentaikhoan,
                    MATKHAU = matkhau,
                    MANHANVIEN = manv,
                };
                ListTaiKhoan.Add(taikhoan);
                Insert.Instance.ThemThongTinTaiKhoan(taikhoan);
            });
            DeleteCommand = new RelayCommand<ListView>((p) => true, (p) =>
            {
                int selectedindex = (p as ListView).SelectedIndex;
                Delete.Instance.XoaThongTinTaiKhoan(ListTaiKhoan[selectedindex]);
                ListTaiKhoan.RemoveAt(selectedindex);
            });
        }
        }
}
