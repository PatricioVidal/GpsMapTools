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
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionada();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfaseDeErroresEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeViasConErrores();
      this.miPáginaModificadas = new System.Windows.Forms.TabPage();
      this.miInterfaseDeVíasModificadas = new GpsYv.ManejadorDeMapa.Interfase.Vias.InterfaseDeVíasModificadas();
      this.miControladorDePestañas.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miDivisión.Panel1.SuspendLayout();
      this.miDivisión.Panel2.SuspendLayout();
      this.miDivisión.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.miPáginaModificadas.SuspendLayout();
      this.SuspendLayout();
      // 
      // miControladorDePestañas
      // 
      this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañas.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañas.Controls.Add(this.miPáginaModificadas);
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
      this.miInterfaseDeMapa.MuestraPDIs = false;
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
      this.miLista.Name = "miLista";
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
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
      this.miPáginaErrores.ResumeLayout(false);
      this.miPáginaModificadas.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas miControladorDePestañas;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaseMapa miInterfaseDeMapa;
    private System.Windows.Forms.TabPage miPáginaDeTodos;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
    private System.Windows.Forms.SplitContainer miDivisión;
    private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionada miMapaDeVíaSeleccionada;
    private InterfaseDeViasConErrores miInterfaseDeErroresEnVías;
    private System.Windows.Forms.TabPage miPáginaModificadas;
    private GpsYv.ManejadorDeMapa.Interfase.Vias.InterfaseDeVíasModificadas miInterfaseDeVíasModificadas;
  }
}
