﻿using CinemaMagic.Models.DTOs.StaffManagement;
using CinemaMagic.ViewModels;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace CinemaMagic.Models.DataAccessLayer
{
    public class StaffDA : DataAccess
    {
        private static int soLuongChuSo;
        public ObservableCollection<StaffDTO> getDSNV()
        {
            ObservableCollection<StaffDTO> list = new ObservableCollection<StaffDTO>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan = "select * from Staff";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            soLuongChuSo = identCurrent().ToString().Length;

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("Id"));

                                string fullname = reader.GetString(reader.GetOrdinal("FullName"));

                                DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("Birth"));
                                string birth = birthDate.ToString("dd/MM/yyyy");

                                string gender = reader.GetString(reader.GetOrdinal("Gender"));

                                string email = reader.GetString(reader.GetOrdinal("Email"));

                                string phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                                int salary = reader.GetInt32(reader.GetOrdinal("Salary"));

                                string role = reader.GetString(reader.GetOrdinal("Role"));

                                DateTime NgayVL = reader.GetDateTime(reader.GetOrdinal("NgayVaolam"));
                                string ngayVL = NgayVL.ToString("dd/MM/yyyy");

                                if (!reader.IsDBNull(reader.GetOrdinal("ImageSource")))
                                {
                                    byte[] imageBytes = (byte[])reader["ImageSource"];
                                    BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);

                                    list.Add(new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL, imageSource));
                                }
                                else
                                {
                                    Uri resourceUri = new Uri("pack://application:,,,/Images/InformationManagement/Default.jpg", UriKind.Absolute);
                                    StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(resourceUri);

                                    byte[] imageBytes;
                                    using (Stream imageStream = streamInfo.Stream)
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            imageStream.CopyTo(ms);
                                            imageBytes = ms.ToArray();
                                        }
                                    }

                                    BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);

                                    list.Add(new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL, imageSource));
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return list;
        }


        //insert
        public void addStaff(StaffDTO staff)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into Staff(FullName,Birth,Gender,Email,PhoneNumber,Salary,Role,NgayVaolam)\n"
                        +
                        "values("
                        +
                        "N'" + staff.FullName + "',"
                        +
                        "'" + staff.Birth + "',"
                        +
                        "N'" + staff.Gender + "',"
                        +
                        "'" + staff.Email + "',"
                        +
                        "'" + staff.PhoneNumber + "',"
                        +
                        staff.Salary + ","
                        +
                        "N'" + staff.Role + "',"
                        +
                        "'" + staff.NgayVaoLam + "')";

                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        //del staff
        public void deleteStaff(StaffDTO staff)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string delete =
                        "delete Staff\n"
                        +
                        "where Id=" + staff.Id;
                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //update
        public void updateStaff(StaffDTO staff)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string update =
                        "update Staff\n"
                        +
                        "set FullName=" + "N'" + staff.FullName + "',"
                        +
                        "Birth=" + "'" + staff.Birth + "',"
                        +
                        "Gender=" + "N'" + staff.Gender + "',"
                        +
                        "Email=" + "'" + staff.Email + "',"
                        +
                        "PhoneNumber=" + "'" + staff.PhoneNumber + "',"
                        +
                        "Salary=" + staff.Salary + ","
                        +
                        "Role=" + "N'" + staff.Role + "',"
                        +
                        "NgayVaolam=" + "'" + staff.NgayVaoLam + "'\n"
                        +
                        "where Id=" + staff.Id;


                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


        //iden curent
        public int identCurrent()
        {
            int identCurrent = 0;
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string cm =
                        "select ident_current('Staff') as lastId";
                    using (SqlCommand command = new SqlCommand(cm, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            identCurrent = (int)reader.GetDecimal(reader.GetOrdinal("lastId"));
                        }
                    }
                }
            }
            catch { }
            return identCurrent;
        }

        public static string formatID(int id, string type = "NV")
        {
            string format = "";
            if (soLuongChuSo < 4)
            {
                format = "000";
            }
            else
            {
                for (int k = 0; k < soLuongChuSo; k++)
                {
                    format += "0";
                }
            }
            string ID = id.ToString();

            int i = format.Length - 1;
            int j = ID.Length - 1;
            char[] s1 = format.ToCharArray();
            char[] s2 = ID.ToCharArray();
            while (i >= 0 && j >= 0)
            {
                s1[i--] = s2[j--];
            }

            format = new string(s1);
            format = type + format;
            return format;
        }




        public StaffDTO Staffstaff_Id(int Staff_Id)
        {
            StaffDTO staffDTO = new StaffDTO("admin", "1/1/2000", "Nam", "admin@123", "0358967125", 1500000, "Quản lí", "1/1/2023");
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select * from Staff\n"
                        +
                        "where Id=" + Staff_Id;

                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            soLuongChuSo = identCurrent().ToString().Length;

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(reader.GetOrdinal("Id"));

                                string fullname = reader.GetString(reader.GetOrdinal("FullName"));

                                DateTime birthDate = reader.GetDateTime(reader.GetOrdinal("Birth"));
                                string birth = birthDate.ToString("dd/MM/yyyy");

                                string gender = reader.GetString(reader.GetOrdinal("Gender"));

                                string email = reader.GetString(reader.GetOrdinal("Email"));

                                string phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

                                int salary = reader.GetInt32(reader.GetOrdinal("Salary"));

                                string role = reader.GetString(reader.GetOrdinal("Role"));

                                DateTime NgayVL = reader.GetDateTime(reader.GetOrdinal("NgayVaolam"));
                                string ngayVL = NgayVL.ToString("dd/MM/yyyy");

                                if (!reader.IsDBNull(reader.GetOrdinal("ImageSource")))
                                {
                                    byte[] imageBytes = (byte[])reader["ImageSource"];
                                    BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);
                                    staffDTO = new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL, imageSource);

                                }
                                else
                                {
                                    Uri resourceUri = new Uri("pack://application:,,,/Images/InformationManagement/Default.jpg", UriKind.Absolute);
                                    StreamResourceInfo streamInfo = System.Windows.Application.GetResourceStream(resourceUri);

                                    byte[] imageBytes;
                                    using (Stream imageStream = streamInfo.Stream)
                                    {
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            imageStream.CopyTo(ms);
                                            imageBytes = ms.ToArray();
                                        }
                                    }

                                    BitmapImage imageSource = ImageVsSQL.ByteArrayToBitmapImage(imageBytes);
                                    staffDTO = new StaffDTO(id, fullname, birth, gender, email, phoneNumber, salary, role, ngayVL, imageSource);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            return staffDTO;
        }



        //update image
        public void updateImageStaff(int id, BitmapImage imageSource)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    byte[] imageBytes = ImageVsSQL.BitmapImageToByteArray(imageSource);

                    string update = "UPDATE Staff SET ImageSource = @ImageSource WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(update, connection))
                    {

                        command.Parameters.AddWithValue("@ImageSource", imageBytes);
                        command.Parameters.AddWithValue("@Id", id);


                        command.ExecuteNonQuery();
                    }
                }

            }
            catch { }
        }



        public long GetSalaryByYear(string year)
        {
            long res = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT SUM(Total) FROM [Staff_Salary] WHERE YEAR(BillDate)=@year";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = year;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader[0].ToString())) res += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }

        public long GetSalaryByMonth(string month)
        {
            long res = 0;
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT SUM(Total) FROM [Staff_Salary] WHERE YEAR(BillDate)=YEAR(GETDATE()) AND MONTH(BillDate)=@month";
                    command.Parameters.Add("@month", SqlDbType.Int).Value = month;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (!string.IsNullOrEmpty(reader[0].ToString())) res += Convert.ToInt64(reader[0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }



        public bool checkQuanLy(int staffid)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string cm =
                        "select Role from Staff where id=" + staffid;
                    using (SqlCommand command = new SqlCommand(cm, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            string role = reader.GetString(reader.GetOrdinal("Role"));
                            if (role == "Manager")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch { }
            return false;
        }
    }
}
