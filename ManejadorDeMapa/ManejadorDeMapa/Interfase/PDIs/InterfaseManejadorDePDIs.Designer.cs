namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseManejadorDePDIs
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
      System.Windows.Forms.ColumnHeader columnaLatitud;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDePDIs));
      System.Windows.Forms.ColumnHeader columnaLongitud;
      this.miControladorDePestañasDePDIs = new System.Windows.Forms.TabControl();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
      this.miPáginaModificados = new System.Windows.Forms.TabPage();
      this.miInterfasePDIsModificados = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseDePDIsModificados();
      this.miPáginaPosiblesDuplicados = new System.Windows.Forms.TabPage();
      this.miInterfasePDIsDuplicados = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseDePDIsDuplicados();
      this.miPáginaEliminados = new System.Windows.Forms.TabPage();
      this.miInterfasePDIsEliminados = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseDePDIsEliminados();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfasePDIsErrores = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseDeErroresEnPDIs();
      columnaLatitud = new System.Windows.Forms.ColumnHeader();
      columnaLongitud = new System.Windows.Forms.ColumnHeader();
      this.miControladorDePestañasDePDIs.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miPáginaModificados.SuspendLayout();
      this.miPáginaPosiblesDuplicados.SuspendLayout();
      this.miPáginaEliminados.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnaLatitud
      // 
      resources.ApplyResources(columnaLatitud, "columnaLatitud");
      // 
      // columnaLongitud
      // 
      resources.ApplyResources(columnaLongitud, "columnaLongitud");
      // 
      // miControladorDePestañasDePDIs
      // 
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaModificados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaPosiblesDuplicados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaEliminados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaErrores);
      resources.ApplyResources(this.miControladorDePestañasDePDIs, "miControladorDePestañasDePDIs");
      this.miControladorDePestañasDePDIs.Name = "miControladorDePestañasDePDIs";
      this.miControladorDePestañasDePDIs.SelectedIndex = 0;
      // 
      // miPáginaMapa
      // 
      this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
      resources.ApplyResources(this.miPáginaMapa, "miPáginaMapa");
      this.miPáginaMapa.Name = "miPáginaMapa";
      this.miPáginaMapa.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeMapa
      // 
      resources.ApplyResources(this.miInterfaseDeMapa, "miInterfaseDeMapa");
      this.miInterfaseDeMapa.EscuchadorDeEstatus = null;
      this.miInterfaseDeMapa.ManejadorDeMapa = null;
      this.miInterfaseDeMapa.MuestraPDIs = true;
      this.miInterfaseDeMapa.MuestraPolígonos = false;
      this.miInterfaseDeMapa.MuestraPolilíneas = false;
      this.miInterfaseDeMapa.MuestraTodoElMapa = true;
      this.miInterfaseDeMapa.MuestraTodosLosElementos = false;
      this.miInterfaseDeMapa.MuestraVías = false;
      this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
      this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
      // 
      // miPáginaDeTodos
      // 
      this.miPáginaDeTodos.Controls.Add(this.miLista);
      resources.ApplyResources(this.miPáginaDeTodos, "miPáginaDeTodos");
      this.miPáginaDeTodos.Name = "miPáginaDeTodos";
      this.miPáginaDeTodos.UseVisualStyleBackColor = true;
      // 
      // miLista
      // 
      this.miLista.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaLatitud,
            columnaLongitud});
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // miPáginaModificados
      // 
      this.miPáginaModificados.Controls.Add(this.miInterfasePDIsModificados);
      resources.ApplyResources(this.miPáginaModificados, "miPáginaModificados");
      this.miPáginaModificados.Name = "miPáginaModificados";
      this.miPáginaModificados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsModificados
      // 
      resources.ApplyResources(this.miInterfasePDIsModificados, "miInterfasePDIsModificados");
      this.miInterfasePDIsModificados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsModificados.ManejadorDeMapa = null;
      this.miInterfasePDIsModificados.Name = "miInterfasePDIsModificados";
      // 
      // miPáginaPosiblesDuplicados
      // 
      this.miPáginaPosiblesDuplicados.BackColor = System.Drawing.Color.Transparent;
      this.miPáginaPosiblesDuplicados.Controls.Add(this.miInterfasePDIsDuplicados);
      resources.ApplyResources(this.miPáginaPosiblesDuplicados, "miPáginaPosiblesDuplicados");
      this.miPáginaPosiblesDuplicados.Name = "miPáginaPosiblesDuplicados";
      this.miPáginaPosiblesDuplicados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsDuplicados
      // 
      resources.ApplyResources(this.miInterfasePDIsDuplicados, "miInterfasePDIsDuplicados");
      this.miInterfasePDIsDuplicados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsDuplicados.ManejadorDeMapa = null;
      this.miInterfasePDIsDuplicados.Name = "miInterfasePDIsDuplicados";
      // 
      // miPáginaEliminados
      // 
      this.miPáginaEliminados.Controls.Add(this.miInterfasePDIsEliminados);
      resources.ApplyResources(this.miPáginaEliminados, "miPáginaEliminados");
      this.miPáginaEliminados.Name = "miPáginaEliminados";
      this.miPáginaEliminados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsEliminados
      // 
      resources.ApplyResources(this.miInterfasePDIsEliminados, "miInterfasePDIsEliminados");
      this.miInterfasePDIsEliminados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsEliminados.ManejadorDeMapa = null;
      this.miInterfasePDIsEliminados.Name = "miInterfasePDIsEliminados";
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfasePDIsErrores);
      resources.ApplyResources(this.miPáginaErrores, "miPáginaErrores");
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsErrores
      // 
      resources.ApplyResources(this.miInterfasePDIsErrores, "miInterfasePDIsErrores");
      this.miInterfasePDIsErrores.EscuchadorDeEstatus = null;
      this.miInterfasePDIsErrores.ManejadorDeMapa = null;
      this.miInterfasePDIsErrores.Name = "miInterfasePDIsErrores";
      // 
      // InterfaseManejadorDePDIs
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañasDePDIs);
      this.Name = "InterfaseManejadorDePDIs";
      this.miControladorDePestañasDePDIs.ResumeLayout(false);
      this.miPáginaMapa.ResumeLayout(false);
      this.miPáginaDeTodos.ResumeLayout(false);
      this.miPáginaModificados.ResumeLayout(false);
      this.miPáginaPosiblesDuplicados.ResumeLayout(false);
      this.miPáginaEliminados.ResumeLayout(false);
      this.miPáginaErrores.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl miControladorDePestañasDePDIs;
    private System.Windows.Forms.TabPage miPáginaDeTodos;
    private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
    private System.Windows.Forms.TabPage miPáginaModificados;
    private InterfaseDePDIsModificados miInterfasePDIsModificados;
    private System.Windows.Forms.TabPage miPáginaPosiblesDuplicados;
    private InterfaseDePDIsDuplicados miInterfasePDIsDuplicados;
    private System.Windows.Forms.TabPage miPáginaEliminados;
    private InterfaseDePDIsEliminados miInterfasePDIsEliminados;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaseMapa miInterfaseDeMapa;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private InterfaseDeErroresEnPDIs miInterfasePDIsErrores;
  }
}
