namespace GpsYv.ManejadorDeMapa.Interface.PDIs
{
  partial class InterfaceManejador
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaceManejador));
      this.miControladorDePestañasDePDIs = new System.Windows.Forms.TabControl();
      this.miPáginaMapa = new System.Windows.Forms.TabPage();
      this.miInterfaceDeMapa = new GpsYv.ManejadorDeMapa.Interface.InterfaceMapa();
      this.miPáginaDeTodos = new System.Windows.Forms.TabPage();
      this.miLista = new System.Windows.Forms.ListView();
      this.miColumnaNúmero = new System.Windows.Forms.ColumnHeader();
      this.miColumnaTipo = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDescripción = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombre = new System.Windows.Forms.ColumnHeader();
      this.miColumnaLatitud = new System.Windows.Forms.ColumnHeader();
      this.miColumnaLongitud = new System.Windows.Forms.ColumnHeader();
      this.miPáginaModificados = new System.Windows.Forms.TabPage();
      this.miInterfacePDIsModificados = new GpsYv.ManejadorDeMapa.Interface.PDIs.InterfaceDeModificados();
      this.miPáginaPosiblesDuplicados = new System.Windows.Forms.TabPage();
      this.miInterfacePDIsDuplicados = new GpsYv.ManejadorDeMapa.Interface.PDIs.InterfaceDeDuplicados();
      this.miPáginaEliminados = new System.Windows.Forms.TabPage();
      this.miInterfacePDIsEliminados = new GpsYv.ManejadorDeMapa.Interface.PDIs.InterfaceDeEliminados();
      this.miPáginaErrores = new System.Windows.Forms.TabPage();
      this.miInterfacePDIsErrores = new GpsYv.ManejadorDeMapa.Interface.PDIs.InterfaceDeErrores();
      this.miControladorDePestañasDePDIs.SuspendLayout();
      this.miPáginaMapa.SuspendLayout();
      this.miPáginaDeTodos.SuspendLayout();
      this.miPáginaModificados.SuspendLayout();
      this.miPáginaPosiblesDuplicados.SuspendLayout();
      this.miPáginaEliminados.SuspendLayout();
      this.miPáginaErrores.SuspendLayout();
      this.SuspendLayout();
      // 
      // miControladorDePestañasDePDIs
      // 
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaMapa);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaDeTodos);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaModificados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaPosiblesDuplicados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaEliminados);
      this.miControladorDePestañasDePDIs.Controls.Add(this.miPáginaErrores);
      this.miControladorDePestañasDePDIs.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miControladorDePestañasDePDIs.Location = new System.Drawing.Point(0, 0);
      this.miControladorDePestañasDePDIs.Name = "miControladorDePestañasDePDIs";
      this.miControladorDePestañasDePDIs.SelectedIndex = 0;
      this.miControladorDePestañasDePDIs.Size = new System.Drawing.Size(591, 431);
      this.miControladorDePestañasDePDIs.TabIndex = 4;
      // 
      // miPáginaMapa
      // 
      this.miPáginaMapa.Controls.Add(this.miInterfaceDeMapa);
      this.miPáginaMapa.Location = new System.Drawing.Point(4, 22);
      this.miPáginaMapa.Name = "miPáginaMapa";
      this.miPáginaMapa.Size = new System.Drawing.Size(583, 405);
      this.miPáginaMapa.TabIndex = 4;
      this.miPáginaMapa.Text = "Mapa";
      this.miPáginaMapa.UseVisualStyleBackColor = true;
      // 
      // miInterfaceDeMapa
      // 
      this.miInterfaceDeMapa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaceDeMapa.EscuchadorDeEstatus = null;
      this.miInterfaceDeMapa.Location = new System.Drawing.Point(0, 0);
      this.miInterfaceDeMapa.ManejadorDeMapa = null;
      this.miInterfaceDeMapa.MuestraPDIs = true;
      this.miInterfaceDeMapa.MuestraPolígonos = false;
      this.miInterfaceDeMapa.MuestraPolilíneas = false;
      this.miInterfaceDeMapa.MuestraTodoElMapa = true;
      this.miInterfaceDeMapa.MuestraTodosLosElementos = false;
      this.miInterfaceDeMapa.Name = "miInterfaceDeMapa";
      this.miInterfaceDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaceDeMapa.RectánguloVisibleEnCoordenadas")));
      this.miInterfaceDeMapa.Size = new System.Drawing.Size(583, 405);
      this.miInterfaceDeMapa.TabIndex = 0;
      // 
      // miPáginaDeTodos
      // 
      this.miPáginaDeTodos.Controls.Add(this.miLista);
      this.miPáginaDeTodos.Location = new System.Drawing.Point(4, 22);
      this.miPáginaDeTodos.Name = "miPáginaDeTodos";
      this.miPáginaDeTodos.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaDeTodos.Size = new System.Drawing.Size(583, 405);
      this.miPáginaDeTodos.TabIndex = 0;
      this.miPáginaDeTodos.Text = "Todos";
      this.miPáginaDeTodos.UseVisualStyleBackColor = true;
      // 
      // miLista
      // 
      this.miLista.Activation = System.Windows.Forms.ItemActivation.OneClick;
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaNúmero,
            this.miColumnaTipo,
            this.miColumnaDescripción,
            this.miColumnaNombre,
            this.miColumnaLatitud,
            this.miColumnaLongitud});
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(3, 3);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(577, 399);
      this.miLista.TabIndex = 2;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      this.miLista.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ObtieneItemDeListaDePDIs);
      // 
      // miColumnaNúmero
      // 
      this.miColumnaNúmero.Text = "#";
      this.miColumnaNúmero.Width = 56;
      // 
      // miColumnaTipo
      // 
      this.miColumnaTipo.Text = "Tipo";
      this.miColumnaTipo.Width = 66;
      // 
      // miColumnaDescripción
      // 
      this.miColumnaDescripción.Text = "Descripción";
      this.miColumnaDescripción.Width = 130;
      // 
      // miColumnaNombre
      // 
      this.miColumnaNombre.Text = "Nombre";
      this.miColumnaNombre.Width = 306;
      // 
      // miColumnaLatitud
      // 
      this.miColumnaLatitud.Text = "Latitud";
      this.miColumnaLatitud.Width = 75;
      // 
      // miColumnaLongitud
      // 
      this.miColumnaLongitud.Text = "Longitud";
      // 
      // miPáginaModificados
      // 
      this.miPáginaModificados.Controls.Add(this.miInterfacePDIsModificados);
      this.miPáginaModificados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaModificados.Name = "miPáginaModificados";
      this.miPáginaModificados.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaModificados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaModificados.TabIndex = 1;
      this.miPáginaModificados.Text = "Modificados";
      this.miPáginaModificados.UseVisualStyleBackColor = true;
      // 
      // miInterfacePDIsModificados
      // 
      this.miInterfacePDIsModificados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfacePDIsModificados.EscuchadorDeEstatus = null;
      this.miInterfacePDIsModificados.Location = new System.Drawing.Point(3, 3);
      this.miInterfacePDIsModificados.ManejadorDeMapa = null;
      this.miInterfacePDIsModificados.Name = "miInterfacePDIsModificados";
      this.miInterfacePDIsModificados.Size = new System.Drawing.Size(577, 399);
      this.miInterfacePDIsModificados.TabIndex = 0;
      // 
      // miPáginaPosiblesDuplicados
      // 
      this.miPáginaPosiblesDuplicados.BackColor = System.Drawing.Color.Transparent;
      this.miPáginaPosiblesDuplicados.Controls.Add(this.miInterfacePDIsDuplicados);
      this.miPáginaPosiblesDuplicados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaPosiblesDuplicados.Name = "miPáginaPosiblesDuplicados";
      this.miPáginaPosiblesDuplicados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaPosiblesDuplicados.TabIndex = 2;
      this.miPáginaPosiblesDuplicados.Text = "Posible Duplicados";
      this.miPáginaPosiblesDuplicados.UseVisualStyleBackColor = true;
      // 
      // miInterfacePDIsDuplicados
      // 
      this.miInterfacePDIsDuplicados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfacePDIsDuplicados.EscuchadorDeEstatus = null;
      this.miInterfacePDIsDuplicados.Location = new System.Drawing.Point(0, 0);
      this.miInterfacePDIsDuplicados.ManejadorDeMapa = null;
      this.miInterfacePDIsDuplicados.Name = "miInterfacePDIsDuplicados";
      this.miInterfacePDIsDuplicados.Size = new System.Drawing.Size(583, 405);
      this.miInterfacePDIsDuplicados.TabIndex = 0;
      // 
      // miPáginaEliminados
      // 
      this.miPáginaEliminados.Controls.Add(this.miInterfacePDIsEliminados);
      this.miPáginaEliminados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaEliminados.Name = "miPáginaEliminados";
      this.miPáginaEliminados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaEliminados.TabIndex = 3;
      this.miPáginaEliminados.Text = "Eliminados";
      this.miPáginaEliminados.UseVisualStyleBackColor = true;
      // 
      // miInterfacePDIsEliminados
      // 
      this.miInterfacePDIsEliminados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfacePDIsEliminados.EscuchadorDeEstatus = null;
      this.miInterfacePDIsEliminados.Location = new System.Drawing.Point(0, 0);
      this.miInterfacePDIsEliminados.ManejadorDeMapa = null;
      this.miInterfacePDIsEliminados.Name = "miInterfacePDIsEliminados";
      this.miInterfacePDIsEliminados.Size = new System.Drawing.Size(583, 405);
      this.miInterfacePDIsEliminados.TabIndex = 0;
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfacePDIsErrores);
      this.miPáginaErrores.Location = new System.Drawing.Point(4, 22);
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaErrores.Size = new System.Drawing.Size(583, 405);
      this.miPáginaErrores.TabIndex = 5;
      this.miPáginaErrores.Text = "Errores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfacePDIsErrores
      // 
      this.miInterfacePDIsErrores.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfacePDIsErrores.EscuchadorDeEstatus = null;
      this.miInterfacePDIsErrores.Location = new System.Drawing.Point(3, 3);
      this.miInterfacePDIsErrores.ManejadorDeMapa = null;
      this.miInterfacePDIsErrores.Name = "miInterfacePDIsErrores";
      this.miInterfacePDIsErrores.Size = new System.Drawing.Size(577, 399);
      this.miInterfacePDIsErrores.TabIndex = 2;
      // 
      // InterfaceManejadorDePDIs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañasDePDIs);
      this.Name = "InterfaceManejadorDePDIs";
      this.Size = new System.Drawing.Size(591, 431);
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
    private System.Windows.Forms.ListView miLista;
    private System.Windows.Forms.ColumnHeader miColumnaNúmero;
    private System.Windows.Forms.ColumnHeader miColumnaNombre;
    private System.Windows.Forms.ColumnHeader miColumnaTipo;
    private System.Windows.Forms.ColumnHeader miColumnaLatitud;
    private System.Windows.Forms.ColumnHeader miColumnaLongitud;
    private System.Windows.Forms.TabPage miPáginaModificados;
    private InterfaceDeModificados miInterfacePDIsModificados;
    private System.Windows.Forms.TabPage miPáginaPosiblesDuplicados;
    private InterfaceDeDuplicados miInterfacePDIsDuplicados;
    private System.Windows.Forms.TabPage miPáginaEliminados;
    private InterfaceDeEliminados miInterfacePDIsEliminados;
    private System.Windows.Forms.TabPage miPáginaMapa;
    private InterfaceMapa miInterfaceDeMapa;
    private System.Windows.Forms.ColumnHeader miColumnaDescripción;
    private System.Windows.Forms.TabPage miPáginaErrores;
    private InterfaceDeErrores miInterfacePDIsErrores;
  }
}
