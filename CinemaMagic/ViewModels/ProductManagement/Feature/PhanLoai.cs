using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs.ProductManagement;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace CinemaMagic.ViewModels.ProductManagement
{
    // Feature for classification in the cboPhanLoai combobox
    public partial class ProductManagementViewModel
    {
        void PhanLoai()//Let's consider it as the initialization function for the feature
        {
            loadData();
            cboSelectionChangedCommand = new ViewModelCommand(CboSelectionChanged);
        }



        public ICommand cboSelectionChangedCommand { get; set; }


        private int _selectedItemIndex;
        public int SelectedItemIndex
        {
            get { return _selectedItemIndex; }
            set
            {
                if (_selectedItemIndex != value)
                {
                    _selectedItemIndex = value;
                    OnPropertyChanged(nameof(SelectedItemIndex));
                }
            }
        }

        private void CboSelectionChanged(object obj)
        {

            // Implement processing when the selected value changes here
            HandleSelectionChanged();
        }

        private void HandleSelectionChanged()
        {
            if (SelectedItemIndex == 1)
            {
                FilterDSSP = DSSP_ThucAn;
            }
            else if (SelectedItemIndex == 2)
            {
                FilterDSSP = DSSP_DoUong;
            }
            else
            {
                FilterDSSP = DSSP_All;
            }
        }

        private void loadData()
        {
            DSSP_All = new ObservableCollection<ProductDTO>();
            DSSP_ThucAn = new ObservableCollection<ProductDTO>();
            DSSP_DoUong = new ObservableCollection<ProductDTO>();

            ProductDA data = new ProductDA();
            ObservableCollection<ProductDTO> getDSSP = data.getDSSP();
            foreach (var item in getDSSP)
            {
                if (item.Type == 1)
                {
                    DSSP_ThucAn.Add(item);
                }
                else if (item.Type == 2)
                {
                    DSSP_DoUong.Add(item);
                }
                DSSP_All.Add(item);
            }

            SearchProduct();
        }
    }
}