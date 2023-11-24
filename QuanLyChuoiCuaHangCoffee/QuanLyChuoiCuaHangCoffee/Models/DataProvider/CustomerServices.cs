﻿using Microsoft.Xaml.Behaviors.Media;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLyChuoiCuaHangCoffee.Models.DataProvider
{
    public class CustomerServices
    {
        public CustomerServices() { }
        private static CustomerServices _ins;
        public static CustomerServices Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new CustomerServices();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        public async void Register(string fullname, string email, string phonenumber, string username, string password)
        {
            if (string.IsNullOrEmpty(fullname) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phonenumber))
            {
                MessageBoxCF ms = new MessageBoxCF("Có vẻ bạn nhập thiếu thông tin rồi.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            if (await Task.Run(() => Ins.CheckUserNameCustomer(username, "-1")))
            {
                MessageBoxCF ms = new MessageBoxCF("Tên tài khoản đã tồn tại.", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }
            using (var context = new CoffeeManagementEntities())
            {
                string currentMaxId = await context.KHACHHANGs.MaxAsync(c => c.IDKHACHHANG);

                string id = CreateNextCustomerId(currentMaxId);

                USER us = new USER();
                us.IDUSER = id;
                us.USERNAME = username;
                us.USERPASSWORD = password;
                us.HOTEN = fullname;
                us.EMAIL = email;
                us.CCCD = "";
                us.SODT = phonenumber;
                us.DOB = DateTime.Now;
                us.DIACHI = "";
                us.NGBATDAU = DateTime.Now;
                us.ROLE = 2;

                context.USERS.Add(us);

                KHACHHANG cus = new KHACHHANG();
                cus.IDKHACHHANG = id;
                cus.TICHDIEM = 0;
                cus.SODONHANG = 0;
                cus.HANGTHANHVIEN = "Chưa";

                context.KHACHHANGs.Add(cus);

                try
                {
                    context.SaveChanges();
                    MessageBoxCF ms = new MessageBoxCF("Chào mừng đến với Quán cafe EPSRO.", MessageType.Accept, MessageButtons.OK);
                    ms.ShowDialog();
                }
                catch
                {
                    MessageBoxCF ms = new MessageBoxCF("Lỗi kết nối cơ sở dữ liệu.", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                }

            }
        }

        private string CreateNextCustomerId(string maxId)
        {
            if (maxId is null)
            {
                return "KH0001";
            }
            string newIdString = $"000{int.Parse(maxId.Substring(2)) + 1}";
            return "KH" + newIdString.Substring(newIdString.Length - 4, 4);
        }

        public async Task<(bool, string, CustomerDTO)> Login(string _username, string _password)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.USERNAME == _username && s.USER.USERPASSWORD == _password && s.USER.ROLE == 2
                               select new CustomerDTO
                               {
                                   IDKHACHHANG = s.IDKHACHHANG,
                                   USERNAME = s.USER.USERNAME,
                                   USERPASSWORD = s.USER.USERPASSWORD,
                                   HOTEN = s.USER.HOTEN,
                                   SODT = s.USER.SODT,
                                   EMAIL = s.USER.EMAIL,
                                   DCHI = s.USER.DIACHI
                               }).FirstOrDefault();

                    if (cus == null)
                    {
                        return (false, "Sai tài khoản hoặc mật khẩu.", null);
                    } else
                    {
                        return (true, "", cus);
                    }
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return (false, "Mất kết nối cơ sở dữ liệu", null);
            }
            catch (Exception)
            {
                return (false, "Lỗi hệ thống", null);
            }
        }

        #region validate
        public async Task<bool> CheckUserNameCustomer(string _username, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    // Tìm khách hàng có mã khách hàng (MaKH)
                    var cus = (from s in context.KHACHHANGs
                                     where s.USER.USERNAME == _username && s.USER.IDUSER != _makh
                                     select new CustomerDTO
                                     {
                                         HOTEN = s.USER.HOTEN,
                                         EMAIL = s.USER.EMAIL
                                     }).FirstOrDefault();
                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Username này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }

        public async Task<bool> CheckEmailCustomer(string _email, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.EMAIL == _email && s.USER.IDUSER != _makh
                               select new CustomerDTO
                               {
                                   HOTEN = s.USER.HOTEN,
                                   EMAIL = s.USER.EMAIL
                               }).FirstOrDefault();

                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Email này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }

        public async Task<bool> CheckPhonenumberCustomer(string _phonenumber, string _makh)
        {
            try
            {
                using (var context = new CoffeeManagementEntities())
                {
                    var cus = (from s in context.KHACHHANGs
                               where s.USER.EMAIL == _phonenumber && s.USER.IDUSER != _makh
                               select new CustomerDTO
                               {
                                   HOTEN = s.USER.HOTEN,
                                   EMAIL = s.USER.EMAIL
                               }).FirstOrDefault();

                    if (cus == null) return false;
                    return true;
                }
            }
            catch (Exception)
            {
                MessageBoxCF ms = new MessageBoxCF("Số điện thoại này đã tồn tại", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return true;
            }
        }
        #endregion
    }
}