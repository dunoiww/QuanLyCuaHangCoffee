using Library.ViewModel;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.TablesPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.TablesVM
{
    public partial class TablesViewModel : BaseViewModel
    {
        #region variable
        public Frame MainFrame { get; set; }
        private string _tableNumber { get; set; }
        public string tableNumber
        {
            get { return _tableNumber; }
            set { _tableNumber = value; OnPropertyChanged(); }
        }

        private string _Employee { get; set; }
        public string Employee
        {
            get { return _Employee; }
            set { _Employee = value; OnPropertyChanged(); }
        }

        private string _CusPhone { get; set; }
        public string CusPhone
        {
            get { return _CusPhone; }
            set { _CusPhone = value; OnPropertyChanged(); }
        }

        private string _CusName { get; set; }
        public string CusName
        {
            get { return _CusName; }
            set { _CusName = value; OnPropertyChanged(); }
        }

        private string _CusID { get; set; } 
        public string CusID
        {
            get { return _CusID; }
            set { _CusID = value; OnPropertyChanged(); }
        }

        private int _CusPoint { get; set; }
        public int CusPoint
        {
            get { return _CusPoint; }
            set { _CusPoint = value; OnPropertyChanged(); }
        }

        private string _DateBill { get; set; }
        public string DateBill
        {
            get { return _DateBill; }
            set { _DateBill = value; OnPropertyChanged(); }
        }

        private decimal _TotalDec { get; set; }
        public decimal TotalDec
        {
            get { return _TotalDec; }
            set { _TotalDec = value; OnPropertyChanged(); }
        }

        private string _Total { get; set; }
        public string Total
        {
            get { return _Total; }
            set { _Total = value; OnPropertyChanged(); }
        }
        private ComboBoxItem _SelectedStatus { get; set; }
        public ComboBoxItem SelectedStatus
        {
            get { return _SelectedStatus; }
            set { _SelectedStatus = value; OnPropertyChanged(); }
        }

        private string _SelectedTableNum { get; set; }
        public string SelectedTableNum
        {
            get { return _SelectedTableNum; }
            set { _SelectedTableNum = value; OnPropertyChanged(); }
        }

        private string _NewNumTable { get; set; }
        public string NewNumTable
        {
            get { return _NewNumTable; }
            set { _NewNumTable = value; OnPropertyChanged(); }
        }

        private string _Note { get; set; }
        public string Note
        {
            get { return _Note; }
            set { _Note = value; OnPropertyChanged(); }
        }

        public static Grid MaskName { get; set; }

        private TablesDTO _SelectedTableItem { get; set; }
        public TablesDTO SelectedTableItem
        {
            get { return _SelectedTableItem; }
            set { _SelectedTableItem = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TablesDTO> _ListTable { get; set; }
        public ObservableCollection<TablesDTO> ListTable
        {
            get { return _ListTable; }
            set { _ListTable = value; OnPropertyChanged(); }
        }

        private ObservableCollection<string> _ListTableNum { get; set; }
        public ObservableCollection<string> ListTableNum
        {
            get { return _ListTableNum; }
            set { _ListTableNum = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuOfTableDTO> _ListTableMenu { get; set; }
        public ObservableCollection<MenuOfTableDTO> ListTableMenu
        {
            get { return _ListTableMenu; }
            set { _ListTableMenu = value; OnPropertyChanged(); }
        }

        private ObservableCollection<MenuOfTableDTO> _MenuOfTable { get; set; }
        public ObservableCollection<MenuOfTableDTO> MenuOfTable
        {
            get { return _MenuOfTable; }
            set { _MenuOfTable = value; OnPropertyChanged(); }
        }

        #endregion

        #region icommand
        public ICommand LoadTablesPage { get; set; }
        public ICommand LoadTable { get; set; }
        public ICommand CheckSelectedStatusCF { get; set; }
        public ICommand ManageTable { get; set; }
        public ICommand LoadMenu { get; set; }
        public ICommand BackTable { get; set; }
        public ICommand LoadTableNum { get; set; }
        public ICommand AddTable { get; set; }
        public ICommand DeleteTable { get; set; }
        public ICommand CheckOut { get; set; }
        public ICommand closeCF { get; set; }
        public ICommand MaskNameCF { get; set; }
        public ICommand LoadUser { get; set; }

        #endregion

        public TablesViewModel()
        {
            MaskNameCF = new RelayCommand<Grid>((p) => { return true; }, (p) => { MaskName = p; });
            ListProduct = new ObservableCollection<MenuItemDTO>();
            MenuOfTable = new ObservableCollection<MenuOfTableDTO>();

            LoadTablesPage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                p.Content = new TablesPage();
                MainFrame = p;
            });

            LoadTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                loadListTables();
            });

            CheckSelectedStatusCF = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                CheckCombobox();
            });

            LoadMenu = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new MenuPage();
                tableNumber = SelectedTableItem.MABAN.ToString();
                Employee = AdminServices.TenNhanVien;
                //Employee = "Ngo Nam";
                DateBill = DateTime.Now.Date.ToString("dd/MM/yyyy");

                var existTable = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                if (existTable != null)
                {
                    ListProduct = new ObservableCollection<MenuItemDTO>(existTable.Products);
                } 
                else
                {
                    ListProduct = new ObservableCollection<MenuItemDTO>();
                    MenuOfTableDTO mot = new MenuOfTableDTO();
                    mot.MABAN = SelectedTableItem.MABAN;
                    mot.Products = new ObservableCollection<MenuItemDTO>();

                    MenuOfTable.Add(mot);
                }

                TotalDec = 0;
                foreach (var item in ListProduct)
                {
                    TotalDec += item.THANHTIEN;
                }
                Total = Helper.FormatVNMoney(TotalDec);
            });

            LoadUser = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                CustomerDTO cus = await CustomerServices.Ins.FindCus(CusPhone);
                if (cus != null)
                {
                    CusID = cus.IDKHACHHANG;
                    CusName = cus.HOTEN;
                    CusPoint = cus.TICHDIEM;
                }
                else
                {
                    CusID = "-1";
                    CusName = "Khách vãng lai";
                    CusPoint = 0;
                }
            });

            BackTable = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new TablesPage();
                ResetProperty();
            });

            ManageTable = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                AddTableWindow wd = new AddTableWindow();
                wd.ShowDialog();
                loadListTables();

            });

            LoadTableNum = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetAllTables());
                ListTableNum = new ObservableCollection<string>();
                foreach (var item in ListTable)
                {
                    ListTableNum.Add(item.MABAN.ToString());
                }

            });

            AddTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                (string s, bool f) = await TablesServices.Ins.AddTable(int.Parse(NewNumTable));
                MessageBoxCF ms = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                NewNumTable = null;
                loadListTables();
            });

            DeleteTable = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                MessageBoxCF mb = new MessageBoxCF("Bạn muốn xoá bàn này?", MessageType.Waitting, MessageButtons.YesNo);
                if (mb.ShowDialog() == true)
                {
                    (string s, bool f) = await TablesServices.Ins.DeleteTable(int.Parse(SelectedTableNum));
                    MessageBoxCF ms = new MessageBoxCF(s, f ? MessageType.Accept : MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    loadListTables();
                }
            });

            CheckOut = new RelayCommand<Window>((p) => { return true; }, async (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                await TablesServices.Ins.SetStatusAvailableTable(SelectedTableItem.MABAN);
                BillWindow bw = new BillWindow();
                bw.ShowDialog();

                var table = MenuOfTable.Where(x => x.MABAN == SelectedTableItem.MABAN).FirstOrDefault();
                await CheckOutServices.Ins.CheckOut(CusID, AdminServices.MaNhanVien, SelectedTableItem.MABAN, _DateBill, TotalDec, 20, "", table.Products);

                //Xoá danh sách món ăn của bàn khi thanh toán
                if (table != null)
                {
                    table.Products = new ObservableCollection<MenuItemDTO>();
                }

                MainFrame.Content = new TablesPage();
                ResetProperty();
            });

            closeCF = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #region page menu
            CheckSelectedStatusMenuCF = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                CheckComboboxMenu();
            });

            LoadMenuProducts = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                await GetListMenuSource("");
            });

            AddToMenuTableList = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                addToListMenuItem();
                await TablesServices.Ins.SetStatusNotAvailableTable((SelectedTableItem.MABAN));

                TotalDec = 0;
                foreach (var item in ListProduct)
                {
                    TotalDec += item.THANHTIEN;
                }
                Total = Helper.FormatVNMoney(TotalDec);
            });

            IncreaseProduct = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p is MenuItemDTO item)
                {
                    var product = ListProduct.Where(x => x.TENMON == item.TENMON).FirstOrDefault();
                    if (product != null)
                    {
                        ObservableCollection<MenuItemDTO> temporaryList = new ObservableCollection<MenuItemDTO>(ListProduct);

                        ListProduct.Remove(product);
                        product.SOLUONG = (int.Parse(product.SOLUONG) + 1).ToString();
                        product.THANHTIEN = product.DONGIA * int.Parse(product.SOLUONG);
                        ListProduct.Add(product);

                        // Cập nhật lại ListProduct từ danh sách tạm thời
                        ListProduct = new ObservableCollection<MenuItemDTO>(temporaryList);

                        TotalDec = 0;
                        foreach (var i in ListProduct)
                        {
                            TotalDec += i.THANHTIEN;
                        }
                        Total = Helper.FormatVNMoney(TotalDec);
                    }
                }
            });

            DecreaseProduct = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                if (p is MenuItemDTO item)
                {
                    var product = ListProduct.Where(x => x.TENMON == item.TENMON).FirstOrDefault();
                    if (product != null)
                    {
                        if (int.Parse(product.SOLUONG) > 1)
                        {
                            // Tạo một danh sách tạm thời để lưu trữ các phần tử theo thứ tự ban đầu
                            ObservableCollection<MenuItemDTO> temporaryList = new ObservableCollection<MenuItemDTO>(ListProduct);

                            ListProduct.Remove(product);
                            product.SOLUONG = (int.Parse(product.SOLUONG) - 1).ToString();
                            product.THANHTIEN = product.DONGIA * int.Parse(product.SOLUONG);
                            ListProduct.Add(product);

                            // Cập nhật lại ListProduct từ danh sách tạm thời
                            ListProduct = new ObservableCollection<MenuItemDTO>(temporaryList);
                        }
                        else
                        {
                            ListProduct.Remove(product);
                        }

                        TotalDec = 0;
                        foreach (var i in ListProduct)
                        {
                            TotalDec += i.THANHTIEN;
                        }
                        Total = Helper.FormatVNMoney(TotalDec);
                    }
                }
            });
        }
        #endregion

        private void ResetProperty()
        {
            tableNumber = "";
            Employee = "";
            DateBill = "";
            ListProduct = null;
            Total = 0.ToString();
            CusName = "";
            CusPhone = "";
            CusPoint = 0;
        }

        #region Lấy danh sách bàn
        private async Task GetListTableSource(string s = "")
        {
            ListTable = new ObservableCollection<TablesDTO>();
            switch (s)
            {
                case "":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetAllTables());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "Có khách":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetTablesNotAvailable());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
                case "Còn trống":
                    {
                        try
                        {
                            ListTable = new ObservableCollection<TablesDTO>(await TablesServices.Ins.GetTablesAvailable());
                            break;
                        }
                        catch (System.Data.Entity.Core.EntityException e)
                        {
                            MessageBoxCF mb = new MessageBoxCF("Mất kết nối cơ sở dữ liệu", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                        catch
                        {
                            MessageBoxCF mb = new MessageBoxCF("Lỗi hệ thống", MessageType.Error, MessageButtons.OK);
                            mb.ShowDialog();
                            throw;
                        }
                    }
            }
        }

        private async void loadListTables()
        {
            await GetListTableSource("");
        }

        private async void CheckCombobox()
        {
            switch(SelectedStatus.Content.ToString())
            {
                case "Toàn bộ":
                    {
                        await GetListTableSource("");
                        return;
                    }
                case "Có khách":
                    {
                        await GetListTableSource("Có khách");
                        return;
                    }
                case "Còn trống":
                    {
                        await GetListTableSource("Còn trống");
                        return;
                    }
            }
        }
        #endregion
    }
}
