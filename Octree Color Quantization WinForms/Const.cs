using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public static class Const
    {
        public const int childCount = 8;
        public const int maxDepth = 8;
        public const int minColorCountDefault = 256;
        public const int stepCountDefault = 10;
        public const int downscalingRatioDefault = 2;

        public const int pictureBoxLowerMargin = 10;
        public const int pictureBoxUpperMargin = 25;
        public const int visualizationImageMaxWidth = 19000;
        public const int visualizationImageMaxXStep = 30;
        public const int visualizationMargin = 20;
        public const int nodeRadius = 8;
        public const int lineThickness = 1;

        public const int defaultGenerateWidth = 1000;
        public const int defaultGenerateHeight = 1000;
    }
}
