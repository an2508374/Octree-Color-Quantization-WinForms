using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Octree_Color_Quantization_WinForms
{
    public partial class Form1 : Form
    {
        private Bitmap? importedImage;
        private Bitmap? processedImage;
        private Octree ocTree;

        private TableLayoutPanel verticalPanel;
        private TableLayoutPanel horizontalPanel;
        private PictureBox pictureBoxImported;
        private PictureBox pictureBoxProcessed;

        public Form1()
        {
            InitializeComponent();

            pictureBoxImported = new PictureBox();
            pictureBoxImported.Visible = false;
            groupBoxImported.Controls.Add(pictureBoxImported);

            pictureBoxProcessed = new PictureBox();
            pictureBoxProcessed.Visible = false;
            groupBoxProcessed.Controls.Add(pictureBoxProcessed);

            verticalPanel = new TableLayoutPanel();
            verticalPanel.ColumnCount = 1;
            verticalPanel.RowCount = 3;
            verticalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 38F));
            verticalPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 62F));

            horizontalPanel = new TableLayoutPanel();
            horizontalPanel.ColumnCount = 3;
            horizontalPanel.RowCount = 1;
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            horizontalPanel.RowStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            panel.Controls.Add(verticalPanel);
            verticalPanel.Controls.Add(menuStrip, 0, 0);
            verticalPanel.Controls.Add(horizontalPanel, 0, 1);

            verticalPanel.Dock = DockStyle.Fill;
            horizontalPanel.Dock = DockStyle.Fill;

            horizontalPanel.Controls.Add(groupBoxImported, 0, 0);
            horizontalPanel.Controls.Add(groupBoxProcessed, 1, 0);
            horizontalPanel.Controls.Add(groupBoxOptions, 2, 0);

            ocTree = new Octree();
            ocTree.InsertColor(Color.FromArgb(0b00101011, 0b11100011, 0b00011101));
        }

        private (int, int, int, int) GetPictureBoxCoords(GroupBox groupBox, int inWidth, int inHeight)
        {
            int px, py, outWidth, outHeight;

            outHeight = groupBox.Height - Const.PictureBoxLowerMargin - Const.PictureBoxUpperMargin;
            outWidth = (int)((double)outHeight / inHeight * inWidth);

            px = (groupBox.Width - outWidth) / 2;
            py = Const.PictureBoxUpperMargin;

            return (px, py, outWidth, outHeight);
        }

        private void SetPictureInPanel(GroupBox groupBox, PictureBox pictureBox, Bitmap bitmap)
        {
            (int px, int py, int outWidth, int outHeight) = GetPictureBoxCoords(groupBox, bitmap.Width, bitmap.Height);

            pictureBox.Visible = false;
            pictureBox.Location = new Point(px, py);
            pictureBox.Size = new Size(outWidth, outHeight);
            pictureBox.Image = new Bitmap(bitmap, outWidth, outHeight);
            pictureBox.Visible = true;
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

            // below elements are there only for tests
            ocTree.UpdateTree(256); // hardcoded magic number
            ocTree.UpdatePalette();
            UpdateProcessedImage();

            if (processedImage != null)
            {
                SetPictureInPanel(groupBoxProcessed, pictureBoxProcessed, processedImage);
            }
        }

        private void UpdateProcessedImage()
        {
            if (importedImage == null)
            {
                return;
            }

            processedImage = new Bitmap(importedImage.Width, importedImage.Height);

            for (int i = 0; i < importedImage.Width; ++i)
            {
                for (int j = 0; j < importedImage.Height; ++j)
                {
                    int paletteIndex = ocTree.GetPaletteIndex(importedImage.GetPixel(i, j));
                    processedImage.SetPixel(i, j, ocTree.Palette[paletteIndex]);
                }
            }
        }

        private void ImportPictureMenuItem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog importFileDialog = new OpenFileDialog();

            importFileDialog.Title = "Import Image";
            importFileDialog.Filter = "bmp files (*.bmp)|*.bmp";

            if (importFileDialog.ShowDialog() == DialogResult.OK)
            {
                importedImage = new Bitmap(importFileDialog.FileName);
                SetPictureInPanel(groupBoxImported, pictureBoxImported, importedImage);
                
                InsertColorsToOctree();
            }
        }

        private void ExportPictureMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
