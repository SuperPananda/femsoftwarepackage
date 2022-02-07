using System;
using FemSoftwarePackage.Logic.ConstructMesh;
using FemSoftwarePackage.MathematicalObjects;

namespace FemSoftwarePackage
{
    class Program
    {
        static void Main()
        {
            var m = new GenerateMesh("input.txt");
            var a = new double[3];
            var b = new double[3];
            var c = Vector.Multiply(a, b);
            Console.WriteLine("Hello World!");
        }
    }
}
