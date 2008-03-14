using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Clase que contiene las características de los PDIs.
  /// </summary>
  static class CaracterísticasDePolilíneas
  {
    #region Campos
    private static readonly string miArchivoDeCaracterísticasDePolilíneas = "CaracterísticasDePolilíneas.csv";

    /// <summary>
    /// Diccionario de lápices por tipo de polilinea.
    /// </summary>
    /// <remarks>
    public readonly static IDictionary<int, Pen> misLápices = new Dictionary<int, Pen>();
    #endregion

    #region Campos y Métodos Públicos
    /// <summary>
    /// Diccionario de descripciones por tipo de polilinea.
    /// </summary>
    public readonly static IDictionary<int, string> Descripciones = new Dictionary<int, string>();


    /// <summary>
    /// Devuelve el lápiz para un tipo de polilinea dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polilinea.</param>
    public static Pen Lápiz(int elTipo)
    {
      Pen lápiz;
      bool existe = misLápices.TryGetValue(elTipo, out lápiz);
      if (!existe)
      {
        // Lápiz por defecto.
        lápiz = misLápices[0];
      }
      return lápiz;
    }


    /// <summary>
    /// Devuelve la descripción para un tipo de polilínea dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polígono.</param>
    public static string Descripción(int elTipo)
    {
      string descripcion;
      bool existe = Descripciones.TryGetValue(elTipo, out descripcion);
      if (!existe)
      {
        // Descripcion por defecto.
        descripcion = Descripciones[0];
      }
      return descripcion;
    }
    #endregion
    #region Métodos Privados
    private class LectorDeCaracterísticasDePolilíneas : LectorDeArchivo
    {
      public readonly IDictionary<int, Pen> misLápices;
      public readonly IDictionary<int, string> misDescripciones;

      public LectorDeCaracterísticasDePolilíneas(
        string elArchivo,
        IDictionary<int, Pen> losLápices,
        IDictionary<int, string> lasDescripciones)
      {
        misLápices = losLápices;
        misDescripciones = lasDescripciones;

        Abrir(elArchivo);
      }


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
          if (partes.Length != 4)
          {
            throw new ArgumentException("No se encontraron 4 partes separadas por coma en la linea: " + línea);
          }

          // Lee las cuatro partes.
          int tipo = Convert.ToInt32(partes[0], 16);
          Color color = Color.FromName(partes[1]);
          int ancho = int.Parse(partes[2]);
          string descripción = partes[3];

          // Llena los diccionarios.
          Pen lápiz = new Pen(color, ancho);
          misLápices[tipo] = lápiz;
          Descripciones[tipo] = descripción;
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    static CaracterísticasDePolilíneas()
    {
      // Lee las características de polígonos.
      LectorDeCaracterísticasDePolilíneas lector = new LectorDeCaracterísticasDePolilíneas(
        miArchivoDeCaracterísticasDePolilíneas,
        misLápices,
        Descripciones);
    }
    #endregion
  }
}
