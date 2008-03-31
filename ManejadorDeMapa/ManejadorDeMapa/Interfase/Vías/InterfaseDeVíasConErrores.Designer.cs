namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  partial class InterfaseDeViasConErrores
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseDeViasConErrores));
      this.miInterfaseListaConMapaDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseListaConMapaDeVías();
      this.SuspendLayout();
      // 
      // miInterfaseListaConMapaDeVías
      // 
      resources.ApplyResources(this.miInterfaseListaConMapaDeVías, "miInterfaseListaConMapaDeVías");
      this.miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = null;
      this.miInterfaseListaConMapaDeVías.ManejadorDeMapa = null;
      this.miInterfaseListaConMapaDeVías.Name = "miInterfaseListaConMapaDeVías";
      // 
      // InterfaseDeViasConErrores
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.miInterfaseListaConMapaDeVías);
      this.Name = "InterfaseDeViasConErrores";
      this.ResumeLayout(false);

    }

    #endregion

    private InterfaseListaConMapaDeVías miInterfaseListaConMapaDeVías;

  }
}
