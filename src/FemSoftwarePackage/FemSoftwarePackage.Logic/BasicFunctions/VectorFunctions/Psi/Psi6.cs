using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi6 : BaseBasicFunction
    {
        public Psi6()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (p.X - region.BeginX) / region.Hx * (region.EndZ - p.Z) / region.Hz, 0 };
        }
    }
}
