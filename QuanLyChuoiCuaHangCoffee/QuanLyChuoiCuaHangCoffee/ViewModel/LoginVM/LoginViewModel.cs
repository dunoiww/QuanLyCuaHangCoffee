using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM
{
    public class LoginViewModel : BaseViewModel
    {
        public static Frame MainFrame { get; set; }
        public static Grid Mask { get; set; }
        public ICommand LoadLoginPage { get; set; }
        public ICommand LoadForgotPassPage { get; set; }
        public ICommand LoadRegisterPage { get; set; }
        public ICommand LoadMask { get; set; }

        public LoginViewModel()
        {
            LoadLoginPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
                p.Content = new LoginPage();
            });

            LoadForgotPassPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ForgotPassPage();
            });

            LoadRegisterPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                RegisterWindow rgw = new RegisterWindow();
                Mask.Visibility = Visibility.Visible;
                rgw.ShowDialog();
            });

            LoadMask = new RelayCommand<Grid>((p) => { return true; }, (P) =>
            {
                Mask = P;
            });
        }
    }
}
