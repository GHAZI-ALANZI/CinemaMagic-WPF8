using CinemaMagic.Models.DataAccessLayer;
using CinemaMagic.Models.DTOs;
using System.Windows.Input;

namespace CinemaMagic.ViewModels.CustomerManagement
{
    public partial class CustomerManagementViewModel
    {
        public ICommand deleteCustomerCommand { get; set; }
        void delete()
        {
            deleteCustomerCommand = new ViewModelCommand(deleteCus);
        }

        private void deleteCus(object obj)
        {
            CustomerDA customer = new CustomerDA();
            if (obj is CustomerDTO cus)
            {
                YesNoMessageBox mb = new YesNoMessageBox("Notification", "Do you want to delete this customer?");
                mb.ShowDialog();
                if (mb.DialogResult == false)
                {
                    return;
                }
                else
                {
                    customer.deleteCustomer(cus);
                    mb.Close();
                    YesMessageBox msb = new YesMessageBox("Notification", "Successfully deleted");
                    msb.ShowDialog();
                    msb.Close();
                }
                loadData();
            }
        }


    }
}
