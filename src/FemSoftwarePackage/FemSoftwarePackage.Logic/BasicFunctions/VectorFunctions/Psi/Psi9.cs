using FemSoftwarePackage.Logic.BasicFunctions.Base;
using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.VectorFunctions.Psi
{
    public class Psi9 : BaseBasicFunction
    {
        public Psi9()
        {
        }

        public override double[] Psi(Point p, Region region)
        {
            return new double[] { 0, 0, (region.EndX - p.X) / region.Hx * (region.EndY - p.Y) / region.Hy };
        }
    }
}
