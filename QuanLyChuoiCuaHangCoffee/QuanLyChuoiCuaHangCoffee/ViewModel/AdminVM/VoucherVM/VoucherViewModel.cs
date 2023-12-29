using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Views.Admin.VoucherPage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.VoucherVM
{
    public partial class VoucherViewModel : BaseViewModel
    {
        #region variable
        private bool _IsReleaseVoucherLoading { get; set; }
        public bool IsReleaseVoucherLoading
        {
            get { return _IsReleaseVoucherLoading; }
            set { _IsReleaseVoucherLoading = value; OnPropertyChanged(); }
        }
        private VoucherDTO _SelectedVoucherItem { get; set; }
        public VoucherDTO SelectedVoucherItem
        {
            get { return _SelectedVoucherItem; }
            set { _SelectedVoucherItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<VoucherDTO> _ListVoucher { get; set; }
        public ObservableCollection<VoucherDTO> ListVoucher
        {
            get { return _ListVoucher; }
            set { _ListVoucher = value; OnPropertyChanged(); }
        }
        #endregion

        #region command
        public ICommand LoadAddVoucher { get; set; }
        #endregion

        public VoucherViewModel()
        {
            LoadAddVoucher = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddVoucherWindow wd = new AddVoucherWindow();
                wd.ShowDialog();
            });
        }

    }
}
