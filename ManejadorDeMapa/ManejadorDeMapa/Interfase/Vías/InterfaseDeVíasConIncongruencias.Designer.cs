namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseDeVíasConIncongruencias
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
      this.miInterfaseListaConMapaDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaConMapaDeVías();
      this.SuspendLayout();
      // 
      // miInterfaseListaConMapaDeVías
      // 
      this.miInterfaseListaConMapaDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
      this.miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = null;
      this.miInterfaseListaConMapaDeVías.Location = new System.Drawing.Point(0, 0);
      this.miInterfaseListaConMapaDeVías.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.Name = "miInterfaseListaConMapaDeVías";
      this.miInterfaseListaConMapaDeVías.Size = new System.Drawing.Size(556, 492);
      this.miInterfaseListaConMapaDeVías.TabIndex = 1;
      // 
      // InterfaseDeVíasConIncongruencias
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miInterfaseListaConMapaDeVías);
      this.Name = "InterfaseDeVíasConIncongruencias";
      this.Size = new System.Drawing.Size(556, 492);
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDeVías miInterfaseListaConMapaDeVías;

  }
}
