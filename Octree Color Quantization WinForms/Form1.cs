namespace Octree_Color_Quantization_WinForms
{
    public partial class Form1 : Form
    {
        public Octree octree;

        public Form1()
        {
            InitializeComponent();

            octree = new Octree();
            octree.InsertColor(Color.FromArgb(0b00101011, 0b11100011, 0b00011101));
        }
    }
}
