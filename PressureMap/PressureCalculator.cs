namespace PressureMap
{
    using System;

    internal class PressureCalculator
    {
        private double _p0;
        private double _mu;
       // private double _Q;
        private double _k;
        private double _H;
        private double _D;

        public PressureCalculator(double p0, double mu, double k, double H, double D)
        {
            this._p0 = p0;
            this._mu = mu;
            //this._Q = Q;
            this._k = k;
            this._H = H;
            this._D = D;
        }

        // вычислить давление в точке (x,y) в момент времени t
        private double p(double x, double y, double t)
        {
            double expression1 = _mu / (4 * Math.PI * _k * _H);
            double expression2 = -(x * x + y * y) / (4 * _D * t);
            if (expression2 > 300)
            {
                expression2 = 300;
            }
            double expression3 = SpecialFunctions.ExponentialIntegral(expression2);
            double result = expression1 * expression3;
            
            return result;
        }

        internal double[,] ComputatePressure(double[] x, double[] y,double t, 
            double[][] coords, (double t, double Q)[][] tQs, double x0 = 0, double y0 = 0)
        {

            double[,] X = new double[x.Length, y.Length];
            double[,] Y = new double[x.Length, y.Length];
            double[,] P = new double[x.Length, y.Length];

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < y.Length; j++)
                {
                    X[i, j] = x[i];
                    Y[i, j] = y[j];
                }
            }

            for (int k = 0; k < coords.Length; k++)
            {
                for (int i = 0; i < tQs[k].Length - 1; i++)
                {
                    for (int m = 0; m < x.Length; m++)
                    {
                        for (int n = 0; n < y.Length; n++)
                        {
                            double q = (tQs[k][i + 1].Q - tQs[k][i].Q);
                            double _p = p(X[m, n] - coords[k][0], Y[m, n] - coords[k][1], t - tQs[k][i].t);
                            double value = q * _p;
                            P[m, n] += value;
                        }
                    }
                }

                for (int m = 0; m < x.Length; m++)
                {
                    for (int n = 0; n < y.Length; n++)
                    {
                        double value = tQs[k][0].Q * p(X[m, n] - coords[k][0], Y[m, n] - coords[k][1], t);
                        P[m, n] += value;
                    }
                }
            }

            for (int m = 0; m < x.Length; m++)
            {
                for (int n = 0; n < y.Length; n++)
                {
                    P[m, n] += _p0;
                }
            }

            return P;
        }
        
    }
}
