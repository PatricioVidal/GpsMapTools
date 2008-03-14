#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Lector de archivo.
  /// </summary>
  public abstract class LectorDeArchivo
  {
    #region Campos
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private StreamReader miLector;
    private long miNúmeroDeLínea = 0;

    /// <summary>
    /// Esta codificación permite intepretar correctamente los acentos de 
    /// archivos ANSI-8 bytes.
    /// </summary>
    private readonly Encoding miCodificaciónPorDefecto = Encoding.GetEncoding(1252);
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene el número de línea en el archivo.
    /// </summary>
    public long NúmeroDeLínea
    {
      get
      {
        return miNúmeroDeLínea;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public LectorDeArchivo()
      : this (new EscuchadorDeEstatusPorOmisión())
    {
    }

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public LectorDeArchivo(IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      miEscuchadorDeEstatus = elEscuchadorDeEstatus;

      // Reporta estatus.
      miEscuchadorDeEstatus.Progreso = 0;
    }


    /// <summary>
    /// Abre un archivo.
    /// </summary>
    /// <param name="elArchivo">El archivo a abrir.</param>
    public void Abrir(string elArchivo)
    {
      // Abre el archivo en modo de texto y empieza a leerlo
      // linea por linea.
      string línea = string.Empty;
      try
      {
        // Crea el camino absoluto al archivo.
        string caminoAbsolutoAlArchivo = elArchivo;
        if (!Path.IsPathRooted(elArchivo))
        {
          string directorio = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
          caminoAbsolutoAlArchivo = Path.Combine(directorio, elArchivo);
        }

        // Abre el archivo.
        using (miLector = new StreamReader(caminoAbsolutoAlArchivo, miCodificaciónPorDefecto))
        {
          // Establece el límite superior de la barra de progreso.
          miEscuchadorDeEstatus.ProgresoMáximo = miLector.BaseStream.Length;

          // Procesa todas las líneas del archivo.
          línea = LeeLaPróximaLínea();
          while (línea != null)
          {
            // Reportar Progreso
            miEscuchadorDeEstatus.Progreso = miLector.BaseStream.Position;

            ProcesaLínea(línea);

            // Lee la próxima línea.
            línea = LeeLaPróximaLínea();
          }
        }
      }
      catch (Exception e)
      {
        string mensaje = "Error leyendo archivo '" + elArchivo + "' en la línea " + miNúmeroDeLínea + ":\n"
          + línea + "\n";

        throw new ArgumentException(mensaje, e);
      }
      finally
      {
        // Borra la barra de progreso.
        miEscuchadorDeEstatus.Progreso = 0;
      }
    }
    #endregion

    #region Métodos Protegidos y Privados
    /// <summary>
    /// Procesa una línea.
    /// </summary>
    /// <param name="línea">La línea.</param>
    protected abstract void ProcesaLínea(string laLínea);


    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    protected string Estatus
    {
      get
      {
        return miEscuchadorDeEstatus.Estatus;
      }

      set
      {
        miEscuchadorDeEstatus.Estatus = value;
      }
    }


    /// <summary>
    /// Devuelve la próxima línea.
    /// </summary>
    protected string LeeLaPróximaLínea()
    {
      string línea = miLector.ReadLine();
      ++miNúmeroDeLínea;

      return línea;
    }
    #endregion
  }
}
