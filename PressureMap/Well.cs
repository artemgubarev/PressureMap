using DevExpress.ClipboardSource.SpreadsheetML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PressureMap
{
    public struct Well
    {
        public Well(int number, double x, double y, (DateTime Date, double value)[] q)
        {
            Number = number;
            X = x;
            Y = y;
            Q = q;
        }

        public Well(int number, double x, double y)
        {
            Number = number;
            X = x;
            Y = y;
        }

        public int Number { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        private (DateTime Date, double value)[] _q;
        public (DateTime Date, double value)[] Q
        {
            get { return _q;}
            set
            {
                var filledData = new List<(DateTime, double)>();

                for (int i = 0; i < value.Length - 1; i++)
                {
                    var start = value[i];
                    var end = value[i + 1];
                    
                    filledData.Add(start);
                    int daysBetween = (end.Date - start.Date).Days;

                    for (int day = 1; day < daysBetween; day++)
                    {
                        DateTime currentDate = start.Date.AddDays(day);
                        filledData.Add((currentDate, start.value));
                    }
                }
                filledData.Add(value[value.Length - 1]);
                _q = filledData.ToArray();
            }
        }

        public (double t, double Q)[] GetTQs(DateTime start, DateTime end, int step)
        {
            double seconds = 0;
            var tQs = new List<(double t, double Q)>();
            if (Q == null)
            {
                return null; 
            }
            while (start < end)
            {
                var time = Q.FirstOrDefault(tuple => tuple.Date == start);
                if (start == time.Date)
                {
                    tQs.Add(new (seconds, time.value));
                }
                else
                {
                    tQs.Add(new(seconds, 0));
                }
                start = start.AddDays(step);
                seconds+=86400;
            }
            return tQs.ToArray();
        }
    }
}
