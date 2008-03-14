#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un Elemento del Mapa
  /// </summary>
  public class ElementoDesconocido : ElementoDelMapa
  {
    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del elemento.</param>
    /// <param name="laClase">La clase de elemento.</param>
    /// <param name="losCampos">Los campos del elemento.</param>
    public ElementoDesconocido(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa,
              elNúmero,
              laClase,
              "",
              losCampos)
    {
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public override object Clone()
    {
      // Como los campos son invariables entonces no necesitamos
      // hacer copias de ellos.
      List<Campo> camposNuevos = new List<Campo>(Campos);
      foreach (Campo campo in Campos)
      {
        camposNuevos.Add(campo);
      }

      ElementoDesconocido clone = new ElementoDesconocido(
        ManejadorDeMapa,
        Número,
        Clase,
        camposNuevos);

      return clone;
    }
    #endregion
  }
}
