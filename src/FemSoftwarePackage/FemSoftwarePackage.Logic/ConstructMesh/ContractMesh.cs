using FemSoftwarePackage.MathematicalObjects.Mesh;

namespace FemSoftwarePackage.Logic.ConstructMesh
{
    public class ContractMesh
    {
        /// <summary>
        /// Начальные точки
        /// </summary>
        public Point PointBegin { get; set; }

        /// <summary>
        /// Конечные точки
        /// </summary>
        public Point PointEnd { get; set; }
    }
}
