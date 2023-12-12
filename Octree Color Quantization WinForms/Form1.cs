using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Octree_Color_Quantization_WinForms
{
    public partial class Form1 : Form
    {
        private Bitmap? importedImage;
        private Bitmap? processedImage;
        private Bitmap? visualizationImage;
        private Octree ocTree;
        private LinearStepGenerator linearStepGenerator;
        private ExponentialStepGenerator exponentialStepGenerator;
        private IStepGenerator stepGenerator;
        private int minColorCount;
        private int currentColorCount;

        private TableLayoutPanel verticalPanel = new TableLayoutPanel();
        private TableLayoutPanel horizontalPanel = new TableLayoutPanel();
        private Panel panelVisualization = new Panel();
        private PictureBox pictureBoxVisualization = new PictureBox();
        private PictureBox pictureBoxImported = new PictureBox();
        private PictureBox pictureBoxProcessed = new PictureBox();

        public Form1()
        {
            InitializeComponent();

            pictureBoxImported.Visible = false;
            groupBoxImported.Controls.Add(pictureBoxImported);

            pictureBoxProcessed.Visible = false;
            groupBoxProcessed.Controls.Add(pictureBoxProcessed);

            verticalPanel.ColumnCount = 1;
            verticalPanel.RowCount = 3;
            verticalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));

            horizontalPanel.ColumnCount = 3;
            horizontalPanel.RowCount = 1;
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            panel.Controls.Add(verticalPanel);
            verticalPanel.Controls.Add(menuStrip, 0, 0);
            verticalPanel.Controls.Add(horizontalPanel, 0, 1);
            verticalPanel.Controls.Add(groupBoxVisualization, 0, 2);

            verticalPanel.Dock = DockStyle.Fill;
            horizontalPanel.Dock = DockStyle.Fill;

            horizontalPanel.Controls.Add(groupBoxImported, 0, 0);
            horizontalPanel.Controls.Add(groupBoxProcessed, 1, 0);
            horizontalPanel.Controls.Add(groupBoxOptions, 2, 0);

            groupBoxVisualization.Controls.Add(panelVisualization);
            panelVisualization.Dock = DockStyle.Fill;
            panelVisualization.AutoScroll = true;
            panelVisualization.Controls.Add(pictureBoxVisualization);

            ocTree = new Octree();
            linearStepGenerator = new LinearStepGenerator();
            exponentialStepGenerator = new ExponentialStepGenerator();
            stepGenerator = linearStepGenerator;
            minColorCount = Const.minColorCountDefault;
            textBoxMinColorCount.Text = minColorCount.ToString();
            textBoxOption.Text = stepGenerator.GetTextBoxOption();
        }

        private void InsertColorsToOctree()
        {
            if (importedImage == null)
            {
                return;
            }

            ocTree = new Octree();

            for (int i = 0; i < importedImage.Width; ++i)
            {
                for (int j = 0; j < importedImage.Height; ++j)
                {
                    ocTree.InsertColor(importedImage.GetPixel(i, j));
                }
            }

            ocTree.UpdateFieldsRec(ocTree.Root, 0);
        }

        private void UpdateProcessedImage()
        {
            if (importedImage == null)
            {
                return;
            }

            processedImage?.Dispose();
            processedImage = new Bitmap(importedImage.Width, importedImage.Height);

            for (int i = 0; i < importedImage.Width; ++i)
            {
                for (int j = 0; j < importedImage.Height; ++j)
                {
                    int paletteIndex = ocTree.GetPaletteIndex(importedImage.GetPixel(i, j));
                    processedImage.SetPixel(i, j, ocTree.Palette[paletteIndex]);
                }
            }

            PictureSetter.SetPictureInPanel(groupBoxProcessed.Width, groupBoxProcessed.Height,
                pictureBoxProcessed, processedImage);
        }

        public void ScrollToLeft(Panel panel)
        {
            using (Control control = new Control() { Parent = panel, Dock = DockStyle.Left })
            {
                panel.ScrollControlIntoView(control);
                control.Parent = null;
            }
        }

        private void UpdateVisualizationImage()
        {
            ScrollToLeft(panelVisualization);
            if (importedImage == null)
            {
                return;
            }

            int visualizationImageWidth = Const.visualizationImageMaxWidth;
            int visualizationImageHeight = panelVisualization.Height - Const.pictureBoxUpperMargin;
            double xStep = ((double)visualizationImageWidth - (2 * Const.visualizationMargin)) / ocTree.LeafCount;
            if (xStep > Const.visualizationImageMaxXStep)
            {
                xStep = Const.visualizationImageMaxXStep;
                visualizationImageWidth = (int)xStep * ocTree.LeafCount + 2 * Const.visualizationMargin;
            }

            visualizationImage?.Dispose();
            visualizationImage = new Bitmap(visualizationImageWidth, visualizationImageHeight);

            int verticalStep = visualizationImage.Height / (Const.maxDepth + 1);
            OctreeDrawer ocTreeDrawer = new OctreeDrawer(ocTree, visualizationImage);
            ocTreeDrawer.DrawTreeOnBitmap(xStep, verticalStep / 2, verticalStep);

            pictureBoxVisualization.Location = new Point(0, 0);
            pictureBoxVisualization.Size = new Size(visualizationImageWidth, visualizationImageHeight);

            pictureBoxVisualization.Image?.Dispose();
            pictureBoxVisualization.Image = visualizationImage;
        }

        private void LoadGeneratedPicture(Bitmap bitmap)
        {
            importedImage?.Dispose();
            importedImage = bitmap;
            PictureSetter.SetPictureInPanel(groupBoxImported.Width, groupBoxImported.Height,
                pictureBoxImported, importedImage);

            pictureBoxProcessed.Image?.Dispose();
            pictureBoxProcessed.Image = null;

            linearStepGenerator.Reset();
            exponentialStepGenerator.Reset();
            textBoxOption.Text = stepGenerator.GetTextBoxOption();
            groupBoxProcessed.Text = "Processed Image";

            InsertColorsToOctree();
            groupBoxImported.Text = $"Imported Image ({ocTree.LeafCount} colors)";

            ocTree.UpdatePalette();
            UpdateVisualizationImage();
            groupBoxVisualization.Text = "Octree Visualization (For Imported Image)";
            buttonNextStep.Enabled = true;
        }

        private void ImportPictureMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog importFileDialog = new OpenFileDialog();

            importFileDialog.Title = "Import Image";
            importFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                LoadGeneratedPicture(new Bitmap(importFileDialog.FileName));
            }
        }

        private void ExportPictureMenuItem_Click(object sender, EventArgs e)
        {
            using SaveFileDialog exportFileDialog = new SaveFileDialog();

            exportFileDialog.Title = "Export Image";
            exportFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            ImageFormat imageFormat = ImageFormat.Jpeg;

            if (exportFileDialog.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(exportFileDialog.FileName);

                switch (extension)
                {
                    case ".bmp":
                    case ".BMP":
                        imageFormat = ImageFormat.Bmp;
                        break;
                    case ".png":
                    case ".PNG":
                        imageFormat = ImageFormat.Png;
                        break;
                }

                if (processedImage != null)
                {
                    processedImage.Save(exportFileDialog.FileName, imageFormat);
                }
            }
        }

        private void ButtonNextStep_Click(object sender, EventArgs e)
        {
            stepGenerator.UpdateOptionAfterStep();
            UpdateCurrentColorCount();
            ocTree.UpdateTree(currentColorCount);
            UpdateProcessedImage();

            textBoxOption.Text = stepGenerator.GetTextBoxOption();
            groupBoxProcessed.Text = $"Processed Image ({ocTree.LeafCount} colors)";

            if (stepGenerator.IsLastStep())
            {
                buttonNextStep.Enabled = false;
            }

            UpdateVisualizationImage();
            groupBoxVisualization.Text = "Octree Visualization (For Processed Image)";
        }

        private void UpdateCurrentColorCount()
        {
            currentColorCount = stepGenerator.GetCurrentColorCount(ocTree.LeafCount, minColorCount);
        }

        private int UpdateDataFromTextBox(TextBox textBox, int updatedValue)
        {
            int result = updatedValue;

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("This field must be an integer number.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }

            if (textBox.Text.Length > 0 && !Int32.TryParse(textBox.Text, out result))
            {
                MessageBox.Show("This field must be an integer number.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void TextBoxMinColorCount_TextChanged(object sender, EventArgs e)
        {
            minColorCount = UpdateDataFromTextBox(textBoxMinColorCount, minColorCount);
        }

        private void TextBoxStepCount_TextChanged(object sender, EventArgs e)
        {
            int changedOption = stepGenerator.GetOptionValue();
            stepGenerator.UpdateOption(UpdateDataFromTextBox(textBoxOption, changedOption));
        }

        private void RadioButtonLinearly_Click(object sender, EventArgs e)
        {
            stepGenerator = linearStepGenerator;
            labelOption.Text = stepGenerator.GetLabelOption();
            textBoxOption.Text = stepGenerator.GetTextBoxOption();
        }

        private void RadioButtonExponentially_Click(object sender, EventArgs e)
        {
            stepGenerator = exponentialStepGenerator;
            labelOption.Text = stepGenerator.GetLabelOption();
            textBoxOption.Text = stepGenerator.GetTextBoxOption();
        }

        private void Generate1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Const.defaultGenerateWidth, Const.defaultGenerateHeight);
            Color[,] colors = new Color[4, 2];

            colors[0, 0] = Color.FromArgb(255, 0, 0, 0);
            colors[1, 0] = Color.FromArgb(255, 255, 0, 0);
            colors[2, 0] = Color.FromArgb(255, 0, 255, 0);
            colors[3, 0] = Color.FromArgb(255, 0, 0, 255);
            colors[0, 1] = Color.FromArgb(255, 255, 255, 255);
            colors[1, 1] = Color.FromArgb(255, 0, 255, 255);
            colors[2, 1] = Color.FromArgb(255, 255, 0, 255);
            colors[3, 1] = Color.FromArgb(255, 255, 255, 0);

            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    int x = (int)Math.Floor((double)(4 * i) / bitmap.Width);
                    int y = (int)Math.Floor((double)(2 * j) / bitmap.Height);
                    bitmap.SetPixel(i, j, colors[x, y]);
                }
            }

            LoadGeneratedPicture(bitmap);
        }

        private void Generate2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Const.defaultGenerateWidth, Const.defaultGenerateHeight);

            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    int x = (255 * i) / bitmap.Width;
                    int y = (255 * j) / bitmap.Height;
                    y = 255 - y;

                    RgbColor rgb = HsvToRgb(new HsvColor(x, y, 255));
                    bitmap.SetPixel(i, j, Color.FromArgb(255, rgb.r, rgb.g, rgb.b));
                }
            }

            LoadGeneratedPicture(bitmap);
        }

        public struct RgbColor
        {
            public int r;
            public int g;
            public int b;

            public RgbColor(int r, int g, int b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }
        }

        public struct HsvColor
        {
            public int h;
            public int s;
            public int v;

            public HsvColor(int h, int s, int v)
            {
                this.h = h;
                this.s = s;
                this.v = v;
            }
        }

        RgbColor HsvToRgb(HsvColor hsv)
        {
            RgbColor rgb;
            int region, remainder, p, q, t;

            if (hsv.s == 0)
            {
                rgb.r = hsv.v;
                rgb.g = hsv.v;
                rgb.b = hsv.v;
                return rgb;
            }

            region = hsv.h / 43;
            remainder = (hsv.h - (region * 43)) * 6;

            p = (hsv.v * (255 - hsv.s)) >> 8;
            q = (hsv.v * (255 - ((hsv.s * remainder) >> 8))) >> 8;
            t = (hsv.v * (255 - ((hsv.s * (255 - remainder)) >> 8))) >> 8;

            switch (region)
            {
                case 0:
                    rgb.r = hsv.v; rgb.g = t; rgb.b = p;
                    break;
                case 1:
                    rgb.r = q; rgb.g = hsv.v; rgb.b = p;
                    break;
                case 2:
                    rgb.r = p; rgb.g = hsv.v; rgb.b = t;
                    break;
                case 3:
                    rgb.r = p; rgb.g = q; rgb.b = hsv.v;
                    break;
                case 4:
                    rgb.r = t; rgb.g = p; rgb.b = hsv.v;
                    break;
                default:
                    rgb.r = hsv.v; rgb.g = p; rgb.b = q;
                    break;
            }

            return rgb;
        }
    }
}
