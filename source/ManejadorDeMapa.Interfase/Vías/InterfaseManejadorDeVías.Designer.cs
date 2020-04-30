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
      GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías menuEditorDeVías;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDeVías));
      this.miDivisión = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaDeVías();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas();
      this.miControladorDePestañas = new GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miPáginaModificadas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasModificadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasModificadas();
      this.miPáginaEliminadas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasEliminadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasEliminadas();
      this.miPáginaPosibleErroresDeRuteo = new System.Windows.Forms.TabPage();
      this.miInterfasePosiblesErroresDeRuteoEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfasePosiblesErroresDeRuteoEnVías();
      this.miPáginaPosiblesNodosDesconectados = new System.Windows.Forms.TabPage();
      this.miInterfasePosiblesNodosDesconectados = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfasePosiblesNodosDesconectados();
      this.miPáginaAlertas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasConIncongruencias = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeVíasConAlertas();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfaseDeErroresEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeViasConErrores();
      menuEditorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías();
      this.miDivisión.Panel1.SuspendLayout();
      this.miDivisión.Panel2.SuspendLayout();
      this.miDivisión.SuspendLayout();
      this.miControladorDePestañas.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miPáginaModificadas.SuspendLayout();
      this.miPáginaEliminadas.SuspendLayout();
      this.miPáginaPosibleErroresDeRuteo.SuspendLayout();
      this.miPáginaPosiblesNodosDesconectados.SuspendLayout();
      this.miPáginaAlertas.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuEditorDeVías
      // 
      menuEditorDeVías.AccessibleDescription = null;
      menuEditorDeVías.AccessibleName = null;
      resources.ApplyResources(menuEditorDeVías, "menuEditorDeVías");
      menuEditorDeVías.BackgroundImage = null;
      menuEditorDeVías.Font = null;
      menuEditorDeVías.ManejadorDeVías = null;
      menuEditorDeVías.Name = "miMenuDeContexto";
      // 
      // miDivisión
      // 
      this.miDivisión.AccessibleDescription = null;
      this.miDivisión.AccessibleName = null;
      resources.ApplyResources(this.miDivisión, "miDivisión");
      this.miDivisión.BackgroundImage = null;
      this.miDivisión.Font = null;
      this.miDivisión.Name = "miDivisión";
      // 
      // miDivisión.Panel1
      // 
      this.miDivisión.Panel1.AccessibleDescription = null;
      this.miDivisión.Panel1.AccessibleName = null;
      resources.ApplyResources(this.miDivisión.Panel1, "miDivisión.Panel1");
      this.miDivisión.Panel1.BackgroundImage = null;
      this.miDivisión.Panel1.Controls.Add(this.miLista);
      this.miDivisión.Panel1.Font = null;
      // 
      // miDivisión.Panel2
      // 
      this.miDivisión.Panel2.AccessibleDescription = null;
      this.miDivisión.Panel2.AccessibleName = null;
      resources.ApplyResources(this.miDivisión.Panel2, "miDivisión.Panel2");
      this.miDivisión.Panel2.BackgroundImage = null;
      this.miDivisión.Panel2.Controls.Add(this.miMapaDeVíaSeleccionada);
      this.miDivisión.Panel2.Font = null;
      // 
      // miLista
      // 
      this.miLista.AccessibleDescription = null;
      this.miLista.AccessibleName = null;
      this.miLista.Activation = System.Windows.Forms.ItemActivation.OneClick;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.BackgroundImage = null;
      this.miLista.ContextMenuStrip = menuEditorDeVías;
      this.miLista.Font = null;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.HideSelection = false;
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
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
      this.miMapaDeVíaSeleccionada.MuestraCiudades = false;
      this.miMapaDeVíaSeleccionada.MuestraEstados = false;
      this.miMapaDeVíaSeleccionada.MuestraPdis = false;
      this.miMapaDeVíaSeleccionada.MuestraPolígonos = false;
      this.miMapaDeVíaSeleccionada.MuestraPolilíneas = false;
      this.miMapaDeVíaSeleccionada.MuestraTodoElMapa = true;
      this.miMapaDeVíaSeleccionada.MuestraTodosLosElementos = true;
      this.miMapaDeVíaSeleccionada.MuestraVías = false;
      this.miMapaDeVíaSeleccionada.Name = "miMapaDeVíaSeleccionada";
      this.miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas")));
      // 
      // miControladorDePestañas
      // 
      this.miControladorDePestañas.AccessibleDescription = null;
      this.miControladorDePestañas.AccessibleName = null;
      resources.ApplyResources(this.miControladorDePestañas, "miControladorDePestañas");
      this.miControladorDePestañas.BackgroundImage = null;
      this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañas.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañas.Controls.Add(this.miPáginaModificadas);
      this.miControladorDePestañas.Controls.Add(this.miPáginaEliminadas);
      this.miControladorDePestañas.Controls.Add(this.miPáginaPosibleErroresDeRuteo);
      this.miControladorDePestañas.Controls.Add(this.miPáginaPosiblesNodosDesconectados);
      this.miControladorDePestañas.Controls.Add(this.miPáginaAlertas);
      this.miControladorDePestañas.Controls.Add(this.miPáginaErrores);
      this.miControladorDePestañas.Font = null;
      this.miControladorDePestañas.Name = "miControladorDePestañas";
      this.miControladorDePestañas.SelectedIndex = 0;
      // 
      // miPáginaMapa
      // 
      this.miPáginaMapa.AccessibleDescription = null;
      this.miPáginaMapa.AccessibleName = null;
      resources.ApplyResources(this.miPáginaMapa, "miPáginaMapa");
      this.miPáginaMapa.BackgroundImage = null;
      this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
      this.miPáginaMapa.Font = null;
      this.miPáginaMapa.Name = "miPáginaMapa";
      this.miPáginaMapa.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeMapa
      // 
      this.miInterfaseDeMapa.AccessibleDescription = null;
      this.miInterfaseDeMapa.AccessibleName = null;
      resources.ApplyResources(this.miInterfaseDeMapa, "miInterfaseDeMapa");
      this.miInterfaseDeMapa.BackgroundImage = null;
      this.miInterfaseDeMapa.EscuchadorDeEstatus = null;
      this.miInterfaseDeMapa.Font = null;
      this.miInterfaseDeMapa.ManejadorDeMapa = null;
      this.miInterfaseDeMapa.MuestraCiudades = false;
      this.miInterfaseDeMapa.MuestraEstados = false;
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
      this.miPáginaDeTodos.AccessibleDescription = null;
      this.miPáginaDeTodos.AccessibleName = null;
      resources.ApplyResources(this.miPáginaDeTodos, "miPáginaDeTodos");
      this.miPáginaDeTodos.BackgroundImage = null;
      this.miPáginaDeTodos.Controls.Add(this.miDivisión);
      this.miPáginaDeTodos.Font = null;
      this.miPáginaDeTodos.Name = "miPáginaDeTodos";
      this.miPáginaDeTodos.UseVisualStyleBackColor = true;
      // 
      // miPáginaModificadas
      // 
      this.miPáginaModificadas.AccessibleDescription = null;
      this.miPáginaModificadas.AccessibleName = null;
      resources.ApplyResources(this.miPáginaModificadas, "miPáginaModificadas");
      this.miPáginaModificadas.BackgroundImage = null;
      this.miPáginaModificadas.Controls.Add(this.miInterfaseDeVíasModificadas);
      this.miPáginaModificadas.Font = null;
      this.miPáginaModificadas.Name = "miPáginaModificadas";
      this.miPáginaModificadas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasModificadas
      // 
      this.miInterfaseDeVíasModificadas.AccessibleDescription = null;
      this.miInterfaseDeVíasModificadas.AccessibleName = null;
      resources.ApplyResources(this.miInterfaseDeVíasModificadas, "miInterfaseDeVíasModificadas");
      this.miInterfaseDeVíasModificadas.BackgroundImage = null;
      this.miInterfaseDeVíasModificadas.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasModificadas.Font = null;
      this.miInterfaseDeVíasModificadas.ManejadorDeMapa = null;
      this.miInterfaseDeVíasModificadas.Name = "miInterfaseDeVíasModificadas";
      // 
      // miPáginaEliminadas
      // 
      this.miPáginaEliminadas.AccessibleDescription = null;
      this.miPáginaEliminadas.AccessibleName = null;
      resources.ApplyResources(this.miPáginaEliminadas, "miPáginaEliminadas");
      this.miPáginaEliminadas.BackgroundImage = null;
      this.miPáginaEliminadas.Controls.Add(this.miInterfaseDeVíasEliminadas);
      this.miPáginaEliminadas.Font = null;
      this.miPáginaEliminadas.Name = "miPáginaEliminadas";
      this.miPáginaEliminadas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasEliminadas
      // 
      this.miInterfaseDeVíasEliminadas.AccessibleDescription = null;
      this.miInterfaseDeVíasEliminadas.AccessibleName = null;
      resources.ApplyResources(this.miInterfaseDeVíasEliminadas, "miInterfaseDeVíasEliminadas");
      this.miInterfaseDeVíasEliminadas.BackgroundImage = null;
      this.miInterfaseDeVíasEliminadas.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasEliminadas.Font = null;
      this.miInterfaseDeVíasEliminadas.ManejadorDeMapa = null;
      this.miInterfaseDeVíasEliminadas.Name = "miInterfaseDeVíasEliminadas";
      // 
      // miPáginaPosibleErroresDeRuteo
      // 
      this.miPáginaPosibleErroresDeRuteo.AccessibleDescription = null;
      this.miPáginaPosibleErroresDeRuteo.AccessibleName = null;
      resources.ApplyResources(this.miPáginaPosibleErroresDeRuteo, "miPáginaPosibleErroresDeRuteo");
      this.miPáginaPosibleErroresDeRuteo.BackgroundImage = null;
      this.miPáginaPosibleErroresDeRuteo.Controls.Add(this.miInterfasePosiblesErroresDeRuteoEnVías);
      this.miPáginaPosibleErroresDeRuteo.Font = null;
      this.miPáginaPosibleErroresDeRuteo.Name = "miPáginaPosibleErroresDeRuteo";
      this.miPáginaPosibleErroresDeRuteo.UseVisualStyleBackColor = true;
      // 
      // miInterfasePosiblesErroresDeRuteoEnVías
      // 
      this.miInterfasePosiblesErroresDeRuteoEnVías.AccessibleDescription = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.AccessibleName = null;
      resources.ApplyResources(this.miInterfasePosiblesErroresDeRuteoEnVías, "miInterfasePosiblesErroresDeRuteoEnVías");
      this.miInterfasePosiblesErroresDeRuteoEnVías.BackgroundImage = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.EscuchadorDeEstatus = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.Font = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.ManejadorDeMapa = null;
      this.miInterfasePosiblesErroresDeRuteoEnVías.Name = "miInterfasePosiblesErroresDeRuteoEnVías";
      // 
      // miPáginaPosiblesNodosDesconectados
      // 
      this.miPáginaPosiblesNodosDesconectados.AccessibleDescription = null;
      this.miPáginaPosiblesNodosDesconectados.AccessibleName = null;
      resources.ApplyResources(this.miPáginaPosiblesNodosDesconectados, "miPáginaPosiblesNodosDesconectados");
      this.miPáginaPosiblesNodosDesconectados.BackgroundImage = null;
      this.miPáginaPosiblesNodosDesconectados.Controls.Add(this.miInterfasePosiblesNodosDesconectados);
      this.miPáginaPosiblesNodosDesconectados.Font = null;
      this.miPáginaPosiblesNodosDesconectados.Name = "miPáginaPosiblesNodosDesconectados";
      this.miPáginaPosiblesNodosDesconectados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePosiblesNodosDesconectados
      // 
      this.miInterfasePosiblesNodosDesconectados.AccessibleDescription = null;
      this.miInterfasePosiblesNodosDesconectados.AccessibleName = null;
      resources.ApplyResources(this.miInterfasePosiblesNodosDesconectados, "miInterfasePosiblesNodosDesconectados");
      this.miInterfasePosiblesNodosDesconectados.BackgroundImage = null;
      this.miInterfasePosiblesNodosDesconectados.EscuchadorDeEstatus = null;
      this.miInterfasePosiblesNodosDesconectados.Font = null;
      this.miInterfasePosiblesNodosDesconectados.ManejadorDeMapa = null;
      this.miInterfasePosiblesNodosDesconectados.Name = "miInterfasePosiblesNodosDesconectados";
      // 
      // miPáginaAlertas
      // 
      this.miPáginaAlertas.AccessibleDescription = null;
      this.miPáginaAlertas.AccessibleName = null;
      resources.ApplyResources(this.miPáginaAlertas, "miPáginaAlertas");
      this.miPáginaAlertas.BackgroundImage = null;
      this.miPáginaAlertas.Controls.Add(this.miInterfaseDeVíasConIncongruencias);
      this.miPáginaAlertas.Font = null;
      this.miPáginaAlertas.Name = "miPáginaAlertas";
      this.miPáginaAlertas.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeVíasConIncongruencias
      // 
      this.miInterfaseDeVíasConIncongruencias.AccessibleDescription = null;
      this.miInterfaseDeVíasConIncongruencias.AccessibleName = null;
      resources.ApplyResources(this.miInterfaseDeVíasConIncongruencias, "miInterfaseDeVíasConIncongruencias");
      this.miInterfaseDeVíasConIncongruencias.BackgroundImage = null;
      this.miInterfaseDeVíasConIncongruencias.EscuchadorDeEstatus = null;
      this.miInterfaseDeVíasConIncongruencias.Font = null;
      this.miInterfaseDeVíasConIncongruencias.ManejadorDeMapa = null;
      this.miInterfaseDeVíasConIncongruencias.Name = "miInterfaseDeVíasConIncongruencias";
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.AccessibleDescription = null;
      this.miPáginaErrores.AccessibleName = null;
      resources.ApplyResources(this.miPáginaErrores, "miPáginaErrores");
      this.miPáginaErrores.BackgroundImage = null;
      this.miPáginaErrores.Controls.Add(this.miInterfaseDeErroresEnVías);
      this.miPáginaErrores.Font = null;
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeErroresEnVías
      // 
      this.miInterfaseDeErroresEnVías.AccessibleDescription = null;
      this.miInterfaseDeErroresEnVías.AccessibleName = null;
      resources.ApplyResources(this.miInterfaseDeErroresEnVías, "miInterfaseDeErroresEnVías");
      this.miInterfaseDeErroresEnVías.BackgroundImage = null;
      this.miInterfaseDeErroresEnVías.EscuchadorDeEstatus = null;
      this.miInterfaseDeErroresEnVías.Font = null;
      this.miInterfaseDeErroresEnVías.ManejadorDeMapa = null;
      this.miInterfaseDeErroresEnVías.Name = "miInterfaseDeErroresEnVías";
      // 
      // InterfaseManejadorDeVías
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.Controls.Add(this.miControladorDePestañas);
      this.Font = null;
      this.Name = "InterfaseManejadorDeVías";
      this.miDivisión.Panel1.ResumeLayout(false);
      this.miDivisión.Panel2.ResumeLayout(false);
      this.miDivisión.ResumeLayout(false);
      this.miControladorDePestañas.ResumeLayout(false);
      this.miPáginaMapa.ResumeLayout(false);
      this.miPáginaDeTodos.ResumeLayout(false);
      this.miPáginaModificadas.ResumeLayout(false);
      this.miPáginaEliminadas.ResumeLayout(false);
      this.miPáginaPosibleErroresDeRuteo.ResumeLayout(false);
      this.miPáginaPosiblesNodosDesconectados.ResumeLayout(false);
      this.miPáginaAlertas.ResumeLayout(false);
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
