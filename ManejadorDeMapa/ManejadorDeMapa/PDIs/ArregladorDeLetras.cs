#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Arreglador de letras en PDIs.
  /// </summary>
  public class ArregladorDeLetrasEnPDIs : ProcesadorBase<ManejadorDePDIs, PDI>
  {
    #region Campos
    private readonly LectorDeConversiónDeLetras miLectorDeConversiónDeLetras;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePDIs">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorDeLetrasEnPDIs(
      ManejadorDePDIs elManejadorDePDIs,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePDIs, elEscuchadorDeEstatus)
    {
      miLectorDeConversiónDeLetras = new LectorDeConversiónDeLetras(elEscuchadorDeEstatus);
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elElemento">El PDI.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(PDI elPDI)
    {
      bool modificóElemento = false;

      #region Arregla el nombre del PDI.
      string nombreACorregir = elPDI.Nombre;

      // Cambia el nombre a mayúsculas.
      string nombreCorregido = nombreACorregir.ToUpper();

      // Arregla la letras no permitidas.
      IDictionary<char, char> diccionarioDeLetras = miLectorDeConversiónDeLetras.DiccionarioDeLetras;
      foreach (KeyValuePair<char, char> par in diccionarioDeLetras)
      {
        char letraOriginal = par.Key;
        char letraArreglada = par.Value;
        nombreCorregido = nombreCorregido.Replace(letraOriginal, letraArreglada);
      }
      #endregion

      // Si el nombre cambió entonces actualizar el PDI y reportar el cambio.
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPDI.CambiaNombre(nombreCorregido, "Cambio de Letras");
        modificóElemento = true;
      }

      return modificóElemento;
    }
    #endregion

    #region Clases Privadas
    private class LectorDeConversiónDeLetras : LectorDeArchivo
    {
      #region Campos
      private static readonly string miArchivoDeConversionDeLetras = "ConversionDeLetras.csv";
      private Dictionary<char, char> miDiccionarioDeLetras = new Dictionary<char, char>();
      #endregion

      /// <summary>
      /// Obtiene el diccionario de letras.
      /// </summary>
      public IDictionary<char, char> DiccionarioDeLetras
      {
        get
        {
          return miDiccionarioDeLetras;
        }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
      public LectorDeConversiónDeLetras(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Abrir(miArchivoDeConversionDeLetras);
      }

      #region Métodos Protegidos y Privados
      protected override void ProcesaLínea(string laLínea)
      {
        // Elimina espacios en blanco.
        string línea = laLínea.Trim();

        // Saltarse lineas en blanco y comentarios.
        bool laLíneaEstaEnBlanco = (línea == string.Empty);
        bool laLíneaEsComentario = línea.StartsWith("//");
        if (!laLíneaEstaEnBlanco & !laLíneaEsComentario)
        {
          // Separa las letras.
          string[] partes = línea.Split(',');

          // Verifica que tenemos dos partes.
          if (partes.Length != 2)
          {
            throw new ArgumentException("No se encontraron 2 partes separadas por coma en la linea: " + línea);
          }

          // Lee las dos partes como la letra y la conversion.
          char letra = Convert.ToChar(partes[0]);
          char conversion = Convert.ToChar(partes[1]);
          miDiccionarioDeLetras[letra] = conversion;
        }
      }
      #endregion
    }
    #endregion
  }
}

