namespace WindowsFormsClient
{
    partial class Client
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
            this.Connect_Button = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.ServerIPBox = new System.Windows.Forms.TextBox();
            this.AX = new System.Windows.Forms.TextBox();
            this.AY = new System.Windows.Forms.TextBox();
            this.AZ = new System.Windows.Forms.TextBox();
            this.BX = new System.Windows.Forms.TextBox();
            this.BY = new System.Windows.Forms.TextBox();
            this.BZ = new System.Windows.Forms.TextBox();
            this.DistanceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CalcButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Connect_Button
            // 
            this.Connect_Button.Location = new System.Drawing.Point(88, 48);
            this.Connect_Button.Name = "Connect_Button";
            this.Connect_Button.Size = new System.Drawing.Size(77, 29);
            this.Connect_Button.TabIndex = 0;
            this.Connect_Button.Text = "Connect";
            this.Connect_Button.UseVisualStyleBackColor = true;
            this.Connect_Button.Click += new System.EventHandler(this.Connect_Button_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Enabled = false;
            this.DisconnectButton.Location = new System.Drawing.Point(163, 48);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(77, 29);
            this.DisconnectButton.TabIndex = 1;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // ServerIPBox
            // 
            this.ServerIPBox.Location = new System.Drawing.Point(88, 24);
            this.ServerIPBox.Name = "ServerIPBox";
            this.ServerIPBox.Size = new System.Drawing.Size(152, 20);
            this.ServerIPBox.TabIndex = 2;
            this.ServerIPBox.TextChanged += new System.EventHandler(this.ServerIPBox_TextChanged);
            // 
            // AX
            // 
            this.AX.Location = new System.Drawing.Point(61, 117);
            this.AX.Name = "AX";
            this.AX.Size = new System.Drawing.Size(55, 20);
            this.AX.TabIndex = 3;
            // 
            // AY
            // 
            this.AY.Location = new System.Drawing.Point(132, 117);
            this.AY.Name = "AY";
            this.AY.Size = new System.Drawing.Size(55, 20);
            this.AY.TabIndex = 4;
            this.AY.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // AZ
            // 
            this.AZ.Location = new System.Drawing.Point(204, 117);
            this.AZ.Name = "AZ";
            this.AZ.Size = new System.Drawing.Size(55, 20);
            this.AZ.TabIndex = 5;
            // 
            // BX
            // 
            this.BX.Location = new System.Drawing.Point(61, 143);
            this.BX.Name = "BX";
            this.BX.Size = new System.Drawing.Size(55, 20);
            this.BX.TabIndex = 6;
            // 
            // BY
            // 
            this.BY.Location = new System.Drawing.Point(132, 143);
            this.BY.Name = "BY";
            this.BY.Size = new System.Drawing.Size(55, 20);
            this.BY.TabIndex = 7;
            // 
            // BZ
            // 
            this.BZ.Location = new System.Drawing.Point(204, 143);
            this.BZ.Name = "BZ";
            this.BZ.Size = new System.Drawing.Size(55, 20);
            this.BZ.TabIndex = 8;
            // 
            // DistanceTextBox
            // 
            this.DistanceTextBox.Location = new System.Drawing.Point(61, 204);
            this.DistanceTextBox.Name = "DistanceTextBox";
            this.DistanceTextBox.ReadOnly = true;
            this.DistanceTextBox.Size = new System.Drawing.Size(198, 20);
            this.DistanceTextBox.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Point A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Point B";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(4, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Distance";
            // 
            // CalcButton
            // 
            this.CalcButton.Enabled = false;
            this.CalcButton.Location = new System.Drawing.Point(61, 169);
            this.CalcButton.Name = "CalcButton";
            this.CalcButton.Size = new System.Drawing.Size(198, 29);
            this.CalcButton.TabIndex = 13;
            this.CalcButton.Text = "Calculate";
            this.CalcButton.UseVisualStyleBackColor = true;
            this.CalcButton.Click += new System.EventHandler(this.CalcButton_Click);
            // 
            // Client
            // 
            this.AccessibleName = "";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 261);
            this.Controls.Add(this.CalcButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DistanceTextBox);
            this.Controls.Add(this.BZ);
            this.Controls.Add(this.BY);
            this.Controls.Add(this.BX);
            this.Controls.Add(this.AZ);
            this.Controls.Add(this.AY);
            this.Controls.Add(this.AX);
            this.Controls.Add(this.ServerIPBox);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.Connect_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Client";
            this.Text = "Client";
            this.Load += new System.EventHandler(this.Client_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Connect_Button;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.TextBox ServerIPBox;
        private System.Windows.Forms.TextBox AX;
        private System.Windows.Forms.TextBox AY;
        private System.Windows.Forms.TextBox AZ;
        private System.Windows.Forms.TextBox BX;
        private System.Windows.Forms.TextBox BY;
        private System.Windows.Forms.TextBox BZ;
        private System.Windows.Forms.TextBox DistanceTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CalcButton;
    }
}

