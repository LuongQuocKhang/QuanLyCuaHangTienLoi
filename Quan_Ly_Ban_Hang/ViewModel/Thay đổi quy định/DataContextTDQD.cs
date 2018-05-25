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
    class DataContextTDQD : BaseViewModel
    {
        public THAMSO QuiDinh { get; set; }
        private string giatricuSLNTT;
        private string giatricuSLTTDDN;
        private string giatricuSLTTTDB;
        private string giatricuNgayXoa;

        public string GiatricuSLNTT { get => giatricuSLNTT; set { if (giatricuSLNTT != value) { giatricuSLNTT = value; OnPropertyChanged(); } } }
        public string GiatricuSLTTDDN { get => giatricuSLTTDDN; set { if (giatricuSLTTDDN != value) { giatricuSLTTDDN = value; OnPropertyChanged(); } } }
        public string GiatricuSLTTTDB { get => giatricuSLTTTDB; set { if (giatricuSLTTTDB != value) { giatricuSLTTTDB = value; OnPropertyChanged(); } } }
        public string GiatricuNgayXoa { get => giatricuNgayXoa; set { if (giatricuNgayXoa != value) { giatricuNgayXoa = value; OnPropertyChanged(); } } }

        #region command

        public ICommand UpdateCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand RefreshCommand { get; set; }


        #endregion

        public DataContextTDQD()
        {
            LoadInfo();
            Command();
        }
        public void LoadInfo()
        {
            QuiDinh = Load.Instance.LoadQuiDinh();
            LoadQuiDinh();
        }
        public void LoadQuiDinh()
        {
            GiatricuSLNTT = QuiDinh.SOLUONGNHAPTOITHIEU.Value.ToString();
            GiatricuSLTTDDN = QuiDinh.SOLUONGTONTOIDADUOCNHAP.Value.ToString();
            GiatricuSLTTTDB = QuiDinh.SOLUONGTONTOITHIEUDUOCBAN.Value.ToString();
            GiatricuNgayXoa = QuiDinh.THOIGIANXOADULIEU.Value.ToString();
        }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            UpdateCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                try
                {
                    int slNTT = Int32.Parse(GiatricuSLNTT);
                    int slTTDDN = Int32.Parse(GiatricuSLTTDDN);
                    int slTTTDB = Int32.Parse(GiatricuSLTTTDB);
                    int Thoigianxoa = Int32.Parse(GiatricuNgayXoa);

                    foreach (var item in p.Children)
                    {

                        if (item is TextBox)
                        {
                            TextBox tb = item as TextBox;
                            switch (tb.Name)
                            {
                                case "txbSLNTTmoi":
                                    {
                                        if (!string.IsNullOrEmpty(tb.Text.ToString()))
                                        {
                                            slNTT = Int32.Parse(tb.Text.ToString());
                                        }
                                    }
                                    break;
                                case "txbSLTTDDNmoi":
                                    {
                                        if (!string.IsNullOrEmpty(tb.Text.ToString()))
                                        {
                                            slTTDDN = Int32.Parse(tb.Text.ToString());
                                        }
                                    }
                                    break;
                                case "txbSLTTTDBmoi":
                                    {
                                        if (!string.IsNullOrEmpty(tb.Text.ToString()))
                                        {
                                            slTTTDB = Int32.Parse(tb.Text.ToString());
                                        }
                                    }
                                    break;
                                case "txbThoiGianXoaMoi":
                                    {
                                        if (!string.IsNullOrEmpty(tb.Text.ToString()))
                                        {
                                            Thoigianxoa = Int32.Parse(tb.Text.ToString());
                                        }
                                    }
                                    break;
                            }
                        }
                    }

                    THAMSO thamso = new THAMSO()
                    {
                        SOLUONGNHAPTOITHIEU = slNTT,
                        SOLUONGTONTOIDADUOCNHAP = slTTDDN,
                        SOLUONGTONTOITHIEUDUOCBAN = slTTTDB,
                        THOIGIANXOADULIEU = Thoigianxoa
                    };

                    Update.Instance.UpdateThamSo(thamso);

                    MessageBox.Show("Cập nhật thành công");

                    LoadQuiDinh();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            });
            RefreshCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                foreach (var item in p.Children)
                {

                    if (item is TextBox)
                    {
                        TextBox tb = item as TextBox;
                        switch (tb.Name)
                        {
                            case "txbSLNTTmoi":
                            case "txbSLTTDDNmoi":
                            case "txbSLTTTDBmoi":
                                {
                                    tb.Text = "";
                                }
                                break;
                            case "txbSLNTTcu":
                                {
                                    tb.Text = QuiDinh.SOLUONGNHAPTOITHIEU.Value.ToString();
                                }
                                break;
                            case "txbSLTTDDNcu":
                                {
                                    tb.Text = QuiDinh.SOLUONGTONTOIDADUOCNHAP.Value.ToString();
                                }
                                break;
                            case "txbSLTTTDBcu":
                                {
                                    tb.Text = QuiDinh.SOLUONGTONTOITHIEUDUOCBAN.Value.ToString();
                                }
                                break;
                            case "txbThoiGianXoaMoi":
                                {
                                    tb.Text = QuiDinh.THOIGIANXOADULIEU.Value.ToString();
                                }
                                break;
                        }
                    }
                }
            });
        }
    }
}

