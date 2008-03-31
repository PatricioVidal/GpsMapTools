namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseListaConMapaDeVías
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
      System.Windows.Forms.SplitContainer division;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseListaConMapaDeVías));
      this.miLista = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaDeVías();
      this.miMapaDeVíasSeleccionadas = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseMapaDeVíasSeleccionadas();
      this.miMenuEditorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.MenuEditorDeVías();
      division = new System.Windows.Forms.SplitContainer();
      division.Panel1.SuspendLayout();
      division.Panel2.SuspendLayout();
      division.SuspendLayout();
      this.SuspendLayout();
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
      division.Panel2.Controls.Add(this.miMapaDeVíasSeleccionadas);
      division.Size = new System.Drawing.Size(602, 485);
      division.SplitterDistance = 288;
      division.TabIndex = 5;
      // 
      // miLista
      // 
      this.miLista.ContextMenuStrip = this.miMenuEditorDeVías;
      this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miLista.FullRowSelect = true;
      this.miLista.GridLines = true;
      this.miLista.Location = new System.Drawing.Point(0, 0);
      this.miLista.Name = "miLista";
      this.miLista.Size = new System.Drawing.Size(602, 288);
      this.miLista.TabIndex = 2;
      this.miLista.UseCompatibleStateImageBehavior = false;
      this.miLista.View = System.Windows.Forms.View.Details;
      this.miLista.VirtualMode = true;
      // 
      // miMapaDeVíasSeleccionadas
      // 
      this.miMapaDeVíasSeleccionadas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miMapaDeVíasSeleccionadas.EscuchadorDeEstatus = null;
      this.miMapaDeVíasSeleccionadas.Lista = this.miLista;
      this.miMapaDeVíasSeleccionadas.Location = new System.Drawing.Point(0, 0);
      this.miMapaDeVíasSeleccionadas.ManejadorDeMapa = null;
      this.miMapaDeVíasSeleccionadas.MuestraPDIs = false;
      this.miMapaDeVíasSeleccionadas.MuestraPolígonos = false;
      this.miMapaDeVíasSeleccionadas.MuestraPolilíneas = false;
      this.miMapaDeVíasSeleccionadas.MuestraTodoElMapa = true;
      this.miMapaDeVíasSeleccionadas.MuestraTodosLosElementos = true;
      this.miMapaDeVíasSeleccionadas.MuestraVías = false;
      this.miMapaDeVíasSeleccionadas.Name = "miMapaDeVíasSeleccionadas";
      this.miMapaDeVíasSeleccionadas.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miMapaDeVíasSeleccionadas.RectánguloVisibleEnCoordenadas")));
      this.miMapaDeVíasSeleccionadas.Size = new System.Drawing.Size(602, 193);
      this.miMapaDeVíasSeleccionadas.TabIndex = 0;
      // 
      // miMenuEditorDeVías
      // 
      this.miMenuEditorDeVías.Lista = this.miLista;
      this.miMenuEditorDeVías.ManejadorDeVías = null;
      this.miMenuEditorDeVías.Name = "miMenuDeContexto";
      this.miMenuEditorDeVías.Size = new System.Drawing.Size(336, 48);
      // 
      // InterfaseListaConMapaDeVías
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(division);
      this.Name = "InterfaseListaConMapaDeVías";
      this.Size = new System.Drawing.Size(602, 485);
      division.Panel1.ResumeLayout(false);
      division.Panel2.ResumeLayout(false);
      division.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaDeVías miLista;
    private InterfaseMapaDeVíasSeleccionadas miMapaDeVíasSeleccionadas;
    private MenuEditorDeVías miMenuEditorDeVías;
  }
}
