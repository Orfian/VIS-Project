using DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessLayer
{
    public static class SaleTS
    {
        public static List<Sale> GetSale()
        {
            List<Sale> sales = new List<Sale>();
            DataTable data = SaleTDG.GetAll();

            foreach (DataRow row in data.Rows)
            {
                Product product = new Product();
                product.Ean = (long)row["ean"];
                product.Name = (string)row["name"];
                product.Price = (double)row["price"];
                product.Stock = (int)row["stock"];

                Sale sale = new Sale();

                sale.IdUser = (int)row["id_user"];
                sale.Product = product;
                sale.Amount = (int)row["amount"];

                sales.Add(sale);
            }

            return sales;
        }
        
        public static int CreateSale(long ean, int amount)
        {
            DataTable data = ProductTDG.GetByEan(ean);

            if (data.Rows.Count != 0 && ((int)data.Rows[0]["stock"] - amount) >= 0)
            {
                SaleTDG.CreateSale(CurrentUser.Instance.User.Id, ean, amount);
                ProductTDG.UpdateProduct((string)data.Rows[0]["name"], (double)data.Rows[0]["price"],(int)data.Rows[0]["stock"] - amount, ean);
                return 0;
            }
            else if (data.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        public static List<SalePrint> GetSalePrint()
        {
            List<SalePrint> printSales = new List<SalePrint>();
            DataTable data = SaleTDG.GetAll();

            foreach (DataRow row in data.Rows)
            {
                SalePrint printSale = new SalePrint();

                printSale.Name = (string)row["name"];
                printSale.Price = (double)row["price"];
                printSale.Amount = (int)row["amount"];
                printSale.Total = (double)row["price"] * (int)row["amount"];

                printSales.Add(printSale);
            }

            return printSales;
        }

        public static void Export()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            
            var json = JsonSerializer.Serialize(GetSalePrint(), options);

            JsonTDG.Export(json);
        }
    }

    public class Sale
    {
        public int IdUser { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }

    public class SalePrint
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double Total { get; set; }
    }
}
