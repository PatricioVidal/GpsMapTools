﻿#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
// GPS Maps in Polish format (.mp).  It is written in C# using the 
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
using System.IO;
using System.Diagnostics;
using GpsYv.ManejadorDeMapa.Pdis;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Manejador de los Puntos de Interes (PDI/POI)
  /// </summary>
  public class ManejadorDeMapa
  {
    #region Campos
    private ElementoDelMapa miEncabezado;
    private readonly IList<ElementoDelMapa> misElementos = new List<ElementoDelMapa>();
    private readonly IList<Pdi> misPdis = new List<Pdi>();
    private readonly IList<Vía> misVías = new List<Vía>();
    private readonly IList<Polilínea> misPolilíneas = new List<Polilínea>();
    private readonly IList<Polígono> misPolígonos = new List<Polígono>();
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private string miArchivo;
    private int miNúmeroDeSuspenciónDeEventos;
    private bool miHayEventosDeModificaciónDeElementoPendientes;
    private bool miHayEventosDeModificaciónDePdisPendientes;
    private bool miHayEventosDeModificaciónDeVíasPendientes;
    private readonly ManejadorDePdis miManejadorDePdis;
    private readonly ManejadorDeVías miManejadorDeVías;
    private readonly ManejadorDeElementos miManejadorDeElementos;
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

    /// <summary>
    /// Evento cuando algún PDI es modificado.
    /// </summary>
    public event EventHandler PdisModificados;

    /// <summary>
    /// Evento cuando alguna Vía es modificada.
    /// </summary>
    public event EventHandler VíasModificadas;
    #endregion

    #region Propiedades
    /// <summary>
    /// Descripción del método AceptarModificaciones().
    /// </summary>
    public readonly static string DescripciónAceptarModificaciones =
      "Acepta las modificaciones a los elementos del mapa. \n" +
      " - Elementos marcados como eliminados son eliminados definitivamente.\n" +
      " - Elementos modificados son re-inicializados borrando el estado de modificados.\n" +
      " - Las listas de errores, duplicados, conflictos, etc. son borradas.";


    /// <summary>
    /// Obtiene el encabezado del mapa.
    /// </summary>
    public ElementoDelMapa Encabezado
    {
      get
      {
        return miEncabezado;
      }
    }


    /// <summary>
    /// Devuelve las Polilíneas.
    /// </summary>
    public IList<Polilínea> Polilíneas
    {
      get
      {
        return misPolilíneas;
      }
    }


    /// <summary>
    /// Devuelve los Polígonos.
    /// </summary>
    public IList<Polígono> Polígonos
    {
      get
      {
        return misPolígonos;
      }
    }


    /// <summary>
    /// Devuelve el manejador de Elementos.
    /// </summary>
    public ManejadorDeElementos ManejadorDeElementos
    {
      get
      {
        return miManejadorDeElementos;
      }
    }


    /// <summary>
    /// Devuelve el manejador de PDIs.
    /// </summary>
    public ManejadorDePdis ManejadorDePdis
    {
      get
      {
        return miManejadorDePdis;
      }
    }


    /// <summary>
    /// Devuelve el manejador de Vías.
    /// </summary>
    public ManejadorDeVías ManejadorDeVías
    {
      get
      {
        return miManejadorDeVías;
      }
    }


    /// <summary>
    /// Filtro de las extensiones válidas.
    /// </summary>
    public static string FiltrosDeExtensiones
    {
      get
      {
        const string filtrosDeExtensiones = "Polish format (*.mp)|*.mp|"
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
      miManejadorDeElementos = new ManejadorDeElementos(this, misElementos, elEscuchadorDeEstatus);
      miManejadorDePdis = new ManejadorDePdis(this, misPdis, elEscuchadorDeEstatus);
      miManejadorDeVías = new ManejadorDeVías(this, misVías, elEscuchadorDeEstatus);
    }


    /// <summary>
    /// Abre un archivo.
    /// </summary>
    /// <param name="elArchivo"></param>
    public void Abrir(string elArchivo)
    {
      miArchivo = elArchivo;
      miEscuchadorDeEstatus.ArchivoActivo = Path.GetFullPath(elArchivo);

      // Por ahora el único formato es el Polish.
      LectorDeFormatoPolish lector = new LectorDeFormatoPolish(this, elArchivo, miEscuchadorDeEstatus);

      IList<ElementoDelMapa> losElementos = lector.ElementosDelMapa;

      CreaMapaNuevo(losElementos);
    }


    /// <summary>
    /// Guarda los cambios.
    /// </summary>
    /// <param name="elArchivo">El archivo a guardar.</param>
    /// <param name="elComentario">El Comentario para la primera línea del archivo.</param>
    public void GuardaEnFormatoPolish(string elArchivo, string elComentario)
    {
      #region Crea las diferentes listas de elementos a guardar.
      // Crea un comentario para ponerlo como el primer elemento de cada archivo.
      ElementoDelMapa comentario = new Comentario(this, 0, "; " + elComentario);
      List<ElementoDelMapa> elementosDelMapa = new List<ElementoDelMapa> { comentario};

      // Las listas especiales necesitan el encabezado para poder ser leídas correctamente
      // por programas como el cGPSmapper, GPSMapEdit, etc.
      ElementoDelMapa[] elementosComunes = new[] {
        comentario,
        miEncabezado };
      List<ElementoDelMapa> elementosEliminados = new List<ElementoDelMapa> (elementosComunes);
      List<ElementoDelMapa> originalDeLosElementosModificados = new List<ElementoDelMapa>(elementosComunes);
      List<ElementoDelMapa> finalDeLosElementosModificados = new List<ElementoDelMapa>(elementosComunes);
      List<ElementoDelMapa> elementosConErrores = new List<ElementoDelMapa>(elementosComunes);
      foreach (ElementoDelMapa elemento in misElementos)
      {
        if (!elemento.FuéEliminado)
        {
          // Si el elemento no fué eliminado entonces se añade a las lista
          // de elementos del mapa.  
          elementosDelMapa.Add(elemento);

          // Si el elemento fué modificado entonces se añade a
          // las listas de elementos modificados. 
          if (elemento.FuéModificado)
          {
            originalDeLosElementosModificados.Add(elemento.Original);
            finalDeLosElementosModificados.Add(elemento);
          }
        }
        else
        {
          // Si el elemento fué eliminado entonces solo se añade a la
          // lista de elemento eliminados.
          elementosEliminados.Add(elemento);
        }
      }
      #endregion

      #region Añade los errores de los distinto manejadores.
      foreach (Pdi pdi in miManejadorDePdis.BuscadorDeErrores.Errores.Keys)
      {
        elementosConErrores.Add(pdi);
      }

      foreach (Vía vía in miManejadorDeVías.BuscadorDeErrores.Errores.Keys)
      {
        elementosConErrores.Add(vía);
      }
      #endregion

      #region Guarda los diferentes archivos.
      // Guarda el mapa nuevo.
      new EscritorDeFormatoPolish(elArchivo, elementosDelMapa, miEscuchadorDeEstatus);

      // Crea el nobre del archivo base.
      string directorio = Path.GetFullPath(Path.GetDirectoryName(elArchivo));
      string nombre = Path.GetFileNameWithoutExtension(elArchivo);
      string archivoBase = Path.Combine(directorio, nombre);

      // Guarda los elementos eliminados.
      if (elementosEliminados.Count > elementosComunes.Length)
      {
        string archivo = archivoBase + ".Eliminados.mp";
        new EscritorDeFormatoPolish(archivo, elementosEliminados, miEscuchadorDeEstatus);
      }

      // Guarda los originales de los elementos modificados.
      if (originalDeLosElementosModificados.Count > elementosComunes.Length)
      {
        string archivo = archivoBase + ".Modificados.Originales.mp";
        new EscritorDeFormatoPolish(archivo, originalDeLosElementosModificados, miEscuchadorDeEstatus);
      }

      // Guarda los finales de los elementos modificados.
      if (finalDeLosElementosModificados.Count > elementosComunes.Length)
      {
        string archivo = archivoBase + ".Modificados.Finales.mp";
        new EscritorDeFormatoPolish(archivo, finalDeLosElementosModificados, miEscuchadorDeEstatus);
      }

      // Guarda los finales de los elementos modificados.
      if (elementosConErrores.Count > elementosComunes.Length)
      {
        string archivo = archivoBase + ".Errores.mp";
        new EscritorDeFormatoPolish(archivo, elementosConErrores, miEscuchadorDeEstatus);
      }
      #endregion

      // Actualiza el archivo activo.
      miArchivo = elArchivo;
      miEscuchadorDeEstatus.ArchivoActivo = elArchivo;
    }


    /// <summary>
    /// Acepta las modificaciones de los elementos.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Elementos eliminados son eliminados completamente.
    /// </para>
    /// <para>
    /// Elementos modificados son re-inicializados borrando el estado
    /// de modificados y la copia original.
    /// </para>
    /// </remarks>
    public void AceptaModificaciones()
    {
      // Genera la nueva lista de elementos del mapa.
      List<ElementoDelMapa> elementosNuevos = new List<ElementoDelMapa>();
      int númeroDeElemento = 1;
      foreach (ElementoDelMapa elemento in misElementos)
      {
        if (!elemento.FuéEliminado)
        {
          elemento.Regenera(númeroDeElemento);
          elementosNuevos.Add(elemento);
          ++númeroDeElemento;
        }
      }

      // Crea el Nuevo mapa con los elementos nuevos.
      CreaMapaNuevo(elementosNuevos);
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
          EnvíaEventoElementosModificados();
          miHayEventosDeModificaciónDeElementoPendientes = false;
        }
        if (miHayEventosDeModificaciónDePdisPendientes)
        {
          EnvíaEventoPdisModificados();
          miHayEventosDeModificaciónDePdisPendientes = false;
        }
        if (miHayEventosDeModificaciónDeVíasPendientes)
        {
          EnvíaEventoVíasModificadas();
          miHayEventosDeModificaciónDeVíasPendientes = false;
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
    internal void SeModificóElemento(ElementoDelMapa elElemento)
    {
      // Si los eventos están suspendidos entonces se indica
      // que hay notificaciones pendientes.
      // Si no, entonces se genera el evento.
      bool esPdi = (elElemento is Pdi);
      bool esVía = (elElemento is Vía);
      if (miNúmeroDeSuspenciónDeEventos > 0)
      {
        miHayEventosDeModificaciónDeElementoPendientes = true;

        // Procesa PDIs y Vías.
        if (esPdi)
        {
          miHayEventosDeModificaciónDePdisPendientes = true;
        }
        else if (esVía)
        {
          miHayEventosDeModificaciónDeVíasPendientes = true;
        }
      }
      else
      {
        EnvíaEventoElementosModificados();

        // Procesa PDIs y Vías.
        if (esPdi)
        {
          EnvíaEventoPdisModificados();
        }
        else if (esVía)
        {
          EnvíaEventoVíasModificadas();
        }
      }
    }


    /// <summary>
    /// Procesa todo lo que se puede procesar.
    /// </summary>
    public void ProcesarTodo()
    {
      int númeroDeElementosModificados = 0;
      númeroDeElementosModificados += miManejadorDePdis.ProcesarTodo();
      númeroDeElementosModificados += miManejadorDeVías.ProcesarTodo();

      miEscuchadorDeEstatus.Estatus = string.Format("Se detectaron {0} problemas", númeroDeElementosModificados);
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Crea un mapa nuevo.
    /// </summary>
    /// <param name="losElementos">Los elementos del mapa nuevo.</param>
    private void CreaMapaNuevo(IEnumerable<ElementoDelMapa> losElementos)
    {
      // Busca el encabezado y crea todas las listas especializadas.
      miEncabezado = null;
      misElementos.Clear();
      misPdis.Clear();
      misVías.Clear();
      misPolilíneas.Clear();
      misPolígonos.Clear();
      foreach (ElementoDelMapa elemento in losElementos)
      {
        misElementos.Add(elemento);

        if (elemento.Clase == "IMG ID")
        {
          miEncabezado = elemento;
        }
        else if (elemento is Pdi)
        {
          misPdis.Add((Pdi)elemento);
        }
        else if (elemento is Vía)
        {
          misVías.Add((Vía)elemento);
        }
        else if (elemento is Polilínea)
        {
          misPolilíneas.Add((Polilínea)elemento);
        }
        else if (elemento is Polígono)
        {
          misPolígonos.Add((Polígono)elemento);
        }
      }

      // Borra el estado de eventos de modificación.
      miHayEventosDeModificaciónDeElementoPendientes = false;

      // Genera el evento de mapa nuevo.
      HayUnMapaNuevo();
    }

    
    /// <summary>
    /// Genera el evento indicando que hay un mapa nuevo.
    /// </summary>
    private void HayUnMapaNuevo()
    {
      if (MapaNuevo != null)
      {
        MapaNuevo(this, new EventArgs());
      }
    }


    /// <summary>
    /// Genera el evento indicando que se modificaron elementos.
    /// </summary>
    private void EnvíaEventoElementosModificados()
    {
      if (ElementosModificados != null)
      {
        ElementosModificados(this, new EventArgs ());
      }
    }


    /// <summary>
    /// Genera el evento indicando que se modificaron PDIs.
    /// </summary>
    private void EnvíaEventoPdisModificados()
    {
      if (PdisModificados != null)
      {
        PdisModificados(this, new EventArgs());
      }
    }


    /// <summary>
    /// Genera el evento indicando que se modificaron Vías.
    /// </summary>
    private void EnvíaEventoVíasModificadas()
    {
      if (VíasModificadas != null)
      {
        VíasModificadas(this, new EventArgs());
      }
    }
    #endregion
  }
}
