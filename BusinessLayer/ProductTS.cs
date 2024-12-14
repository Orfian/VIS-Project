using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataLayer;
using System.Data;

namespace BusinessLayer
{
    public static class ProductTS
    {
        public static List<Product> GetProduct()
        {
            List<Product> products = new List<Product>();
            DataTable data = ProductTDG.GetAll();

            foreach (DataRow row in data.Rows)
            {
                Product product = new Product();

                product.Name = (string)row["name"];
                product.Price = (double)row["price"];
                product.Stock = (int)row["stock"];
                product.Ean = (long)row["ean"];
                
                products.Add(product);
            }

            return products;
        }

        public static int CreateProduct(string name, double price, long ean)
        {
            DataTable data = ProductTDG.GetByEan(ean);

            if (data.Rows.Count == 0)
            {
                ProductTDG.CreateProduct(name, price, 0, ean);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int UpdateProduct(string name, double price, int stock, long ean)
        {
            DataTable data = ProductTDG.GetByEan(ean);

            if (data.Rows.Count != 0)
            {
                ProductTDG.UpdateProduct(name, price, stock, ean);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int UpdateProduct(string name, double price, long ean)
        {
            DataTable data = ProductTDG.GetByEan(ean);

            if (data.Rows.Count != 0)
            {
                ProductTDG.UpdateProduct(name, price, (int)data.Rows[0]["stock"], ean);
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int ProductDelivery(long ean, int amount)
        {
            DataTable data = ProductTDG.GetByEan(ean);

            if (data.Rows.Count != 0)
            {
                ProductTDG.UpdateProduct((string)data.Rows[0]["name"], (double)data.Rows[0]["price"], (int)data.Rows[0]["stock"] + amount, ean);
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
    
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public long Ean { get; set; }
    }
}
