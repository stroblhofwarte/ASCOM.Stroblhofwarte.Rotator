
// This file is part of the Stroblhofwarte.Rotator project 
// (https://github.com/stroblhofwarte/ASCOM.Stroblhofwarte.Rotator).
// Copyright (c) 2021, Othmar Ehrhardt, https://astro.stroblhof-oberrohrbach.de
//
// This program is free software: you can redistribute it and/or modify  
// it under the terms of the GNU General Public License as published by  
// the Free Software Foundation, version 3.
//
// This program is distributed in the hope that it will be useful, but 
// WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU 
// General Public License for more details.
//
// You should have received a copy of the GNU General Public License 
// along with this program. If not, see <http://www.gnu.org/licenses/>.
//

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
            this.buttonSetPark = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonPark = new System.Windows.Forms.Button();
            this.textBoxSpeed = new System.Windows.Forms.TextBox();
            this.textBoxInitSpeed = new System.Windows.Forms.TextBox();
            this.buttonSetSpeed = new System.Windows.Forms.Button();
            this.buttonSetInitSpeed = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMaxMove = new System.Windows.Forms.TextBox();
            this.buttonSetMaxMovement = new System.Windows.Forms.Button();
            this.buttonMoveRight = new System.Windows.Forms.Button();
            this.timerMove = new System.Windows.Forms.Timer(this.components);
            this.buttonMoveLeft = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTelescopeId = new System.Windows.Forms.TextBox();
            this.buttonTelescopeChoose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelAltAzInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.BackColor = System.Drawing.Color.Gray;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdOK.Location = new System.Drawing.Point(678, 445);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(71, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.Gray;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdCancel.Location = new System.Drawing.Point(604, 445);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(68, 24);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(570, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.ForeColor = System.Drawing.Color.Silver;
            this.chkTrace.Location = new System.Drawing.Point(519, 449);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(573, 41);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(90, 21);
            this.comboBoxComPort.TabIndex = 7;
            // 
            // textBoxAngle
            // 
            this.textBoxAngle.Location = new System.Drawing.Point(249, 449);
            this.textBoxAngle.Name = "textBoxAngle";
            this.textBoxAngle.Size = new System.Drawing.Size(100, 20);
            this.textBoxAngle.TabIndex = 8;
            this.textBoxAngle.Text = "0.0";
            // 
            // buttonMove
            // 
            this.buttonMove.BackColor = System.Drawing.Color.Gray;
            this.buttonMove.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMove.Location = new System.Drawing.Point(355, 446);
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
            this.labelPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPosition.ForeColor = System.Drawing.Color.Silver;
            this.labelPosition.Location = new System.Drawing.Point(190, 338);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(54, 25);
            this.labelPosition.TabIndex = 10;
            this.labelPosition.Text = "0.0°";
            // 
            // checkBoxPower
            // 
            this.checkBoxPower.AutoSize = true;
            this.checkBoxPower.ForeColor = System.Drawing.Color.Silver;
            this.checkBoxPower.Location = new System.Drawing.Point(573, 68);
            this.checkBoxPower.Name = "checkBoxPower";
            this.checkBoxPower.Size = new System.Drawing.Size(210, 17);
            this.checkBoxPower.TabIndex = 11;
            this.checkBoxPower.Text = "Don\'t switch of motor power after move";
            this.checkBoxPower.UseVisualStyleBackColor = true;
            this.checkBoxPower.CheckStateChanged += new System.EventHandler(this.checkBoxPower_CheckStateChanged);
            // 
            // textBoxSync
            // 
            this.textBoxSync.Location = new System.Drawing.Point(23, 41);
            this.textBoxSync.Name = "textBoxSync";
            this.textBoxSync.Size = new System.Drawing.Size(100, 20);
            this.textBoxSync.TabIndex = 12;
            this.textBoxSync.Text = "0.0";
            // 
            // buttonSync
            // 
            this.buttonSync.BackColor = System.Drawing.Color.Gray;
            this.buttonSync.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSync.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSync.Location = new System.Drawing.Point(124, 39);
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
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(116, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Position:";
            // 
            // buttonSetPark
            // 
            this.buttonSetPark.BackColor = System.Drawing.Color.Gray;
            this.buttonSetPark.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetPark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetPark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetPark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetPark.Location = new System.Drawing.Point(674, 288);
            this.buttonSetPark.Name = "buttonSetPark";
            this.buttonSetPark.Size = new System.Drawing.Size(75, 23);
            this.buttonSetPark.TabIndex = 22;
            this.buttonSetPark.Text = "Set";
            this.buttonSetPark.UseVisualStyleBackColor = false;
            this.buttonSetPark.Click += new System.EventHandler(this.buttonSetPark_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(571, 272);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(176, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Set park position to current position:";
            // 
            // buttonPark
            // 
            this.buttonPark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonPark.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.buttonPark.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonPark.Location = new System.Drawing.Point(673, 328);
            this.buttonPark.Name = "buttonPark";
            this.buttonPark.Size = new System.Drawing.Size(75, 23);
            this.buttonPark.TabIndex = 24;
            this.buttonPark.Text = "Park";
            this.buttonPark.UseVisualStyleBackColor = false;
            this.buttonPark.Click += new System.EventHandler(this.buttonPark_Click);
            // 
            // textBoxSpeed
            // 
            this.textBoxSpeed.Location = new System.Drawing.Point(572, 115);
            this.textBoxSpeed.Name = "textBoxSpeed";
            this.textBoxSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxSpeed.TabIndex = 25;
            this.textBoxSpeed.Text = "1.0";
            // 
            // textBoxInitSpeed
            // 
            this.textBoxInitSpeed.Location = new System.Drawing.Point(573, 163);
            this.textBoxInitSpeed.Name = "textBoxInitSpeed";
            this.textBoxInitSpeed.Size = new System.Drawing.Size(100, 20);
            this.textBoxInitSpeed.TabIndex = 26;
            this.textBoxInitSpeed.Text = "1.0";
            // 
            // buttonSetSpeed
            // 
            this.buttonSetSpeed.BackColor = System.Drawing.Color.Gray;
            this.buttonSetSpeed.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetSpeed.Location = new System.Drawing.Point(673, 113);
            this.buttonSetSpeed.Name = "buttonSetSpeed";
            this.buttonSetSpeed.Size = new System.Drawing.Size(75, 23);
            this.buttonSetSpeed.TabIndex = 27;
            this.buttonSetSpeed.Text = "Set";
            this.buttonSetSpeed.UseVisualStyleBackColor = false;
            this.buttonSetSpeed.Click += new System.EventHandler(this.buttonSetSpeed_Click);
            // 
            // buttonSetInitSpeed
            // 
            this.buttonSetInitSpeed.BackColor = System.Drawing.Color.Gray;
            this.buttonSetInitSpeed.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetInitSpeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetInitSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetInitSpeed.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetInitSpeed.Location = new System.Drawing.Point(674, 161);
            this.buttonSetInitSpeed.Name = "buttonSetInitSpeed";
            this.buttonSetInitSpeed.Size = new System.Drawing.Size(75, 23);
            this.buttonSetInitSpeed.TabIndex = 28;
            this.buttonSetInitSpeed.Text = "Set";
            this.buttonSetInitSpeed.UseVisualStyleBackColor = false;
            this.buttonSetInitSpeed.Click += new System.EventHandler(this.buttonSetInitSpeed_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(570, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(172, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Speed factor for normal movement:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(571, 144);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 13);
            this.label9.TabIndex = 30;
            this.label9.Text = "Speed factor for init movement:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Silver;
            this.label10.Location = new System.Drawing.Point(571, 194);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(124, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Maximal movement [in °]:";
            // 
            // txtMaxMove
            // 
            this.txtMaxMove.Location = new System.Drawing.Point(574, 210);
            this.txtMaxMove.Name = "txtMaxMove";
            this.txtMaxMove.Size = new System.Drawing.Size(99, 20);
            this.txtMaxMove.TabIndex = 31;
            this.txtMaxMove.Text = "1.0";
            // 
            // buttonSetMaxMovement
            // 
            this.buttonSetMaxMovement.BackColor = System.Drawing.Color.Gray;
            this.buttonSetMaxMovement.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetMaxMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetMaxMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetMaxMovement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetMaxMovement.Location = new System.Drawing.Point(674, 208);
            this.buttonSetMaxMovement.Name = "buttonSetMaxMovement";
            this.buttonSetMaxMovement.Size = new System.Drawing.Size(75, 23);
            this.buttonSetMaxMovement.TabIndex = 33;
            this.buttonSetMaxMovement.Text = "Set";
            this.buttonSetMaxMovement.UseVisualStyleBackColor = false;
            this.buttonSetMaxMovement.Click += new System.EventHandler(this.buttonSetMaxMovement_Click);
            // 
            // buttonMoveRight
            // 
            this.buttonMoveRight.BackColor = System.Drawing.Color.Gray;
            this.buttonMoveRight.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonMoveRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMoveRight.Location = new System.Drawing.Point(114, 433);
            this.buttonMoveRight.Name = "buttonMoveRight";
            this.buttonMoveRight.Size = new System.Drawing.Size(75, 37);
            this.buttonMoveRight.TabIndex = 34;
            this.buttonMoveRight.Text = ">";
            this.buttonMoveRight.UseVisualStyleBackColor = false;
            this.buttonMoveRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMoveRight_MouseDown);
            this.buttonMoveRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonMoveRight_MouseUp);
            // 
            // timerMove
            // 
            this.timerMove.Enabled = true;
            this.timerMove.Tick += new System.EventHandler(this.timerMove_Tick);
            // 
            // buttonMoveLeft
            // 
            this.buttonMoveLeft.BackColor = System.Drawing.Color.Gray;
            this.buttonMoveLeft.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonMoveLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMoveLeft.Location = new System.Drawing.Point(23, 433);
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Size = new System.Drawing.Size(75, 37);
            this.buttonMoveLeft.TabIndex = 35;
            this.buttonMoveLeft.Text = "<";
            this.buttonMoveLeft.UseVisualStyleBackColor = false;
            this.buttonMoveLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonMoveLeft_MouseDown);
            this.buttonMoveLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonMoveLeft_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ASCOM.Stroblhofwarte.Properties.Resources.ASCOM;
            this.pictureBox1.Location = new System.Drawing.Point(894, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 34;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(20, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Synchronize Rotator with given angle [°]";
            // 
            // textBoxTelescopeId
            // 
            this.textBoxTelescopeId.Location = new System.Drawing.Point(574, 386);
            this.textBoxTelescopeId.Name = "textBoxTelescopeId";
            this.textBoxTelescopeId.Size = new System.Drawing.Size(100, 20);
            this.textBoxTelescopeId.TabIndex = 36;
            // 
            // buttonTelescopeChoose
            // 
            this.buttonTelescopeChoose.BackColor = System.Drawing.Color.Gray;
            this.buttonTelescopeChoose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonTelescopeChoose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTelescopeChoose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTelescopeChoose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonTelescopeChoose.Location = new System.Drawing.Point(675, 384);
            this.buttonTelescopeChoose.Name = "buttonTelescopeChoose";
            this.buttonTelescopeChoose.Size = new System.Drawing.Size(75, 23);
            this.buttonTelescopeChoose.TabIndex = 37;
            this.buttonTelescopeChoose.Text = "Choose";
            this.buttonTelescopeChoose.UseVisualStyleBackColor = false;
            this.buttonTelescopeChoose.Click += new System.EventHandler(this.buttonTelescopeChoose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(572, 368);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 13);
            this.label1.TabIndex = 38;
            this.label1.Text = "Telescope mount for derotation:";
            // 
            // labelAltAzInfo
            // 
            this.labelAltAzInfo.AutoSize = true;
            this.labelAltAzInfo.ForeColor = System.Drawing.Color.Silver;
            this.labelAltAzInfo.Location = new System.Drawing.Point(572, 410);
            this.labelAltAzInfo.Name = "labelAltAzInfo";
            this.labelAltAzInfo.Size = new System.Drawing.Size(10, 13);
            this.labelAltAzInfo.TabIndex = 39;
            this.labelAltAzInfo.Text = ",";
            // 
            // SetupDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(801, 489);
            this.Controls.Add(this.labelAltAzInfo);
            this.Controls.Add(this.textBoxTelescopeId);
            this.Controls.Add(this.buttonTelescopeChoose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonSetMaxMovement);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSpeed);
            this.Controls.Add(this.buttonSetPark);
            this.Controls.Add(this.textBoxInitSpeed);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonSetSpeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.buttonSetInitSpeed);
            this.Controls.Add(this.buttonPark);
            this.Controls.Add(this.txtMaxMove);
            this.Controls.Add(this.textBoxAngle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSync);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.buttonMoveLeft);
            this.Controls.Add(this.comboBoxComPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSync);
            this.Controls.Add(this.checkBoxPower);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.buttonMoveRight);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stroblhofwarte.Rotator ASCOM Panel";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SetupDialogForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
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
        private System.Windows.Forms.Button buttonSetPark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonPark;
        private System.Windows.Forms.TextBox textBoxSpeed;
        private System.Windows.Forms.TextBox textBoxInitSpeed;
        private System.Windows.Forms.Button buttonSetSpeed;
        private System.Windows.Forms.Button buttonSetInitSpeed;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMaxMove;
        private System.Windows.Forms.Button buttonSetMaxMovement;
        private System.Windows.Forms.Button buttonMoveRight;
        private System.Windows.Forms.Timer timerMove;
        private System.Windows.Forms.Button buttonMoveLeft;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTelescopeId;
        private System.Windows.Forms.Button buttonTelescopeChoose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelAltAzInfo;
    }
}
