namespace PressureMap
{
    using System;

    internal class PressureCalculator
    {
        private double p0;
        private double mu;
        private double Q;
        private double k;
        private double H;
        private double D;

        public PressureCalculator(double p0, double mu, double Q, double k, double H, double D)
        {
            this.p0 = p0;
            this.mu = mu;
            this.Q = Q;
            this.k = k;
            this.H = H;
            this.D = D;
        }

        // вычислить давление в точке (x,y) в момент времени t
        private double p(double x, double y, double t)
        {
            return p0 + ((mu * Q) / (4 * Math.PI * k * H)) 
                * ExpIntegralEi(-(x * x + y * y) / (4 * D * t));
        }

        internal double[,] ComputatePressureConst(double[] x, double[] y,double t, 
            double[][] coords, double x0 = 0, double y0 = 0)
        {
            int rows = y.Length;
            int cols = x.Length;
            double[,] P = new double[rows, cols];

            if (coords == null || coords.Length == 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        P[i, j] = p(x[j] - x0, y[i] - y0, t);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        P[i, j] = 0;
                        foreach (double[] coord in coords)
                        {
                            double cx = coord[0];
                            double cy = coord[1];
                            P[i, j] += p(x[j] - cx, y[i] - cy, t);
                        }
                    }
                }
            }
            return P;
        }

        // Экспоненциальный интеграл (Ei)
        private double ExpIntegralEi(double x)
        {
            const double euler = 0.5772156649015328606065120900824024;
            double sum = 0;
            double term = 1;
            int n = 1;

            while (Math.Abs(term) > 1e-15 * Math.Abs(sum))
            {
                sum += term;
                term *= x / n;
                n++;
            }

            return euler + Math.Log(Math.Abs(x)) + sum;
        }
    }
}
