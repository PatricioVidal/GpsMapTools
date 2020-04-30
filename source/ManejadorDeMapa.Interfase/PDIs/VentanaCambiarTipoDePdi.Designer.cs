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
      this.miTextoTipoNuevo.AccessibleDescription = null;
      this.miTextoTipoNuevo.AccessibleName = null;
      resources.ApplyResources(this.miTextoTipoNuevo, "miTextoTipoNuevo");
      this.miTextoTipoNuevo.BackgroundImage = null;
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoTipoNuevo, resources.GetString("miTextoTipoNuevo.Error"));
      this.miTextoTipoNuevo.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoTipoNuevo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoTipoNuevo.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoTipoNuevo, ((int)(resources.GetObject("miTextoTipoNuevo.IconPadding"))));
      this.miTextoTipoNuevo.Name = "miTextoTipoNuevo";
      this.miTextoTipoNuevo.TextChanged += new System.EventHandler(this.EnTipoNuevoCambió);
      this.miTextoTipoNuevo.Validated += new System.EventHandler(this.TipoValidado);
      // 
      // miBotónCambiar
      // 
      this.miBotónCambiar.AccessibleDescription = null;
      this.miBotónCambiar.AccessibleName = null;
      resources.ApplyResources(this.miBotónCambiar, "miBotónCambiar");
      this.miBotónCambiar.BackgroundImage = null;
      this.miProveedorDeErrorDeTipo.SetError(this.miBotónCambiar, resources.GetString("miBotónCambiar.Error"));
      this.miBotónCambiar.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miBotónCambiar, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miBotónCambiar.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miBotónCambiar, ((int)(resources.GetObject("miBotónCambiar.IconPadding"))));
      this.miBotónCambiar.Name = "miBotónCambiar";
      this.miBotónCambiar.UseVisualStyleBackColor = true;
      this.miBotónCambiar.Click += new System.EventHandler(this.EnBotónCambiar);
      // 
      // miBotónCancelar
      // 
      this.miBotónCancelar.AccessibleDescription = null;
      this.miBotónCancelar.AccessibleName = null;
      resources.ApplyResources(this.miBotónCancelar, "miBotónCancelar");
      this.miBotónCancelar.BackgroundImage = null;
      this.miBotónCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.miProveedorDeErrorDeTipo.SetError(this.miBotónCancelar, resources.GetString("miBotónCancelar.Error"));
      this.miBotónCancelar.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miBotónCancelar, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miBotónCancelar.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miBotónCancelar, ((int)(resources.GetObject("miBotónCancelar.IconPadding"))));
      this.miBotónCancelar.Name = "miBotónCancelar";
      this.miBotónCancelar.UseVisualStyleBackColor = true;
      this.miBotónCancelar.Click += new System.EventHandler(this.EnBotónCancelar);
      // 
      // miTextoDescripción
      // 
      this.miTextoDescripción.AccessibleDescription = null;
      this.miTextoDescripción.AccessibleName = null;
      resources.ApplyResources(this.miTextoDescripción, "miTextoDescripción");
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoDescripción, resources.GetString("miTextoDescripción.Error"));
      this.miTextoDescripción.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoDescripción, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoDescripción.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoDescripción, ((int)(resources.GetObject("miTextoDescripción.IconPadding"))));
      this.miTextoDescripción.Name = "miTextoDescripción";
      // 
      // miEtiquetaTipoNuevo
      // 
      this.miEtiquetaTipoNuevo.AccessibleDescription = null;
      this.miEtiquetaTipoNuevo.AccessibleName = null;
      resources.ApplyResources(this.miEtiquetaTipoNuevo, "miEtiquetaTipoNuevo");
      this.miProveedorDeErrorDeTipo.SetError(this.miEtiquetaTipoNuevo, resources.GetString("miEtiquetaTipoNuevo.Error"));
      this.miEtiquetaTipoNuevo.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miEtiquetaTipoNuevo, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miEtiquetaTipoNuevo.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miEtiquetaTipoNuevo, ((int)(resources.GetObject("miEtiquetaTipoNuevo.IconPadding"))));
      this.miEtiquetaTipoNuevo.Name = "miEtiquetaTipoNuevo";
      // 
      // groupBox1
      // 
      this.groupBox1.AccessibleDescription = null;
      this.groupBox1.AccessibleName = null;
      resources.ApplyResources(this.groupBox1, "groupBox1");
      this.groupBox1.BackgroundImage = null;
      this.groupBox1.Controls.Add(this.miTextoTipoNuevo);
      this.groupBox1.Controls.Add(this.miEtiquetaTipoNuevo);
      this.groupBox1.Controls.Add(this.miTextoDescripción);
      this.miProveedorDeErrorDeTipo.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
      this.groupBox1.Font = null;
      this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.TabStop = false;
      // 
      // groupBox2
      // 
      this.groupBox2.AccessibleDescription = null;
      this.groupBox2.AccessibleName = null;
      resources.ApplyResources(this.groupBox2, "groupBox2");
      this.groupBox2.BackgroundImage = null;
      this.groupBox2.Controls.Add(this.miTextoDescripciónOriginal);
      this.groupBox2.Controls.Add(this.miTextoTipoOriginal);
      this.miProveedorDeErrorDeTipo.SetError(this.groupBox2, resources.GetString("groupBox2.Error"));
      this.groupBox2.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.groupBox2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox2.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.groupBox2, ((int)(resources.GetObject("groupBox2.IconPadding"))));
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.TabStop = false;
      // 
      // miTextoDescripciónOriginal
      // 
      this.miTextoDescripciónOriginal.AccessibleDescription = null;
      this.miTextoDescripciónOriginal.AccessibleName = null;
      resources.ApplyResources(this.miTextoDescripciónOriginal, "miTextoDescripciónOriginal");
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoDescripciónOriginal, resources.GetString("miTextoDescripciónOriginal.Error"));
      this.miTextoDescripciónOriginal.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoDescripciónOriginal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoDescripciónOriginal.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoDescripciónOriginal, ((int)(resources.GetObject("miTextoDescripciónOriginal.IconPadding"))));
      this.miTextoDescripciónOriginal.Name = "miTextoDescripciónOriginal";
      // 
      // miTextoTipoOriginal
      // 
      this.miTextoTipoOriginal.AccessibleDescription = null;
      this.miTextoTipoOriginal.AccessibleName = null;
      resources.ApplyResources(this.miTextoTipoOriginal, "miTextoTipoOriginal");
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoTipoOriginal, resources.GetString("miTextoTipoOriginal.Error"));
      this.miTextoTipoOriginal.Font = null;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoTipoOriginal, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoTipoOriginal.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoTipoOriginal, ((int)(resources.GetObject("miTextoTipoOriginal.IconPadding"))));
      this.miTextoTipoOriginal.Name = "miTextoTipoOriginal";
      // 
      // miProveedorDeErrorDeTipo
      // 
      this.miProveedorDeErrorDeTipo.ContainerControl = this;
      resources.ApplyResources(this.miProveedorDeErrorDeTipo, "miProveedorDeErrorDeTipo");
      // 
      // miTextoNombrePdi
      // 
      this.miTextoNombrePdi.AccessibleDescription = null;
      this.miTextoNombrePdi.AccessibleName = null;
      resources.ApplyResources(this.miTextoNombrePdi, "miTextoNombrePdi");
      this.miTextoNombrePdi.AutoEllipsis = true;
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoNombrePdi, resources.GetString("miTextoNombrePdi.Error"));
      this.miTextoNombrePdi.Font = null;
      this.miTextoNombrePdi.ForeColor = System.Drawing.SystemColors.Highlight;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoNombrePdi, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoNombrePdi.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoNombrePdi, ((int)(resources.GetObject("miTextoNombrePdi.IconPadding"))));
      this.miTextoNombrePdi.Name = "miTextoNombrePdi";
      // 
      // miTextoCoordenadasPdi
      // 
      this.miTextoCoordenadasPdi.AccessibleDescription = null;
      this.miTextoCoordenadasPdi.AccessibleName = null;
      resources.ApplyResources(this.miTextoCoordenadasPdi, "miTextoCoordenadasPdi");
      this.miTextoCoordenadasPdi.AutoEllipsis = true;
      this.miProveedorDeErrorDeTipo.SetError(this.miTextoCoordenadasPdi, resources.GetString("miTextoCoordenadasPdi.Error"));
      this.miTextoCoordenadasPdi.Font = null;
      this.miTextoCoordenadasPdi.ForeColor = System.Drawing.SystemColors.Highlight;
      this.miProveedorDeErrorDeTipo.SetIconAlignment(this.miTextoCoordenadasPdi, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("miTextoCoordenadasPdi.IconAlignment"))));
      this.miProveedorDeErrorDeTipo.SetIconPadding(this.miTextoCoordenadasPdi, ((int)(resources.GetObject("miTextoCoordenadasPdi.IconPadding"))));
      this.miTextoCoordenadasPdi.Name = "miTextoCoordenadasPdi";
      // 
      // VentanaCambiarTipoDePdi
      // 
      this.AcceptButton = this.miBotónCambiar;
      this.AccessibleDescription = null;
      this.AccessibleName = null;
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackgroundImage = null;
      this.CancelButton = this.miBotónCancelar;
      this.Controls.Add(this.miTextoCoordenadasPdi);
      this.Controls.Add(this.miTextoNombrePdi);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.miBotónCancelar);
      this.Controls.Add(this.miBotónCambiar);
      this.Font = null;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = null;
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