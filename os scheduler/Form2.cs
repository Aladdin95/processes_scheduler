using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

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
            int time_f_v = drawable[drawable.Count - 1].alaa_start
                + drawable[drawable.Count - 1].alaa_burst;

            int chart_length = 760 , x_o = 12, y_o= 79, x, y = y_o,
                width, height;

            ShapeContainer shapeContainer1 = new ShapeContainer();
            shapeContainer1.Location = new System.Drawing.Point(0, 0);
            shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            shapeContainer1.Name = "shapeContainer1";
            //shapeContainer1.Shapes.AddRange(new Shape[] { });
            shapeContainer1.Size = new System.Drawing.Size(684, 271);
            shapeContainer1.TabIndex = 4;
            shapeContainer1.TabStop = false;
            this.Controls.Add(shapeContainer1);

            for (int i = 0; i < drawable.Count; ++i)
            {
                x = (drawable[i].alaa_start * chart_length / time_f_v) + x_o;
                width = drawable[i].alaa_burst * chart_length / time_f_v;
                height = 76;

                //recs
                
                RectangleShape rectangleShape1 = new RectangleShape();
                rectangleShape1.Location = new System.Drawing.Point(x, y);
                rectangleShape1.Name = "rectangleShape"+(i+1).ToString();
                rectangleShape1.Size = new System.Drawing.Size(width, height);
                shapeContainer1.Shapes.Add(rectangleShape1);
                

                //on labels
                Label o = new Label();
                o.Text = "P" + drawable[i].alaa_process_id.ToString();
                o.Size = new System.Drawing.Size(width-2, 20);
                o.Location = new System.Drawing.Point(x+1, y + 28);
                o.TextAlign = ContentAlignment.MiddleCenter;
                //remove background color
                o.BackColor = Color.Transparent;
                this.Controls.Add(o);
                o.BringToFront();

                //under labels
                Label u = new Label();
                u.Text = drawable[i].alaa_start.ToString();
                u.Size = new System.Drawing.Size(width, height);
                u.Location = new System.Drawing.Point(x-6 , y + 10 + height);
                u.TextAlign = ContentAlignment.TopLeft;
                this.Controls.Add(u);

                if (i == drawable.Count - 1)
                {
                    Label z = new Label();
                    z.Text = time_f_v.ToString();
                    u.Size = new System.Drawing.Size(width, height);
                    z.Location = new System.Drawing.Point(x + width-5, y + 10 + height);
                    this.Controls.Add(z);
                }
            }
           
        }

        private void calculate_wt()
        {

            for (int i = 0; i < Form1.s.nprocess; ++i)
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
            for (int i = 0; i < Form1.s.nprocess; ++i)
            {
                wait_time_int[i] = leave_time_int[i]
                    - Form1.burst_int[i]
                    - Form1.arrival_int[i];
                wt_time += wait_time_int[i];
            }
            wt_time = wt_time / Form1.s.nprocess;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        //private void 
    }
}
