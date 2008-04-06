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
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Representa una Vía.
  /// </summary>
  public class Vía : Polilínea
  {
    #region Campos
    private CampoParámetrosDeRuta miCampoParámetrosDeRuta = CampoParámetrosDeRuta.Nulo;
    #endregion 

    #region Propiedades
    /// <summary>
    /// Clase de Ruta nula.
    /// </summary>
    public static readonly Vía Nula = new Vía(null, 0, string.Empty, new List<Campo> ());


    /// <summary>
    /// Obtiene el Límite de Velocidad.
    /// </summary>
    public LímiteDeVelocidad LímiteDeVelocidad
    {
      get
      {
        return miCampoParámetrosDeRuta.LímiteDeVelocidad;
      }
    }


    /// <summary>
    /// Obtiene la Clase de Ruta.
    /// </summary>
    public ClaseDeRuta ClaseDeRuta
    {
      get
      {
        return miCampoParámetrosDeRuta.ClaseDeRuta;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número de la Polilínea.</param>
    /// <param name="laClase">La clase de la Polilínea.</param>
    /// <param name="losCampos">Los campos de la Polilínea.</param>
    public Vía(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa,
             elNúmero,
             laClase,
             losCampos)
    {
      // Busca los campos específicos de las vías.
      foreach (Campo campo in losCampos)
      {
        if (campo is CampoParámetrosDeRuta)
        {
          miCampoParámetrosDeRuta = (CampoParámetrosDeRuta)campo;
        }
      }
    }


    /// <summary>
    /// Cambia el Límite de Velocidad.
    /// </summary>
    /// <param name="elLímiteDeVelocidadNuevo">El Límite de Velocidad nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaLímiteDeVelocidad(LímiteDeVelocidad elLímiteDeVelocidadNuevo, string laRazón)
    {
      // Si el Límite de Velocidad es nulo entonces la Clase de Ruta
      // también debe ser nula.  En este case ponemos la Clase de Ruta estandar.
      CampoParámetrosDeRuta campoNuevo;
      if (miCampoParámetrosDeRuta.LímiteDeVelocidad.EsNulo())
      {
        ClaseDeRuta claseDeRutaEstandard = RestriccionesDeParámetrosDeRuta.ClasesDeRuta[Tipo];
        campoNuevo = new CampoParámetrosDeRuta(
          elLímiteDeVelocidadNuevo,
          claseDeRutaEstandard);
      }
      else
      {
        campoNuevo = new CampoParámetrosDeRuta(
          elLímiteDeVelocidadNuevo,
          miCampoParámetrosDeRuta.ClaseDeRuta);
      }

      // Cambia el campo.
      base.CambiaCampo(campoNuevo, miCampoParámetrosDeRuta, laRazón);
      miCampoParámetrosDeRuta = campoNuevo;
    }


    /// <summary>
    /// Cambia la Clase de Ruta
    /// </summary>
    /// <param name="laClaseDeRutaNueva">La Clase de Ruta nueva.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaClaseDeRuta(ClaseDeRuta laClaseDeRutaNueva, string laRazón)
    {
      // Si la Clase de Ruta es nula entonces el Límite de Velocidad
      // también debe ser nulo.  En este case ponemos el Límite de 
      // Velocidad estandar.
      CampoParámetrosDeRuta campoNuevo;
      if (miCampoParámetrosDeRuta.ClaseDeRuta.EsNula())
      {
        LímiteDeVelocidad límiteDeVelocidadEstandard = RestriccionesDeParámetrosDeRuta.LímitesDeVelocidad[Tipo];
        campoNuevo = new CampoParámetrosDeRuta(
          límiteDeVelocidadEstandard,
          laClaseDeRutaNueva);
      }
      else
      {
        campoNuevo = new CampoParámetrosDeRuta(
          miCampoParámetrosDeRuta.LímiteDeVelocidad,
          laClaseDeRutaNueva);
      }

      // Cambia el campo.
      base.CambiaCampo(campoNuevo, miCampoParámetrosDeRuta, laRazón);
      miCampoParámetrosDeRuta = campoNuevo;
    }
    #endregion
  }
}
