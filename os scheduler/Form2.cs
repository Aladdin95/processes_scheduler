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
    public partial class Form2 : Form
    {
        List<alaa_data> drawable ;
        List<int> leave_time_int = new List<int>();
        List<int> wait_time_int = new List<int>();
        double wt_time = 0 ;


        public Form2(List<alaa_data> data)
        {
            InitializeComponent();
            drawable = data;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            calculate_wt();
            //MessageBox.Show(wt_time.ToString());
            label2.Text = wt_time.ToString();
        }

        private void calculate_wt()
        {

            for (int i = 0; i < Form1.s.count; ++i)
            {
                leave_time_int.Add(-1);
                wait_time_int.Add(0);
            }
            //fill leave time
            int id;
            for (int i = drawable.Count-1 ; i >= 0; --i)
            {
                id = drawable[i].alaa_process_id-1;
                if (leave_time_int[id] == -1)
                {
                    leave_time_int[id] =
                        drawable[i].alaa_start + drawable[i].alaa_burst;
                }
            }
            
            //waiting time
            for (int i = 0; i < Form1.s.count; ++i)
            {
                wait_time_int[i] = leave_time_int[i]
                    - Form1.burst_int[i]
                    - Form1.arrival_int[i];
                wt_time += wait_time_int[i];
            }
            wt_time = wt_time / Form1.s.count;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
