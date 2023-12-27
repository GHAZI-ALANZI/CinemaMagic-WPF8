using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {

        //Handle export success notification
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
        private void ExportExcel(object obj)
        {

            //select folder to save export file
            string Path = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
            }


            string fileExcelName = "";//export filename
            string pathFile = "";

            while (true)
            {
                fileExcelName = "Staff_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }



            //prepare export

            // create corresponding datatable
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Họ và tên", typeof(string));
            table.Columns.Add("Ngày sinh", typeof(string));
            table.Columns.Add("Giới tính", typeof(string));
            table.Columns.Add("SĐT", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Chức vụ", typeof(string));
            table.Columns.Add("Lương", typeof(int));



            foreach (var item in FilterDSNV)
            {
                table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
            }


            //proceed with export
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
                    Notify = "Exporting DSNV to Excel file successful!";
                }
                catch
                {
                    Notify = "An error occurred during the export process!";
                }
                await Task.Delay(2000);
                Notify = "";
            });
        }
    }
}
