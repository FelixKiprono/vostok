namespace Vostok
{
    partial class Process
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Process));
            this.progress = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbltask = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.processtask = new System.ComponentModel.BackgroundWorker();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.animated = true;
            this.progress.animationIterval = 5;
            this.progress.animationSpeed = 300;
            this.progress.BackColor = System.Drawing.Color.LightSeaGreen;
            this.progress.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("progress.BackgroundImage")));
            this.progress.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progress.ForeColor = System.Drawing.Color.White;
            this.progress.LabelVisible = true;
            this.progress.LineProgressThickness = 5;
            this.progress.LineThickness = 5;
            this.progress.Location = new System.Drawing.Point(204, 60);
            this.progress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.progress.MaxValue = 100;
            this.progress.Name = "progress";
            this.progress.ProgressBackColor = System.Drawing.Color.LightSeaGreen;
            this.progress.ProgressColor = System.Drawing.Color.White;
            this.progress.Size = new System.Drawing.Size(269, 269);
            this.progress.TabIndex = 6;
            this.progress.Value = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(81)))), ((int)(((byte)(81)))));
            this.panel3.Controls.Add(this.lbltask);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 390);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(706, 71);
            this.panel3.TabIndex = 13;
            // 
            // lbltask
            // 
            this.lbltask.AutoSize = true;
            this.lbltask.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltask.ForeColor = System.Drawing.Color.White;
            this.lbltask.Location = new System.Drawing.Point(183, 18);
            this.lbltask.Name = "lbltask";
            this.lbltask.Size = new System.Drawing.Size(251, 32);
            this.lbltask.TabIndex = 4;
            this.lbltask.Text = "Initializing Database ...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Current Task : ";
            // 
            // processtask
            // 
            this.processtask.DoWork += new System.ComponentModel.DoWorkEventHandler(this.processtask_DoWork);
            // 
            // Process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSeaGreen;
            this.ClientSize = new System.Drawing.Size(706, 461);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.progress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Process";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing ...";
            this.Load += new System.EventHandler(this.Process_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Bunifu.Framework.UI.BunifuCircleProgressbar progress;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbltask;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker processtask;
    }
}