namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseDePDIsDuplicados
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDePDIsDuplicados));
      GpsYv.ManejadorDeMapa.Interfase.OrdenadorDeColumnaDeLista ordenadorDeColumnaDeLista;
      this.miLista = new System.Windows.Forms.ListView();
      this.miColumnaNúmeroDeElemento = new System.Windows.Forms.ColumnHeader();
      this.miColumnaTipo = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDescripción = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombre = new System.Windows.Forms.ColumnHeader();
      this.miColumnaCoordenadas = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDistancia = new System.Windows.Forms.ColumnHeader();
      this.miRecipienteDividido = new System.Windows.Forms.SplitContainer();
      this.miMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.panel1 = new System.Windows.Forms.Panel();
      this.miTextoNumeroDePDIsSelecionados = new System.Windows.Forms.Label();
      this.miBotonEliminarPDIs = new System.Windows.Forms.Button();
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
      // miLista
      // 
      this.miLista.CheckBoxes = true;
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaNúmeroDeElemento,
            this.miColumnaTipo,
            this.miColumnaDescripción,
            this.miColumnaNombre,
            this.miColumnaCoordenadas,
            this.miColumnaDistancia});
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(607, 190);
      this.miLista.TabIndex = 2;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.EnItemSeleccionado);
      this.miLista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EnClick);
      // 
      // miColumnaNúmeroDeElemento
      // 
      this.miColumnaNúmeroDeElemento.Text = "#";
      // 
      // miColumnaTipo
      // 
      this.miColumnaTipo.Text = "Tipo";
      this.miColumnaTipo.Width = 59;
      // 
      // miColumnaDescripción
      // 
      this.miColumnaDescripción.Text = "Descripción";
      this.miColumnaDescripción.Width = 130;
      // 
      // miColumnaNombre
      // 
      this.miColumnaNombre.Text = "Nombre";
      this.miColumnaNombre.Width = 211;
      // 
      // miColumnaCoordenadas
      // 
      this.miColumnaCoordenadas.Text = "Coordenadas";
      this.miColumnaCoordenadas.Width = 156;
      // 
      // miColumnaDistancia
      // 
      this.miColumnaDistancia.Text = "Distancia (m)";
      this.miColumnaDistancia.Width = 84;
      // 
      // miRecipienteDividido
      // 
      this.miRecipienteDividido.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miRecipienteDividido.Location = new System.Drawing.Point(0, 62);
      this.miRecipienteDividido.Name = "miRecipienteDividido";
      this.miRecipienteDividido.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // miRecipienteDividido.Panel1
      // 
      this.miRecipienteDividido.Panel1.Controls.Add(this.miLista);
      // 
      // miRecipienteDividido.Panel2
      // 
      this.miRecipienteDividido.Panel2.Controls.Add(this.miMapa);
      this.miRecipienteDividido.Size = new System.Drawing.Size(607, 358);
      this.miRecipienteDividido.SplitterDistance = 190;
      this.miRecipienteDividido.TabIndex = 4;
      // 
      // miMapa
      // 
      this.miMapa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miMapa.EscuchadorDeEstatus = null;
      this.miMapa.Location = new System.Drawing.Point(0, 0);
      this.miMapa.ManejadorDeMapa = null;
      this.miMapa.MuestraPDIs = true;
      this.miMapa.MuestraPolígonos = false;
      this.miMapa.MuestraPolilíneas = false;
      this.miMapa.MuestraTodoElMapa = true;
      this.miMapa.MuestraTodosLosElementos = true;
      this.miMapa.MuestraVías = false;
      this.miMapa.Name = "miMapa";
      this.miMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapa.RectánguloVisibleEnCoordenadas")));
      this.miMapa.Size = new System.Drawing.Size(607, 164);
      this.miMapa.TabIndex = 0;
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.miTextoNumeroDePDIsSelecionados);
      this.panel1.Controls.Add(this.miBotonEliminarPDIs);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.panel1.Location = new System.Drawing.Point(0, 420);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(607, 30);
      this.panel1.TabIndex = 4;
      // 
      // miTextoNumeroDePDIsSelecionados
      // 
      this.miTextoNumeroDePDIsSelecionados.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.miTextoNumeroDePDIsSelecionados.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.miTextoNumeroDePDIsSelecionados.Location = new System.Drawing.Point(110, 4);
      this.miTextoNumeroDePDIsSelecionados.Name = "miTextoNumeroDePDIsSelecionados";
      this.miTextoNumeroDePDIsSelecionados.Size = new System.Drawing.Size(259, 23);
      this.miTextoNumeroDePDIsSelecionados.TabIndex = 1;
      this.miTextoNumeroDePDIsSelecionados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // miBotonEliminarPDIs
      // 
      this.miBotonEliminarPDIs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.miBotonEliminarPDIs.Enabled = false;
      this.miBotonEliminarPDIs.Location = new System.Drawing.Point(375, 4);
      this.miBotonEliminarPDIs.Name = "miBotonEliminarPDIs";
      this.miBotonEliminarPDIs.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.miBotonEliminarPDIs.Size = new System.Drawing.Size(229, 23);
      this.miBotonEliminarPDIs.TabIndex = 0;
      this.miBotonEliminarPDIs.Text = "Eliminar PDI(s) Seleccionados";
      this.miBotonEliminarPDIs.UseVisualStyleBackColor = true;
      this.miBotonEliminarPDIs.Click += new System.EventHandler(this.EnBotónEliminarPDIs);
      // 
      // miBarraDeDistancia
      // 
      this.miBarraDeDistancia.AutoSize = false;
      this.miBarraDeDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miBarraDeDistancia.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::GpsYv.ManejadorDeMapa.Properties.Settings.Default, "DistanciaMáximaEnDecenasDeMetrosParaBuscarPDIsDuplicados", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.miBarraDeDistancia.LargeChange = 20;
      this.miBarraDeDistancia.Location = new System.Drawing.Point(6, 18);
      this.miBarraDeDistancia.Maximum = 20;
      this.miBarraDeDistancia.Minimum = 1;
      this.miBarraDeDistancia.Name = "miBarraDeDistancia";
      this.miBarraDeDistancia.Size = new System.Drawing.Size(153, 32);
      this.miBarraDeDistancia.TabIndex = 2;
      this.miBarraDeDistancia.Value = global::GpsYv.ManejadorDeMapa.Properties.Settings.Default.DistanciaMáximaEnDecenasDeMetrosParaBuscarPDIsDuplicados;
      this.miBarraDeDistancia.ValueChanged += new System.EventHandler(this.EnCambióBarraDeDistancia);
      // 
      // panel3
      // 
      this.panel3.Controls.Add(this.miBotónBuscaDuplicados);
      this.panel3.Controls.Add(this.groupBox2);
      this.panel3.Controls.Add(this.groupBox1);
      this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel3.Location = new System.Drawing.Point(0, 0);
      this.panel3.Name = "panel3";
      this.panel3.Size = new System.Drawing.Size(607, 62);
      this.panel3.TabIndex = 3;
      // 
      // miBotónBuscaDuplicados
      // 
      this.miBotónBuscaDuplicados.Location = new System.Drawing.Point(477, 21);
      this.miBotónBuscaDuplicados.Name = "miBotónBuscaDuplicados";
      this.miBotónBuscaDuplicados.Size = new System.Drawing.Size(118, 23);
      this.miBotónBuscaDuplicados.TabIndex = 9;
      this.miBotónBuscaDuplicados.Text = "Busca Duplicados";
      this.miBotónBuscaDuplicados.UseVisualStyleBackColor = true;
      this.miBotónBuscaDuplicados.Click += new System.EventHandler(this.EnBotónBuscaDuplicados);
      // 
      // groupBox2
      // 
      this.groupBox2.BackColor = System.Drawing.SystemColors.Window;
      this.groupBox2.Controls.Add(this.miTextoParecidoDelNombre);
      this.groupBox2.Controls.Add(this.miBarraDeParecidoDeNombre);
      this.groupBox2.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox2.Location = new System.Drawing.Point(224, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(247, 56);
      this.groupBox2.TabIndex = 8;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Parecido del Nombre (Hamming)";
      // 
      // miTextoParecidoDelNombre
      // 
      this.miTextoParecidoDelNombre.AutoSize = true;
      this.miTextoParecidoDelNombre.BackColor = System.Drawing.SystemColors.Window;
      this.miTextoParecidoDelNombre.Location = new System.Drawing.Point(149, 18);
      this.miTextoParecidoDelNombre.Name = "miTextoParecidoDelNombre";
      this.miTextoParecidoDelNombre.Size = new System.Drawing.Size(13, 13);
      this.miTextoParecidoDelNombre.TabIndex = 3;
      this.miTextoParecidoDelNombre.Text = "3";
      // 
      // miBarraDeParecidoDeNombre
      // 
      this.miBarraDeParecidoDeNombre.AutoSize = false;
      this.miBarraDeParecidoDeNombre.BackColor = System.Drawing.SystemColors.Window;
      this.miBarraDeParecidoDeNombre.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::GpsYv.ManejadorDeMapa.Properties.Settings.Default, "DistanciaHammingBuscarPDIsDuplicados", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.miBarraDeParecidoDeNombre.LargeChange = 2;
      this.miBarraDeParecidoDeNombre.Location = new System.Drawing.Point(6, 18);
      this.miBarraDeParecidoDeNombre.Maximum = 7;
      this.miBarraDeParecidoDeNombre.Name = "miBarraDeParecidoDeNombre";
      this.miBarraDeParecidoDeNombre.Size = new System.Drawing.Size(137, 32);
      this.miBarraDeParecidoDeNombre.TabIndex = 3;
      this.miBarraDeParecidoDeNombre.Value = global::GpsYv.ManejadorDeMapa.Properties.Settings.Default.DistanciaHammingBuscarPDIsDuplicados;
      this.miBarraDeParecidoDeNombre.ValueChanged += new System.EventHandler(this.EnCambioBarraDeParecidoDelNombre);
      // 
      // groupBox1
      // 
      this.groupBox1.BackColor = System.Drawing.SystemColors.Window;
      this.groupBox1.Controls.Add(this.miTextoDistancia);
      this.groupBox1.Controls.Add(this.miBarraDeDistancia);
      this.groupBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(215, 56);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Distancia Máxima";
      // 
      // miTextoDistancia
      // 
      this.miTextoDistancia.AutoSize = true;
      this.miTextoDistancia.BackColor = System.Drawing.SystemColors.Window;
      this.miTextoDistancia.Location = new System.Drawing.Point(165, 18);
      this.miTextoDistancia.Name = "miTextoDistancia";
      this.miTextoDistancia.Size = new System.Drawing.Size(24, 13);
      this.miTextoDistancia.TabIndex = 1;
      this.miTextoDistancia.Text = "0 m";
      // 
      // ordenadorDeColumnaDeLista
      // 
      ordenadorDeColumnaDeLista.Lista = this.miLista;
      // 
      // InterfaseDePDIsDuplicados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miRecipienteDividido);
      this.Controls.Add(this.panel1);
      this.Controls.Add(this.panel3);
      this.Name = "InterfaseDePDIsDuplicados";
      this.Size = new System.Drawing.Size(607, 450);
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
    private System.Windows.Forms.Label miTextoNumeroDePDIsSelecionados;
    private System.Windows.Forms.Button miBotonEliminarPDIs;
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

  }
}
