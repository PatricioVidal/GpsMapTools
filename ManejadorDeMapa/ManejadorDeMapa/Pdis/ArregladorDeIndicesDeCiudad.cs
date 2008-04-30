#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
// (For English, see further down.)
//
// GpsYv.ManejadorDeMapa es una aplicaci�n para manejar Mapas de GPS en el
// formato Polish (.mp).  Esta escrito en C# usando el .NET Framework 3.5. 
//
// Esta programa naci� por la necesidad del Grupo GPS de Venezuela, 
// GPS_YV (http://www.gpsyv.net), de analizar y corregir los mapas que el
// grupo genera para la comunidad.  GpsYv.ManejadorDeMapa se distribuye bajo 
// la licencia GPL con la finalidad de que sea �til para otros grupos o
// individuos que hacen mapas, y tambi�n para promover la colaboraci�n 
// con este proyecto.
//
// Visita http://www.codeplex.com/GPSYVManejadorDeMapa para m�s informaci�n.
//
// La l�gica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Programador: Patricio Vidal (PatricioV2@hotmail.com)
//
// Este programa es software libre. Puede redistribuirlo y/o modificarlo
// bajo los t�rminos de la Licencia P�blica General de GNU seg�n es publicada
// por la Free Software Foundation, bien de la versi�n 2 de dicha Licencia o 
// bien (seg�n su elecci�n) de cualquier versi�n posterior. 
//
// Este programa se distribuye con la esperanza de que sea �til, 
// pero SIN NINGUNA GARANT�A, incluso sin la garant�a MERCANTIL
// impl�cita o sin garantizar la CONVENIENCIA PARA UN PROP�SITO PARTICULAR.
// V�ase la Licencia P�blica General de GNU para m�s detalles. 
//
// Deber�a haber recibido una copia de la Licencia P�blica General 
// junto con este programa. Si no ha sido as�, escriba a la 
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
using SWallTech.Drawing.Shapes;

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Arregla los Indices de Ciudad de los PDIs.
  /// </summary>
  public class ArregladorDeIndicesDeCiudad : ProcesadorBase<ManejadorDePdis, Pdi>
  {
    #region M�todos P�blicos
    /// <summary>
    /// Descripci�n de �ste procesador.
    /// </summary>
    public static readonly string Descripci�n =
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

    #region M�todos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPdi">El PDI.</param>
    /// <returns>El n�mero de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Pdi elPdi)
    {
      int n�meroDeProblemasDetectados = 0;

      // Por cada ciudad, si el PDI est� adentro de la ciudad entonces
      // se le actualiza el Indice de Ciudad.
      foreach(Ciudad ciudad in Manejador.ManejadorDeMapa.Ciudades)
      {
        PolygonF pol�gono = new PolygonF(ciudad.CoordenadasComoPuntos);
        if (pol�gono.Contains(elPdi.Coordenadas))
        {
          bool cambi� = elPdi.ActualizaCampoIndiceDeCiudad(
            ciudad.Indice,
            string.Format("El PDI pertenece a la Ciudad {0}", ciudad));
          if (cambi�)
          {
            ++n�meroDeProblemasDetectados;
          }

          // El PDI solo puede pertenecer a una sola ciudad.
          break;
        }
      }

      return n�meroDeProblemasDetectados;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que env�a el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aqu�.
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que env�a el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aqu�.
    }
    #endregion
  }
}
