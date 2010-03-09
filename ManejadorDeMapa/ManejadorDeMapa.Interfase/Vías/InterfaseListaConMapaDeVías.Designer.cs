namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseListaConMapaDeVías
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseListaConMapaDeVías));
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaDeVías();
      this.miMenuEditorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías();
      this.miMapaDeVíasSeleccionadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas();
      this.miDivision.Panel1.SuspendLayout();
      this.miDivision.Panel2.SuspendLayout();
      this.miDivision.SuspendLayout();
      this.SuspendLayout();
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
      this.miDivision.Panel2.Controls.Add(this.miMapaDeVíasSeleccionadas);
      // 
      // miLista
      // 
      this.miLista.ContextMenuStrip = this.miMenuEditorDeVías;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.HideSelection = false;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miMenuEditorDeVías
      // 
      this.miMenuEditorDeVías.Lista = this.miLista;
      this.miMenuEditorDeVías.ManejadorDeVías = null;
      this.miMenuEditorDeVías.Name = "miMenuDeContexto";
      resources.ApplyResources(this.miMenuEditorDeVías, "miMenuEditorDeVías");
      // 
      // miMapaDeVíasSeleccionadas
      // 
      resources.ApplyResources(this.miMapaDeVíasSeleccionadas, "miMapaDeVíasSeleccionadas");
      this.miMapaDeVíasSeleccionadas.EscuchadorDeEstatus = null;
      this.miMapaDeVíasSeleccionadas.Lista = this.miLista;
      this.miMapaDeVíasSeleccionadas.ManejadorDeMapa = null;
      this.miMapaDeVíasSeleccionadas.MuestraCiudades = false;
      this.miMapaDeVíasSeleccionadas.MuestraEstados = false;
      this.miMapaDeVíasSeleccionadas.MuestraPdis = false;
      this.miMapaDeVíasSeleccionadas.MuestraPolígonos = false;
      this.miMapaDeVíasSeleccionadas.MuestraPolilíneas = false;
      this.miMapaDeVíasSeleccionadas.MuestraTodoElMapa = true;
      this.miMapaDeVíasSeleccionadas.MuestraTodosLosElementos = true;
      this.miMapaDeVíasSeleccionadas.MuestraVías = false;
      this.miMapaDeVíasSeleccionadas.Name = "miMapaDeVíasSeleccionadas";
      this.miMapaDeVíasSeleccionadas.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíasSeleccionadas.RectánguloVisibleEnCoordenadas")));
      // 
      // InterfaseListaConMapaDeVías
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miDivision);
      this.Name = "InterfaseListaConMapaDeVías";
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaDeVías miLista;
    private InterfaseMapaDeVíasSeleccionadas miMapaDeVíasSeleccionadas;
    private MenuEditorDeVías miMenuEditorDeVías;
    private System.Windows.Forms.SplitContainer miDivision;
  }
}
