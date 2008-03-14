#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de coordenadas.
  /// </summary>
  public class CampoCoordenadas : Campo
  {
    #region Campos
    public const string IdentificadorDeCoordenadas = "Data";
    public const string IdentificadorDeCoordenadasAlterno = "Origin";
    #endregion

    #region Propiedades
    /// <summary>
    /// Coordenadas vacia.
    /// </summary>
    static readonly public CampoCoordenadas Vacio = new CampoCoordenadas(IdentificadorDeCoordenadas, 0, new Coordenadas[0]);


    /// <summary>
    /// Devuelve la latitud.
    /// </summary>
    public readonly Coordenadas[] Coordenadas;


    /// <summary>
    /// Nivel.
    /// </summary>
    public readonly int Nivel;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elIdentificador">El Identificador.</param>
    /// <param name="elNivel">El nivel.</param>
    /// <param name="lasCoordenadas">Las coordenadas.</param>
    public CampoCoordenadas(
      string elIdentificador,
      int elNivel,
      params Coordenadas[] lasCoordenadas)
      : base(elIdentificador)
    {
      Nivel = elNivel;
      Coordenadas = lasCoordenadas;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      StringBuilder texto = new StringBuilder();

      foreach (Coordenadas coordenadas in Coordenadas)
      {
        texto.Append(coordenadas.ToString());
      }

      return texto.ToString();
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
      if (!(elObjecto is CampoCoordenadas))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoCoordenadas comparador = (CampoCoordenadas)elObjecto;
      bool esIgual = false;

      if (comparador.Coordenadas.Length == Coordenadas.Length)
      {
        esIgual = true;
        for (int i = 0; i < Coordenadas.Length; ++i)
        {
          if (comparador.Coordenadas[i] != Coordenadas[i])
          {
            esIgual = false;
            break;
          }
        }
      }

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
