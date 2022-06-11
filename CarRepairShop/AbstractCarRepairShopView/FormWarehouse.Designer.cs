
namespace AbstractCarRepairShopView
{
    partial class FormWarehouse
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxRespName = new System.Windows.Forms.TextBox();
            this.dataGridViewComponents = new System.Windows.Forms.DataGridView();
            this.labelRespName = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelComponents = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.componentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponents)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(179, 25);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(246, 27);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxRespName
            // 
            this.textBoxRespName.Location = new System.Drawing.Point(179, 71);
            this.textBoxRespName.Name = "textBoxRespName";
            this.textBoxRespName.Size = new System.Drawing.Size(246, 27);
            this.textBoxRespName.TabIndex = 1;
            // 
            // dataGridViewComponents
            // 
            this.dataGridViewComponents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewComponents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.componentName,
            this.componentCount});
            this.dataGridViewComponents.Location = new System.Drawing.Point(12, 146);
            this.dataGridViewComponents.Name = "dataGridViewComponents";
            this.dataGridViewComponents.RowHeadersVisible = false;
            this.dataGridViewComponents.RowHeadersWidth = 51;
            this.dataGridViewComponents.RowTemplate.Height = 29;
            this.dataGridViewComponents.Size = new System.Drawing.Size(413, 283);
            this.dataGridViewComponents.TabIndex = 2;
            // 
            // labelRespName
            // 
            this.labelRespName.AutoSize = true;
            this.labelRespName.Location = new System.Drawing.Point(29, 74);
            this.labelRespName.Name = "labelRespName";
            this.labelRespName.Size = new System.Drawing.Size(118, 20);
            this.labelRespName.TabIndex = 3;
            this.labelRespName.Text = "Ответственный:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(29, 28);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(80, 20);
            this.labelName.TabIndex = 4;
            this.labelName.Text = "Название:";
            // 
            // labelComponents
            // 
            this.labelComponents.AutoSize = true;
            this.labelComponents.Location = new System.Drawing.Point(29, 114);
            this.labelComponents.Name = "labelComponents";
            this.labelComponents.Size = new System.Drawing.Size(152, 20);
            this.labelComponents.TabIndex = 5;
            this.labelComponents.Text = "Компоненты склада:";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(132, 447);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(140, 29);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(285, 447);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(140, 29);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // componentName
            // 
            this.componentName.HeaderText = "Название";
            this.componentName.MinimumWidth = 6;
            this.componentName.Name = "componentName";
            this.componentName.Width = 285;
            // 
            // componentCount
            // 
            this.componentCount.HeaderText = "Количество";
            this.componentCount.MinimumWidth = 6;
            this.componentCount.Name = "componentCount";
            this.componentCount.Width = 125;
            // 
            // FormWarehouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 504);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelComponents);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelRespName);
            this.Controls.Add(this.dataGridViewComponents);
            this.Controls.Add(this.textBoxRespName);
            this.Controls.Add(this.textBoxName);
            this.Name = "FormWarehouse";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.FormWarehouse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewComponents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxRespName;
        private System.Windows.Forms.DataGridView dataGridViewComponents;
        private System.Windows.Forms.Label labelRespName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelComponents;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentName;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentCount;
    }
}