using System;
using System.Windows.Forms;
using SimpleVerticaApp.Forms;
using SimpleVerticaApp.Models;
using SimpleVerticaApp.Models.Repository;

namespace SimpleVerticaApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ButtonsUp(false);
        }

        private void bConnect_Click(object sender, EventArgs e)
        {
            Db.CurrDbType = Db.DbType.Vertica;

            using (var form = new FormLogin())
            {
                form.Credentials = new DbCredentials
                {
                    ServerIp = "192.168.1.7",
                    User = "dbadmin",
                    DataBase = "lkTest"
                };

                form.ShowDialog();
                if (form.DialogResult == DialogResult.OK)
                {
                    try
                    {
                        Db.ConnectDb(form.Credentials);
                        ButtonsUp(true);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Не удалось подключиться. " + Environment.NewLine + ex.Message);
                    }
                }
            }
        }

        private void bSelect_Click(object sender, EventArgs e)
        {
            if (Db.CheckConnection()) 
            {
                dataGridView1.DataSource = Db.SelectSampleTable();
            }
            else
            {
                MessageBox.Show("Нет подключения.");
                ButtonsUp(false);
            }
            
            
        }

        /// <summary>
        /// Активация кнопок Select/Insetr/Update/Delete.
        /// </summary>
        /// <param name="flag">True - кнопки доступны, кнопка подключения заблокирована. False - кнопки не доступны, открыта кнопка подключения</param>
        private void ButtonsUp(bool flag)
        {
            bConnect.Enabled = !flag;

            bSelect.Enabled = flag;
            bInsert.Enabled = flag;
            bUpdate.Enabled = flag;
            bDelete.Enabled = flag;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Db.CheckConnection())
            {
                Db.Connection.Close();
            }
            
        }

        private void bInsert_Click(object sender, EventArgs e)
        {
            //var vData = new SampleTable {Id = 2, Text = "тест"};

            using (var form = new NESampleTable())
            {
                form.ShowDialog();

                if (form.DialogResult != DialogResult.OK) return;

                if (Db.CheckConnection())
                {
                    var vRes = Db.InsertSampleTable(form.CurrTable);
                    if (vRes > 0)
                    {
                        bSelect.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("Нет подключения.");
                    ButtonsUp(false);
                }
            }
        }

        private void bDelete_Click(object sender, EventArgs e)
        {
            //var vData = new SampleTable { Id = 2, Text = "тест" };

            if (dataGridView1.CurrentRow == null) return;

            if (Db.CheckConnection())
            {
                var vRes = Db.DeleteSampleTableById(dataGridView1.CurrentRow.DataBoundItem as SampleTable);
                if (vRes > 0)
                {
                    bSelect.PerformClick();
                }
            }
            else
            {
                MessageBox.Show("Нет подключения.");
                ButtonsUp(false);
            }
        }

        private void bUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            
            using (var form = new NESampleTable())
            {
                //var t = dataGridView1.CurrentRow.DataBoundItem as SampleTable;
                form.CurrTable = dataGridView1.CurrentRow.DataBoundItem as SampleTable; ;
                form.ShowDialog();

                if (form.DialogResult != DialogResult.OK) return;

                if (Db.CheckConnection())
                {
                    var vRes = Db.UpdateSampleTable(form.CurrTable);
                    if (vRes > 0)
                    {
                        bSelect.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("Нет подключения.");
                    ButtonsUp(false);
                }
            }
        }
    }
}
