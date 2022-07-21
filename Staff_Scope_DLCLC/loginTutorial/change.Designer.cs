
namespace loginTutorial
{
    partial class change
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(change));
            this.loginadmin = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt2 = new System.Windows.Forms.TextBox();
            this.txt1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginadmin
            // 
            this.loginadmin.BackColor = System.Drawing.Color.SteelBlue;
            this.loginadmin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginadmin.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.loginadmin.Location = new System.Drawing.Point(119, 317);
            this.loginadmin.Name = "loginadmin";
            this.loginadmin.Size = new System.Drawing.Size(179, 40);
            this.loginadmin.TabIndex = 17;
            this.loginadmin.Text = "Save Changes";
            this.loginadmin.UseVisualStyleBackColor = false;
            this.loginadmin.Click += new System.EventHandler(this.loginadmin_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 20F);
            this.label5.Location = new System.Drawing.Point(42, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(346, 33);
            this.label5.TabIndex = 18;
            this.label5.Text = "Change Admin Password";
            // 
            // txt2
            // 
            this.txt2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2.Location = new System.Drawing.Point(88, 238);
            this.txt2.Name = "txt2";
            this.txt2.Size = new System.Drawing.Size(251, 27);
            this.txt2.TabIndex = 23;
            this.txt2.UseSystemPasswordChar = true;
            // 
            // txt1
            // 
            this.txt1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt1.Location = new System.Drawing.Point(88, 148);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(251, 27);
            this.txt1.TabIndex = 24;
            this.txt1.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(115, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 21);
            this.label3.TabIndex = 25;
            this.label3.Text = "Confirm New Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 21);
            this.label1.TabIndex = 26;
            this.label1.Text = "Enter New Password";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txt1);
            this.panel1.Controls.Add(this.txt2);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.loginadmin);
            this.panel1.Location = new System.Drawing.Point(35, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 382);
            this.panel1.TabIndex = 27;
            // 
            // change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 450);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "change";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Change Admin Password";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.change_FormClosing);
            this.Load += new System.EventHandler(this.change_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button loginadmin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt2;
        private System.Windows.Forms.TextBox txt1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
    }
}