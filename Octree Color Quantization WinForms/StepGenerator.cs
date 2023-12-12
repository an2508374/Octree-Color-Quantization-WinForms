using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public interface IStepGenerator
    {
        public string GetLabelOption();
        public string GetTextBoxOption();
        public int GetOptionValue();
        public bool IsLastStep();
        public void Reset();
        public void UpdateOptionAfterStep();
        public void UpdateOption(int option);
        public int GetCurrentColorCount(int leafCount, int minColorCount);
    }

    public class LinearStepGenerator : IStepGenerator
    {
        private int stepCount;
        private bool isLastStep;

        public LinearStepGenerator()
        {
            stepCount = Const.stepCountDefault;
            isLastStep = false;
        }

        public string GetLabelOption() => "Number of remaining steps:";

        public string GetTextBoxOption() => stepCount.ToString();

        public int GetOptionValue() => stepCount;

        public bool IsLastStep() => isLastStep;

        public void Reset()
        {
            stepCount = Const.stepCountDefault;
            isLastStep = false;
        }

        public void UpdateOptionAfterStep()
        {
            --stepCount;
        }

        public void UpdateOption(int stepCount)
        {
            this.stepCount = stepCount;
        }

        public int GetCurrentColorCount(int leafCount, int minColorCount)
        {
            if (stepCount == 0)
            {
                isLastStep = true;
                return minColorCount;
            }

            int currentColorCount = leafCount;
            currentColorCount -= (leafCount - minColorCount) / stepCount;

            if (minColorCount > currentColorCount)
            {
                isLastStep = true;
                return minColorCount;
            }

            return currentColorCount;
        }
    }

    public class ExponentialStepGenerator : IStepGenerator
    {
        private int downscalingRatio;
        private bool isLastStep;

        public ExponentialStepGenerator()
        {
            downscalingRatio = Const.downscalingRatioDefault;
            isLastStep = false;
        }

        public string GetLabelOption() => "Ratio of downscaling:";

        public string GetTextBoxOption() => downscalingRatio.ToString();

        public int GetOptionValue() => downscalingRatio;

        public bool IsLastStep() => isLastStep;

        public void Reset()
        {
            downscalingRatio = Const.downscalingRatioDefault;
            isLastStep = false;
        }

        public void UpdateOptionAfterStep()
        {}

        public void UpdateOption(int downscalingRatio)
        {
            this.downscalingRatio = downscalingRatio;
        }

        public int GetCurrentColorCount(int leafCount, int minColorCount)
        {
            if (downscalingRatio == 0)
            {
                isLastStep = true;
                return minColorCount;
            }

            int currentColorCount = leafCount / downscalingRatio;

            if (minColorCount > currentColorCount)
            {
                isLastStep = true;
                return minColorCount;
            }

            return currentColorCount;
        }
    }
}
