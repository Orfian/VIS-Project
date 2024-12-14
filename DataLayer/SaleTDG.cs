using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class SaleTDG
    {
        public static DataTable GetAll()
        {
            var query = "select * from VisSale " +
                        "join VisProduct on VisSale.ean = VisProduct.ean;";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            return result;
        }
        /*
        public static DataTable GetAll()
        {
            var query = "select * from VisSale;";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }

            return result;
        }
        */
        public static void CreateSale(int idUser, long ean, int amount)
        {
            var query = "insert into VisSale (id_user, ean, amount) values (@id_user, @ean, @amount)";
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id_user", idUser);
                    command.Parameters.AddWithValue("ean", ean);
                    command.Parameters.AddWithValue("amount", amount);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
