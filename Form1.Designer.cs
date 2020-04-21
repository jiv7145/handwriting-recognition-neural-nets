namespace WindowsFormsApp3 {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button_chknum = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.PictureBox();
            this.button3 = new System.Windows.Forms.Button();
            this.textbox_epochs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button_chkFile = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 243);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "The number is: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Please draw inside the square";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(89, 374);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(131, 24);
            this.button1.TabIndex = 3;
            this.button1.Text = "Reset";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_chknum
            // 
            this.button_chknum.Location = new System.Drawing.Point(89, 285);
            this.button_chknum.Margin = new System.Windows.Forms.Padding(4);
            this.button_chknum.Name = "button_chknum";
            this.button_chknum.Size = new System.Drawing.Size(131, 39);
            this.button_chknum.TabIndex = 4;
            this.button_chknum.Text = "Check Number";
            this.button_chknum.UseVisualStyleBackColor = true;
            this.button_chknum.Click += new System.EventHandler(this.button_chknum_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(27, 86);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(112, 112);
            this.panel1.TabIndex = 5;
            this.panel1.TabStop = false;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(489, 325);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 28);
            this.button3.TabIndex = 6;
            this.button3.Text = "Start Training";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textbox_epochs
            // 
            this.textbox_epochs.Location = new System.Drawing.Point(489, 266);
            this.textbox_epochs.Name = "textbox_epochs";
            this.textbox_epochs.Size = new System.Drawing.Size(100, 22);
            this.textbox_epochs.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(399, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Epochs";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(387, 101);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(293, 125);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // button_chkFile
            // 
            this.button_chkFile.Location = new System.Drawing.Point(89, 331);
            this.button_chkFile.Name = "button_chkFile";
            this.button_chkFile.Size = new System.Drawing.Size(131, 36);
            this.button_chkFile.TabIndex = 12;
            this.button_chkFile.Text = "Check A File";
            this.button_chkFile.UseVisualStyleBackColor = true;
            this.button_chkFile.Click += new System.EventHandler(this.button_chkFile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(211, 86);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(109, 112);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 436);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_chkFile);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textbox_epochs);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_chknum);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button_chknum;
        private System.Windows.Forms.PictureBox panel1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textbox_epochs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button_chkFile;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

