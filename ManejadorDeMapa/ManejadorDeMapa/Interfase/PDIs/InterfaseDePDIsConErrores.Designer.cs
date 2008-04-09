namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseDePDIsConErrores
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDePDIsConErrores));
      System.Windows.Forms.SplitContainer division;
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseListaDePDIs();
      this.miMenúEditorDePDI = new GpsYv.ManejadorDeMapa.Interfase.PDIs.MenuEditorDePDI();
      this.miMapa = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseMapaDePDIsSeleccionados();
      columnaRazón = new System.Windows.Forms.ColumnHeader();
      division = new System.Windows.Forms.SplitContainer();
      division.Panel1.SuspendLayout();
      division.Panel2.SuspendLayout();
      division.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnaRazón
      // 
      resources.ApplyResources(columnaRazón, "columnaRazón");
      // 
      // division
      // 
      resources.ApplyResources(division, "division");
      division.Name = "division";
      // 
      // division.Panel1
      // 
      division.Panel1.Controls.Add(this.miLista);
      // 
      // division.Panel2
      // 
      division.Panel2.Controls.Add(this.miMapa);
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaRazón});
      this.miLista.ContextMenuStrip = this.miMenúEditorDePDI;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // miMenúEditorDePDI
      // 
      resources.ApplyResources(this.miMenúEditorDePDI, "miMenúEditorDePDI");
      this.miMenúEditorDePDI.Lista = this.miLista;
      this.miMenúEditorDePDI.ManejadorDePDIs = null;
      this.miMenúEditorDePDI.Name = "miMenuDeContexto";
      this.miMenúEditorDePDI.ShowImageMargin = false;
      // 
      // miMapa
      // 
      resources.ApplyResources(this.miMapa, "miMapa");
      this.miMapa.EscuchadorDeEstatus = null;
      this.miMapa.Lista = this.miLista;
      this.miMapa.ManejadorDeMapa = null;
      this.miMapa.MuestraPDIs = false;
      this.miMapa.MuestraPolígonos = false;
      this.miMapa.MuestraPolilíneas = false;
      this.miMapa.MuestraTodoElMapa = true;
      this.miMapa.MuestraTodosLosElementos = true;
      this.miMapa.MuestraVías = false;
      this.miMapa.Name = "miMapa";
      this.miMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapa.RectánguloVisibleEnCoordenadas")));
      // 
      // InterfaseDePDIsConErroress
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(division);
      this.Name = "InterfaseDePDIsConErroress";
      division.Panel1.ResumeLayout(false);
      division.Panel2.ResumeLayout(false);
      division.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseListaDePDIs miLista;
    private MenuEditorDePDI miMenúEditorDePDI;
    private InterfaseMapaDePDIsSeleccionados miMapa;
  }
}
