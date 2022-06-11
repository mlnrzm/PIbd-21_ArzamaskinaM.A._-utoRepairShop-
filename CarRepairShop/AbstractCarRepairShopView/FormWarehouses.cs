using System;
using System.Windows.Forms;
using Unity;
using AbstractCarRepairShopBusinessLogic.BusinessLogics;
using AbstractCarRepairShopContracts.BindingModels;

namespace AbstractCarRepairShopView
{
    public partial class FormWarehouses : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly WarehouseLogic _logic;
        public FormWarehouses(WarehouseLogic logic)
        {
            InitializeComponent();
            _logic = logic;
        }
        private void FormWarehouses_Load(object sender, EventArgs e)
        {
            LoadData();
        }
		private void LoadData()
		{
			try
			{
				var list = _logic.Read(null);
				if (list != null)
				{
					dataGridViewWarehouses.DataSource = list;
					dataGridViewWarehouses.Columns[0].Visible = false;
					dataGridViewWarehouses.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dataGridViewWarehouses.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
					dataGridViewWarehouses.Columns[4].Visible = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormWarehouse>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewWarehouses.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormWarehouse>();
                form.Id = Convert.ToInt32(dataGridViewWarehouses.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewWarehouses.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewWarehouses.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        _logic.Delete(new WarehouseBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
