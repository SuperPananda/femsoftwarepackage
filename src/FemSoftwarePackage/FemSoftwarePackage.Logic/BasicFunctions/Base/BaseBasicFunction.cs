using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.BasicFunctions.Base
{
    /// <summary>
    /// Базовый класс базисной функции
    /// </summary>
    public abstract class BaseBasicFunction : IBaseBasicFunction
    {
        protected BaseBasicFunction()
        {
        }

        public virtual double[] Psi(Point p, Region region)
        {
            return null;
        }
    }
}
