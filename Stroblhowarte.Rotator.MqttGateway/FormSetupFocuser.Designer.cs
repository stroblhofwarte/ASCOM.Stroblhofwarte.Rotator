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
            this.textBoxRightOvershoot = new System.Windows.Forms.TextBox();
            this.textBoxLeftOvershoot = new System.Windows.Forms.TextBox();
            this.buttonRightSet = new System.Windows.Forms.Button();
            this.buttonLeftSet = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxRightOvershoot
            // 
            this.textBoxRightOvershoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxRightOvershoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxRightOvershoot.Location = new System.Drawing.Point(56, 74);
            this.textBoxRightOvershoot.Name = "textBoxRightOvershoot";
            this.textBoxRightOvershoot.Size = new System.Drawing.Size(100, 23);
            this.textBoxRightOvershoot.TabIndex = 0;
            // 
            // textBoxLeftOvershoot
            // 
            this.textBoxLeftOvershoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxLeftOvershoot.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLeftOvershoot.Location = new System.Drawing.Point(56, 132);
            this.textBoxLeftOvershoot.Name = "textBoxLeftOvershoot";
            this.textBoxLeftOvershoot.Size = new System.Drawing.Size(100, 23);
            this.textBoxLeftOvershoot.TabIndex = 1;
            // 
            // buttonRightSet
            // 
            this.buttonRightSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRightSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRightSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonRightSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonRightSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRightSet.Location = new System.Drawing.Point(162, 74);
            this.buttonRightSet.Name = "buttonRightSet";
            this.buttonRightSet.Size = new System.Drawing.Size(57, 23);
            this.buttonRightSet.TabIndex = 2;
            this.buttonRightSet.Text = "set";
            this.buttonRightSet.UseVisualStyleBackColor = false;
            this.buttonRightSet.Click += new System.EventHandler(this.buttonRightSet_Click);
            // 
            // buttonLeftSet
            // 
            this.buttonLeftSet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonLeftSet.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonLeftSet.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonLeftSet.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonLeftSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLeftSet.Location = new System.Drawing.Point(162, 132);
            this.buttonLeftSet.Name = "buttonLeftSet";
            this.buttonLeftSet.Size = new System.Drawing.Size(57, 23);
            this.buttonLeftSet.TabIndex = 3;
            this.buttonLeftSet.Text = "set";
            this.buttonLeftSet.UseVisualStyleBackColor = false;
            this.buttonLeftSet.Click += new System.EventHandler(this.buttonLeftSet_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(250, 178);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Right overshoot:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Left overshoot:";
            // 
            // FormSetupFocuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(345, 212);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonLeftSet);
            this.Controls.Add(this.buttonRightSet);
            this.Controls.Add(this.textBoxLeftOvershoot);
            this.Controls.Add(this.textBoxRightOvershoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormSetupFocuser";
            this.Text = "Setup Focuser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxRightOvershoot;
        private TextBox textBoxLeftOvershoot;
        private Button buttonRightSet;
        private Button buttonLeftSet;
        private Button buttonOk;
        private Label label1;
        private Label label2;
    }
}