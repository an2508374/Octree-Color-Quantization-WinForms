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
        public ulong references;
        public ulong red;
        public ulong green;
        public ulong blue;
        public Node?[] children;

        public Node()
        {
            references = 0;
            red = 0;
            green = 0;
            blue = 0;
            children = new Node[Const.ChildCount];
        }
    }

    public class Octree
    {
        public Node Root { get; set; }
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

                nextNode = lastNode.children[index];

                if (nextNode == null)
                {
                    nextNode = new Node();
                    lastNode.children[index] = nextNode;
                }

                lastNode = nextNode;
            }

            ++lastNode.references;
            lastNode.red += color.R;
            lastNode.green += color.G;
            lastNode.blue += color.B;
        }

        public void UpdateFieldsRec(Node? node, int level)
        {
            if (node == null)
            {
                return;
            }

            bool isLeaf = true;
            for (int i = 0; i < Const.ChildCount; ++i)
            {
                Node? child = node.children[i];

                UpdateFieldsRec(child, level + 1);
                
                if (child != null)
                {
                    node.references += child.references;
                    node.red += child.red;
                    node.green += child.green;
                    node.blue += child.blue;
                    isLeaf = false;
                }
            }

            if (isLeaf)
            {
                ++LeafCount;
            }
            else
            {
                Levels[level].Enqueue(node, node.references);
            }
        }

        public void UpdateTree(int colorCount)
        {
            for (int i = Const.MaxDepth - 1; i >= 0; --i)
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
                        if (minNode.children[j] != null)
                        {
                            minNode.children[j] = null;
                            --LeafCount;
                        }
                    }
                    ++LeafCount; // minNode becomes a leaf
                }
            }
        }
    }
}
