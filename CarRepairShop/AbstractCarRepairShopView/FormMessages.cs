using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;

namespace AbstractCarRepairShopView
{
    public partial class FormMessages : Form
    {
        private readonly IMessageInfoLogic _logic;
        public FormMessages(IMessageInfoLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void FormMessages_Load(object sender, EventArgs e)
        {
            var list = _logic.Read(null);
            if (list != null)
            {
                dataGridViewMessages.DataSource = list;
                dataGridViewMessages.Columns[0].Visible = false;
                dataGridViewMessages.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }
    }
}
