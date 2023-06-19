using System;

namespace ClassBoxData 
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            double length = double.Parse(Console.ReadLine());
            double width = double.Parse(Console.ReadLine());
            double height = double.Parse(Console.ReadLine());

            try
            {
                Box box = new(length, width, height);
                double surfaceArea = box.SurfaceArea();
                double volume = box.Volume();
                double lateralSurfaceArea = box.LateralSurfaceArea();
                Console.WriteLine($"Surface Area - {surfaceArea:F2}");
                Console.WriteLine($"Lateral Surface Area - {lateralSurfaceArea:f2}");
                Console.WriteLine($"Volume - {volume:f2}");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}