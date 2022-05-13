using System;

namespace Factories
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // This is a Factory, and specifically an Inner Factory.
        public static class Factory
        {
            // This is a Factory Method.
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        // This is a Factory Method.
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
        }
    }
}

