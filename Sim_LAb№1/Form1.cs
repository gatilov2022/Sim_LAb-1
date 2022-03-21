using System;
using System.Collections.Generic;
using System.Drawing;
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
            textBox3.Text = "0,96";

            chart1.ChartAreas[0].BackColor = Color.LightGray;
        }

        private void create_graf(int col_ser,int col_popit)
        {
            chart1.Series.Clear();
            int expr;
            int orel;
            for (int i = 0; i < col_ser; i++) // Количество серий
            {
                expr = orel = 0; 
                chart1.Series.Add(i.ToString());
                chart1.Series[i].ChartType = SeriesChartType.Line;
                chart1.Series[i].Color = Color.Gray;

                for (int j = 0; j < col_popit; j++) // Количество опытов
                {
                    double m = rand.NextDouble(); // Число от 0 до 1 не включая
                    expr++; // Кол-во эксперементов 
                    if (m >= 0.5) // Проверка на выпадение орла
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

            chart1.Series.Add("Accuracy");
            chart1.Series["Accuracy"].Color = Color.Gold;
            chart1.Series["Accuracy"].ChartType = SeriesChartType.Line;
            chart1.Series["Accuracy"].BorderWidth = 3;

            chart1.Series.Add("Accuracy2");
            chart1.Series["Accuracy2"].Color = Color.Gold;
            chart1.Series["Accuracy2"].ChartType = SeriesChartType.Line;
            chart1.Series["Accuracy2"].BorderWidth = 3;
            chart2.Series[0].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.Series[1].Points.Clear();


            double y;
            List<double> NValues = new List<double>();

            double Delta = (1 - Convert.ToDouble(textBox3.Text) )/ 2;

            int AmountToDel = (int)(Delta * col_ser);

            for (int i = 0; i < col_popit; i++)
            {
                y = 0;
                NValues.Clear();
                for (int j = 0; j < col_ser; j++)
                {
                    y += chart1.Series[j].Points[i].YValues[0];
                    NValues.Add(chart1.Series[j].Points[i].YValues[0]);
                }
                NValues.Sort();
                NValues.RemoveRange(NValues.Count - AmountToDel, AmountToDel);
                NValues.RemoveRange(0, AmountToDel);

                chart1.Series["Accuracy"].Points.AddXY(i + 1, NValues[0] + 0.01);
                chart1.Series["Accuracy2"].Points.AddXY(i + 1, NValues[NValues.Count - 1] - 0.01);

                chart2.Series[2].Points.AddXY(i + 1, (NValues[NValues.Count - 1] - NValues[0]) / 2);

                double sred = y / (double)col_ser;
                chart1.Series["SredArif"].Points.AddXY(i + 1, sred);
                chart2.Series[0].Points.AddXY(i + 1, Math.Abs(sred - 0.5));
            }
            
            chart2.ChartAreas[0].AxisX.Title = "Номер эксперемента";
            chart2.ChartAreas[0].AxisY.Title = "Погрешность";

        }



        private void rez_gtoup(int col_popit)
        {
            
            double last_rez = chart1.Series["SredArif"].Points[col_popit - 1].YValues[0];
            if (last_rez.ToString().Length < 16)
            {
                label4.Text = "Результат:" + last_rez;
                label5.Text = "Отклонение от теор. знач:" + Math.Abs((decimal)Math.Round(last_rez - 0.5, last_rez.ToString().Length));
            }
            else
            {
                label4.Text = "Результат:" + (decimal)Math.Round(last_rez, last_rez.ToString().Length - 3);
                label5.Text = "Отклонение от теор. знач:" + Math.Abs((decimal)Math.Round(last_rez - 0.5, last_rez.ToString().Length - 3));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int col_ser = Convert.ToInt32(textBox2.Text);
            int col_popit = Convert.ToInt32(textBox1.Text);
            chart1.ChartAreas[0].AxisX.Maximum = col_popit + 1;
            chart2.ChartAreas[0].AxisX.Maximum = col_popit + 1;

            if (checkBox1.Checked == true)
            {
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart2.ChartAreas[0].AxisX.IsLogarithmic = true;
            }
            else
            {
                chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
                chart2.ChartAreas[0].AxisX.IsLogarithmic = false;
            }
            create_graf(col_ser, col_popit);
            create_graf_task2(col_ser,col_popit);
            rez_gtoup(col_popit);


            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && Convert.ToDouble(textBox3.Text) < 1 && Convert.ToDouble(textBox3.Text) > 0)
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

    }
}
