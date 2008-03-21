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
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Ordenador de columans.
  /// </summary>
  class OrdenadorDeColumnas : IComparer
  {
    #region Campos
    private readonly ListView miLista;
    private int miColumnaAOrdenar = -1;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="laLista">La lista a ordenar.</param>
    public OrdenadorDeColumnas(ListView laLista)
    {
      miLista = laLista;

      laLista.ListViewItemSorter = this;
      miLista.ColumnClick += EnClickDeLaColumna;
    }

    public int Compare(object elPrimerObjeto, object elSegundoObjecto)
    {
      // No ordenamos hasta que el usuario click en una columna.
      if (miColumnaAOrdenar < 0)
      {
        return 0;
      }

      ListViewItem primerItem = (ListViewItem)elPrimerObjeto;
      ListViewItem segundoItem = (ListViewItem)elSegundoObjecto;

      // Compara los texto de la columna a ordenar.
      int comparasión = String.Compare(
        primerItem.SubItems[miColumnaAOrdenar].Text,
        segundoItem.SubItems[miColumnaAOrdenar].Text);

      // El signo del resultado depende de como queremos ordenar la lista.
      int resultado = 0;
      switch (miLista.Sorting)
      {
        case SortOrder.Ascending:
          resultado = comparasión;
          break;
        case SortOrder.Descending:
          resultado = -comparasión;
          break;
      }

      return resultado;
    }
    #endregion


    #region Métodos Privados
    private void EnClickDeLaColumna(object elEnviador, ColumnClickEventArgs losArgumentos)
    {
      int columnaSeleccionada = losArgumentos.Column;

      // Si es la misma columna entonces cambiamos el sentido del orden.
      // Si no, entonces ordemans de menor a mayor.
      if (miColumnaAOrdenar == columnaSeleccionada)
      {
        // Cambiamos el sentido.
        switch (miLista.Sorting)
        {
          case SortOrder.Ascending:
            miLista.Sorting = SortOrder.Descending;
            break;
          case SortOrder.Descending:
            miLista.Sorting = SortOrder.Ascending;
            break;
        }
      }
      else
      {
        miColumnaAOrdenar = columnaSeleccionada;
        miLista.Sorting = SortOrder.Ascending;
      }

      // Ordena la lista.
      miLista.Sort();
    }
    #endregion
  }
}
