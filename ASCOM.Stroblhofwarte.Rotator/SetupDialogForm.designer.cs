
namespace ASCOM.Stroblhofwarte
{
    partial class SetupDialogForm
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.textBoxAngle = new System.Windows.Forms.TextBox();
            this.buttonMove = new System.Windows.Forms.Button();
            this.timerPosition = new System.Windows.Forms.Timer(this.components);
            this.labelPosition = new System.Windows.Forms.Label();
            this.checkBoxPower = new System.Windows.Forms.CheckBox();
            this.textBoxSync = new System.Windows.Forms.TextBox();
            this.buttonSync = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSyncValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonUnsync = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.labelMechanicalPosition = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelTargetPosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Location = new System.Drawing.Point(493, 249);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(493, 279);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe Script", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(375, 48);
            this.label1.TabIndex = 2;
            this.label1.Text = "Stroblhofwarte Rotator";
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.Stroblhofwarte.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(504, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.ForeColor = System.Drawing.Color.Silver;
            this.chkTrace.Location = new System.Drawing.Point(406, 283);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(77, 87);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(90, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(246, 164);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(100, 20);
            this.textBoxAngle.TabIndex = 8;
            this.textBoxAngle.Text = "0.0";
            // 
            // buttonMove
            // 
            this.buttonMove.BackColor = System.Drawing.Color.BlueViolet;
            this.buttonMove.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.buttonMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMove.ForeColor = System.Drawing.Color.Gold;
            this.buttonMove.Location = new System.Drawing.Point(352, 162);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(75, 23);
            this.buttonMove.TabIndex = 9;
            this.buttonMove.Text = "Move to..";
            this.buttonMove.UseVisualStyleBackColor = false;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // timerPosition
            // 
            this.timerPosition.Enabled = true;
            this.timerPosition.Interval = 1000;
            this.timerPosition.Tick += new System.EventHandler(this.timerPosition_Tick);
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.ForeColor = System.Drawing.Color.Silver;
            this.labelPosition.Location = new System.Drawing.Point(134, 167);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(26, 13);
            this.labelPosition.TabIndex = 10;
            this.labelPosition.Text = "0.0°";
            // 
            // checkBoxPower
            // 
            this.checkBoxPower.AutoSize = true;
            this.checkBoxPower.ForeColor = System.Drawing.Color.Silver;
            this.checkBoxPower.Location = new System.Drawing.Point(77, 128);
            this.checkBoxPower.Name = "checkBoxPower";
            this.checkBoxPower.Size = new System.Drawing.Size(210, 17);
            this.checkBoxPower.TabIndex = 11;
            this.checkBoxPower.Text = "Don\'t switch of motor power after move";
            this.checkBoxPower.UseVisualStyleBackColor = true;
            this.checkBoxPower.CheckStateChanged += new System.EventHandler(this.checkBoxPower_CheckStateChanged);
            // 
            // textBoxSync
            // 
            this.textBoxSync.Location = new System.Drawing.Point(246, 191);
            this.textBoxSync.Name = "textBoxSync";
            this.textBoxSync.Size = new System.Drawing.Size(100, 20);
            this.textBoxSync.TabIndex = 12;
            this.textBoxSync.Text = "0.0";
            // 
            // buttonSync
            // 
            this.buttonSync.BackColor = System.Drawing.Color.BlueViolet;
            this.buttonSync.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.buttonSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSync.ForeColor = System.Drawing.Color.Gold;
            this.buttonSync.Location = new System.Drawing.Point(352, 189);
            this.buttonSync.Name = "buttonSync";
            this.buttonSync.Size = new System.Drawing.Size(75, 23);
            this.buttonSync.TabIndex = 13;
            this.buttonSync.Text = "Sync";
            this.buttonSync.UseVisualStyleBackColor = false;
            this.buttonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(75, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Position:";
            // 
            // labelSyncValue
            // 
            this.labelSyncValue.AutoSize = true;
            this.labelSyncValue.ForeColor = System.Drawing.Color.Silver;
            this.labelSyncValue.Location = new System.Drawing.Point(134, 259);
            this.labelSyncValue.Name = "labelSyncValue";
            this.labelSyncValue.Size = new System.Drawing.Size(26, 13);
            this.labelSyncValue.TabIndex = 15;
            this.labelSyncValue.Text = "0.0°";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(57, 259);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Synced Diff:";
            // 
            // buttonUnsync
            // 
            this.buttonUnsync.BackColor = System.Drawing.Color.BlueViolet;
            this.buttonUnsync.FlatAppearance.BorderColor = System.Drawing.Color.MediumOrchid;
            this.buttonUnsync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUnsync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUnsync.ForeColor = System.Drawing.Color.Gold;
            this.buttonUnsync.Location = new System.Drawing.Point(433, 189);
            this.buttonUnsync.Name = "buttonUnsync";
            this.buttonUnsync.Size = new System.Drawing.Size(75, 23);
            this.buttonUnsync.TabIndex = 17;
            this.buttonUnsync.Text = "Clear Sync";
            this.buttonUnsync.UseVisualStyleBackColor = false;
            this.buttonUnsync.Click += new System.EventHandler(this.buttonUnsync_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(17, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Mechanical Position:";
            // 
            // labelMechanicalPosition
            // 
            this.labelMechanicalPosition.AutoSize = true;
            this.labelMechanicalPosition.ForeColor = System.Drawing.Color.Silver;
            this.labelMechanicalPosition.Location = new System.Drawing.Point(134, 217);
            this.labelMechanicalPosition.Name = "labelMechanicalPosition";
            this.labelMechanicalPosition.Size = new System.Drawing.Size(26, 13);
            this.labelMechanicalPosition.TabIndex = 19;
            this.labelMechanicalPosition.Text = "0.0°";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(41, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 20;
            this.label6.Text = "Target Position:";
            // 
            // labelTargetPosition
            // 
            this.labelTargetPosition.AutoSize = true;
            this.labelTargetPosition.ForeColor = System.Drawing.Color.Gold;
            this.labelTargetPosition.Location = new System.Drawing.Point(134, 191);
            this.labelTargetPosition.Name = "labelTargetPosition";
            this.labelTargetPosition.Size = new System.Drawing.Size(26, 13);
            this.labelTargetPosition.TabIndex = 21;
            this.labelTargetPosition.Text = "0.0°";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.ClientSize = new System.Drawing.Size(562, 312);
            this.Controls.Add(this.labelTargetPosition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.labelMechanicalPosition);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonUnsync);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.labelSyncValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSync);
            this.Controls.Add(this.textBoxSync);
            this.Controls.Add(this.checkBoxPower);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stroblhofwarte Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.ComboBox comboBoxComPort;
        private System.Windows.Forms.TextBox textBoxAngle;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Timer timerPosition;
        private System.Windows.Forms.Label labelPosition;
        private System.Windows.Forms.CheckBox checkBoxPower;
        private System.Windows.Forms.TextBox textBoxSync;
        private System.Windows.Forms.Button buttonSync;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSyncValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonUnsync;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelMechanicalPosition;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelTargetPosition;
    }
}