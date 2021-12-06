namespace FemSoftwarePackage.MathematicalObjects.Mesh
{
    /// <summary>
    /// Конечный элемент
    /// </summary>
    public class FiniteElement
    {
        /// <summary>
        /// Номер конечного элемента
        /// </summary>
        public int ElementTag { get; set; }

        /// <summary>
        /// Узлы входящие в конечный элемент
        /// </summary>
        public Node[] Nodes { get; set; }

        /// <summary>
        /// Ребра входящие в конечный элемент
        /// </summary>
        public Edge[] Edges { get; set; }

        public Region GetRegion()
        {
            return new Region
            {
                BeginX = this.Nodes[0].Point.X,
                BeginY = this.Nodes[0].Point.Y,
                BeginZ = this.Nodes[0].Point.Z,
                EndX = this.Nodes[7].Point.X,
                EndY = this.Nodes[7].Point.Y,
                EndZ = this.Nodes[7].Point.Z,
                Hx = this.Nodes[7].Point.X - this.Nodes[0].Point.X,
                Hy = this.Nodes[7].Point.Y - this.Nodes[0].Point.Y,
                Hz = this.Nodes[7].Point.Z - this.Nodes[0].Point.Z
            };
        }
    }
}
