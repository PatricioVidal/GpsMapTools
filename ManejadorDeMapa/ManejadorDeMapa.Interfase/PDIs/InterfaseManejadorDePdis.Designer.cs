namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class InterfaseManejadorDePdis
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDePdis));
      this.miControladorDePestañas = new GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseListaDePdis();
      this.miPáginaModificados = new System.Windows.Forms.TabPage();
      this.miInterfasePdisModificados = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseDePdisModificados();
      this.miPáginaPosiblesDuplicados = new System.Windows.Forms.TabPage();
      this.miInterfasePdisDuplicados = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseDePdisDuplicados();
      this.miPáginaEliminados = new System.Windows.Forms.TabPage();
      this.miInterfasePdisEliminados = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseDePdisEliminados();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfasePdisErrores = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseDePdisConErrores();
      this.miControladorDePestañas.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miPáginaModificados.SuspendLayout();
      this.miPáginaPosiblesDuplicados.SuspendLayout();
      this.miPáginaEliminados.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // miControladorDePestañas
      // 
      this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañas.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañas.Controls.Add(this.miPáginaModificados);
      this.miControladorDePestañas.Controls.Add(this.miPáginaEliminados);
      this.miControladorDePestañas.Controls.Add(this.miPáginaPosiblesDuplicados);
      this.miControladorDePestañas.Controls.Add(this.miPáginaErrores);
      resources.ApplyResources(this.miControladorDePestañas, "miControladorDePestañas");
      this.miControladorDePestañas.Name = "miControladorDePestañas";
      this.miControladorDePestañas.SelectedIndex = 0;
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
      this.miInterfaseDeMapa.MuestraPdis = true;
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
      this.miPáginaModificados.Controls.Add(this.miInterfasePdisModificados);
      resources.ApplyResources(this.miPáginaModificados, "miPáginaModificados");
      this.miPáginaModificados.Name = "miPáginaModificados";
      this.miPáginaModificados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePdisModificados
      // 
      resources.ApplyResources(this.miInterfasePdisModificados, "miInterfasePdisModificados");
      this.miInterfasePdisModificados.EscuchadorDeEstatus = null;
      this.miInterfasePdisModificados.ManejadorDeMapa = null;
      this.miInterfasePdisModificados.Name = "miInterfasePdisModificados";
      // 
      // miPáginaPosiblesDuplicados
      // 
      this.miPáginaPosiblesDuplicados.BackColor = System.Drawing.Color.Transparent;
      this.miPáginaPosiblesDuplicados.Controls.Add(this.miInterfasePdisDuplicados);
      resources.ApplyResources(this.miPáginaPosiblesDuplicados, "miPáginaPosiblesDuplicados");
      this.miPáginaPosiblesDuplicados.Name = "miPáginaPosiblesDuplicados";
      this.miPáginaPosiblesDuplicados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePdisDuplicados
      // 
      resources.ApplyResources(this.miInterfasePdisDuplicados, "miInterfasePdisDuplicados");
      this.miInterfasePdisDuplicados.EscuchadorDeEstatus = null;
      this.miInterfasePdisDuplicados.ManejadorDeMapa = null;
      this.miInterfasePdisDuplicados.Name = "miInterfasePdisDuplicados";
      // 
      // miPáginaEliminados
      // 
      this.miPáginaEliminados.Controls.Add(this.miInterfasePdisEliminados);
      resources.ApplyResources(this.miPáginaEliminados, "miPáginaEliminados");
      this.miPáginaEliminados.Name = "miPáginaEliminados";
      this.miPáginaEliminados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePdisEliminados
      // 
      resources.ApplyResources(this.miInterfasePdisEliminados, "miInterfasePdisEliminados");
      this.miInterfasePdisEliminados.EscuchadorDeEstatus = null;
      this.miInterfasePdisEliminados.ManejadorDeMapa = null;
      this.miInterfasePdisEliminados.Name = "miInterfasePdisEliminados";
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfasePdisErrores);
      resources.ApplyResources(this.miPáginaErrores, "miPáginaErrores");
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfasePdisErrores
      // 
      resources.ApplyResources(this.miInterfasePdisErrores, "miInterfasePdisErrores");
      this.miInterfasePdisErrores.EscuchadorDeEstatus = null;
      this.miInterfasePdisErrores.ManejadorDeMapa = null;
      this.miInterfasePdisErrores.Name = "miInterfasePdisErrores";
      // 
      // InterfaseManejadorDePdis
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañas);
      this.Name = "InterfaseManejadorDePdis";
      this.miControladorDePestañas.ResumeLayout(false);
      this.miPáginaMapa.ResumeLayout(false);
      this.miPáginaDeTodos.ResumeLayout(false);
      this.miPáginaModificados.ResumeLayout(false);
      this.miPáginaPosiblesDuplicados.ResumeLayout(false);
      this.miPáginaEliminados.ResumeLayout(false);
      this.miPáginaErrores.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas miControladorDePestañas;
    private System.Windows.Forms.TabPage miPáginaDeTodos;
    private GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseListaDePdis miLista;
    private System.Windows.Forms.TabPage miPáginaModificados;
    private InterfaseDePdisModificados miInterfasePdisModificados;
    private System.Windows.Forms.TabPage miPáginaPosiblesDuplicados;
    private InterfaseDePdisDuplicados miInterfasePdisDuplicados;
    private System.Windows.Forms.TabPage miPáginaEliminados;
    private InterfaseDePdisEliminados miInterfasePdisEliminados;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaseMapa miInterfaseDeMapa;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private InterfaseDePdisConErrores miInterfasePdisErrores;
  }
}
