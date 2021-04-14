using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Models
{
   
        public class Line
        {
            public float a, b;
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public Line()
        {
            this.a = 0;
            this.b = 0;
        }

        public float y_hat(float x)
            {
                return this.a * x + this.b;
            }
        }
    
}
