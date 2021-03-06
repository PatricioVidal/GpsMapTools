﻿namespace GpsYv.ManejadorDeMapa.Interfase
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

      // Libera recursos.
      if (disposing)
      {
        miLápizDeFondoParaEscala.Dispose();
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseMapa));
      this.miGráficoDelMapa = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.miGráficoDelMapa)).BeginInit();
      this.SuspendLayout();
      // 
      // miGráficoDelMapa
      // 
      this.miGráficoDelMapa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      resources.ApplyResources(this.miGráficoDelMapa, "miGráficoDelMapa");
      this.miGráficoDelMapa.Name = "miGráficoDelMapa";
      this.miGráficoDelMapa.TabStop = false;
      this.miGráficoDelMapa.DoubleClick += new System.EventHandler(this.EnDobleClick);
      this.miGráficoDelMapa.MouseLeave += new System.EventHandler(this.EnRatónSaliendo);
      this.miGráficoDelMapa.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EnRatónMoviendo);
      this.miGráficoDelMapa.Paint += new System.Windows.Forms.PaintEventHandler(this.EnPintar);
      this.miGráficoDelMapa.MouseEnter += new System.EventHandler(this.EnRatónEntrando);
      // 
      // InterfaseMapa
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miGráficoDelMapa);
      this.Name = "InterfaseMapa";
      ((System.ComponentModel.ISupportInitialize)(this.miGráficoDelMapa)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox miGráficoDelMapa;
  }
}
