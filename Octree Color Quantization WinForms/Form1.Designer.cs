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
            panelLeftPic = new Panel();
            panelRightPic = new Panel();
            SuspendLayout();
            // 
            // panelLeftPic
            // 
            panelLeftPic.Location = new Point(0, 0);
            panelLeftPic.Name = "panelLeftPic";
            panelLeftPic.Size = new Size(708, 764);
            panelLeftPic.TabIndex = 0;
            // 
            // panelRightPic
            // 
            panelRightPic.Location = new Point(715, 0);
            panelRightPic.Name = "panelRightPic";
            panelRightPic.Size = new Size(708, 764);
            panelRightPic.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 763);
            Controls.Add(panelRightPic);
            Controls.Add(panelLeftPic);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Octree Color Quantization";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLeftPic;
        private Panel panelRightPic;
    }
}
