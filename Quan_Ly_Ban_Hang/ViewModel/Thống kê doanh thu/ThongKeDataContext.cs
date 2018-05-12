using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel.Thống_kê_doanh_thu
{
    public class ThongKeDataContext : BaseViewModel
    {
        public List<ThongKe> listthongke { get; set; }
        public ObservableCollection<THONGKEDONHANG> ListDonDatHang { get; set; }
        public ObservableCollection<THONGKEHOADON> ListHoaDon { get; set; }
        #region command
        public ICommand MouseDownCommand { get; set; }
        public ICommand CB_Thang_DonDatHang_Command { get; set; }
        public ICommand CB_Thang_HoaDon_Command { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion
        public ThongKeDataContext()
        {
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }

        private void Command()
        {
            MouseDownCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });
            CB_Thang_DonDatHang_Command = new RelayCommand<object>((p) => true, (p) =>
            {
                string thang = ((p as ComboBox).SelectedValue as ComboBoxItem).Content.ToString();
                if (thang.Equals("tất cả"))
                {
                    ListDonDatHang.Clear();
                    var collection = from tk in DataProvider.Instance.DB.THONGKEDONHANGs
                                     join ddh in DataProvider.Instance.DB.DONDATHANGs on tk.MADONDATHANG equals ddh.MADONDATHANG
                                     select tk;
                    foreach (var item in collection)
                    {
                        ListDonDatHang.Add(item);
                    }
                }
                else
                {
                    int Thang  = Int32.Parse(thang);
                    var collection = from tk in DataProvider.Instance.DB.THONGKEDONHANGs
                                     join ddh in DataProvider.Instance.DB.DONDATHANGs on tk.MADONDATHANG equals ddh.MADONDATHANG
                                     where ddh.NGAYDATHANG.Value.Month == Thang
                                     select tk;

                    ListDonDatHang.Clear();

                    foreach (var item in collection)
                    {
                        ListDonDatHang.Add(item);
                    }
                }
                
            });
            CB_Thang_HoaDon_Command = new RelayCommand<object>((p) => true, (p) =>
            {
                string thang = ((p as ComboBox).SelectedValue as ComboBoxItem).Content.ToString();
                if (thang.Equals("tất cả"))
                {
                    ListHoaDon.Clear();
                    var collection = from tk in DataProvider.Instance.DB.THONGKEHOADONs
                                     join hd in DataProvider.Instance.DB.HOADONBHs
                                     on tk.MAHOADONBH equals hd.MAHOADONBH
                                     select tk;
                    foreach (var item in collection)
                    {
                        ListHoaDon.Add(item);
                    }
                }
                else
                {
                    int Thang = Int32.Parse(thang);
                    var collection = from tk in DataProvider.Instance.DB.THONGKEHOADONs
                                     join hd in DataProvider.Instance.DB.HOADONBHs
                                     on tk.MAHOADONBH equals hd.MAHOADONBH
                                     where hd.NGAYLAPHOADON.Value.Month == Thang
                                     select tk;

                    ListHoaDon.Clear();

                    foreach (var item in collection)
                    {
                        ListHoaDon.Add(item);
                    }
                }
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
        }

        public void LoadInfo()
        {
            listthongke = new List<ThongKe>();
            for (int i = 1; i <= 12; i++)
            {
                var thongkenhaphang = from tk in DataProvider.Instance.DB.THONGKEDONHANGs
                                       join ddh in DataProvider.Instance.DB.DONDATHANGs on tk.MADONDATHANG equals ddh.MADONDATHANG
                                       where ddh.NGAYDATHANG.Value.Month == i
                                       select tk;
                float tongtiennhaphang = 0;
                if (thongkenhaphang.Count() > 0)
                {
                    tongtiennhaphang = (float)thongkenhaphang.Sum(x => x.TIENDATHANG).Value / 10000;
                }

                var thongkebanhang = from tk in DataProvider.Instance.DB.THONGKEHOADONs
                                     join hd in DataProvider.Instance.DB.HOADONBHs
                                     on tk.MAHOADONBH equals hd.MAHOADONBH
                                     where hd.NGAYLAPHOADON.Value.Month == i
                                     select tk;

                float tongtienbanhang = 0;
                if (thongkebanhang.Count() > 0)
                {
                    tongtienbanhang = (float)thongkebanhang.Sum(x => x.TIENBANHANG).Value / 10000;
                }
                ThongKe thongke = new ThongKe();
                thongke.Thang = i;
                thongke.TongTienNhapHang = tongtiennhaphang;
                thongke.TongTienBanHang = tongtienbanhang;
                listthongke.Add(thongke);
            }
            ListDonDatHang = new ObservableCollection<THONGKEDONHANG>();
            ListHoaDon = new ObservableCollection<THONGKEHOADON>();
            ListDonDatHang = Load.Instance.LoadThongKeDonHang();
            ListHoaDon = Load.Instance.LoadThongKeHoaDon();
        }
        public FrameworkElement GetWindowParent(object p)
        {
            FrameworkElement parent = (FrameworkElement)p;

            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
