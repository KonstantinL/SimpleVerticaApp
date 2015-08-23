using System;
using System.Windows.Forms;
using SimpleVerticaApp.Models;

namespace SimpleVerticaApp.Forms
{
    public partial class NESampleTable : Form
    {
        public SampleTable CurrTable { get; set; }

        public NESampleTable()
        {
            InitializeComponent();
            
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            try
            {
                CurrTable = GetSampleTable();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось распознать значение в поле ID");
            }
        }

        /// <summary>
        /// Собрать информацию из контролов в экземпляр объекта SampleTable
        /// </summary>
        /// <returns></returns>
        private SampleTable GetSampleTable()
        {
            return new SampleTable
            {
                Id = Int32.Parse(tbID.Text),
                Text = tbText.Text
            };
        }

        /// <summary>
        /// Отобразить инфомрацию экземпляра объекта SampleTable в контролах формы
        /// </summary>
        /// <param name="pData"></param>
        private void ShowSampleTable(SampleTable pData)
        {
            if (pData == null) return;

            tbID.Text = pData.Id.ToString();
            tbText.Text = pData.Text;


            tbID.ReadOnly = true;
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void NESampleTable_Load(object sender, EventArgs e)
        {
            ShowSampleTable(CurrTable);
        }
    }
}
