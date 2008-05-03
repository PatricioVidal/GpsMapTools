namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class InterfaseDePdisModificados
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDePdisModificados));
      System.Windows.Forms.ColumnHeader columnaModificaciones;
      this.miInterfaseListaConMapaDePdis = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseListaConMapaDePdis();
      columnaModificaciones = new System.Windows.Forms.ColumnHeader();
      this.miInterfaseListaConMapaDePdis.División.Panel1.SuspendLayout();
      this.miInterfaseListaConMapaDePdis.División.Panel2.SuspendLayout();
      this.miInterfaseListaConMapaDePdis.División.SuspendLayout();
      this.miInterfaseListaConMapaDePdis.SuspendLayout();
      this.SuspendLayout();
      // 
      // miInterfaseListaConMapaDePdis
      // 
      // 
      // miInterfaseListaConMapaDePdis.División
      // 
      resources.ApplyResources(this.miInterfaseListaConMapaDePdis.División, "miInterfaseListaConMapaDePdis.División");
      this.miInterfaseListaConMapaDePdis.División.Name = "División";
      resources.ApplyResources(this.miInterfaseListaConMapaDePdis, "miInterfaseListaConMapaDePdis");
      this.miInterfaseListaConMapaDePdis.EscuchadorDeEstatus = null;
      // 
      // miInterfaseListaConMapaDePdis.InterfaseListaDePdis
      // 
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaModificaciones});
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.ContextMenuStrip = this.miInterfaseListaConMapaDePdis.MenuEditorDePdis;
      resources.ApplyResources(this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis, "miInterfaseListaConMapaDePdis.InterfaseListaDePdis");
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.FullRowSelect = true;
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.GridLines = true;
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.HideSelection = false;
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.Name = "InterfaseListaDePdis";
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.UseCompatibleStateImageBehavior = false;
      this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis.View = System.Windows.Forms.View.Details;
      // 
      // miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionadas
      // 
      resources.ApplyResources(this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados, "miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionadas");
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.EscuchadorDeEstatus = null;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.Lista = this.miInterfaseListaConMapaDePdis.InterfaseListaDePdis;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraPdis = false;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraPolígonos = false;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraPolilíneas = false;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraTodoElMapa = true;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraTodosLosElementos = true;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.MuestraVías = false;
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.Name = "InterfaseMapaDePdisSeleccionadas";
      this.miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionadas.RectánguloVisibleE" +
              "nCoordenadas")));
      this.miInterfaseListaConMapaDePdis.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDePdis.Name = "miInterfaseListaConMapaDePdis";
      // 
      // columnaModificaciones
      // 
      resources.ApplyResources(columnaModificaciones, "columnaModificaciones");
      // 
      // InterfaseDePdisModificados
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miInterfaseListaConMapaDePdis);
      this.Name = "InterfaseDePdisModificados";
      this.miInterfaseListaConMapaDePdis.División.Panel1.ResumeLayout(false);
      this.miInterfaseListaConMapaDePdis.División.Panel2.ResumeLayout(false);
      this.miInterfaseListaConMapaDePdis.División.ResumeLayout(false);
      this.miInterfaseListaConMapaDePdis.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDePdis miInterfaseListaConMapaDePdis;

  }
}
