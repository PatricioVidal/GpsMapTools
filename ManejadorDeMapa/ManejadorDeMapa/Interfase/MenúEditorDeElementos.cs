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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;
using System.IO;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Menú para editar elementos.
  /// </summary>
  public partial class MenúEditorDeElementos : ContextMenuStrip
  {
    #region Campos
    private ToolStripMenuItem miMenúNúmeroDeElementosSeleccionados = new ToolStripMenuItem();
    private ListView miLista = null; 
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando se editan elementos.
    /// </summary>
    public event EventHandler Editó;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la lista con los elementos del mapa.
    /// </summary>
    /// <remarks>
    /// Cada Tag de los items de la lista tiene que ser un elemento de mapa.
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
        miLista = value;

        if (miLista != null)
        {
          // Desconecta el menú si ya estabamos conectados a una lista.
          if (miLista != null)
          {
            miLista.ContextMenuStrip = null;
          }

          // Conecta el menú a la lista.
          miLista.ContextMenuStrip = this;
        }
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public MenúEditorDeElementos()
    {
      InitializeComponent();

      // Inicialización.
      Name = "miMenuDeContexto";
      Size = new System.Drawing.Size(153, 48);
      AutoSize = true;

      // Añade los menús.
      Items.Add(miMenúNúmeroDeElementosSeleccionados);
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Envía evento Editó.
    /// </summary>
    protected void EnvíaEventoEditó()
    {
      // Envía el evento indicando que se editaron elementos.
      if (Editó != null)
      {
        Editó(this, new EventArgs());
      }
    }


    private void EnMenúAbriendo(object sender, CancelEventArgs e)
    {
      // Pone el número de elementos seleccionados en el primer menú para informar
      // al usuario.
      int númeroDeItemsSeleccionados = miLista.SelectedIndices.Count;
      miMenúNúmeroDeElementosSeleccionados.Text = "[ Hay " + númeroDeItemsSeleccionados + " elementos seleccionados ]";
      miMenúNúmeroDeElementosSeleccionados.Enabled = false;
    }
    #endregion
  }
}
