using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Quan_Ly_Ban_Hang.ViewModel.Nhập_hàng
{
    public class GuiEmailDataContext : BaseViewModel
    {
        public DONDATHANG dondathang { get; set; }
        Attachment attach = null;

        #region properties
        private string filePath;
        private string from;
        private string to;
        private string subject;
        private string userName;
        private string conTent;
        #endregion

        #region get set properties
        public string From { get => from; set { if (from != value) { from = value; OnPropertyChanged(); } } }
        public string To { get => to; set { if (to != value) { to = value; OnPropertyChanged(); } } }
        public string Subject { get => subject; set { if (subject != value) { subject = value; OnPropertyChanged(); } } }
        public string UserName { get => userName; set { if (userName != value) { userName = value; OnPropertyChanged(); } } }
        public string FilePath { get => filePath; set { if (filePath != value) { filePath = value; OnPropertyChanged(); } } }
        public string ConTent { get => conTent; set { if (conTent != value) { conTent = value; OnPropertyChanged(); } } }
        #endregion

        #region Command
        public ICommand SendCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand AttachFileCommand { get; set; }   
        #endregion

        public GuiEmailDataContext(DONDATHANG dondathang)
        {
            this.dondathang = dondathang;
            LoadInfo();
            new Task(() =>
            {
                Command();
            }).Start();
        }
        public void LoadInfo()
        {
            ConTent = "Mã đơn đặt hàng : " + dondathang.MADONDATHANG + "\n"
                    + "Địa chỉ : " + DataProvider.Instance.DB.CUAHANGs.ToList()[0].DIACHI + "\n"
                    + "Ngày đặt hàng : " + dondathang.NGAYDATHANG + "\n"
                    + "Ngày giao hàng : " + dondathang.NGAYGIAOHANG + "\n"
                    + "Hình thức thanh toán : " + DataProvider.Instance.DB.HINHTHUCTHANHTOANs.Where(x => x.MAHINHTHUCTHANHTOAN == dondathang.MAHINHTHUCTHANHTOAN).SingleOrDefault().TENHINHTHUCTHANHTOAN + "\n";
        }
        public void Command()
        {
            AttachFileCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if ( dialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = dialog.FileName;
                    attach = new Attachment(FilePath);
                }
            });
            SendCommand = new RelayCommand<object>((p) => true, (p) =>
            {           
                new Task(() =>
                {
                    SendMail(From, To, Subject, ConTent, UserName, (p as PasswordBox).Password, attach);
                }).Start();
            });
            ExitCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                (p as Window).Close();
            });
        }
        public void SendMail(string from, string to, string subject, string content,string username , string password, Attachment fileattach = null)
        {
            // tạo 1 message chứa các thông tin và nội dung
            try
            {
                MailMessage message = new MailMessage(from, to, subject, content);
                if (fileattach != null)
                {
                    message.Attachments.Add(fileattach);
                }
                // tạo smtpclient dùng để gửi mail
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                // đăng nhập tài khoản
                client.Credentials = new NetworkCredential(username?.ToString(), password?.ToString());
                client.Send(message);

                System.Windows.MessageBox.Show("Gửi thành công");
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
            }
        }
    }
}
