using System;
using FemSoftwarePackage.MathematicalObjects;

namespace FemSoftwarePackage
{
    class Program
    {
        static void Main()
        {
            var a = new double[3];
            var b = new double[3];
            var c = Vector.Multiply(a, b);
            Console.WriteLine("Hello World!");
        }
    }
}
