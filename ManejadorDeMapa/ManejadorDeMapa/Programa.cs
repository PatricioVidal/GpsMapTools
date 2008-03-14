using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GpsYv.ManejadorDeMapa
{
  static class Programa
  {
    #region Métodos Públicos
    /// <summary>
    /// Punto de entrada de la applicación.
    /// </summary>
    [STAThread]
    static void Main()
    {
      TextWriterTraceListener escritorDeRastro = new TextWriterTraceListener("Rastro.log");
      Trace.AutoFlush = true;
      Trace.WriteLine("Comenzando Aplicación");
      Trace.Indent();
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      try
      {
        Application.Run(new Interface.InterfaceManejadorDeMapa());
      }
      catch (Exception e)
      {
        MuestraExcepción(e);
      }
      Trace.Unindent();
      Trace.Flush();
    }


    /// <summary>
    /// Muestra información sobre una excepción dada.
    /// </summary>
    /// <param name="laExcepción">La excepción dada.</param>
    public static void MuestraExcepción(Exception laExcepción)
    {
      // Crea un archivo de registro.
      string archivoDeRegistro = Interface.VentanaDeAcerca.AssemblyTitle + ".Error.log";
      using (StreamWriter registro = new StreamWriter(archivoDeRegistro, true))
      {
        string encabezado = DateTime.Now + ": " + laExcepción.Message;
        registro.WriteLine(encabezado);
        registro.WriteLine(laExcepción);
        registro.WriteLine();
      }

      // Muestra la excepción al usuario.
      string mensaje = laExcepción.Message;
      Exception innerException = laExcepción.InnerException;
      while (innerException != null)
      {
        mensaje += "\n"+ innerException.Message;
        innerException = innerException.InnerException;
      }
      MessageBox.Show(
        mensaje,
        Interface.VentanaDeAcerca.AssemblyTitle,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error);
    }

    #endregion
  }
}
