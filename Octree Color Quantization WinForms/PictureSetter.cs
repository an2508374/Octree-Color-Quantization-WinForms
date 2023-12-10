using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public static class PictureSetter
    {
        public static (int, int, int, int) GetPictureBoxCoords(int panelWidth, int panelHeight, int inWidth, int inHeight)
        {
            int px, py, outWidth, outHeight;

            outHeight = panelHeight - Const.pictureBoxLowerMargin - Const.pictureBoxUpperMargin;
            outWidth = (int)((double)outHeight / inHeight * inWidth);

            px = (panelWidth - outWidth) / 2;
            py = Const.pictureBoxUpperMargin;

            return (px, py, outWidth, outHeight);
        }

        public static void SetPictureInPanel(int panelWidth, int panelHeight, PictureBox pictureBox, Bitmap bitmap)
        {
            (int px, int py, int outWidth, int outHeight) = GetPictureBoxCoords(panelWidth, panelHeight, bitmap.Width, bitmap.Height);

            pictureBox.Visible = false;
            pictureBox.Location = new Point(px, py);
            pictureBox.Size = new Size(outWidth, outHeight);
            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(bitmap, outWidth, outHeight);
            pictureBox.Visible = true;
        }
    }
}
