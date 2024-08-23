namespace PressureMap
{
    using System;

    internal static class SpecialFunctions
    {
        internal static double ExponentialIntegral(double x)
        {
            const double ga = 0.5772156649015328;
            double ei, r;

            if (x == 0.0)
            {
                ei = -1.0e+300;
            }
            else if (x < 0)
            {
                ei = -ExponentialIntegralXb(-x);
            }
            else if (Math.Abs(x) <= 40.0)
            {
                // Power series around x=0
                ei = 1.0;
                r = 1.0;

                for (int k = 1; k <= 100; k++)
                {
                    r = r * k * x / ((k + 1.0) * (k + 1.0));
                    ei += r;
                    if (Math.Abs(r / ei) <= 1.0e-15) { break; }
                }
                ei = ga + Math.Log(x) + x * ei;
            }
            else
            {
                // Asymptotic expansion (the series is not convergent)
                ei = 1.0;
                r = 1.0;
                for (int k = 1; k <= 20; k++)
                {
                    r = r * k / x;
                    ei += r;
                }
                ei = Math.Exp(x) / x * ei;
            }
            return ei;
        }

        private static double ExponentialIntegralXb(double x)
        {
            int k, m;
            double e1, r, t, t0;
            const double ga = 0.5772156649015328;

            if (x == 0.0)
            {
                e1 = 1e300;
            }
            else if (x <= 1.0)
            {
                e1 = 1.0;
                r = 1.0;
                for (k = 1; k < 26; k++)
                {
                    r = -r * k * x / Math.Pow(k + 1.0, 2);
                    e1 += r;
                    if (Math.Abs(r) <= Math.Abs(e1) * 1e-15) { break; }
                }
                e1 = -ga - Math.Log(x) + x * e1;
            }
            else
            {
                m = 20 + (int)(80.0 / x);
                t0 = 0.0;
                for (k = m; k > 0; k--)
                {
                    t0 = k / (1.0 + k / (x + t0));
                }
                t = 1.0 / (x + t0);
                e1 = Math.Exp(-x) * t;
            }
            return e1;
        }
    }
}
