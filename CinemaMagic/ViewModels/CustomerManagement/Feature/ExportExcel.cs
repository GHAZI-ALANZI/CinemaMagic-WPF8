﻿
using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {

        private string notify;
        public string Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged(nameof(Notify));
            }
        }
        public ICommand ExportExcelCommand { get; set; }
        void exportExcel()
        {
            ExportExcelCommand = new ViewModelCommand(ExportExcel);
        }
        private async void ExportExcel(object obj)
        {

            // file export
            string Path = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
            }


            string fileExcelName = "";
            string pathFile = "";

            while (true)
            {
                fileExcelName = "Customer_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }





            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Họ và tên", typeof(string));
            table.Columns.Add("Ngày sinh", typeof(string));
            table.Columns.Add("Giới tính", typeof(string));
            table.Columns.Add("SĐT", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Ngày đăng ký", typeof(string));
            table.Columns.Add("Điểm", typeof(int));



            foreach (var item in FilterDSCTM)
            {
                // table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
                table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.RegDate, item.Point);
            }



            Task.Run(async () =>
            {
                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        worksheet.Cells.LoadFromDataTable(table, true);

                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        File.WriteAllBytes(pathFile, package.GetAsByteArray());
                    }
                    Notify = "Xuất ra danh sách khách hàng thành công!";
                }
                catch
                {
                    Notify = "Có lỗi xảy ra trong quá trình xuất";
                }
                await Task.Delay(2000);
                Notify = "";
            });
        }
    }
}
