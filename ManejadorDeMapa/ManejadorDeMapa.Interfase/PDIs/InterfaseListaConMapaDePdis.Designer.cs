namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class InterfaseListaConMapaDePdis
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseListaConMapaDePdis));
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseListaDePdis();
      this.miMenuEditorDePdis = new GpsYv.ManejadorDeMapa.Interfase.Pdis.MenuEditorDePdis();
      this.miMapaDePdisSeleccionados = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseMapaDePdisSeleccionados();
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
      this.miDivision.Panel2.Controls.Add(this.miMapaDePdisSeleccionados);
      // 
      // miLista
      // 
      this.miLista.ContextMenuStrip = this.miMenuEditorDePdis;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.HideSelection = false;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miMenuEditorDePdis
      // 
      this.miMenuEditorDePdis.Lista = this.miLista;
      this.miMenuEditorDePdis.ManejadorDePdis = null;
      this.miMenuEditorDePdis.Name = "miMenuDeContexto";
      resources.ApplyResources(this.miMenuEditorDePdis, "miMenuEditorDePdis");
      // 
      // miMapaDePdisSeleccionados
      // 
      resources.ApplyResources(this.miMapaDePdisSeleccionados, "miMapaDePdisSeleccionados");
      this.miMapaDePdisSeleccionados.EscuchadorDeEstatus = null;
      this.miMapaDePdisSeleccionados.Lista = this.miLista;
      this.miMapaDePdisSeleccionados.ManejadorDeMapa = null;
      this.miMapaDePdisSeleccionados.MuestraCiudades = false;
      this.miMapaDePdisSeleccionados.MuestraEstados = false;
      this.miMapaDePdisSeleccionados.MuestraPdis = false;
      this.miMapaDePdisSeleccionados.MuestraPolígonos = false;
      this.miMapaDePdisSeleccionados.MuestraPolilíneas = false;
      this.miMapaDePdisSeleccionados.MuestraTodoElMapa = true;
      this.miMapaDePdisSeleccionados.MuestraTodosLosElementos = true;
      this.miMapaDePdisSeleccionados.MuestraVías = false;
      this.miMapaDePdisSeleccionados.Name = "miMapaDePdisSeleccionados";
      this.miMapaDePdisSeleccionados.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDePdisSeleccionados.RectánguloVisibleEnCoordenadas")));
      // 
      // InterfaseListaConMapaDePdis
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miDivision);
      this.Name = "InterfaseListaConMapaDePdis";
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaDePdis miLista;
    private InterfaseMapaDePdisSeleccionados miMapaDePdisSeleccionados;
    private MenuEditorDePdis miMenuEditorDePdis;
    private System.Windows.Forms.SplitContainer miDivision;
  }
}
