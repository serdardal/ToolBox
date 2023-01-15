﻿namespace ToolBox
{
    partial class MainForm
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
            this.toolComboBox = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.runButton = new System.Windows.Forms.Button();
            this.toolLabel = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // toolComboBox
            // 
            this.toolComboBox.FormattingEnabled = true;
            this.toolComboBox.Location = new System.Drawing.Point(27, 32);
            this.toolComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolComboBox.Name = "toolComboBox";
            this.toolComboBox.Size = new System.Drawing.Size(306, 28);
            this.toolComboBox.TabIndex = 0;
            this.toolComboBox.SelectedIndexChanged += new System.EventHandler(this.toolComboBox_SelectedIndexChanged);
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(434, 32);
            this.runButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(86, 31);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // toolLabel
            // 
            this.toolLabel.AutoSize = true;
            this.toolLabel.ForeColor = System.Drawing.Color.Maroon;
            this.toolLabel.Location = new System.Drawing.Point(24, 12);
            this.toolLabel.Name = "toolLabel";
            this.toolLabel.Size = new System.Drawing.Size(128, 20);
            this.toolLabel.TabIndex = 2;
            this.toolLabel.Text = "Please select tool!";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 67);
            this.progressBar.MarqueeAnimationSpeed = 300;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(493, 15);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 344);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.toolLabel);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.toolComboBox);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "Super Tool Box";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox toolComboBox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label toolLabel;
        private ProgressBar progressBar;
    }
}