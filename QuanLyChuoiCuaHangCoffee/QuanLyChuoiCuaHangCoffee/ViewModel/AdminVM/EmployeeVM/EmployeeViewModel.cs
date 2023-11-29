using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.Views.Admin.EmployeePage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.EmployeeVM
{
    public class EmployeeViewModel : BaseViewModel
    {
        public ICommand LoadListEmployeePage { get; set; }

        public EmployeeViewModel()
        {
            LoadListEmployeePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new ListEmployeePage();
            });
        }
    }
}
