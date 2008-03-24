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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDePDIs));
      System.Windows.Forms.ColumnHeader columnaLatitud;
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
      this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
      this.miPáginaMapa.Location = new System.Drawing.Point(4, 22);
      this.miPáginaMapa.Name = "miPáginaMapa";
      this.miPáginaMapa.Size = new System.Drawing.Size(583, 405);
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
      this.miInterfaseDeMapa.MuestraPDIs = true;
      this.miInterfaseDeMapa.MuestraPolígonos = false;
      this.miInterfaseDeMapa.MuestraPolilíneas = false;
      this.miInterfaseDeMapa.MuestraTodoElMapa = true;
      this.miInterfaseDeMapa.MuestraTodosLosElementos = false;
      this.miInterfaseDeMapa.MuestraVías = false;
      this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
      this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
      this.miInterfaseDeMapa.Size = new System.Drawing.Size(583, 405);
      this.miInterfaseDeMapa.TabIndex = 0;
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
            columnaLatitud,
            columnaLongitud});
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
      // 
      // columnaLatitud
      // 
      columnaLatitud.Text = "Latitud";
      columnaLatitud.Width = 75;
      // 
      // columnaLongitud
      // 
      columnaLongitud.Text = "Longitud";
      // 
      // miPáginaModificados
      // 
      this.miPáginaModificados.Controls.Add(this.miInterfasePDIsModificados);
      this.miPáginaModificados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaModificados.Name = "miPáginaModificados";
      this.miPáginaModificados.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaModificados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaModificados.TabIndex = 1;
      this.miPáginaModificados.Text = "Modificados";
      this.miPáginaModificados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsModificados
      // 
      this.miInterfasePDIsModificados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfasePDIsModificados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsModificados.Location = new System.Drawing.Point(3, 3);
      this.miInterfasePDIsModificados.ManejadorDeMapa = null;
      this.miInterfasePDIsModificados.Name = "miInterfasePDIsModificados";
      this.miInterfasePDIsModificados.Size = new System.Drawing.Size(577, 399);
      this.miInterfasePDIsModificados.TabIndex = 0;
      // 
      // miPáginaPosiblesDuplicados
      // 
      this.miPáginaPosiblesDuplicados.BackColor = System.Drawing.Color.Transparent;
      this.miPáginaPosiblesDuplicados.Controls.Add(this.miInterfasePDIsDuplicados);
      this.miPáginaPosiblesDuplicados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaPosiblesDuplicados.Name = "miPáginaPosiblesDuplicados";
      this.miPáginaPosiblesDuplicados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaPosiblesDuplicados.TabIndex = 2;
      this.miPáginaPosiblesDuplicados.Text = "Posible Duplicados";
      this.miPáginaPosiblesDuplicados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsDuplicados
      // 
      this.miInterfasePDIsDuplicados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfasePDIsDuplicados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsDuplicados.Location = new System.Drawing.Point(0, 0);
      this.miInterfasePDIsDuplicados.ManejadorDeMapa = null;
      this.miInterfasePDIsDuplicados.Name = "miInterfasePDIsDuplicados";
      this.miInterfasePDIsDuplicados.Size = new System.Drawing.Size(583, 405);
      this.miInterfasePDIsDuplicados.TabIndex = 0;
      // 
      // miPáginaEliminados
      // 
      this.miPáginaEliminados.Controls.Add(this.miInterfasePDIsEliminados);
      this.miPáginaEliminados.Location = new System.Drawing.Point(4, 22);
      this.miPáginaEliminados.Name = "miPáginaEliminados";
      this.miPáginaEliminados.Size = new System.Drawing.Size(583, 405);
      this.miPáginaEliminados.TabIndex = 3;
      this.miPáginaEliminados.Text = "Eliminados";
      this.miPáginaEliminados.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsEliminados
      // 
      this.miInterfasePDIsEliminados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfasePDIsEliminados.EscuchadorDeEstatus = null;
      this.miInterfasePDIsEliminados.Location = new System.Drawing.Point(0, 0);
      this.miInterfasePDIsEliminados.ManejadorDeMapa = null;
      this.miInterfasePDIsEliminados.Name = "miInterfasePDIsEliminados";
      this.miInterfasePDIsEliminados.Size = new System.Drawing.Size(583, 405);
      this.miInterfasePDIsEliminados.TabIndex = 0;
      // 
      // miPáginaErrores
      // 
      this.miPáginaErrores.Controls.Add(this.miInterfasePDIsErrores);
      this.miPáginaErrores.Location = new System.Drawing.Point(4, 22);
      this.miPáginaErrores.Name = "miPáginaErrores";
      this.miPáginaErrores.Padding = new System.Windows.Forms.Padding(3);
      this.miPáginaErrores.Size = new System.Drawing.Size(583, 405);
      this.miPáginaErrores.TabIndex = 5;
      this.miPáginaErrores.Text = "Errores";
      this.miPáginaErrores.UseVisualStyleBackColor = true;
      // 
      // miInterfasePDIsErrores
      // 
      this.miInterfasePDIsErrores.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfasePDIsErrores.EscuchadorDeEstatus = null;
      this.miInterfasePDIsErrores.Location = new System.Drawing.Point(3, 3);
      this.miInterfasePDIsErrores.ManejadorDeMapa = null;
      this.miInterfasePDIsErrores.Name = "miInterfasePDIsErrores";
      this.miInterfasePDIsErrores.Size = new System.Drawing.Size(577, 399);
      this.miInterfasePDIsErrores.TabIndex = 2;
      // 
      // InterfaseManejadorDePDIs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miControladorDePestañasDePDIs);
      this.Name = "InterfaseManejadorDePDIs";
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
