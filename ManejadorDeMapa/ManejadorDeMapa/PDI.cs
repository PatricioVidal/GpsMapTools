#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un Punto De Interés (PDI/POI)
  /// </summary>
  public class PDI : ElementoDelMapa
  {
    #region Campos
    private readonly static Coordenadas misCoordenadasVacias = new Coordenadas(double.NaN, double.NaN);
    private readonly CampoCoordenadas misCoordenadas = CampoCoordenadas.Vacio;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las coordenadas del PDI.
    /// </summary>
    public Coordenadas Coordenadas
    {
      get
      {
        if (misCoordenadas.Coordenadas.Length == 0)
        {
          return misCoordenadasVacias;
        }

        return misCoordenadas.Coordenadas[0];
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del PDI.</param>
    /// <param name="laClase">La clase de PDI.</param>
    /// <param name="losCampos">Los campos del PDI.</param>
    public PDI(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa, 
             elNúmero, 
             laClase,
             CaracterísticasDePDIs.Descripciones,
             losCampos)
    {
      // Busca los campos especificos de los PDIs.
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
      string texto = Nombre + Coordenadas.ToString();

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

      PDI clone = new PDI(ManejadorDeMapa, Número, Clase, camposNuevos);
      return clone;
    }

    public bool TieneLaMismaInformación(PDI elPDI)
    {
      bool tieneLaMismaInformación = false;

      // El PDI tiene la misma información si:
      //  - El nombre es igual.
      //  - El tipo es igual.
      //  - Las coordenadas son iguales.
      //  - La información de los campos son iguales.
      if ((Nombre == elPDI.Nombre) &&
        (Tipo == elPDI.Tipo) &&
        (Coordenadas == elPDI.Coordenadas) &&
        (Campos.Count == elPDI.Campos.Count))
      {
        // Ahora hay que asegurarse que todos lo campos son iguales.
        tieneLaMismaInformación = true;

        foreach (Campo campo in Campos)
        {
          bool encontróCampoIgual = false;
          foreach (Campo campoAComparar in elPDI.Campos)
          {
            if (campo.Equals(campoAComparar))
            {
              encontróCampoIgual = true;
              break;
            }
          }

          // Si no encontró un campo igual entonces el PDI
          // no contiene la misma información.
          if (!encontróCampoIgual)
          {
            tieneLaMismaInformación = false;
            break;
          }
        }
      }

      return tieneLaMismaInformación;
    }
    #endregion
  }
}
