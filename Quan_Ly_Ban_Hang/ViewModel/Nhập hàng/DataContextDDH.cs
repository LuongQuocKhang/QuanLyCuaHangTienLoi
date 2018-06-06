using OfficeOpenXml;
using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View.Quản_lý_đơn_đặt_hàng;
using Quan_Ly_Ban_Hang.ViewModel.Nhập_hàng;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextDDH : BaseViewModel
    {
        // khi binding đến thuộc tính nào thì thuộc tính đó phải là property( có get set )
        #region list 
        public ObservableCollection<NHAPHANG> ListHang { get; set; }
        public List<NHACUNGCAP> Listnhacungcap { get; set; }
        public ObservableCollection<HANG> ListTenHang { get; set; }
        public List<HINHTHUCTHANHTOAN> listHTTT { get; set; }
        #endregion

        #region properties
        private string soHoaDon;
        public string SoHoaDon { get => soHoaDon; set { if (soHoaDon != value) { soHoaDon = value; OnPropertyChanged(); } } }

        private string manhacungcap;
        public string Manhacungcap
        {
            get
            {
                return manhacungcap;
            }
            set
            {
                if (manhacungcap != value)
                {
                    manhacungcap = value;
                    OnPropertyChanged();
                }
            }
        }

        private string diachi;
        public string DiaChi { get => diachi; set { if (diachi != value) { diachi = value;OnPropertyChanged(); } } }

        private string maCuaHang;
        public string MaCuaHang { get => maCuaHang; set { if (maCuaHang != value) { maCuaHang = value; OnPropertyChanged(); } } }

        private string maHang;
        public string MaHang { get => maHang; set { if (maHang != value) { maHang = value; OnPropertyChanged(); } } }

        private DateTime ngayDatHang;
        public DateTime NgayDatHang { get => ngayDatHang; set { if (ngayDatHang != value) { ngayDatHang = value; OnPropertyChanged(); } } }

        private DateTime ngayGiaoHang;
        public DateTime NgayGiaoHang { get => ngayGiaoHang; set { if (ngayGiaoHang != value) { ngayGiaoHang = value; OnPropertyChanged(); } } }

        private int hinhThucThanhToan;
        public int HinhThucThanhToan { get => hinhThucThanhToan; set { if (hinhThucThanhToan != value) { hinhThucThanhToan = value; OnPropertyChanged(); } } }

        private int tongTien;
        public int TongTien { get => tongTien; set { if (tongTien != value) { tongTien = value; OnPropertyChanged(); } } }
        #endregion

        #region commands
        public ICommand SelectedItemChangedCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SelectedProductCommand { get; set; }
        public ICommand SaveToDatabaseCommand { get; set; }
        public ICommand HinhThucThanhToanCommand { get; set; }
        public ICommand SendCommand { get; set; }
        #endregion
        public DataContextDDH()
        {
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();  
        }
        void Command()
        {
            SelectedItemChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Manhacungcap = ((p as ComboBox).SelectedItem as NHACUNGCAP).MANHACUNGCAP;
            });
            DeleteCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                int selectedindex = (p as ListView).SelectedIndex;
                TongTien -= ListHang[selectedindex].DONGIA * ListHang[selectedindex].SOLUONGNHAP;
                ListHang.RemoveAt(selectedindex);
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            AddCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                string mahang = "";
                string tenhang = "";
                int dongia = 0;
                int soluongnhap = 0;

                foreach (var item in p.Children)
                {
                    if (item is ComboBox)
                    {
                        tenhang = (item as ComboBox).Text;
                    }
                    if (item is TextBox)
                    {
                        TextBox textbox = item as TextBox;
                        switch (textbox.Name)
                        {
                            case "txbMaHang":
                                mahang = textbox.Text;
                                break;
                            case "txbSoluong":
                                if (textbox.Text.Length == 0) return;
                                soluongnhap = Int32.Parse(textbox.Text.ToString());
                                break;
                        }
                    }
                    dongia = Convert.ToInt32(DataProvider.Instance.DB.HANGs.ToList().Where((h) => h.MAHANG == mahang).Select((h) => h.DONGIA).SingleOrDefault());
                }
                if (soluongnhap < 0) soluongnhap = soluongnhap * (-1);
                int? sln = DataProvider.Instance.DB.THAMSOes.SingleOrDefault().SOLUONGNHAPTOITHIEU;
                if (soluongnhap < sln)
                {
                    MessageBox.Show("Số lượng nhập nhỏ hơn quy định số lượng nhập tối thiểu " + sln);
                    return;
                }
                if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(tenhang) || ListHang.Where((a) => a.MAHANG == mahang).ToList().Count > 0 ) return;

                NHAPHANG hang = new NHAPHANG()
                {
                    MAHANG = mahang,
                    TENHANG = tenhang,
                    DONGIA = dongia,
                    SOLUONGNHAP = soluongnhap,
                };
                hang.TONGITEN = hang.DONGIA * hang.SOLUONGNHAP;
                hang.SOLUONGTON = hang.SOLUONGTON + hang.SOLUONGNHAP;
                TongTien += hang.TONGITEN;
                ListHang.Add(hang);
            });
            SelectedProductCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                MaHang = ((p as ComboBox).SelectedItem as HANG).MAHANG;
            });
            HinhThucThanhToanCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                HinhThucThanhToan = (p as ComboBox).SelectedIndex + 1;
            });
            SaveToDatabaseCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (ListHang.Count == 0) return;
                if ( NgayGiaoHang < NgayDatHang)
                {
                    MessageBox.Show("Ngày giao hàng không được trước ngày đặt hàng");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Bạn có chắc muốn đặt hàng không ?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // tạo 1 file excel để lưu thông tin
                        CreateExcelFile();

                        // thêm đơn đặt hàng
                        DONDATHANG dondathang = new DONDATHANG();
                        dondathang.MADONDATHANG = SoHoaDon;
                        dondathang.MANHACUNGCAP = Manhacungcap.Trim();
                        dondathang.MACUAHANG = MaCuaHang.Trim();
                        dondathang.NGAYDATHANG = NgayDatHang;
                        dondathang.NGAYGIAOHANG = NgayGiaoHang;
                        dondathang.MAHINHTHUCTHANHTOAN = HinhThucThanhToan;

                        Insert.Instance.ThemDonDatHang(dondathang);

                        int tongtien = 0;
                        foreach (var item in ListHang)
                        {
                            // thêm chi tiết hóa đơn
                            CHITIETDONDATHANG chitiethoadon = new CHITIETDONDATHANG();
                            chitiethoadon.MADONDATHANG = dondathang.MADONDATHANG;
                            chitiethoadon.MAHANG = item.MAHANG.Trim();
                            chitiethoadon.SOLUONGNHAP = item.SOLUONGNHAP;
                            chitiethoadon.TONGTIENCHITIET = item.TONGITEN;
                            Insert.Instance.ThemChiTietDDH(chitiethoadon);

                            tongtien += item.TONGITEN;

                            // cập nhật hàng hóa
                            Update.Instance.Update_SoLuongTon_NhapHang(item.MAHANG, item.SOLUONGNHAP);
                        }
                        THONGKEDONHANG thongkedonhang = new THONGKEDONHANG();
                        thongkedonhang.MADONDATHANG = dondathang.MADONDATHANG;
                        thongkedonhang.TIENDATHANG = tongtien;
                        Insert.Instance.ThemThongKeDonHang(thongkedonhang);

                        MessageBox.Show("Lưu thành công");

                        SoHoaDon = PrimaryKey.Instance.CreatePrimaryKey("DONDATHANG", "DDH", 1);
                        ListHang.Clear();
                    }
                    catch (Exception e) { MessageBox.Show(e.ToString()); }
                }
                else
                {
                    return;
                }
            });
            SendCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                try
                {
                    DONDATHANG dondathang = new DONDATHANG();
                    dondathang.MADONDATHANG = SoHoaDon;
                    dondathang.MANHACUNGCAP = Manhacungcap.Trim();
                    dondathang.MACUAHANG = MaCuaHang.Trim();
                    dondathang.NGAYDATHANG = NgayDatHang;
                    dondathang.NGAYGIAOHANG = NgayGiaoHang;
                    dondathang.MAHINHTHUCTHANHTOAN = HinhThucThanhToan;

                    Gui_Mail mail = new Gui_Mail();
                    mail.DataContext = new GuiEmailDataContext(dondathang);
                    mail.ShowDialog();
                }
                catch(Exception e)
                {
                    MessageBox.Show("Nhập đầy đủ thông tin");
                }
            });
        }
        public void CreateExcelFile()
        {
            string filepath = "";
            System.Windows.Forms.SaveFileDialog dialog = new System.Windows.Forms.SaveFileDialog();
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {       
                    filepath = dialog.FileName;
                    using (ExcelPackage package = new ExcelPackage())
                    {
                        package.Workbook.Worksheets.Add("Sheet 1");

                        ExcelWorksheet ws = package.Workbook.Worksheets[1];
                        ws.Cells.Style.Font.Size = 12;

                        string[] ColumnHeader = { "Mã hàng", "Tên hàng", "Số lượng nhập", "Đơn giá", "Tổng tiền" };

                        // tạo title
                        int ColumnCount = ColumnHeader.Length;
                        ws.Cells[1, 1].Value = "Thông tin chi tiết đơn đặt hàng";
                        ws.Cells[1, 1, 1, ColumnCount].Merge = true;
                        ws.Cells[1, 1, 1, ColumnCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                        // thêm column header
                        int ColumnIndex = 1;
                        int RowIndex = 2;
                        foreach (var item in ColumnHeader)
                        {
                            var cell = ws.Cells[RowIndex, ColumnIndex];

                            var fill = cell.Style.Fill;
                            fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.Azure);
                            // gán giá trị
                            cell.Value = item;
                            ColumnIndex++;
                        }

                        // thêm giá trị vào Excel
                        foreach (var item in ListHang)
                        {
                            ColumnIndex = 1;
                            RowIndex++;
                            ws.Cells[RowIndex, ColumnIndex++].Value = item.MAHANG;
                            ws.Cells[RowIndex, ColumnIndex++].Value = item.TENHANG;
                            ws.Cells[RowIndex, ColumnIndex++].Value = item.SOLUONGNHAP;
                            ws.Cells[RowIndex, ColumnIndex++].Value = item.DONGIA;
                            ws.Cells[RowIndex, ColumnIndex++].Value = item.TONGITEN;
                        }
                        // lưu file
                        Byte[] ByteArray = package.GetAsByteArray();
                        File.WriteAllBytes(filepath, ByteArray);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
        }
        void LoadInfo()
        {
            NgayDatHang = DateTime.Now;
            NgayGiaoHang = DateTime.Now;

            ListHang = new ObservableCollection<NHAPHANG>();
            Listnhacungcap = DataProvider.Instance.DB.NHACUNGCAPs.ToList();
            SoHoaDon = PrimaryKey.Instance.CreatePrimaryKey("DONDATHANG", "DDH", 1);
            ListTenHang = Load.Instance.Load_Thong_Tin_Hang();
            listHTTT = Load.Instance.Load_HTTT();

            var cuahang = Load.Instance.Load_Cua_Hang();
            DiaChi = cuahang.DIACHI;
            MaCuaHang = cuahang.MACUAHANG;  
        }
    }
}
