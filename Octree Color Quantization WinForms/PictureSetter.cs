using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octree_Color_Quantization_WinForms
{
    public static class PictureSetter
    {
        public static (int, int, int, int) GetPictureBoxCoords(GroupBox groupBox, int inWidth, int inHeight)
        {
            int px, py, outWidth, outHeight;

            outHeight = groupBox.Height - Const.PictureBoxLowerMargin - Const.PictureBoxUpperMargin;
            outWidth = (int)((double)outHeight / inHeight * inWidth);

            px = (groupBox.Width - outWidth) / 2;
            py = Const.PictureBoxUpperMargin;

            return (px, py, outWidth, outHeight);
        }

        public static void SetPictureInPanel(GroupBox groupBox, PictureBox pictureBox, Bitmap bitmap)
        {
            (int px, int py, int outWidth, int outHeight) = GetPictureBoxCoords(groupBox, bitmap.Width, bitmap.Height);

            pictureBox.Visible = false;
            pictureBox.Location = new Point(px, py);
            pictureBox.Size = new Size(outWidth, outHeight);
            pictureBox.Image = new Bitmap(bitmap, outWidth, outHeight);
            pictureBox.Visible = true;
        }
    }
}
