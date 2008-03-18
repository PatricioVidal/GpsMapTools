using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Clase que contiene las características de los polígonos.
  /// </summary>
  static class CaracterísticasDePolígonos
  {
    #region Campos
    private static readonly string miArchivoDeCaracterísticasDePolígonos = "CaracterísticasDePolígonos.csv";

    /// <summary>
    /// Diccionario de pinceles por tipo de polígono.
    /// </summary>
    public readonly static IDictionary<Tipo, Brush> misPinceles = new Dictionary<Tipo, Brush>();
    #endregion

    #region Campos y Métodos Públicos
    /// <summary>
    /// Diccionario de descripciones por tipo de polígono.
    /// </summary>
    public readonly static IDictionary<Tipo, string> Descripciones = new Dictionary<Tipo, string>();

    
    /// <summary>
    /// Devuelve el pincel para un tipo de polígono dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polígono.</param>
    public static Brush Pincel(Tipo elTipo)
    {
      Brush pincel;
      bool existe = misPinceles.TryGetValue(elTipo, out pincel);
      if (!existe)
      {
        // Pincel por defecto.
        pincel = misPinceles[Tipo.TipoVacio];
      }
      return pincel;
    }


    /// <summary>
    /// Devuelve la descripción para un tipo de polígono dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polígono.</param>
    public static string Descripción(Tipo elTipo)
    {
      string descripcion;  
      bool existe = Descripciones.TryGetValue(elTipo, out descripcion);
      if (!existe)
      {
        // Descripcion por defecto.
        descripcion = Descripciones[Tipo.TipoVacio];
      }
      return descripcion;
    }
    #endregion
  
    #region Métodos Privados
    private class LectorDeCaracterísticasDePolígonos : LectorDeArchivo
    {
      public readonly IDictionary<Tipo, Brush> misPinceles;
      public readonly IDictionary<Tipo, string> misDescripciones;

      public LectorDeCaracterísticasDePolígonos(
        string elArchivo,
        IDictionary<Tipo, Brush> losPinceles,
        IDictionary<Tipo, string> lasDescripciones)
      {
        misPinceles = losPinceles;
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

          // Verifica que tenemos tres partes.
          if (partes.Length != 3)
          {
            throw new ArgumentException("No se encontraron 3 partes separadas por coma en la linea: " + línea);
          }

          // Lee las tres partes.
          Tipo tipo = new Tipo(partes[0]);
          Brush pincel = new SolidBrush(Color.FromName(partes[1]));
          string descripción = partes[2];

          // Llena los diccionarios.
          misPinceles[tipo] = pincel;
          Descripciones[tipo] = descripción;
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    static CaracterísticasDePolígonos()
    {
      // Lee las características de polígonos.
      LectorDeCaracterísticasDePolígonos lector = new LectorDeCaracterísticasDePolígonos(
        miArchivoDeCaracterísticasDePolígonos,
        misPinceles,
        Descripciones);
    }
    #endregion
  }
}
