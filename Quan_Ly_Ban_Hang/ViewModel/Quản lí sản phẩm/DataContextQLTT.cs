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
    public class DataContextQLTT : BaseViewModel
    {
        public ObservableCollection<HANG> ListHang { get; set; }
        #region commands
        public ICommand SelectedItemChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        #endregion
        public DataContextQLTT()
        {
            LoadInfo();
            Command();
        }
        public void LoadInfo()
        {
            ListHang = Load.Instance.Load_Thong_Tin_Hang();
        }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            AddCommand = new RelayCommand<Grid>((p) => true, (p) =>
              {
                  try
                  {
                      string mahang = "";
                      string tenhang = "";
                      decimal dongia = 0;

                      foreach (var item in p.Children)
                      {

                          if (item is TextBox)
                          {
                              TextBox tb = item as TextBox;
                              if (!string.IsNullOrEmpty(tb.Text))
                              {
                                  switch (tb.Name)
                                  {
                                      case "txbMaHang":
                                          {
                                              mahang = tb.Text;
                                          }
                                          break;
                                      case "txbTenHang":
                                          {
                                              tenhang = tb.Text;
                                          }
                                          break;
                                      case "txbDonGia":
                                          {

                                              dongia = decimal.Parse(tb.Text.ToString());
                                          }
                                          break;
                                  }
                              }
                          }

                      }
                      if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(dongia.ToString()) || string.IsNullOrEmpty(tenhang))
                      {
                          MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                          return;
                      }
                      if (ListHang.Where((a) => a.MAHANG == mahang).ToList().Count > 0)
                      {
                          MessageBox.Show("Sản phẩm đã tồn tại");
                          return;
                      }
                      if (dongia == 0)
                      {
                          {
                              MessageBox.Show("Đon giá phải lớn hơn 0");
                              return;
                          }
                      }
                      HANG hang = new HANG()
                      {
                          MAHANG = mahang,
                          TENHANG = tenhang,
                          DONGIA = dongia,
                          SOLUONGTON = 0
                      };
                      ListHang.Add(hang);
                      Insert.Instance.ThemThongTinSanPham(hang);
                  }
                  catch (Exception e) { }
              });
            DeleteCommand = new RelayCommand<ListView>((p) => true, (p) =>
            {
                try
                {
                    int selectedindex = (p as ListView).SelectedIndex;
                    if (selectedindex >= 0)
                    {
                        Delete.Instance.XoaThongTinSanPham(ListHang[selectedindex]);
                        ListHang.RemoveAt(selectedindex);
                    }
                    else
                    {
                        MessageBox.Show("vui lòng chọn sản phẩm cần xóa");
                    }
                }
                catch (Exception e) { }
            });
            UpdateCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                try
                {
                    string mahang = "";
                    string tenhang = "";
                    decimal dongia = 0;
                    foreach (var item in p.Children)
                    {

                        if (item is TextBox)
                        {
                            TextBox tb = item as TextBox;
                            if (!string.IsNullOrEmpty(tb.Text))
                            {
                                switch (tb.Name)
                                {
                                    case "txbMaHang":
                                        {
                                            mahang = tb.Text;
                                        }
                                        break;
                                    case "txbTenHang":
                                        {
                                            tenhang = tb.Text;
                                        }
                                        break;
                                    case "txbDonGia":
                                        {

                                            dongia = decimal.Parse(tb.Text.ToString());
                                        }
                                        break;
                                }
                            }
                        }

                    }
                    if (string.IsNullOrEmpty(mahang) || string.IsNullOrEmpty(dongia.ToString()) || string.IsNullOrEmpty(tenhang))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                        return;
                    }

                    if (DataProvider.Instance.DB.HANGs.Where(h => h.MAHANG == mahang).ToList().Count == 0)
                    {
                        return;
                    }
                    if (dongia == 0)
                    {
                        {
                            MessageBox.Show("Đon giá phải lớn hơn 0");
                            return;
                        }
                    }
                    var update = ListHang.FirstOrDefault(x => x.MAHANG == mahang);
                    update.TENHANG = tenhang;
                    update.DONGIA = dongia;

                    ListHang.Remove(ListHang.Where(a => a.MAHANG == update.MAHANG).Single());
                    ListHang.Add(update);
                    Update.Instance.Update_ThongTinSanPham(update);
                }
                catch (Exception e) { }
            });
        
        }
    }
}
