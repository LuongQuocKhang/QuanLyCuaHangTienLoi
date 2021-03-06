﻿using Quan_Ly_Ban_Hang.Model;
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
    class DataContextQLNV : BaseViewModel
    {
        #region list 
        public ObservableCollection<NHANVIEN> ListNhanVien { get; set; }
        public ObservableCollection<string> ListLoaiNV { get; set; }
        public string manv { get; set; }
        #endregion
        #region command

        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        #endregion

        public DataContextQLNV()
        {
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }
        public void LoadInfo()
        {
            ListLoaiNV = Load.Instance.Load_Thong_Tin_LoaiNV();
            ListNhanVien = Load.Instance.Load_Thong_Tin_Nhan_Vien();
            manv = PrimaryKey.Instance.CreatePrimaryKey("NHANVIEN", "NV", 1);
        }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            AddCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                string tennv = "";
                string loainv = "";

                foreach (var item in p.Children)
                {

                    if (item is TextBox)
                    {
                        TextBox tb = item as TextBox;
                        switch (tb.Name)
                        {
                            case "txbTenNV":
                                {
                                    tennv = tb.Text;
                                }
                                break;
                        }
                    }
                    if (item is ComboBox)
                    {
                        ComboBox cb = item as ComboBox;
                        switch (cb.Name)
                        {
                            case "cbLoaiNV":
                                {
                                    loainv = cb.Text;
                                }
                                break;
                        }

                    }
                }
                if (string.IsNullOrEmpty(tennv) || string.IsNullOrEmpty(loainv))
                {
                    MessageBox.Show("Nhâp đầy đủ thông tin nhân viên");
                    return;
                }
                int MaloaiNV = DataProvider.Instance.DB.LOAINHANVIENs.Where(x => x.TENLOAINV == loainv).SingleOrDefault().MALOAINV;
                NHANVIEN nhanvien = new NHANVIEN()
                {
                    TENNHANVIEN = tennv,
                    MANHANVIEN = manv,
                    MALOAINV = MaloaiNV
                };
                ListNhanVien.Add(nhanvien);
                Insert.Instance.ThemThongTinNhanVien(nhanvien);
                MessageBox.Show("Thêm thành công");
            });
            DeleteCommand = new RelayCommand<ListView>((p) => true, (p) =>
            {
                try
                {
                    int selectedindex = (p as ListView).SelectedIndex;
                    if ( selectedindex >= 0 )
                    {
                        string manv = ListNhanVien[selectedindex].MANHANVIEN;
                        if (DataProvider.Instance.DB.HOADONBHs.Where(x => x.MANHANVIEN == manv).ToList().Count == 0)
                        {
                            if (Delete.Instance.XoaThongTinNhanVien(ListNhanVien[selectedindex]) == true)
                            {
                                ListNhanVien.RemoveAt(selectedindex);
                                MessageBox.Show("Xóa nhân viên thành công");
                            }
                            else
                            {
                                MessageBox.Show("Xóa tài khoản trước khi xóa nhân viên");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa nhân viên", "CẢNH BÁO");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn nhân viên cần xóa;");
                        return;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("NHÂN VIÊN ĐÃ CÓ TÀI KHOẢN " + "\n" + "XÓA TÀI KHOẢN TRƯỚC KHI XÓA NHÂN VIÊN", "CẢNH BÁO");
                }

            });

            RefreshCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {

                manv = PrimaryKey.Instance.CreatePrimaryKey("NHANVIEN", "NV", 1);
                foreach (var item in p.Children)
                {

                    if (item is TextBox)
                    {
                        TextBox tb = item as TextBox;
                        switch (tb.Name)
                        {
                            case "txbMaNV":
                                {
                                    tb.Text = manv;
                                }
                                break;
                            case "txbTenNV":
                                {
                                    tb.Text = "";
                                }
                                break;
                        }
                    }
                    if (item is ComboBox)
                    {
                        ComboBox cb = item as ComboBox;
                        switch (cb.Name)
                        {
                            case "cbLoaiNV":
                                {
                                    cb.Text = "";
                                }
                                break;
                        }

                    }
                }
            });
        }
    }
}