namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  partial class VentanaCambiarTipo
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
      this.miTextoNombrePDI = new System.Windows.Forms.Label();
      this.miTextCoordenadasPDI = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.miProveedorDeErrorDeTipo)).BeginInit();
      this.SuspendLayout();
      // 
      // miTextoTipoNuevo
      // 
      this.miTextoTipoNuevo.Location = new System.Drawing.Point(24, 19);
      this.miTextoTipoNuevo.Name = "miTextoTipoNuevo";
      this.miTextoTipoNuevo.Size = new System.Drawing.Size(50, 20);
      this.miTextoTipoNuevo.TabIndex = 2;
      this.miTextoTipoNuevo.TextChanged += new System.EventHandler(this.EnTipoNuevoCambió);
      this.miTextoTipoNuevo.Validated += new System.EventHandler(this.TipoValidado);
      // 
      // miBotónCambiar
      // 
      this.miBotónCambiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.miBotónCambiar.Location = new System.Drawing.Point(221, 156);
      this.miBotónCambiar.Name = "miBotónCambiar";
      this.miBotónCambiar.Size = new System.Drawing.Size(75, 23);
      this.miBotónCambiar.TabIndex = 0;
      this.miBotónCambiar.Text = "Cambiar";
      this.miBotónCambiar.UseVisualStyleBackColor = true;
      this.miBotónCambiar.Click += new System.EventHandler(this.EnBotónCambiar);
      // 
      // miBotónCancelar
      // 
      this.miBotónCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.miBotónCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.miBotónCancelar.Location = new System.Drawing.Point(302, 156);
      this.miBotónCancelar.Name = "miBotónCancelar";
      this.miBotónCancelar.Size = new System.Drawing.Size(75, 23);
      this.miBotónCancelar.TabIndex = 1;
      this.miBotónCancelar.Text = "Cancelar";
      this.miBotónCancelar.UseVisualStyleBackColor = true;
      this.miBotónCancelar.Click += new System.EventHandler(this.EnBotónCancelar);
      // 
      // miTextoDescripción
      // 
      this.miTextoDescripción.AutoSize = true;
      this.miTextoDescripción.Location = new System.Drawing.Point(80, 22);
      this.miTextoDescripción.Name = "miTextoDescripción";
      this.miTextoDescripción.Size = new System.Drawing.Size(0, 13);
      this.miTextoDescripción.TabIndex = 3;
      // 
      // miEtiquetaTipoNuevo
      // 
      this.miEtiquetaTipoNuevo.AutoSize = true;
      this.miEtiquetaTipoNuevo.Location = new System.Drawing.Point(6, 22);
      this.miEtiquetaTipoNuevo.Name = "miEtiquetaTipoNuevo";
      this.miEtiquetaTipoNuevo.Size = new System.Drawing.Size(21, 13);
      this.miEtiquetaTipoNuevo.TabIndex = 4;
      this.miEtiquetaTipoNuevo.Text = " 0x";
      this.miEtiquetaTipoNuevo.TextAlign = System.Drawing.ContentAlignment.TopRight;
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.miTextoTipoNuevo);
      this.groupBox1.Controls.Add(this.miEtiquetaTipoNuevo);
      this.groupBox1.Controls.Add(this.miTextoDescripción);
      this.groupBox1.ForeColor = System.Drawing.SystemColors.Highlight;
      this.groupBox1.Location = new System.Drawing.Point(12, 94);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(365, 50);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Tipo Nuevo";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.miTextoDescripciónOriginal);
      this.groupBox2.Controls.Add(this.miTextoTipoOriginal);
      this.groupBox2.Location = new System.Drawing.Point(12, 36);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(365, 52);
      this.groupBox2.TabIndex = 6;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Tipo Original";
      // 
      // miTextoDescripciónOriginal
      // 
      this.miTextoDescripciónOriginal.AutoSize = true;
      this.miTextoDescripciónOriginal.Location = new System.Drawing.Point(80, 22);
      this.miTextoDescripciónOriginal.Name = "miTextoDescripciónOriginal";
      this.miTextoDescripciónOriginal.Size = new System.Drawing.Size(63, 13);
      this.miTextoDescripciónOriginal.TabIndex = 1;
      this.miTextoDescripciónOriginal.Text = "Descripción";
      // 
      // miTextoTipoOriginal
      // 
      this.miTextoTipoOriginal.AutoSize = true;
      this.miTextoTipoOriginal.Location = new System.Drawing.Point(6, 22);
      this.miTextoTipoOriginal.Name = "miTextoTipoOriginal";
      this.miTextoTipoOriginal.Size = new System.Drawing.Size(66, 13);
      this.miTextoTipoOriginal.TabIndex = 0;
      this.miTextoTipoOriginal.Text = "Tipo Original";
      // 
      // miProveedorDeErrorDeTipo
      // 
      this.miProveedorDeErrorDeTipo.ContainerControl = this;
      // 
      // miTextoNombrePDI
      // 
      this.miTextoNombrePDI.AutoEllipsis = true;
      this.miTextoNombrePDI.ForeColor = System.Drawing.SystemColors.Highlight;
      this.miTextoNombrePDI.Location = new System.Drawing.Point(9, 9);
      this.miTextoNombrePDI.Name = "miTextoNombrePDI";
      this.miTextoNombrePDI.Size = new System.Drawing.Size(206, 13);
      this.miTextoNombrePDI.TabIndex = 7;
      this.miTextoNombrePDI.Text = "PDI";
      // 
      // miTextCoordenadasPDI
      // 
      this.miTextCoordenadasPDI.AutoEllipsis = true;
      this.miTextCoordenadasPDI.ForeColor = System.Drawing.SystemColors.Highlight;
      this.miTextCoordenadasPDI.Location = new System.Drawing.Point(221, 9);
      this.miTextCoordenadasPDI.Name = "miTextCoordenadasPDI";
      this.miTextCoordenadasPDI.Size = new System.Drawing.Size(156, 13);
      this.miTextCoordenadasPDI.TabIndex = 8;
      this.miTextCoordenadasPDI.Text = "Coordenadas";
      // 
      // VentanaCambiarTipo
      // 
      this.AcceptButton = this.miBotónCambiar;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.miBotónCancelar;
      this.ClientSize = new System.Drawing.Size(389, 191);
      this.Controls.Add(this.miTextCoordenadasPDI);
      this.Controls.Add(this.miTextoNombrePDI);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.miBotónCancelar);
      this.Controls.Add(this.miBotónCambiar);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VentanaCambiarTipo";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
    private System.Windows.Forms.Label miTextoNombrePDI;
    private System.Windows.Forms.TextBox miTextoTipoNuevo;
    private System.Windows.Forms.Label miTextCoordenadasPDI;
  }
}