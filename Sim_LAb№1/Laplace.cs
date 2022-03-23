using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim_LAb_1
{
    internal class TestLaplas
    {
        public double F(double x)
        {
            MathNet.Numerics.Distributions.Normal result = new MathNet.Numerics.Distributions.Normal();
            return result.InverseCumulativeDistribution(x);
        }
    }
}