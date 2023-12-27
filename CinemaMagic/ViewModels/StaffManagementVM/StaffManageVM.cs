using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.StaffManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM : BaseViewModel
    {

        private ObservableCollection<StaffDTO> dsnv;
        public ObservableCollection<StaffDTO> DSNV
        {
            get => dsnv;
            set
            {
                if (dsnv != value)
                {
                    dsnv = value;
                    OnPropertyChanged(nameof(DSNV));
                }
            }
        }


        private StaffManagementView staffManagementView;// Serve to reset column width when adding, editing, deleting
        private int StaffId;
        public StaffManageVM(StaffManagementView staffManagementView, int StaffId)
        {
            this.StaffId = StaffId;
            DSNV = new ObservableCollection<StaffDTO>();
            loadData();
            // Already called in the loadData function, no need anymore
            addStaff();
            delete();
            editStaff();
            exportExcel();
            staffDetail();
            phatluong();
            this.staffManagementView = staffManagementView;
        }

        private void loadData()
        {
            StaffDA staffDA = new StaffDA();
            DSNV = staffDA.getDSNV();
            SearchStaff();// Call here to handle the case of adding a staff member
            loadWidthColumn();
        }

        private void loadWidthColumn()
        {
            if (staffManagementView != null)
            {
                staffManagementView.clId.Width = 0;
                staffManagementView.clId.Width = double.NaN;

                staffManagementView.clFullName.Width = 0;
                staffManagementView.clFullName.Width = double.NaN;

                //staffManagementView.clBirth.Width = 0;
                //staffManagementView.clBirth.Width = double.NaN;

                staffManagementView.clGender.Width = 0;
                staffManagementView.clGender.Width = double.NaN;

                staffManagementView.clEmail.Width = 0;
                staffManagementView.clEmail.Width = double.NaN;

                staffManagementView.clPhoneNumber.Width = 0;
                staffManagementView.clPhoneNumber.Width = double.NaN;

                //staffManagementView.clSalary.Width = 0;
                //staffManagementView.clSalary.Width = double.NaN;

                staffManagementView.clRole.Width = 0;
                staffManagementView.clRole.Width = double.NaN;
            }
        }
    }
}

