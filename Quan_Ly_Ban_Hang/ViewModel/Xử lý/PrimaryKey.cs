using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    class PrimaryKey
    {
        private static PrimaryKey instance;

        public static PrimaryKey Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PrimaryKey();
                }
                return instance;
            }
            set => instance = value;
        }

        public string CreatePrimaryKey(string tenbang, string prefix, int buocnhay)
        {
            int DataRow = 0;
            int lengthMax = 6;
            List<string> list = null;
            if (tenbang == "DONDATHANG")
            {
                DataRow = DataProvider.Instance.DB.DONDATHANGs.Count();
                list = DataProvider.Instance.DB.DONDATHANGs.Select(x => x.MADONDATHANG).ToList();
            }
            else if (tenbang == "HOADONBH")
            {
                DataRow = DataProvider.Instance.DB.HOADONBHs.Count();
                list = DataProvider.Instance.DB.HOADONBHs.Select(x => x.MAHOADONBH).ToList();
            }
            else if (tenbang == "NHANVIEN")
            {
                DataRow = DataProvider.Instance.DB.NHANVIENs.Count();
                list = DataProvider.Instance.DB.NHANVIENs.Select(x => x.MANHANVIEN).ToList();
            }
            string khoa = string.Empty;

            if (DataRow == 0)
            {
                if (prefix.Length == 1)
                    khoa = prefix + "00000";
                if (prefix.Length == 2)
                    khoa = prefix + "0000";
                if (prefix.Length == 3)
                    khoa = prefix + "000";
                if (prefix.Length == 4)
                    khoa = prefix + "00";
            }
            int[] sokhoa = new int[DataRow];
            for (var i = 0; i <= DataRow - 1; i++)
            {
                string str;
                str = list[i];
                str = str.Remove(0, prefix.Length);
                sokhoa[i] = int.Parse(str);
            }

            int max = sokhoa[0];
            foreach (int i in sokhoa)
                if (max < i)
                    max = i;
            int chisokhoaLenght;
            chisokhoaLenght = (max + 1 + buocnhay).ToString().Length;
            string strChuoiSo0 = "";
            for (var i = 0; i <= (lengthMax - chisokhoaLenght - prefix.Length - 1); i++)
                strChuoiSo0 += "0";
            khoa = (prefix + strChuoiSo0 + (max + 1).ToString());
            return khoa;
        }
    }
}