using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Customer.BillsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.CustomerVM
{
    public class MainCustomerViewModel : BaseViewModel
    {
        public ICommand LoadMainDashboardPageCF { get; set; }
        public ICommand LoadMainBillsPageCF { get; set; }
        public ICommand LoadMainVoucherPageCF { get; set; }
        public ICommand LoadMainSettingPageCF { get; set; }

        public MainCustomerViewModel()
        {
            LoadMainBillsPageCF = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new MainBillsCusPage();
            });
        }
    }
}
