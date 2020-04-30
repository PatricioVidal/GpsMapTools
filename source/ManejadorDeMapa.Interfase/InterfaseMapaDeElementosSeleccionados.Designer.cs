namespace GpsYv.ManejadorDeMapa.Interfase
{
  partial class InterfaseMapaDeElementosSeleccionados
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges")]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.miTimerCambioDeItemsSeleccionados = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // miTimer
      // 
      this.miTimerCambioDeItemsSeleccionados.Interval = 10;
      this.miTimerCambioDeItemsSeleccionados.Tick += new System.EventHandler(this.EnTimerCambioDeItemsSeleccionadosTick);
      // 
      // InterfaseMapaDeElementosSeleccionados
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "InterfaseMapaDeElementosSeleccionados";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer miTimerCambioDeItemsSeleccionados;
  }
}
