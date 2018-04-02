using Quan_Ly_Ban_Hang.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_Ly_Ban_Hang.ViewModel
{
    public class DataProvider
    {
        private static DataProvider _instance;
        public static DataProvider Instance{get{if (_instance == null) _instance = new DataProvider(); return _instance;} set => _instance = value; }

        public QLBHEntities DB { get => db; set => db = value; }

        private QLBHEntities db;
        public DataProvider()
        {
            DB = new QLBHEntities();
        }
    }
}
