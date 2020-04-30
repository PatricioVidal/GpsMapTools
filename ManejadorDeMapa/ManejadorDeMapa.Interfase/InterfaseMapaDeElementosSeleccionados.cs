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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Interfase de Mapa de Vías seleccionadas.
  /// </summary>
  public partial class InterfaseMapaDeElementosSeleccionados : InterfaseMapa
  {
    #region Campos
    private ListView miLista;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando se dibujaron los elementos en el mapa.
    /// </summary>
    public event EventHandler DibujóElementos;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la lista con los elementos del mapa.
    /// </summary>
    /// <remarks>
    /// Cada Tag de los items de la lista tiene que ser una Vía.
    /// </remarks>
    [Browsable(true)]
    public ListView Lista
    {
      get
      {
        return miLista;
      }
      set
      {
        // Desconectar el evento si ya estabamos conectados a una lista.
        if (miLista != null)
        {
          miLista.SelectedIndexChanged -= EnCambioDeItemsSeleccionados;
          miLista.VirtualItemsSelectionRangeChanged -= EnCambioDeItemsSeleccionados;
        }

        // Conectar el evento a la lista.
        miLista = value;
        miLista.SelectedIndexChanged += EnCambioDeItemsSeleccionados;
        miLista.VirtualItemsSelectionRangeChanged += EnCambioDeItemsSeleccionados;
      }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseMapaDeElementosSeleccionados()
    {
      InitializeComponent();
    }
    #endregion


    #region Métodos Públicos
    /// <summary>
    /// Dibuja el mapa resaltando los elementos seleccionados.
    /// </summary>
    public void DibujaElementos()
    {
      // Nos salimos si no hay elementos seleccionados.
      if (miLista.SelectedIndices.Count == 0)
      {
        MuestraTodoElMapa = true;
        return;
      }

      // Desabilita el mapa.
      Enabled = false;

      List<ElementoDelMapa> elementos = new List<ElementoDelMapa>();
      foreach (int indice in miLista.SelectedIndices)
      {
        ListViewItem item = miLista.Items[indice];

        // El Tag del item de la lista tiene que ser una vía.
        ElementoConEtiqueta elementoConEtiqueta = item.Tag as ElementoConEtiqueta;
        if (elementoConEtiqueta == null)
        {
          throw new InvalidOperationException("El Tag del item de la lista tiene que ser una ElementoConEtiqueta, pero es: " + item.Tag.GetType());
        }

        // Añade el elemento a la lista.
        elementos.Add(elementoConEtiqueta.ElementoDelMapa);
      }

      // Busca el rango visible para los elementos.
      const float margen = 0.0005f;
      RectangleF rectánguloQueEncierra = RectanguloQueEncierra(elementos);
      RectangleF rectánguloVisible = new RectangleF(
        rectánguloQueEncierra.X - margen,
        rectánguloQueEncierra.Y - margen,
        rectánguloQueEncierra.Width + (2 * margen),
        rectánguloQueEncierra.Height + (2 * margen));

      DibujaObjectosAdicionales(elementos);

      // Muestra el mapa en la region deseada.
      RectánguloVisibleEnCoordenadas = rectánguloVisible;
      MuestraTodoElMapa = false;

      // Envía el evento.
      if (DibujóElementos != null)
      {
        DibujóElementos(this, new EventArgs());
      }
      
      // Dibuja el mapa.
      Enabled = true;
      Refresh();
    }
    #endregion

    #region Métodos Privados
    private void EnCambioDeItemsSeleccionados(object laLista, EventArgs losArgumentos)
    {
      // Comienza el timer.
      // Este manejador de eventos está implementado con un timer porque a veces
      // se generan muchos eventos consecutivos y no es necesario mostrar
      // el mapa para todos ellos.
      miTimerCambioDeItemsSeleccionados.Stop();
      miTimerCambioDeItemsSeleccionados.Start();
    }


    private void EnCambioDeItemsSeleccionados(object laLista, ListViewVirtualItemsSelectionRangeChangedEventArgs losArgumentos)
    {
      EnCambioDeItemsSeleccionados(laLista, (EventArgs)losArgumentos);
    }


    private void EnTimerCambioDeItemsSeleccionadosTick(object sender, EventArgs e)
    {
      // Detiene el timer para no seguir dibujando el mapa repetidamente.
      miTimerCambioDeItemsSeleccionados.Stop();

      DibujaElementos();
    }


    /// <summary>
    /// Dibuja los objectos adicionales en el mapa. 
    /// </summary>
    /// <param name="losElementos">Los elementos seleccionados.</param>
    protected virtual void DibujaObjectosAdicionales(IList<ElementoDelMapa> losElementos)
    {
      throw new NotImplementedException("La clase derivada debe implementar el método DibujaObjectossAdicionales(IList<ElementoDelMapa> losElementos)");
    }
    #endregion
  }
}
