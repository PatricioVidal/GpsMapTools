#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo genérico.
  /// </summary>
  public class CampoGenérico : Campo
  {
    #region Campos
    private readonly string miTexto;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el texto del nombre.
    /// </summary>
    public string Texto
    {
      get
      {
        return miTexto;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elIdentificador">El identificador.</param>
    /// <param name="elTexto">El texto.</param>
    public CampoGenérico(string elIdentificador, string elTexto)
      : base(elIdentificador)
    {
      miTexto = elTexto;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      return miTexto;
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">EL objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Si el objeto es nulo entonces no puede ser igual.
      if (elObjecto == null)
      {
        return false;
      }

      // Si el objecto no es del mismo tipo entonces no puede ser igual.
      if (!(elObjecto is CampoGenérico))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoGenérico comparador = (CampoGenérico)elObjecto;
      bool esIgual = (Texto == comparador.Texto);

      return esIgual;
    }


    /// <summary>
    /// Obtiene una clave única para este objecto.
    /// </summary>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Método GetHashCode() no está implementado.");
    }
    #endregion
  }
}
