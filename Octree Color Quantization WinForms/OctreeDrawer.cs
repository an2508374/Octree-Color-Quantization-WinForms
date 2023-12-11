using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public class OctreeDrawer
    {
        private Octree Octree { get; set; }
        private Bitmap VisualizationImage { get; set; }
        private Graphics Graphics { get; set; }
        private Pen Pen { get; set; }
        private double XStep { get; set; }
        private int YStep { get; set; }
        private double XLastLeaf { get; set; }

        public OctreeDrawer(Octree octree, Bitmap visualizationImage)
        {
            Octree = octree;
            VisualizationImage = visualizationImage;
            Graphics = Graphics.FromImage(VisualizationImage);
            Pen = new Pen(Brushes.Gray, Const.lineThickness);
        }

        private void FillCircle(Node node, Point med, int rad)
        {
            SolidBrush brush = new SolidBrush(Octree.Palette[node.PaletteIndex]);
            Graphics.FillEllipse(brush, new Rectangle(new Point(med.X - rad, med.Y - rad), new Size(2 * rad, 2 * rad)));
            brush.Dispose();
        }

        public void DrawTreeOnBitmap(double xStep, int yBegin, int yStep)
        {
            XStep = xStep;
            YStep = yStep;
            XLastLeaf = Const.visualizationMargin;
            FindTreeNodesCoordsRec(Octree.Root, yBegin);
            DrawTreeEdgesRec(Octree.Root);
            DrawTreeNodesRec(Octree.Root);
            Graphics.Dispose();
        }

        private int FindTreeNodesCoordsRec(Node node, int yValue)
        {
            int xValue;

            if (node.IsLeaf)
            {
                xValue = (int)XLastLeaf;
                XLastLeaf += XStep;

                node.BitmapX = xValue;
                node.BitmapY = yValue;

                return xValue;
            }

            int xMin = int.MaxValue;
            int xMax = int.MinValue;

            for (int i = 0; i < node.Children.Length; ++i)
            {
                Node? child = node.Children[i];
                if (child != null)
                {
                    int yChild = yValue + YStep;
                    int xChild = FindTreeNodesCoordsRec(child, yChild);

                    if (xChild < xMin)
                    {
                        xMin = xChild;
                    }

                    if (xChild > xMax)
                    {
                        xMax = xChild;
                    }
                }
            }

            xValue = (xMin + xMax) / 2;

            node.BitmapX = xValue;
            node.BitmapY = yValue;

            return xValue;
        }

        private void DrawTreeEdgesRec(Node node)
        {
            if (node.IsLeaf)
            {
                return;
            }

            for (int i = 0; i < node.Children.Length; ++i)
            {
                Node? child = node.Children[i];
                if (child != null)
                {
                    DrawTreeEdgesRec(child);
                    Graphics.DrawLine(Pen, new Point(node.BitmapX, node.BitmapY), new Point(child.BitmapX, child.BitmapY));
                }
            }
        }

        private void DrawTreeNodesRec(Node node)
        {
            if (node.IsLeaf)
            {
                FillCircle(node, new Point(node.BitmapX, node.BitmapY), Const.nodeRadius);
            }

            for (int i = 0; i < node.Children.Length; ++i)
            {
                Node? child = node.Children[i];
                if (child != null)
                {
                    DrawTreeNodesRec(child);
                }
            }

            FillCircle(node, new Point(node.BitmapX, node.BitmapY), Const.nodeRadius);
        }
    }
}
