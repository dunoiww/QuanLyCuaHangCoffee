﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChuoiCuaHangCoffee.Utils
{
    public class ROLEint
    {
        public static readonly int Admin = 1;
        public static readonly int Manager = 2;
        public static readonly int Customer = 3;
    }

    public class ProductTypes
    {
        public static readonly List<string> ListLoaiSanPham = new List<string>()
        {
            "Cà phê",
            "Trà",
            "Sinh tố",
            "Nước ép",
            "Đá xay",
            "Nước ngọt",
            "Soda",
            "Ăn vặt",
        };
    }

    public class Sizes
    {
        public static readonly List<string> ListKichThuoc = new List<string>()
        {
            "Nhỏ",
            "Vừa",
            "Lớn",
        };
    }

    public class Positions
    {
        public static readonly List<string> ListChucVu = new List<string>()
        {
            "Quản lý cửa hàng",
            "Trưởng nhân viên pha chế",
            "Nhân viên pha chế",
            "Nhân viên thu ngân",
            "Nhân viên phục vụ",
        };
    }
}
