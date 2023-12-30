using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System.Data;
using System.IO;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
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
        private void ExportExcel(object obj)
        {


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
                fileExcelName = "Staff_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }




            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("FullName", typeof(string));
            table.Columns.Add("Birth", typeof(string));
            table.Columns.Add("Gender", typeof(string));
            table.Columns.Add("PhoneNumber", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Role", typeof(string));
            table.Columns.Add("Salary", typeof(int));



            foreach (var item in FilterDSNV)
            {
                table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
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
                    Notify = "Successfully exported employees to Excel file!";
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
