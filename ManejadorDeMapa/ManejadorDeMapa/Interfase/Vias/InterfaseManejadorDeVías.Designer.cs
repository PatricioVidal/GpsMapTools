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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDeVías));
      this.miControladorDePestañas = new System.Windows.Forms.TabControl();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miDivisión = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
      this.miMapaDeVíaSeleccionada = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíaSeleccionada();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfaseDeErroresEnVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseDeErroresEnVías();
      this.miControladorDePestañas.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miDivisión.Panel1.SuspendLayout();
      this.miDivisión.Panel2.SuspendLayout();
      this.miDivisión.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // miControladorDePestañas
      // 
      this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañas.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañas.Controls.Add(this.miPáginaErrores);
      this.miControladorDePestañas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miControladorDePestañas.Location = new System.Drawing.Point(0, 0);
      this.miControladorDePestañas.Name = "miControladorDePestañas";
      this.miControladorDePestañas.SelectedIndex = 0;
      this.miControladorDePestañas.Size = new System.Drawing.Size(594, 457);
      this.miControladorDePestañas.TabIndex = 5;
      // 
      // miPáginaMapa
      // 
      this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
      this.miPáginaMapa.Location = new System.Drawing.Point(4, 22);
      this.miPáginaMapa.Name = "miPáginaMapa";
      this.miPáginaMapa.Size = new System.Drawing.Size(586, 431);
      this.miPáginaMapa.TabIndex = 4;
      this.miPáginaMapa.Text = "Mapa";
      this.miPáginaMapa.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeMapa
      // 
      this.miInterfaseDeMapa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseDeMapa.EscuchadorDeEstatus = null;
      this.miInterfaseDeMapa.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseDeMapa.ManejadorDeMapa = null;
      this.miInterfaseDeMapa.MuestraPDIs = false;
      this.miInterfaseDeMapa.MuestraPolígonos = false;
      this.miInterfaseDeMapa.MuestraPolilíneas = false;
      this.miInterfaseDeMapa.MuestraTodoElMapa = true;
      this.miInterfaseDeMapa.MuestraTodosLosElementos = false;
      this.miInterfaseDeMapa.MuestraVías = true;
      this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
      this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
      this.miInterfaseDeMapa.Size = new System.Drawing.Size(586, 431);
      this.miInterfaseDeMapa.TabIndex = 0;
      // 
      // miPáginaDeTodos
      // 
      this.miPáginaDeTodos.Controls.Add(this.miDivisión);
      this.miPáginaDeTodos.Location = new System.Drawing.Point(4, 22);
      this.miPáginaDeTodos.Name = "miPáginaDeTodos";
      this.miPáginaDeTodos.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaDeTodos.Size = new System.Drawing.Size(586, 431);
      this.miPáginaDeTodos.TabIndex = 0;
      this.miPáginaDeTodos.Text = "Todos";
      this.miPáginaDeTodos.UseVisualStyleBackColor = true;
      // 
      // miDivisión
      // 
      this.miDivisión.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miDivisión.Location = new System.Drawing.Point(3, 3);
      this.miDivisión.Name = "miDivisión";
      this.miDivisión.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // miDivisión.Panel1
      // 
      this.miDivisión.Panel1.Controls.Add(this.miLista);
      // 
      // miDivisión.Panel2
      // 
      this.miDivisión.Panel2.Controls.Add(this.miMapaDeVíaSeleccionada);
      this.miDivisión.Size = new System.Drawing.Size(580, 425);
      this.miDivisión.SplitterDistance = 212;
      this.miDivisión.TabIndex = 4;
      // 
      // miLista
      // 
      this.miLista.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(580, 212);
      this.miLista.TabIndex = 3;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // miMapaDeVíaSeleccionada
      // 
      this.miMapaDeVíaSeleccionada.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miMapaDeVíaSeleccionada.EscuchadorDeEstatus = null;
      this.miMapaDeVíaSeleccionada.Location = new System.Drawing.Point(0, 0);
      this.miMapaDeVíaSeleccionada.ManejadorDeMapa = null;
      this.miMapaDeVíaSeleccionada.MuestraPDIs = false;
      this.miMapaDeVíaSeleccionada.MuestraPolígonos = false;
      this.miMapaDeVíaSeleccionada.MuestraPolilíneas = false;
      this.miMapaDeVíaSeleccionada.MuestraTodoElMapa = true;
      this.miMapaDeVíaSeleccionada.MuestraTodosLosElementos = true;
      this.miMapaDeVíaSeleccionada.MuestraVías = false;
      this.miMapaDeVíaSeleccionada.Name = "miMapaDeVíaSeleccionada";
      this.miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíaSeleccionada.RectánguloVisibleEnCoordenadas")));
      this.miMapaDeVíaSeleccionada.Size = new System.Drawing.Size(580, 209);
      this.miMapaDeVíaSeleccionada.TabIndex = 0;
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfaseDeErroresEnVías);
      this.miPáginaErrores.Location = new System.Drawing.Point(4, 22);
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaErrores.Size = new System.Drawing.Size(586, 431);
      this.miPáginaErrores.TabIndex = 5;
      this.miPáginaErrores.Text = "Errores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfaseDeErroresEnVías
      // 
      this.miInterfaseDeErroresEnVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseDeErroresEnVías.EscuchadorDeEstatus = null;
      this.miInterfaseDeErroresEnVías.Location = new System.Drawing.Point(3, 3);
      this.miInterfaseDeErroresEnVías.ManejadorDeMapa = null;
      this.miInterfaseDeErroresEnVías.Name = "miInterfaseDeErroresEnVías";
      this.miInterfaseDeErroresEnVías.Size = new System.Drawing.Size(580, 425);
      this.miInterfaseDeErroresEnVías.TabIndex = 0;
      // 
      // InterfaseManejadorDeVías
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañas);
      this.Name = "InterfaseManejadorDeVías";
      this.Size = new System.Drawing.Size(594, 457);
      this.miControladorDePestañas.ResumeLayout(false);
      this.miPáginaMapa.ResumeLayout(false);
      this.miPáginaDeTodos.ResumeLayout(false);
      this.miDivisión.Panel1.ResumeLayout(false);
      this.miDivisión.Panel2.ResumeLayout(false);
      this.miDivisión.ResumeLayout(false);
      this.miPáginaErrores.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl miControladorDePestañas;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaseMapa miInterfaseDeMapa;
    private System.Windows.Forms.TabPage miPáginaDeTodos;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
    private System.Windows.Forms.SplitContainer miDivisión;
    private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíaSeleccionada miMapaDeVíaSeleccionada;
    private InterfaseDeErroresEnVías miInterfaseDeErroresEnVías;
  }
}
