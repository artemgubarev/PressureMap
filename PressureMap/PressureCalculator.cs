namespace PressureMap
{
    using System;

    internal class PressureCalculator
    {
        private double _p0;
        private double _mu;
        private double _Q;
        private double _k;
        private double _H;
        private double _D;

        public PressureCalculator(double p0, double mu, double Q, double k, double H, double D)
        {
            this._p0 = p0;
            this._mu = mu;
            this._Q = Q;
            this._k = k;
            this._H = H;
            this._D = D;
        }

        // вычислить давление в точке (x,y) в момент времени t
        private double p(double x, double y, double t)
        {
            return _p0 + ((_mu * _Q) / (4 * Math.PI * _k * _H)) * 
                SpecialFunctions.ExponentialIntegral(-(x * x + y * y) / (4 * _D * t));
        }

        internal double[,] ComputatePressure(double[] x, double[] y,double t, 
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
                            double pressure = p(x[j] - cx, y[i] - cy, t);
                            P[i, j] += pressure;
                        }
                    }
                }
            }
            return P;
        }
        
    }
}
