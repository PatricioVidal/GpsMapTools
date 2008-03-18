using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa el tipo de un elemento de mapa.
  /// </summary>
  public struct Tipo
  {
    #region Campos
    private readonly int miValor;
    #endregion

    #region Propiedades
    /// <summary>
    /// Representa un tipo vacio.
    /// </summary>
    public static readonly Tipo TipoVacio = new Tipo(0);
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor con un tipo dado.
    /// </summary>
    /// <param name="elTipo">El tipo dado.</param>
    public Tipo(int elTipo)
    {
      miValor = elTipo;
    }


    /// <summary>
    /// Constructor con un texto en formato hexadecimal.
    /// </summary>
    /// <param name="elTextoFormatoHexadecimal">El texto en formato hexadecimal.</param>
    public Tipo(string elTextoFormatoHexadecimal)
      : this (Convert.ToInt32(elTextoFormatoHexadecimal, 16))
    {
    }


    /// <summary>
    /// Devuelve el tipo como texto en formato hexadecimal.
    /// </summary>
    public override string ToString()
    {
      string texto = string.Empty;

      // Solo genera el texto para tipos válidos.
      if (miValor > 0)
      {
        texto = "0x" + miValor.ToString("x2");
      }

      return texto;
    }


    /// <summary>
    /// Conversion a número entero.
    /// </summary>
    /// <param name="elTipo">EL tipo.</param>
    public static implicit operator int(Tipo elTipo)
    {
      return elTipo.miValor;
    }


    /// <summary>
    /// Operador de igualdad.
    /// </summary>
    /// <param name="elPrimerTipo">El primer tipo.</param>
    /// <param name="elSegundoTipo">EL segundo tipo.</param>
    public static bool operator ==(
      Tipo elPrimerTipo,
      Tipo elSegundoTipo)
    {
      bool esIgual = (elPrimerTipo.miValor == elSegundoTipo.miValor);
      return esIgual;
    }


    /// <summary>
    /// Operador de desigualdad.
    /// </summary>
    /// <param name="elPrimerTipo">El primer tipo.</param>
    /// <param name="elSegundoTipo">EL segundo tipo.</param>
    public static bool operator !=(
      Tipo elPrimerTipo,
      Tipo elSegundoTipo)
    {
      return !(elPrimerTipo == elSegundoTipo);
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">El objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Si el objeto es nulo entonces no puede ser igual.
      if (elObjecto == null)
      {
        return false;
      }

      // Si el objecto no es del mismo tipo entonces no puede ser igual.
      if (!(elObjecto is Tipo))
      {
        return false;
      }

      // Compara el objecto.
      Tipo tipo = (Tipo)elObjecto;
      bool esIgual = (this == tipo);

      return esIgual;
    }


    /// <summary>
    /// Deveulve el código para ser usado como identificador único.
    /// </summary>
    public override int GetHashCode()
    {
      return miValor;
    }
    #endregion
  }
}
