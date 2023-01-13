
namespace ASCOM.Stroblhofwarte
{
    partial class SetupFocuserForm
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
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFineSteps = new System.Windows.Forms.TextBox();
            this.textBoxFastSteps = new System.Windows.Forms.TextBox();
            this.buttonSetFineSteps = new System.Windows.Forms.Button();
            this.buttonSetFastSteps = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonFineLeft = new System.Windows.Forms.Button();
            this.buttonFineRight = new System.Windows.Forms.Button();
            this.buttonFastLeft = new System.Windows.Forms.Button();
            this.buttonFastRight = new System.Windows.Forms.Button();
            this.textBoxMoveAbsolute = new System.Windows.Forms.TextBox();
            this.buttonMoveAbsolute = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.BackColor = System.Drawing.Color.Gray;
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmdOK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdOK.Location = new System.Drawing.Point(567, 327);
            this.cmdOK.Margin = new System.Windows.Forms.Padding(4);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(89, 30);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = false;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.Color.Gray;
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.cmdCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cmdCancel.Location = new System.Drawing.Point(470, 326);
            this.cmdCancel.Margin = new System.Windows.Forms.Padding(4);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(89, 31);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = global::ASCOM.Stroblhofwarte.Properties.Resources.ASCOM;
            this.picASCOM.Location = new System.Drawing.Point(608, 11);
            this.picASCOM.Margin = new System.Windows.Forms.Padding(4);
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
            this.label2.Location = new System.Drawing.Point(22, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Comm Port:";
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(367, 333);
            this.chkTrace.Margin = new System.Windows.Forms.Padding(4);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(95, 21);
            this.chkTrace.TabIndex = 6;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(121, 50);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = " Is set from ASCOM.Stroblhofwarte.Rotator!";
            // 
            // textBoxFineSteps
            // 
            this.textBoxFineSteps.Location = new System.Drawing.Point(422, 138);
            this.textBoxFineSteps.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFineSteps.Name = "textBoxFineSteps";
            this.textBoxFineSteps.Size = new System.Drawing.Size(132, 22);
            this.textBoxFineSteps.TabIndex = 31;
            this.textBoxFineSteps.Text = "1.0";
            // 
            // textBoxFastSteps
            // 
            this.textBoxFastSteps.Location = new System.Drawing.Point(423, 197);
            this.textBoxFastSteps.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFastSteps.Name = "textBoxFastSteps";
            this.textBoxFastSteps.Size = new System.Drawing.Size(132, 22);
            this.textBoxFastSteps.TabIndex = 32;
            this.textBoxFastSteps.Text = "1.0";
            // 
            // buttonSetFineSteps
            // 
            this.buttonSetFineSteps.BackColor = System.Drawing.Color.Gray;
            this.buttonSetFineSteps.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetFineSteps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetFineSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetFineSteps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetFineSteps.Location = new System.Drawing.Point(556, 135);
            this.buttonSetFineSteps.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetFineSteps.Name = "buttonSetFineSteps";
            this.buttonSetFineSteps.Size = new System.Drawing.Size(100, 28);
            this.buttonSetFineSteps.TabIndex = 33;
            this.buttonSetFineSteps.Text = "Set";
            this.buttonSetFineSteps.UseVisualStyleBackColor = false;
            this.buttonSetFineSteps.Click += new System.EventHandler(this.buttonSetFineSteps_Click);
            // 
            // buttonSetFastSteps
            // 
            this.buttonSetFastSteps.BackColor = System.Drawing.Color.Gray;
            this.buttonSetFastSteps.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonSetFastSteps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetFastSteps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetFastSteps.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSetFastSteps.Location = new System.Drawing.Point(558, 194);
            this.buttonSetFastSteps.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSetFastSteps.Name = "buttonSetFastSteps";
            this.buttonSetFastSteps.Size = new System.Drawing.Size(100, 28);
            this.buttonSetFastSteps.TabIndex = 34;
            this.buttonSetFastSteps.Text = "Set";
            this.buttonSetFastSteps.UseVisualStyleBackColor = false;
            this.buttonSetFastSteps.Click += new System.EventHandler(this.buttonSetFastSteps_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(419, 115);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 17);
            this.label8.TabIndex = 35;
            this.label8.Text = "Steps for fine movements:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(420, 173);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(197, 17);
            this.label9.TabIndex = 36;
            this.label9.Text = "Steps for fast movements:";
            // 
            // buttonFineLeft
            // 
            this.buttonFineLeft.BackColor = System.Drawing.Color.Gray;
            this.buttonFineLeft.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonFineLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFineLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFineLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFineLeft.Location = new System.Drawing.Point(25, 199);
            this.buttonFineLeft.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFineLeft.Name = "buttonFineLeft";
            this.buttonFineLeft.Size = new System.Drawing.Size(100, 28);
            this.buttonFineLeft.TabIndex = 37;
            this.buttonFineLeft.Text = "<";
            this.buttonFineLeft.UseVisualStyleBackColor = false;
            this.buttonFineLeft.Click += new System.EventHandler(this.buttonFineLeft_Click);
            // 
            // buttonFineRight
            // 
            this.buttonFineRight.BackColor = System.Drawing.Color.Gray;
            this.buttonFineRight.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonFineRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFineRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFineRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFineRight.Location = new System.Drawing.Point(133, 199);
            this.buttonFineRight.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFineRight.Name = "buttonFineRight";
            this.buttonFineRight.Size = new System.Drawing.Size(100, 28);
            this.buttonFineRight.TabIndex = 38;
            this.buttonFineRight.Text = ">";
            this.buttonFineRight.UseVisualStyleBackColor = false;
            this.buttonFineRight.Click += new System.EventHandler(this.buttonFineRight_Click);
            // 
            // buttonFastLeft
            // 
            this.buttonFastLeft.BackColor = System.Drawing.Color.Gray;
            this.buttonFastLeft.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonFastLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFastLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFastLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastLeft.Location = new System.Drawing.Point(25, 250);
            this.buttonFastLeft.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFastLeft.Name = "buttonFastLeft";
            this.buttonFastLeft.Size = new System.Drawing.Size(100, 28);
            this.buttonFastLeft.TabIndex = 39;
            this.buttonFastLeft.Text = "<<";
            this.buttonFastLeft.UseVisualStyleBackColor = false;
            this.buttonFastLeft.Click += new System.EventHandler(this.buttonFastLeft_Click);
            // 
            // buttonFastRight
            // 
            this.buttonFastRight.BackColor = System.Drawing.Color.Gray;
            this.buttonFastRight.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonFastRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFastRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFastRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastRight.Location = new System.Drawing.Point(133, 250);
            this.buttonFastRight.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFastRight.Name = "buttonFastRight";
            this.buttonFastRight.Size = new System.Drawing.Size(100, 28);
            this.buttonFastRight.TabIndex = 40;
            this.buttonFastRight.Text = ">>";
            this.buttonFastRight.UseVisualStyleBackColor = false;
            this.buttonFastRight.Click += new System.EventHandler(this.buttonFastRight_Click);
            // 
            // textBoxMoveAbsolute
            // 
            this.textBoxMoveAbsolute.Location = new System.Drawing.Point(25, 135);
            this.textBoxMoveAbsolute.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxMoveAbsolute.Name = "textBoxMoveAbsolute";
            this.textBoxMoveAbsolute.Size = new System.Drawing.Size(132, 22);
            this.textBoxMoveAbsolute.TabIndex = 41;
            this.textBoxMoveAbsolute.Text = "1.0";
            // 
            // buttonMoveAbsolute
            // 
            this.buttonMoveAbsolute.BackColor = System.Drawing.Color.Gray;
            this.buttonMoveAbsolute.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.buttonMoveAbsolute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveAbsolute.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMoveAbsolute.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMoveAbsolute.Location = new System.Drawing.Point(165, 132);
            this.buttonMoveAbsolute.Margin = new System.Windows.Forms.Padding(4);
            this.buttonMoveAbsolute.Name = "buttonMoveAbsolute";
            this.buttonMoveAbsolute.Size = new System.Drawing.Size(100, 28);
            this.buttonMoveAbsolute.TabIndex = 42;
            this.buttonMoveAbsolute.Text = "Set";
            this.buttonMoveAbsolute.UseVisualStyleBackColor = false;
            this.buttonMoveAbsolute.Click += new System.EventHandler(this.buttonMoveAbsolute_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(22, 95);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 17);
            this.label1.TabIndex = 43;
            this.label1.Text = "Position:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPosition.ForeColor = System.Drawing.Color.Silver;
            this.labelPosition.Location = new System.Drawing.Point(101, 95);
            this.labelPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(69, 20);
            this.labelPosition.TabIndex = 44;
            this.labelPosition.Text = "<POS>";
            // 
            // SetupFocuserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(692, 370);
            this.Controls.Add(this.labelPosition);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonMoveAbsolute);
            this.Controls.Add(this.textBoxMoveAbsolute);
            this.Controls.Add(this.buttonFastRight);
            this.Controls.Add(this.buttonFastLeft);
            this.Controls.Add(this.buttonFineRight);
            this.Controls.Add(this.buttonFineLeft);
            this.Controls.Add(this.textBoxFineSteps);
            this.Controls.Add(this.textBoxFastSteps);
            this.Controls.Add(this.buttonSetFineSteps);
            this.Controls.Add(this.buttonSetFastSteps);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Silver;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupFocuserForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stroblhof.Focuser Setup";
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdOK;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.PictureBox picASCOM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFineSteps;
        private System.Windows.Forms.TextBox textBoxFastSteps;
        private System.Windows.Forms.Button buttonSetFineSteps;
        private System.Windows.Forms.Button buttonSetFastSteps;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonFineLeft;
        private System.Windows.Forms.Button buttonFineRight;
        private System.Windows.Forms.Button buttonFastLeft;
        private System.Windows.Forms.Button buttonFastRight;
        private System.Windows.Forms.TextBox textBoxMoveAbsolute;
        private System.Windows.Forms.Button buttonMoveAbsolute;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelPosition;
    }

}