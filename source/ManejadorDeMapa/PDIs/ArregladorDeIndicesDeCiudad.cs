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
using SWallTech.Drawing.Shapes;

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Arregla los Indices de Ciudad de los PDIs.
  /// </summary>
  public class ArregladorDeIndicesDeCiudad : ProcesadorBase<ManejadorDePdis, Pdi>
  {
    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public const string Descripción =
      "Arregla el Indice de Ciudad (CityIdx) de los PDIs.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePdis">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorDeIndicesDeCiudad(
      ManejadorDePdis elManejadorDePdis,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePdis, elEscuchadorDeEstatus)
    {
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPdi">El PDI.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Pdi elPdi)
    {
      // Retorna si el PDI es una ciudad.
      if (elPdi.EsCiudad)
      {
        return 0;
      }

      int númeroDeProblemasDetectados = 0;

      // Por cada ciudad, si el PDI está adentro de la ciudad entonces
      // se le actualiza el Indice de Ciudad.
      Ciudad ciudadDelPdi = null;
      Ciudad estadoDelPdi = null;
      foreach (Ciudad ciudad in ManejadorDeMapa.Ciudades)
      {
        PolygonF polígono = new PolygonF(ciudad.CoordenadasComoPuntos);
        if (polígono.Contains(elPdi.Coordenadas))
        {
          // Tipo 0x4a representa un Estado.
          if (ciudad.Tipo.Value.TipoPrincipal == 0x4a)
          {
            estadoDelPdi = ciudad;
          }
          else
          {
            // El PDI solo puede pertenecer a una sola ciudad.
            ciudadDelPdi = ciudad;
            break;
          }
        }
      }
      if (ciudadDelPdi != null)
      {
        bool cambió = elPdi.ActualizaCampoIndiceDeCiudad(
          ciudadDelPdi.Indice,
          string.Format(Properties.Recursos.M000, ciudadDelPdi));
        if (cambió)
        {
          ++númeroDeProblemasDetectados;
        }
      }
      else if (estadoDelPdi != null)
      {
        bool cambió = elPdi.ActualizaCampoIndiceDeCiudad(
          estadoDelPdi.Indice,
          string.Format(Properties.Recursos.M008, estadoDelPdi));
        if (cambió)
        {
          ++númeroDeProblemasDetectados;
        }
      }
      else
      {
        bool cambió = elPdi.RemueveCampoIndiceDeCiudad(Properties.Recursos.M001);
        if (cambió)
        {
          ++númeroDeProblemasDetectados;
        }
      }

      return númeroDeProblemasDetectados;
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
