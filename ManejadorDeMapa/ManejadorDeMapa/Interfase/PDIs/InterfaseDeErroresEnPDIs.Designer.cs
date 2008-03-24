namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class InterfaseDeErroresEnPDIs
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
      System.Windows.Forms.ColumnHeader columnaCoordenadas;
      System.Windows.Forms.ColumnHeader columnaRazón;
      System.Windows.Forms.SplitContainer division;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDeErroresEnPDIs));
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
      this.miMenúEditorDePDI = new GpsYv.ManejadorDeMapa.Interfase.PDIs.MenuEditorDePDI();
      this.miMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
      columnaCoordenadas = new System.Windows.Forms.ColumnHeader();
      columnaRazón = new System.Windows.Forms.ColumnHeader();
      division = new System.Windows.Forms.SplitContainer();
      division.Panel1.SuspendLayout();
      division.Panel2.SuspendLayout();
      division.SuspendLayout();
      this.SuspendLayout();
      // 
      // columnaCoordenadas
      // 
      columnaCoordenadas.Text = "Coordenadas";
      columnaCoordenadas.Width = 169;
      // 
      // columnaRazón
      // 
      columnaRazón.Text = "Razón";
      columnaRazón.Width = 284;
      // 
      // division
      // 
      division.Dock = System.Windows.Forms.DockStyle.Fill;
      division.Location = new System.Drawing.Point(0, 0);
      division.Name = "division";
      division.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // division.Panel1
      // 
      division.Panel1.Controls.Add(this.miLista);
      // 
      // division.Panel2
      // 
      division.Panel2.Controls.Add(this.miMapa);
      division.Size = new System.Drawing.Size(837, 501);
      division.SplitterDistance = 299;
      division.TabIndex = 3;
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaCoordenadas,
            columnaRazón});
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
      this.miLista.VirtualMode = true;
      this.miLista.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EnClick);
      // 
      // miMenúEditorDePDI
      // 
      this.miMenúEditorDePDI.Enabled = false;
      this.miMenúEditorDePDI.ManejadorDePDIs = null;
      this.miMenúEditorDePDI.Name = "miMenuDeContexto";
      this.miMenúEditorDePDI.PDI = null;
      this.miMenúEditorDePDI.ShowImageMargin = false;
      this.miMenúEditorDePDI.Size = new System.Drawing.Size(122, 26);
      this.miMenúEditorDePDI.Text = "MenúEditorDePDI";
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
      this.miMapa.MuestraVías = false;
      this.miMapa.Name = "miMapa";
      this.miMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapa.RectánguloVisibleEnCoordenadas")));
      this.miMapa.Size = new System.Drawing.Size(837, 198);
      this.miMapa.TabIndex = 0;
      // 
      // InterfaseDeErroresEnPDIs
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(division);
      this.Name = "InterfaseDeErroresEnPDIs";
      this.Size = new System.Drawing.Size(837, 501);
      division.Panel1.ResumeLayout(false);
      division.Panel2.ResumeLayout(false);
      division.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
    private MenuEditorDePDI miMenúEditorDePDI;
    private InterfaseMapa miMapa;
  }
}
