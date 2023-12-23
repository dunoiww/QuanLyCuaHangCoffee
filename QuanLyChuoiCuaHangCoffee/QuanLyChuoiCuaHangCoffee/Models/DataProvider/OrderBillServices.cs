using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class OrderBillServices
    {
        public OrderBillServices() { }
        private static OrderBillServices _ins;
        public static OrderBillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new OrderBillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        
    }
}
