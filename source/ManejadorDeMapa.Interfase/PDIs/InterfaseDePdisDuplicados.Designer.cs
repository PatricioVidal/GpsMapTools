namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class InterfaseDePdisDuplicados
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
      GpsYv.ManejadorDeMapa.Interfase.OrdenadorDeColumnaDeLista ordenadorDeColumnaDeLista;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDePdisDuplicados));
      this.miLista = new System.Windows.Forms.ListView();
      this.miRecipienteDividido = new System.Windows.Forms.SplitContainer();
      this.miMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miColumnaNúmeroDeElemento = new System.Windows.Forms.ColumnHeader();
      this.miColumnaTipo = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDescripción = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombre = new System.Windows.Forms.ColumnHeader();
      this.miColumnaCoordenadas = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDistancia = new System.Windows.Forms.ColumnHeader();
      this.miMenuEditorDePdis = new GpsYv.ManejadorDeMapa.Interfase.Pdis.MenuEditorDePdis();
      this.panel1 = new System.Windows.Forms.Panel();
      this.miTextoNumeroDePdisSelecionados = new System.Windows.Forms.Label();
      this.miBotonEliminarPdis = new System.Windows.Forms.Button();
      this.miBarraDeDistancia = new System.Windows.Forms.TrackBar();
      this.panel3 = new System.Windows.Forms.Panel();
      this.miBotónBuscaDuplicados = new System.Windows.Forms.Button();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.miTextoParecidoDelNombre = new System.Windows.Forms.Label();
      this.miBarraDeParecidoDeNombre = new System.Windows.Forms.TrackBar();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.miTextoDistancia = new System.Windows.Forms.Label();
      ordenadorDeColumnaDeLista = new GpsYv.ManejadorDeMapa.Interfase.OrdenadorDeColumnaDeLista(this.components);
      this.miRecipienteDividido.Panel1.SuspendLayout();
      this.miRecipienteDividido.Panel2.SuspendLayout();
      this.miRecipienteDividido.SuspendLayout();
      this.panel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeDistancia)).BeginInit();
      this.panel3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeParecidoDeNombre)).BeginInit();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ordenadorDeColumnaDeLista
      // 
      ordenadorDeColumnaDeLista.ItemsDeLaListaVirtual = null;
      ordenadorDeColumnaDeLista.Lista = this.miLista;
      // 
      // miLista
      // 
      this.miLista.AccessibleDescription = null;
      this.miLista.AccessibleName = null;
      resources.ApplyResources(this.miLista, "miLista");
      this.miLista.BackgroundImage = null;
      this.miLista.CheckBoxes = true;
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaNúmeroDeElemento,
            this.miColumnaTipo,
            this.miColumnaDescripción,
            this.miColumnaNombre,
            this.miColumnaCoordenadas,
            this.miColumnaDistancia});
      this.miLista.ContextMenuStrip = this.miMenuEditorDePdis;
      this.miLista.Font = null;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Name = "miLista";
      this.miLista.Sorting = System.Windows.Forms.SortOrder.Ascending;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.EnItemSeleccionado);
      this.miLista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EnClick);
      // 
      // miRecipienteDividido
      // 
      this.miRecipienteDividido.AccessibleDescription = null;
      this.miRecipienteDividido.AccessibleName = null;
      resources.ApplyResources(this.miRecipienteDividido, "miRecipienteDividido");
      this.miRecipienteDividido.BackgroundImage = null;
      this.miRecipienteDividido.Font = null;
      this.miRecipienteDividido.Name = "miRecipienteDividido";
      // 
      // miRecipienteDividido.Panel1
      // 
      this.miRecipienteDividido.Panel1.AccessibleDescription = null;
      this.miRecipienteDividido.Panel1.AccessibleName = null;
      resources.ApplyResources(this.miRecipienteDividido.Panel1, "miRecipienteDividido.Panel1");
      this.miRecipienteDividido.Panel1.BackgroundImage = null;
      this.miRecipienteDividido.Panel1.Controls.Add(this.miLista);
      this.miRecipienteDividido.Panel1.Font = null;
      // 
      // miRecipienteDividido.Panel2
      // 
      this.miRecipienteDividido.Panel2.AccessibleDescription = null;
      this.miRecipienteDividido.Panel2.AccessibleName = null;
      resources.ApplyResources(this.miRecipienteDividido.Panel2, "miRecipienteDividido.Panel2");
      this.miRecipienteDividido.Panel2.BackgroundImage = null;
      this.miRecipienteDividido.Panel2.Controls.Add(this.miMapa);
      this.miRecipienteDividido.Panel2.Font = null;
      // 
      // miMapa
      // 
      this.miMapa.AccessibleDescription = null;
      this.miMapa.AccessibleName = null;
      resources.ApplyResources(this.miMapa, "miMapa");
      this.miMapa.BackgroundImage = null;
      this.miMapa.EscuchadorDeEstatus = null;
      this.miMapa.Font = null;
      this.miMapa.ManejadorDeMapa = null;
      this.miMapa.MuestraCiudades = false;
      this.miMapa.MuestraEstados = false;
      this.miMapa.MuestraPdis = true;
      this.miMapa.MuestraPolígonos = false;
      this.miMapa.MuestraPolilíneas = false;
      this.miMapa.MuestraTodoElMapa = true;
      this.miMapa.MuestraTodosLosElementos = true;
      this.miMapa.MuestraVías = false;
      this.miMapa.Name = "miMapa";
      this.miMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapa.RectánguloVisibleEnCoordenadas")));
      // 
      // miColumnaNúmeroDeElemento
      // 
      resources.ApplyResources(this.miColumnaNúmeroDeElemento, "miColumnaNúmeroDeElemento");
      // 
      // miColumnaTipo
      // 
      resources.ApplyResources(this.miColumnaTipo, "miColumnaTipo");
      // 
      // miColumnaDescripción
      // 
      resources.ApplyResources(this.miColumnaDescripción, "miColumnaDescripción");
      // 
      // miColumnaNombre
      // 
      resources.ApplyResources(this.miColumnaNombre, "miColumnaNombre");
      // 
      // miColumnaCoordenadas
      // 
      resources.ApplyResources(this.miColumnaCoordenadas, "miColumnaCoordenadas");
      // 
      // miColumnaDistancia
      // 
      resources.ApplyResources(this.miColumnaDistancia, "miColumnaDistancia");
      // 
      // miMenuEditorDePdis
      // 
      this.miMenuEditorDePdis.AccessibleDescription = null;
      this.miMenuEditorDePdis.AccessibleName = null;
      resources.ApplyResources(this.miMenuEditorDePdis, "miMenuEditorDePdis");
      this.miMenuEditorDePdis.BackgroundImage = null;
      this.miMenuEditorDePdis.Font = null;
      this.miMenuEditorDePdis.Lista = this.miLista;
      this.miMenuEditorDePdis.ManejadorDePdis = null;
      this.miMenuEditorDePdis.Name = "miMenuDeContexto";
      // 
      // panel1
      // 
      this.panel1.AccessibleDescription = null;
      this.panel1.AccessibleName = null;
      resources.ApplyResources(this.panel1, "panel1");
      this.panel1.BackgroundImage = null;
      this.panel1.Controls.Add(this.miTextoNumeroDePdisSelecionados);
      this.panel1.Controls.Add(this.miBotonEliminarPdis);
      this.panel1.Font = null;
      this.panel1.Name = "panel1";
      // 
      // miTextoNumeroDePdisSelecionados
      // 
      this.miTextoNumeroDePdisSelecionados.AccessibleDescription = null;
      this.miTextoNumeroDePdisSelecionados.AccessibleName = null;
      resources.ApplyResources(this.miTextoNumeroDePdisSelecionados, "miTextoNumeroDePdisSelecionados");
      this.miTextoNumeroDePdisSelecionados.Font = null;
      this.miTextoNumeroDePdisSelecionados.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.miTextoNumeroDePdisSelecionados.Name = "miTextoNumeroDePdisSelecionados";
      // 
      // miBotonEliminarPdis
      // 
      this.miBotonEliminarPdis.AccessibleDescription = null;
      this.miBotonEliminarPdis.AccessibleName = null;
      resources.ApplyResources(this.miBotonEliminarPdis, "miBotonEliminarPdis");
      this.miBotonEliminarPdis.BackgroundImage = null;
      this.miBotonEliminarPdis.Font = null;
      this.miBotonEliminarPdis.Name = "miBotonEliminarPdis";
      this.miBotonEliminarPdis.UseVisualStyleBackColor = true;
      this.miBotonEliminarPdis.Click += new System.EventHandler(this.EnBotónEliminarPdis);
      // 
      // miBarraDeDistancia
      // 
      this.miBarraDeDistancia.AccessibleDescription = null;
      this.miBarraDeDistancia.AccessibleName = null;
      resources.ApplyResources(this.miBarraDeDistancia, "miBarraDeDistancia");
      this.miBarraDeDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miBarraDeDistancia.BackgroundImage = null;
      this.miBarraDeDistancia.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default, "DistanciaMáximaEnDecenasDeMetrosParaBuscarPdisDuplicados", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.miBarraDeDistancia.Font = null;
      this.miBarraDeDistancia.LargeChange = 20;
      this.miBarraDeDistancia.Maximum = 20;
      this.miBarraDeDistancia.Name = "miBarraDeDistancia";
      this.miBarraDeDistancia.Value = global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default.DistanciaMáximaEnDecenasDeMetrosParaBuscarPdisDuplicados;
      this.miBarraDeDistancia.ValueChanged += new System.EventHandler(this.EnCambióBarraDeDistancia);
      // 
      // panel3
      // 
      this.panel3.AccessibleDescription = null;
      this.panel3.AccessibleName = null;
      resources.ApplyResources(this.panel3, "panel3");
      this.panel3.BackgroundImage = null;
      this.panel3.Controls.Add(this.miBotónBuscaDuplicados);
      this.panel3.Controls.Add(this.groupBox2);
      this.panel3.Controls.Add(this.groupBox1);
      this.panel3.Font = null;
      this.panel3.Name = "panel3";
      // 
      // miBotónBuscaDuplicados
      // 
      this.miBotónBuscaDuplicados.AccessibleDescription = null;
      this.miBotónBuscaDuplicados.AccessibleName = null;
      resources.ApplyResources(this.miBotónBuscaDuplicados, "miBotónBuscaDuplicados");
      this.miBotónBuscaDuplicados.BackgroundImage = null;
      this.miBotónBuscaDuplicados.Font = null;
      this.miBotónBuscaDuplicados.Name = "miBotónBuscaDuplicados";
      this.miBotónBuscaDuplicados.UseVisualStyleBackColor = true;
      this.miBotónBuscaDuplicados.Click += new System.EventHandler(this.EnBotónBuscaDuplicados);
      // 
      // groupBox2
      // 
      this.groupBox2.AccessibleDescription = null;
      this.groupBox2.AccessibleName = null;
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
      this.groupBox2.BackgroundImage = null;
      this.groupBox2.Controls.Add(this.miTextoParecidoDelNombre);
      this.groupBox2.Controls.Add(this.miBarraDeParecidoDeNombre);
      this.groupBox2.Font = null;
      this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // miTextoParecidoDelNombre
      // 
      this.miTextoParecidoDelNombre.AccessibleDescription = null;
      this.miTextoParecidoDelNombre.AccessibleName = null;
      resources.ApplyResources(this.miTextoParecidoDelNombre, "miTextoParecidoDelNombre");
      this.miTextoParecidoDelNombre.BackColor = System.Drawing.SystemColors.Window;
      this.miTextoParecidoDelNombre.Font = null;
      this.miTextoParecidoDelNombre.Name = "miTextoParecidoDelNombre";
      // 
      // miBarraDeParecidoDeNombre
      // 
      this.miBarraDeParecidoDeNombre.AccessibleDescription = null;
      this.miBarraDeParecidoDeNombre.AccessibleName = null;
      resources.ApplyResources(this.miBarraDeParecidoDeNombre, "miBarraDeParecidoDeNombre");
      this.miBarraDeParecidoDeNombre.BackColor = System.Drawing.SystemColors.Window;
      this.miBarraDeParecidoDeNombre.BackgroundImage = null;
      this.miBarraDeParecidoDeNombre.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default, "DistanciaHammingBuscarPdisDuplicados", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.miBarraDeParecidoDeNombre.Font = null;
      this.miBarraDeParecidoDeNombre.LargeChange = 2;
      this.miBarraDeParecidoDeNombre.Maximum = 6;
      this.miBarraDeParecidoDeNombre.Name = "miBarraDeParecidoDeNombre";
      this.miBarraDeParecidoDeNombre.Value = global::GpsYv.ManejadorDeMapa.Interfase.Properties.Settings.Default.DistanciaHammingBuscarPdisDuplicados;
      this.miBarraDeParecidoDeNombre.ValueChanged += new System.EventHandler(this.EnCambioBarraDeParecidoDelNombre);
      // 
      // groupBox1
      // 
      this.groupBox1.AccessibleDescription = null;
      this.groupBox1.AccessibleName = null;
      resources.ApplyResources(this.groupBox1, "groupBox1");
      this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
      this.groupBox1.BackgroundImage = null;
      this.groupBox1.Controls.Add(this.miTextoDistancia);
      this.groupBox1.Controls.Add(this.miBarraDeDistancia);
      this.groupBox1.Font = null;
      this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      // 
      // miTextoDistancia
      // 
      this.miTextoDistancia.AccessibleDescription = null;
      this.miTextoDistancia.AccessibleName = null;
      resources.ApplyResources(this.miTextoDistancia, "miTextoDistancia");
      this.miTextoDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miTextoDistancia.Font = null;
      this.miTextoDistancia.Name = "miTextoDistancia";
      // 
      // InterfaseDePdisDuplicados
      // 
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.Controls.Add(this.miRecipienteDividido);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel3);
      this.Font = null;
      this.Name = "InterfaseDePdisDuplicados";
      this.miRecipienteDividido.Panel1.ResumeLayout(false);
      this.miRecipienteDividido.Panel2.ResumeLayout(false);
      this.miRecipienteDividido.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeDistancia)).EndInit();
      this.panel3.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miBarraDeParecidoDeNombre)).EndInit();
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView miLista;
    private System.Windows.Forms.ColumnHeader miColumnaNúmeroDeElemento;
    private System.Windows.Forms.ColumnHeader miColumnaTipo;
    private System.Windows.Forms.ColumnHeader miColumnaNombre;
    private System.Windows.Forms.SplitContainer miRecipienteDividido;
    private System.Windows.Forms.ColumnHeader miColumnaCoordenadas;
    private System.Windows.Forms.ColumnHeader miColumnaDistancia;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label miTextoNumeroDePdisSelecionados;
    private System.Windows.Forms.Button miBotonEliminarPdis;
    private InterfaseMapa miMapa;
    private System.Windows.Forms.ColumnHeader miColumnaDescripción;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.TrackBar miBarraDeDistancia;
    private System.Windows.Forms.TrackBar miBarraDeParecidoDeNombre;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button miBotónBuscaDuplicados;
    private System.Windows.Forms.Label miTextoParecidoDelNombre;
    private System.Windows.Forms.Label miTextoDistancia;
    private MenuEditorDePdis miMenuEditorDePdis;

  }
}
