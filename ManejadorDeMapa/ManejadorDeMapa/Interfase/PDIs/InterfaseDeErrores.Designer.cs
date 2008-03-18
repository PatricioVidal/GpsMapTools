namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseDeErrores
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDeErrores));
      this.miLista = new System.Windows.Forms.ListView();
      this.miColumnaNúmeroDeElemento = new System.Windows.Forms.ColumnHeader();
      this.miColumnaTipo = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDescripción = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombre = new System.Windows.Forms.ColumnHeader();
      this.miColumnaCoordenadas = new System.Windows.Forms.ColumnHeader();
      this.miColumnaRazón = new System.Windows.Forms.ColumnHeader();
      this.miMenúEditorDePDI = new GpsYv.ManejadorDeMapa.Interfase.PDIs.MenuEditorDePDI();
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      this.miDivision.Panel1.SuspendLayout();
      this.miDivision.Panel2.SuspendLayout();
      this.miDivision.SuspendLayout();
      this.SuspendLayout();
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaNúmeroDeElemento,
            this.miColumnaTipo,
            this.miColumnaDescripción,
            this.miColumnaNombre,
            this.miColumnaCoordenadas,
            this.miColumnaRazón});
      this.miLista.ContextMenuStrip = this.miMenúEditorDePDI;
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(837, 299);
      this.miLista.TabIndex = 2;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EnClick);
      // 
      // miColumnaNúmeroDeElemento
      // 
      this.miColumnaNúmeroDeElemento.Text = "#";
      this.miColumnaNúmeroDeElemento.Width = 45;
      // 
      // miColumnaTipo
      // 
      this.miColumnaTipo.Text = "Tipo";
      // 
      // miColumnaDescripción
      // 
      this.miColumnaDescripción.Text = "Descripción";
      this.miColumnaDescripción.Width = 130;
      // 
      // miColumnaNombre
      // 
      this.miColumnaNombre.Text = "Nombre";
      this.miColumnaNombre.Width = 250;
      // 
      // miColumnaCoordenadas
      // 
      this.miColumnaCoordenadas.Text = "Coordenadas";
      this.miColumnaCoordenadas.Width = 169;
      // 
      // miColumnaRazón
      // 
      this.miColumnaRazón.Text = "Razón";
      this.miColumnaRazón.Width = 284;
      // 
      // miMenúEditorDePDI
      // 
      this.miMenúEditorDePDI.Enabled = false;
      this.miMenúEditorDePDI.ManejadorDePDIs = null;
      this.miMenúEditorDePDI.Name = "miMenuDeContexto";
      this.miMenúEditorDePDI.PDI = null;
      this.miMenúEditorDePDI.ShowImageMargin = false;
      this.miMenúEditorDePDI.Size = new System.Drawing.Size(128, 48);
      this.miMenúEditorDePDI.Text = "MenúEditorDePDI";
      // 
      // miDivision
      // 
      this.miDivision.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miDivision.Location = new System.Drawing.Point(0, 0);
      this.miDivision.Name = "miDivision";
      this.miDivision.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // miDivision.Panel1
      // 
      this.miDivision.Panel1.Controls.Add(this.miLista);
      // 
      // miDivision.Panel2
      // 
      this.miDivision.Panel2.Controls.Add(this.miMapa);
      this.miDivision.Size = new System.Drawing.Size(837, 501);
      this.miDivision.SplitterDistance = 299;
      this.miDivision.TabIndex = 3;
      // 
      // miMapa
      // 
      this.miMapa.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miMapa.EscuchadorDeEstatus = null;
      this.miMapa.Location = new System.Drawing.Point(0, 0);
      this.miMapa.ManejadorDeMapa = null;
      this.miMapa.MuestraPDIs = false;
      this.miMapa.MuestraPolígonos = false;
      this.miMapa.MuestraPolilíneas = false;
      this.miMapa.MuestraTodoElMapa = true;
      this.miMapa.MuestraTodosLosElementos = true;
      this.miMapa.Name = "miMapa";
      this.miMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapa.RectánguloVisibleEnCoordenadas")));
      this.miMapa.Size = new System.Drawing.Size(837, 198);
      this.miMapa.TabIndex = 0;
      // 
      // InterfaseDeErrores
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miDivision);
      this.Name = "InterfaseDeErrores";
      this.Size = new System.Drawing.Size(837, 501);
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView miLista;
    private System.Windows.Forms.ColumnHeader miColumnaNúmeroDeElemento;
    private System.Windows.Forms.ColumnHeader miColumnaTipo;
    private System.Windows.Forms.ColumnHeader miColumnaDescripción;
    private System.Windows.Forms.ColumnHeader miColumnaNombre;
    private System.Windows.Forms.ColumnHeader miColumnaRazón;
    private System.Windows.Forms.ColumnHeader miColumnaCoordenadas;
    private System.Windows.Forms.SplitContainer miDivision;
    private InterfaseMapa miMapa;
    private MenuEditorDePDI miMenúEditorDePDI;
  }
}
