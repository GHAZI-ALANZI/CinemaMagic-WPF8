using CinemaMagic.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace CinemaMagic.Models.DataAccessLayer
{
    public class BillAddMovieDA : DataAccess
    {
        //add  bill
        public void addBill(BillAddMovieDTO billAddMovieDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into Bill_AddMovie(Movie_Id,BillDate,Total)\n"
                        +
                        "values("
                        +
                        billAddMovieDTO.Movie_Id + ","
                        +
                        "'" + billAddMovieDTO.BillDate + "',"
                        +
                        billAddMovieDTO.Total + ")";
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        //delete  movie
        public void updateMovie_IdNull(int Movie_Id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string updateNull =
                        "update Bill_AddMovie\n"
                        +
                        "set Movie_Id=null\n"
                        +
                        "where Movie_Id=" + Movie_Id;
                    using (SqlCommand command = new SqlCommand(updateNull, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


    }
}
