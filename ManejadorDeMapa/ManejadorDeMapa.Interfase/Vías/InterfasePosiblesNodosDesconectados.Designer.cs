namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfasePosiblesNodosDesconectados
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
      System.Windows.Forms.Panel panelSuperior;
      System.Windows.Forms.Button miBotónBuscaPosiblesNodosDesconectados;
      System.Windows.Forms.GroupBox groupBox1;
      System.Windows.Forms.Panel panelInferior;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfasePosiblesNodosDesconectados));
      System.Windows.Forms.ColumnHeader columnaCoordenadas;
      System.Windows.Forms.ColumnHeader columnaDistancia;
      System.Windows.Forms.ColumnHeader columnaDetalle;
      this.miTextoDistancia = new System.Windows.Forms.Label();
      this.miBarraDeDistancia = new System.Windows.Forms.TrackBar();
      this.miInterfaseListaConMapaDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaConMapaDeVías();
      this.miBotónActualizaLista = new System.Windows.Forms.Button();
      panelSuperior = new System.Windows.Forms.Panel();
      miBotónBuscaPosiblesNodosDesconectados = new System.Windows.Forms.Button();
      groupBox1 = new System.Windows.Forms.GroupBox();
      panelInferior = new System.Windows.Forms.Panel();
      columnaCoordenadas = new System.Windows.Forms.ColumnHeader();
      columnaDistancia = new System.Windows.Forms.ColumnHeader();
      columnaDetalle = new System.Windows.Forms.ColumnHeader();
      panelSuperior.SuspendLayout();
      groupBox1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeDistancia)).BeginInit();
      panelInferior.SuspendLayout();
      this.miInterfaseListaConMapaDeVías.División.Panel1.SuspendLayout();
      this.miInterfaseListaConMapaDeVías.División.Panel2.SuspendLayout();
      this.miInterfaseListaConMapaDeVías.División.SuspendLayout();
      this.miInterfaseListaConMapaDeVías.SuspendLayout();
      this.SuspendLayout();
      // 
      // panelSuperior
      // 
      panelSuperior.Controls.Add(this.miBotónActualizaLista);
      panelSuperior.Controls.Add(miBotónBuscaPosiblesNodosDesconectados);
      panelSuperior.Controls.Add(groupBox1);
      panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
      panelSuperior.Location = new System.Drawing.Point(0, 0);
      panelSuperior.Name = "panelSuperior";
      panelSuperior.Size = new System.Drawing.Size(836, 61);
      panelSuperior.TabIndex = 0;
      // 
      // miBotónBuscaPosiblesNodosDesconectados
      // 
      miBotónBuscaPosiblesNodosDesconectados.AutoSize = true;
      miBotónBuscaPosiblesNodosDesconectados.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      miBotónBuscaPosiblesNodosDesconectados.Location = new System.Drawing.Point(224, 21);
      miBotónBuscaPosiblesNodosDesconectados.Name = "miBotónBuscaPosiblesNodosDesconectados";
      miBotónBuscaPosiblesNodosDesconectados.Size = new System.Drawing.Size(201, 23);
      miBotónBuscaPosiblesNodosDesconectados.TabIndex = 10;
      miBotónBuscaPosiblesNodosDesconectados.Text = "Busca Posibles Nodos Desconectados";
      miBotónBuscaPosiblesNodosDesconectados.UseVisualStyleBackColor = true;
      miBotónBuscaPosiblesNodosDesconectados.Click += new System.EventHandler(this.EnBotónBuscaPosiblesNodosDesconectados);
      // 
      // groupBox1
      // 
      groupBox1.BackColor = System.Drawing.SystemColors.Window;
      groupBox1.Controls.Add(this.miTextoDistancia);
      groupBox1.Controls.Add(this.miBarraDeDistancia);
      groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
      groupBox1.Location = new System.Drawing.Point(3, 3);
      groupBox1.Name = "groupBox1";
      groupBox1.Size = new System.Drawing.Size(215, 56);
      groupBox1.TabIndex = 8;
      groupBox1.TabStop = false;
      groupBox1.Text = "Distancia Máxima";
      // 
      // miTextoDistancia
      // 
      this.miTextoDistancia.AutoSize = true;
      this.miTextoDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miTextoDistancia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.miTextoDistancia.Location = new System.Drawing.Point(165, 18);
      this.miTextoDistancia.Name = "miTextoDistancia";
      this.miTextoDistancia.Size = new System.Drawing.Size(24, 13);
      this.miTextoDistancia.TabIndex = 1;
      this.miTextoDistancia.Text = "0 m";
      // 
      // miBarraDeDistancia
      // 
      this.miBarraDeDistancia.AutoSize = false;
      this.miBarraDeDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miBarraDeDistancia.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default, "DistanciaMáximaBuscarPosiblesNodosDesconectados", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.miBarraDeDistancia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.miBarraDeDistancia.LargeChange = 20;
      this.miBarraDeDistancia.Location = new System.Drawing.Point(6, 18);
      this.miBarraDeDistancia.Maximum = 20;
      this.miBarraDeDistancia.Name = "miBarraDeDistancia";
      this.miBarraDeDistancia.Size = new System.Drawing.Size(153, 32);
      this.miBarraDeDistancia.TabIndex = 2;
      this.miBarraDeDistancia.Value = global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default.DistanciaMáximaBuscarPosiblesNodosDesconectados;
      this.miBarraDeDistancia.ValueChanged += new System.EventHandler(this.EnCambióBarraDeDistancia);
      // 
      // panelInferior
      // 
      panelInferior.Controls.Add(this.miInterfaseListaConMapaDeVías);
      panelInferior.Dock = System.Windows.Forms.DockStyle.Fill;
      panelInferior.Location = new System.Drawing.Point(0, 61);
      panelInferior.Name = "panelInferior";
      panelInferior.Size = new System.Drawing.Size(836, 396);
      panelInferior.TabIndex = 1;
      // 
      // miInterfaseListaConMapaDeVías
      // 
      // 
      // miInterfaseListaConMapaDeVías.División
      // 
      this.miInterfaseListaConMapaDeVías.División.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.División.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.División.Name = "División";
      this.miInterfaseListaConMapaDeVías.División.Orientation = System.Windows.Forms.Orientation.Horizontal;
      this.miInterfaseListaConMapaDeVías.División.Size = new System.Drawing.Size(836, 396);
      this.miInterfaseListaConMapaDeVías.División.SplitterDistance = 233;
      this.miInterfaseListaConMapaDeVías.División.TabIndex = 5;
      this.miInterfaseListaConMapaDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = null;
      // 
      // miInterfaseListaConMapaDeVías.InterfaseListaDeVías
      // 
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaCoordenadas,
            columnaDistancia,
            columnaDetalle});
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.ContextMenuStrip = this.miInterfaseListaConMapaDeVías.MenuEditorDeVías;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.FullRowSelect = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.GridLines = true;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.HideSelection = false;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Name = "InterfaseListaDeVías";
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Size = new System.Drawing.Size(836, 233);
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.TabIndex = 2;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.UseCompatibleStateImageBehavior = false;
      this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías.View = System.Windows.Forms.View.Details;
      // 
      // miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas
      // 
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.EscuchadorDeEstatus = null;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.Lista = this.miInterfaseListaConMapaDeVías.InterfaseListaDeVías;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraCiudades = false;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraPdis = false;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraPolígonos = false;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraPolilíneas = false;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraTodoElMapa = true;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraTodosLosElementos = true;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.MuestraVías = false;
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.Name = "InterfaseMapaDeVíasSeleccionadas";
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.RectánguloVisibleE" +
              "nCoordenadas")));
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.Size = new System.Drawing.Size(836, 159);
      this.miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.TabIndex = 0;
      this.miInterfaseListaConMapaDeVías.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.Name = "miInterfaseListaConMapaDeVías";
      this.miInterfaseListaConMapaDeVías.Size = new System.Drawing.Size(836, 396);
      this.miInterfaseListaConMapaDeVías.TabIndex = 0;
      // 
      // columnaCoordenadas
      // 
      columnaCoordenadas.Text = "Coordenadas";
      columnaCoordenadas.Width = 160;
      // 
      // columnaDistancia
      // 
      columnaDistancia.Text = "Distancia (m)";
      columnaDistancia.Width = 85;
      // 
      // columnaDetalle
      // 
      columnaDetalle.Text = "Detalle";
      columnaDetalle.Width = 400;
      // 
      // miBotónActualizaLista
      // 
      this.miBotónActualizaLista.AutoSize = true;
      this.miBotónActualizaLista.Enabled = false;
      this.miBotónActualizaLista.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.miBotónActualizaLista.Location = new System.Drawing.Point(431, 21);
      this.miBotónActualizaLista.Name = "miBotónActualizaLista";
      this.miBotónActualizaLista.Size = new System.Drawing.Size(88, 23);
      this.miBotónActualizaLista.TabIndex = 11;
      this.miBotónActualizaLista.Text = "Actualiza Lista";
      this.miBotónActualizaLista.UseVisualStyleBackColor = true;
      this.miBotónActualizaLista.Click += new System.EventHandler(this.EnBotónActualizaLista);
      // 
      // InterfasePosiblesNodosDesconectados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(panelInferior);
      this.Controls.Add(panelSuperior);
      this.Name = "InterfasePosiblesNodosDesconectados";
      this.Size = new System.Drawing.Size(836, 457);
      panelSuperior.ResumeLayout(false);
      panelSuperior.PerformLayout();
      groupBox1.ResumeLayout(false);
      groupBox1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeDistancia)).EndInit();
      panelInferior.ResumeLayout(false);
      this.miInterfaseListaConMapaDeVías.División.Panel1.ResumeLayout(false);
      this.miInterfaseListaConMapaDeVías.División.Panel2.ResumeLayout(false);
      this.miInterfaseListaConMapaDeVías.División.ResumeLayout(false);
      this.miInterfaseListaConMapaDeVías.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDeVías miInterfaseListaConMapaDeVías;
    private System.Windows.Forms.Label miTextoDistancia;
    private System.Windows.Forms.TrackBar miBarraDeDistancia;
    private System.Windows.Forms.Button miBotónActualizaLista;
  }
}
