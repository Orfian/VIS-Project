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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void Reload()
        {
            this.guna2DataGridView1.Rows.Clear();
            this.guna2DataGridView2.Rows.Clear();
            this.guna2DataGridView3.Rows.Clear();
            this.guna2DataGridView4.Rows.Clear();

            List<Product> products = BusinessLayer.ProductTS.GetProduct();

            foreach (Product product in products)
            {
                this.guna2DataGridView1.Rows.Add(product.Name, product.Price, product.Stock, product.Ean);
                this.guna2DataGridView3.Rows.Add(product.Name, product.Price, product.Stock, product.Ean);
            }

            List<SalePrint> sales = BusinessLayer.SaleTS.GetSalePrint();

            foreach (SalePrint sale in sales)
            {
                this.guna2DataGridView2.Rows.Add(sale.Name, sale.Price, sale.Amount, sale.Total);
            }

            List<User> users = BusinessLayer.UserTS.GetUser();

            foreach (User user in users)
            {
                this.guna2DataGridView4.Rows.Add(user.Id, user.Username, user.Password, user.Role);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Reload();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.UserTS.CreateUser(textBox1.Text, textBox2.Text, textBox3.Text, (int)numericUpDown1.Value);

            if (success == 0)
            {
                Reload();
                MessageBox.Show("User created succesfully");
            }
            else if (success == 1)
            {
                MessageBox.Show("Username is already taken");
            }
            else
            {
                MessageBox.Show("Passwords are not the same");
            }
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

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.ProductTS.ProductDelivery(long.Parse(textBox4.Text), (int)numericUpDown3.Value);

            if (success == 0)
            {
                Reload();
                MessageBox.Show("Stock added succesfully");
            }
            else
            {
                MessageBox.Show("Product does not exist");
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.ProductTS.CreateProduct(textBox7.Text, (double)numericUpDown5.Value, long.Parse(textBox8.Text));

            if (success == 0)
            {
                Reload();
                MessageBox.Show("Product created succesfully");
            }
            else
            {
                MessageBox.Show("Product already exists");
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.ProductTS.UpdateProduct(textBox9.Text, (double)numericUpDown4.Value, long.Parse(textBox6.Text));

            if (success == 0)
            {
                Reload();
            }
            else
            {
                MessageBox.Show("Product does not exist");
            }
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            int success = BusinessLayer.UserTS.UpdateUser((int)numericUpDown7.Value, textBox11.Text, textBox12.Text, textBox10.Text, (int)numericUpDown6.Value);

            if (success == 0)
            {
                Reload();
            }
            else if (success == 1)
            {
                MessageBox.Show("Username is already taken");
            }
            else
            {
                MessageBox.Show("Passwords are not the same");
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            SaleTS.Export();
            MessageBox.Show("Exported to receipt.json");
        }
    }
}
