#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
// (For English, see further down.)
//
// GpsYv.ManejadorDeMapa es una aplicación para manejar Mapas de GPS en el
// formato Polish (.mp).  Esta escrito en C# usando el .NET Framework 3.5. 
//
// Esta programa nació por la necesidad del Grupo GPS de Venezuela, 
// GPS_YV (http://www.gpsyv.net), de analizar y corregir los mapas que el
// grupo genera para la comunidad.  GpsYv.ManejadorDeMapa se distribuye bajo 
// la licencia GPL con la finalidad de que sea útil para otros grupos o
// individuos que hacen mapas, y también para promover la colaboración 
// con este proyecto.
//
// Visita http://www.codeplex.com/GPSYVManejadorDeMapa para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Programador: Patricio Vidal (PatricioV2@hotmail.com)
//
// Este programa es software libre. Puede redistribuirlo y/o modificarlo
// bajo los términos de la Licencia Pública General de GNU según es publicada
// por la Free Software Foundation, bien de la versión 2 de dicha Licencia o 
// bien (según su elección) de cualquier versión posterior. 
//
// Este programa se distribuye con la esperanza de que sea útil, 
// pero SIN NINGUNA GARANTÍA, incluso sin la garantía MERCANTIL
// implícita o sin garantizar la CONVENIENCIA PARA UN PROPÓSITO PARTICULAR.
// Véase la Licencia Pública General de GNU para más detalles. 
//
// Debería haber recibido una copia de la Licencia Pública General 
// junto con este programa. Si no ha sido así, escriba a la 
// Free Software Foundation, Inc., en 675 Mass Ave, 
// Cambridge, MA 02139, EEUU.
//
//-----------------------------------------------------------------------------
//
// GpsYv.ManejadorDeMapa (GPS Map Manager) is an application to Manage 
// GPS Maps // in Polish format (.mp).  It is written in C# using the 
// .NET Framework 3.5.
//
// This program was born by the need of the GPS Group of Venezuela,
// GPS_YV (http://www.gpsyv.net), to analyze and fix the maps that
// the group generates for the community. GpsYv.ManejadorDeMapa is 
// distributed under the GPL license with the purpose that it could 
// be useful for other groups or individuals that create maps, and 
// also to promote the collaboration with this project.
//
// Visit http://www.codeplex.com/GPSYVManejadorDeMapa for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Programmer: Patricio Vidal (PatricioV2@hotmail.com)
//
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
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
