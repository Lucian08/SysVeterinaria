namespace SysVeterinaria
{
    partial class Home
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mascotaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarAtencionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.atencionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clienteToolStripMenuItem,
            this.mascotaToolStripMenuItem,
            this.generarAtencionesToolStripMenuItem,
            this.atencionesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clienteToolStripMenuItem
            // 
            this.clienteToolStripMenuItem.Name = "clienteToolStripMenuItem";
            this.clienteToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.clienteToolStripMenuItem.Text = "Cliente";
            this.clienteToolStripMenuItem.Click += new System.EventHandler(this.clienteToolStripMenuItem_Click);
            // 
            // mascotaToolStripMenuItem
            // 
            this.mascotaToolStripMenuItem.Name = "mascotaToolStripMenuItem";
            this.mascotaToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.mascotaToolStripMenuItem.Text = "Mascota";
            this.mascotaToolStripMenuItem.Click += new System.EventHandler(this.mascotaToolStripMenuItem_Click);
            // 
            // generarAtencionesToolStripMenuItem
            // 
            this.generarAtencionesToolStripMenuItem.Name = "generarAtencionesToolStripMenuItem";
            this.generarAtencionesToolStripMenuItem.Size = new System.Drawing.Size(122, 20);
            this.generarAtencionesToolStripMenuItem.Text = "Generar Atenciones";
            this.generarAtencionesToolStripMenuItem.Click += new System.EventHandler(this.generarAtencionesToolStripMenuItem_Click);
            // 
            // atencionesToolStripMenuItem
            // 
            this.atencionesToolStripMenuItem.Name = "atencionesToolStripMenuItem";
            this.atencionesToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.atencionesToolStripMenuItem.Text = "Atenciones";
            this.atencionesToolStripMenuItem.Click += new System.EventHandler(this.atencionesToolStripMenuItem_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 470);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Home";
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mascotaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarAtencionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem atencionesToolStripMenuItem;
    }
}