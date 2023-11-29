using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Admin.DashboardPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.EmployeePage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.IngredientsPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.SettingPage;
using QuanLyChuoiCuaHangCoffee.Views.Admin.TablesPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM
{
    public class MainAdminViewModel : BaseViewModel
    {
        public ICommand LoadMainDashboardPageCF { get; set; }

        public MainAdminViewModel()
        {
            LoadMainDashboardPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainSettingPage();
            });
        }
    }
}
