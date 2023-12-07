﻿namespace Octree_Color_Quantization_WinForms
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
            groupBoxImported = new GroupBox();
            groupBoxProcessed = new GroupBox();
            groupBoxOptions = new GroupBox();
            panel = new Panel();
            menuStrip.SuspendLayout();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip
            // 
            menuStrip.Dock = DockStyle.None;
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { importPictureMenuItem, exportPictureMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(240, 28);
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
            // groupBoxImported
            // 
            groupBoxImported.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxImported.Location = new Point(0, 31);
            groupBoxImported.Name = "groupBoxImported";
            groupBoxImported.Size = new Size(470, 277);
            groupBoxImported.TabIndex = 2;
            groupBoxImported.TabStop = false;
            groupBoxImported.Text = "Imported Image";
            // 
            // groupBoxProcessed
            // 
            groupBoxProcessed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxProcessed.Location = new Point(476, 31);
            groupBoxProcessed.Name = "groupBoxProcessed";
            groupBoxProcessed.Size = new Size(470, 277);
            groupBoxProcessed.TabIndex = 3;
            groupBoxProcessed.TabStop = false;
            groupBoxProcessed.Text = "Processed Image";
            // 
            // groupBoxOptions
            // 
            groupBoxOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxOptions.Location = new Point(952, 31);
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Size = new Size(470, 277);
            groupBoxOptions.TabIndex = 4;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Quantization Options";
            // 
            // panel
            // 
            panel.Controls.Add(menuStrip);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Size = new Size(1422, 763);
            panel.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 763);
            Controls.Add(groupBoxImported);
            Controls.Add(groupBoxProcessed);
            Controls.Add(groupBoxOptions);
            Controls.Add(panel);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Octree Color Quantization";
            WindowState = FormWindowState.Maximized;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private MenuStrip menuStrip;
        private ToolStripMenuItem importPictureMenuItem;
        private ToolStripMenuItem exportPictureMenuItem;
        private GroupBox groupBoxImported;
        private GroupBox groupBoxProcessed;
        private GroupBox groupBoxOptions;
        private Panel panel;
    }
}
