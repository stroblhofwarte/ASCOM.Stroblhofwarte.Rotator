namespace Stroblhowarte.Rotator.MqttGateway
{
    partial class FormSetupDevice
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
            this.comboBoxComPort = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMqttBroker = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMQTTPort = new System.Windows.Forms.TextBox();
            this.buttonMqttTest = new System.Windows.Forms.Button();
            this.labelMqttTestInfo = new System.Windows.Forms.Label();
            this.buttonSerialTest = new System.Windows.Forms.Button();
            this.labelSerialTestInfo = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.checkBoxAutoconnect = new System.Windows.Forms.CheckBox();
            this.checkBoxMqtt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxComPort
            // 
            this.comboBoxComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxComPort.FormattingEnabled = true;
            this.comboBoxComPort.Location = new System.Drawing.Point(36, 69);
            this.comboBoxComPort.Name = "comboBoxComPort";
            this.comboBoxComPort.Size = new System.Drawing.Size(121, 23);
            this.comboBoxComPort.TabIndex = 0;
            this.comboBoxComPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPort_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Serial Port:";
            // 
            // textBoxMqttBroker
            // 
            this.textBoxMqttBroker.Location = new System.Drawing.Point(36, 176);
            this.textBoxMqttBroker.Name = "textBoxMqttBroker";
            this.textBoxMqttBroker.Size = new System.Drawing.Size(181, 23);
            this.textBoxMqttBroker.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "MQTT Host:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(224, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "MQTT Port:";
            // 
            // textBoxMQTTPort
            // 
            this.textBoxMQTTPort.Location = new System.Drawing.Point(224, 176);
            this.textBoxMQTTPort.Name = "textBoxMQTTPort";
            this.textBoxMQTTPort.Size = new System.Drawing.Size(104, 23);
            this.textBoxMQTTPort.TabIndex = 4;
            // 
            // buttonMqttTest
            // 
            this.buttonMqttTest.Location = new System.Drawing.Point(334, 176);
            this.buttonMqttTest.Name = "buttonMqttTest";
            this.buttonMqttTest.Size = new System.Drawing.Size(75, 23);
            this.buttonMqttTest.TabIndex = 6;
            this.buttonMqttTest.Text = "test";
            this.buttonMqttTest.UseVisualStyleBackColor = true;
            this.buttonMqttTest.Click += new System.EventHandler(this.buttonMqttTest_Click);
            // 
            // labelMqttTestInfo
            // 
            this.labelMqttTestInfo.AutoSize = true;
            this.labelMqttTestInfo.Location = new System.Drawing.Point(36, 210);
            this.labelMqttTestInfo.Name = "labelMqttTestInfo";
            this.labelMqttTestInfo.Size = new System.Drawing.Size(16, 15);
            this.labelMqttTestInfo.TabIndex = 7;
            this.labelMqttTestInfo.Text = "...";
            // 
            // buttonSerialTest
            // 
            this.buttonSerialTest.Location = new System.Drawing.Point(163, 68);
            this.buttonSerialTest.Name = "buttonSerialTest";
            this.buttonSerialTest.Size = new System.Drawing.Size(75, 23);
            this.buttonSerialTest.TabIndex = 8;
            this.buttonSerialTest.Text = "test";
            this.buttonSerialTest.UseVisualStyleBackColor = true;
            this.buttonSerialTest.Click += new System.EventHandler(this.buttonSerialTest_Click);
            // 
            // labelSerialTestInfo
            // 
            this.labelSerialTestInfo.AutoSize = true;
            this.labelSerialTestInfo.Location = new System.Drawing.Point(36, 95);
            this.labelSerialTestInfo.Name = "labelSerialTestInfo";
            this.labelSerialTestInfo.Size = new System.Drawing.Size(16, 15);
            this.labelSerialTestInfo.TabIndex = 9;
            this.labelSerialTestInfo.Text = "...";
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(424, 249);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // checkBoxAutoconnect
            // 
            this.checkBoxAutoconnect.AutoSize = true;
            this.checkBoxAutoconnect.Location = new System.Drawing.Point(256, 71);
            this.checkBoxAutoconnect.Name = "checkBoxAutoconnect";
            this.checkBoxAutoconnect.Size = new System.Drawing.Size(93, 19);
            this.checkBoxAutoconnect.TabIndex = 11;
            this.checkBoxAutoconnect.Text = "autoconnect";
            this.checkBoxAutoconnect.UseVisualStyleBackColor = true;
            this.checkBoxAutoconnect.CheckedChanged += new System.EventHandler(this.checkBoxAutoconnect_CheckedChanged);
            // 
            // checkBoxMqtt
            // 
            this.checkBoxMqtt.AutoSize = true;
            this.checkBoxMqtt.Location = new System.Drawing.Point(36, 128);
            this.checkBoxMqtt.Name = "checkBoxMqtt";
            this.checkBoxMqtt.Size = new System.Drawing.Size(95, 19);
            this.checkBoxMqtt.TabIndex = 12;
            this.checkBoxMqtt.Text = "Enable MQTT";
            this.checkBoxMqtt.UseVisualStyleBackColor = true;
            this.checkBoxMqtt.CheckedChanged += new System.EventHandler(this.checkBoxMqtt_CheckedChanged);
            // 
            // FormSetupDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 295);
            this.Controls.Add(this.checkBoxMqtt);
            this.Controls.Add(this.checkBoxAutoconnect);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.labelSerialTestInfo);
            this.Controls.Add(this.buttonSerialTest);
            this.Controls.Add(this.labelMqttTestInfo);
            this.Controls.Add(this.buttonMqttTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxMQTTPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxMqttBroker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxComPort);
            this.Name = "FormSetupDevice";
            this.Text = "Setup Device";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComboBox comboBoxComPort;
        private Label label1;
        private TextBox textBoxMqttBroker;
        private Label label2;
        private Label label3;
        private TextBox textBoxMQTTPort;
        private Button buttonMqttTest;
        private Label labelMqttTestInfo;
        private Button buttonSerialTest;
        private Label labelSerialTestInfo;
        private Button buttonOk;
        private CheckBox checkBoxAutoconnect;
        private CheckBox checkBoxMqtt;
    }
}