using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi1 : BaseBasicFunction
    {
        public Psi1()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { (region.EndY - p.Y) / region.Hy * (region.EndZ - p.Z) / region.Hz, 0, 0 };
        }
    }
}
