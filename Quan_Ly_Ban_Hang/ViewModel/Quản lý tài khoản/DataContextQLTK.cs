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
        private string matKhau;
        public string MatKhau { get { return matKhau; } set { if (matKhau != value){ matKhau = value;OnPropertyChanged(); }}}
        #region list 
        public ObservableCollection<NHANVIEN> ListMaNV { get; set; }
        public ObservableCollection<TAIKHOAN> ListTaiKhoan { get; set; }

        #endregion
        #region command

        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        #endregion

        public DataContextQLTK()
        {
            LoadInfo();
            Command();
        }
        public void LoadInfo()
        {
            ListMaNV = Load.Instance.Load_Thong_Tin_Nhan_Vien();
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
                if (DataProvider.Instance.DB.TAIKHOANs.Where(x => x.TAIKHOAN1.ToLower().Contains(tentaikhoan.ToLower())).ToList().Count > 0)
                {
                    MessageBox.Show("Tài khoản đã tồn tại");
                    return;
                }
                if (DataProvider.Instance.DB.TAIKHOANs.Where(x => x.MANHANVIEN == manv).ToList().Count > 0)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại");
                    return;
                }
                TAIKHOAN taikhoan = new TAIKHOAN()
                {
                    TAIKHOAN1 = tentaikhoan,
                    MATKHAU = Encryptor.EncryptData(matkhau),
                    MANHANVIEN = manv,
                };
                ListTaiKhoan.Add(taikhoan);
                Insert.Instance.ThemThongTinTaiKhoan(taikhoan);
            });
            DeleteCommand = new RelayCommand<ListView>((p) => true, (p) =>
            {
                int selectedindex = (p as ListView).SelectedIndex;
                if ( selectedindex >= 0)
                {
                    string manv = ListTaiKhoan[selectedindex].MANHANVIEN;
                    if (DataProvider.Instance.DB.HOADONBHs.Where(x => x.MANHANVIEN == manv).ToList().Count == 0)
                    {
                        Delete.Instance.XoaThongTinTaiKhoan(ListTaiKhoan[selectedindex]);
                        ListTaiKhoan.RemoveAt(selectedindex);
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa tài khoản");
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn nhân viên");
                }
            });
            ShowPasswordCommand = new RelayCommand<ListView>((p) => true, (p) =>
            {
                int index = (p as ListView).SelectedIndex;
                if(index >= 0)
                {
                    string matkhau = ListTaiKhoan[index].MATKHAU;
                    MatKhau = Encryptor.DecryptString(matkhau);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn tài khoản");
                }
            });
        }
    }
}
