using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            this.guna2DataGridView1.Rows.Clear();
            this.guna2DataGridView2.Rows.Clear();

            List<Product> products = BusinessLayer.ProductTS.GetProduct();

            foreach (Product product in products)
            {
                this.guna2DataGridView1.Rows.Add(product.Name, product.Price, product.Stock, product.Ean);
            }

            List<SalePrint> sales = BusinessLayer.SaleTS.GetSalePrint();

            foreach (SalePrint sale in sales)
            {
                this.guna2DataGridView2.Rows.Add(sale.Name, sale.Price, sale.Amount, sale.Total);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            UserTS.Logout();

            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.SaleTS.CreateSale(long.Parse(textBox5.Text), (int)numericUpDown2.Value);

            if (success == 0)
            {
                Reload();
            }
            else if (success == 1)
            {
                MessageBox.Show("Product does not exist");
            }
            else
            {
                MessageBox.Show("Not enough stock");
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Refresh();
            Reload();
        }
    }
}
