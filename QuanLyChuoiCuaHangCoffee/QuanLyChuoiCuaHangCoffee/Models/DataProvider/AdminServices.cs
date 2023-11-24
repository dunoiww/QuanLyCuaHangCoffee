using Microsoft.Xaml.Behaviors.Media;
using QuanLyChuoiCuaHangCoffee.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class AdminServices
    {
        public static string TenNhanVien { get; set; }
        public static DateTime NgSinhNhanVien { get; set; }
        public static string cccdNhanVien { get; set; }
        public static string SoDT { get; set; }
        public static string DiaChiNhanVien { get; set; }
        public static string MaNhanVien { get; set; }
        public static string EmailNhanVien { get; set; }
        public static string UserNameNhanVien { get; set; }
        public static string PasswordNhanVien { get; set; }

        public AdminServices() { }   
        private static AdminServices _ins;
        public static AdminServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new AdminServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async Task<(bool, string)> Login(string username, string password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var admin = await (from s in context.NHANVIENs
                                       where s.USER.USERNAME == username && s.USER.USERPASSWORD == password && s.USER.ROLE == 1
                                       select new AdminDTO
                                       {
                                           IDNHANVIEN = s.IDNHANVIEN,
                                           USERNAME = s.USER.USERNAME,
                                           EMAIL = s.USER.EMAIL,
                                           USERPASSWORD = s.USER.USERPASSWORD,
                                           HOTEN = s.USER.HOTEN,
                                           CCCD = s.USER.CCCD,
                                           SODT = s.USER.SODT,
                                           DOB = s.USER.DOB,
                                           DCHI = s.USER.DIACHI,
                                           NGBATDAU = s.USER.NGBATDAU
                                       }).FirstOrDefaultAsync();

                    if (admin == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu");
                    } else
                    {
                        TenNhanVien = admin.HOTEN;
                        NgSinhNhanVien = admin.DOB;
                        cccdNhanVien = admin.CCCD;
                        SoDT = admin.SODT;
                        DiaChiNhanVien = admin.DCHI;
                        MaNhanVien = admin.IDNHANVIEN;
                        EmailNhanVien = admin.EMAIL;
                        UserNameNhanVien = admin.USERNAME;
                        PasswordNhanVien = admin.USERPASSWORD;
                        return (true, "");
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu");
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống");
            }
        }

    }
}
