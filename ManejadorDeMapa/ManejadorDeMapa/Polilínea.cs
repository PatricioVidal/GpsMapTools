#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa una Polilínea.
  /// </summary>
  public class Polilínea : ElementoDelMapa
  {
    #region Campos
    private readonly CampoCoordenadas misCoordenadas = CampoCoordenadas.Vacio;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las coordenadas de la Polilínea.
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
    /// <param name="elNúmero">El número de la Polilínea.</param>
    /// <param name="laClase">La clase de la Polilínea.</param>
    /// <param name="losCampos">Los campos de la Polilínea.</param>
    public Polilínea(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa,
             elNúmero,
             laClase,
             CaracterísticasDePolilíneas.Descripciones,
             losCampos)
    {
      // Busca los campos especificos de los Polilíneas.
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

      Polilínea clone = new Polilínea(ManejadorDeMapa, Número, Clase, camposNuevos);
      return clone;
    }
    #endregion
  }
}
