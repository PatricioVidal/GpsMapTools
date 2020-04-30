#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
// (For English scroll down.)
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
// Visita https://github.com/PatricioVidal/GpsMapTools para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Autor: Patricio Vidal.
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
// Visit https://github.com/PatricioVidal/GpsMapTools for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Author: Patricio Vidal.
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
using GpsYv.ManejadorDeMapa.Properties;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Remplaza los Nombres de las Vías.
  /// </summary>
  public class RemplazadorDeNombresDeVias : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly IOpenFileDialogService myOpenFileDialogService;
    private IList<ElementoDelMapa> mySourceElements;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Gets or sets the minimum distance in meters for road matching.
    /// </summary>
    public double MinimumDistanceInMeters { get; set; }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    /// <param name="elServicioDiálogoAbrirArchivos">El servicio de diálogo para abrir archivos.</param>
    public RemplazadorDeNombresDeVias(
      ManejadorDeVías elManejadorDeVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus,
      IOpenFileDialogService elServicioDiálogoAbrirArchivos)
      : base(elManejadorDeVías, elEscuchadorDeEstatus)
    {
      myOpenFileDialogService = elServicioDiálogoAbrirArchivos;
      MinimumDistanceInMeters = 100;
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override bool ComenzóAProcesar()
    {
      // Ask for the file.
      string file = myOpenFileDialogService.OpenFileDialog();

      // Return early if the user cancels.
      if (string.IsNullOrEmpty(file))
      {
        return false;
      }

      var sourceMapManager = new ManejadorDeMapa(EscuchadorDeEstatus);
      var reader = new LectorDeFormatoPolish(sourceMapManager, file);
      mySourceElements = reader.ElementosDelMapa;
      return base.ComenzóAProcesar();
    }

    /// <summary>
    /// Process one road.
    /// </summary>
    /// <param name="theRoad">The road.</param>
    /// <returns>The number of changes made to the element.</returns>
    protected override int ProcesaElemento(Vía theRoad)
    {
      int numberOfChanges = 0;
      string targetName = null;
      double minimumDistance = double.NaN;

      // For each road we look for a match.
      var currentName = theRoad.Nombre.Trim();

      // We only consider non-empty names.
      if (!string.IsNullOrEmpty(currentName))
      {
        foreach (ElementoDelMapa element in mySourceElements)
        {
          var sourceRoad = element as Vía;
          if (sourceRoad != null)
          {
            // The first match is the road name.
            var sourceName = sourceRoad.Nombre.Trim();
            if (!string.IsNullOrEmpty(sourceName))
            {
              if (sourceName.StartsWith(currentName, StringComparison.InvariantCultureIgnoreCase))
              {
                // We may have a match.
                // Now we look for coordinates.
                var start = theRoad.Coordenadas[0];
                var sourceStart = sourceRoad.Coordenadas[0];
                double distance = Coordenadas.DistanciaEnMetros(start, sourceStart);
                if ((distance < MinimumDistanceInMeters) && !(distance >= minimumDistance))
                {
                  minimumDistance = distance;
                  targetName = sourceName;
                }
              }
            }
          }
        }
      }

      if (targetName != null)
      {
        bool cambió = theRoad.ActualizaNombre(
          targetName,
          string.Format(Recursos.M108, string.Format("{0:0.0 m.}", minimumDistance)));
        if (cambió)
        {
          ++numberOfChanges;
        }
      }

      return numberOfChanges;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }
    #endregion
  }
}
