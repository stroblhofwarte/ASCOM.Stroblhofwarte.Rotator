namespace Stroblhowarte.Rotator.MqttGateway
{
    partial class FormControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.timerRotatorPosition = new System.Windows.Forms.Timer(this.components);
            this.buttonRotatorMoveLeft = new System.Windows.Forms.Button();
            this.buttonRotatorMoveRight = new System.Windows.Forms.Button();
            this.textBoxPosition = new System.Windows.Forms.TextBox();
            this.buttonGoto = new System.Windows.Forms.Button();
            this.buttonRotatorSetup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBaseSetup = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonMqtt = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(670, 23);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // timerRotatorPosition
            // 
            this.timerRotatorPosition.Enabled = true;
            this.timerRotatorPosition.Tick += new System.EventHandler(this.timerRotatorPosition_Tick);
            // 
            // buttonRotatorMoveLeft
            // 
            this.buttonRotatorMoveLeft.Location = new System.Drawing.Point(123, 318);
            this.buttonRotatorMoveLeft.Name = "buttonRotatorMoveLeft";
            this.buttonRotatorMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorMoveLeft.TabIndex = 3;
            this.buttonRotatorMoveLeft.Text = "<";
            this.buttonRotatorMoveLeft.UseVisualStyleBackColor = true;
            this.buttonRotatorMoveLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveLeft_MouseDown);
            this.buttonRotatorMoveLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveLeft_MouseUp);
            // 
            // buttonRotatorMoveRight
            // 
            this.buttonRotatorMoveRight.Location = new System.Drawing.Point(204, 318);
            this.buttonRotatorMoveRight.Name = "buttonRotatorMoveRight";
            this.buttonRotatorMoveRight.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorMoveRight.TabIndex = 4;
            this.buttonRotatorMoveRight.Text = ">";
            this.buttonRotatorMoveRight.UseVisualStyleBackColor = true;
            this.buttonRotatorMoveRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveRight_MouseDown);
            this.buttonRotatorMoveRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveRight_MouseUp);
            // 
            // textBoxPosition
            // 
            this.textBoxPosition.Location = new System.Drawing.Point(37, 23);
            this.textBoxPosition.Name = "textBoxPosition";
            this.textBoxPosition.Size = new System.Drawing.Size(75, 23);
            this.textBoxPosition.TabIndex = 5;
            // 
            // buttonGoto
            // 
            this.buttonGoto.Location = new System.Drawing.Point(118, 23);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(61, 23);
            this.buttonGoto.TabIndex = 6;
            this.buttonGoto.Text = "go";
            this.buttonGoto.UseVisualStyleBackColor = true;
            this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
            // 
            // buttonRotatorSetup
            // 
            this.buttonRotatorSetup.Location = new System.Drawing.Point(670, 146);
            this.buttonRotatorSetup.Name = "buttonRotatorSetup";
            this.buttonRotatorSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorSetup.TabIndex = 7;
            this.buttonRotatorSetup.Text = ">>";
            this.buttonRotatorSetup.UseVisualStyleBackColor = true;
            this.buttonRotatorSetup.Click += new System.EventHandler(this.buttonRotatorSetup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(571, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rotator Setup:";
            // 
            // buttonBaseSetup
            // 
            this.buttonBaseSetup.Location = new System.Drawing.Point(670, 117);
            this.buttonBaseSetup.Name = "buttonBaseSetup";
            this.buttonBaseSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonBaseSetup.TabIndex = 9;
            this.buttonBaseSetup.Text = ">>";
            this.buttonBaseSetup.UseVisualStyleBackColor = true;
            this.buttonBaseSetup.Click += new System.EventHandler(this.buttonBaseSetup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(548, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Base Device Setup:";
            // 
            // labelPos
            // 
            this.labelPos.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelPos.Location = new System.Drawing.Point(123, 63);
            this.labelPos.Name = "labelPos";
            this.labelPos.Size = new System.Drawing.Size(172, 30);
            this.labelPos.TabIndex = 11;
            this.labelPos.Text = "0.0 °";
            this.labelPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(513, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Connect to MQTT Broker:";
            // 
            // buttonMqtt
            // 
            this.buttonMqtt.Location = new System.Drawing.Point(670, 59);
            this.buttonMqtt.Name = "buttonMqtt";
            this.buttonMqtt.Size = new System.Drawing.Size(75, 23);
            this.buttonMqtt.TabIndex = 13;
            this.buttonMqtt.Text = "Connect";
            this.buttonMqtt.UseVisualStyleBackColor = true;
            this.buttonMqtt.Click += new System.EventHandler(this.buttonMqtt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Connect Rotator Device:";
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonMqtt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelPos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBaseSetup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRotatorSetup);
            this.Controls.Add(this.buttonGoto);
            this.Controls.Add(this.textBoxPosition);
            this.Controls.Add(this.buttonRotatorMoveRight);
            this.Controls.Add(this.buttonRotatorMoveLeft);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormControl";
            this.Text = "Stroblhofwarte.Rotator.MqttGateway";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormControl_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button buttonConnect;
        private System.Windows.Forms.Timer timerRotatorPosition;
        private Button buttonRotatorMoveLeft;
        private Button buttonRotatorMoveRight;
        private TextBox textBoxPosition;
        private Button buttonGoto;
        private Button buttonRotatorSetup;
        private Label label1;
        private Button buttonBaseSetup;
        private Label label2;
        private Label labelPos;
        private Label label3;
        private Button buttonMqtt;
        private Label label4;
    }
}