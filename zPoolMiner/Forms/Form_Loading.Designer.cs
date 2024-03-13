namespace zPoolMiner
{
    partial class Form_Loading
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
            this.LoadText = new System.Windows.Forms.Label();
            this.label_LoadingText = new System.Windows.Forms.Label();
            this.progressBar1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // LoadText
            // 
            this.LoadText.AutoSize = true;
            this.LoadText.BackColor = System.Drawing.Color.Transparent;
            this.LoadText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LoadText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.LoadText.Location = new System.Drawing.Point(9, 30);
            this.LoadText.Name = "LoadText";
            this.LoadText.Size = new System.Drawing.Size(283, 13);
            this.LoadText.TabIndex = 2;
            this.LoadText.Text = "                                                                                 " +
    "           ";
            this.LoadText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_LoadingText
            // 
            this.label_LoadingText.AutoSize = true;
            this.label_LoadingText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_LoadingText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label_LoadingText.Location = new System.Drawing.Point(148, 9);
            this.label_LoadingText.Name = "label_LoadingText";
            this.label_LoadingText.Size = new System.Drawing.Size(169, 13);
            this.label_LoadingText.TabIndex = 0;
            this.label_LoadingText.Text = "Getting Setup... Please Wait";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.progressBar1.BackGradientEndColor = System.Drawing.Color.Silver;
            this.progressBar1.BackGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.progressBar1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.progressBar1.BackgroundStyle = Syncfusion.Windows.Forms.Tools.ProgressBarBackgroundStyles.Gradient;
            this.progressBar1.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar1.BackSegments = false;
            this.progressBar1.BackTubeEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.progressBar1.BackTubeStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.progressBar1.Border3DStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.progressBar1.BorderColor = System.Drawing.Color.Transparent;
            this.progressBar1.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.progressBar1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar1.CustomText = null;
            this.progressBar1.CustomWaitingRender = false;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.FontColor = System.Drawing.Color.White;
            this.progressBar1.ForegroundImage = null;
            this.progressBar1.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.progressBar1.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.progressBar1.Location = new System.Drawing.Point(0, 46);
            this.progressBar1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Metro;
            this.progressBar1.SegmentWidth = 6;
            this.progressBar1.ShowProgressImage = true;
            this.progressBar1.Size = new System.Drawing.Size(445, 39);
            this.progressBar1.Step = 6;
            this.progressBar1.TabIndex = 3;
            this.progressBar1.Text = "progressBarAdv1";
            this.progressBar1.TextStyle = Syncfusion.Windows.Forms.Tools.ProgressBarTextStyles.Custom;
            this.progressBar1.ThemesEnabled = false;
            this.progressBar1.TubeEndColor = System.Drawing.Color.DimGray;
            this.progressBar1.TubeStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.progressBar1.Value = 75;
            this.progressBar1.WaitingGradientEnabled = true;
            this.progressBar1.WaitingGradientWidth = 400;
            // 
            // Form_Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(445, 85);
            this.ControlBox = false;
            this.Controls.Add(this.LoadText);
            this.Controls.Add(this.label_LoadingText);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Loading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form_Loading";
            this.Shown += new System.EventHandler(this.Form_Loading_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LoadText;
        private System.Windows.Forms.Label label_LoadingText;
        public Syncfusion.Windows.Forms.Tools.ProgressBarAdv progressBar1;
    }
}