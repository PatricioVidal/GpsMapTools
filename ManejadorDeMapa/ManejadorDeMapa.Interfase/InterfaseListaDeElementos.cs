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
using System.Windows.Forms;
using System.Globalization;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Interfase de Lista de elementos.
  /// </summary>
  public partial class InterfaseListaDeElementos : ListView
  {
    #region Campos
    private readonly List<ListViewItem> misItemsVirtuales = new List<ListViewItem>();
    private LlenadorDeItems miLlenadorDeItems;
    #endregion

    #region Propiedades
    /// <summary>
    /// Variable lógica que indica si la lista está en modo virtual.
    /// </summary>
    [DefaultValue(true)]
    public new bool VirtualMode
    {
      get
      {
        return base.VirtualMode;
      }

      set
      {
        base.VirtualMode = value;
      }
    }


    /// <summary>
    /// Obtiene el número de elementos en la lista.
    /// </summary>
    public int NúmeroDeElementos
    {
      get
      {
        int númeroDeElementos;

        // Si la lista esta en modo virtual entonces el número de elementos
        // está dado por los items virtuales.
        if (VirtualMode)
        {
          númeroDeElementos = misItemsVirtuales.Count;
        }
        else
        {
          númeroDeElementos = Items.Count;
        }

        return númeroDeElementos;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseListaDeElementos()
    {
      InitializeComponent();

      miOrdenadorDeColumnaDeLista.ItemsDeLaListaVirtual = misItemsVirtuales;
    }


    /// <summary>
    /// Método que llena los items.
    /// </summary>
    /// <param name="laLista">La lista.</param>
    /// <remarks>
    /// <para>
    /// Los items se añaden a la lista llamando a AñadeItem(...).
    /// llamar a <see cref="RegeneraLista"/>.
    /// </para>
    /// <para>
    /// El método que llena los items tiene que ser asignado antes de
    /// llamar a <see cref="RegeneraLista"/>.
    /// </para>
    /// </remarks>
    public delegate void LlenadorDeItems(InterfaseListaDeElementos laLista);


    /// <summary>
    /// Pone el método Llenador de Items.
    /// </summary>
    /// <param name="elLlenadorDeItems">El método Llenador de Items.</param>
    public virtual void PoneLlenadorDeItems(LlenadorDeItems elLlenadorDeItems)
    {
      miLlenadorDeItems = elLlenadorDeItems;
    }


    /// <summary>
    /// Regenera la lista de elementos.
    /// </summary>
    public virtual void RegeneraLista()
    {
      // Lanza una excepción si no hay un llenador de lista.
      if (miLlenadorDeItems == null)
      {
        throw new InvalidOperationException("No hay un Llenador de Items.  Llame ConectaLlenadorDeItems() antes de llamar LlenaLista().");
      }

      // Desabilita la listas.
      Enabled = false;

      // Llena los items dependiendo del modo Virtual de la lista.
      if (VirtualMode)
      {
        // Inicializa los items.
        misItemsVirtuales.Clear();

        // Llama al llenador de la lista.
        miLlenadorDeItems(this);

        // Pone el número de elementos virtuales y habilita la lista.
        VirtualListSize = misItemsVirtuales.Count;
      }
      else
      {
        // Inicializa los items y grupos.
        Items.Clear();
        Groups.Clear();
        
        // Llama al llenador de la lista.
        miLlenadorDeItems(this);
      }

      // Habilita la lista.
      Enabled = true;
    }


    /// <summary>
    /// Añade un item a la lista.
    /// </summary>
    /// <param name="elElementoConEtiqueta">El elemento dado.</param>
    /// <param name="losSubItemsAdicionales">Los textos de los subitems adicionales</param>
    public void AñadeItem(ElementoConEtiqueta elElementoConEtiqueta, params string[] losSubItemsAdicionales)
    {
      AñadeItem(elElementoConEtiqueta, BackColor, null, losSubItemsAdicionales);
    }


    /// <summary>
    /// Añade un item a la lista.
    /// </summary>
    /// <param name="elElementoConEtiqueta">El elemento dado.</param>
    /// <param name="elGrupo">El grupo.</param>
    /// <param name="losSubItemsAdicionales">Los textos de los subitems adicionales</param>
    public void AñadeItem(ElementoConEtiqueta elElementoConEtiqueta, ListViewGroup elGrupo, params string[] losSubItemsAdicionales)
    {
      AñadeItem(elElementoConEtiqueta, BackColor, elGrupo, losSubItemsAdicionales);
    }


    /// <summary>
    /// Añade un item a la lista.
    /// </summary>
    /// <param name="elElementoConEtiqueta">El elemento dado.</param>
    /// <param name="elColorDeFondo">El color de fondo.</param>
    /// <param name="losSubItemsAdicionales">Los textos de los subitems adicionales</param>
    public void AñadeItem(ElementoConEtiqueta elElementoConEtiqueta, Color elColorDeFondo, params string[] losSubItemsAdicionales)
    {
      AñadeItem(elElementoConEtiqueta, elColorDeFondo, null, losSubItemsAdicionales);
    }


    /// <summary>
    /// Añade un item a la lista.
    /// </summary>
    /// <param name="elElementoConEtiqueta">El elemento dado.</param>
    /// <param name="elColorDeFondo">El color de fondo.</param>
    /// <param name="elGrupo">El grupo.</param>
    /// <param name="losSubItemsAdicionales">Los textos de los subitems adicionales</param>
    public virtual void AñadeItem(ElementoConEtiqueta elElementoConEtiqueta, Color elColorDeFondo, ListViewGroup elGrupo, params string[] losSubItemsAdicionales)
    {
      ElementoDelMapa elemento = elElementoConEtiqueta.ElementoDelMapa;
      List<string> subItems = new List<string> {
                elemento.Número.ToString(CultureInfo.CurrentCulture).PadLeft(6),
                elemento.Tipo.ToString(), 
                elemento.Descripción,
                elemento.Nombre};
      subItems.AddRange(losSubItemsAdicionales);

      ListViewItem item = new ListViewItem(subItems.ToArray()) {
          BackColor = elColorDeFondo,
          Group = elGrupo,
          Tag = elElementoConEtiqueta
        };

      if (VirtualMode)
      {
        if (elGrupo != null)
        {
          throw new ArgumentException("No se pueden añadir grupos a lista virtuales. Ponga VirtualMode = false");
        }

        misItemsVirtuales.Add(item);
      }
      else
      {
        Items.Add(item);
      }
    }
    #endregion

    #region Métodos Privados
    private void ObtieneItemDeLista(object elEnviador, RetrieveVirtualItemEventArgs elArgumento)
    {
      elArgumento.Item = misItemsVirtuales[elArgumento.ItemIndex];
    }
    #endregion
  }
}
