namespace UPS_Monitor
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.checkBoxAutoScroll = new System.Windows.Forms.CheckBox();
            this.buttonShowLog = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonPauseOneHour = new System.Windows.Forms.Button();
            this.buttonEditConfig = new System.Windows.Forms.Button();
            this.buttonLoadConfig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // listBoxLog
            // 
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.ItemHeight = 16;
            this.listBoxLog.Location = new System.Drawing.Point(145, 15);
            this.listBoxLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(573, 244);
            this.listBoxLog.TabIndex = 0;
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(5, 122);
            this.buttonClearLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(132, 28);
            this.buttonClearLog.TabIndex = 1;
            this.buttonClearLog.Text = "Clear Log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            // 
            // checkBoxAutoScroll
            // 
            this.checkBoxAutoScroll.AutoSize = true;
            this.checkBoxAutoScroll.Location = new System.Drawing.Point(5, 236);
            this.checkBoxAutoScroll.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkBoxAutoScroll.Name = "checkBoxAutoScroll";
            this.checkBoxAutoScroll.Size = new System.Drawing.Size(111, 20);
            this.checkBoxAutoScroll.TabIndex = 2;
            this.checkBoxAutoScroll.Text = "Auto scroll log";
            this.checkBoxAutoScroll.UseVisualStyleBackColor = true;
            // 
            // buttonShowLog
            // 
            this.buttonShowLog.Location = new System.Drawing.Point(5, 86);
            this.buttonShowLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonShowLog.Name = "buttonShowLog";
            this.buttonShowLog.Size = new System.Drawing.Size(132, 28);
            this.buttonShowLog.TabIndex = 3;
            this.buttonShowLog.Text = "Show Log";
            this.buttonShowLog.UseVisualStyleBackColor = true;
            this.buttonShowLog.Click += new System.EventHandler(this.buttonShowLog_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(5, 50);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(133, 28);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonPauseOneHour
            // 
            this.buttonPauseOneHour.Location = new System.Drawing.Point(5, 15);
            this.buttonPauseOneHour.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPauseOneHour.Name = "buttonPauseOneHour";
            this.buttonPauseOneHour.Size = new System.Drawing.Size(133, 28);
            this.buttonPauseOneHour.TabIndex = 5;
            this.buttonPauseOneHour.Text = "Snooze one hour";
            this.buttonPauseOneHour.UseVisualStyleBackColor = true;
            this.buttonPauseOneHour.Click += new System.EventHandler(this.buttonPauseOneHour_Click);
            // 
            // buttonEditConfig
            // 
            this.buttonEditConfig.Location = new System.Drawing.Point(6, 158);
            this.buttonEditConfig.Margin = new System.Windows.Forms.Padding(4);
            this.buttonEditConfig.Name = "buttonEditConfig";
            this.buttonEditConfig.Size = new System.Drawing.Size(132, 28);
            this.buttonEditConfig.TabIndex = 6;
            this.buttonEditConfig.Text = "Edit Config";
            this.buttonEditConfig.UseVisualStyleBackColor = true;
            this.buttonEditConfig.Click += new System.EventHandler(this.buttonEditConfig_Click);
            // 
            // buttonLoadConfig
            // 
            this.buttonLoadConfig.Location = new System.Drawing.Point(6, 194);
            this.buttonLoadConfig.Margin = new System.Windows.Forms.Padding(4);
            this.buttonLoadConfig.Name = "buttonLoadConfig";
            this.buttonLoadConfig.Size = new System.Drawing.Size(132, 28);
            this.buttonLoadConfig.TabIndex = 7;
            this.buttonLoadConfig.Text = "Load New Config";
            this.buttonLoadConfig.UseVisualStyleBackColor = true;
            this.buttonLoadConfig.Click += new System.EventHandler(this.buttonLoadConfig_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 269);
            this.Controls.Add(this.buttonLoadConfig);
            this.Controls.Add(this.buttonEditConfig);
            this.Controls.Add(this.buttonPauseOneHour);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonShowLog);
            this.Controls.Add(this.checkBoxAutoScroll);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.listBoxLog);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Windows NAS Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.CheckBox checkBoxAutoScroll;
        private System.Windows.Forms.Button buttonShowLog;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonPauseOneHour;
        private System.Windows.Forms.Button buttonEditConfig;
        private System.Windows.Forms.Button buttonLoadConfig;
    }
}

