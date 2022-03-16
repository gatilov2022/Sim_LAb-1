using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sim_LAb_1
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Maximum = Convert.ToInt32(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int expr;
            int orel;

            chart1.Series.Clear();

            for (int i = 0; i < Convert.ToInt32(textBox2.Text); i++)
            {
                
                expr = 0;
                orel = 0;
                chart1.Series.Add(i.ToString());
                chart1.Series[i].ChartType = SeriesChartType.Spline;
                chart1.Series[i].Color = Color.Black;

                for (int j = 0; j < Convert.ToInt32(textBox1.Text); j++)
                {
                    double m = rand.NextDouble();
                    expr++;
                    if (m > 0.5)
                    {
                        orel++;
                    }

                    chart1.Series[i].Points.AddXY(expr, (double)orel / (double)expr);
                }
            }
        }
    }
}
