namespace Stroblhofwarte.Rotator.MqttGateway
{
    partial class FormSetupFocuser
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSetupFocuser));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonReCal = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridViewFocusPoints = new System.Windows.Forms.DataGridView();
            this.bindingSourceFocusPoints = new System.Windows.Forms.BindingSource(this.components);
            this.buttonFocuserGoTo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFocusPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFocusPoints)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(364, 490);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonReCal
            // 
            this.buttonReCal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonReCal.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonReCal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonReCal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonReCal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReCal.Location = new System.Drawing.Point(33, 38);
            this.buttonReCal.Name = "buttonReCal";
            this.buttonReCal.Size = new System.Drawing.Size(57, 23);
            this.buttonReCal.TabIndex = 32;
            this.buttonReCal.Text = "re-cal";
            this.buttonReCal.UseVisualStyleBackColor = false;
            this.buttonReCal.Click += new System.EventHandler(this.buttonReCal_Click);
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(90, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(265, 46);
            this.label13.TabIndex = 33;
            this.label13.Text = "After a manual movement a quick recalibration can be done:";
            // 
            // dataGridViewFocusPoints
            // 
            this.dataGridViewFocusPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFocusPoints.Location = new System.Drawing.Point(33, 129);
            this.dataGridViewFocusPoints.Name = "dataGridViewFocusPoints";
            this.dataGridViewFocusPoints.RowTemplate.Height = 25;
            this.dataGridViewFocusPoints.Size = new System.Drawing.Size(375, 286);
            this.dataGridViewFocusPoints.TabIndex = 34;
            this.dataGridViewFocusPoints.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewFocusPoints_UserAddedRow);
            this.dataGridViewFocusPoints.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridViewFocusPoints_UserDeletingRow);
            // 
            // bindingSourceFocusPoints
            // 
            this.bindingSourceFocusPoints.CurrentChanged += new System.EventHandler(this.bindingSourceFocusPoints_CurrentChanged);
            // 
            // buttonFocuserGoTo
            // 
            this.buttonFocuserGoTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserGoTo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserGoTo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFocuserGoTo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonFocuserGoTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFocuserGoTo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonFocuserGoTo.Location = new System.Drawing.Point(414, 129);
            this.buttonFocuserGoTo.Name = "buttonFocuserGoTo";
            this.buttonFocuserGoTo.Size = new System.Drawing.Size(46, 34);
            this.buttonFocuserGoTo.TabIndex = 35;
            this.buttonFocuserGoTo.Text = ">";
            this.buttonFocuserGoTo.UseVisualStyleBackColor = false;
            this.buttonFocuserGoTo.Click += new System.EventHandler(this.buttonFocuserGoTo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "Focus points:";
            // 
            // FormSetupFocuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(472, 538);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFocuserGoTo);
            this.Controls.Add(this.dataGridViewFocusPoints);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.buttonReCal);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSetupFocuser";
            this.Text = "Setup Focuser";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFocusPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceFocusPoints)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonOk;
        private Button buttonReCal;
        private Label label13;
        private DataGridView dataGridViewFocusPoints;
        private BindingSource bindingSourceFocusPoints;
        private Button buttonFocuserGoTo;
        private Label label1;
    }
}