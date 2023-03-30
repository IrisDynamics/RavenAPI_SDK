using System;
using System.Windows.Forms;

namespace RavenAPI
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ResponseBox = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.textBox0 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.ModeLabel = new System.Windows.Forms.Label();
            this.ThermalLabel = new System.Windows.Forms.Label();
            this.InitLabel = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.FrameContentBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Change Mode";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ModeChangeRequest);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Items.AddRange(new object[] {
            RavenAPI.ModeStatus.Modes.Off,
            RavenAPI.ModeStatus.Modes.LevelBrake,
            RavenAPI.ModeStatus.Modes.Loading,
            RavenAPI.ModeStatus.Modes.Queuing});
            this.listBox1.Location = new System.Drawing.Point(77, 57);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 69);
            this.listBox1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(215, 74);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Forces Request";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ForcesRequest);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(215, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(142, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Temperature Request";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.TempRequest);
            // 
            // ResponseBox
            // 
            this.ResponseBox.Location = new System.Drawing.Point(78, 434);
            this.ResponseBox.Multiline = true;
            this.ResponseBox.Name = "ResponseBox";
            this.ResponseBox.ReadOnly = true;
            this.ResponseBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResponseBox.Size = new System.Drawing.Size(598, 178);
            this.ResponseBox.TabIndex = 24;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(374, 44);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(142, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "DOF Positions Request";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.DOFPosRequest);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(374, 73);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(142, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Actuator Target Positions";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.TargetPosRequest);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(374, 103);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(142, 23);
            this.button6.TabIndex = 6;
            this.button6.Text = "Actuator Commanded Forces";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.CommandedForcesRequest);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(533, 44);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(142, 23);
            this.button7.TabIndex = 7;
            this.button7.Text = "Actuator Position Request";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.PositionRequest);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Platform Modes";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(533, 74);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(142, 23);
            this.button8.TabIndex = 14;
            this.button8.Text = "Washout Targets Frame";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.WashoutRequest);
            // 
            // textBox0
            // 
            this.textBox0.Location = new System.Drawing.Point(78, 181);
            this.textBox0.Name = "textBox0";
            this.textBox0.Size = new System.Drawing.Size(62, 20);
            this.textBox0.TabIndex = 8;
            this.textBox0.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 204);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Surge";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 181);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(62, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(168, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Sway";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(261, 181);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(62, 20);
            this.textBox2.TabIndex = 10;
            this.textBox2.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(258, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Heave";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(351, 181);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(62, 20);
            this.textBox3.TabIndex = 11;
            this.textBox3.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(348, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Roll";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(437, 181);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(62, 20);
            this.textBox4.TabIndex = 12;
            this.textBox4.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(434, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Pitch";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(524, 181);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(62, 20);
            this.textBox5.TabIndex = 13;
            this.textBox5.TextChanged += new System.EventHandler(this.ParseValue);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(521, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Yaw";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 226);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "Note: Please enter position values in mm";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(533, 103);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(142, 23);
            this.button9.TabIndex = 23;
            this.button9.Text = "Initialize Actuators";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.ActuatorInit);
            // 
            // ModeLabel
            // 
            this.ModeLabel.AutoSize = true;
            this.ModeLabel.Location = new System.Drawing.Point(75, 626);
            this.ModeLabel.Name = "ModeLabel";
            this.ModeLabel.Size = new System.Drawing.Size(81, 13);
            this.ModeLabel.TabIndex = 25;
            this.ModeLabel.Text = "Platform Mode: ";
            // 
            // ThermalLabel
            // 
            this.ThermalLabel.AutoSize = true;
            this.ThermalLabel.Location = new System.Drawing.Point(277, 626);
            this.ThermalLabel.Name = "ThermalLabel";
            this.ThermalLabel.Size = new System.Drawing.Size(81, 13);
            this.ThermalLabel.TabIndex = 26;
            this.ThermalLabel.Text = "Thermal Mode: ";
            // 
            // InitLabel
            // 
            this.InitLabel.AutoSize = true;
            this.InitLabel.Location = new System.Drawing.Point(489, 626);
            this.InitLabel.Name = "InitLabel";
            this.InitLabel.Size = new System.Drawing.Size(97, 13);
            this.InitLabel.TabIndex = 27;
            this.InitLabel.Text = "Initialization Status:";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(215, 132);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(142, 23);
            this.button10.TabIndex = 28;
            this.button10.Text = "Firmware Version";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.FirmwareRequest);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(77, 255);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Request Frame Contents:";
            // 
            // FrameContentBox
            // 
            this.FrameContentBox.BackColor = System.Drawing.SystemColors.Control;
            this.FrameContentBox.Location = new System.Drawing.Point(77, 275);
            this.FrameContentBox.Multiline = true;
            this.FrameContentBox.Name = "FrameContentBox";
            this.FrameContentBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FrameContentBox.Size = new System.Drawing.Size(595, 128);
            this.FrameContentBox.TabIndex = 30;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(77, 415);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Response:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 670);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.FrameContentBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.InitLabel);
            this.Controls.Add(this.ThermalLabel);
            this.Controls.Add(this.ModeLabel);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox0);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.ResponseBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "RavenAPI ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox ResponseBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private Label label1;
        private Button button8;
        private TextBox textBox0;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private TextBox textBox3;
        private Label label5;
        private TextBox textBox4;
        private Label label6;
        private TextBox textBox5;
        private Label label7;
        private Label label8;
        private Button button9;
        private Label ModeLabel;
        private Label ThermalLabel;
        private Label InitLabel;
        private Button button10;
        private Label label9;
        private TextBox FrameContentBox;
        private Label label10;
    }
}

