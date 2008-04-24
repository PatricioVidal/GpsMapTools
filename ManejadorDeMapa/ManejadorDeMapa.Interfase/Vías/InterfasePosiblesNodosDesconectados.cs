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

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Interfase de Posibles Nodos Desconectados.
  /// </summary>
  public partial class InterfasePosiblesNodosDesconectados : InterfaseBase
  {
    #region Campos
    private BuscadorDePosiblesNodosDesconectados miBuscadorDePosiblesNodosDesconectados;
    private Brush miPincelDeBordeDeNodo = Brushes.Black;
    private Brush miPincelDeNodo = Brushes.Yellow;
    private Pen miLápizDeViaConElPosibleNodoDesconectado = new Pen(Color.LightSalmon, 9);
    private Brush miPincelDePosibleNodoDesconectado = Brushes.LightSalmon;
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
        if (miBuscadorDePosiblesNodosDesconectados != null)
        {
          miBuscadorDePosiblesNodosDesconectados.Invalidado -= EnInvalidado;
          miBuscadorDePosiblesNodosDesconectados.Procesó -= EnSeBuscaronPosiblesNodosDesconectados;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDePosiblesNodosDesconectados = value.ManejadorDeVías.BuscadorDePosiblesNodosDesconectados;
          miBuscadorDePosiblesNodosDesconectados.Invalidado += EnInvalidado;
          miBuscadorDePosiblesNodosDesconectados.Procesó += EnSeBuscaronPosiblesNodosDesconectados;
          InicializaDistanciaMáxima();
        }

        // Pone el manejador de mapa en los componentes.
        miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;
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
    public InterfasePosiblesNodosDesconectados()
    {
      InitializeComponent();

      // Pone el llenador de items.
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.PoneLlenadorDeItems(LlenaItems);

      miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.DibujóElementos += EnDibujóElementos;

      // Escucha el evento de edición de Vías.
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Editó += delegate(object elObjecto, EventArgs losArgumentos)
      {
        // Borra las polilíneas adicionales que pudieran estar dibujadas en el mapa.
        miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.PolilíneasAdicionales.Clear();

        // Busca posibles nodos desconectados otra vez.
        miBuscadorDePosiblesNodosDesconectados.Procesa();
      };
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();

      // Borra las polilíneas adicionales que pudieran estar dibujadas en el mapa.
      miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.PolilíneasAdicionales.Clear();
    }


    private void EnSeBuscaronPosiblesNodosDesconectados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<PosibleNodoDesconectado> posibleNodosDesconectados = miBuscadorDePosiblesNodosDesconectados.PosibleNodosDesconectados;
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodosDesconectados)
      {
        ElementoConEtiqueta elemento = new ElementoConEtiqueta(posibleNodoDesconectado.Vía, posibleNodoDesconectado);
        laLista.AñadeItem(
          elemento,
          posibleNodoDesconectado.Nodo.ToString(),
          posibleNodoDesconectado.Distancia.ToString("0.0"),
          posibleNodoDesconectado.Detalle);
      }
    }
    

    private void EnDibujóElementos(object elEnviador, EventArgs losArgumentos)
    {
      InterfaseMapa mapa = miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas;
      mapa.PuntosAddicionales.Clear();
      RectangleF rectánguloQueEncierra = new RectangleF(
        float.PositiveInfinity,
        float.PositiveInfinity,
        0,
        0);


      // Dibuja los nodos como puntos addicionales para resaltarlos.
      foreach(int i in miInterfaseListaConMapaDeVías.InterfaseListaDeVías.SelectedIndices)
      {
        ElementoConEtiqueta elemento = (ElementoConEtiqueta)miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Items[i].Tag;
        PosibleNodoDesconectado posibleNodoDesconectado = (PosibleNodoDesconectado)elemento.Etiqueta;

        // Dibuja la vía del posible nodo desconectado.
        mapa.PolilíneasAdicionales.Add(new InterfaseMapa.PolilíneaAdicional(
          posibleNodoDesconectado.VíaConElPosibleNodoDesconectado.Coordenadas,
          miLápizDeViaConElPosibleNodoDesconectado));

        // Dibuja el posible nodo desconectado.
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.PosiblesNodoDesconectado,
          miPincelDeBordeDeNodo,
          11));
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.PosiblesNodoDesconectado,
          miPincelDePosibleNodoDesconectado,
          7));

        // Dibuja el nodo.
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Nodo,
          miPincelDeBordeDeNodo,
          13));
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Nodo,
          miPincelDeNodo,
          9));

        InterfaseMapa.ActualizaRectánguloQueEncierra(
          posibleNodoDesconectado.Nodo,
          ref rectánguloQueEncierra);
      }

      const float margen = 0.0001f;
      RectangleF rectánguloVisible = new RectangleF(
        rectánguloQueEncierra.X - margen,
        rectánguloQueEncierra.Y - margen,
        rectánguloQueEncierra.Width + (2 * margen),
        rectánguloQueEncierra.Height + (2 * margen));
      mapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
    }

    
    private void EnCambióBarraDeDistancia(object sender, EventArgs e)
    {
      InicializaDistanciaMáxima();
    }


    private void InicializaDistanciaMáxima()
    {
      int distancia = miBarraDeDistancia.Value * 5;
      miTextoDistancia.Text = distancia + " m";
      miBuscadorDePosiblesNodosDesconectados.DistanciaMáxima = distancia;
    }


    private void EnBotónBuscaPosiblesNodosDesconectados(object sender, EventArgs e)
    {
      miBuscadorDePosiblesNodosDesconectados.Procesa();
    }
    #endregion
  }
}
