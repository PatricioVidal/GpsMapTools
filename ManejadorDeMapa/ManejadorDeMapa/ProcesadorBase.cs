#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Clase base para procesadores de elementos.
  /// </summary>
  public abstract class ProcesadorBase<T,K> 
    where T: ManejadorBase<K>
    where K: ElementoDelMapa
  {
    #region Campos
    private readonly T miManejador;
    private readonly string miNombreDeElemento;
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private int miNúmeroDeElementos = 0;
    private int miNúmeroDeElementoProcesándose = 0;
    private int miNúmeroDeElementosModificados;
    private int miIntervaloParaReportarEstatus = 1;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene el manejador.
    /// </summary>
    public T Manejador
    {
      get
      {
        return miManejador;
      }
    }


    /// <summary>
    /// Obtiene el número de elemento procesandose.
    /// </summary>
    public int NúmeroDeElementoProcesándose
    {
      get
      {
        return miNúmeroDeElementoProcesándose;
      }
    }


    /// <summary>
    /// Devuelve el número de elementos modificados.
    /// </summary>
    public int NúmeroDeElementosModificados
    {
      get
      {
        return miNúmeroDeElementosModificados;
      }
    }


    /// <summary>
    /// Obtiene el número de elementos.
    /// </summary>
    public int NúmeroDeElementos
    {
      get
      {
        return miNúmeroDeElementos;
      }
    }


    public K this[int elIndice]
    {
      get
      {
        return miManejador.Elementos[elIndice];
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejador">El manejador.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ProcesadorBase(
      T elManejador,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      miManejador = elManejador;
      miEscuchadorDeEstatus = elEscuchadorDeEstatus;

      // Genera el nombre del elemento.
      // El nombre completo es de la forma GpsYv.ManejadorDeMapa.<Nombre>
      // asi que tenemos que extraer la última palabra.
      string nombreCompleto = typeof(K).ToString();
      string[] palabras = nombreCompleto.Split('.');
      int últimoIndice = palabras.Length - 1;
      miNombreDeElemento = palabras[últimoIndice];
    }


    /// <summary>
    /// Procesa los elementos.
    /// </summary>
    /// <returns>El número de elementos modificados.</returns>
    public int Procesa()
    {
      miNúmeroDeElementosModificados = 0;
      miNúmeroDeElementoProcesándose = 0;
      IList<K> elementos = miManejador.Elementos;
      miNúmeroDeElementos = elementos.Count;

      // Indica que se van a procesar los elementos.
      ComenzóAProcesar();

      // Reporta estatus.
      miEscuchadorDeEstatus.Progreso = 0;

      // Suspende notificaciones.
      miManejador.SuspendeEventos();

      // Procesar todos los elementos.
      miEscuchadorDeEstatus.ProgresoMáximo = elementos.Count;
      foreach (K elemento in elementos)
      {
        // Reportar Progreso
        ++miNúmeroDeElementoProcesándose;
        if ((miNúmeroDeElementoProcesándose % miIntervaloParaReportarEstatus) == 0)
        {
          Estatus = "Procesando " + miNombreDeElemento + " #" + miNúmeroDeElementoProcesándose;
        }
        miEscuchadorDeEstatus.Progreso = miNúmeroDeElementoProcesándose;

        // Procesa el elemento si no está eliminado.
        if (!elemento.FuéEliminado)
        {
          // Procesa el elemento.
          bool modificóElemento = ProcesaElemento(elemento);

          if (modificóElemento)
          {
            ++miNúmeroDeElementosModificados;
          }
        }
      }

      // Reporta estatus.
      miEscuchadorDeEstatus.Progreso = 0;
      miEscuchadorDeEstatus.Estatus = "Listo.";

      // Restablece notificaciones.
      miManejador.RestableceEventos();

      // Indica que se terminó de procesar los elementos.
      TerminoDeProcesar();

      return miNúmeroDeElementosModificados;
    }
    #endregion

    #region Métodos Protegidos y Privados
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected virtual void ComenzóAProcesar()
    {
      miIntervaloParaReportarEstatus = Math.Max(1, miNúmeroDeElementos / 200);
    }


    /// <summary>
    /// Procesa un elemento.
    /// </summary>
    /// <param name="elElemento">El Elemento.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected abstract bool ProcesaElemento(K elElemento);


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected virtual void TerminoDeProcesar()
    {
      // Reporta estatus.
      Estatus = "Se hicieron " + NúmeroDeElementosModificados + " modificaciones a " + miNombreDeElemento + "(s)";
    }


    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    protected string Estatus
    {
      get
      {
        return miEscuchadorDeEstatus.Estatus;
      }

      set
      {
        miEscuchadorDeEstatus.Estatus = value;
      }
    }
    #endregion
  }
}
