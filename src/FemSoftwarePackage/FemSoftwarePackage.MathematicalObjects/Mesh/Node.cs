namespace FemSoftwarePackage.MathematicalObjects.Mesh
{
    /// <summary>
    /// Узел
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Номер узла
        /// </summary>
        public int NodeTag { get; set; }

        /// <summary>
        /// Точка узла
        /// </summary>
        public Point Point { get; set; }
    }
}
