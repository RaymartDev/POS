
namespace POSBiz
{
    partial class CustomerForm9
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerForm9));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.old = new MetroFramework.Controls.MetroTextBox();
            this.newPass = new MetroFramework.Controls.MetroTextBox();
            this.newPassConfirm = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 40);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(324, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 40);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Change Password";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(12, 182);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(123, 34);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(186, 182);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 34);
            this.button1.TabIndex = 11;
            this.button1.Text = "Cancel";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // old
            // 
            // 
            // 
            // 
            this.old.CustomButton.Image = null;
            this.old.CustomButton.Location = new System.Drawing.Point(265, 2);
            this.old.CustomButton.Name = "";
            this.old.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.old.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.old.CustomButton.TabIndex = 1;
            this.old.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.old.CustomButton.UseSelectable = true;
            this.old.CustomButton.Visible = false;
            this.old.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.old.Lines = new string[0];
            this.old.Location = new System.Drawing.Point(16, 65);
            this.old.MaxLength = 32767;
            this.old.Name = "old";
            this.old.PasswordChar = '*';
            this.old.PromptText = "Old Password";
            this.old.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.old.SelectedText = "";
            this.old.SelectionLength = 0;
            this.old.SelectionStart = 0;
            this.old.ShortcutsEnabled = true;
            this.old.Size = new System.Drawing.Size(293, 30);
            this.old.TabIndex = 12;
            this.old.UseSelectable = true;
            this.old.WaterMark = "Old Password";
            this.old.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.old.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // newPass
            // 
            // 
            // 
            // 
            this.newPass.CustomButton.Image = null;
            this.newPass.CustomButton.Location = new System.Drawing.Point(265, 2);
            this.newPass.CustomButton.Name = "";
            this.newPass.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.newPass.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.newPass.CustomButton.TabIndex = 1;
            this.newPass.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.newPass.CustomButton.UseSelectable = true;
            this.newPass.CustomButton.Visible = false;
            this.newPass.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.newPass.Lines = new string[0];
            this.newPass.Location = new System.Drawing.Point(16, 101);
            this.newPass.MaxLength = 32767;
            this.newPass.Name = "newPass";
            this.newPass.PasswordChar = '*';
            this.newPass.PromptText = "New Password";
            this.newPass.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.newPass.SelectedText = "";
            this.newPass.SelectionLength = 0;
            this.newPass.SelectionStart = 0;
            this.newPass.ShortcutsEnabled = true;
            this.newPass.Size = new System.Drawing.Size(293, 30);
            this.newPass.TabIndex = 13;
            this.newPass.UseSelectable = true;
            this.newPass.WaterMark = "New Password";
            this.newPass.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.newPass.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // newPassConfirm
            // 
            // 
            // 
            // 
            this.newPassConfirm.CustomButton.Image = null;
            this.newPassConfirm.CustomButton.Location = new System.Drawing.Point(265, 2);
            this.newPassConfirm.CustomButton.Name = "";
            this.newPassConfirm.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.newPassConfirm.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.newPassConfirm.CustomButton.TabIndex = 1;
            this.newPassConfirm.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.newPassConfirm.CustomButton.UseSelectable = true;
            this.newPassConfirm.CustomButton.Visible = false;
            this.newPassConfirm.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.newPassConfirm.Lines = new string[0];
            this.newPassConfirm.Location = new System.Drawing.Point(16, 137);
            this.newPassConfirm.MaxLength = 32767;
            this.newPassConfirm.Name = "newPassConfirm";
            this.newPassConfirm.PasswordChar = '*';
            this.newPassConfirm.PromptText = "Confirm New Password";
            this.newPassConfirm.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.newPassConfirm.SelectedText = "";
            this.newPassConfirm.SelectionLength = 0;
            this.newPassConfirm.SelectionStart = 0;
            this.newPassConfirm.ShortcutsEnabled = true;
            this.newPassConfirm.Size = new System.Drawing.Size(293, 30);
            this.newPassConfirm.TabIndex = 14;
            this.newPassConfirm.UseSelectable = true;
            this.newPassConfirm.WaterMark = "Confirm New Password";
            this.newPassConfirm.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.newPassConfirm.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // CustomerForm9
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(348, 247);
            this.ControlBox = false;
            this.Controls.Add(this.newPassConfirm);
            this.Controls.Add(this.newPass);
            this.Controls.Add(this.old);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CustomerForm9";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnSave;
        public System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroTextBox old;
        private MetroFramework.Controls.MetroTextBox newPass;
        private MetroFramework.Controls.MetroTextBox newPassConfirm;
    }
}