#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Escuchador de Estatus por omisión.
  /// </summary>
  public class EscuchadorDeEstatusPorOmisión : IEscuchadorDeEstatus
  {
    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    public string Estatus
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone las coordenadas.
    /// </summary>
    public Coordenadas Coordenadas
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el texto del archivo activo de la aplicación.
    /// </summary>
    public string ArchivoActivo
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el progreso.
    /// </summary>
    public long Progreso
    {
      get;
      set;
    }


    /// <summary>
    /// Obtiene o pone el progreso máximo.
    /// </summary>
    public long ProgresoMáximo
    {
      get;
      set;
    }
  }
}
