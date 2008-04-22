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
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Pdis;

namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  /// <summary>
  /// Interfase para el manejo de PDIs.
  /// </summary>
  public partial class InterfaseManejadorDePdis : InterfaseBase
  {
    #region Campos
    private readonly InterfaseBase[] misInterfases;
    private readonly Dictionary<TabPage, int> misIndicesDePestañas = new Dictionary<TabPage, int>();
    private readonly string miTextoPestañaErrores = "Errores";
    private readonly string miTextoPestañaDuplicados = "Posibles Duplicados";
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando cambió el estado máximo de las Pestañas.
    /// </summary>
    public event EventHandler<ControladorDePestañas.CambióEstadoMáximoDePestañasEventArgs> CambióEstadoMáximoDePestañas;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public override ManejadorDeMapa ManejadorDeMapa
    {
      set
      {
        base.ManejadorDeMapa = value;

        foreach (InterfaseBase interfaseBase in misInterfases)
        {
          interfaseBase.ManejadorDeMapa = value;
        }

        // Maneja Eventos
        if (value != null)
        {
          ManejadorDePdis manejadorDePdis = value.ManejadorDePdis;

          // Buscador de Errores.
          BuscadorDeErrores buscadorDeErrores = manejadorDePdis.BuscadorDeErrores;
          buscadorDeErrores.Invalidado += EnInvalidadoPdisConErrores;
          buscadorDeErrores.Procesó += EnSeBuscaronPdisConErrores;

          // Buscador de Duplicados.
          BuscadorDeDuplicados buscadorDeDuplicados = manejadorDePdis.BuscadorDeDuplicados;
          buscadorDeDuplicados.Invalidado += EnInvalidadoPdisDuplicados;
          buscadorDeDuplicados.Procesó += EnSeBuscaronPdisDuplicados;
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

        foreach (InterfaseBase interfaseBase in misInterfases)
        {
          interfaseBase.EscuchadorDeEstatus = value;
        }
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseManejadorDePdis()
    {
      InitializeComponent();

      // Crea el vector de interfases.
      misInterfases = new InterfaseBase[] {
        miInterfaseDeMapa,
        miInterfasePdisDuplicados,
        miInterfasePdisEliminados,
        miInterfasePdisErrores,
        miInterfasePdisModificados};

      // Escucha los eventos para actualizar las pestañas.
      miInterfasePdisEliminados.PdisEliminados += EnPdisEliminados;
      miInterfasePdisModificados.PdisModificados += EnPdisModificados;

      // Pone el método llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Crea el diccionario de índices de pestañas.
      TabControl.TabPageCollection pestañas = miControladorDePestañas.TabPages;
      for (int i = 0; i < pestañas.Count; ++i)
      {
        misIndicesDePestañas[pestañas[i]] = i;
      }

      // Maneja evento de cambio de Estado Máximo de Pestañas.
      miControladorDePestañas.CambióEstadoMáximoDePestañas +=
        delegate(object elEnviador, ControladorDePestañas.CambióEstadoMáximoDePestañasEventArgs losArgumentos)
        {
          if (CambióEstadoMáximoDePestañas != null)
          {
            CambióEstadoMáximoDePestañas(this, losArgumentos);
          }
        };
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnElementosModificados(elEnviador, losArgumentos);
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      miLista.RegeneraLista();

      // Actualiza la Pestaña.
      miPáginaDeTodos.Text = "Todos (" + miLista.NúmeroDeElementos + ")";
    }


    private void EnPdisModificados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      // Cambia el texto de la pestaña.
      int númeroDePdisModificados = losArgumentos.NúmeroDeItems;
      miPáginaModificados.Text = "Modificados (" + númeroDePdisModificados + ")";
    }


    private void EnPdisEliminados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      // Cambia el texto de la pestaña.
      int númeroDePdisEliminados = losArgumentos.NúmeroDeItems;
      miPáginaEliminados.Text = "Eliminados (" + númeroDePdisEliminados + ")";
    }


    private void EnInvalidadoPdisDuplicados(object elEnviador, EventArgs losArgumentos)
    {
      // Pone las pestañas en estado de "No Sé" para indicar que
      // no se sabe si hay duplicados.
      miControladorDePestañas.PoneEstadoDePestaña(
        misIndicesDePestañas[miPáginaPosiblesDuplicados],
        ControladorDePestañas.EstadoDePestaña.NoSé);

      // Cambia el texto de la pestaña.
      miPáginaPosiblesDuplicados.Text = miTextoPestañaDuplicados;
    }


    private void EnSeBuscaronPdisDuplicados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      int númeroDePdisDuplicados = losArgumentos.NúmeroDeItems;
      miPáginaPosiblesDuplicados.Text = miTextoPestañaDuplicados + " (" + númeroDePdisDuplicados + ")";

      // Si hay posibles PDIs duplicados no se sabe si son errores o está bien,
      // así que ponemos el estado de la pestaña en Alerta.
      // Si no, entonces cambia el estado de la pestaña a Bíen.
      if (númeroDePdisDuplicados > 0)
      {
        miControladorDePestañas.PoneEstadoDePestaña(
          misIndicesDePestañas[miPáginaPosiblesDuplicados],
          ControladorDePestañas.EstadoDePestaña.Alerta);
      }
      else
      {
        miControladorDePestañas.PoneEstadoDePestaña(
          misIndicesDePestañas[miPáginaPosiblesDuplicados],
          ControladorDePestañas.EstadoDePestaña.Bién);
      }
    }


    private void EnInvalidadoPdisConErrores(object elEnviador, EventArgs losArgumentos)
    {
      // Pone las pestañas en estado de "No Sé" para indicar que
      // no se sabe si hay errores.
      miControladorDePestañas.PoneEstadoDePestaña(
        misIndicesDePestañas[miPáginaErrores],
        ControladorDePestañas.EstadoDePestaña.NoSé);

      // Cambia el texto de la pestaña.
      miPáginaErrores.Text = miTextoPestañaErrores;
    }


    private void EnSeBuscaronPdisConErrores(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      int númeroDePdisConErrores = losArgumentos.NúmeroDeItems;

      // Cambia el texto de la pestaña.
      miPáginaErrores.Text = miTextoPestañaErrores + " (" + númeroDePdisConErrores + ")";

      // Si hay PDIs con errores entonces cambia el estado de la pestaña a Error.
      // Si no, entonces cambia el estado de la pestaña a Bíen.
      if (númeroDePdisConErrores > 0)
      {
        miControladorDePestañas.PoneEstadoDePestaña(
          misIndicesDePestañas[miPáginaErrores],
          ControladorDePestañas.EstadoDePestaña.Error);
      }
      else
      {
        miControladorDePestañas.PoneEstadoDePestaña(
          misIndicesDePestañas[miPáginaErrores],
          ControladorDePestañas.EstadoDePestaña.Bién);
      }
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<Pdi> pdis = ManejadorDeMapa.ManejadorDePdis.Elementos;
      foreach (Pdi pdi in pdis)
      {
        laLista.AñadeItem(new ElementoConEtiqueta(pdi));
      }
    }
    #endregion
  }
}
