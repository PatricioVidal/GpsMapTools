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
using Tst;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Buscador de incongruencias en Vías.
  /// </summary>
  public class BuscadorDeIncongruencias : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly IList<IList<Vía>> misIncongruencias;
    private readonly List<Vía> misVíasYaProcesadas = new List<Vía>();
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción = "Busca incongruencias en las Vías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeIncongruencias(
      ManejadorDeVías elManejadorDeVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeVías, elEscuchadorDeEstatus)
    {
      misIncongruencias = elManejadorDeVías.Incongruencias;
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override void ComenzóAProcesar()
    {
      misIncongruencias.Clear();
      misVíasYaProcesadas.Clear();

      base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa na Vía.
    /// </summary>
    /// <param name="laVía">La Vía.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(Vía laVía)
    {
      // Retorna si la Vía ya ha sido identificado como incongruencia.
      if (misVíasYaProcesadas.Contains(laVía))
      {
        return false;
      }

      // Retorna si la Vía np tiene nombre.
      if (laVía.Nombre == string.Empty)
      {
        return false;
      }

      // Busca las Vías que tengan el mismo nombre desde la posición n + 1 y que
      // no estén eliminadas.
      List<Vía> víasConElMismoNombre = new List<Vía> ();
      bool hayIncongruencias = false;
      for (int i = NúmeroDeElementoProcesándose; i < NúmeroDeElementos; ++i)
      {
        Vía vía = this[i];
        if (!vía.FuéEliminado &&
          (vía.Nombre == laVía.Nombre))
        {
          víasConElMismoNombre.Add(vía);
          misVíasYaProcesadas.Add(vía);
        }
      }

      // Busca incongruencias si tenemos vías con el mismo nombre.
      if (víasConElMismoNombre.Count > 0)
      {
        LímiteDeVelocidad límiteDeVelocidadDeReferencia = laVía.LímiteDeVelocidad;
        ClaseDeRuta claseDeRutaDeReferencia = laVía.ClaseDeRuta;

        foreach (Vía víaConElMismoNombre in víasConElMismoNombre)
        {
          #region Procesa Límite de Velocidad.
          LímiteDeVelocidad límiteDeVelocidad = víaConElMismoNombre.LímiteDeVelocidad;
          if (límiteDeVelocidadDeReferencia.EsNulo())
          {
            if (!límiteDeVelocidad.EsNulo())
            {
              hayIncongruencias = true;
              break;
            }
          }
          else if (límiteDeVelocidad.EsNulo())
          {
            hayIncongruencias = true;
            break;
          }
          else
          {
            // Calcula la diferencia del índice de Límite de Velocidad.
            int diferenciaDelIndice = Math.Abs(límiteDeVelocidad.Indice - límiteDeVelocidadDeReferencia.Indice);

            // Si las diferencia es mayor del límite entonces
            // hay incongruencias.
            if (diferenciaDelIndice > 2)
            {
              hayIncongruencias = true;
              break;
            }
          }
          #endregion

          #region Procesa Clase de Ruta.
          ClaseDeRuta claseDeRuta = víaConElMismoNombre.ClaseDeRuta;
          if (claseDeRutaDeReferencia.EsNula())
          {
            if (!claseDeRuta.EsNula())
            {
              hayIncongruencias = true;
              break;
            }
          }
          else if (claseDeRuta.EsNula())
          {
            hayIncongruencias = true;
            break;
          }
          else
          {
            // Calcula la diferencia del índice de Límite de Velocidad.
            int diferenciaDeIndice = Math.Abs(claseDeRuta.Indice - claseDeRutaDeReferencia.Indice);

            // Si las diferencia es mayor del límite entonces
            // hay incongruencias.
            if (diferenciaDeIndice > 2)
            {
              hayIncongruencias = true;
              break;
            }
          }
          #endregion
        }
      }

      // Si se detectaron incongruencias entonces añadimos todas las
      // vías a las incongruencias.
      if (hayIncongruencias)
      {
        List<Vía> vías = new List<Vía> { laVía };
        vías.AddRange(víasConElMismoNombre.ToArray());

        misIncongruencias.Add(vías);
      }

      // Este método no modifica elementos.
      bool seModificóElemento = false;
      return seModificóElemento;
    }


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected override void TerminoDeProcesar()
    {
      base.TerminoDeProcesar();

      // Reporta estatus.
      Estatus = "Vías con Incongruencias: " + misIncongruencias.Count;
    }
    #endregion
  }
}
