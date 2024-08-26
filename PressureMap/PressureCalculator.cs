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
            if (t < 0.0)
            {
                return 0.0;
            }
            
            double expression1 = _mu / (4 * Math.PI * _k * _H);
            double expression2 = -(x * x + y * y);
            double expression3 = (4 * _D * t);
            double expression4 = -(x * x + y * y) / (4 * _D * t);
            double expression5 = SpecialFunctions.ExponentialIntegral(-(x * x + y * y) / (4 * _D * t));
            
            double result = _mu / (4 * Math.PI * _k * _H) * SpecialFunctions.ExponentialIntegral(-(x * x + y * y) / (4 * _D * t));
            
            return result;
        }

        internal double[,] ComputatePressure(double[] x, double[] y,double t, 
            double[][] coords, (double t, double Q)[][] tQs)
        {

            double[,] X = new double[x.Length, y.Length];
            double[,] Y = new double[x.Length, y.Length];
            double[,] P = new double[x.Length, y.Length];

            // MeshGrid
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
                            // tQs[i+1][1] - tQs[i][1]
                            double q = (tQs[k][i + 1].Q - tQs[k][i].Q);
                            // p1(X-x0, Y-y0, t-tQs[i][0])
                            double pressure = p(X[m, n] - coords[k][0], Y[m, n] - coords[k][1], t - tQs[k][i].t);
                            double result = q * pressure;
                            P[m, n] += result;
                        }
                    }
                }

                for (int m = 0; m < x.Length; m++)
                {
                    for (int n = 0; n < y.Length; n++)
                    {
                        //tQs[0][1]*p1(X-x0, Y-y0, t)
                        double value = tQs[k][0].Q * p(X[m, n] - coords[k][0], Y[m, n] - coords[k][1], t);
                        P[m, n] += value;
                    }
                }
            }

            for (int m = 0; m < x.Length; m++)
            {
                for (int n = 0; n < y.Length; n++)
                {
                    // P+=p0
                    P[m, n] += _p0;
                }
            }

            return P;
        }
        
    }
}
