using FemSoftwarePackage.MathematicalObjects.Mesh;
using System;
using System.Collections.Generic;
using System.IO;

namespace FemSoftwarePackage.Logic.ConstructMesh
{
    /// <summary>
    /// Класс для генерации сетки
    /// </summary>
    public class GenerateMesh
    {
        /// <summary>
        /// Начальная точка
        /// </summary>
        private List<Point> beginPoints;

        /// <summary>
        /// Конечная точка
        /// </summary>
        private List<Point> endPoints;

        /// <summary>
        /// 
        /// </summary>
        private int[,] neighbors;

        /// <summary>
        /// Количество областей
        /// </summary>
        private int numberRegions;

        /// <summary>
        /// Количество ребер
        /// </summary>
        private int numberEdges;

        /// <summary>
        /// Количество узлов
        /// </summary>
        private int numberNodes;

        /// <summary>
        /// Количество паралепипедов
        /// </summary>
        private int numberParallelepipeds;

        private int[,] k;

        private Node[] nodes;

        private Edge[] edges;

        private FiniteElement[] elements;

        private double eps = 1.0e-16;

        public GenerateMesh(string fileName)
        {
            GetData(fileName);
            var finiteElements = new FiniteElement[numberParallelepipeds];
            numberNodes = GetNumNodes();
            numberEdges = GetNumEdges();

            nodes = new Node[numberNodes];
            GetNodes();
            edges = new Edge[numberEdges];
            GetEdges();
            elements = new FiniteElement[numberParallelepipeds];
            for (int i = 0; i < numberParallelepipeds; i++)
            {
                elements[i] = new FiniteElement
                {
                    Nodes = new int[8],
                    Edges = new int[12]
                };
            }
            CreatFiniteElementNode();
            CreatFiniteElementEdge();
            Console.WriteLine(elements.Length);
            foreach (var element in elements)
            {
                Console.WriteLine();
                foreach (var node in element.Nodes)
                {
                    Console.Write(node);
                    Console.Write(" ");
                }
                Console.WriteLine();
                foreach (var node in element.Edges)
                {
                    Console.Write(node);
                    Console.Write(" ");
                }
            }
        }

        private void GetData(string fileName)
        {
            numberParallelepipeds = 0;
            beginPoints = new List<Point>();
            endPoints = new List<Point>();
            using (StreamReader sr = new StreamReader(fileName, System.Text.Encoding.Default))
            {
                string line;
                var i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    var c = line.Split(" ");
                    if (i == 0)
                    {
                        numberRegions = int.Parse(line);
                        k = new int[numberRegions, 3];
                        neighbors = new int[numberRegions, 3];
                    }
                    if (i == 1)
                    {
                        for (var j = 0; j < numberRegions; j++)
                        {
                            var beginPoint = new Point
                            {
                                X = double.Parse(c[0]),
                                Y = double.Parse(c[1]),
                                Z = double.Parse(c[2])
                            };

                            beginPoints.Add(beginPoint);

                            var endPoint = new Point
                            {
                                X = double.Parse(c[3]),
                                Y = double.Parse(c[4]),
                                Z = double.Parse(c[5])
                            };

                            endPoints.Add(endPoint);

                            neighbors[j, 0] = int.Parse(c[6]);
                            neighbors[j, 1] = int.Parse(c[7]);
                            neighbors[j, 2] = int.Parse(c[8]);
                        }
                    }
                    if (i == 2)
                    {
                        for (var j = 0; j < numberRegions; j++)
                        {
                            k[j, 0] = int.Parse(c[0]);
                            k[j, 1] = int.Parse(c[1]);
                            k[j, 2] = int.Parse(c[2]);

                            numberParallelepipeds += k[j, 0] * k[j, 1] * k[j, 2];
                        }                       
                    }
                    
                    i++;
                }
            }
        }

        private int GetNumNodes()
        {
            var z = 0;
            var num = 0;
            for (var i = 0; i < numberRegions; i++)
            {
                z = (k[i, 0] + 1) * (k[i, 1] + 1) * (k[i, 2] + 1);
                if (neighbors[i, 0] != -1 && neighbors[i, 1] == -1 && neighbors[i, 2] == -1) z -= (k[i, 1] + 1) * (k[i, 2] + 1);
                if (neighbors[i, 0] == -1 && neighbors[i, 1] != -1 && neighbors[i, 2] == -1) z -= (k[i, 0] + 1) * (k[i, 2] + 1);
                if (neighbors[i, 0] == -1 && neighbors[i, 1] == -1 && neighbors[i, 2] != -1) z -= (k[i, 0] + 1) * (k[i, 1] + 1);
                if (neighbors[i, 0] != -1 && neighbors[i, 1] != -1 && neighbors[i, 2] == -1) z -= (k[i, 1] + 1) * (k[i, 2] + 1) + (k[i, 0] + 1) * (k[i, 2] + 1) - (k[i, 2] + 1);
                if (neighbors[i, 0] == -1 && neighbors[i, 1] != -1 && neighbors[i, 2] != -1) z -= (k[i, 0] + 1) * (k[i, 2] + 1) + (k[i, 0] + 1) * (k[i, 1] + 1) - (k[i, 0] + 1);
                if (neighbors[i, 0] != -1 && neighbors[i, 1] == -1 && neighbors[i, 2] != -1) z -= (k[i, 0] + 1) * (k[i, 1] + 1) + (k[i, 1] + 1) * (k[i, 2] + 1) - (k[i, 1] + 1);
                if (neighbors[i, 0] != -1 && neighbors[i, 1] != -1 && neighbors[i, 2] != -1) z -= (k[i, 1] + 1) * (k[i, 2] + 1) + (k[i, 0] + 1) * (k[i, 2] + 1) + (k[i, 0] + 1) * (k[i, 1] + 1) - (k[i, 0] + 1) - (k[i, 1] + 1) - (k[i, 2] + 1) + 1;
                num += z;
                z = 0;
            }

            return num;
        }

        private int GetNumEdges()
        {
            int z = 0;
            var num = 0;
            for (int i = 0; i < numberRegions; i++)
            {
                z = k[i, 0] * (k[i, 1] + 1) * (k[i, 2] + 1) + (k[i, 0] + 1) * k[i, 1] * (k[i, 2] + 1) + (k[i, 0] + 1) * (k[i, 1] + 1) * k[i, 2];
                if (neighbors[i, 0] != -1 && neighbors[i, 1] == -1 && neighbors[i, 2] == -1) z -= k[i, 2] * (k[i, 1] + 1) + k[i, 1] * (k[i, 2] + 1);
                if (neighbors[i, 0] == -1 && neighbors[i, 1] != -1 && neighbors[i, 2] == -1) z -= k[i, 0] * (k[i, 2] + 1) + k[i, 2] * (k[i, 0] + 1);
                if (neighbors[i, 0] == -1 && neighbors[i, 1] == -1 && neighbors[i, 2] != -1) z -= k[i, 0] * (k[i, 1] + 1) + k[i, 1] * (k[i, 0] + 1);
                if (neighbors[i, 0] != -1 && neighbors[i, 1] != -1 && neighbors[i, 2] == -1) z -= k[i, 2] * (k[i, 1] + 1) + k[i, 1] * (k[i, 2] + 1) + k[i, 0] * (k[i, 2] + 1) + k[i, 2] * (k[i, 0] + 1) - k[i, 1];
                if (neighbors[i, 0] == -1 && neighbors[i, 1] != -1 && neighbors[i, 2] != -1) z -= k[i, 0] * (k[i, 1] + 1) + k[i, 1] * (k[i, 0] + 1) + k[i, 0] * (k[i, 2] + 1) + k[i, 2] * (k[i, 0] + 1) - k[i, 0];
                if (neighbors[i, 0] != -1 && neighbors[i, 1] == -1 && neighbors[i, 2] != -1) z -= k[i, 2] * (k[i, 1] + 1) + k[i, 1] * (k[i, 2] + 1) + k[i, 0] * (k[i, 1] + 1) + k[i, 1] * (k[i, 0] + 1) - k[i, 2];
                if (neighbors[i, 0] != -1 && neighbors[i, 1] != -1 && neighbors[i, 2] != -1) z -= k[i, 2] * (k[i, 1] + 1) + k[i, 1] * (k[i, 2] + 1) + k[i, 0] * (k[i, 1] + 1) + k[i, 1] * (k[i, 0] + 1) + k[i, 0] * (k[i, 2] + 1) + k[i, 2] * (k[i, 0] + 1) - k[i, 0] - k[i, 1] - k[i, 2];
                num += z;
                z = 0;
            }
            return num;
        }

        private void GetNodes()
        {
            var num_uzla = 0;
            int n_obl = 0;
            var flag2 = true;
            int n_obl1, n_obl2;
            int n_obl0 = -1;
            n_obl2 = n_obl;
            while (flag2)
            {

                for (int i = 0; i < k[n_obl2, 2] + 1; i++)
                {
                    var flag1 = true;
                    n_obl1 = n_obl2;
                    while (flag1)
                    {
                        for (int j = 0; j < k[n_obl1, 1] + 1; j++)
                        {
                            var flag0 = true;
                            n_obl0 = n_obl1;
                            while (flag0)
                            {
                                for (int l = 0; l < k[n_obl0,0] + 1; l++)
                                {
                                    var hz = (endPoints[n_obl0].Z - beginPoints[n_obl0].Z) / k[n_obl0, 2];
                                    var hy = (endPoints[n_obl0].Y - beginPoints[n_obl0].Y) / k[n_obl0, 1];
                                    var hx = (endPoints[n_obl0].X - beginPoints[n_obl0].X) / k[n_obl0, 0];
                                    double x = beginPoints[n_obl0].X + hx * l;
                                    double y = beginPoints[n_obl0].Y + hy * j;
                                    double z = beginPoints[n_obl0].Z + hz * i;
                                    if (SearchPoint(x, y, z, num_uzla) == -1)
                                    {
                                        nodes[num_uzla] = new Node
                                        {
                                            Point = new Point
                                            {
                                                X = x,
                                                Y = y,
                                                Z = z
                                            }
                                        };
                                        num_uzla++;
                                    }
                                }
                                if (n_obl0 + 1 < numberRegions)
                                    if (neighbors[n_obl0 + 1,0] == n_obl0)
                                    {
                                        n_obl0++;
                                    }
                                    else flag0 = false;
                                else flag0 = false;
                            }
                        }

                        var flag3 = true;
                        flag1 = false;
                        for (int l = n_obl0 + 1; l < numberRegions && flag3; l++)
                        {
                            for (int r = n_obl1; r < n_obl0 + 1 && flag3; r++)
                                if (neighbors[l,1] == r)
                                {
                                    n_obl1 = l;
                                    flag3 = false;
                                    flag1 = true;
                                }
                        }
                    }
                }

                var flag4 = true;
                flag2 = false;
                for (int l = n_obl0 + 1; l < numberRegions && flag4; l++)
                {
                    for (int r = n_obl2; r < n_obl0 + 1 && flag4 && n_obl0 + 1 < numberRegions; r++)
                    {
                        if (neighbors[l, 2] == r)
                        {
                            n_obl2 = l;
                            flag4 = false;
                            flag2 = true;
                        }
                    }                    
                }
            }
        }

        private int SearchPoint(double x, double y, double z, int num_uzla = -1)
        {
            if (num_uzla == -1)
            {
                num_uzla = numberNodes;
            }

            for (int i = 0; i < num_uzla; i++)
            {
                if (Math.Abs(x - nodes[i].Point.X) < eps && Math.Abs(y - nodes[i].Point.Y) < eps && Math.Abs(z - nodes[i].Point.Z) < eps)
                    return i;
            }
            return -1;
        }

        private void GetEdges()
        {
            var numberEdge = 0;
            int n_obl = 0;
            var flag2 = true;
            int n_obl1, n_obl2;
            int n_obl0 = -1;
            n_obl2 = n_obl;
            bool flag1;
            while (flag2)
            {
                for (int i = 0; i < k[n_obl2, 2] + 1; i++)
                {
                    flag1 = true;
                    n_obl1 = n_obl2;
                    while (flag1)
                    {
                        for (int j = 0; j < k[n_obl1, 1] + 1; j++)
                        {
                            var flag0 = true;
                            n_obl0 = n_obl1;
                            while (flag0)
                            {
                                var hz = (endPoints[n_obl1].Z - beginPoints[n_obl1].Z) / k[n_obl1, 2];
                                var hy = (endPoints[n_obl1].Y - beginPoints[n_obl1].Y) / k[n_obl1, 1];
                                var hx = (endPoints[n_obl1].X - beginPoints[n_obl1].X) / k[n_obl1, 0];
                                for (int l = 0; l < k[n_obl1,0]; l++)
                                {
                                    double x = beginPoints[n_obl0].X + hx * l;
                                    double y = beginPoints[n_obl0].Y + hy * j;
                                    double z = beginPoints[n_obl0].Z + hz * i;
                                    var edgeNodes = new int[2];
                                    edgeNodes[0] = SearchPoint(x, y, z, numberNodes);
                                    edgeNodes[1] = SearchPoint(x + hx, y, z, numberNodes);
                                    if (SearchEdge(edgeNodes, numberEdge) == -1)
                                    {
                                        edges[numberEdge] = new Edge
                                        {
                                            NodeTag1 = edgeNodes[0],
                                            NodeTag2 = edgeNodes[1]
                                        };
                                        numberEdge++;
                                    }
                                }
                                if (n_obl0 + 1 < numberRegions)
                                    if (neighbors[n_obl0 + 1, 0] == n_obl0)
                                    {
                                        n_obl0++;
                                    }
                                    else flag0 = false;
                                else flag0 = false;
                            }


                        }
                        var flag3 = true;
                        flag1 = false;
                        for (int l = n_obl0 + 1; l < numberRegions && flag3; l++)
                        {
                            for (int r = n_obl1; r < n_obl0 + 1 && flag3; r++)
                                if (neighbors[l,1] == r)
                                {
                                    n_obl1 = l;
                                    flag3 = false;
                                    flag1 = true;
                                }
                        }
                    }

                    flag1 = true;
                    n_obl1 = n_obl2;
                    while (flag1)
                    {
                        for (int j = 0; j < k[n_obl1, 1]; j++)
                        {
                            var flag0 = true;
                            n_obl0 = n_obl1;
                            while (flag0)
                            {
                                var hz = (endPoints[n_obl1].Z - beginPoints[n_obl1].Z) / k[n_obl1,2];
                                var hy = (endPoints[n_obl1].Y - beginPoints[n_obl1].Y) / k[n_obl1,1];
                                var hx = (endPoints[n_obl1].X - beginPoints[n_obl1].X) / k[n_obl1,0];
                                for (var l = 0; l < k[n_obl1, 0] + 1; l++)
                                {
                                    var x = beginPoints[n_obl0].X + hx * l;
                                    var y = beginPoints[n_obl0].Y + hy * j;
                                    var z = beginPoints[n_obl0].Z + hz * i;
                                    var edgeNodes = new int[2];
                                    edgeNodes[0] = SearchPoint(x, y, z, numberNodes);
                                    edgeNodes[1] = SearchPoint(x, y + hy, z, numberNodes);
                                    if (SearchEdge(edgeNodes, numberEdge) == -1)
                                    {
                                        edges[numberEdge] = new Edge
                                        {
                                            NodeTag1 = edgeNodes[0],
                                            NodeTag2 = edgeNodes[1]
                                        };
                                        numberEdge++;
                                    }
                                }
                                if (n_obl0 + 1 < numberRegions)
                                    if (neighbors[n_obl0 + 1,0] == n_obl0)
                                    {
                                        n_obl0++;
                                    }
                                    else flag0 = false;
                                else flag0 = false;
                            }


                        }
                        var flag3 = true;
                        flag1 = false;
                        for (int l = n_obl0 + 1; l < numberRegions && flag3; l++)
                        {
                            for (int r = n_obl1; r < n_obl0 + 1 && flag3; r++)
                                if (neighbors[l,1] == r)
                                {
                                    n_obl1 = l;
                                    flag3 = false;
                                    flag1 = true;
                                }
                        }
                    }

                    flag1 = true;
                    n_obl1 = n_obl2;
                    if (i < k[n_obl2,2])
                        while (flag1)
                        {
                            for (int j = 0; j < k[n_obl1,1] + 1; j++)
                            {
                                var flag0 = true;
                                n_obl0 = n_obl1;
                                while (flag0)
                                {
                                    var hz = (endPoints[n_obl1].Z - beginPoints[n_obl1].Z) / k[n_obl1, 2];
                                    var hy = (endPoints[n_obl1].Y - beginPoints[n_obl1].Y) / k[n_obl1, 1];
                                    var hx = (endPoints[n_obl1].X - beginPoints[n_obl1].X) / k[n_obl1, 0];
                                    for (int l = 0; l < k[n_obl1,0] + 1; l++)
                                    {
                                        double x = beginPoints[n_obl0].X + hx * l;
                                        double y = beginPoints[n_obl0].Y + hy * j;
                                        double z = beginPoints[n_obl0].Z + hz * i;
                                        var edgeNodes = new int[2];
                                        edgeNodes[0] = SearchPoint(x, y, z, numberNodes);
                                        edgeNodes[1] = SearchPoint(x, y, z + hz, numberNodes);
                                        if (SearchEdge(edgeNodes, numberEdge) == -1)
                                        {
                                            edges[numberEdge] = new Edge
                                            {
                                                NodeTag1 = edgeNodes[0],
                                                NodeTag2 = edgeNodes[1]
                                            };
                                            numberEdge++;
                                        }
                                    }
                                    if (n_obl0 + 1 < numberRegions)
                                        if (neighbors[n_obl0 + 1,0] == n_obl0)
                                        {
                                            n_obl0++;
                                        }
                                        else flag0 = false;
                                    else flag0 = false;
                                }


                            }
                            var flag3 = true;
                            flag1 = false;
                            for (int l = n_obl0 + 1; l < numberRegions && flag3; l++)
                            {
                                for (int r = n_obl1; r < n_obl0 + 1 && flag3; r++)
                                    if (neighbors[l,1] == r)
                                    {
                                        n_obl1 = l;
                                        flag3 = false;
                                        flag1 = true;
                                    }
                            }
                        }

                }
                var flag4 = true;
                flag2 = false;
                for (int l = n_obl0 + 1; l < numberRegions && flag4; l++)
                {
                    for (int r = n_obl2; r < n_obl0 + 1 && flag4 && n_obl0 + 1 < numberRegions; r++)
                        if (neighbors[l,2] == r)
                        {
                            n_obl2 = l;
                            flag4 = false;
                            flag2 = true;
                        }
                }

            }
        }

        private int SearchEdge(int[] edgeNodes, int number_edge = -1)
        {
            if (number_edge == -1)
            {
                number_edge = numberEdges;
            }

            int k = -1;
            for (int i = 0; k == -1 && i < number_edge; i++)
                if (edgeNodes[0] == edges[i].NodeTag1 && edgeNodes[1] == edges[i].NodeTag2 || edgeNodes[1] == edges[i].NodeTag1 && edgeNodes[0] == edges[i].NodeTag2) 
                    k = i;
            return k;
        }

        private void CreatFiniteElementNode()
        {
            int n_obl = 0;
            var flag2 = true;
            int n_obl1, n_obl2;
            int n_obl0 = -1;
            n_obl2 = n_obl;
            int f = 0;
            while (flag2)
            {

                for (int i = 0; i < k[n_obl2, 2]; i++)
                {
                    var flag1 = true;
                    n_obl1 = n_obl2;
                    while (flag1)
                    {
                        for (int j = 0; j < k[n_obl1,1]; j++)
                        {
                            var flag0 = true;
                            n_obl0 = n_obl1;
                            while (flag0)
                            {
                                var hz = (endPoints[n_obl1].Z - beginPoints[n_obl1].Z) / k[n_obl1, 2];
                                var hy = (endPoints[n_obl1].Y - beginPoints[n_obl1].Y) / k[n_obl1, 1];
                                var hx = (endPoints[n_obl1].X - beginPoints[n_obl1].X) / k[n_obl1, 0];
                                for (int l = 0; l < k[n_obl1, 0]; l++)
                                {
                                    double x = beginPoints[n_obl0].X + hx * l;
                                    double y = beginPoints[n_obl0].Y + hy * j;
                                    double z = beginPoints[n_obl0].Z + hz * i;
                                    if (SearchParallelepiped(SearchPoint(x, y, z),
                                        SearchPoint(x + hx, y, z),
                                        SearchPoint(x, y + hy, z),
                                        SearchPoint(x + hx, y + hy, z),
                                        SearchPoint(x, y, z + hz),
                                        SearchPoint(x + hx, y, z + hz),
                                        SearchPoint(x, y + hy, z + hz),
                                        SearchPoint(x + hx, y + hy, z + hz),
                                        f))
                                    {
                                        var elemNodes = new int[8];
                                        elemNodes[0] = SearchPoint(x, y, z);
                                        elemNodes[1] = SearchPoint(x + hx, y, z);
                                        elemNodes[2] = SearchPoint(x, y + hy, z);
                                        elemNodes[3] = SearchPoint(x + hx, y + hy, z);
                                        elemNodes[4] = SearchPoint(x, y, z + hz);
                                        elemNodes[5] = SearchPoint(x + hx, y, z + hz);
                                        elemNodes[6] = SearchPoint(x, y + hy, z + hz);
                                        elemNodes[7] = SearchPoint(x + hx, y + hy, z + hz);
                                        //elements[f,8] = n_obl0;
                                        elements[f].Nodes = elemNodes;
                                        f++;
                                    }
                                }
                                if (n_obl0 + 1 < numberRegions)
                                    if (neighbors[n_obl0 + 1,0] == n_obl0)
                                    {
                                        n_obl0++;
                                    }
                                    else flag0 = false;
                                else flag0 = false;
                            }
                        }
                        var flag3 = true;
                        flag1 = false;
                        for (int l = n_obl0 + 1; l < numberRegions && flag3; l++)
                        {
                            for (int r = n_obl1; r < n_obl0 + 1 && flag3; r++)
                                if (neighbors[l,1] == r)
                                {
                                    n_obl1 = l;
                                    flag3 = false;
                                    flag1 = true;
                                }
                        }
                    }
                }
                var flag4 = true;
                flag2 = false;
                for (int l = n_obl0 + 1; l < numberRegions && flag4; l++)
                {
                    for (int r = n_obl2; r < n_obl0 + 1 && flag4 && n_obl0 + 1 < numberRegions; r++)
                        if (neighbors[l,2] == r)
                        {
                            n_obl2 = l;
                            flag4 = false;
                            flag2 = true;
                        }
                }

            }
        }

        private bool SearchParallelepiped(int point_1, int point_2, int point_3, int point_4, int point_5, int point_6, int point_7, int point_8, int f)
        {         
            for (int i = 0; i < f; i++)
            {
                var elemNodes = elements[i].Nodes;
                if (elemNodes[0] == point_1 && elemNodes[1] == point_2 && elemNodes[2] == point_3 && elemNodes[3] == 4 && elemNodes[4] == point_5 && elemNodes[5] == point_6 && elemNodes[6] == point_7 && elemNodes[7] == point_8)
                    return false;
            }
            return true;
        }

        private void CreatFiniteElementEdge()
        {
            for (int j = 0; j < numberParallelepipeds; j++)
            {
                elements[j].Edges = new int[12];
                var rebro = new int[2];
                var elemNodes = elements[j].Nodes;
                rebro[0] = elemNodes[0];
                rebro[1] = elemNodes[1];
                AddElem(j, 0, rebro);
                rebro[0] = elemNodes[2];
                rebro[1] = elemNodes[3];
                AddElem(j, 1, rebro);
                rebro[0] = elemNodes[4];
                rebro[1] = elemNodes[5];
                AddElem(j, 2, rebro);
                rebro[0] = elemNodes[6];
                rebro[1] = elemNodes[7];
                AddElem(j, 3, rebro);
                rebro[0] = elemNodes[0];
                rebro[1] = elemNodes[2];
                AddElem(j, 4, rebro);
                rebro[0] = elemNodes[1];
                rebro[1] = elemNodes[3];
                AddElem(j, 5, rebro);
                rebro[0] = elemNodes[4];
                rebro[1] = elemNodes[6];
                AddElem(j, 6, rebro);
                rebro[0] = elemNodes[5];
                rebro[1] = elemNodes[7];
                AddElem(j, 7, rebro);
                rebro[0] = elemNodes[0];
                rebro[1] = elemNodes[4];
                AddElem(j, 8, rebro);
                rebro[0] = elemNodes[1];
                rebro[1] = elemNodes[5];
                AddElem(j, 9, rebro);
                rebro[0] = elemNodes[2];
                rebro[1] = elemNodes[6];
                AddElem(j, 10, rebro);
                rebro[0] = elemNodes[3];
                rebro[1] = elemNodes[7];
                AddElem(j, 11, rebro);
            }
        }

        private void AddElem(int i, int j, int[] elem)
        {
            var num = SearchEdge(elem);
            elements[i].Edges[j] = num;
        }
    }
}
