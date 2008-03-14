namespace GpsYv.ManejadorDeMapa.Interface.PDIs
{
  partial class InterfaceDeModificados
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
      this.miLista = new System.Windows.Forms.ListView();
      this.miColumnaNúmeroDeElemento = new System.Windows.Forms.ColumnHeader();
      this.miColumnaTipo = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombreOriginal = new System.Windows.Forms.ColumnHeader();
      this.miColumnaNombreFinal = new System.Windows.Forms.ColumnHeader();
      this.miColumnaDescripción = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // miLista
      // 
      this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaNúmeroDeElemento,
            this.miColumnaTipo,
            this.miColumnaDescripción,
            this.miColumnaNombreOriginal,
            this.miColumnaNombreFinal});
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(603, 484);
      this.miLista.TabIndex = 1;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miColumnaNúmeroDeElemento
      // 
      this.miColumnaNúmeroDeElemento.Text = "#";
      // 
      // miColumnaTipo
      // 
      this.miColumnaTipo.Text = "Tipo";
      // 
      // miColumnaNombreOriginal
      // 
      this.miColumnaNombreOriginal.Text = "Nombre Original";
      this.miColumnaNombreOriginal.Width = 319;
      // 
      // miColumnaNombreFinal
      // 
      this.miColumnaNombreFinal.Text = "Nombre Final";
      this.miColumnaNombreFinal.Width = 284;
      // 
      // miColumnaDescripción
      // 
      this.miColumnaDescripción.Text = "Descripción";
      this.miColumnaDescripción.Width = 130;
      // 
      // PDIsModificados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miLista);
      this.Name = "PDIsModificados";
      this.Size = new System.Drawing.Size(603, 484);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView miLista;
    private System.Windows.Forms.ColumnHeader miColumnaNúmeroDeElemento;
    private System.Windows.Forms.ColumnHeader miColumnaTipo;
    private System.Windows.Forms.ColumnHeader miColumnaNombreOriginal;
    private System.Windows.Forms.ColumnHeader miColumnaNombreFinal;
    private System.Windows.Forms.ColumnHeader miColumnaDescripción;
  }
}
