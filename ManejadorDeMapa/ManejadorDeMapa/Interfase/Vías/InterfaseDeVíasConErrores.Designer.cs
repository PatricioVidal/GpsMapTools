namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseDeViasConErrores
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
      System.Windows.Forms.ColumnHeader columnaRazón;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDeViasConErrores));
      this.miInterfaseListaConMapaDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaConMapaDeVías();
      columnaRazón = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // columnaRazón
      // 
      resources.ApplyResources(columnaRazón, "columnaRazón");
      // 
      // miInterfaseListaConMapaDeVías
      // 
      resources.ApplyResources(this.miInterfaseListaConMapaDeVías, "miInterfaseListaConMapaDeVías");
      this.miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = null;
      // 
      // 
      // 
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaRazón});
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.ContextMenuStrip = this.miInterfaseListaConMapaDeVías.MenuEditorDeVías;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Dock")));
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.FullRowSelect = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.GridLines = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Location = ((System.Drawing.Point)(resources.GetObject("miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Location")));
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Name = "miLista";
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Size = ((System.Drawing.Size)(resources.GetObject("miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Size")));
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.TabIndex = ((int)(resources.GetObject("miInterfaseListaConMapaDeVías.InterfaseListaDeVías.TabIndex")));
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.UseCompatibleStateImageBehavior = false;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.View = System.Windows.Forms.View.Details;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.VirtualMode = true;
      this.miInterfaseListaConMapaDeVías.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.Name = "miInterfaseListaConMapaDeVías";
      // 
      // InterfaseDeViasConErrores
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miInterfaseListaConMapaDeVías);
      this.Name = "InterfaseDeViasConErrores";
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDeVías miInterfaseListaConMapaDeVías;

  }
}
