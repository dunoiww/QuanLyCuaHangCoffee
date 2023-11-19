using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.LoginVM
{
    public class ForgotPassViewModel : BaseViewModel
    {
        public ICommand CancelForgotPass { get; set; }

        public ForgotPassViewModel()
        {
            CancelForgotPass = new RelayCommand<object>((p) => { return p == null ? false : true; }, (p) =>
            {
                LoginViewModel.MainFrame.Content = new LoginPage();
            });
        }
    }
}
