using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using Tools;

namespace _102
{
    class Program
    {
        private const string FILENAME = @"../../p102_triangles.txt";

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines(FILENAME)
                        .Select(s => s.Split(',').Select(int.Parse).ToArray());

            Decorators.TimeItAccurate(Solve, lines, 2000);
        }

        private static int Solve(IEnumerable<int[]> lines)
        {
            int res = 0;

            foreach (var line in lines)
            {
                var a = new Point(line[0], line[1]);
                var b = new Point(line[2], line[3]);
                var c = new Point(line[4], line[5]);

                var t = new Triangle(a, b, c);
                if (t.Contains(Point.Origin))
                {
                    res++;
                }
            }

            return res;
        }

        private struct Point
        {
            public static readonly Point Origin = new Point(0, 0);

            public int X { get; private set; }
            public int Y { get; private set; }

            public Point(int x, int y)
                : this()
            {
                X = x;
                Y = y;
            }

            public override string ToString()
            {
                return String.Format("({0};{1})", X, Y);
            }

            public static Point operator +(Point a, Point b)
            {
                return new Point(a.X + b.X, a.Y + b.Y);
            }

            public static Point operator -(Point a, Point b)
            {
                return new Point(a.X - b.X, a.Y - b.Y);                
            }
        }

        private struct Line
        {
            public Point Start { get; private set; }
            public Point End { get; private set; }

            public Point Vector { get; private set; }

            public Line(Point start, Point end)
                : this()
            {
                Start = start;
                End = end;

                Vector = End - Start;
            }

            public double Angle(Point point)
            {
                var other = point - Start;

                return Math.Atan2(Vector.X * other.Y - other.X * Vector.Y,
                                  Vector.X * other.X + Vector.Y * other.Y);
            }
        }

        private struct Triangle
        {
            public Point A { get; private set; }
            public Point B { get; private set; }
            public Point C { get; private set; }

            public Line SideA { get; private set; }
            public Line SideB { get; private set; }
            public Line SideC { get; private set; }

            public Triangle(Point a, Point b, Point c)
                : this()
            {
                A = a;
                B = b;
                C = c;

                SideA = new Line(A, B);
                SideB = new Line(B, C);
                SideC = new Line(C, A);
            }

            public bool Contains(Point point)
            {
                int signA = Math.Sign(SideA.Angle(point));
                int signB = Math.Sign(SideB.Angle(point));
                int signC = Math.Sign(SideC.Angle(point));

                if (signA != signB || signB != signC)
                {
                    return false;
                }
                return true;
            }

            public override string ToString()
            {
                return String.Join(", ", A, B, C);
            }
        }
    }
}
