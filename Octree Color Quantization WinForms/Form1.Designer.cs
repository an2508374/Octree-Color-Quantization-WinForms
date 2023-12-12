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
            groupBoxImported = new GroupBox();
            groupBoxVisualization = new GroupBox();
            groupBoxProcessed = new GroupBox();
            groupBoxOptions = new GroupBox();
            radioButtonExponentially = new RadioButton();
            radioButtonLinearly = new RadioButton();
            label2 = new Label();
            textBoxOption = new TextBox();
            labelOption = new Label();
            textBoxMinColorCount = new TextBox();
            label1 = new Label();
            buttonNextStep = new Button();
            panel = new Panel();
            menuStrip.SuspendLayout();
            groupBoxImported.SuspendLayout();
            groupBoxOptions.SuspendLayout();
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
            groupBoxImported.Controls.Add(groupBoxVisualization);
            groupBoxImported.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxImported.Location = new Point(0, 31);
            groupBoxImported.Name = "groupBoxImported";
            groupBoxImported.Size = new Size(470, 277);
            groupBoxImported.TabIndex = 2;
            groupBoxImported.TabStop = false;
            groupBoxImported.Text = "Imported Image";
            // 
            // groupBoxVisualization
            // 
            groupBoxVisualization.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxVisualization.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxVisualization.Location = new Point(3, 283);
            groupBoxVisualization.Name = "groupBoxVisualization";
            groupBoxVisualization.Size = new Size(1416, 449);
            groupBoxVisualization.TabIndex = 6;
            groupBoxVisualization.TabStop = false;
            groupBoxVisualization.Text = "Octree Visualization";
            // 
            // groupBoxProcessed
            // 
            groupBoxProcessed.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBoxProcessed.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
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
            groupBoxOptions.Controls.Add(radioButtonExponentially);
            groupBoxOptions.Controls.Add(radioButtonLinearly);
            groupBoxOptions.Controls.Add(label2);
            groupBoxOptions.Controls.Add(textBoxOption);
            groupBoxOptions.Controls.Add(labelOption);
            groupBoxOptions.Controls.Add(textBoxMinColorCount);
            groupBoxOptions.Controls.Add(label1);
            groupBoxOptions.Controls.Add(buttonNextStep);
            groupBoxOptions.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            groupBoxOptions.Location = new Point(952, 31);
            groupBoxOptions.Name = "groupBoxOptions";
            groupBoxOptions.Size = new Size(470, 277);
            groupBoxOptions.TabIndex = 4;
            groupBoxOptions.TabStop = false;
            groupBoxOptions.Text = "Quantization Options";
            // 
            // radioButtonExponentially
            // 
            radioButtonExponentially.AutoSize = true;
            radioButtonExponentially.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonExponentially.Location = new Point(48, 162);
            radioButtonExponentially.Name = "radioButtonExponentially";
            radioButtonExponentially.Size = new Size(108, 24);
            radioButtonExponentially.TabIndex = 7;
            radioButtonExponentially.TabStop = true;
            radioButtonExponentially.Text = "Exponential";
            radioButtonExponentially.UseVisualStyleBackColor = true;
            radioButtonExponentially.Click += RadioButtonExponentially_Click;
            // 
            // radioButtonLinearly
            // 
            radioButtonLinearly.AutoSize = true;
            radioButtonLinearly.Checked = true;
            radioButtonLinearly.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            radioButtonLinearly.Location = new Point(48, 132);
            radioButtonLinearly.Name = "radioButtonLinearly";
            radioButtonLinearly.Size = new Size(70, 24);
            radioButtonLinearly.TabIndex = 6;
            radioButtonLinearly.TabStop = true;
            radioButtonLinearly.Text = "Linear";
            radioButtonLinearly.UseVisualStyleBackColor = true;
            radioButtonLinearly.Click += RadioButtonLinearly_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(25, 109);
            label2.Name = "label2";
            label2.Size = new Size(192, 20);
            label2.TabIndex = 5;
            label2.Text = "Changing number of colors:";
            // 
            // textBoxOption
            // 
            textBoxOption.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxOption.Location = new Point(224, 59);
            textBoxOption.Name = "textBoxOption";
            textBoxOption.Size = new Size(125, 27);
            textBoxOption.TabIndex = 4;
            textBoxOption.TextChanged += TextBoxStepCount_TextChanged;
            // 
            // labelOption
            // 
            labelOption.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            labelOption.Location = new Point(25, 62);
            labelOption.Name = "labelOption";
            labelOption.Size = new Size(193, 20);
            labelOption.TabIndex = 3;
            labelOption.Text = "Number of remaining steps:";
            labelOption.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxMinColorCount
            // 
            textBoxMinColorCount.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxMinColorCount.Location = new Point(224, 25);
            textBoxMinColorCount.Name = "textBoxMinColorCount";
            textBoxMinColorCount.Size = new Size(125, 27);
            textBoxMinColorCount.TabIndex = 2;
            textBoxMinColorCount.TextChanged += TextBoxMinColorCount_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(58, 28);
            label1.Name = "label1";
            label1.Size = new Size(160, 20);
            label1.TabIndex = 1;
            label1.Text = "Final number of colors:";
            // 
            // buttonNextStep
            // 
            buttonNextStep.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonNextStep.Enabled = false;
            buttonNextStep.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonNextStep.Location = new Point(25, 227);
            buttonNextStep.Name = "buttonNextStep";
            buttonNextStep.Size = new Size(147, 29);
            buttonNextStep.TabIndex = 0;
            buttonNextStep.Text = "Go To Next Step";
            buttonNextStep.UseVisualStyleBackColor = true;
            buttonNextStep.Click += ButtonNextStep_Click;
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
            groupBoxImported.ResumeLayout(false);
            groupBoxOptions.ResumeLayout(false);
            groupBoxOptions.PerformLayout();
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
        private Button buttonNextStep;
        private Label label1;
        private TextBox textBoxOption;
        private Label labelOption;
        private TextBox textBoxMinColorCount;
        private GroupBox groupBoxVisualization;
        private RadioButton radioButtonExponentially;
        private RadioButton radioButtonLinearly;
        private Label label2;
    }
}
