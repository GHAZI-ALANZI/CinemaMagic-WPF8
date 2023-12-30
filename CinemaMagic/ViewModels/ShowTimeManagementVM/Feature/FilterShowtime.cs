using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ShowTimeManagement;
using System.Collections.ObjectModel;

namespace CinemaMagic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        string dateChoice = "";
        private DateTime? filterStartDate;
        public DateTime? FilterStartDate
        {
            get => filterStartDate;
            set
            {

                filterStartDate = value;
                OnPropertyChanged(nameof(FilterStartDate));
                if (FilterStartDate != null)
                {
                    dateChoice = FilterStartDate.Value.ToString("dd/MM/yyyy");
                    ObservableCollection<ShowTimeDTO> list = new ObservableCollection<ShowTimeDTO>();

                    ShowTimeDA showTimeDA = new ShowTimeDA();
                    ObservableCollection<ShowTimeDTO> DSST = showTimeDA.getDSShowTime(phong);
                    foreach (var item in DSST)
                    {
                        string[] s = item.StartTime.Split(' ');
                        if (s[0] == dateChoice)
                        {
                            list.Add(item);
                        }
                    }
                    DSSuatChieu = list;
                }
            }
        }
    }
}
