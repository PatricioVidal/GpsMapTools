﻿namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseDePDIsModificados
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
      System.Windows.Forms.ColumnHeader columnaModificaciones;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDePDIsModificados));
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseListaDePDIs();
      columnaModificaciones = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // columnaModificaciones
      // 
      resources.ApplyResources(columnaModificaciones, "columnaModificaciones");
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaModificaciones});
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // InterfaseDePDIsModificados
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miLista);
      this.Name = "InterfaseDePDIsModificados";
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseListaDePDIs miLista;
  }
}