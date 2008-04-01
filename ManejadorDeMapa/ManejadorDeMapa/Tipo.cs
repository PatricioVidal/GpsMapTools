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

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa el tipo de un elemento de mapa.
  /// </summary>
  public struct Tipo
  {
    #region Campos
    private readonly int? miSubTipo;
    private readonly int miTipoPrincipal;
    private readonly int miTipoCompleto;
    #endregion

    #region Propiedades
    /// <summary>
    /// Representa un tipo nulo.
    /// </summary>
    public static readonly Tipo TipoNulo = new Tipo(0);


    /// <summary>
    /// Obtiene el Tipo Completo.
    /// </summary>
    /// <remarks>El Tipo Completo corresponde al los dos bytes del Tipo.</remarks>
    public int TipoCompleto
    {
      get
      {
        return miTipoCompleto;
      }
    }


    /// <summary>
    /// Obtiene el Tipo Principal.
    /// </summary>
    /// <remarks>El Tipo Principal corresponde al primer byte del Tipo.</remarks>
    public int TipoPrincipal
    {
      get
      {
        return miTipoPrincipal;
      }
    }


    /// <summary>
    /// Obtiene el Sub-Tipo.
    /// </summary>
    /// <remarks>El Sub-Tipo corresponde al segundo byte del Tipo.</remarks>
    public int SubTipo
    {
      get
      {
        int subTipo = 0;
        if (miSubTipo.HasValue)
        {
          subTipo = (int)miSubTipo;
        }

        return subTipo;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor con un tipo dado.
    /// </summary>
    /// <param name="elTipo">El tipo dado.</param>
    public Tipo(int elTipo)
    {
      bool esUnSoloByte = (elTipo < 256);

      // Contruye los componentes del tipo según el número de bytes.
      if (esUnSoloByte)
      {
        // Por ejempo: 0x0a
        //   Tipo Principal = 0a
        //         Sub Tipo = 00
        //    Tipo Completo = 0a00
        miTipoPrincipal = elTipo;
        miSubTipo = null;
        miTipoCompleto = miTipoPrincipal * 256;
      }
      else
      {
        // Por ejempo: 0x0a03
        //   Tipo Principal = 0a
        //         Sub Tipo = 03
        //    Tipo Completo = 0a03
        miTipoPrincipal = elTipo / 256;
        miSubTipo = (int)Math.IEEERemainder(elTipo, 256);
        miTipoCompleto = elTipo;
      }
    }


    /// <summary>
    /// Constructor con un texto en formato hexadecimal.
    /// </summary>
    /// <param name="elTextoFormatoHexadecimal">El texto en formato hexadecimal.</param>
    public Tipo(string elTextoFormatoHexadecimal)
      : this (Convert.ToInt32(elTextoFormatoHexadecimal, 16))
    {
    }


    /// <summary>
    /// Devuelve el tipo como texto en formato hexadecimal.
    /// </summary>
    public override string ToString()
    {
      string texto = string.Empty;

      // Solo genera el texto para tipos válidos.
      if (miTipoPrincipal > 0)
      {
        texto = "0x" + miTipoPrincipal.ToString("x");

        // Si el Tipo tiene un sub-tipo entonces lo añadimos al texto.
        if (miSubTipo.HasValue)
        {
          texto += ((int)miSubTipo).ToString("x2");
        }
      }

      return texto;
    }


    /// <summary>
    /// Operador de igualdad.
    /// </summary>
    /// <param name="elPrimerTipo">El primer tipo.</param>
    /// <param name="elSegundoTipo">EL segundo tipo.</param>
    public static bool operator ==(
      Tipo elPrimerTipo,
      Tipo elSegundoTipo)
    {
      bool esIgual = (elPrimerTipo.miTipoCompleto == elSegundoTipo.miTipoCompleto);
      return esIgual;
    }


    /// <summary>
    /// Operador de desigualdad.
    /// </summary>
    /// <param name="elPrimerTipo">El primer tipo.</param>
    /// <param name="elSegundoTipo">EL segundo tipo.</param>
    public static bool operator !=(
      Tipo elPrimerTipo,
      Tipo elSegundoTipo)
    {
      return !(elPrimerTipo == elSegundoTipo);
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">El objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Si el objeto es nulo entonces no puede ser igual.
      if (elObjecto == null)
      {
        return false;
      }

      // Si el objecto no es del mismo tipo entonces no puede ser igual.
      if (!(elObjecto is Tipo))
      {
        return false;
      }

      // Compara el objecto.
      Tipo tipo = (Tipo)elObjecto;
      bool esIgual = (this == tipo);

      return esIgual;
    }


    /// <summary>
    /// Deveulve el código para ser usado como identificador único.
    /// </summary>
    public override int GetHashCode()
    {
      return miTipoCompleto;
    }
    #endregion
  }
}
