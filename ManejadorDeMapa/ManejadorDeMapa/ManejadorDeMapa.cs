#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Manejador de los Puntos de Interes (PDI/POI)
  /// </summary>
  public class ManejadorDeMapa
  {
    #region Campos
    private IList<ElementoDelMapa> misElementos = new List<ElementoDelMapa>();
    private IList<PDI> misPDIs = new List<PDI>();
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private string miArchivo = null;
    private int miNúmeroDeSuspenciónDeEventos = 0;
    private bool miHayEventosDeModificaciónDeElementoPendientes = false;
    private readonly ManejadorDePDIs miManejadorDePDIs;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando se lee un nuevo mapa.
    /// </summary>
    public event EventHandler MapaNuevo;

    /// <summary>
    /// Evento cuando algún elemento del mapa es modificado.
    /// </summary>
    public event EventHandler ElementosModificados;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve los elementos del mapa.
    /// </summary>
    public IList<ElementoDelMapa> Elementos
    {
      get
      {
        return misElementos;
      }
    }


    /// <summary>
    /// Devuelve el manejador de PDIs.
    /// </summary>
    public ManejadorDePDIs ManejadorDePDIs
    {
      get
      {
        return miManejadorDePDIs;
      }
    }


    /// <summary>
    /// Filtro de las extensiones válidas.
    /// </summary>
    public static string FiltrosDeExtensiones
    {
      get
      {
        string filtrosDeExtensiones = "Polish format (*.mp)|*.mp|"
                                      + "Polish format (*.txt)|*.txt|"
                                      + "Todos los archivos (*.*)|*.*";
        return filtrosDeExtensiones;
      }
    }


    /// <summary>
    /// Devuelve el nombre del archivo del mapa.
    /// </summary>
    public string Archivo
    {
      get
      {
        return miArchivo;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorDeMapa(IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      Trace.WriteLine("Inicializando ManejadorDeMapa");
      miEscuchadorDeEstatus = elEscuchadorDeEstatus;
      miManejadorDePDIs = new ManejadorDePDIs(this, misPDIs, elEscuchadorDeEstatus);
    }


    /// <summary>
    /// Abre un archivo.
    /// </summary>
    /// <param name="elArchivo"></param>
    public void Abrir(string elArchivo)
    {
      // Nos aseguramos que nadie modifique el manejador durante esta operación.
      lock (misPDIs)
      {
        miArchivo = elArchivo;
        miEscuchadorDeEstatus.ArchivoActivo = Path.GetFullPath(elArchivo);

        // Por ahora el único formato es el Polish.
        LectorDeFormatoPolish lector = new LectorDeFormatoPolish(this, elArchivo, miEscuchadorDeEstatus);

        // Genera las listas.
        misPDIs.Clear();
        foreach (ElementoDelMapa elemento in misElementos)
        {
          if (elemento is PDI)
          {
            misPDIs.Add((PDI)elemento);
          }
        }
      }

      // Genera el evento de lectura de mapa nuevo.
      SeAbrioUnMapaNuevo();
    }


    /// <summary>
    /// Guarda los cambios.
    /// </summary>
    public void Guarda(string elArchivo)
    {
      // Guarda los cambios.
      new EscritorDeFormatoPolish(elArchivo, misElementos, miEscuchadorDeEstatus);

      // Actualiza el archivo.
      miArchivo = elArchivo;
      miEscuchadorDeEstatus.ArchivoActivo = elArchivo;
    }


    /// <summary>
    /// Suspende la generación de eventos.
    /// </summary>
    public void SuspendeEventos()
    {
      // Incrementa el contador de suspenciones.
      ++miNúmeroDeSuspenciónDeEventos;
    }


    /// <summary>
    /// Restablece la generación de eventos.
    /// </summary>
    public void RestableceEventos()
    {
      // Decrementa el contador de suspenciones.
      --miNúmeroDeSuspenciónDeEventos;

      if (miNúmeroDeSuspenciónDeEventos == 0)
      {
        // Genera los eventos pendientes.
        if (miHayEventosDeModificaciónDeElementoPendientes)
        {
          SeModificóElemento();
          miHayEventosDeModificaciónDeElementoPendientes = false;
        }
      }
      else if (miNúmeroDeSuspenciónDeEventos < 0)
      {
        throw new InvalidOperationException("Se han restablecido los eventos más veces de las que se han suspendido.");
      }
    }


    /// <summary>
    /// Indica que se modificó un elemento del mapa.
    /// </summary>
    internal void SeModificóUnElemento()
    {
      // Si los eventos están suspendidos entonces se indica
      // que hay notificaciones pendientes.
      // Si no, entonces se genera el evento.
      if (miNúmeroDeSuspenciónDeEventos > 0)
      {
        miHayEventosDeModificaciónDeElementoPendientes = true;
      }
      else
      {
        SeModificóElemento();
      }
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Genera el evento indicando que se modificaron elementos.
    /// </summary>
    private void SeAbrioUnMapaNuevo()
    {
      if (MapaNuevo != null)
      {
        MapaNuevo(this, new EventArgs());
      }
    }

    /// <summary>
    /// Genera el evento indicando que se modificaron elementos.
    /// </summary>
    private void SeModificóElemento()
    {
      if (ElementosModificados != null)
      {
        ElementosModificados(this, new EventArgs ());
      }
    }
    #endregion
  }
}
