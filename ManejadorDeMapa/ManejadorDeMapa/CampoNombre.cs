#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de Nombre.
  /// </summary>
  public class CampoNombre : Campo
  {
    #region Campos
    public const string IdentificadorDeEtiqueta = "Label";
    private readonly string miNombre;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el nombre.
    /// </summary>
    public string Nombre
    {
      get
      {
        return miNombre;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elTexto">El nombre.</param>
    public CampoNombre(string elTexto)
      : base(IdentificadorDeEtiqueta)
    {
      miNombre = elTexto;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      return miNombre;
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
      if (!(elObjecto is CampoNombre))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoNombre comparador = (CampoNombre)elObjecto;
      bool esIgual = (Nombre == comparador.Nombre);

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
