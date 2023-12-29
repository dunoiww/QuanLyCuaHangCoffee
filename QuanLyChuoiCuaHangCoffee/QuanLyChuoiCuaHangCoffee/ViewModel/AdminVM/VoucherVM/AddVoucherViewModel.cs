using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM
{
    public partial class VoucherViewModel : BaseViewModel
    {
        #region variable
        private int _Quantity { get; set; }
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; OnPropertyChanged(); }
        }

        private int _Length { get; set; }
        public int Length
        {
            get { return _Length; }
            set { _Length = value; OnPropertyChanged(); }
        }

        private string _FirstChar { get; set; }
        public string FirstChar
        {
            get { return _FirstChar; }
            set { _FirstChar = value; OnPropertyChanged(); }
        }

        private string _LastChar { get; set; }
        public string LastChar
        {
            get { return _LastChar; }
            set { _LastChar = value; OnPropertyChanged(); }
        }

        private string _ReleaseName { get; set; }
        public string ReleaseName
        {
            get { return _ReleaseName; }
            set { _ReleaseName = value; OnPropertyChanged(); }
        }
        #endregion

        #region command
        public ICommand SaveListVoucherCF { get; set;}
        #endregion
    }
}
