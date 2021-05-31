
namespace Sleave.view
{
    partial class FrmAbsences
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnValid = new System.Windows.Forms.Button();
            this.lblReason = new System.Windows.Forms.Label();
            this.lblDateStart = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.cboReason = new System.Windows.Forms.ComboBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtDateStart = new System.Windows.Forms.TextBox();
            this.txtDateEnd = new System.Windows.Forms.TextBox();
            this.txtLastname = new System.Windows.Forms.TextBox();
            this.dgvAbsences = new System.Windows.Forms.DataGridView();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.cboAction = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsences)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(580, 236);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Annuler";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnValid
            // 
            this.btnValid.Location = new System.Drawing.Point(480, 236);
            this.btnValid.Name = "btnValid";
            this.btnValid.Size = new System.Drawing.Size(75, 23);
            this.btnValid.TabIndex = 4;
            this.btnValid.Text = "Valider";
            this.btnValid.UseVisualStyleBackColor = true;
            this.btnValid.Click += new System.EventHandler(this.BtnValid_Click);
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(401, 157);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(30, 13);
            this.lblReason.TabIndex = 24;
            this.lblReason.Text = "Motif";
            // 
            // lblDateStart
            // 
            this.lblDateStart.AutoSize = true;
            this.lblDateStart.Location = new System.Drawing.Point(401, 93);
            this.lblDateStart.Name = "lblDateStart";
            this.lblDateStart.Size = new System.Drawing.Size(60, 13);
            this.lblDateStart.TabIndex = 23;
            this.lblDateStart.Text = "Date début";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(401, 58);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(43, 13);
            this.lblFirstName.TabIndex = 22;
            this.lblFirstName.Text = "Prénom";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(401, 26);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(29, 13);
            this.lblLastName.TabIndex = 21;
            this.lblLastName.Text = "Nom";
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(401, 125);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(44, 13);
            this.lblEnd.TabIndex = 20;
            this.lblEnd.Text = "Date fin";
            // 
            // cboReason
            // 
            this.cboReason.FormattingEnabled = true;
            this.cboReason.ItemHeight = 13;
            this.cboReason.Location = new System.Drawing.Point(480, 154);
            this.cboReason.Name = "cboReason";
            this.cboReason.Size = new System.Drawing.Size(175, 21);
            this.cboReason.TabIndex = 3;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Enabled = false;
            this.txtFirstName.Location = new System.Drawing.Point(480, 55);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(175, 20);
            this.txtFirstName.TabIndex = 0;
            // 
            // txtDateStart
            // 
            this.txtDateStart.Enabled = false;
            this.txtDateStart.Location = new System.Drawing.Point(480, 87);
            this.txtDateStart.Name = "txtDateStart";
            this.txtDateStart.Size = new System.Drawing.Size(175, 20);
            this.txtDateStart.TabIndex = 1;
            // 
            // txtDateEnd
            // 
            this.txtDateEnd.Enabled = false;
            this.txtDateEnd.Location = new System.Drawing.Point(480, 122);
            this.txtDateEnd.Name = "txtDateEnd";
            this.txtDateEnd.Size = new System.Drawing.Size(175, 20);
            this.txtDateEnd.TabIndex = 2;
            // 
            // txtLastname
            // 
            this.txtLastname.AccessibleDescription = "";
            this.txtLastname.Enabled = false;
            this.txtLastname.Location = new System.Drawing.Point(480, 23);
            this.txtLastname.Name = "txtLastname";
            this.txtLastname.Size = new System.Drawing.Size(175, 20);
            this.txtLastname.TabIndex = 0;
            // 
            // dgvAbsences
            // 
            this.dgvAbsences.AllowUserToAddRows = false;
            this.dgvAbsences.AllowUserToDeleteRows = false;
            this.dgvAbsences.AllowUserToResizeColumns = false;
            this.dgvAbsences.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAbsences.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAbsences.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvAbsences.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAbsences.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAbsences.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAbsences.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAbsences.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAbsences.EnableHeadersVisualStyles = false;
            this.dgvAbsences.GridColor = System.Drawing.SystemColors.ScrollBar;
            this.dgvAbsences.Location = new System.Drawing.Point(12, 12);
            this.dgvAbsences.MultiSelect = false;
            this.dgvAbsences.Name = "dgvAbsences";
            this.dgvAbsences.ReadOnly = true;
            this.dgvAbsences.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAbsences.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAbsences.RowHeadersVisible = false;
            this.dgvAbsences.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAbsences.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAbsences.Size = new System.Drawing.Size(376, 265);
            this.dgvAbsences.TabIndex = 0;
            this.dgvAbsences.TabStop = false;
            // 
            // dtpStart
            // 
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(480, 87);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(175, 20);
            this.dtpStart.TabIndex = 34;
            this.dtpStart.ValueChanged += new System.EventHandler(this.DtpStart_ValueChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(480, 122);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(175, 20);
            this.dtpEnd.TabIndex = 35;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.DtpEnd_ValueChanged);
            // 
            // cboAction
            // 
            this.cboAction.FormattingEnabled = true;
            this.cboAction.Location = new System.Drawing.Point(480, 203);
            this.cboAction.Name = "cboAction";
            this.cboAction.Size = new System.Drawing.Size(175, 21);
            this.cboAction.TabIndex = 36;
            this.cboAction.Text = "Gérer les absences";
            this.cboAction.SelectedIndexChanged += new System.EventHandler(this.CboAction_SelectedIndexChanged);
            // 
            // FrmAbsences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 301);
            this.Controls.Add(this.cboAction);
            this.Controls.Add(this.txtDateStart);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.dgvAbsences);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnValid);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.lblDateStart);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.cboReason);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtDateEnd);
            this.Controls.Add(this.txtLastname);
            this.Controls.Add(this.dtpEnd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmAbsences";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des absences";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAbsences_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAbsences)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnValid;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblDateStart;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.ComboBox cboReason;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtDateStart;
        private System.Windows.Forms.TextBox txtDateEnd;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.DataGridView dgvAbsences;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.ComboBox cboAction;
    }
}