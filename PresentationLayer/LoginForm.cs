using BusinessLayer;

namespace PresentationLayer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int role = BusinessLayer.UserTS.Login(textBox1.Text, textBox2.Text);

            if (role == 1)
            {
                AdminForm userForm = new AdminForm();
                userForm.Show();
                this.Hide();
            }
            else if (role == 2)
            {
                SupervisorForm supervisorForm = new SupervisorForm();
                supervisorForm.Show();
                this.Hide();
            }
            else if (role == 3)
            {
                UserForm userForm = new UserForm();
                userForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }
    }
}