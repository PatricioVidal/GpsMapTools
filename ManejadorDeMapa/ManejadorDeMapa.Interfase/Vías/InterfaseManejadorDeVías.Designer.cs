namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseManejadorDeVías
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDeVías));
      this.miControladorDePestañas = new GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miDivisión = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaDeVías();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas();
      this.miPáginaModificadas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasModificadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasModificadas();
      this.miPáginaEliminadas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasEliminadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasEliminadas();
      this.miPáginaAlertas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasConIncongruencias = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasConAlertas();
      this.miPáginaPosibleErroresDeRuteo = new System.Windows.Forms.TabPage();
      this.miInterfasePosiblesErroresDeRuteoEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfasePosiblesErroresDeRuteoEnVías();
      this.miPáginaPosiblesNodosDesconectados = new System.Windows.Forms.TabPage();
      this.miInterfasePosiblesNodosDesconectados = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfasePosiblesNodosDesconectados();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfaseDeErroresEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeViasConErrores();
      this.miControladorDePestañas.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miDivisión.Panel1.SuspendLayout();
      this.miDivisión.Panel2.SuspendLayout();
      this.miDivisión.SuspendLayout();
      this.miPáginaModificadas.SuspendLayout();
      this.miPáginaEliminadas.SuspendLayout();
      this.miPáginaAlertas.SuspendLayout();
      this.miPáginaPosibleErroresDeRuteo.SuspendLayout();
      this.miPáginaPosiblesNodosDesconectados.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // miControladorDePestañas
      // 
      this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañas.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañas.Controls.Add(this.miPáginaModificadas);
      this.miControladorDePestañas.Controls.Add(this.miPáginaEliminadas);
      this.miControladorDePestañas.Controls.Add(this.miPáginaPosibleErroresDeRuteo);
      this.miControladorDePestañas.Controls.Add(this.miPáginaPosiblesNodosDesconectados);
      this.miControladorDePestañas.Controls.Add(this.miPáginaAlertas);
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
      this.miInterfaseDeMapa.MuestraCiudades = false;
      this.miInterfaseDeMapa.MuestraPdis = false;
      this.miInterfaseDeMapa.MuestraPolígonos = false;
      this.miInterfaseDeMapa.MuestraPolilíneas = false;
      this.miInterfaseDeMapa.MuestraTodoElMapa = true;
      this.miInterfaseDeMapa.MuestraTodosLosElementos = false;
      this.miInterfaseDeMapa.MuestraVías = true;
      this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
      this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
      // 
      // miPáginaDeTodos
      // 
      this.miPáginaDeTodos.Controls.Add(this.miDivisión);
      resources.ApplyResources(this.miPáginaDeTodos, "miPáginaDeTodos");
      this.miPáginaDeTodos.Name = "miPáginaDeTodos";
      this.miPáginaDeTodos.UseVisualStyleBackColor = true;
      // 
      // miDivisión
      // 
      resources.ApplyResources(this.miDivisión, "miDivisión");
      this.miDivisión.Name = "miDivisión";
      // 
      // miDivisión.Panel1
      // 
      this.miDivisión.Panel1.Controls.Add(this.miLista);
      // 
      // miDivisión.Panel2
      // 
      this.miDivisión.Panel2.Controls.Add(this.miMapaDeVíaSeleccionada);
      // 
      // miLista
      // 
      this.miLista.Activation = System.Windows.Forms.ItemActivation.OneClick;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.HideSelection = false;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miMapaDeVíaSeleccionada
      // 
      resources.ApplyResources(this.miMapaDeVíaSeleccionada, "miMapaDeVíaSeleccionada");
      this.miMapaDeVíaSeleccionada.EscuchadorDeEstatus = null;
      this.miMapaDeVíaSeleccionada.Lista = this.miLista;
      this.miMapaDeVíaSeleccionada.ManejadorDeMapa = null;
      this.miMapaDeVíaSeleccionada.MuestraCiudades = false;
      this.miMapaDeVíaSeleccionada.MuestraPdis = false;
      this.miMapaDeVíaSeleccionada.MuestraPolígonos = false;
      this.miMapaDeVíaSeleccionada.MuestraPolilíneas = false;
      this.miMapaDeVíaSeleccionada.MuestraTodoElMapa = true;
      this.miMapaDeVíaSeleccionada.MuestraTodosLosElementos = true;
      this.miMapaDeVíaSeleccionada.MuestraVías = false;
      this.miMapaDeVíaSeleccionada.Name = "miMapaDeVíaSeleccionada";
      this.miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas")));
      // 
      // miPáginaModificadas
      // 
      this.miPáginaModificadas.Controls.Add(this.miInterfaseDeVíasModificadas);
      resources.ApplyResources(this.miPáginaModificadas, "miPáginaModificadas");
      this.miPáginaModificadas.Name = "miPáginaModificadas";
      this.miPáginaModificadas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasModificadas
      // 
      resources.ApplyResources(this.miInterfaseDeVíasModificadas, "miInterfaseDeVíasModificadas");
      this.miInterfaseDeVíasModificadas.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasModificadas.ManejadorDeMapa = null;
      this.miInterfaseDeVíasModificadas.Name = "miInterfaseDeVíasModificadas";
      // 
      // miPáginaEliminadas
      // 
      this.miPáginaEliminadas.Controls.Add(this.miInterfaseDeVíasEliminadas);
      resources.ApplyResources(this.miPáginaEliminadas, "miPáginaEliminadas");
      this.miPáginaEliminadas.Name = "miPáginaEliminadas";
      this.miPáginaEliminadas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasEliminadas
      // 
      resources.ApplyResources(this.miInterfaseDeVíasEliminadas, "miInterfaseDeVíasEliminadas");
      this.miInterfaseDeVíasEliminadas.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasEliminadas.ManejadorDeMapa = null;
      this.miInterfaseDeVíasEliminadas.Name = "miInterfaseDeVíasEliminadas";
      // 
      // miPáginaAlertas
      // 
      this.miPáginaAlertas.Controls.Add(this.miInterfaseDeVíasConIncongruencias);
      resources.ApplyResources(this.miPáginaAlertas, "miPáginaAlertas");
      this.miPáginaAlertas.Name = "miPáginaAlertas";
      this.miPáginaAlertas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasConIncongruencias
      // 
      resources.ApplyResources(this.miInterfaseDeVíasConIncongruencias, "miInterfaseDeVíasConIncongruencias");
      this.miInterfaseDeVíasConIncongruencias.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasConIncongruencias.ManejadorDeMapa = null;
      this.miInterfaseDeVíasConIncongruencias.Name = "miInterfaseDeVíasConIncongruencias";
      // 
      // miPáginaPosibleErroresDeRuteo
      // 
      this.miPáginaPosibleErroresDeRuteo.Controls.Add(this.miInterfasePosiblesErroresDeRuteoEnVías);
      resources.ApplyResources(this.miPáginaPosibleErroresDeRuteo, "miPáginaPosibleErroresDeRuteo");
      this.miPáginaPosibleErroresDeRuteo.Name = "miPáginaPosibleErroresDeRuteo";
      this.miPáginaPosibleErroresDeRuteo.UseVisualStyleBackColor = true;
      // 
      // miInterfasePosiblesErroresDeRuteoEnVías
      // 
      resources.ApplyResources(this.miInterfasePosiblesErroresDeRuteoEnVías, "miInterfasePosiblesErroresDeRuteoEnVías");
      this.miInterfasePosiblesErroresDeRuteoEnVías.EscuchadorDeEstatus = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.ManejadorDeMapa = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.Name = "miInterfasePosiblesErroresDeRuteoEnVías";
      // 
      // miPáginaPosiblesNodosDesconectados
      // 
      this.miPáginaPosiblesNodosDesconectados.Controls.Add(this.miInterfasePosiblesNodosDesconectados);
      resources.ApplyResources(this.miPáginaPosiblesNodosDesconectados, "miPáginaPosiblesNodosDesconectados");
      this.miPáginaPosiblesNodosDesconectados.Name = "miPáginaPosiblesNodosDesconectados";
      this.miPáginaPosiblesNodosDesconectados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePosiblesNodosDesconectados
      // 
      resources.ApplyResources(this.miInterfasePosiblesNodosDesconectados, "miInterfasePosiblesNodosDesconectados");
      this.miInterfasePosiblesNodosDesconectados.EscuchadorDeEstatus = null;
      this.miInterfasePosiblesNodosDesconectados.ManejadorDeMapa = null;
      this.miInterfasePosiblesNodosDesconectados.Name = "miInterfasePosiblesNodosDesconectados";
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfaseDeErroresEnVías);
      resources.ApplyResources(this.miPáginaErrores, "miPáginaErrores");
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeErroresEnVías
      // 
      resources.ApplyResources(this.miInterfaseDeErroresEnVías, "miInterfaseDeErroresEnVías");
      this.miInterfaseDeErroresEnVías.EscuchadorDeEstatus = null;
      this.miInterfaseDeErroresEnVías.ManejadorDeMapa = null;
      this.miInterfaseDeErroresEnVías.Name = "miInterfaseDeErroresEnVías";
      // 
      // InterfaseManejadorDeVías
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañas);
      this.Name = "InterfaseManejadorDeVías";
      this.miControladorDePestañas.ResumeLayout(false);
      this.miPáginaMapa.ResumeLayout(false);
      this.miPáginaDeTodos.ResumeLayout(false);
      this.miDivisión.Panel1.ResumeLayout(false);
      this.miDivisión.Panel2.ResumeLayout(false);
      this.miDivisión.ResumeLayout(false);
      this.miPáginaModificadas.ResumeLayout(false);
      this.miPáginaEliminadas.ResumeLayout(false);
      this.miPáginaAlertas.ResumeLayout(false);
      this.miPáginaPosibleErroresDeRuteo.ResumeLayout(false);
      this.miPáginaPosiblesNodosDesconectados.ResumeLayout(false);
      this.miPáginaErrores.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas miControladorDePestañas;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaseMapa miInterfaseDeMapa;
    private System.Windows.Forms.TabPage miPáginaDeTodos;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private InterfaseListaDeVías miLista;
    private System.Windows.Forms.SplitContainer miDivisión;
    private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas miMapaDeVíaSeleccionada;
    private InterfaseDeViasConErrores miInterfaseDeErroresEnVías;
    private System.Windows.Forms.TabPage miPáginaModificadas;
    private InterfaseDeVíasModificadas miInterfaseDeVíasModificadas;
    private System.Windows.Forms.TabPage miPáginaAlertas;
    private InterfaseDeVíasConAlertas miInterfaseDeVíasConIncongruencias;
    private System.Windows.Forms.TabPage miPáginaEliminadas;
    private InterfaseDeVíasEliminadas miInterfaseDeVíasEliminadas;
    private System.Windows.Forms.TabPage miPáginaPosibleErroresDeRuteo;
    private InterfasePosiblesErroresDeRuteoEnVías miInterfasePosiblesErroresDeRuteoEnVías;
    private System.Windows.Forms.TabPage miPáginaPosiblesNodosDesconectados;
    private InterfasePosiblesNodosDesconectados miInterfasePosiblesNodosDesconectados;
  }
}
