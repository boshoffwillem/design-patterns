using System;
using UdemyCourseExample;

namespace Adapter
{
    class Program
    {
        static void Main()
        {
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (VectorObject vectorObject in Vector_Raster_Example.VectorObjects)
            {
                foreach (Line line in vectorObject)
                {
                    LineToPointAdapter adapter = new(line);

                    foreach (Point point in adapter)
                    {
                        Vector_Raster_Example.DrawPoint(point);
                    }
                }
            }
        }
    }
}
