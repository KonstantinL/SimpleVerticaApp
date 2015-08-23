using System;
using System.Linq;
using System.Windows.Forms;
using SimpleVerticaApp.Models;

namespace SimpleVerticaApp.Forms
{
    public partial class FormLogin : Form
    {
        public DbCredentials Credentials { get; set; }

        public FormLogin()
        {
            InitializeComponent();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            if (Controls.OfType<TextBox>()
                .Any(control => (control).Text == ""))
            {
                MessageBox.Show("Заполнены не все параметры");
                return;
            }

            Credentials = GetCredentials();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private DbCredentials GetCredentials()
        {
            return new DbCredentials
            {
                ServerIp = tbServer.Text,
                DataBase = tbSchema.Text,
                User = tbLogin.Text,
                Password = tbPass.Text
            };
        }

        private void ShowCredentials()
        {
            if (Credentials == null) return;

            tbServer.Text = Credentials.ServerIp;
            tbSchema.Text = Credentials.DataBase;
            tbLogin.Text = Credentials.User;
            tbPass.Text = Credentials.Password;
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            ShowCredentials();
        }
    }
}
