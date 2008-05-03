namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class InterfaseListaConMapaDePdis
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseListaConMapaDePdis));
      this.miDivision = new System.Windows.Forms.SplitContainer();
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseListaDePdis();
      this.miMenuEditorDePdis = new GpsYv.ManejadorDeMapa.Interfase.Pdis.MenuEditorDePdis();
      this.miMapaDePdisSeleccionados = new GpsYv.ManejadorDeMapa.Interfase.Pdis.InterfaseMapaDePdisSeleccionados();
      this.miDivision.Panel1.SuspendLayout();
      this.miDivision.Panel2.SuspendLayout();
      this.miDivision.SuspendLayout();
      this.SuspendLayout();
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
      this.miDivision.Panel2.Controls.Add(this.miMapaDePdisSeleccionados);
      this.miDivision.Size = new System.Drawing.Size(602, 485);
      this.miDivision.SplitterDistance = 288;
      this.miDivision.TabIndex = 5;
      // 
      // miLista
      // 
      this.miLista.ContextMenuStrip = this.miMenuEditorDePdis;
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(602, 288);
      this.miLista.TabIndex = 2;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      // 
      // miMenuEditorDePdis
      // 
      this.miMenuEditorDePdis.Lista = this.miLista;
      this.miMenuEditorDePdis.ManejadorDePdis = null;
      this.miMenuEditorDePdis.Name = "miMenuDeContexto";
      this.miMenuEditorDePdis.Size = new System.Drawing.Size(325, 114);
      // 
      // miMapaDePdisSeleccionadas
      // 
      this.miMapaDePdisSeleccionados.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miMapaDePdisSeleccionados.EscuchadorDeEstatus = null;
      this.miMapaDePdisSeleccionados.Lista = this.miLista;
      this.miMapaDePdisSeleccionados.Location = new System.Drawing.Point(0, 0);
      this.miMapaDePdisSeleccionados.ManejadorDeMapa = null;
      this.miMapaDePdisSeleccionados.MuestraPdis = false;
      this.miMapaDePdisSeleccionados.MuestraPolígonos = false;
      this.miMapaDePdisSeleccionados.MuestraPolilíneas = false;
      this.miMapaDePdisSeleccionados.MuestraTodoElMapa = true;
      this.miMapaDePdisSeleccionados.MuestraTodosLosElementos = true;
      this.miMapaDePdisSeleccionados.MuestraPdis = false;
      this.miMapaDePdisSeleccionados.Name = "miMapaDePdisSeleccionados";
      this.miMapaDePdisSeleccionados.Size = new System.Drawing.Size(602, 193);
      this.miMapaDePdisSeleccionados.TabIndex = 0;
      // 
      // InterfaseListaConMapaDePdis
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miDivision);
      this.Name = "InterfaseListaConMapaDePdis";
      this.Size = new System.Drawing.Size(602, 485);
      this.miDivision.Panel1.ResumeLayout(false);
      this.miDivision.Panel2.ResumeLayout(false);
      this.miDivision.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaDePdis miLista;
    private InterfaseMapaDePdisSeleccionados miMapaDePdisSeleccionados;
    private MenuEditorDePdis miMenuEditorDePdis;
    private System.Windows.Forms.SplitContainer miDivision;
  }
}
