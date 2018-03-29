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
    public struct alaa_data
    {
        public int alaa_start, alaa_burst, alaa_process_id;
        public alaa_data(int a=0, int b=0 ,int c=0)
        {
            alaa_start = a;
            alaa_burst = b;
            alaa_process_id =c;
        }
    }
    public partial class Form1 : Form
    {
        bool first_input = true;
        
        public static scheduler s = new scheduler();
        public List<TextBox> burst_time = new List<TextBox>();
        public static List<int> burst_int = new List<int>();

        List<TextBox> arrival_time = new List<TextBox>();
        public static List<int> arrival_int = new List<int>();

        List<TextBox> priority = new List<TextBox>();
        public List<int> priority_int = new List<int>();

        TextBox rr_quan = new TextBox();
        int rr_quan_int;

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
            Form1.s.nprocess = (count.Text == "")? 0:Int32.Parse(count.Text);

            //MessageBox.Show(c.ToString());
            if (  s.method.Length != 0)
            {
                //store this number in your class
                take_processes_data();
            }
        }

        private void method_SelectedIndexChanged(object sender, EventArgs e)
        {
            //store the method in ur class
            Form1.s.method = method.Text;
            //MessageBox.Show(Form1.s.method);
            if ( s.method.Length != 0)
            {
                if (first_input == true)
                {
                    first_input = false;
                    take_processes_data();
                }
                else deactivate_unNecessary();

            }
        }
        private void take_processes_data()
        {
            //MessageBox.Show("ready to retrieve input");
            //reset
            groupBox1.Controls.Clear();
            groupBox1.Size = new System.Drawing.Size(588, 160);
            this.Size = new System.Drawing.Size(628, 318);
            burst_time.Clear();
            arrival_time.Clear();
            priority.Clear();
            if (s.nprocess == 0) return;

            int x = 15, y = 20;//relative to groupBox

            Label write_arrival = new Label();
            write_arrival.Text = "arrival time";
            write_arrival.Size = new System.Drawing.Size(120, 13);
            write_arrival.Location = new System.Drawing.Point(x+360+20, y);
            this.groupBox1.Controls.Add(write_arrival);

            Label write_bur_time = new Label();
            write_bur_time.Text = "burst time";
            write_bur_time.Size = new System.Drawing.Size(120, 13);
            write_bur_time.Location = new System.Drawing.Point(x + 120, y);
            this.groupBox1.Controls.Add(write_bur_time);

            Label prio = new Label();
            prio.Text = "priority";
            prio.Size = new System.Drawing.Size(120, 13);
            prio.Location = new System.Drawing.Point(x + 240 + 10, y);
            this.groupBox1.Controls.Add(prio);


            for (int i = 0; i < Form1.s.nprocess; ++i)
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

                TextBox pr = new TextBox();
                priority.Add(pr);
                priority[i].Location = new System.Drawing.Point(x + 240 + 10, y + (i + 1) * 30);
                priority[i].Size = new System.Drawing.Size(120, 13);
                this.groupBox1.Controls.Add(priority[i]);

                TextBox arr = new TextBox();
                arrival_time.Add(arr);
                arrival_time[i].Location = new System.Drawing.Point(x + 360+20, y + (i + 1) * 30);
                arrival_time[i].Size = new System.Drawing.Size(120, 13);
                this.groupBox1.Controls.Add(arrival_time[i]);


                groupBox1.Size = new System.Drawing.Size(groupBox1.Size.Width, groupBox1.Size.Height + 30);
                this.Size = new System.Drawing.Size(this.Size.Width, this.Size.Height + 30);
            }
            y += (Form1.s.nprocess+1)*30;
            Label quan = new Label();
            quan.Text = "quantum:";
            quan.Size = new System.Drawing.Size(120, 13);
            quan.Location = new System.Drawing.Point(x + 10 + 240, y+10);
            this.groupBox1.Controls.Add(quan); y += 25;

            rr_quan.Location = new System.Drawing.Point(x + 10 + 240, y);y+=25;
            rr_quan.Size = new System.Drawing.Size(120, 13);
            this.groupBox1.Controls.Add(rr_quan);

            //button Run
            Button run = new Button();
            run.Size = new System.Drawing.Size(120, 50);
            run.Location = new System.Drawing.Point(x + 10 + 240, y);
            run.Text = "Run";
            run.Name = "run";
            run.Click += new System.EventHandler(this.run_Click);
            this.groupBox1.Controls.Add(run);

            deactivate_unNecessary();
        }

        private void fill_int()
        {
            if (Form1.s.method == "RR")
            {
                if (rr_quan.Text == "")
                {
                    Exception e = new Exception("please fill quantum field");
                    throw (e);
                }
                rr_quan_int = Int32.Parse(rr_quan.Text);
            }
            if (Form1.s.method == "Priority (non pre-emptive)" || Form1.s.method == "Priority (pre-emptive)")
            {
                priority_int.Clear();
                for (int i = 0; i < s.nprocess; ++i)
                {
                    if (priority[i].Text == "")
                    {
                        Exception e = new Exception("please fill all possible fields");
                        throw (e);
                    }
                    priority_int.Add(Int32.Parse(priority[i].Text));
                }
            }

            arrival_int.Clear();
            burst_int.Clear();
            for (int i = 0; i < s.nprocess; ++i)
            {
                if (burst_time[i].Text == "" || arrival_time[i].Text == "")
                {
                    Exception e = new Exception("please fill all possible fields");
                    throw (e);
                }

                arrival_int.Add(Int32.Parse(arrival_time[i].Text));
                burst_int.Add(Int32.Parse(burst_time[i].Text));
            }
        }

        private void deactivate_unNecessary()
        {

            if (Form1.s.method != "RR") this.rr_quan.Enabled = false;
            else rr_quan.Enabled = true;

            if (Form1.s.method != "Priority (non pre-emptive)" && Form1.s.method != "Priority (pre-emptive)")
            {
                for (int i = 0; i < s.nprocess; ++i)
                {
                    priority[i].Enabled = false;
                }
            }
            else if (!priority[0].Enabled)
            {
                for (int i = 0; i < s.nprocess; ++i)
                {
                    priority[i].Enabled = true;
                }
            }
        }

        private void run_Click(object sender, EventArgs e)
        {
            try
            {
                fill_int();
                draw();
                //MessageBox.Show("Gantt chart");
            }
            catch(Exception exc){
                MessageBox.Show(exc.Message);
            }
        }

        private void draw()
        {
            List<alaa_data> drawable_data = get_drawable_data();
            //test
            //string g = "";
            //for (int i = 0; i <= s.nprocess; i++)
            //{
            //    g += drawable_data[i].alaa_start.ToString() + "  " + drawable_data[i].alaa_process_id.ToString() + "\n";
            //}
            //MessageBox.Show(g);
            //draw
            Form2 chart = new Form2(drawable_data);
            chart.ShowDialog();
        }

        private List<alaa_data> get_drawable_data()
        {
            List<alaa_data> drawable_data = new List<alaa_data>();
            //call alaa's functions based on Form1.s.method to fill drawable_data
            //testing
            for (int i = 0; i < s.nprocess; i++)
            {
                drawable_data.Add(new alaa_data(i*2,2,i+1));
            }
            //end testing
            return drawable_data;
        }
       
    }
}
