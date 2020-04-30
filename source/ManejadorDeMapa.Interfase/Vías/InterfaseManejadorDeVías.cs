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
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Interfase manejador de Vías.
  /// </summary>
  public partial class InterfaseManejadorDeVías : InterfaseBase
  {
    #region Campos
    private readonly InterfaseBase[] misInterfases;
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
          ManejadorDeVías manejadorDeVías = value.ManejadorDeVías;

          // Buscador de Errores.
          BuscadorDeErrores buscadorDeErrores = manejadorDeVías.BuscadorDeErrores;
          buscadorDeErrores.Invalidado += EnInvalidadoVíasConErrores;
          buscadorDeErrores.Procesó += EnSeBuscaronVíasConErrores;

          // Buscador de Posible Errores de Ruteo.
          BuscadorDePosiblesErroresDeRuteo buscadorDePosiblesErroresDeRuteo = manejadorDeVías.BuscadorDePosiblesErroresDeRuteo;
          buscadorDePosiblesErroresDeRuteo.Invalidado += EnInvalidadoPosiblesErroresDeRuteo;
          buscadorDePosiblesErroresDeRuteo.Procesó += EnSeBuscaronPosiblesErroresDeRuteo;

          // Buscador de Posibles Bodos Desconectados.
          BuscadorDePosiblesNodosDesconectados buscadorDePosiblesNodosDesconectados = manejadorDeVías.BuscadorDePosiblesNodosDesconectados;
          buscadorDePosiblesNodosDesconectados.Invalidado += EnInvalidadoPosiblesNodosDesconectados;
          buscadorDePosiblesNodosDesconectados.Procesó += EnSeBuscaronPosiblesNodosDesconectados;

          // Buscador de Incongruencias.
          BuscadorDeAlertas buscadorDeIncongruencias = manejadorDeVías.BuscadorDeAlertas;
          buscadorDeIncongruencias.Invalidado += EnInvalidadoVíasConAlertas;
          buscadorDeIncongruencias.Procesó += EnSeBuscaronVíasConAlertas;
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
    public InterfaseManejadorDeVías()
    {
      InitializeComponent();

      // Crea la lista de interfases.
      misInterfases = new InterfaseBase[] {
        miInterfaseDeMapa,
        miMapaDeVíaSeleccionada,
        miInterfaseDeVíasModificadas,
        miInterfaseDeVíasConIncongruencias,
        miInterfaseDeErroresEnVías,
        miInterfaseDeVíasEliminadas,
        miInterfasePosiblesErroresDeRuteoEnVías,
        miInterfasePosiblesNodosDesconectados
      };

      // Pone el método llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Escucha los eventos para actualizar las pestañas.
      miInterfaseDeVíasModificadas.VíasModificadas += EnVíasModificadas;
      miInterfaseDeVíasEliminadas.VíasEliminadas += EnVíasEliminadas;
      
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
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<Vía> vías = ManejadorDeMapa.ManejadorDeVías.Elementos;
      foreach (Vía vía in vías)
      {
        laLista.AñadeItem(new ElementoConEtiqueta(vía));
      }
    }


    private void EnVíasModificadas(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaModificadas,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Nada);
    }


    private void EnVíasEliminadas(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaEliminadas,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Nada);
    }


    private void EnInvalidadoVíasConErrores(object elEnviador, EventArgs losArgumentos)
    {
      miControladorDePestañas.InvalidaPestaña(miPáginaErrores);
    }


    private void EnSeBuscaronVíasConErrores(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaErrores,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Error);
    }


    private void EnInvalidadoPosiblesErroresDeRuteo(object elEnviador, EventArgs losArgumentos)
    {
      miControladorDePestañas.InvalidaPestaña(miPáginaPosibleErroresDeRuteo);
    }


    private void EnSeBuscaronPosiblesErroresDeRuteo(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaPosibleErroresDeRuteo,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Alerta);
    }


    private void EnInvalidadoPosiblesNodosDesconectados(object elEnviador, EventArgs losArgumentos)
    {
      miControladorDePestañas.InvalidaPestaña(
        miPáginaPosiblesNodosDesconectados);
    }


    private void EnSeBuscaronPosiblesNodosDesconectados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaPosiblesNodosDesconectados,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Alerta);
    }


    private void EnInvalidadoVíasConAlertas(object elEnviador, EventArgs losArgumentos)
    {
      miControladorDePestañas.InvalidaPestaña(miPáginaAlertas);
    }


    private void EnSeBuscaronVíasConAlertas(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miControladorDePestañas.ActualizaPestaña(
        miPáginaAlertas,
        losArgumentos.NúmeroDeItems,
        ControladorDePestañas.EstadoDePestaña.Alerta);
    }
    #endregion
  }
}
