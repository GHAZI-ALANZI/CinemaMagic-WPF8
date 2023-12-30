using CinemaMagic.Models.DataAccessLayer;

namespace CinemaMagic.ViewModels.StaffManagementVM
{
    public class MotSoPTBoTro
    {


        public static string RandomFileName()
        {
            Random random = new Random();
            int passwordLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var password = new char[passwordLength];

            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }
            return new string(password);
        }



        public static bool uniqueUsername(string username)
        {
            UserDA userDA = new UserDA();
            foreach (var item in userDA.selectUsername())
            {
                if (username == item)
                {
                    return false;
                }
            }
            return true;
        }





        public static bool checkSalary()
        {
            StaffSalaryDA staffSalaryDA = new StaffSalaryDA();
            List<string> list = staffSalaryDA.listDateBill();
            string timeNow = DateTime.Today.ToString("yyyy-MM-dd");
            foreach (var item in list)
            {
                if (item == timeNow)
                {
                    return true;
                }
            }

            return false; ;
        }
    }
}
