#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Drawing;
namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un par de coordenadas.
  /// </summary>
  public class Coordenadas
  {
    /// <summary>
    /// Latitud.
    /// </summary>
    public readonly double Latitud;
    
    /// <summary>
    /// Longitud.
    /// </summary>
    public readonly double Longitud;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="laLatitud">La Latitud.</param>
    /// <param name="laLongitud">La Longitud.</param>
    public Coordenadas(double laLatitud, double laLongitud)
    {
      Latitud = laLatitud;
      Longitud = laLongitud;
    }


    /// <summary>
    /// Devuelve la distancia en metros entre dos coordenads.
    /// </summary>
    /// <param name="laPrimeraCoordenada">La primera coordenada.</param>
    /// <param name="laSegundaCoordenada">La segunda coordenada.</param>
    public static double Distancia(
      Coordenadas laPrimeraCoordenada,
      Coordenadas laSegundaCoordenada)
    {
      // Esta fórmula fue provista por Antonio Cincotti D'Orazio
      double diferenciaDeLatitud = laPrimeraCoordenada.Latitud - laSegundaCoordenada.Latitud;
      double diferenciaDeLongitud = laPrimeraCoordenada.Longitud - laSegundaCoordenada.Longitud;

      double diferenciaDeLatitudEnMetros = diferenciaDeLatitud * 60 * 1852;
      double diferenciaDeLongitudEnMetros = (diferenciaDeLongitud
        * Math.Cos(laPrimeraCoordenada.Latitud * Math.PI / 180)) * 60 * 1852;

      double distanciaEnMetros = Math.Sqrt(
        (diferenciaDeLatitudEnMetros * diferenciaDeLatitudEnMetros) +
        (diferenciaDeLongitudEnMetros * diferenciaDeLongitudEnMetros));
      
      return distanciaEnMetros;
    }


    /// <summary>
    /// Conversion de coordenadas a PointF.
    /// </summary>
    /// <param name="lasCoordenadas">Las coordenadas.</param>
    public static implicit operator PointF(Coordenadas lasCoordenadas)
    {
      return new PointF((float)lasCoordenadas.Longitud, (float)lasCoordenadas.Latitud);
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      string textoNorteSur = "S";
      if (Latitud > 0)
      {
        textoNorteSur = "N";
      }
      string textoEsteOeste = "W";
      if (Longitud > 0)
      {
        textoEsteOeste = "E";
      }

      string texto = textoNorteSur + " " + Math.Abs(Latitud).ToString("0.000000")
        + "  " + textoEsteOeste + " " + Math.Abs(Longitud).ToString("0.000000");

      return texto;
    }


    /// <summary>
    /// Operador de igualdad.
    /// </summary>
    /// <param name="laPrimeraCoordenada">La primera coordenada.</param>
    /// <param name="laSegundaCoordenada">La segunda coordenada.</param>
    public static bool operator ==(
      Coordenadas laPrimeraCoordenada,
      Coordenadas laSegundaCoordenada)
    {
      bool esIgual = (
        (laPrimeraCoordenada.Latitud == laSegundaCoordenada.Latitud) &&
        (laPrimeraCoordenada.Longitud == laSegundaCoordenada.Longitud));

      return esIgual;
    }


    /// <summary>
    /// Operador de desigualdad.
    /// </summary>
    /// <param name="laPrimeraCoordenada">La primera coordenada.</param>
    /// <param name="laSegundaCoordenada">La segunda coordenada.</param>
    public static bool operator !=(
      Coordenadas laPrimeraCoordenada,
      Coordenadas laSegundaCoordenada)
    {
      return !(laPrimeraCoordenada == laSegundaCoordenada);
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
      if (!(elObjecto is Coordenadas))
      {
        return false;
      }

      // Compara latitud y longitud.
      Coordenadas comparador = (Coordenadas)elObjecto;
      bool esIgual = (this == comparador);

      return esIgual;
    }


    /// <summary>
    /// Obtiene una clave única para este objecto.
    /// </summary>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Método GetHashCode() no está implementado.");
    }
  }
}
