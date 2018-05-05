using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextLogin : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public DataContextLogin()
        {
            loadInfo();
            AddCommand();
        }

        private void AddCommand()
        {
            LoginCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                try
                {
                    string taikhoan = "";
                    string matkhau = "";
                    foreach (Grid grid in p.Children)
                    {
                        foreach (var item in grid.Children)
                        {
                            if (item is TextBox)
                            {
                                taikhoan = (item as TextBox).Text;
                            }
                            else if (item is PasswordBox)
                            {
                                matkhau = (item as PasswordBox).Password;
                            }
                        }
                    }
                    string encode = Encryptor.EncryptData(matkhau);
                    if (DataProvider.Instance.DB.TAIKHOANs.Where(t => t.TAIKHOAN1 == taikhoan && t.MATKHAU == encode).Count() > 0)
                    {
                        MainWindow main = new MainWindow();
                        FrameworkElement parent = GetWindowParent(p);
                        (parent as Window).Visibility = Visibility.Hidden;
                        main.DataContext = new DataContext();
                        main.Closing += (sender, e) =>
                        {
                            (parent as Window).Visibility = Visibility.Visible;
                        };
                        main.Show();
                        User.Instance.Setvalue(taikhoan,matkhau);
                    }
                    else
                    {
                        MessageBox.Show("tài khoản hoặc mật khẩu sai");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                Application.Current.Shutdown();
            });
            MouseDownCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                FrameworkElement window = GetWindowParent(p);
                var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });
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
        private void loadInfo()
        {
            
        }
    }
}
