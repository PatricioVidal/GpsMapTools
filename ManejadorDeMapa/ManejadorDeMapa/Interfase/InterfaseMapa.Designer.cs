namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class InterfaseMapa
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.miGráficoDelMapa = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.miGráficoDelMapa)).BeginInit();
      this.SuspendLayout();
      // 
      // miGráficoDelMapa
      // 
      this.miGráficoDelMapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.miGráficoDelMapa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miGráficoDelMapa.Location = new System.Drawing.Point(0, 0);
      this.miGráficoDelMapa.Name = "miGráficoDelMapa";
      this.miGráficoDelMapa.Size = new System.Drawing.Size(545, 452);
      this.miGráficoDelMapa.TabIndex = 0;
      this.miGráficoDelMapa.TabStop = false;
      this.miGráficoDelMapa.DoubleClick += new System.EventHandler(this.EnDobleClick);
      this.miGráficoDelMapa.MouseLeave += new System.EventHandler(this.EnRatónSaliendo);
      this.miGráficoDelMapa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EnRatónMoviendo);
      this.miGráficoDelMapa.Paint += new System.Windows.Forms.PaintEventHandler(this.EnPintar);
      this.miGráficoDelMapa.MouseEnter += new System.EventHandler(this.EnRatónEntrando);
      // 
      // Mapa
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miGráficoDelMapa);
      this.Name = "Mapa";
      this.Size = new System.Drawing.Size(545, 452);
      ((System.ComponentModel.ISupportInitialize)(this.miGráficoDelMapa)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox miGráficoDelMapa;
  }
}
