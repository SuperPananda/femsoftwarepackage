using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi5 : BaseBasicFunction
    {
        public Psi5()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (region.EndX - p.X) / region.Hx * (region.EndZ - p.Z) / region.Hz, 0 };
        }
    }
}
