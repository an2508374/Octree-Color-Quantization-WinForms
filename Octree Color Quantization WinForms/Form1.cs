using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Octree_Color_Quantization_WinForms
{
    public partial class Form1 : Form
    {
        private Bitmap? importedImage;
        private Octree octreeStructure;

        private PictureBox pictureBoxLeft;

        public Form1()
        {
            InitializeComponent();

            pictureBoxLeft = new PictureBox();
            splitContainer.Panel1.Controls.Add(pictureBoxLeft);

            octreeStructure = new Octree();
            octreeStructure.InsertColor(Color.FromArgb(0b00101011, 0b11100011, 0b00011101));
        }

        private (int, int, int, int) GetPictureBoxCoords(SplitterPanel panel, int inWidth, int inHeight)
        {
            int px, py, outWidth, outHeight;

            if (inWidth > inHeight)
            {
                outWidth = panel.Width - 2 * Const.PictureBoxXMargin;
                outHeight = (int)((double)outWidth / inWidth * inHeight);
            }
            else
            {
                outHeight = panel.Height - 2 * Const.PictureBoxYMargin;
                outWidth = (int)((double)outHeight / inHeight * inWidth);
            }

            px = Const.PictureBoxXMargin;
            py = Const.PictureBoxYMargin;

            return (px, py, outWidth, outHeight);
        }

        private void InsertColorsToOctree()
        {
            if (importedImage == null)
            {
                return;
            }

            octreeStructure = new Octree();

            for (int i = 0; i < importedImage.Width; ++i)
            {
                for (int j = 0; j < importedImage.Height; ++j)
                {
                    octreeStructure.InsertColor(importedImage.GetPixel(i, j));
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

                (int px, int py, int outWidth, int outHeight) = GetPictureBoxCoords(splitContainer.Panel1,
                    importedImage.Width, importedImage.Height);

                pictureBoxLeft.Location = new Point(px, py);
                pictureBoxLeft.Size = new Size(outWidth, outHeight);
                pictureBoxLeft.Image = new Bitmap(importedImage, outWidth, outHeight);

                InsertColorsToOctree();
            }
        }

        private void ExportPictureMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
