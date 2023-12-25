using CinemaMagic.Models.DTOs;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;

namespace CinemaMagic.Models.DataAccessLayer
{
    public class VoucherDA : DataAccess
    {


        public ObservableCollection<VoucherDTO> getDSVC()
        {


            deleteVoucherExpiry();

            ObservableCollection<VoucherDTO> list = new ObservableCollection<VoucherDTO>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan = "select * from Voucher";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("ID"));

                                string name = reader.GetString(reader.GetOrdinal("NAME"));

                                string code = reader.GetString(reader.GetOrdinal("CODE"));

                                string voucherDetail = reader.GetString(reader.GetOrdinal("VOUCHERDETAIL"));

                                string type = reader.GetString(reader.GetOrdinal("TYPE"));

                                DateTime StartDate = reader.GetDateTime(reader.GetOrdinal("STARTDATE"));
                                string startDate = StartDate.ToString("dd/MM/yyyy");

                                DateTime FinDate = reader.GetDateTime(reader.GetOrdinal("FINDATE"));
                                string finDate = FinDate.ToString("dd/MM/yyyy");

                                list.Add(new VoucherDTO(id, name, code, voucherDetail, type, startDate, finDate));
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }



        //add 1 voucher
        public void addVoucher(VoucherDTO voucher)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into Voucher\n"
                        +
                        "values("
                        + "N'" + voucher.Name + "',"
                        + "'" + voucher.Code + "',"
                        + "N'" + voucher.VoucherDetail + "',"
                        + "'" + voucher.Type + "',"
                        + "'" + voucher.StartDate + "',"
                        + "'" + voucher.FinDate + "')";

                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        //edit 1 voucher
        public void editVoucher(VoucherDTO voucher)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string update =
                        "update Voucher\n"
                        +
                        "set Name=" + "N'" + voucher.Name + "',"
                        +
                        "VoucherDetail=" + "N'" + voucher.VoucherDetail + "',"
                        +
                        "Type=" + "'" + voucher.Type + "',"
                        +
                        "StartDate=" + "'" + voucher.StartDate + "',"
                        +
                        "FinDate=" + "'" + voucher.FinDate + "'\n"
                        +
                        "where Id=" + voucher.Id;

                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }




        public List<string> listCode()
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select Code\n"
                        +
                        "from Voucher";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string code = reader.GetString(reader.GetOrdinal("CODE"));
                                list.Add(code);
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }


        public List<string> listCodeOk()
        {
            List<string> list = new List<string>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    string dateNow = DateTime.Now.ToString("yyyy-MM-dd");

                    connection.Open();
                    string truyvan =
                        "select Code\n"
                        +
                        "from Voucher\n"
                        +
                        "where '" + dateNow + "'>=STARTDATE" + " and '" + dateNow + "'<=FINDATE";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string code = reader.GetString(reader.GetOrdinal("CODE"));
                                list.Add(code);
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }


        public void deleteVoucherExpiry()
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string deleteVoucherExpiry =
                        "delete Voucher\n"
                        +
                        "where FINDATE<GETDATE()";
                    using (SqlCommand command = new SqlCommand(deleteVoucherExpiry, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }
    }
}
