#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Interface para un escuchador de estatus.
  /// </summary>
  public interface IEscuchadorDeEstatus
  {
    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    string Estatus
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone las coordenadas.
    /// </summary>
    Coordenadas Coordenadas
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el texto del archivo activo de la aplicación.
    /// </summary>
    string ArchivoActivo
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el progreso.
    /// </summary>
    long Progreso
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el progreso máximo.
    /// </summary>
    long ProgresoMáximo
    {
      get;
      set;
    }
  }
}
