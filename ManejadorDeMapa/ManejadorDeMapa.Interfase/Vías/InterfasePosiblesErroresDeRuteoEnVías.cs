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
using System.Drawing;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Interfase de Vías con incongruencias.
  /// </summary>
  public partial class InterfasePosiblesErroresDeRuteoEnVías : InterfaseBase
  {
    #region Campos
    private BuscadorDePosiblesErroresDeRuteo miBuscadorDePosiblesErroresDeRuteo;
    private readonly InterfaseListaDeElementos miLista;
    private readonly MenuEditorDeVías miMenú;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public override ManejadorDeMapa ManejadorDeMapa
    {
      set
      {
        // Deja de manejar los eventos.
        if (miBuscadorDePosiblesErroresDeRuteo != null)
        {
          miBuscadorDePosiblesErroresDeRuteo.Invalidado -= EnInvalidado;
          miBuscadorDePosiblesErroresDeRuteo.Procesó -= EnSeBuscaronPosiblesErroresDeRuteo;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;
        miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDePosiblesErroresDeRuteo = value.ManejadorDeVías.BuscadorDePosiblesErroresDeRuteo;

          if (miBuscadorDePosiblesErroresDeRuteo != null)
          {
            miBuscadorDePosiblesErroresDeRuteo.Invalidado += EnInvalidado;
            miBuscadorDePosiblesErroresDeRuteo.Procesó += EnSeBuscaronPosiblesErroresDeRuteo;
          }
        }
      }
    }


    /// <summary>
    /// Obtiene o pone el escuchador de estatus.
    /// </summary>
    public override IEscuchadorDeEstatus EscuchadorDeEstatus
    {
      set
      {
        base.EscuchadorDeEstatus = value;
        miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Constructor.
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfasePosiblesErroresDeRuteoEnVías()
    {
      InitializeComponent();

      // Inicializa campos.
      miLista = miInterfaseListaConMapaDeVías.InterfaseListaDeVías;
      miMenú = miInterfaseListaConMapaDeVías.MenuEditorDeVías;

      // Pone el llenador de items.
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.PoneLlenadorDeItems(LlenaItems);

      // Escucha el evento de edición de Vías.
      miMenú.Editó += delegate {
        // Borra las polilíneas adicionales que pudieran estar dibujadas en el mapa.
        miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.PolilíneasAdicionales.Clear();

        // Busca errores otra vez.
        miBuscadorDePosiblesErroresDeRuteo.Procesa();
      };

      // Añade menú "Ignorar Cambios Bruscos de Clase de Ruta". 
      miMenú.Items.Add(new ToolStripSeparator());
      ToolStripMenuItem menú = new ToolStripMenuItem("Ignorar Cambios Bruscos de Clase de Ruta");
      menú.Click += EnMenúIgnorarCambiosBruscosDeClaseDeRuta;
      miMenú.Items.Add(menú);
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();

      // Borra las polilíneas adicionales que pudieran estar dibujadas en el mapa.
      miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.PolilíneasAdicionales.Clear();
    }


    private void EnSeBuscaronPosiblesErroresDeRuteo(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IDictionary<Vía, IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo>> erroresDeRuteo = miBuscadorDePosiblesErroresDeRuteo.PosiblesErrorDeRuteo;
      foreach (KeyValuePair<Vía, IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo>> ítem in erroresDeRuteo)
      {
        // Crea el grupo.
        Vía vía = ítem.Key;
        ListViewGroup grupo = new ListViewGroup(vía.Nombre);
        laLista.Groups.Add(grupo);

        // Añade los elementos de las incongruencia a la lista.
        IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo> elementosDePosibleErrorDeRuteo = ítem.Value;
        foreach (BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo elementoDePosibleErrorDeRuteo in elementosDePosibleErrorDeRuteo)
        {
          ElementoConEtiqueta elemento = new ElementoConEtiqueta(elementoDePosibleErrorDeRuteo.Vía);
          // Si el elemento es un posible error entonces le ponemos un fondo amarillo.
          if (elementoDePosibleErrorDeRuteo.EsPosibleError)
          {
            laLista.AñadeItem(elemento, Color.PaleGoldenrod, grupo, elementoDePosibleErrorDeRuteo.Detalle);
          }
          else
          {
            laLista.AñadeItem(elemento, grupo, elementoDePosibleErrorDeRuteo.Detalle);
          }
        }
      }
    }


    private void EnMenúIgnorarCambiosBruscosDeClaseDeRuta(object elEnviador, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeVíasSeleccionadas = miLista.SelectedIndices.Count;
      if (númeroDeVíasSeleccionadas == 0)
      {
        return;
      }

      if (númeroDeVíasSeleccionadas > 1)
      {
        // Pregunta si se quiere ignorar los cambios bruscos de clase de ruta.
        DialogResult respuesta = MessageBox.Show(
          string.Format("Está seguro que quiere ignorar los cambios bruscos de clase de ruta en las {0} Vías seleccionadas?",
                        númeroDeVíasSeleccionadas),
          "Ignorar Cambios Bruscos de Clase de Ruta",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Warning);

        if (respuesta != DialogResult.Yes)
        {
          return;
        }
      }

      // Añade el attributo a las vías.
      ManejadorDeMapa.SuspendeEventos();
      IList<Vía> vías = miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneElementosSeleccionados<Vía>();
      foreach (Vía vía in vías)
      {
        vía.AñadeAtributo(BuscadorDePosiblesErroresDeRuteo.AtributoIgnorarCambioBruscoDeClaseDeRuta);
      }
      ManejadorDeMapa.RestableceEventos();

      // Notifica la edición.
      miMenú.EnvíaEventoEditó();
    }
    #endregion
  }
}
