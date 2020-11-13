using System;

namespace lab06
{
    struct Point2D
    {
        public readonly double X;
        public readonly double Y;

        public Point2D(double x = 0, double y = 0)
        {
            X = x;
            Y = y;
            Console.WriteLine($"Point2D{this} has been created");
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public void Deconstruct(out double px, out double py) { px = X; py = Y; }                
    }
    
    class Geometry
    {
        public static double Distance(in Point2D p1, in Point2D p2)
        {
            double x1, x2, y1, y2;
            (x1, y1) = p1;
            (x2, y2) = p2;
            return Math.Sqrt(((x1 - x2) * (x1 - x2)) + ((y1 - y2) * (y1 - y2)));
        }

        public static double PolygonCircuit(params Point2D[] points)
        {
            if (points.Length < 3) return -1;

            double res = 0.0;

            for (int i = 0; i < points.Length - 1; i++)
            {
                res += Distance(points[i], points[i + 1]);
                // Console.WriteLine($"{Distance(points[i], points[i + 1])}");
            }

            res += Distance(points[^1], points[0]);
            // Console.WriteLine($"{Distance(points[^1], points[0])}");

            return res;

        }

        public static double ToOriginDistance((double x, double y) center, double r)
        {

            double y = center.x;
            double x = center.y;

            return Math.Abs(Math.Sqrt(x*x + y*y) - r);

        }
    }

    abstract class Shape
    {
        public abstract double Circuit();
    }

    abstract class Polygon : Shape
    {
        public Point2D[] Points;

        protected Polygon(params Point2D[] points)
        {
            Points = points[..];
        }

        public override double Circuit()
        {
            return Geometry.PolygonCircuit(Points);
        }
    }

    class Rectangle : Polygon 
    {
        public double diag;
        
        public Rectangle(Point2D p, double w, double h) : base(new Point2D[] 
                    {p, new Point2D(p.X, p.Y+h), new Point2D(p.X+w, p.Y+h), new Point2D(p.X, p.Y+h)})
        {
            diag = Math.Sqrt(w*w + h*h);
        }

        public Rectangle(Point2D p, double s) : base(new Point2D[]
                    {p, new Point2D(p.X, p.Y+s), new Point2D(p.X+s, p.Y+s), new Point2D(p.X, p.Y+s)})
        {
            diag = Math.Sqrt(2*s*s);
        }

    }

    class Circle : Shape
    {
        public Point2D P;
        public double R;
                
        public Circle(Point2D p, double r)
        {
            P = p;
            R = r;
        }

        public override double Circuit()
        {
            return 2*Math.PI*R;
        }

        public void Deconstruct(out (double px, double py) cen, out double rad) { cen.px = P.X; cen.py = P.Y ; rad = R; }
    }

}



