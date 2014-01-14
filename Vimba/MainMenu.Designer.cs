namespace Vimba
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.actionLog_lb = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.macAdd_txt = new System.Windows.Forms.TextBox();
            this.randomMaxChkBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.adapterCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionLog_lb
            // 
            this.actionLog_lb.BackColor = System.Drawing.SystemColors.MenuText;
            this.actionLog_lb.ForeColor = System.Drawing.SystemColors.Highlight;
            this.actionLog_lb.FormattingEnabled = true;
            this.actionLog_lb.Location = new System.Drawing.Point(665, 383);
            this.actionLog_lb.Name = "actionLog_lb";
            this.actionLog_lb.Size = new System.Drawing.Size(303, 121);
            this.actionLog_lb.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(665, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Action Log";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.Window;
            this.listBox1.ForeColor = System.Drawing.SystemColors.MenuText;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(665, 201);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(303, 121);
            this.listBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 474);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(246, 30);
            this.button1.TabIndex = 4;
            this.button1.Text = "Launch PlanetSide 2";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // macAdd_txt
            // 
            this.macAdd_txt.Location = new System.Drawing.Point(108, 63);
            this.macAdd_txt.Name = "macAdd_txt";
            this.macAdd_txt.Size = new System.Drawing.Size(167, 20);
            this.macAdd_txt.TabIndex = 5;
            // 
            // randomMaxChkBox
            // 
            this.randomMaxChkBox.AutoSize = true;
            this.randomMaxChkBox.Checked = true;
            this.randomMaxChkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.randomMaxChkBox.Location = new System.Drawing.Point(282, 65);
            this.randomMaxChkBox.Name = "randomMaxChkBox";
            this.randomMaxChkBox.Size = new System.Drawing.Size(144, 17);
            this.randomMaxChkBox.TabIndex = 6;
            this.randomMaxChkBox.Text = "Randomize Mac Address";
            this.randomMaxChkBox.UseVisualStyleBackColor = true;
            this.randomMaxChkBox.CheckedChanged += new System.EventHandler(this.randomMaxChkBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Mac Address";
            // 
            // adapterCB
            // 
            this.adapterCB.FormattingEnabled = true;
            this.adapterCB.Location = new System.Drawing.Point(108, 29);
            this.adapterCB.Name = "adapterCB";
            this.adapterCB.Size = new System.Drawing.Size(204, 21);
            this.adapterCB.TabIndex = 8;
            this.adapterCB.SelectedIndexChanged += new System.EventHandler(this.adapterCB_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Adapter";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 528);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.adapterCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.randomMaxChkBox);
            this.Controls.Add(this.macAdd_txt);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.actionLog_lb);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainMenu";
            this.Text = "Vimba: A Planetside 2 Tool";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox actionLog_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox macAdd_txt;
        private System.Windows.Forms.CheckBox randomMaxChkBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox adapterCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

