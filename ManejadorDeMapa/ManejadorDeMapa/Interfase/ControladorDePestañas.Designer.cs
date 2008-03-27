namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class ControladorDePestañas
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
      System.Windows.Forms.ImageList listaDeImágenes;
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControladorDePestañas));
      listaDeImágenes = new System.Windows.Forms.ImageList(this.components);
      this.SuspendLayout();
      // 
      // listaDeImágenes
      // 
      listaDeImágenes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("listaDeImágenes.ImageStream")));
      listaDeImágenes.TransparentColor = System.Drawing.Color.Transparent;
      listaDeImágenes.Images.SetKeyName(0, "vote_yes.png");
      listaDeImágenes.Images.SetKeyName(1, "attention.png");
      listaDeImágenes.Images.SetKeyName(2, "stop.png");
      // 
      // ControladorDePestañas
      // 
      this.ImageList = listaDeImágenes;
      this.ResumeLayout(false);

    }

    #endregion


  }
}
