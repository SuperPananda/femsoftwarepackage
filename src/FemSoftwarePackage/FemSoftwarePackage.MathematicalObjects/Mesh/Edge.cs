using FemSoftwarePackage.MathematicalObjects.Enum;

namespace FemSoftwarePackage.MathematicalObjects.Mesh
{
    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// Координата, к которой относится ребро
        /// </summary>
        public Coordinate Coordinate { get; set; }

        /// <summary>
        /// Узел первый
        /// </summary>
        public int NodeTag1 { get; set; }

        /// <summary>
        /// Узел второй
        /// </summary>
        public int NodeTag2 { get; set; }
    }
}
