namespace Stroblhofwarte.Rotator.MqttGateway
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControl));
            buttonConnect = new Button();
            buttonRotatorMoveLeft = new Button();
            buttonRotatorMoveRight = new Button();
            textBoxPosition = new TextBox();
            buttonGoto = new Button();
            buttonRotatorSetup = new Button();
            label1 = new Label();
            buttonBaseSetup = new Button();
            label2 = new Label();
            labelPos = new Label();
            label3 = new Label();
            buttonMqtt = new Button();
            label4 = new Label();
            pictureBoxRotator = new PictureBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            textBoxFocuserGo = new TextBox();
            buttonFocuserGo = new Button();
            buttonFastLeft = new Button();
            buttonSlowLeft = new Button();
            buttonSlowRight = new Button();
            buttonFastRight = new Button();
            labelFocuserPosition = new Label();
            label9 = new Label();
            buttonExpand = new Button();
            buttonSync = new Button();
            textBoxSync = new TextBox();
            buttonFocuserSetup = new Button();
            label8 = new Label();
            timer = new System.Windows.Forms.Timer(components);
            timerStartup = new System.Windows.Forms.Timer(components);
            labelFocuserMax = new Label();
            checkBoxMoff = new CheckBox();
            label10 = new Label();
            progressBarFocuser = new ProgressBar();
            label11 = new Label();
            label12 = new Label();
            btnBroker = new Button();
            label13 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxRotator).BeginInit();
            SuspendLayout();
            // 
            // buttonConnect
            // 
            buttonConnect.BackColor = Color.FromArgb(255, 128, 0);
            buttonConnect.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonConnect.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonConnect.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonConnect.FlatStyle = FlatStyle.Flat;
            buttonConnect.Location = new Point(571, 30);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(75, 23);
            buttonConnect.TabIndex = 1;
            buttonConnect.Text = "Connect";
            buttonConnect.UseVisualStyleBackColor = false;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonRotatorMoveLeft
            // 
            buttonRotatorMoveLeft.BackColor = Color.FromArgb(255, 128, 0);
            buttonRotatorMoveLeft.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonRotatorMoveLeft.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonRotatorMoveLeft.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonRotatorMoveLeft.FlatStyle = FlatStyle.Flat;
            buttonRotatorMoveLeft.Location = new Point(59, 275);
            buttonRotatorMoveLeft.Name = "buttonRotatorMoveLeft";
            buttonRotatorMoveLeft.Size = new Size(75, 23);
            buttonRotatorMoveLeft.TabIndex = 3;
            buttonRotatorMoveLeft.Text = "<";
            buttonRotatorMoveLeft.UseVisualStyleBackColor = false;
            buttonRotatorMoveLeft.MouseDown += buttonRotatorMoveLeft_MouseDown;
            buttonRotatorMoveLeft.MouseUp += buttonRotatorMoveLeft_MouseUp;
            // 
            // buttonRotatorMoveRight
            // 
            buttonRotatorMoveRight.BackColor = Color.FromArgb(255, 128, 0);
            buttonRotatorMoveRight.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonRotatorMoveRight.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonRotatorMoveRight.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonRotatorMoveRight.FlatStyle = FlatStyle.Flat;
            buttonRotatorMoveRight.Location = new Point(171, 275);
            buttonRotatorMoveRight.Name = "buttonRotatorMoveRight";
            buttonRotatorMoveRight.Size = new Size(75, 23);
            buttonRotatorMoveRight.TabIndex = 4;
            buttonRotatorMoveRight.Text = ">";
            buttonRotatorMoveRight.UseVisualStyleBackColor = false;
            buttonRotatorMoveRight.MouseDown += buttonRotatorMoveRight_MouseDown;
            buttonRotatorMoveRight.MouseUp += buttonRotatorMoveRight_MouseUp;
            // 
            // textBoxPosition
            // 
            textBoxPosition.BackColor = Color.FromArgb(255, 192, 128);
            textBoxPosition.BorderStyle = BorderStyle.FixedSingle;
            textBoxPosition.Location = new Point(59, 319);
            textBoxPosition.Name = "textBoxPosition";
            textBoxPosition.Size = new Size(128, 23);
            textBoxPosition.TabIndex = 5;
            // 
            // buttonGoto
            // 
            buttonGoto.BackColor = Color.FromArgb(255, 128, 0);
            buttonGoto.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonGoto.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonGoto.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonGoto.FlatStyle = FlatStyle.Flat;
            buttonGoto.Location = new Point(185, 318);
            buttonGoto.Name = "buttonGoto";
            buttonGoto.Size = new Size(61, 25);
            buttonGoto.TabIndex = 6;
            buttonGoto.Text = "go";
            buttonGoto.UseVisualStyleBackColor = false;
            buttonGoto.Click += buttonGoto_Click;
            // 
            // buttonRotatorSetup
            // 
            buttonRotatorSetup.BackColor = Color.FromArgb(255, 128, 0);
            buttonRotatorSetup.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonRotatorSetup.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonRotatorSetup.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonRotatorSetup.FlatStyle = FlatStyle.Flat;
            buttonRotatorSetup.Location = new Point(571, 153);
            buttonRotatorSetup.Name = "buttonRotatorSetup";
            buttonRotatorSetup.Size = new Size(75, 23);
            buttonRotatorSetup.TabIndex = 7;
            buttonRotatorSetup.Text = ">>";
            buttonRotatorSetup.UseVisualStyleBackColor = false;
            buttonRotatorSetup.Click += buttonRotatorSetup_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Silver;
            label1.Location = new Point(472, 157);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 8;
            label1.Text = "Rotator Setup:";
            // 
            // buttonBaseSetup
            // 
            buttonBaseSetup.BackColor = Color.FromArgb(255, 128, 0);
            buttonBaseSetup.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonBaseSetup.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonBaseSetup.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonBaseSetup.FlatStyle = FlatStyle.Flat;
            buttonBaseSetup.Location = new Point(571, 124);
            buttonBaseSetup.Name = "buttonBaseSetup";
            buttonBaseSetup.Size = new Size(75, 23);
            buttonBaseSetup.TabIndex = 9;
            buttonBaseSetup.Text = ">>";
            buttonBaseSetup.UseVisualStyleBackColor = false;
            buttonBaseSetup.Click += buttonBaseSetup_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Silver;
            label2.Location = new Point(449, 128);
            label2.Name = "label2";
            label2.Size = new Size(105, 15);
            label2.TabIndex = 10;
            label2.Text = "Base Device Setup:";
            // 
            // labelPos
            // 
            labelPos.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            labelPos.ForeColor = Color.Silver;
            labelPos.Location = new Point(48, 35);
            labelPos.Name = "labelPos";
            labelPos.Size = new Size(198, 30);
            labelPos.TabIndex = 11;
            labelPos.Text = "0.0 °";
            labelPos.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.Silver;
            label3.Location = new Point(414, 70);
            label3.Name = "label3";
            label3.Size = new Size(142, 15);
            label3.TabIndex = 12;
            label3.Text = "Connect to MQTT Broker:";
            // 
            // buttonMqtt
            // 
            buttonMqtt.BackColor = Color.FromArgb(255, 128, 0);
            buttonMqtt.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonMqtt.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonMqtt.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonMqtt.FlatStyle = FlatStyle.Flat;
            buttonMqtt.Location = new Point(571, 66);
            buttonMqtt.Name = "buttonMqtt";
            buttonMqtt.Size = new Size(75, 23);
            buttonMqtt.TabIndex = 13;
            buttonMqtt.Text = "Connect";
            buttonMqtt.UseVisualStyleBackColor = false;
            buttonMqtt.Click += buttonMqtt_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.Silver;
            label4.Location = new Point(419, 34);
            label4.Name = "label4";
            label4.Size = new Size(135, 15);
            label4.TabIndex = 14;
            label4.Text = "Connect Rotator Device:";
            // 
            // pictureBoxRotator
            // 
            pictureBoxRotator.Location = new Point(45, 68);
            pictureBoxRotator.Name = "pictureBoxRotator";
            pictureBoxRotator.Size = new Size(227, 205);
            pictureBoxRotator.TabIndex = 16;
            pictureBoxRotator.TabStop = false;
            pictureBoxRotator.Paint += pictureBoxRotator_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(12, 358);
            label5.Name = "label5";
            label5.Size = new Size(327, 15);
            label5.TabIndex = 17;
            label5.Text = "________________________________________________________________";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label6.ForeColor = Color.Silver;
            label6.Location = new Point(12, 6);
            label6.Name = "label6";
            label6.Size = new Size(108, 32);
            label6.TabIndex = 18;
            label6.Text = "Rotator:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label7.ForeColor = Color.Silver;
            label7.Location = new Point(12, 375);
            label7.Name = "label7";
            label7.Size = new Size(110, 32);
            label7.TabIndex = 19;
            label7.Text = "Focuser:";
            // 
            // textBoxFocuserGo
            // 
            textBoxFocuserGo.BackColor = Color.FromArgb(255, 192, 128);
            textBoxFocuserGo.BorderStyle = BorderStyle.FixedSingle;
            textBoxFocuserGo.Location = new Point(28, 568);
            textBoxFocuserGo.Name = "textBoxFocuserGo";
            textBoxFocuserGo.Size = new Size(128, 23);
            textBoxFocuserGo.TabIndex = 20;
            // 
            // buttonFocuserGo
            // 
            buttonFocuserGo.BackColor = Color.FromArgb(255, 128, 0);
            buttonFocuserGo.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonFocuserGo.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonFocuserGo.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonFocuserGo.FlatStyle = FlatStyle.Flat;
            buttonFocuserGo.Location = new Point(154, 567);
            buttonFocuserGo.Name = "buttonFocuserGo";
            buttonFocuserGo.Size = new Size(61, 25);
            buttonFocuserGo.TabIndex = 21;
            buttonFocuserGo.Text = "go";
            buttonFocuserGo.UseVisualStyleBackColor = false;
            buttonFocuserGo.Click += buttonFocuserGo_Click;
            // 
            // buttonFastLeft
            // 
            buttonFastLeft.BackColor = Color.FromArgb(255, 128, 0);
            buttonFastLeft.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonFastLeft.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonFastLeft.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonFastLeft.FlatStyle = FlatStyle.Flat;
            buttonFastLeft.Location = new Point(30, 618);
            buttonFastLeft.Name = "buttonFastLeft";
            buttonFastLeft.Size = new Size(62, 23);
            buttonFastLeft.TabIndex = 22;
            buttonFastLeft.Text = "<<";
            buttonFastLeft.UseVisualStyleBackColor = false;
            buttonFastLeft.Click += buttonFastLeft_Click;
            // 
            // buttonSlowLeft
            // 
            buttonSlowLeft.BackColor = Color.FromArgb(255, 128, 0);
            buttonSlowLeft.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonSlowLeft.FlatAppearance.CheckedBackColor = Color.FromArgb(192, 64, 0);
            buttonSlowLeft.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonSlowLeft.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonSlowLeft.FlatStyle = FlatStyle.Flat;
            buttonSlowLeft.Location = new Point(98, 618);
            buttonSlowLeft.Name = "buttonSlowLeft";
            buttonSlowLeft.Size = new Size(75, 23);
            buttonSlowLeft.TabIndex = 23;
            buttonSlowLeft.Text = "<";
            buttonSlowLeft.UseVisualStyleBackColor = false;
            buttonSlowLeft.Click += buttonSlowLeft_Click;
            // 
            // buttonSlowRight
            // 
            buttonSlowRight.BackColor = Color.FromArgb(255, 128, 0);
            buttonSlowRight.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonSlowRight.FlatAppearance.CheckedBackColor = Color.FromArgb(192, 64, 0);
            buttonSlowRight.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonSlowRight.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonSlowRight.FlatStyle = FlatStyle.Flat;
            buttonSlowRight.Location = new Point(179, 618);
            buttonSlowRight.Name = "buttonSlowRight";
            buttonSlowRight.Size = new Size(75, 23);
            buttonSlowRight.TabIndex = 24;
            buttonSlowRight.Text = ">";
            buttonSlowRight.UseVisualStyleBackColor = false;
            buttonSlowRight.Click += buttonSlowRight_Click;
            // 
            // buttonFastRight
            // 
            buttonFastRight.BackColor = Color.FromArgb(255, 128, 0);
            buttonFastRight.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonFastRight.FlatAppearance.CheckedBackColor = Color.FromArgb(192, 64, 0);
            buttonFastRight.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonFastRight.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonFastRight.FlatStyle = FlatStyle.Flat;
            buttonFastRight.Location = new Point(260, 618);
            buttonFastRight.Name = "buttonFastRight";
            buttonFastRight.Size = new Size(62, 23);
            buttonFastRight.TabIndex = 25;
            buttonFastRight.Text = ">>";
            buttonFastRight.UseVisualStyleBackColor = false;
            buttonFastRight.Click += buttonFastRight_Click;
            // 
            // labelFocuserPosition
            // 
            labelFocuserPosition.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            labelFocuserPosition.ForeColor = Color.Silver;
            labelFocuserPosition.Location = new Point(23, 428);
            labelFocuserPosition.Name = "labelFocuserPosition";
            labelFocuserPosition.Size = new Size(223, 30);
            labelFocuserPosition.TabIndex = 26;
            labelFocuserPosition.Text = "0.0 ";
            labelFocuserPosition.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8.25F);
            label9.ForeColor = Color.Silver;
            label9.Location = new Point(138, 415);
            label9.Name = "label9";
            label9.Size = new Size(92, 13);
            label9.TabIndex = 27;
            label9.Text = "Focuser Position";
            // 
            // buttonExpand
            // 
            buttonExpand.BackColor = Color.FromArgb(255, 128, 0);
            buttonExpand.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonExpand.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonExpand.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonExpand.FlatStyle = FlatStyle.Flat;
            buttonExpand.Location = new Point(343, 109);
            buttonExpand.Name = "buttonExpand";
            buttonExpand.Size = new Size(26, 131);
            buttonExpand.TabIndex = 28;
            buttonExpand.Text = ">";
            buttonExpand.UseVisualStyleBackColor = false;
            buttonExpand.Click += buttonExpand_Click;
            // 
            // buttonSync
            // 
            buttonSync.BackColor = Color.FromArgb(255, 128, 0);
            buttonSync.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonSync.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonSync.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonSync.FlatStyle = FlatStyle.Flat;
            buttonSync.Location = new Point(575, 318);
            buttonSync.Name = "buttonSync";
            buttonSync.Size = new Size(61, 25);
            buttonSync.TabIndex = 30;
            buttonSync.Text = "sync";
            buttonSync.UseVisualStyleBackColor = false;
            buttonSync.Click += buttonSync_Click;
            // 
            // textBoxSync
            // 
            textBoxSync.BackColor = Color.FromArgb(255, 192, 128);
            textBoxSync.BorderStyle = BorderStyle.FixedSingle;
            textBoxSync.Location = new Point(449, 319);
            textBoxSync.Name = "textBoxSync";
            textBoxSync.Size = new Size(128, 23);
            textBoxSync.TabIndex = 29;
            // 
            // buttonFocuserSetup
            // 
            buttonFocuserSetup.BackColor = Color.FromArgb(255, 128, 0);
            buttonFocuserSetup.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            buttonFocuserSetup.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            buttonFocuserSetup.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            buttonFocuserSetup.FlatStyle = FlatStyle.Flat;
            buttonFocuserSetup.Location = new Point(571, 182);
            buttonFocuserSetup.Name = "buttonFocuserSetup";
            buttonFocuserSetup.Size = new Size(75, 23);
            buttonFocuserSetup.TabIndex = 31;
            buttonFocuserSetup.Text = ">>";
            buttonFocuserSetup.UseVisualStyleBackColor = false;
            buttonFocuserSetup.Click += buttonFocuserSetup_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.Silver;
            label8.Location = new Point(470, 186);
            label8.Name = "label8";
            label8.Size = new Size(84, 15);
            label8.TabIndex = 32;
            label8.Text = "Focuser Setup:";
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 300;
            timer.Tick += timer_Tick;
            // 
            // timerStartup
            // 
            timerStartup.Enabled = true;
            timerStartup.Interval = 500;
            timerStartup.Tick += timerStartup_Tick;
            // 
            // labelFocuserMax
            // 
            labelFocuserMax.AutoSize = true;
            labelFocuserMax.Font = new Font("Segoe UI", 8.25F);
            labelFocuserMax.ForeColor = Color.Silver;
            labelFocuserMax.Location = new Point(247, 433);
            labelFocuserMax.Name = "labelFocuserMax";
            labelFocuserMax.Size = new Size(39, 13);
            labelFocuserMax.TabIndex = 36;
            labelFocuserMax.Text = "max. []";
            // 
            // checkBoxMoff
            // 
            checkBoxMoff.AutoSize = true;
            checkBoxMoff.ForeColor = Color.Silver;
            checkBoxMoff.Location = new Point(128, 388);
            checkBoxMoff.Name = "checkBoxMoff";
            checkBoxMoff.Size = new Size(165, 19);
            checkBoxMoff.TabIndex = 37;
            checkBoxMoff.Text = "Power off after movement";
            checkBoxMoff.UseVisualStyleBackColor = true;
            checkBoxMoff.CheckedChanged += checkBoxMoff_CheckedChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(0, 0);
            label10.Name = "label10";
            label10.Size = new Size(44, 15);
            label10.TabIndex = 38;
            label10.Text = "label10";
            // 
            // progressBarFocuser
            // 
            progressBarFocuser.ForeColor = Color.DarkBlue;
            progressBarFocuser.Location = new Point(29, 507);
            progressBarFocuser.Name = "progressBarFocuser";
            progressBarFocuser.Size = new Size(293, 10);
            progressBarFocuser.Style = ProgressBarStyle.Continuous;
            progressBarFocuser.TabIndex = 39;
            progressBarFocuser.Value = 10;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.Silver;
            label11.Location = new Point(28, 479);
            label11.Name = "label11";
            label11.Size = new Size(106, 15);
            label11.TabIndex = 40;
            label11.Text = "focal point in front";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.Silver;
            label12.Location = new Point(216, 479);
            label12.Name = "label12";
            label12.Size = new Size(104, 15);
            label12.TabIndex = 41;
            label12.Text = "focal point behind";
            // 
            // btnBroker
            // 
            btnBroker.BackColor = Color.FromArgb(255, 128, 0);
            btnBroker.FlatAppearance.BorderColor = Color.FromArgb(255, 128, 0);
            btnBroker.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 64, 0);
            btnBroker.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 192, 128);
            btnBroker.FlatStyle = FlatStyle.Flat;
            btnBroker.Location = new Point(571, 232);
            btnBroker.Name = "btnBroker";
            btnBroker.Size = new Size(75, 23);
            btnBroker.TabIndex = 43;
            btnBroker.Text = "Start";
            btnBroker.UseVisualStyleBackColor = false;
            btnBroker.Click += btnBroker_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.ForeColor = Color.Silver;
            label13.Location = new Point(474, 236);
            label13.Name = "label13";
            label13.Size = new Size(80, 15);
            label13.TabIndex = 42;
            label13.Text = "MQTT Broker:";
            // 
            // FormControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 17, 40);
            ClientSize = new Size(665, 668);
            Controls.Add(btnBroker);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(progressBarFocuser);
            Controls.Add(label10);
            Controls.Add(checkBoxMoff);
            Controls.Add(labelFocuserMax);
            Controls.Add(label8);
            Controls.Add(buttonFocuserSetup);
            Controls.Add(buttonSync);
            Controls.Add(textBoxSync);
            Controls.Add(buttonExpand);
            Controls.Add(label9);
            Controls.Add(labelFocuserPosition);
            Controls.Add(buttonFastRight);
            Controls.Add(buttonSlowRight);
            Controls.Add(buttonSlowLeft);
            Controls.Add(buttonFastLeft);
            Controls.Add(buttonFocuserGo);
            Controls.Add(textBoxFocuserGo);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(pictureBoxRotator);
            Controls.Add(label4);
            Controls.Add(buttonMqtt);
            Controls.Add(label3);
            Controls.Add(labelPos);
            Controls.Add(label2);
            Controls.Add(buttonBaseSetup);
            Controls.Add(label1);
            Controls.Add(buttonRotatorSetup);
            Controls.Add(buttonGoto);
            Controls.Add(textBoxPosition);
            Controls.Add(buttonRotatorMoveRight);
            Controls.Add(buttonRotatorMoveLeft);
            Controls.Add(buttonConnect);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormControl";
            Text = "Stroblhofwarte.Rotator.MqttGateway";
            FormClosing += FormControl_FormClosing;
            Load += FormControl_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxRotator).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private Button buttonConnect;
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
        private PictureBox pictureBoxRotator;
        private Label label5;
        private Label label6;
        private Label label7;
        private TextBox textBoxFocuserGo;
        private Button buttonFocuserGo;
        private Button buttonFastLeft;
        private Button buttonSlowLeft;
        private Button buttonSlowRight;
        private Button buttonFastRight;
        private Label labelFocuserPosition;
        private Label label9;
        private Button buttonExpand;
        private Button buttonSync;
        private TextBox textBoxSync;
        private Button buttonFocuserSetup;
        private Label label8;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerStartup;
        private Label labelFocuserMax;
        private CheckBox checkBoxMoff;
        private Label label10;
        private ProgressBar progressBarFocuser;
        private Label label11;
        private Label label12;
        private Button btnBroker;
        private Label label13;
    }
}