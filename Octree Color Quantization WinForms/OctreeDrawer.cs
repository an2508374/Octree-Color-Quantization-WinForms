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
            DrawTreeRec(Octree.Root, yBegin);
            Graphics.Dispose();
        }

        private int DrawTreeRec(Node node, int yValue)
        {
            int xValue;

            if (node.IsLeaf)
            {
                xValue = (int)XLastLeaf;
                XLastLeaf += XStep;
                FillCircle(node, new Point(xValue, yValue), Const.nodeRadius);

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
                    int xChild = DrawTreeRec(child, yChild);

                    //Graphics.DrawLine(Pen, new Point(xValue, yValue), new Point(xChild, yChild));

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
            FillCircle(node, new Point(xValue, yValue), Const.nodeRadius);

            return xValue;
        }
    }
}
