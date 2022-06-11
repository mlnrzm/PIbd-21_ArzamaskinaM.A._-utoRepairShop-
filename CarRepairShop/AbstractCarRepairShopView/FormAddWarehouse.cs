using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractCarRepairShopView
{
    public partial class FormAddWarehouse : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IWarehouseLogic _warehouseLogic;
        public int WareHouseId { get { return Convert.ToInt32(comboBoxWarehouse.SelectedValue); } set { comboBoxWarehouse.SelectedValue = value; } }
        public int ComponentId { get { return Convert.ToInt32(comboBoxComponent.SelectedValue); } set { comboBoxComponent.SelectedValue = value; } }
        public int Count { get { return Convert.ToInt32(textBoxCount.Text); } set { textBoxCount.Text = value.ToString(); } }
        public FormAddWarehouse(IWarehouseLogic logicWarehouse, IComponentLogic logicComponent)
        {
            InitializeComponent();
            _warehouseLogic = logicWarehouse;
            List<WarehouseViewModel> listWarehouse = logicWarehouse.Read(null);
            if (listWarehouse != null)
            {
                comboBoxWarehouse.DisplayMember = "WarehouseName";
                comboBoxWarehouse.ValueMember = "Id";
                comboBoxWarehouse.DataSource = listWarehouse;
                comboBoxWarehouse.SelectedItem = null;
            }
            List<ComponentViewModel> listComponent = logicComponent.Read(null);
            if (listComponent != null)
            {
                comboBoxComponent.DisplayMember = "ComponentName";
                comboBoxComponent.ValueMember = "Id";
                comboBoxComponent.DataSource = listComponent;
                comboBoxComponent.SelectedItem = null;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxWarehouse.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Вы не выбрали компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Введите количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                int count = Convert.ToInt32(textBoxCount.Text);
                if (count < 1)
                {
                    throw new Exception("Надо пополнять, а не уменьшать");
                }
                _warehouseLogic.AddComponent(new WarehouseBindingModel
                {
                    Id = Convert.ToInt32(comboBoxWarehouse.SelectedValue)
                }, Convert.ToInt32(comboBoxComponent.SelectedValue), Convert.ToInt32(textBoxCount.Text));
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
