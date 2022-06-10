using System;
using System.Windows.Forms;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using CarRepairShop;

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
            Program.ConfigGrid(_logic.Read(null), dataGridViewMessages);
        }
    }
}
