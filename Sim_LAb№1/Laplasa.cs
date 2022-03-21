using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sim_LAb_1
{
    class Laplace
    {
        double x;
        public Laplace(double p, int k,int n)
        {
            double q=1-p;
            x = (k - n * p)/ Math.Sqrt(n * p * q);
        }
        public double GetX()
        {
            return x;
        }
    }
}
