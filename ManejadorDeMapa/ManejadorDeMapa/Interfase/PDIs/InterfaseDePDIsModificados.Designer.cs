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
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
      columnaModificaciones = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaModificaciones});
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(603, 484);
      this.miLista.TabIndex = 1;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // columnaModificaciones
      // 
      columnaModificaciones.Text = "Modificaciones";
      columnaModificaciones.Width = 400;
      // 
      // InterfaseDePDIsModificados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miLista);
      this.Name = "InterfaseDePDIsModificados";
      this.Size = new System.Drawing.Size(603, 484);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
  }
}
