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
        public string giatricuSLNTT { get; set; }
        public string giatricuSLTTDDN{get; set ;}
        public string giatricuSLTTTDB { get; set; }
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
            giatricuSLNTT = Load.Instance.LoadSoLuongNhapToiThieu();
            giatricuSLTTDDN = Load.Instance.LoadSoLuongTonToiDaDuocNhap();
            giatricuSLTTTDB = Load.Instance.LoadSoLuongTonToiThieuDuocBan();
         }
        public void Command()
        {
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
            UpdateCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                int slNTT =Int32.Parse(giatricuSLNTT) ;
                int slTTDDN = Int32.Parse(giatricuSLTTDDN);
                int slTTTDB = Int32.Parse(giatricuSLTTTDB);
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
                        }
                    }
                  
                }
 
                THAMSO thamso = new THAMSO()
                {
                    SOLUONGNHAPTOITHIEU =  slNTT,
                    SOLUONGTONTOIDADUOCNHAP =slTTDDN,
                    SOLUONGTONTOITHIEUDUOCBAN = slTTTDB,
                };
               
                Update.Instance.UpdateThamSo(thamso);
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
                                    tb.Text = Load.Instance.LoadSoLuongNhapToiThieu();
                                }
                                break;
                            case "txbSLTTDDNcu":
                                {
                                    tb.Text = Load.Instance.LoadSoLuongTonToiDaDuocNhap();
                                }
                                break;
                            case "txbSLTTTDBcu":
                                {
                                    tb.Text = Load.Instance.LoadSoLuongTonToiThieuDuocBan();
                                }
                                break;
                        }
                    }
                }


            });
        }
    }
    }

