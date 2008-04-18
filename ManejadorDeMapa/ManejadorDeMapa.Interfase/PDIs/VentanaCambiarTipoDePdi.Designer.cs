namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  partial class VentanaCambiarTipoDePdi
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VentanaCambiarTipoDePdi));
      this.miTextoTipoNuevo = new System.Windows.Forms.TextBox();
      this.miBotónCambiar = new System.Windows.Forms.Button();
      this.miBotónCancelar = new System.Windows.Forms.Button();
      this.miTextoDescripción = new System.Windows.Forms.Label();
      this.miEtiquetaTipoNuevo = new System.Windows.Forms.Label();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.miTextoDescripciónOriginal = new System.Windows.Forms.Label();
      this.miTextoTipoOriginal = new System.Windows.Forms.Label();
      this.miProveedorDeErrorDeTipo = new System.Windows.Forms.ErrorProvider(this.components);
      this.miTextoNombrePdi = new System.Windows.Forms.Label();
      this.miTextoCoordenadasPdi = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miProveedorDeErrorDeTipo)).BeginInit();
      this.SuspendLayout();
      // 
      // miTextoTipoNuevo
      // 
      resources.ApplyResources(this.miTextoTipoNuevo, "miTextoTipoNuevo");
      this.miTextoTipoNuevo.Name = "miTextoTipoNuevo";
      this.miTextoTipoNuevo.TextChanged += new System.EventHandler(this.EnTipoNuevoCambió);
      this.miTextoTipoNuevo.Validated += new System.EventHandler(this.TipoValidado);
      // 
      // miBotónCambiar
      // 
      resources.ApplyResources(this.miBotónCambiar, "miBotónCambiar");
      this.miBotónCambiar.Name = "miBotónCambiar";
      this.miBotónCambiar.UseVisualStyleBackColor = true;
      this.miBotónCambiar.Click += new System.EventHandler(this.EnBotónCambiar);
      // 
      // miBotónCancelar
      // 
      resources.ApplyResources(this.miBotónCancelar, "miBotónCancelar");
      this.miBotónCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.miBotónCancelar.Name = "miBotónCancelar";
      this.miBotónCancelar.UseVisualStyleBackColor = true;
      this.miBotónCancelar.Click += new System.EventHandler(this.EnBotónCancelar);
      // 
      // miTextoDescripción
      // 
      resources.ApplyResources(this.miTextoDescripción, "miTextoDescripción");
      this.miTextoDescripción.Name = "miTextoDescripción";
      // 
      // miEtiquetaTipoNuevo
      // 
      resources.ApplyResources(this.miEtiquetaTipoNuevo, "miEtiquetaTipoNuevo");
      this.miEtiquetaTipoNuevo.Name = "miEtiquetaTipoNuevo";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.miTextoTipoNuevo);
      this.groupBox1.Controls.Add(this.miEtiquetaTipoNuevo);
      this.groupBox1.Controls.Add(this.miTextoDescripción);
      this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
      resources.ApplyResources(this.groupBox1, "groupBox1");
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.miTextoDescripciónOriginal);
      this.groupBox2.Controls.Add(this.miTextoTipoOriginal);
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // miTextoDescripciónOriginal
      // 
      resources.ApplyResources(this.miTextoDescripciónOriginal, "miTextoDescripciónOriginal");
      this.miTextoDescripciónOriginal.Name = "miTextoDescripciónOriginal";
      // 
      // miTextoTipoOriginal
      // 
      resources.ApplyResources(this.miTextoTipoOriginal, "miTextoTipoOriginal");
      this.miTextoTipoOriginal.Name = "miTextoTipoOriginal";
      // 
      // miProveedorDeErrorDeTipo
      // 
      this.miProveedorDeErrorDeTipo.ContainerControl = this;
      // 
      // miTextoNombrePdi
      // 
      this.miTextoNombrePdi.AutoEllipsis = true;
      this.miTextoNombrePdi.ForeColor = System.Drawing.SystemColors.Highlight;
      resources.ApplyResources(this.miTextoNombrePdi, "miTextoNombrePdi");
      this.miTextoNombrePdi.Name = "miTextoNombrePdi";
      // 
      // miTextCoordenadasPdi
      // 
      this.miTextoCoordenadasPdi.AutoEllipsis = true;
      this.miTextoCoordenadasPdi.ForeColor = System.Drawing.SystemColors.Highlight;
      resources.ApplyResources(this.miTextoCoordenadasPdi, "miTextCoordenadasPdi");
      this.miTextoCoordenadasPdi.Name = "miTextCoordenadasPdi";
      // 
      // VentanaCambiarTipoDePdi
      // 
      this.AcceptButton = this.miBotónCambiar;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.miBotónCancelar;
      this.Controls.Add(this.miTextoCoordenadasPdi);
      this.Controls.Add(this.miTextoNombrePdi);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.miBotónCancelar);
      this.Controls.Add(this.miBotónCambiar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VentanaCambiarTipoDePdi";
      this.TopMost = true;
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miProveedorDeErrorDeTipo)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button miBotónCambiar;
    private System.Windows.Forms.Button miBotónCancelar;
    private System.Windows.Forms.Label miTextoDescripción;
    private System.Windows.Forms.Label miEtiquetaTipoNuevo;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label miTextoTipoOriginal;
    private System.Windows.Forms.Label miTextoDescripciónOriginal;
    private System.Windows.Forms.ErrorProvider miProveedorDeErrorDeTipo;
    private System.Windows.Forms.Label miTextoNombrePdi;
    private System.Windows.Forms.TextBox miTextoTipoNuevo;
    private System.Windows.Forms.Label miTextoCoordenadasPdi;
  }
}