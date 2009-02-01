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

using System.Collections.Generic;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Manejador de Vías.
  /// </summary>
  public class ManejadorDeVías : ManejadorBase<Vía>
  {
    #region Propiedades
    /// <summary>
    /// Descripción de la acción "Procesar Todo".
    /// </summary>
    public const string DescripciónProcesarTodo =
      "Procesa todo lo relacionado con las Vías. Los pasos se hacen en el orden indicado por el número en el menú.";


    /// <summary>
    /// Obtiene el Arreglador de Indices de Ciudad.
    /// </summary>
    public ArregladorDeIndicesDeCiudad ArregladorDeIndicesDeCiudad { get; private set; }


    /// <summary>
    /// Obtiene el Arreglador General.
    /// </summary>
    public ArregladorGeneral ArregladorGeneral { get; private set; }


    /// <summary>
    /// Obtiene el Buscador de Alertas.
    /// </summary>
    public BuscadorDeAlertas BuscadorDeAlertas { get; private set; }


    /// <summary>
    /// Obtiene el Buscador de Posibles Errores de Ruteo.
    /// </summary>
    public BuscadorDePosiblesErroresDeRuteo BuscadorDePosiblesErroresDeRuteo { get; private set; }


    /// <summary>
    /// Obtiene el Buscador de Posibles Nodos Desconectados.
    /// </summary>
    public BuscadorDePosiblesNodosDesconectados BuscadorDePosiblesNodosDesconectados { get; private set; }


    /// <summary>
    /// Obtiene el Buscador de Errores.
    /// </summary>
    public BuscadorDeErrores BuscadorDeErrores { get; private set; }

    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El Manejador de Mapa.</param>
    /// <param name="lasVías">Las Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorDeVías(
      ManejadorDeMapa elManejadorDeMapa,
      IList<Vía> lasVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeMapa, lasVías, elEscuchadorDeEstatus)
    {
      // Crea los procesadores.
      ArregladorDeIndicesDeCiudad = new ArregladorDeIndicesDeCiudad(this, elEscuchadorDeEstatus);
      ArregladorGeneral = new ArregladorGeneral(this, elEscuchadorDeEstatus);
      BuscadorDeErrores = new BuscadorDeErrores(this, elEscuchadorDeEstatus);
      BuscadorDeAlertas = new BuscadorDeAlertas(this, elEscuchadorDeEstatus);
      BuscadorDePosiblesErroresDeRuteo = new BuscadorDePosiblesErroresDeRuteo(this, elEscuchadorDeEstatus);
      BuscadorDePosiblesNodosDesconectados = new BuscadorDePosiblesNodosDesconectados(this, elEscuchadorDeEstatus);

      // Escucha eventos.
      elManejadorDeMapa.VíasModificadas += EnElementosModificados;
    }


    /// <summary>
    /// Procesa todo lo relacionado a Vías.
    /// </summary>
    /// <returns>El número de problemas en Vías.</returns>
    public int ProcesarTodo()
    {
      // Hacer todos las operaciones en orden.
      int númeroDeProblemasEnVías = 0;
      númeroDeProblemasEnVías += ArregladorDeIndicesDeCiudad.Procesa();
      númeroDeProblemasEnVías += ArregladorGeneral.Procesa();
      númeroDeProblemasEnVías += BuscadorDeAlertas.Procesa();
      númeroDeProblemasEnVías += BuscadorDePosiblesErroresDeRuteo.Procesa();
      númeroDeProblemasEnVías += BuscadorDeErrores.Procesa();

      // Reporta estatus.
      EscuchadorDeEstatus.Estatus = "Se detectaron " + númeroDeProblemasEnVías + " problemas en Vías.";

      return númeroDeProblemasEnVías;
    }
    #endregion
  }
}
