using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Sim_LAb_1
{
    public partial class Form1 : Form
    {
        private readonly Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void ExpirienceChart(int numOfSeries, int numOfAttemp, Chart chart)
        {
            chart1.Series.Clear();
            double m;

            for (var currentSeries = 0; currentSeries < numOfSeries; currentSeries++) 
            {
                int favResults = 0; 
                chart.Series.Add(currentSeries.ToString());
                chart.Series[currentSeries].ChartType = SeriesChartType.Line;
                chart.Series[currentSeries].Color = Color.LightSlateGray;

                for (int currnetAttemp = 0; currnetAttemp < numOfAttemp; currnetAttemp++) 
                {
                    m = rand.NextDouble();
                    if (m >= 0.5)
                    {
                        favResults++;
                    }
                    chart1.Series[currentSeries].Points.AddXY(currnetAttemp + 1, (double)favResults / (double)(currnetAttemp + 1));
                }
            }
            
            chart1.ChartAreas[0].AxisX.Title = "Номер эксперемента";
            chart1.ChartAreas[0].AxisY.Title = "Вероятность";

            chart1.Series.Add("averageArefm");
            chart1.Series["averageArefm"].Color = Color.Maroon;
            chart1.Series["averageArefm"].ChartType = SeriesChartType.Spline;
            chart1.Series["averageArefm"].BorderWidth = 3;

            chart1.Series.Add("Accuracy");
            chart1.Series["Accuracy"].Color = Color.Goldenrod;
            chart1.Series["Accuracy"].ChartType = SeriesChartType.Line;
            chart1.Series["Accuracy"].BorderWidth = 3;

            chart1.Series.Add("Accuracy2");
            chart1.Series["Accuracy2"].Color = Color.Goldenrod;
            chart1.Series["Accuracy2"].ChartType = SeriesChartType.Line;
            chart1.Series["Accuracy2"].BorderWidth = 3;
        }

        private void ErrorChart(int numOfSeries, int numOfAttemp, Chart chart)
        {
            chart.Series[0].Points.Clear();
            chart.Series[2].Points.Clear();
            chart.Series[1].Points.Clear();
            
            
            List<double> expValues = new List<double>();

            double Delta = (1 - Convert.ToDouble(textBox3.Text) )/ 2;

            int AmountToDel = (int)(Delta * numOfSeries);
            TestLaplas test = new TestLaplas();


            for (int i = 0; i < numOfAttemp; i++)
            {
                double y = 0;
                expValues.Clear();
                

                for (int j = 0; j < numOfSeries; j++)
                {
                    y += chart1.Series[j].Points[i].YValues[0];
                    expValues.Add(chart1.Series[j].Points[i].YValues[0]);
                }
                expValues.Sort();
                expValues.RemoveRange(expValues.Count - AmountToDel, AmountToDel);
                expValues.RemoveRange(0, AmountToDel);

                chart1.Series["Accuracy"].Points.AddXY(i + 1, expValues[0] + 0.01);
                chart1.Series["Accuracy2"].Points.AddXY(i + 1, expValues[expValues.Count - 1] - 0.01);

                chart2.Series[2].Points.AddXY(i + 1, (expValues[expValues.Count - 1] - expValues[0]) / 2);

                double average = y / (double)numOfSeries;

                double laplaceResult = test.F(Convert.ToDouble(textBox3.Text) / 2 + 0.5);
                double resultValue = Convert.ToDouble(laplaceResult.ToString()) * Math.Sqrt(0.25 / (i + 1 ));

                chart1.Series["averageArefm"].Points.AddXY(i + 1, average);
                chart2.Series[0].Points.AddXY(i + 1, Math.Abs(average - 0.5));
                chart2.Series[1].Points.AddXY(i + 1, resultValue);
            }

            chart2.ChartAreas[0].AxisX.Title = "Номер эксперемента";
            chart2.ChartAreas[0].AxisY.Title = "Погрешность";
        }

        private void RezGroup(int numOfAttemp)
        {
            
            double lastResult = chart1.Series["averageArefm"].Points[numOfAttemp - 1].YValues[0];

            if (lastResult.ToString().Length < 16)
            {
                label4.Text = "Вероятность:" + lastResult;
                label5.Text = "Отклонение от теор. знач:" + Math.Abs((decimal)Math.Round(lastResult - 0.5, lastResult.ToString().Length));
            }
            else
            {
                label4.Text = "Вероятность:" + (decimal)Math.Round(lastResult, lastResult.ToString().Length - 3);
                label5.Text = "Отклонение от теор. знач:" + Math.Abs((decimal)Math.Round(lastResult - 0.5, lastResult.ToString().Length - 3));
            }
            label4.Text += " ± " + Math.Round(chart2.Series[1].Points[numOfAttemp - 1].YValues[0], 4);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            int numOfSeries = Convert.ToInt32(textBox2.Text);
            int numOfAttemp = Convert.ToInt32(textBox1.Text);

            chart1.ChartAreas[0].AxisX.Maximum = numOfAttemp + 1;
            chart2.ChartAreas[0].AxisX.Maximum = numOfAttemp + 1;
            
            ExpirienceChart(numOfSeries, numOfAttemp, chart1);
            ErrorChart(numOfSeries,numOfAttemp, chart2);
            RezGroup(numOfAttemp);

            checkBox1_CheckedChanged(checkBox1, e);
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && Convert.ToDouble(textBox3.Text) < 1 && Convert.ToDouble(textBox3.Text) > 0)
            {
                button1.Enabled = true;
            }
            else button1.Enabled = false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && chart1.Series.Any())
            {
                chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
                chart2.ChartAreas[0].AxisX.IsLogarithmic = true;
            }
            else if (textBox2.Text.Any() )
            {
                chart1.ChartAreas[0].AxisX.Maximum = Convert.ToInt32(textBox1.Text);
                chart2.ChartAreas[0].AxisX.Maximum = Convert.ToInt32(textBox1.Text);

                chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
                chart2.ChartAreas[0].AxisX.IsLogarithmic = false;
            }
        }
    }
}
