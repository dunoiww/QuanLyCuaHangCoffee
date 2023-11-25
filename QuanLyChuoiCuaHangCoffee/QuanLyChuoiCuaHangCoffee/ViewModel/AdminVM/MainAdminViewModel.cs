using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Admin.DashboardPage;
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
        public ICommand LoadStatisticalPageML { get; set; }

        public MainAdminViewModel()
        {
            LoadStatisticalPageML = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainDashboardPage();
            });
        }
    }
}
