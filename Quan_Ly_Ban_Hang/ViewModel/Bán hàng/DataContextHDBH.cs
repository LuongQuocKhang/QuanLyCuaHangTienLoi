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
    public class DataContextHDBH : BaseViewModel
    {
        #region list 
        public ObservableCollection<BANHANG> ListHang { get; set; }
        public ObservableCollection<HANG> ListTenHang { get; set; }
        #endregion

        #region properties
        private string soHoaDon;
        public string SoHoaDon { get => soHoaDon; set { if (soHoaDon != value) { soHoaDon = value; OnPropertyChanged(); } } }
        private string diachi;
        public string DiaChi { get => diachi; set { if (diachi != value) { diachi = value; OnPropertyChanged(); } } }
        private string maHang;
        public string MaHang { get => maHang; set { if (maHang != value) { maHang = value; OnPropertyChanged(); } } }
        private int tongTien;
        public int TongTien { get => tongTien; set { if (tongTien != value) { tongTien = value; OnPropertyChanged(); } } }
        private DateTime ngayLapHoaDon;
        public DateTime NgayLapHoaDon { get => ngayLapHoaDon; set { if (ngayLapHoaDon != value) { ngayLapHoaDon = value; OnPropertyChanged(); } } }
        #endregion

        #region command
        public ICommand SelectedProductCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SaveToDatabaseCommand { get; set; }
        #endregion

        public DataContextHDBH()
        {
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }
        public void LoadInfo()
        {
            NgayLapHoaDon = DateTime.Now;

            ListHang = new ObservableCollection<BANHANG>();

            DiaChi = Load.Instance.Load_Cua_Hang().DIACHI;
            ListTenHang = Load.Instance.Load_Thong_Tin_Hang();
            SoHoaDon = PrimaryKey.Instance.CreatePrimaryKey("HOADONBH", "HD", 1);
        }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            SelectedProductCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                MaHang = ((p as ComboBox).SelectedItem as HANG).MAHANG;
            });
            AddCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                string mahang = "";
                string tenhang = "";
                int dongia = 0;
                int soluongban = 0;
                if ( p == null )
                {

                }
                foreach (var item in p.Children)
                {
                    if (item is ComboBox)
                    {
                        tenhang = (item as ComboBox).Text;
                    }
                    if (item is TextBox)
                    {
                        TextBox textbox = item as TextBox;
                        if (textbox.Text.Length == 0)
                        {
                            MessageBox.Show("Nhập đầy đủ tên hàng và số lượng bán");
                            return;
                        }
                        switch (textbox.Name)
                        {     
                            case "txbMaHang":
                                mahang = textbox.Text;
                                break;
                            case "txbSoluong":
                                soluongban = Int32.Parse(textbox.Text.ToString());
                                break;
                        }
                    }
                    dongia = Convert.ToInt32(DataProvider.Instance.DB.HANGs.ToList().Where((h) => h.MAHANG == mahang).Select((h) => h.DONGIA).SingleOrDefault());
                }
                if (soluongban < 0) soluongban = soluongban * (-1);
                int? soluongton = DataProvider.Instance.DB.HANGs.Where(x => x.MAHANG == mahang).Single().SOLUONGTON;
                if (soluongban > soluongton)
                {
                    MessageBox.Show("Số lượng bán vượt quá số lượng tồn hiện tại của sản phẩm " + soluongton);
                    return;
                }
                if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang) || ListHang.Where((a) => a.MAHANG == mahang).ToList().Count > 0) return;

                BANHANG hang = new BANHANG()
                {
                    MAHANG = mahang,
                    TENHANG = tenhang,
                    DONGIA = dongia,
                    SOLUONGBAN = soluongban,
                };
                hang.TONGITEN = hang.DONGIA * hang.SOLUONGBAN;
                hang.SOLUONGTON = hang.SOLUONGTON - hang.SOLUONGBAN;
                TongTien += hang.TONGITEN;
                ListHang.Add(hang);
            });
            DeleteCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int selectedindex = (p as ListView).SelectedIndex;
                if ( selectedindex >= 0)
                {
                    TongTien -= ListHang[selectedindex].DONGIA * ListHang[selectedindex].SOLUONGBAN;
                    ListHang.RemoveAt(selectedindex);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phầm cần xóa");
                    return;
                }
            });
            SaveToDatabaseCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (ListHang.Count == 0) return;
                try
                {
                    // thêm hóa đơn
                    HOADONBH hoadon = new HOADONBH();
                    hoadon.MAHOADONBH = SoHoaDon;
                    hoadon.MACUAHANG = Load.Instance.Load_Cua_Hang().MACUAHANG;
                    hoadon.NGAYLAPHOADON = NgayLapHoaDon;
                    hoadon.MANHANVIEN = User.Instance.MaNhanVien;
                    Insert.Instance.ThemHoaDonBH(hoadon);

                    int? tongtien = 0;
                    foreach (var item in ListHang)
                    {
                        // thêm chi tiết hóa đơn
                        CHITIETHOADON chitiet = new CHITIETHOADON();
                        chitiet.MAHOADONBH = hoadon.MAHOADONBH;
                        chitiet.MAHANG = item.MAHANG;
                        chitiet.SOLUONGBAN = item.SOLUONGBAN;
                        chitiet.TONGTIEN = item.TONGITEN;
                        tongtien += item.TONGITEN;
                        Insert.Instance.ThemChiTietHoaDonBH(chitiet);
                        // cập nhật sản phẩm
                        Update.Instance.Update_SoLuongTon_BanHang(item.MAHANG, item.SOLUONGBAN);
                    }
                    THONGKEHOADON thongke = new THONGKEHOADON();
                    thongke.MAHOADONBH = hoadon.MAHOADONBH;
                    thongke.TIENBANHANG = tongtien;
                    Insert.Instance.ThemThongKeHoaDon(thongke);

                    MessageBox.Show("Bán thành công");

                    SoHoaDon = PrimaryKey.Instance.CreatePrimaryKey("HOADONBH", "HD", 1);
                    ListHang.Clear();
                }
                catch (Exception e) { MessageBox.Show(e.ToString()); }
            });
        }
    }
}
