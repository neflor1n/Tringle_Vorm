using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tringle_Vorm
{

    class Tringle
    {
        public double a, b, c, h;

        public Tringle() { }
        public Tringle(double A) { a = A; b = A; c = A; }
        public Tringle (double A, double H)
        {
            a = A; h = H;
        }

        public Tringle(double A, double B, double C)
        {
            a = A;
            b = B;
            c = C;
        }
        
        public string outputA()
        {
            return Convert.ToString(a);
        }
        public string outputH()
        {
            return Convert.ToString(h);
        }
        public string outputB()
        {
            return Convert.ToString(b);
        }

        public string outputC()
        {
            return Convert.ToString(c);
        }

        public double Perimeter()
        {
            double p = 0;
            p = a + b + c;
            return p;

        }


        

        public double Surface()
        {
            double s = 0;
            double p = 0;
            p = (a + b + c) / 2;
            s = Math.Sqrt((p * (p - a) * (p - b) * (p - c)));
            return s;
        }

        public double GetSetA
        {
            get { return a; }
            set { a = value; }
        }

        public double GetSetB
        {
            get { return b; }
            set { b = value; }
        }

        public double GetSetC
        {
            get { return c; }
            set { c = value; }
        }


        public bool ExistTriange
        {
            get
            {
                // Проверка положительности сторон и выполнения условий существования
                return a > 0 && b > 0 && c > 0 && (a + b > c) && (a + c > b) && (b + c > a);
            }
        }
        public double Surface2()
        {
            return 0.5 * a * h; // Площадь треугольника: 0.5 * основание * высота
        }

        public bool ExistTriange2()
        {
            // Простейшее условие существования треугольника
            return (a > 0 && h > 0);
        }

        public double Height()
        {
            double p = (a + b + c) / 2;
            double area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            return (2 * area) / a;
        }

        private void Tringle_Load(object sender, EventArgs e)
        {

        }
    }

}
