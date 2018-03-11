using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace os_scheduler
{
    public partial class Form1 : Form
    {
        private scheduler s = new scheduler();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // this.MaximizeBox = false;
           // this.MinimizeBox = false;
            //this.BackColor = Color.White;
            //this.ForeColor = Color.Black;
            //this.Size = new System.Drawing.Size(850, 480);
            this.Text = "Processes Scheduler";
           // this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            
        }

        private void count_TextChanged(object sender, EventArgs e)
        {
            //store this number in your class
            this.s.count = (count.Text=="")?0:Int32.Parse(count.Text);
           // MessageBox.Show(this.s.count.ToString());
            if (s.count != 0 && s.method.Length != 0) take_processes_data();
        }

        private void method_SelectedIndexChanged(object sender, EventArgs e)
        {
            //store the method in ur class
            this.s.method = method.Text;
            //MessageBox.Show(this.s.method);
            if (s.count != 0 && s.method.Length != 0) take_processes_data();
        }
        private void take_processes_data()
        {
            //MessageBox.Show("ready to retrieve input");
            //reset
            groupBox1.Controls.Clear();
            groupBox1.Size = new System.Drawing.Size(588 ,100);
            this.Size = new System.Drawing.Size(628, 318);
            //data
            List<TextBox> burst_time = new List<TextBox>();
            List<TextBox> arrival_time = new List<TextBox>();
            List<TextBox> priority = new List<TextBox>();

            int x = 15, y = 20;//relative to groupBox
            Label write_bur_time = new Label();
            write_bur_time.Text = "burst time";
            write_bur_time.Size = new System.Drawing.Size(120,13);
            write_bur_time.Location = new System.Drawing.Point(x+120, y);
            this.groupBox1.Controls.Add(write_bur_time);

            Label write_arrival = new Label();
            write_arrival.Text = "arrival time";
            write_arrival.Size = new System.Drawing.Size(120, 13);
            write_arrival.Location = new System.Drawing.Point(x+240+10, y);
            this.groupBox1.Controls.Add(write_arrival);

            Label prio = new Label();
            prio.Text = "priority";
            prio.Size = new System.Drawing.Size(120, 13);
            prio.Location = new System.Drawing.Point(x + 360 + 20, y);
            this.groupBox1.Controls.Add(prio);


            for (int i = 0; i < this.s.count; ++i)
            {
                // int x = (i!=0) ? burst_time[i - 1].Location.X : 15;
                //  int y = (i != 0) ? burst_time[i - 1].Location.Y+30 : 20;

                Label name = new Label();
                name.Text = "task" + (i+1).ToString();
                name.Size = new System.Drawing.Size(120, 13);
                name.Location = new System.Drawing.Point(x, y + ((i + 1) * 30));
                this.groupBox1.Controls.Add(name);

                TextBox bur = new TextBox();
                burst_time.Add(bur);
                burst_time[i].Location = new System.Drawing.Point(x + 120, y + (i+1) * 30);
                burst_time[i].Size = new System.Drawing.Size(120, 13);
                this.groupBox1.Controls.Add(burst_time[i]);

                TextBox arr = new TextBox();
                arrival_time.Add(arr);
                arrival_time[i].Location = new System.Drawing.Point(x + 240+10, y + (i + 1) * 30);
                arrival_time[i].Size = new System.Drawing.Size(120, 13);
                this.groupBox1.Controls.Add(arrival_time[i]);

                TextBox pr = new TextBox();
                priority.Add(pr);
                priority[i].Location = new System.Drawing.Point(x + 360 + 20, y + (i + 1) * 30);
                priority[i].Size = new System.Drawing.Size(120, 13);
                this.groupBox1.Controls.Add(priority[i]);

                groupBox1.Size = new System.Drawing.Size(groupBox1.Size.Width, groupBox1.Size.Height + 30);
                this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + 30);
            }
            y += (this.s.count+1)*30;
            Label quan = new Label();
            quan.Text = "quantum:";
            quan.Size = new System.Drawing.Size(120, 13);
            quan.Location = new System.Drawing.Point(x, y);
            this.groupBox1.Controls.Add(quan);
        }
    }
}
