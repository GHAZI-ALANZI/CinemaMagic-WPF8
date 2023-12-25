using CinemaMagic.Models.DTOs;
using CinemaMagic.Models.DTOs.StaffManagement;
using Microsoft.Data.SqlClient;
using System.Data;


namespace CinemaMagic.Models.DataAccessLayer
{
    public class UserDA : DataAccess
    {
        public void Add(UserDTO userModel)
        {
            throw new NotImplementedException();
        }






        public List<UserDTO> getAccounts()
        {
            List<UserDTO> list = new List<UserDTO>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string select = "select * from Accounts";
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Username = reader.GetString(reader.GetOrdinal("Username"));
                                string Password = reader.GetString(reader.GetOrdinal("Password"));
                                int Staff_Id = reader.GetInt32(reader.GetOrdinal("Staff_Id"));
                                list.Add(new UserDTO(Username, Password, Staff_Id));
                            }
                        }
                    }
                }
            }
            catch { }

            return list;
        }

        public void Edit(UserDTO userModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetByAll()
        {
            throw new NotImplementedException();
        }

        public UserDTO GetById(int id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUsername(string username)
        {
            UserDTO user = new();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [ACCOUNTS] WHERE Username=@username";
                    command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserDTO()
                            {
                                //Id = reader[0].ToString(),
                                //Username = reader[1].ToString(),
                                //Password = string.Empty,
                                //AccountType = reader[3].ToString(),
                            };
                        }
                    }
                }
            }
            catch { }
            return user;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        //insert  account
        public void addAccount(UserDTO user)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into ACCOUNTS\n"
                        +
                        "values("
                        +
                        "'" + user.Username + "',"
                        +
                        "'" + user.Password + "',"
                        +
                        user.Staff_Id + ")";
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        //delete account
        public void deleteAccount(StaffDTO staff)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string delete =
                        "delete ACCOUNTS\n"
                    +
                        "where Staff_Id=" + staff.Id;
                    using (SqlCommand command = new SqlCommand(delete, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }

        public void changePassword(string username, string passwordNew)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string update =
                        "update Accounts\n"
                        +
                        "set Password=" + "'" + passwordNew + "'\n"
                        +
                        "where Username=" + "'" + username + "'";
                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        public List<Tuple<string, string>> selectUserAndMail()
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string select =
                        "select Username,Email\n"
                        +
                        "from Accounts\n"
                        +
                        "join Staff on Accounts.Staff_Id=Staff.Id";
                    using (SqlCommand command = new SqlCommand(select, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string Username = reader.GetString(reader.GetOrdinal("Username"));
                                string Email = reader.GetString(reader.GetOrdinal("Email"));
                                result.Add(Tuple.Create(Username, Email));
                            }
                        }
                    }
                }
            }
            catch { }
            return result;
        }



        public List<string> selectUsername()
        {
            List<string> result = new List<string>();
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select Username\n"
                        +
                        "from Accounts";
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string username = reader.GetString(reader.GetOrdinal("Username"));
                                result.Add(username);
                            }
                        }
                    }
                }
            }
            catch { }

            return result;
        }



        public void changePassword(int Staff_Id, string passwordNew)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string update =
                        "update Accounts\n"
                        +
                        "set Password=" + "'" + passwordNew + "'\n"
                        +
                        "where Staff_Id=" + "'" + Staff_Id + "'";
                    using (SqlCommand command = new SqlCommand(update, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        public string passwordStaff_Id(int Staff_Id)
        {
            string result = "";
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string truyvan =
                        "select Password\n"
                        +
                        "from Accounts\n"
                        +
                        "where Staff_Id=" + Staff_Id;
                    using (SqlCommand command = new SqlCommand(truyvan, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string password = reader.GetString(reader.GetOrdinal("Password"));
                                result = password;
                            }
                        }
                    }
                }
            }
            catch { }

            return result;
        }
    }
}
