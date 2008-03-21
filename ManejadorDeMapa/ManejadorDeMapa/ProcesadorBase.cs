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
