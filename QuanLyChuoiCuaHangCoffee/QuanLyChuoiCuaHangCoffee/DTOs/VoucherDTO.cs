using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.DTOs
{
    public class VoucherDTO
    {
        public string MAVOUCHER { get; set; }
        public string CODEVOUCHER { get; set; }
        public string DISCOUNT { get; set; }
        public DateTime DATEEXPIRED { get; set; }
        public string STATUS { get; set; }
        public string REASON { get; set; }
    }
}
