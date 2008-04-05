namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseDeVíasConIncongruencias
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
      System.Windows.Forms.ColumnHeader columnaDetalle;
      this.miInterfaseListaConMapaDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaConMapaDeVías();
      columnaDetalle = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // columnaDetalle
      // 
      columnaDetalle.Text = "Detalle";
      columnaDetalle.Width = 400;
      // 
      // miInterfaseListaConMapaDeVías
      // 
      this.miInterfaseListaConMapaDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = null;
      // 
      // 
      // 
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaDetalle});
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.ContextMenuStrip = this.miInterfaseListaConMapaDeVías.MenuEditorDeVías;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.FullRowSelect = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.GridLines = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Name = "miLista";
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Size = new System.Drawing.Size(556, 292);
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.TabIndex = 2;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.UseCompatibleStateImageBehavior = false;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.View = System.Windows.Forms.View.Details;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.VirtualMode = false;
      this.miInterfaseListaConMapaDeVías.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.Name = "miInterfaseListaConMapaDeVías";
      this.miInterfaseListaConMapaDeVías.Size = new System.Drawing.Size(556, 492);
      this.miInterfaseListaConMapaDeVías.TabIndex = 1;
      // 
      // InterfaseDeVíasConIncongruencias
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miInterfaseListaConMapaDeVías);
      this.Name = "InterfaseDeVíasConIncongruencias";
      this.Size = new System.Drawing.Size(556, 492);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDeVías miInterfaseListaConMapaDeVías;

  }
}
