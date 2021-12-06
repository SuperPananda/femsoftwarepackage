using FemSoftwarePackage.MathematicalObjects.Enum;

namespace FemSoftwarePackage.MathematicalObjects.Mesh
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Номер ребра
        /// </summary>
        public int EdgeTag { get; set; }

        /// <summary>
        /// Координата, к которой относится ребро
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Узел первый
        /// </summary>
        public Node Node1 { get; set; }

        /// <summary>
        /// Узел второй
        /// </summary>
        public Node Node2 { get; set; }
    }
}
