namespace os_scheduler
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
            this.method = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.count = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.quan = new System.Windows.Forms.Label();
            this.rr_quan = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // method
            // 
            this.method.FormattingEnabled = true;
            this.method.Items.AddRange(new object[] {
            "FCFS",
            "Priority (non pre-emptive)",
            "Priority (pre-emptive)",
            "RR",
            "SJF (non pre-emptive)",
            "SJF (pre-emptive)"});
            this.method.Location = new System.Drawing.Point(221, 49);
            this.method.Name = "method";
            this.method.Size = new System.Drawing.Size(209, 21);
            this.method.TabIndex = 2;
            this.method.SelectedIndexChanged += new System.EventHandler(this.method_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(218, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Choose method of scheduling";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Kristen ITC", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter number of processes";
            // 
            // count
            // 
            this.count.Location = new System.Drawing.Point(12, 49);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(183, 20);
            this.count.TabIndex = 0;
            this.count.TextChanged += new System.EventHandler(this.count_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Font = new System.Drawing.Font("Kristen ITC", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(588, 140);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Processes information";
            // 
            // quan
            // 
            this.quan.AutoSize = true;
            this.quan.Font = new System.Drawing.Font("Kristen ITC", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quan.Location = new System.Drawing.Point(455, 23);
            this.quan.Name = "quan";
            this.quan.Size = new System.Drawing.Size(70, 16);
            this.quan.TabIndex = 7;
            this.quan.Text = "Quantum:";
            // 
            // rr_quan
            // 
            this.rr_quan.Font = new System.Drawing.Font("Kristen ITC", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rr_quan.Location = new System.Drawing.Point(458, 50);
            this.rr_quan.Name = "rr_quan";
            this.rr_quan.Size = new System.Drawing.Size(143, 22);
            this.rr_quan.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 280);
            this.Controls.Add(this.rr_quan);
            this.Controls.Add(this.quan);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.count);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.method);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox method;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox count;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label quan;
        private System.Windows.Forms.TextBox rr_quan;


    }
}

