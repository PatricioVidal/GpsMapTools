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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  public partial class InterfaseMapaDeVíaSeleccionada : InterfaseMapa
  {
    #region
    private ListView miLista;
    private static readonly Pen miLápiz = new Pen(Color.Yellow, 11);
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseMapaDeVíaSeleccionada()
    {
      InitializeComponent();
    }


    /// <summary>
    /// Conecta a una lista.
    /// </summary>
    /// <param name="laLista">La lista.</param>
    /// <remarks>
    /// El Tag de los items de la lista tiene que ser una vía.
    /// </remarks>
    public void Conecta(ListView laLista)
    {
      // Desconectar el evento si ya estabamos conectados a una lista.
      if (miLista != null)
      {
        miLista.SelectedIndexChanged -= EnCambioDeItemsSeleccionados;
        miLista.VirtualItemsSelectionRangeChanged -= EnCambioDeItemsSeleccionados;
      }

      // Esta clase es muy lenta con listas que no están en modo virtual. 
      // Entonces solo permitimos listas virtuales.
      if (!laLista.VirtualMode)
      {
        throw new ArgumentException("La InterfaseMapaDeVíasSeleccionadas solo se puede conectar con listas virtuales.");
      }

      // Conectar el evento a la lista.
      miLista = laLista;
      miLista.SelectedIndexChanged += EnCambioDeItemsSeleccionados;
      miLista.VirtualItemsSelectionRangeChanged += EnCambioDeItemsSeleccionados;
    }
    #endregion

    #region Métodos Privados
    private void EnCambioDeItemsSeleccionados(object laLista, EventArgs losArgumentosDelRatón)
    {
      DibujaVías(miLista.SelectedIndices);
    }


    private void EnCambioDeItemsSeleccionados(object laLista, ListViewVirtualItemsSelectionRangeChangedEventArgs losArgumentos)
    {
      DibujaVías(miLista.SelectedIndices);
    }


    private void DibujaVías(IEnumerable losIndicesSeleccionados)
    {
      List<Vía> vías = new List<Vía>();
      foreach (int indice in losIndicesSeleccionados)
      {
        ListViewItem item = miLista.Items[indice];

        // El Tag del item de la lista tiene que ser una vía.
        Vía vía = item.Tag as Vía;
        if (vía == null)
        {
          throw new InvalidOperationException("El Tag del item de la lista tiene que ser una Vía, pero es: " + vía);
        }

        // Añade la vía a la lista.
        vías.Add(vía);
      }

      if (vías.Count > 0)
      {
        // Busca el rango visible para la vía.
        float margen = 0.0005f;
        RectangleF rectánguloQueEncierra = InterfaseMapa.RectanguloQueEncierra(
          new List<ElementoDelMapa>(vías.ToArray()));
        RectangleF rectánguloVisible = new RectangleF(
          rectánguloQueEncierra.X - margen,
          rectánguloQueEncierra.Y - margen,
          rectánguloQueEncierra.Width + (2 * margen),
          rectánguloQueEncierra.Height + (2 * margen));

        // Dibuja la vías como polilíneas adicional para resaltarla.
        PolilíneasAdicionales.Clear();
        foreach (Vía vía in vías)
        {
          PolilíneasAdicionales.Add(new InterfaseMapa.PolilíneaAdicional(
           vía.Coordenadas, miLápiz));
        }

        // Muestra el mapa en la region deseada.
        Enabled = true;
        RectánguloVisibleEnCoordenadas = rectánguloVisible;
        MuestraTodoElMapa = false;
        Refresh();
      }
    }
    #endregion
  }
}
