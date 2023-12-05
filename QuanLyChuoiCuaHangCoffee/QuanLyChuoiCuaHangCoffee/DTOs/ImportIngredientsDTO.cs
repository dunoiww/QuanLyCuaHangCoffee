using QuanLyChuoiCuaHangCoffee.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class ImportIngredientsDTO
    {
        public string TenNguyenLieu { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public decimal Gia { get; set; }
        public string GiaStr
        {
            get { return Helper.FormatVNMoney(Gia); }
        }
    }
}
