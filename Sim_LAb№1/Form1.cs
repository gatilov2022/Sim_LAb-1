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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int expr;
            int orel;
            int col_ser = Convert.ToInt32(textBox2.Text);
            int col_popit = Convert.ToInt32(textBox1.Text);
            chart1.ChartAreas[0].AxisX.Maximum = col_popit + 1;

            chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
            

            chart1.Series.Clear();

            for (int i = 0; i < col_ser; i++)
            {
                expr = 0;
                orel = 0;
                chart1.Series.Add(i.ToString());
                chart1.Series[i].ChartType = SeriesChartType.Spline;
                chart1.Series[i].Color = Color.Gray;

                for (int j = 0; j < col_popit; j++)
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
            chart1.ChartAreas[0].AxisX.Title = "Кол-во опытов в серии";

            chart1.Series.Add("SredArif");
            chart1.Series["SredArif"].Color = Color.Blue;
            chart1.Series["SredArif"].ChartType = SeriesChartType.Spline;

            for (int i = 0; i < col_popit; i++)
            {
                double y = 0;
                for(int j = 0;j< col_ser; j++)
                {
                    y += chart1.Series[j].Points[i].YValues[0];
                }
                chart1.Series["SredArif"].Points.AddXY(i+1, y/(double)col_ser);
            }



            chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
        }
    }
}
