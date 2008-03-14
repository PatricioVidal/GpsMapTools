#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de un PDI (POI).
  /// </summary>
  public abstract class Campo
  {
    #region Campos
    private readonly string miIdentificador;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el identificador del campo.
    /// </summary>
    public string Identificador
    {
      get
      {
        return miIdentificador;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elIdentificador">El identificador del campo.</param>
    public Campo(string elIdentificador)
    {
      miIdentificador = elIdentificador;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override abstract string ToString();

    #endregion
  }
}
