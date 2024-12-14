using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class ProductTDG
    {
        public static DataTable GetAll()
        {
            var query = "select * from VisProduct;";
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
        
        public static DataTable GetByEan(long ean)
        {
            var query = "select * from VisProduct where ean = @ean";
            var result = new DataTable();
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("ean", ean);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        result.Load(reader);
                    }
                }
            }
            
            return result;
        }

        public static void CreateProduct(string name, double price, int stock, long ean)
        {
            var query = "insert into VisProduct (name, price, stock, ean) values (@name, @price, @stock, @ean)";
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("price", price);
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("ean", ean);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateProduct(string name, double price, int stock, long ean)
        {
            var query = "update VisProduct set name = @name, price = @price, stock = @stock where ean = @ean";
            var connString = DBConnector.GetBuilder().ConnectionString;

            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("name", name);
                    command.Parameters.AddWithValue("price", price);
                    command.Parameters.AddWithValue("stock", stock);
                    command.Parameters.AddWithValue("ean", ean);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
