using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand ExportExcelCommand { get; set; }
        void exportExcel()
        {
            ExportExcelCommand = new ViewModelCommand(ExportExcel);
        }
        private void ExportExcel(object obj)
        {
            // Select folder to save the export file
            string Path = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
            }


            string fileExcelName = "";// Export file name
            string pathFile = "";// Absolute path of the export file

            while (true)
            {
                fileExcelName = "Voucher_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }

            // Prepare for export

            // Create corresponding DataTable
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Voucher name", typeof(string));
            table.Columns.Add("Voucher code", typeof(string));
            table.Columns.Add("Voucher information", typeof(string));
            table.Columns.Add("Type", typeof(string));
            table.Columns.Add("Start date", typeof(string));
            table.Columns.Add("End date", typeof(string));
            // Populate the table with the list
            foreach (var item in FilterDSVC)
            {
                // table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
                table.Rows.Add(item.Id, item.Name, item.Code, item.VoucherDetail, item.Type, item.StartDate, item.FinDate);
            }



            // Proceed with export
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
                    Notify = "Successfully exported the voucher list file!";
                }
                catch
                {
                    Notify = "An error occurred while exporting the file!";
                }
                await Task.Delay(2000);
                Notify = "";
            });
        }
    }
}