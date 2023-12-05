using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class ImportBillServices
    {
        private static ImportBillServices _ins;
        public static ImportBillServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ImportBillServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<List<ImportBillDTO>> GetAllBill()
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var BillList = (from bl in context.NHAPKHOes
                                    select new ImportBillDTO
                                    {
                                        MAPHIEU = bl.MAPHIEU,
                                        IDNHANVIEN = bl.IDNHANVIEN,
                                        NGNHAPKHO = bl.NGNHAPKHO
                                    }).ToList();

                    return BillList;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
