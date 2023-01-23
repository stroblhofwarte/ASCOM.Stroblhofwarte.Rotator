namespace Stroblhofwarte.Rotator.MqttGateway
{
    partial class FormSetupDialogRotator
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.textBoxSpeedFactor = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSetSpeedFactor = new System.Windows.Forms.Button();
            this.buttonSetMaximalMovement = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRotatorMaximalMovement = new System.Windows.Forms.TextBox();
            this.checkBoxSwitchMotorOff = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(405, 281);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = false;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // textBoxSpeedFactor
            // 
            this.textBoxSpeedFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxSpeedFactor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSpeedFactor.Location = new System.Drawing.Point(95, 66);
            this.textBoxSpeedFactor.Name = "textBoxSpeedFactor";
            this.textBoxSpeedFactor.Size = new System.Drawing.Size(203, 23);
            this.textBoxSpeedFactor.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Rotator Speed Factor:";
            // 
            // buttonSetSpeedFactor
            // 
            this.buttonSetSpeedFactor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetSpeedFactor.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetSpeedFactor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSetSpeedFactor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonSetSpeedFactor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetSpeedFactor.Location = new System.Drawing.Point(300, 65);
            this.buttonSetSpeedFactor.Name = "buttonSetSpeedFactor";
            this.buttonSetSpeedFactor.Size = new System.Drawing.Size(75, 23);
            this.buttonSetSpeedFactor.TabIndex = 4;
            this.buttonSetSpeedFactor.Text = "Set";
            this.buttonSetSpeedFactor.UseVisualStyleBackColor = false;
            this.buttonSetSpeedFactor.Click += new System.EventHandler(this.buttonSetSpeedFactor_Click);
            // 
            // buttonSetMaximalMovement
            // 
            this.buttonSetMaximalMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetMaximalMovement.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetMaximalMovement.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSetMaximalMovement.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonSetMaximalMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetMaximalMovement.Location = new System.Drawing.Point(300, 132);
            this.buttonSetMaximalMovement.Name = "buttonSetMaximalMovement";
            this.buttonSetMaximalMovement.Size = new System.Drawing.Size(75, 23);
            this.buttonSetMaximalMovement.TabIndex = 7;
            this.buttonSetMaximalMovement.Text = "Set";
            this.buttonSetMaximalMovement.UseVisualStyleBackColor = false;
            this.buttonSetMaximalMovement.Click += new System.EventHandler(this.buttonSetMaximalMovement_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Rotator Maximal Mocement in [°]:";
            // 
            // textBoxRotatorMaximalMovement
            // 
            this.textBoxRotatorMaximalMovement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxRotatorMaximalMovement.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRotatorMaximalMovement.Location = new System.Drawing.Point(95, 132);
            this.textBoxRotatorMaximalMovement.Name = "textBoxRotatorMaximalMovement";
            this.textBoxRotatorMaximalMovement.Size = new System.Drawing.Size(203, 23);
            this.textBoxRotatorMaximalMovement.TabIndex = 5;
            // 
            // checkBoxSwitchMotorOff
            // 
            this.checkBoxSwitchMotorOff.AutoSize = true;
            this.checkBoxSwitchMotorOff.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.checkBoxSwitchMotorOff.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.checkBoxSwitchMotorOff.Location = new System.Drawing.Point(95, 178);
            this.checkBoxSwitchMotorOff.Name = "checkBoxSwitchMotorOff";
            this.checkBoxSwitchMotorOff.Size = new System.Drawing.Size(203, 19);
            this.checkBoxSwitchMotorOff.TabIndex = 10;
            this.checkBoxSwitchMotorOff.Text = "Switch Motor off after movement";
            this.checkBoxSwitchMotorOff.UseVisualStyleBackColor = true;
            this.checkBoxSwitchMotorOff.CheckedChanged += new System.EventHandler(this.checkBoxSwitchMotorOff_CheckedChanged);
            // 
            // FormSetupDialogRotator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(494, 320);
            this.Controls.Add(this.checkBoxSwitchMotorOff);
            this.Controls.Add(this.buttonSetMaximalMovement);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxRotatorMaximalMovement);
            this.Controls.Add(this.buttonSetSpeedFactor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSpeedFactor);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormSetupDialogRotator";
            this.Text = "Setup Rotator Device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button buttonOk;
        private TextBox textBoxSpeedFactor;
        private Label label1;
        private Button buttonSetSpeedFactor;
        private Button buttonSetMaximalMovement;
        private Label label2;
        private TextBox textBoxRotatorMaximalMovement;
        private CheckBox checkBoxSwitchMotorOff;
    }
}