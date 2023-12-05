using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public static class Const
    {
        public const int ChildCount = 8;
        public const int MaxDepth = 8;

        public const int PictureBoxXMargin = 30;
        public const int PictureBoxYMargin = 50;
    }

    public class Node
    {
        public ulong References { get; set; }
        public ulong Red { get; set; }
        public ulong Green { get; set; }
        public ulong Blue { get; set; }
        public Node?[] Children { get; set; }

        public Node()
        {
            References = 0;
            Red = 0;
            Green = 0;
            Blue = 0;
            Children = new Node[Const.ChildCount];
        }
    }

    public class Octree
    {
        public Node Root { get; private set; }
        private PriorityQueue<Node, ulong>[] Levels { get; set; }
        private int LeafCount { get; set; }

        public Octree()
        {
            Root = new Node();
            Levels = new PriorityQueue<Node, ulong>[Const.MaxDepth];
            LeafCount = 0;

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

            for (int i = 0; i < Const.MaxDepth; ++i)
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
        }

        public void UpdateFieldsRec(Node? node, int level)
        {
            if (node == null)
            {
                return;
            }

            bool isLeaf = true;
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
                    isLeaf = false;
                }
            }

            if (isLeaf)
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

                    for (int j = 0; j < Const.ChildCount; ++j)
                    {
                        if (minNode.Children[j] != null)
                        {
                            minNode.Children[j] = null;
                            --LeafCount;
                        }
                    }
                    ++LeafCount; // minNode becomes a leaf
                }
            }
        }
    }
}
