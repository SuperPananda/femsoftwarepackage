using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBaseBasicFunction
    {
        double[] Psi(Point p, Region region);
    }
}
