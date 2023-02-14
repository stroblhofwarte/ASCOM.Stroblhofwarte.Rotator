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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormControl));
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
            this.pictureBoxRotator = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxFocuserGo = new System.Windows.Forms.TextBox();
            this.buttonFocuserGo = new System.Windows.Forms.Button();
            this.buttonFastLeft = new System.Windows.Forms.Button();
            this.buttonSlowLeft = new System.Windows.Forms.Button();
            this.buttonSlowRight = new System.Windows.Forms.Button();
            this.buttonFastRight = new System.Windows.Forms.Button();
            this.labelFocuserPosition = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonExpand = new System.Windows.Forms.Button();
            this.buttonSync = new System.Windows.Forms.Button();
            this.textBoxSync = new System.Windows.Forms.TextBox();
            this.buttonFocuserSetup = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.timerFocuser = new System.Windows.Forms.Timer(this.components);
            this.timerStartup = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxFocuserFix = new System.Windows.Forms.PictureBox();
            this.pictureBoxFocuserMove = new System.Windows.Forms.PictureBox();
            this.labelFocuserMax = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRotator)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFocuserFix)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFocuserMove)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonConnect.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Location = new System.Drawing.Point(571, 30);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // timerRotatorPosition
            // 
            this.timerRotatorPosition.Enabled = true;
            this.timerRotatorPosition.Tick += new System.EventHandler(this.timerRotatorPosition_Tick);
            // 
            // buttonRotatorMoveLeft
            // 
            this.buttonRotatorMoveLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveLeft.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonRotatorMoveLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotatorMoveLeft.Location = new System.Drawing.Point(59, 275);
            this.buttonRotatorMoveLeft.Name = "buttonRotatorMoveLeft";
            this.buttonRotatorMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorMoveLeft.TabIndex = 3;
            this.buttonRotatorMoveLeft.Text = "<";
            this.buttonRotatorMoveLeft.UseVisualStyleBackColor = false;
            this.buttonRotatorMoveLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveLeft_MouseDown);
            this.buttonRotatorMoveLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveLeft_MouseUp);
            // 
            // buttonRotatorMoveRight
            // 
            this.buttonRotatorMoveRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveRight.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonRotatorMoveRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonRotatorMoveRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotatorMoveRight.Location = new System.Drawing.Point(171, 275);
            this.buttonRotatorMoveRight.Name = "buttonRotatorMoveRight";
            this.buttonRotatorMoveRight.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorMoveRight.TabIndex = 4;
            this.buttonRotatorMoveRight.Text = ">";
            this.buttonRotatorMoveRight.UseVisualStyleBackColor = false;
            this.buttonRotatorMoveRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveRight_MouseDown);
            this.buttonRotatorMoveRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.buttonRotatorMoveRight_MouseUp);
            // 
            // textBoxPosition
            // 
            this.textBoxPosition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPosition.Location = new System.Drawing.Point(59, 319);
            this.textBoxPosition.Name = "textBoxPosition";
            this.textBoxPosition.Size = new System.Drawing.Size(128, 23);
            this.textBoxPosition.TabIndex = 5;
            // 
            // buttonGoto
            // 
            this.buttonGoto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonGoto.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonGoto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonGoto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonGoto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGoto.Location = new System.Drawing.Point(185, 318);
            this.buttonGoto.Name = "buttonGoto";
            this.buttonGoto.Size = new System.Drawing.Size(61, 25);
            this.buttonGoto.TabIndex = 6;
            this.buttonGoto.Text = "go";
            this.buttonGoto.UseVisualStyleBackColor = false;
            this.buttonGoto.Click += new System.EventHandler(this.buttonGoto_Click);
            // 
            // buttonRotatorSetup
            // 
            this.buttonRotatorSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonRotatorSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonRotatorSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonRotatorSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotatorSetup.Location = new System.Drawing.Point(571, 153);
            this.buttonRotatorSetup.Name = "buttonRotatorSetup";
            this.buttonRotatorSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonRotatorSetup.TabIndex = 7;
            this.buttonRotatorSetup.Text = ">>";
            this.buttonRotatorSetup.UseVisualStyleBackColor = false;
            this.buttonRotatorSetup.Click += new System.EventHandler(this.buttonRotatorSetup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(472, 157);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 8;
            this.label1.Text = "Rotator Setup:";
            // 
            // buttonBaseSetup
            // 
            this.buttonBaseSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonBaseSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonBaseSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonBaseSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonBaseSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBaseSetup.Location = new System.Drawing.Point(571, 124);
            this.buttonBaseSetup.Name = "buttonBaseSetup";
            this.buttonBaseSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonBaseSetup.TabIndex = 9;
            this.buttonBaseSetup.Text = ">>";
            this.buttonBaseSetup.UseVisualStyleBackColor = false;
            this.buttonBaseSetup.Click += new System.EventHandler(this.buttonBaseSetup_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(449, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Base Device Setup:";
            // 
            // labelPos
            // 
            this.labelPos.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelPos.ForeColor = System.Drawing.Color.Silver;
            this.labelPos.Location = new System.Drawing.Point(48, 35);
            this.labelPos.Name = "labelPos";
            this.labelPos.Size = new System.Drawing.Size(198, 30);
            this.labelPos.TabIndex = 11;
            this.labelPos.Text = "0.0 °";
            this.labelPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(414, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "Connect to MQTT Broker:";
            // 
            // buttonMqtt
            // 
            this.buttonMqtt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMqtt.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonMqtt.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonMqtt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonMqtt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMqtt.Location = new System.Drawing.Point(571, 66);
            this.buttonMqtt.Name = "buttonMqtt";
            this.buttonMqtt.Size = new System.Drawing.Size(75, 23);
            this.buttonMqtt.TabIndex = 13;
            this.buttonMqtt.Text = "Connect";
            this.buttonMqtt.UseVisualStyleBackColor = false;
            this.buttonMqtt.Click += new System.EventHandler(this.buttonMqtt_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(419, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Connect Rotator Device:";
            // 
            // pictureBoxRotator
            // 
            this.pictureBoxRotator.Location = new System.Drawing.Point(45, 68);
            this.pictureBoxRotator.Name = "pictureBoxRotator";
            this.pictureBoxRotator.Size = new System.Drawing.Size(227, 205);
            this.pictureBoxRotator.TabIndex = 16;
            this.pictureBoxRotator.TabStop = false;
            this.pictureBoxRotator.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxRotator_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Gray;
            this.label5.Location = new System.Drawing.Point(12, 358);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(307, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "____________________________________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(12, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 32);
            this.label6.TabIndex = 18;
            this.label6.Text = "Rotator:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(12, 375);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 32);
            this.label7.TabIndex = 19;
            this.label7.Text = "Focuser:";
            // 
            // textBoxFocuserGo
            // 
            this.textBoxFocuserGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxFocuserGo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFocuserGo.Location = new System.Drawing.Point(28, 568);
            this.textBoxFocuserGo.Name = "textBoxFocuserGo";
            this.textBoxFocuserGo.Size = new System.Drawing.Size(128, 23);
            this.textBoxFocuserGo.TabIndex = 20;
            // 
            // buttonFocuserGo
            // 
            this.buttonFocuserGo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserGo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserGo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFocuserGo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonFocuserGo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFocuserGo.Location = new System.Drawing.Point(154, 567);
            this.buttonFocuserGo.Name = "buttonFocuserGo";
            this.buttonFocuserGo.Size = new System.Drawing.Size(61, 25);
            this.buttonFocuserGo.TabIndex = 21;
            this.buttonFocuserGo.Text = "go";
            this.buttonFocuserGo.UseVisualStyleBackColor = false;
            this.buttonFocuserGo.Click += new System.EventHandler(this.buttonFocuserGo_Click);
            // 
            // buttonFastLeft
            // 
            this.buttonFastLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastLeft.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFastLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonFastLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFastLeft.Location = new System.Drawing.Point(30, 618);
            this.buttonFastLeft.Name = "buttonFastLeft";
            this.buttonFastLeft.Size = new System.Drawing.Size(62, 23);
            this.buttonFastLeft.TabIndex = 22;
            this.buttonFastLeft.Text = "<<";
            this.buttonFastLeft.UseVisualStyleBackColor = false;
            this.buttonFastLeft.Click += new System.EventHandler(this.buttonFastLeft_Click);
            // 
            // buttonSlowLeft
            // 
            this.buttonSlowLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSlowLeft.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSlowLeft.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSlowLeft.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSlowLeft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonSlowLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSlowLeft.Location = new System.Drawing.Point(98, 618);
            this.buttonSlowLeft.Name = "buttonSlowLeft";
            this.buttonSlowLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonSlowLeft.TabIndex = 23;
            this.buttonSlowLeft.Text = "<";
            this.buttonSlowLeft.UseVisualStyleBackColor = false;
            this.buttonSlowLeft.Click += new System.EventHandler(this.buttonSlowLeft_Click);
            // 
            // buttonSlowRight
            // 
            this.buttonSlowRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSlowRight.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSlowRight.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSlowRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSlowRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonSlowRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSlowRight.Location = new System.Drawing.Point(179, 618);
            this.buttonSlowRight.Name = "buttonSlowRight";
            this.buttonSlowRight.Size = new System.Drawing.Size(75, 23);
            this.buttonSlowRight.TabIndex = 24;
            this.buttonSlowRight.Text = ">";
            this.buttonSlowRight.UseVisualStyleBackColor = false;
            this.buttonSlowRight.Click += new System.EventHandler(this.buttonSlowRight_Click);
            // 
            // buttonFastRight
            // 
            this.buttonFastRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastRight.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFastRight.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFastRight.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFastRight.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonFastRight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFastRight.Location = new System.Drawing.Point(260, 618);
            this.buttonFastRight.Name = "buttonFastRight";
            this.buttonFastRight.Size = new System.Drawing.Size(62, 23);
            this.buttonFastRight.TabIndex = 25;
            this.buttonFastRight.Text = ">>";
            this.buttonFastRight.UseVisualStyleBackColor = false;
            this.buttonFastRight.Click += new System.EventHandler(this.buttonFastRight_Click);
            // 
            // labelFocuserPosition
            // 
            this.labelFocuserPosition.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelFocuserPosition.ForeColor = System.Drawing.Color.Silver;
            this.labelFocuserPosition.Location = new System.Drawing.Point(23, 428);
            this.labelFocuserPosition.Name = "labelFocuserPosition";
            this.labelFocuserPosition.Size = new System.Drawing.Size(223, 30);
            this.labelFocuserPosition.TabIndex = 26;
            this.labelFocuserPosition.Text = "0.0 ";
            this.labelFocuserPosition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(138, 415);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Focuser Position";
            // 
            // buttonExpand
            // 
            this.buttonExpand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonExpand.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonExpand.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonExpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonExpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExpand.Location = new System.Drawing.Point(343, 109);
            this.buttonExpand.Name = "buttonExpand";
            this.buttonExpand.Size = new System.Drawing.Size(26, 131);
            this.buttonExpand.TabIndex = 28;
            this.buttonExpand.Text = ">";
            this.buttonExpand.UseVisualStyleBackColor = false;
            this.buttonExpand.Click += new System.EventHandler(this.buttonExpand_Click);
            // 
            // buttonSync
            // 
            this.buttonSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSync.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonSync.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonSync.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSync.Location = new System.Drawing.Point(575, 318);
            this.buttonSync.Name = "buttonSync";
            this.buttonSync.Size = new System.Drawing.Size(61, 25);
            this.buttonSync.TabIndex = 30;
            this.buttonSync.Text = "sync";
            this.buttonSync.UseVisualStyleBackColor = false;
            this.buttonSync.Click += new System.EventHandler(this.buttonSync_Click);
            // 
            // textBoxSync
            // 
            this.textBoxSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBoxSync.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSync.Location = new System.Drawing.Point(449, 319);
            this.textBoxSync.Name = "textBoxSync";
            this.textBoxSync.Size = new System.Drawing.Size(128, 23);
            this.textBoxSync.TabIndex = 29;
            // 
            // buttonFocuserSetup
            // 
            this.buttonFocuserSetup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserSetup.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonFocuserSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonFocuserSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.buttonFocuserSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFocuserSetup.Location = new System.Drawing.Point(571, 182);
            this.buttonFocuserSetup.Name = "buttonFocuserSetup";
            this.buttonFocuserSetup.Size = new System.Drawing.Size(75, 23);
            this.buttonFocuserSetup.TabIndex = 31;
            this.buttonFocuserSetup.Text = ">>";
            this.buttonFocuserSetup.UseVisualStyleBackColor = false;
            this.buttonFocuserSetup.Click += new System.EventHandler(this.buttonFocuserSetup_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(470, 186);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 15);
            this.label8.TabIndex = 32;
            this.label8.Text = "Focuser Setup:";
            // 
            // timerFocuser
            // 
            this.timerFocuser.Enabled = true;
            this.timerFocuser.Interval = 300;
            this.timerFocuser.Tick += new System.EventHandler(this.timerFocuser_Tick);
            // 
            // timerStartup
            // 
            this.timerStartup.Enabled = true;
            this.timerStartup.Interval = 500;
            this.timerStartup.Tick += new System.EventHandler(this.timerStartup_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.pictureBoxFocuserFix);
            this.panel1.Controls.Add(this.pictureBoxFocuserMove);
            this.panel1.Location = new System.Drawing.Point(28, 461);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(243, 92);
            this.panel1.TabIndex = 35;
            // 
            // pictureBoxFocuserFix
            // 
            this.pictureBoxFocuserFix.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxFocuserFix.BackgroundImage")));
            this.pictureBoxFocuserFix.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxFocuserFix.InitialImage")));
            this.pictureBoxFocuserFix.Location = new System.Drawing.Point(0, 9);
            this.pictureBoxFocuserFix.Name = "pictureBoxFocuserFix";
            this.pictureBoxFocuserFix.Size = new System.Drawing.Size(150, 80);
            this.pictureBoxFocuserFix.TabIndex = 34;
            this.pictureBoxFocuserFix.TabStop = false;
            // 
            // pictureBoxFocuserMove
            // 
            this.pictureBoxFocuserMove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxFocuserMove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxFocuserMove.BackgroundImage")));
            this.pictureBoxFocuserMove.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxFocuserMove.InitialImage")));
            this.pictureBoxFocuserMove.Location = new System.Drawing.Point(50, 10);
            this.pictureBoxFocuserMove.Name = "pictureBoxFocuserMove";
            this.pictureBoxFocuserMove.Size = new System.Drawing.Size(150, 80);
            this.pictureBoxFocuserMove.TabIndex = 33;
            this.pictureBoxFocuserMove.TabStop = false;
            // 
            // labelFocuserMax
            // 
            this.labelFocuserMax.AutoSize = true;
            this.labelFocuserMax.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelFocuserMax.ForeColor = System.Drawing.Color.Silver;
            this.labelFocuserMax.Location = new System.Drawing.Point(247, 433);
            this.labelFocuserMax.Name = "labelFocuserMax";
            this.labelFocuserMax.Size = new System.Drawing.Size(39, 13);
            this.labelFocuserMax.TabIndex = 36;
            this.labelFocuserMax.Text = "max. []";
            // 
            // FormControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(17)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(665, 668);
            this.Controls.Add(this.labelFocuserMax);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.buttonFocuserSetup);
            this.Controls.Add(this.buttonSync);
            this.Controls.Add(this.textBoxSync);
            this.Controls.Add(this.buttonExpand);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.labelFocuserPosition);
            this.Controls.Add(this.buttonFastRight);
            this.Controls.Add(this.buttonSlowRight);
            this.Controls.Add(this.buttonSlowLeft);
            this.Controls.Add(this.buttonFastLeft);
            this.Controls.Add(this.buttonFocuserGo);
            this.Controls.Add(this.textBoxFocuserGo);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBoxRotator);
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
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormControl";
            this.Text = "Stroblhofwarte.Rotator.MqttGateway";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormControl_FormClosing);
            this.Load += new System.EventHandler(this.FormControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRotator)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFocuserFix)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFocuserMove)).EndInit();
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
        private System.Windows.Forms.Timer timerFocuser;
        private System.Windows.Forms.Timer timerStartup;
        private PictureBox pictureBoxFocuserMove;
        private PictureBox pictureBoxFocuserFix;
        private Panel panel1;
        private Label labelFocuserMax;
    }
}