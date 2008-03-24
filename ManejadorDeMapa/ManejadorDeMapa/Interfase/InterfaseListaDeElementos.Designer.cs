namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class InterfaseListaDeElementos
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
      System.Windows.Forms.ColumnHeader columnaNúmero;
      System.Windows.Forms.ColumnHeader columnaTipo;
      System.Windows.Forms.ColumnHeader columnaDescripción;
      System.Windows.Forms.ColumnHeader columnaNombre;
      this.miOrdenadorDeColumnaDeLista = new GpsYv.ManejadorDeMapa.Interfase.OrdenadorDeColumnaDeLista(this.components);
      columnaNúmero = new System.Windows.Forms.ColumnHeader();
      columnaTipo = new System.Windows.Forms.ColumnHeader();
      columnaDescripción = new System.Windows.Forms.ColumnHeader();
      columnaNombre = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // columnaNúmero
      // 
      columnaNúmero.Text = "#";
      columnaNúmero.Width = 45;
      // 
      // columnaTipo
      // 
      columnaTipo.Text = "Tipo";
      // 
      // columnaDescripción
      // 
      columnaDescripción.Text = "Descripción";
      columnaDescripción.Width = 130;
      // 
      // columnaNombre
      // 
      columnaNombre.Text = "Nombre";
      columnaNombre.Width = 250;
      // 
      // miOrdenadorDeColumnaDeLista
      // 
      this.miOrdenadorDeColumnaDeLista.ItemsDeLaListaVirtual = null;
      this.miOrdenadorDeColumnaDeLista.Lista = this;
      // 
      // InterfaseListaDeElementos
      // 
      this.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnaNúmero,
            columnaTipo,
            columnaDescripción,
            columnaNombre});
      this.FullRowSelect = true;
      this.GridLines = true;
      this.Size = new System.Drawing.Size(513, 444);
      this.View = System.Windows.Forms.View.Details;
      this.VirtualMode = true;
      this.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ObtieneItemDeLista);
      this.ResumeLayout(false);

    }

    #endregion

    private OrdenadorDeColumnaDeLista miOrdenadorDeColumnaDeLista;

  }
}
