using Quan_Ly_Ban_Hang.Model;
using Quan_Ly_Ban_Hang.ViewModel.Xử_lý;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataContextLogin : BaseViewModel
    {
        private bool ghiNho;
        public bool GhiNho { get => ghiNho; set { if (ghiNho != value) { ghiNho = value; OnPropertyChanged(); } } }

        List<TAIKHOAN> listtaikhoan { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand MouseDownCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand LoadCommand { get; set; }
      
        public DataContextLogin()
        {
            loadInfo();
            new Task(() =>
            {
                AddCommand();
            }).Start();
        }

        private void AddCommand()
        {
            LoginCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                try
                {
                    string taikhoan = "";
                    string matkhau = "";

                    // không xóa tài khoản , mật khẩu khi nhập sai
                    TextBox textbox = null;
                    PasswordBox passwordBox = null;

                    foreach (Grid grid in p.Children)
                    {
                        foreach (var item in grid.Children)
                        {
                            if (item is TextBox)
                            {
                                textbox = (item as TextBox);
                                taikhoan = (item as TextBox).Text;
                            }
                            else if (item is PasswordBox)
                            {
                                passwordBox = (item as PasswordBox);
                                matkhau = (item as PasswordBox).Password;
                            }
                        }
                    }
                    // mã hóa
                    string encode = Encryptor.EncryptData(matkhau);

                    if (listtaikhoan.Where(t => t.TAIKHOAN1 == taikhoan && t.MATKHAU == encode).Count() > 0)
                    {
                        MainWindow main = new MainWindow();
                        FrameworkElement parent = GetWindowParent(p);
                        main.Show();

                        // clear
                        textbox.Text = "";
                        passwordBox.Password = "";

                        // lưu tài khoản , mật khầu 
                        User.Instance.Setvalue(taikhoan,matkhau, GhiNho);
                        main.DataContext = new DataContext((parent as Window));
                        (parent as Window).Close();

                    }
                    else
                    {
                        MessageBox.Show("tài khoản hoặc mật khẩu sai");
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("vui lòng nhập tài khoản và mật khảu");
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
            LoadCommand = new RelayCommand<Grid>((p) => true, (p) =>
            {
                if ( User.Instance.GhiNho == true )
                {
                    foreach (Grid grid in p.Children)
                    {
                        foreach (var item in grid.Children)
                        {
                            if (item is TextBox)
                            {
                                (item as TextBox).Text = User.Instance.TaiKhoan;
                            }
                            else if (item is PasswordBox)
                            {
                                (item as PasswordBox).Password = User.Instance.MatKhau;
                            }
                        }
                    }
                }
                listtaikhoan = DataProvider.Instance.DB.TAIKHOANs.ToList();
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
            new Thread(() =>
            {
                listtaikhoan = DataProvider.Instance.DB.TAIKHOANs.ToList();
            }).Start();

            var win = Application.Current.MainWindow;
            if (Properties.Settings.Default.GhiNho == true)
            {
                User.Instance.Setvalue(Properties.Settings.Default.TaiKhoan, Properties.Settings.Default.MatKhau, true);
                string taikhoan = Properties.Settings.Default.TaiKhoan;
                string matkhau = Properties.Settings.Default.MatKhau;

                string encode = Encryptor.EncryptData(matkhau);

                if (listtaikhoan.Where(t => t.TAIKHOAN1 == taikhoan && t.MATKHAU == encode).Count() > 0)
                {
                    MainWindow main = new MainWindow();
                    main.DataContext = new DataContext(win);
                    main.Show();

                    win.Close();
                }
            }
        }
    }
}
