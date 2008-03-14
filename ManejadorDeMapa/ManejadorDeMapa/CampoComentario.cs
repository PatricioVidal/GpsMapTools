#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de comentario.
  /// </summary>
  public class CampoComentario : Campo
  {
    #region Campos
    private readonly string miTexto = string.Empty;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el texto del comentario.
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
    /// <param name="elTexto">La línea.</param>
    public CampoComentario(string elTexto)
      : base(";")
    {
      // El comentario es lo que está despues del ';'.
      if (elTexto.Length > 1)
      {
        miTexto = elTexto.Substring(1);
      }
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
      if (!(elObjecto is CampoComentario))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoComentario comparador = (CampoComentario)elObjecto;
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
