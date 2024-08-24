namespace PressureMap
{
    public struct Well
    {
        public Well(int number, double x, double y)
        {
            Number = number;
            X = x;
            Y = y;
        }

        public int Number { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }
}
