namespace Octree_Color_Quantization_WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip = new MenuStrip();
            importPictureMenuItem = new ToolStripMenuItem();
            exportPictureMenuItem = new ToolStripMenuItem();
            splitContainer = new SplitContainer();
            menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { importPictureMenuItem, exportPictureMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(1422, 28);
            menuStrip.TabIndex = 1;
            menuStrip.Text = "menuStrip1";
            // 
            // importPictureMenuItem
            // 
            importPictureMenuItem.Name = "importPictureMenuItem";
            importPictureMenuItem.Size = new Size(117, 24);
            importPictureMenuItem.Text = "Import Picture";
            importPictureMenuItem.Click += ImportPictureMenuItem_Click;
            // 
            // exportPictureMenuItem
            // 
            exportPictureMenuItem.Name = "exportPictureMenuItem";
            exportPictureMenuItem.Size = new Size(115, 24);
            exportPictureMenuItem.Text = "Export Picture";
            exportPictureMenuItem.Click += ExportPictureMenuItem_Click;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Name = "splitContainer";
            splitContainer.Size = new Size(1422, 763);
            splitContainer.SplitterDistance = 710;
            splitContainer.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 763);
            Controls.Add(menuStrip);
            Controls.Add(splitContainer);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Octree Color Quantization";
            WindowState = FormWindowState.Maximized;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem importPictureMenuItem;
        private ToolStripMenuItem exportPictureMenuItem;
        private SplitContainer splitContainer;
    }
}
