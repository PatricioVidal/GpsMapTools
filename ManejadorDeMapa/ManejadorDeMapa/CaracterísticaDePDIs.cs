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
  static class CaracterísticasDePDIs
  {
    #region Campos
    private static readonly string miArchivoDeCaracterísticasDePDIs = "CaracterísticasDePDIs.csv";
    #endregion

    #region Campos y Métodos Públicos
    /// <summary>
    /// Diccionario de descripciones por tipo de PDIs.
    /// </summary>
    public readonly static IDictionary<int, string> Descripciones = new Dictionary<int, string>();


    /// <summary>
    /// Devuelve la descripción para un tipo de PDIs dado.
    /// </summary>
    /// <param name="elTipo">El tipo de PDIs.</param>
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
    private class LectorDeCaracterísticasDePDIs : LectorDeArchivo
    {
      public readonly IDictionary<int, string> misDescripciones;

      public LectorDeCaracterísticasDePDIs(
        string elArchivo,
        IDictionary<int, string> lasDescripciones)
      {
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

          // Verifica que al menos tenemos dos partes.
          if (partes.Length < 2)
          {
            throw new ArgumentException("No se encontraron al menos 2 partes separadas por coma en la linea: " + línea);
          }

          // Lee las partes.
          string[] tipos = partes[0].Split('-');
          int índiceDeDescripción = 1;
          int númeroDePartes = partes.Length - índiceDeDescripción;
          string descripción = string.Join(",", partes, índiceDeDescripción, númeroDePartes);

          // Llena los diccionarios.
          int primerTipo = Convert.ToInt32(tipos[0], 16);
          int últimoTipo = primerTipo;
          if (tipos.Length > 1)
          {
            últimoTipo = Convert.ToInt32(tipos[1], 16);
          }
          for (int tipo = primerTipo; tipo <= últimoTipo; ++tipo)
          {
            Descripciones[tipo] = descripción;
          }
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    static CaracterísticasDePDIs()
    {
      // Lee las características de PDIs.
      LectorDeCaracterísticasDePDIs lector = new LectorDeCaracterísticasDePDIs(
        miArchivoDeCaracterísticasDePDIs,
        Descripciones);
    }
    #endregion
  }
}
