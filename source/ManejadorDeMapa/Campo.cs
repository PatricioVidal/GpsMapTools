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

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un campo de un elemento de mapa.
  /// </summary>
  public abstract class Campo
  {
    #region Propiedades
    /// <summary>
    /// Devuelve el identificador del campo.
    /// </summary>
    public string Identificador { get; private set; }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elIdentificador">El identificador del campo.</param>
    protected Campo(string elIdentificador)
    {
      Identificador = elIdentificador;
    }


    /// <summary>
    /// Operador de igualdad.
    /// </summary>
    /// <param name="elPrimerElemento">El Primer Elemento.</param>
    /// <param name="elSegundoElemento">El Segundo Elemento.</param>
    public static bool operator ==(
      Campo elPrimerElemento,
      Campo elSegundoElemento)
    {
      bool esIgual = Equals(elPrimerElemento, elSegundoElemento);

      return esIgual;
    }


    /// <summary>
    /// Operador de desigualdad.
    /// </summary>
    /// <param name="elPrimerElemento">El Primer Elemento.</param>
    /// <param name="elSegundoElemento">El Segundo Elemento.</param>
    public static bool operator !=(
      Campo elPrimerElemento,
      Campo elSegundoElemento)
    {
      return !Equals(elPrimerElemento, elSegundoElemento);
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">El objecto dado.</param>
    public abstract override bool Equals(object elObjecto);


    /// <summary>
    /// Obtiene una clave única para este objecto.
    /// </summary>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Método GetHashCode() no está implementado.");
    }

    
    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override abstract string ToString();

    #endregion
  }
}
