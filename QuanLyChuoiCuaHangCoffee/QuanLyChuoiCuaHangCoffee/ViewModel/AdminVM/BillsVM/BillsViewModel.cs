using Library.ViewModel;
using OfficeOpenXml;
using QuanLyChuoiCuaHangCoffee.DTOs;
using QuanLyChuoiCuaHangCoffee.Models.DataProvider;
using QuanLyChuoiCuaHangCoffee.Utils;
using QuanLyChuoiCuaHangCoffee.Views.Admin.BillsPage;
using QuanLyChuoiCuaHangCoffee.Views.MessageBoxCF;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace QuanLyChuoiCuaHangCoffee.ViewModel.AdminVM.BillsVM
{
    public partial class BillsViewModel : BaseViewModel
    {
        private bool _IsGettingSource { get; set; }
        public bool IsGettingSource
        {
            get => _IsGettingSource;
            set
            {
                _IsGettingSource = value;
                OnPropertyChanged();
            }
        }

        public static Grid MaskName { get; set; }

        public ICommand MaskNameCF { get; set; }
        public ICommand LoadSalesInvoicePage { get; set; }
        public ICommand LoadImportInvoicePage { get; set; }

        public BillsViewModel()
        {
            SelectedDateEnd = DateTime.Now;
            SelectedDateStart = DateTime.Now.AddDays(-30);
            //main page bills
            MaskNameCF = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            LoadSalesInvoicePage = new RelayCommand<Frame>((p) => { return true; }, async (p) =>
            {
                
                ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                SalesInvoicePage w = new SalesInvoicePage();
                p.Content = w;
            });

            LoadImportInvoicePage = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                ImportInvoicePage w = new ImportInvoicePage();
                p.Content = w;
            });


            //page sales invoice
            ExportFileSaleInvoiceCF = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = "SaleInvoice",
                    Filter = "Excel |*.xlsx",
                    ValidateNames = true
                };

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

                    // Tạo một đối tượng ExcelPackage
                    ExcelPackage package = new ExcelPackage();

                    // Tạo một đối tượng ExcelWorksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    // Tiêu đề cột
                    worksheet.Cells[1, 1].Value = "Mã đơn hàng";
                    worksheet.Cells[1, 2].Value = "Tên khách hàng";
                    worksheet.Cells[1, 3].Value = "Mã bàn";
                    worksheet.Cells[1, 4].Value = "Ngày mua hàng";
                    worksheet.Cells[1, 5].Value = "Tổng trị giá hoá đơn";

                    // Dữ liệu
                    int count = 2;
                    foreach (var item in ListSaleInvoice)
                    {
                        worksheet.Cells[count, 1].Value = item.MADH;
                        worksheet.Cells[count, 2].Value = item.TENKHACHHANG;
                        worksheet.Cells[count, 3].Value = item.MABAN;
                        worksheet.Cells[count, 4].Value = item.NGDH;
                        worksheet.Cells[count, 5].Value = item.TONGTIENSTR;

                        count++;
                    }

                    // Lưu file Excel
                    FileInfo fileInfo = new FileInfo(sf.FileName);
                    package.SaveAs(fileInfo);

                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                    MessageBoxCF mb = new MessageBoxCF("Xuất file thành công", MessageType.Accept, MessageButtons.OK);
                    mb.ShowDialog();
                }
            });

            SelectedDateStartChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart >= SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày bắt đầu không thể lớn hơn ngày kết thúc!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                    IsGettingSource = false;
                } else
                {
                    IsGettingSource = true;
                    ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                    IsGettingSource = false;
                }
            });

            SelectedDateEndChanged = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedDateStart >= SelectedDateEnd)
                {
                    MessageBoxCF ms = new MessageBoxCF("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu!", MessageType.Error, MessageButtons.OK);
                    ms.ShowDialog();
                    IsGettingSource = true;
                    ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                    IsGettingSource = false;
                } else
                {
                    IsGettingSource = true;
                    ListSaleInvoice = new ObservableCollection<OrderBillsDTO>(await OrderBillServices.Ins.GetAllBill(SelectedDateStart, SelectedDateEnd));
                    IsGettingSource = false;
                }
            });

            LoadInforSaleInvoice = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                if (SelectedSaleInvoiceItem != null)
                {
                    tableNumber = SelectedSaleInvoiceItem.MABAN.ToString();
                    DateBill = SelectedSaleInvoiceItem.NGDH;
                    Employee = SelectedSaleInvoiceItem.TENNV;
                    HourBillIn = DateTime.Parse(SelectedSaleInvoiceItem.TIMEIN).ToString("HH:mm:ss");
                    MADH = SelectedSaleInvoiceItem.MADH;
                    CusName = SelectedSaleInvoiceItem.TENKHACHHANG;
                    HourBillOut = SelectedSaleInvoiceItem.TIMEOUT != null ? DateTime.Parse(SelectedSaleInvoiceItem.TIMEOUT).ToString("HH:mm:ss") : "";
                    Note = SelectedSaleInvoiceItem.GHICHU;
                    Total = Helper.FormatVNMoney(SelectedSaleInvoiceItem.TONGTIEN);

                    ListProduct = new ObservableCollection<DTOs.MenuItemDTO>(await OrderBillServices.Ins.GetProductFromBill(SelectedSaleInvoiceItem.MADH));

                    BillDetailWindow w = new BillDetailWindow();
                    w.ShowDialog();
                }
            });

            closeCF = new RelayCommand<System.Windows.Window>((p) => { return true; }, (p) =>
            {
                p.Close();

            });
        }
    }
}
