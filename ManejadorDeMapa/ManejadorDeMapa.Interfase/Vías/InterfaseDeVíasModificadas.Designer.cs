namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseDeVíasModificadas
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDeVíasModificadas));
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaDeVías();
      this.miMenuEditorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas();
      columnaModificaciones = new System.Windows.Forms.ColumnHeader();
      this.miDivision.Panel1.SuspendLayout();
      this.miDivision.Panel2.SuspendLayout();
      this.miDivision.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnaModificaciones
      // 
      resources.ApplyResources(columnaModificaciones, "columnaModificaciones");
      // 
      // miDivision
      // 
      this.miDivision.AccessibleDescription = null;
      this.miDivision.AccessibleName = null;
      resources.ApplyResources(this.miDivision, "miDivision");
      this.miDivision.BackgroundImage = null;
      this.miDivision.Font = null;
      this.miDivision.Name = "miDivision";
      // 
      // miDivision.Panel1
      // 
      this.miDivision.Panel1.AccessibleDescription = null;
      this.miDivision.Panel1.AccessibleName = null;
      resources.ApplyResources(this.miDivision.Panel1, "miDivision.Panel1");
      this.miDivision.Panel1.BackgroundImage = null;
      this.miDivision.Panel1.Controls.Add(this.miLista);
      this.miDivision.Panel1.Font = null;
      // 
      // miDivision.Panel2
      // 
      this.miDivision.Panel2.AccessibleDescription = null;
      this.miDivision.Panel2.AccessibleName = null;
      resources.ApplyResources(this.miDivision.Panel2, "miDivision.Panel2");
      this.miDivision.Panel2.BackgroundImage = null;
      this.miDivision.Panel2.Controls.Add(this.miMapaDeVíaSeleccionada);
      this.miDivision.Panel2.Font = null;
      // 
      // miLista
      // 
      this.miLista.AccessibleDescription = null;
      this.miLista.AccessibleName = null;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.BackgroundImage = null;
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaModificaciones});
      this.miLista.ContextMenuStrip = this.miMenuEditorDeVías;
      this.miLista.Font = null;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.HideSelection = false;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miMenuEditorDeVías
      // 
      this.miMenuEditorDeVías.AccessibleDescription = null;
      this.miMenuEditorDeVías.AccessibleName = null;
      resources.ApplyResources(this.miMenuEditorDeVías, "miMenuEditorDeVías");
      this.miMenuEditorDeVías.BackgroundImage = null;
      this.miMenuEditorDeVías.Font = null;
      this.miMenuEditorDeVías.Lista = this.miLista;
      this.miMenuEditorDeVías.ManejadorDeVías = null;
      this.miMenuEditorDeVías.Name = "miMenuDeContexto";
      // 
      // miMapaDeVíaSeleccionada
      // 
      this.miMapaDeVíaSeleccionada.AccessibleDescription = null;
      this.miMapaDeVíaSeleccionada.AccessibleName = null;
      resources.ApplyResources(this.miMapaDeVíaSeleccionada, "miMapaDeVíaSeleccionada");
      this.miMapaDeVíaSeleccionada.BackgroundImage = null;
      this.miMapaDeVíaSeleccionada.EscuchadorDeEstatus = null;
      this.miMapaDeVíaSeleccionada.Font = null;
      this.miMapaDeVíaSeleccionada.Lista = this.miLista;
      this.miMapaDeVíaSeleccionada.ManejadorDeMapa = null;
      this.miMapaDeVíaSeleccionada.MuestraCiudades = true;
      this.miMapaDeVíaSeleccionada.MuestraEstados = false;
      this.miMapaDeVíaSeleccionada.MuestraPdis = false;
      this.miMapaDeVíaSeleccionada.MuestraPolígonos = false;
      this.miMapaDeVíaSeleccionada.MuestraPolilíneas = false;
      this.miMapaDeVíaSeleccionada.MuestraTodoElMapa = true;
      this.miMapaDeVíaSeleccionada.MuestraTodosLosElementos = true;
      this.miMapaDeVíaSeleccionada.MuestraVías = true;
      this.miMapaDeVíaSeleccionada.Name = "miMapaDeVíaSeleccionada";
      this.miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas")));
      // 
      // InterfaseDeVíasModificadas
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.Controls.Add(this.miDivision);
      this.Font = null;
      this.Name = "InterfaseDeVíasModificadas";
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer miDivision;
    private InterfaseListaDeVías miLista;
    private GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías miMenuEditorDeVías;
    private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas miMapaDeVíaSeleccionada;
  }
}
