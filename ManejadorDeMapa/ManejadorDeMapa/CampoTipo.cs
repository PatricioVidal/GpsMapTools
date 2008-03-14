#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de Tipo.
  /// </summary>
  public class CampoTipo : Campo
  {
    #region Campos
    public const string IdentificadorDeTipo = "Type";
    private readonly int miTipo;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el tipo.
    /// </summary>
    public int Tipo
    {
      get
      {
        return miTipo;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elTexto">El texto de la etiqueta.</param>
    public CampoTipo(string elTexto)
      : base(IdentificadorDeTipo)
    {
      miTipo = Convert.ToInt32(elTexto, 16);
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      string texto = string.Empty;

      // Solo genera el texto para tipos válidos.
      if (miTipo > 0)
      {
        texto = "0x" + miTipo.ToString("x2");
      }

      return texto;
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
      if (!(elObjecto is CampoTipo))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoTipo comparador = (CampoTipo)elObjecto;
      bool esIgual = (Tipo == comparador.Tipo);

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
