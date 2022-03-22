namespace HookeJeevesMethod
{
    public class Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Value { get; }

        public Point(decimal x, decimal y)
        {
            X = x;
            Y = y;
            Value = RosenbrockFunction();
        }

        public override string ToString()
        {
            return "{" + X + "; " + Y + "} - " + Value;
        }

        private decimal RosenbrockFunction()
        {
            return (1 - X) * (1 - X) + (Y - X * X) * (Y - X * X) * 100;
        }
    }
}
