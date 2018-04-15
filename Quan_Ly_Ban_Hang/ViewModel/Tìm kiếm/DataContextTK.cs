using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextTK : BaseViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<HANG> ListHang { get; set; }

        private string filterText_MH,filterText_TH;
        private CollectionViewSource usersCollection;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICollectionView SourceCollection
        {
            get
            {
                return this.usersCollection.View;
            }
        }

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
                RaisePropertyChanged("FilterText_MH");
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
                RaisePropertyChanged("FilterText_TH");
            }
        }
        public DataContextTK()
        {
            LoadInfo();
            usersCollection = new CollectionViewSource();
            usersCollection.Source = ListHang;
            usersCollection.Filter += usersCollection_Filter;
        }
    
        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        void usersCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(filterText_MH) && string.IsNullOrEmpty(filterText_TH))
            {
                e.Accepted = true;
                return;
            }

            HANG usr = e.Item as HANG;
            if(string.IsNullOrEmpty(filterText_MH))
            {
                if((usr.TENHANG.ToUpper().Contains(FilterText_TH.ToUpper())) )
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
            if (string.IsNullOrEmpty(filterText_TH))
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

         
        }
        public void LoadInfo()
        {
            ListHang = Load.Instance.Load_Thong_Tin_Hang();
        }
     
        
    }
}

