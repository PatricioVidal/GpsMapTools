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
      this.miMenuEditorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías();
      this.miLista = new InterfaseListaDeVías();
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionada();
      columnaRazón = new System.Windows.Forms.ColumnHeader();
      this.miDivision.Panel1.SuspendLayout();
      this.miDivision.Panel2.SuspendLayout();
      this.miDivision.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnaRazón
      // 
      resources.ApplyResources(columnaRazón, "columnaRazón");
      // 
      // miMenuEditorDeVías
      // 
      this.miMenuEditorDeVías.Lista = this.miLista;
      this.miMenuEditorDeVías.ManejadorDeVías = null;
      this.miMenuEditorDeVías.Name = "miMenuDeContexto";
      resources.ApplyResources(this.miMenuEditorDeVías, "miMenuEditorDeVías");
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaRazón});
      this.miLista.ContextMenuStrip = this.miMenuEditorDeVías;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // miDivision
      // 
      resources.ApplyResources(this.miDivision, "miDivision");
      this.miDivision.Name = "miDivision";
      // 
      // miDivision.Panel1
      // 
      this.miDivision.Panel1.Controls.Add(this.miLista);
      // 
      // miDivision.Panel2
      // 
      this.miDivision.Panel2.Controls.Add(this.miMapaDeVíaSeleccionada);
      // 
      // miMapaDeVíaSeleccionada
      // 
      resources.ApplyResources(this.miMapaDeVíaSeleccionada, "miMapaDeVíaSeleccionada");
      this.miMapaDeVíaSeleccionada.EscuchadorDeEstatus = null;
      this.miMapaDeVíaSeleccionada.Lista = this.miLista;
      this.miMapaDeVíaSeleccionada.ManejadorDeMapa = null;
      this.miMapaDeVíaSeleccionada.MuestraPDIs = false;
      this.miMapaDeVíaSeleccionada.MuestraPolígonos = false;
      this.miMapaDeVíaSeleccionada.MuestraPolilíneas = false;
      this.miMapaDeVíaSeleccionada.MuestraTodoElMapa = true;
      this.miMapaDeVíaSeleccionada.MuestraTodosLosElementos = true;
      this.miMapaDeVíaSeleccionada.MuestraVías = false;
      this.miMapaDeVíaSeleccionada.Name = "miMapaDeVíaSeleccionada";
      this.miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas")));
      // 
      // InterfaseDeErroresEnVías
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miDivision);
      this.Name = "InterfaseDeErroresEnVías";
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer miDivision;
    private InterfaseMapaDeVíasSeleccionada miMapaDeVíaSeleccionada;
    private InterfaseListaDeVías miLista;
    private MenuEditorDeVías miMenuEditorDeVías;

  }
}
