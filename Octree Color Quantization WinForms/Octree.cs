using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public class Node
    {
        public ulong References { get; set; }
        public ulong Red { get; set; }
        public ulong Green { get; set; }
        public ulong Blue { get; set; }
        public Node?[] Children { get; set; }
        public bool IsLeaf { get; set; }
        public int PaletteIndex { get; set; }
        public int BitmapX { get; set; }
        public int BitmapY { get; set; }

        public Node()
        {
            References = 0;
            Red = 0;
            Green = 0;
            Blue = 0;
            Children = new Node[Const.childCount];
        }
    }

    public class Octree
    {
        public Node Root { get; private set; }
        private PriorityQueue<Node, ulong>[] Levels { get; set; }
        public int LeafCount { get; private set; }
        public List<Color> Palette { get; set; }
        public int PaletteLength { get; set; }

        public Octree()
        {
            Root = new Node();
            Levels = new PriorityQueue<Node, ulong>[Const.maxDepth];
            LeafCount = 0;
            Palette = new List<Color>();
            PaletteLength = 0;

            for (int i = 0; i < Levels.Length; ++i)
            {
                Levels[i] = new PriorityQueue<Node, ulong>();
            }
        }

        private static int GetColorIndex(Color color, int level)
        {
            int index = 0;
            int mask = 0b10000000 >> level;

            if ((color.R & mask) != 0)
            {
                index |= 0b100;
            }

            if ((color.G & mask) != 0)
            {
                index |= 0b010;
            }

            if ((color.B & mask) != 0)
            {
                index |= 0b001;
            }

            return index;
        }

        public void InsertColor(Color color)
        {
            int index;
            Node lastNode = Root;
            Node? nextNode;

            for (int i = 0; i < Const.maxDepth; ++i)
            {
                index = GetColorIndex(color, i);

                nextNode = lastNode.Children[index];

                if (nextNode == null)
                {
                    nextNode = new Node();
                    lastNode.Children[index] = nextNode;
                }

                lastNode = nextNode;
            }

            ++lastNode.References;
            lastNode.Red += color.R;
            lastNode.Green += color.G;
            lastNode.Blue += color.B;
            lastNode.IsLeaf = true;
        }

        public void UpdateFieldsRec(Node? node, int level)
        {
            if (node == null)
            {
                return;
            }

            for (int i = 0; i < node.Children.Length; ++i)
            {
                Node? child = node.Children[i];

                UpdateFieldsRec(child, level + 1);

                if (child != null)
                {
                    node.References += child.References;
                    node.Red += child.Red;
                    node.Green += child.Green;
                    node.Blue += child.Blue;
                }
            }

            if (node.IsLeaf)
            {
                ++LeafCount;
            }
            else
            {
                Levels[level].Enqueue(node, node.References);
            }
        }

        public void UpdateTree(int colorCount)
        {
            for (int i = Levels.Length - 1; i >= 0; --i)
            {
                PriorityQueue<Node, ulong> pQueue = Levels[i];

                while (true)
                {
                    if (LeafCount <= colorCount)
                    {
                        return;
                    }

                    if (pQueue.Count <= 0)
                    {
                        break;
                    }

                    Node minNode = pQueue.Dequeue();

                    for (int j = 0; j < minNode.Children.Length; ++j)
                    {
                        if (minNode.Children[j] != null)
                        {
                            minNode.Children[j] = null;
                            --LeafCount;
                        }
                    }

                    minNode.IsLeaf = true;
                    ++LeafCount;
                }
            }
        }

        public void UpdatePalette()
        {
            PaletteLength = 0;
            Palette.Clear();
            UpdatePaletteRec(Root);
        }

        private void UpdatePaletteRec(Node? node)
        {
            if (node == null)
            {
                return;
            }

            int red = (int)(node.Red / node.References);
            int green = (int)(node.Green / node.References);
            int blue = (int)(node.Blue / node.References);

            Palette.Add(Color.FromArgb(255, red, green, blue));
            node.PaletteIndex = PaletteLength++;

            if (node.IsLeaf)
            {
                return;
            }

            for (int i = 0; i < node.Children.Length; ++i)
            {
                UpdatePaletteRec(node.Children[i]);
            }
        }

        public int GetPaletteIndex(Color color)
        {
            int level = 0;
            int colorIndex, paletteIndex = 0;
            Node? node = Root;

            while (node != null && !node.IsLeaf)
            {
                colorIndex = GetColorIndex(color, level++);
                node = node.Children[colorIndex];
            }

            if (node != null)
            {
                paletteIndex = node.PaletteIndex;
            }

            return paletteIndex;
        }
    }
}
