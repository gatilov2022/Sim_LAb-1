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

        private void create_graf(int col_ser,int col_popit)
        {
            int expr;
            int orel;
            for (int i = 0; i < col_ser; i++)
            {
                expr = orel = 0;
                chart1.Series.Add(i.ToString());
                chart1.Series[i].ChartType = SeriesChartType.Line;
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
            chart1.ChartAreas[0].AxisX.Title = "Номер эксперемента";
            chart1.ChartAreas[0].AxisY.Title = "Вероятность";
        }

        private void create_graf_task2(int col_ser, int col_popit)
        {
            chart1.Series.Add("SredArif");
            chart1.Series["SredArif"].Color = Color.Red;
            chart1.Series["SredArif"].ChartType = SeriesChartType.Spline;
            chart1.Series["SredArif"].BorderWidth = 2;
            chart2.Series[0].Points.Clear();

            for (int i = 0; i < col_popit; i++)
            {
                double y = 0;
                for (int j = 0; j < col_ser; j++)
                {
                    y += chart1.Series[j].Points[i].YValues[0];
                }
                double sred = y / (double)col_ser;
                chart1.Series["SredArif"].Points.AddXY(i + 1, sred);
                chart2.Series[0].Points.AddXY(i + 1, Math.Abs(sred - 0.5));
            }

        }

        private void rez_gtoup(int col_popit)
        {
            double last_rez = chart1.Series["SredArif"].Points[col_popit - 1].YValues[0];

            label4.Text = "Результат:" + last_rez;
            label5.Text = "Отклонение от теор. знач:" + Math.Abs(Math.Round(last_rez - 0.5, last_rez.ToString().Length));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int col_ser = Convert.ToInt32(textBox2.Text);
            int col_popit = Convert.ToInt32(textBox1.Text);
            chart1.ChartAreas[0].AxisX.Maximum = col_popit + 1;
            chart2.ChartAreas[0].AxisX.Maximum = col_popit + 1;

            chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
            chart2.ChartAreas[0].AxisX.IsLogarithmic = false;

            chart1.Series.Clear();

            create_graf(col_ser, col_popit);
            create_graf_task2(col_ser,col_popit);
            rez_gtoup(col_popit);


            chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
            chart2.ChartAreas[0].AxisX.IsLogarithmic = true;
        }
    }
}
