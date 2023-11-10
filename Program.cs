using System;

// Δομή για τα σημεία (x, y)
struct Point
{
    public double X { get; set; }
    public double Y { get; set; }
}

// Δομή για τα ευθύγραμμα τμήματα
struct LineSegment
{
    public Point StartPoint { get; set; }
    public Point EndPoint { get; set; }
}

// Δομή για τον κύκλο
struct Circle
{
    public Point Center { get; set; }
    public double Radius { get; set; }
}

class Program
{
    // Συνάρτηση για τον υπολογισμό της τομής δύο ευθυγράμμων τμημάτων
    static Point FindIntersection(LineSegment line1, LineSegment line2)
    {
        double x1 = line1.StartPoint.X;
        double y1 = line1.StartPoint.Y;
        double x2 = line1.EndPoint.X;
        double y2 = line1.EndPoint.Y;

        double x3 = line2.StartPoint.X;
        double y3 = line2.StartPoint.Y;
        double x4 = line2.EndPoint.X;
        double y4 = line2.EndPoint.Y;

        double determinant = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

        if (determinant == 0)
        {
            // Τα ευθύγραμμα τμήματα είναι παράλληλα
            return new Point();
        }

        double intersectionX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / determinant;
        double intersectionY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / determinant;

        return new Point { X = intersectionX, Y = intersectionY };
    }

    // Συνάρτηση για τον υπολογισμό της τομής ευθυγράμμου τμήματος με κύκλο
    static Point FindIntersection(LineSegment line, Circle circle)
    {
        double x1 = line.StartPoint.X;
        double y1 = line.StartPoint.Y;
        double x2 = line.EndPoint.X;
        double y2 = line.EndPoint.Y;

        double cx = circle.Center.X;
        double cy = circle.Center.Y;
        double r = circle.Radius;

        double dx = x2 - x1;
        double dy = y2 - y1;
        double a = dx * dx + dy * dy;
        double b = 2 * (dx * (x1 - cx) + dy * (y1 - cy));
        double c = (x1 - cx) * (x1 - cx) + (y1 - cy) * (y1 - cy) - r * r;

        double discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            // Το ευθύγραμμο τμήμα και ο κύκλος δεν τέμνονται
            return new Point();
        }

        double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);

        if (t1 >= 0 && t1 <= 1)
        {
            return new Point { X = x1 + t1 * dx, Y = y1 + t1 * dy };
        }
        else if (t2 >= 0 && t2 <= 1)
        {
            return new Point { X = x1 + t2 * dx, Y = y1 + t2 * dy };
        }

        return new Point();
    }

    static void Main(string[] args)
    {
        // Εισάγετε τα δεδομένα των ευθυγράμμων τμημάτων και του κύκλου
        LineSegment line1 = new LineSegment { StartPoint = new Point { X = 0, Y = 0 }, EndPoint = new Point { X = 2, Y = 2 } };
        LineSegment line2 = new LineSegment { StartPoint = new Point { X = 0, Y = 1 }, EndPoint = new Point { X = 2, Y = 3 } };
        Circle circle = new Circle { Center = new Point { X = 1, Y = 1 }, Radius = 1 };

        // Καλέστε τις συναρτήσεις FindIntersection για να βρείτε την τομή

        Point intersection1 = FindIntersection(line1, line2);
        Point intersection2 = FindIntersection(line1, circle);

        // Εμφανίστε τα αποτελέσματα
        Console.WriteLine($"Intersection 1: X = {intersection1.X}, Y = {intersection1.Y}");
        Console.WriteLine($"Intersection 2: X = {intersection2.X}, Y = {intersection2.Y}");
    }
}
