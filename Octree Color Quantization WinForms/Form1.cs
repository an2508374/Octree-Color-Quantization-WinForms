using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
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
        private int minColorCount;
        private int currentColorCount;
        private int stepCount;

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
            minColorCount = Const.minColorCountDefault;
            stepCount = Const.stepCountDefault;
            textBoxMinColorCount.Text = minColorCount.ToString();
            textBoxStepCount.Text = stepCount.ToString();
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

        private void ImportPictureMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog importFileDialog = new OpenFileDialog();

            importFileDialog.Title = "Import Image";
            importFileDialog.Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";

            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                importedImage?.Dispose();
                importedImage = new Bitmap(importFileDialog.FileName);
                PictureSetter.SetPictureInPanel(groupBoxImported.Width, groupBoxImported.Height,
                    pictureBoxImported, importedImage);

                pictureBoxProcessed.Image?.Dispose();
                pictureBoxProcessed.Image = null;

                stepCount = Const.stepCountDefault;
                textBoxStepCount.Text = stepCount.ToString();
                groupBoxProcessed.Text = "Processed Image";

                InsertColorsToOctree();
                groupBoxImported.Text = $"Imported Image ({ocTree.LeafCount} colors)";
                buttonNextStep.Enabled = true;

                ocTree.UpdatePalette();
                UpdateVisualizationImage();
                groupBoxVisualization.Text = "Octree Visualization (For Imported Image)";
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
            UpdateCurrentColorCount();
            ocTree.UpdateTree(currentColorCount);
            //ocTree.UpdatePalette();
            UpdateProcessedImage();

            --stepCount;
            textBoxStepCount.Text = stepCount.ToString();
            groupBoxProcessed.Text = $"Processed Image ({ocTree.LeafCount} colors)";
            
            if (stepCount <= 0)
            {
                buttonNextStep.Enabled = false;
            }

            UpdateVisualizationImage();
            groupBoxVisualization.Text = "Octree Visualization (For Processed Image)";
        }

        private void UpdateCurrentColorCount()
        {
            if (stepCount <= 0 || ocTree.LeafCount <= 0)
            {
                return;
            }

            currentColorCount = ocTree.LeafCount;
            currentColorCount -= (ocTree.LeafCount - minColorCount) / stepCount;
        }

        private int UpdateDataFromTextBox(TextBox textBox, int updatedValue)
        {
            int result = updatedValue;

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("This field must be an integer number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }

            if (textBox.Text.Length > 0 && !Int32.TryParse(textBox.Text, out result))
            {
                MessageBox.Show("This field must be an integer number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private void TextBoxMinColorCount_TextChanged(object sender, EventArgs e)
        {
            minColorCount = UpdateDataFromTextBox(textBoxMinColorCount, minColorCount);
            UpdateCurrentColorCount();
        }

        private void TextBoxStepCount_TextChanged(object sender, EventArgs e)
        {
            stepCount = UpdateDataFromTextBox(textBoxStepCount, stepCount);
            UpdateCurrentColorCount();
        }
    }
}
