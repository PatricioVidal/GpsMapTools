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

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de Es Ciudad.
  /// </summary>
  public class CampoEsCiudad : Campo
  {
    #region Propiedades
    /// <summary>
    /// Identificador.
    /// </summary>
    public const string IdentificadorDeEtiqueta = "City";

    /// <summary>
    /// Devuelve una variable l�gica que indica si es una ciudad.
    /// </summary>
    public bool EsCiudad { get; private set; }

    #endregion

    #region M�todos P�blicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elEsCiudad">Variable l�gica que indica si es una ciudad.</param>
    public CampoEsCiudad(bool elEsCiudad)
      : base(IdentificadorDeEtiqueta)
    {
      EsCiudad = elEsCiudad;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      return EsCiudad.ToString();
    }


    /// <summary>
    /// Devuelve una variable l�gica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">EL objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Si el objeto es nulo entonces no puede ser igual.
      if (elObjecto == null)
      {
        return false;
      }

      // Si el objecto no es del mismo tipo entonces no puede ser igual.
      if (!(elObjecto is CampoEsCiudad))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoEsCiudad comparador = (CampoEsCiudad)elObjecto;
      bool esIgual = (EsCiudad == comparador.EsCiudad);

      return esIgual;
    }
    #endregion
  }
}
