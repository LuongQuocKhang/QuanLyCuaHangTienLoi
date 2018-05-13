using Quan_Ly_Ban_Hang.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.Model
{
    public class User
    {
        private static User instance;
        public static User Instance
        {
            get
            {
                if (instance == null)
                    instance = new User();
                return instance;
            }
            set => instance = value;
        }

        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string MaNhanVien { get; set; }
        public string TenNhanVien { get; set; }
        public bool GhiNho { get; set; } = false;
        public void Setvalue(string taikhoan , string matkhau , bool ghinho)
        {
            TaiKhoan = taikhoan;
            MatKhau = matkhau;
            MaNhanVien = DataProvider.Instance.DB.TAIKHOANs.Where(tk => tk.TAIKHOAN1 == taikhoan).SingleOrDefault().MANHANVIEN;
            TenNhanVien = DataProvider.Instance.DB.NHANVIENs.Where(nv => nv.MANHANVIEN == MaNhanVien).SingleOrDefault().TENNHANVIEN;
            GhiNho = ghinho;
        }
    }
}
