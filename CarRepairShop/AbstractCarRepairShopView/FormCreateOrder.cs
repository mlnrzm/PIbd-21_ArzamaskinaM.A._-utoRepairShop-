using AbstractCarRepairShopContracts.BindingModels;
using AbstractCarRepairShopContracts.BusinessLogicsContracts;
using AbstractCarRepairShopContracts.ViewModels;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AbstractCarRepairShopView
{
    public partial class FormCreateOrder : Form
    {
        private readonly IClientLogic _logicC;
        private readonly IRepairLogic _logicP;
        private readonly IOrderLogic _logicO;
        public FormCreateOrder(IRepairLogic logicP, IOrderLogic logicO, IClientLogic logicC)
        {
            InitializeComponent();
            _logicC = logicC;
            _logicP = logicP;
            _logicO = logicO;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<RepairViewModel> list = _logicP.Read(null);
                if (list != null)
                {
                    comboBoxRepair.DisplayMember = "RepairName";
                    comboBoxRepair.ValueMember = "Id";
                    comboBoxRepair.DataSource = list;
                    comboBoxRepair.SelectedItem = null;
                }

                List<ClientViewModel> listC = _logicC.Read(null);
                if (list != null)
                {
                    comboBoxClient.DisplayMember = "Name";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxRepair.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxRepair.SelectedValue);
                    RepairViewModel product = _logicP.Read(new RepairBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxRepair.SelectedValue == null)
            {
                MessageBox.Show("Выберите ремонт", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    RepairId = Convert.ToInt32(comboBoxRepair.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

