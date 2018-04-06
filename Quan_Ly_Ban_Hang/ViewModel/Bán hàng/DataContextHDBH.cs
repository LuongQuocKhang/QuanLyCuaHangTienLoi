using Quan_Ly_Ban_Hang.View;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextHDBH : BaseViewModel
    {
        #region properties
        private string diachi;
        public string DiaChi { get => diachi; set { if (diachi != value) { diachi = value; OnPropertyChanged(); } } }
        #endregion

        #region command

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
            DiaChi = Load.Instance.Load_Cua_Hang().DIACHI;
        }
        public void Command()
        {

        }
    }
}
