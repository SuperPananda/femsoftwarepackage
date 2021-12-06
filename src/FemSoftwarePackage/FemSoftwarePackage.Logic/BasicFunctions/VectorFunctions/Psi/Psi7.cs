using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi7 : BaseBasicFunction
    {
        public Psi7()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, (region.EndX - p.X) / region.Hx * (p.Z - region.BeginZ) / region.Hz, 0 };
        }
    }
}
