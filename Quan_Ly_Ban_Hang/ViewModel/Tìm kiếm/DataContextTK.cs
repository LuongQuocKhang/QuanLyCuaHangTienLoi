using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextTK : BaseViewModel
    {
        public ObservableCollection<HANG> ListHang { get; set; }
        private string filterText_MH, filterText_TH;
        private decimal? filterText_DG;
        private CollectionViewSource usersCollection;

        public ICollectionView SourceCollection
        {
            get
            {
                return this.usersCollection.View;
            }
        }

        private ICommand textChangedCommand;

        public string FilterText_MH
        {
            get
            {
                return filterText_MH;
            }
            set
            {
                filterText_MH = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("FilterText_MH");
            }
        }
        public decimal? FilterText_DG
        {
            get
            {
                return filterText_DG;
            }
            set
            {
                filterText_DG = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("filterText_DG");
            }
        }
        public string FilterText_TH
        {
            get
            {
                return filterText_TH;
            }
            set
            {
                filterText_TH = value;
                this.usersCollection.View.Refresh();
                OnPropertyChanged("FilterText_TH");
            }
        }

        public ICommand TextChangedCommand
        {
            get => textChangedCommand;
            set
            {
                if (textChangedCommand != value)
                {
                    textChangedCommand = value;
                    OnPropertyChanged();
                }
            }
        }
        public DataContextTK()
        {
            LoadInfo();

            TextChangedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TextBox dongia = p as TextBox;
                if (dongia.Text.Length == 0)
                {
                    dongia.Text = "0";
                }
            });

            usersCollection = new CollectionViewSource();
            usersCollection.Source = ListHang;
            usersCollection.Filter += usersCollection_Filter;
        }
        void usersCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText_MH) && string.IsNullOrEmpty(FilterText_TH) && string.IsNullOrEmpty(FilterText_DG.ToString()))
            {
                e.Accepted = true;
                return;
            }

            HANG usr = e.Item as HANG;
            if (!string.IsNullOrEmpty(FilterText_MH))
            {    
                if ((usr.MAHANG.ToUpper().Contains(FilterText_MH.ToUpper())))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            if (!string.IsNullOrEmpty(FilterText_TH))
            {
                string filter_tenhang = StringUtil.RemoveSign4VietnameseString(FilterText_TH);
                string tenhang = StringUtil.RemoveSign4VietnameseString(usr.TENHANG);
                if ((tenhang.ToUpper().Contains(filter_tenhang.ToUpper())))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            if (filterText_DG != null)
            {
                if (usr.DONGIA.ToString().Contains(filterText_DG.ToString()))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
        public void LoadInfo()
        {
            ListHang = Load.Instance.Load_Thong_Tin_Hang();
        }
     
        
    }
}

