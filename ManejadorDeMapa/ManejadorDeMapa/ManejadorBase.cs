#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un manejador base.
  /// </summary>
  public abstract class ManejadorBase<T> where T: ElementoDelMapa
  {
    #region Campos
    private readonly ManejadorDeMapa miManejadorDeMapa;
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private readonly IList<T> misElementos;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el escuchador de estatus.
    /// </summary>
    public IEscuchadorDeEstatus EscuchadorDeEstatus
    {
      get
      {
        return miEscuchadorDeEstatus;
      }
    }


    /// <summary>
    /// Devuelve los elementos del manejador.
    /// </summary>
    public IList<T> Elementos
    {
      get
      {
        return misElementos;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El Manejador de Mapa.</param>
    /// <param name="losElementos">Los Elementos.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorBase(
      ManejadorDeMapa elManejadorDeMapa,
      IList<T> losElementos,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      miManejadorDeMapa = elManejadorDeMapa;
      misElementos = losElementos;
      miEscuchadorDeEstatus = elEscuchadorDeEstatus;

      // Escucha eventos.
      miManejadorDeMapa.MapaNuevo += EnMapaNuevo;
    }


    /// <summary>
    /// Suspende la generación de eventos.
    /// </summary>
    public void SuspendeEventos()
    {
      miManejadorDeMapa.SuspendeEventos();
    }


    /// <summary>
    /// Restablece la generación de eventos.
    /// </summary>
    public void RestableceEventos()
    {
      miManejadorDeMapa.RestableceEventos();
    }
    #endregion

    #region Métodos Privados
    protected abstract void EnMapaNuevo(object elEnviador, EventArgs losArgumentos);
    #endregion
  }
}
