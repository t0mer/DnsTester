namespace DNSTester
{
    partial class TesterUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TesterUI));
            this.ResultView = new System.Windows.Forms.ListView();
            this.WebName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DNS1IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DNS1Timing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DNS2IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DNS2Timing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RunTest = new MetroFramework.Controls.MetroButton();
            this.lblDns1 = new MetroFramework.Controls.MetroLabel();
            this.lblDns2 = new MetroFramework.Controls.MetroLabel();
            this.DnsList1 = new MetroFramework.Controls.MetroComboBox();
            this.DnsList2 = new MetroFramework.Controls.MetroComboBox();
            this.UseCustomServers = new MetroFramework.Controls.MetroCheckBox();
            this.lblCustom1 = new MetroFramework.Controls.MetroLabel();
            this.lblCustom2 = new MetroFramework.Controls.MetroLabel();
            this.CustomDns1 = new MetroFramework.Controls.MetroTextBox();
            this.CustomDns2 = new MetroFramework.Controls.MetroTextBox();
            this.lslStatus = new MetroFramework.Controls.MetroLabel();
            this.StatusBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ResultView
            // 
            this.ResultView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WebName,
            this.DNS1IP,
            this.DNS1Timing,
            this.DNS2IP,
            this.DNS2Timing});
            this.ResultView.GridLines = true;
            this.ResultView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ResultView.HideSelection = false;
            this.ResultView.Location = new System.Drawing.Point(58, 159);
            this.ResultView.MultiSelect = false;
            this.ResultView.Name = "ResultView";
            this.ResultView.Size = new System.Drawing.Size(831, 318);
            this.ResultView.TabIndex = 11;
            this.ResultView.UseCompatibleStateImageBehavior = false;
            this.ResultView.View = System.Windows.Forms.View.Details;
            // 
            // WebName
            // 
            this.WebName.Text = "URL";
            this.WebName.Width = 160;
            // 
            // DNS1IP
            // 
            this.DNS1IP.Text = "IP Address from DNS 1";
            this.DNS1IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DNS1IP.Width = 140;
            // 
            // DNS1Timing
            // 
            this.DNS1Timing.Text = "DNS 1 Timing";
            this.DNS1Timing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DNS1Timing.Width = 120;
            // 
            // DNS2IP
            // 
            this.DNS2IP.Text = "IP Address from DNS 2";
            this.DNS2IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DNS2IP.Width = 140;
            // 
            // DNS2Timing
            // 
            this.DNS2Timing.Text = "DNS 2 Timing";
            this.DNS2Timing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DNS2Timing.Width = 120;
            // 
            // RunTest
            // 
            this.RunTest.Location = new System.Drawing.Point(424, 483);
            this.RunTest.Name = "RunTest";
            this.RunTest.Size = new System.Drawing.Size(75, 23);
            this.RunTest.TabIndex = 12;
            this.RunTest.Text = "Run Test";
            this.RunTest.Theme = MetroFramework.MetroThemeStyle.Light;
            this.RunTest.Click += new System.EventHandler(this.RunTest_Click);
            // 
            // lblDns1
            // 
            this.lblDns1.AutoSize = true;
            this.lblDns1.Location = new System.Drawing.Point(58, 70);
            this.lblDns1.Name = "lblDns1";
            this.lblDns1.Size = new System.Drawing.Size(42, 19);
            this.lblDns1.TabIndex = 13;
            this.lblDns1.Text = "Dns 1:";
            // 
            // lblDns2
            // 
            this.lblDns2.AutoSize = true;
            this.lblDns2.Location = new System.Drawing.Point(411, 70);
            this.lblDns2.Name = "lblDns2";
            this.lblDns2.Size = new System.Drawing.Size(44, 19);
            this.lblDns2.TabIndex = 14;
            this.lblDns2.Text = "Dns 2:";
            // 
            // DnsList1
            // 
            this.DnsList1.FormattingEnabled = true;
            this.DnsList1.ItemHeight = 23;
            this.DnsList1.Location = new System.Drawing.Point(104, 65);
            this.DnsList1.Name = "DnsList1";
            this.DnsList1.Size = new System.Drawing.Size(286, 29);
            this.DnsList1.TabIndex = 15;
            // 
            // DnsList2
            // 
            this.DnsList2.FormattingEnabled = true;
            this.DnsList2.ItemHeight = 23;
            this.DnsList2.Location = new System.Drawing.Point(461, 65);
            this.DnsList2.Name = "DnsList2";
            this.DnsList2.Size = new System.Drawing.Size(298, 29);
            this.DnsList2.TabIndex = 16;
            // 
            // UseCustomServers
            // 
            this.UseCustomServers.AutoSize = true;
            this.UseCustomServers.Location = new System.Drawing.Point(784, 70);
            this.UseCustomServers.Name = "UseCustomServers";
            this.UseCustomServers.Size = new System.Drawing.Size(105, 15);
            this.UseCustomServers.TabIndex = 17;
            this.UseCustomServers.Text = "Custom Servers";
            this.UseCustomServers.UseVisualStyleBackColor = true;
            this.UseCustomServers.CheckedChanged += new System.EventHandler(this.UseCustomServers_CheckedChanged);
            // 
            // lblCustom1
            // 
            this.lblCustom1.AutoSize = true;
            this.lblCustom1.Location = new System.Drawing.Point(58, 123);
            this.lblCustom1.Name = "lblCustom1";
            this.lblCustom1.Size = new System.Drawing.Size(95, 19);
            this.lblCustom1.TabIndex = 18;
            this.lblCustom1.Text = "Custom Dns 1: ";
            this.lblCustom1.Visible = false;
            // 
            // lblCustom2
            // 
            this.lblCustom2.AutoSize = true;
            this.lblCustom2.Location = new System.Drawing.Point(411, 123);
            this.lblCustom2.Name = "lblCustom2";
            this.lblCustom2.Size = new System.Drawing.Size(97, 19);
            this.lblCustom2.TabIndex = 19;
            this.lblCustom2.Text = "Custom Dns 2: ";
            this.lblCustom2.Visible = false;
            // 
            // CustomDns1
            // 
            this.CustomDns1.BackColor = System.Drawing.Color.White;
            this.CustomDns1.Enabled = false;
            this.CustomDns1.Location = new System.Drawing.Point(153, 121);
            this.CustomDns1.Name = "CustomDns1";
            this.CustomDns1.Size = new System.Drawing.Size(237, 23);
            this.CustomDns1.TabIndex = 20;
            this.CustomDns1.Visible = false;
            // 
            // CustomDns2
            // 
            this.CustomDns2.Enabled = false;
            this.CustomDns2.Location = new System.Drawing.Point(506, 121);
            this.CustomDns2.Name = "CustomDns2";
            this.CustomDns2.Size = new System.Drawing.Size(253, 23);
            this.CustomDns2.TabIndex = 21;
            this.CustomDns2.Visible = false;
            // 
            // lslStatus
            // 
            this.lslStatus.AutoSize = true;
            this.lslStatus.Location = new System.Drawing.Point(58, 511);
            this.lslStatus.Name = "lslStatus";
            this.lslStatus.Size = new System.Drawing.Size(46, 19);
            this.lslStatus.TabIndex = 22;
            this.lslStatus.Text = "Status:";
            // 
            // StatusBox
            // 
            this.StatusBox.FormattingEnabled = true;
            this.StatusBox.HorizontalScrollbar = true;
            this.StatusBox.Location = new System.Drawing.Point(58, 533);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(831, 160);
            this.StatusBox.TabIndex = 23;
            // 
            // TesterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Drawing.MetroBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(944, 710);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.lslStatus);
            this.Controls.Add(this.CustomDns2);
            this.Controls.Add(this.CustomDns1);
            this.Controls.Add(this.lblCustom2);
            this.Controls.Add(this.lblCustom1);
            this.Controls.Add(this.UseCustomServers);
            this.Controls.Add(this.DnsList2);
            this.Controls.Add(this.DnsList1);
            this.Controls.Add(this.lblDns2);
            this.Controls.Add(this.lblDns1);
            this.Controls.Add(this.RunTest);
            this.Controls.Add(this.ResultView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "TesterUI";
            this.Text = "DNSTester";
            this.Load += new System.EventHandler(this.TesterUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ResultView;
        private System.Windows.Forms.ColumnHeader WebName;
        private System.Windows.Forms.ColumnHeader DNS1IP;
        private System.Windows.Forms.ColumnHeader DNS1Timing;
        private System.Windows.Forms.ColumnHeader DNS2IP;
        private System.Windows.Forms.ColumnHeader DNS2Timing;
        private MetroFramework.Controls.MetroButton RunTest;
        private MetroFramework.Controls.MetroLabel lblDns1;
        private MetroFramework.Controls.MetroLabel lblDns2;
        private MetroFramework.Controls.MetroComboBox DnsList1;
        private MetroFramework.Controls.MetroComboBox DnsList2;
        private MetroFramework.Controls.MetroCheckBox UseCustomServers;
        private MetroFramework.Controls.MetroLabel lblCustom1;
        private MetroFramework.Controls.MetroLabel lblCustom2;
        private MetroFramework.Controls.MetroTextBox CustomDns1;
        private MetroFramework.Controls.MetroTextBox CustomDns2;
        private MetroFramework.Controls.MetroLabel lslStatus;
        private System.Windows.Forms.ListBox StatusBox;
    }
}

