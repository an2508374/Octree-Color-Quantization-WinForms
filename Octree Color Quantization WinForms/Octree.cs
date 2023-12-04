using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public static class Const
    {
        public const int ChildNumber = 8;
        public const int MaxDepth = 8;
    }

    public class Node
    {
        public ulong references;
        public ulong red;
        public ulong green;
        public ulong blue;
        public Node?[] childs;

        public Node()
        {
            references = 0;
            red = 0;
            green = 0;
            blue = 0;
            childs = new Node[Const.ChildNumber];
        }
    }

    public class Octree
    {
        Node Root { get; set; }

        public Octree()
        {
            Root = new Node();
        }

        private int GetColorIndex(Color color, int level)
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

                nextNode = lastNode.childs[index];

                if (nextNode == null)
                {
                    nextNode = new Node();
                    lastNode.childs[index] = nextNode;
                }

                lastNode = nextNode;
            }

            ++lastNode.references;
            lastNode.red += color.R;
            lastNode.green += color.G;
            lastNode.blue += color.B;
        }
    }
}
