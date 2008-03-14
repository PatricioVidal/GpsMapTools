#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un Polígono.
  /// </summary>
  public class Polígono : ElementoDelMapa
  {
    #region Campos
    private readonly CampoCoordenadas misCoordenadas = CampoCoordenadas.Vacio;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las coordenadas del Polígono.
    /// </summary>
    public Coordenadas[] Coordenadas
    {
      get
      {
        return misCoordenadas.Coordenadas;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del Polígono.</param>
    /// <param name="laClase">La clase de Polígono.</param>
    /// <param name="losCampos">Los campos del Polígono.</param>
    public Polígono(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa, 
             elNúmero,
             laClase,
             CaracterísticasDePolígonos.Descripciones,
             losCampos)
    {
      // Busca los campos especificos de los Polígonos.
      foreach (Campo campo in losCampos)
      {
        if (campo is CampoCoordenadas)
        {
          misCoordenadas = (CampoCoordenadas)campo;
        }
      }
    }


    /// <summary>
    /// Devuelve una version de texto del objecto.
    /// </summary>
    public override string ToString()
    {
      StringBuilder coordenadas = new StringBuilder();
      int númeroDeCoordenasAMostrar = Math.Min(Coordenadas.Length, 5);
      for (int i = 0; i < númeroDeCoordenasAMostrar; ++i)
      {
        coordenadas.Append(Coordenadas[i].ToString());
      }

      string texto = Nombre + coordenadas.ToString();

      return texto;
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public override object Clone()
    {
      // Como los campos son invariables entonces no necesitamos
      // hacer copias de ellos.
      List<Campo> camposNuevos = new List<Campo>(Campos.Count);
      foreach (Campo campo in Campos)
      {
        camposNuevos.Add(campo);
      }

      Polígono clone = new Polígono(ManejadorDeMapa, Número, Clase, camposNuevos);
      return clone;
    }
    #endregion
  }
}
