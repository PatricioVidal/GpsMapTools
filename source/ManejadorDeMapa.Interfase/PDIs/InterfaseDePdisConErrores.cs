﻿#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
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
using GpsYv.ManejadorDeMapa.Pdis;

namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  /// <summary>
  /// Interfase de Errores de PDIs.
  /// </summary>
  public partial class InterfaseDePdisConErrores : InterfaseBase
  {
    #region Campos
    private BuscadorDeErrores miBuscadorDeErrores;
    private readonly InterfaseListaDePdis miLista;
    private readonly InterfaseMapaDeElementosSeleccionados miMapa;
    private readonly MenuEditorDePdis miMenú;
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
        if (miBuscadorDeErrores != null)
        {
          miBuscadorDeErrores.Invalidado -= EnInvalidado;
          miBuscadorDeErrores.Procesó -= EnSeBuscaronErrores;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;
        miInterfaseListaConMapaDePdis.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDeErrores = value.ManejadorDePdis.BuscadorDeErrores;

          if (miBuscadorDeErrores != null)
          {
            miBuscadorDeErrores.Invalidado += EnInvalidado;
            miBuscadorDeErrores.Procesó += EnSeBuscaronErrores;
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
        miInterfaseListaConMapaDePdis.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDePdisConErrores()
    {
      InitializeComponent();

      // Asigna los campos.
      miLista = miInterfaseListaConMapaDePdis.InterfaseListaDePdis;
      miMapa = miInterfaseListaConMapaDePdis.InterfaseMapaDePdisSeleccionados;
      miMenú = miInterfaseListaConMapaDePdis.MenuEditorDePdis;

      // Pone el método llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Escucha el evento de edición de PDIs.
      miMenú.Editó += delegate
      {
        // Borra los puntos adicionales que estén en el mapa.
        miMapa.PuntosAddicionales.Clear();

        // Busca errores otra vez.
        miBuscadorDeErrores.Procesa();
      };

      // Añade el menú para ignorar que el PDI no tenga coordenadas a nivel zero.
      miInterfaseListaConMapaDePdis.MenuEditorDePdis.Items.Add(new ToolStripSeparator());
      var menú1 = new ToolStripMenuItem(Properties.Recursos.InterfaseDePdisConErroresMenuIgnorarPdiNoCoordenadasANivel0);
      menú1.Click += ((s, e) => AñadeAttributo(
       menú1.Text,
       Properties.Recursos.InterfaseDePdisConErroresPreguntaIgnorarPdiNoCoordenadasANivel0,
       BuscadorDeErrores.AtributoIgnorarNoCoordenadasNivel0));
      miInterfaseListaConMapaDePdis.MenuEditorDePdis.Items.Add(menú1);

      // Añade el menú para ignorar que el PDI de Ciudad no tenga campos City=Y o CityIdx.
      var menú2 = new ToolStripMenuItem(Properties.Recursos.InterfaseDePdisConErroresMenuIgnorarPdiCiudadNoCamposCityOCityIdx);
      menú2.Click += ((s, e) => AñadeAttributo(
       menú2.Text,
       Properties.Recursos.InterfaseDePdisConErroresPreguntaIgnorarPdiCiudadNoCamposCityOCityIdx,
       BuscadorDeErrores.AtributoIgnorarCamposCityYCityIdx));
      miInterfaseListaConMapaDePdis.MenuEditorDePdis.Items.Add(menú2);
    }


    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      miLista.RegeneraLista();

      // Borra los puntos que pudieran estar dibujadas en el mapa.
      miMapa.PuntosAddicionales.Clear();
    }


    private void EnSeBuscaronErrores(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miLista.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los PDIs.
      IDictionary<Pdi, string> errores = miBuscadorDeErrores.Errores;
      foreach (KeyValuePair<Pdi, string> error in errores)
      {
        Pdi pdi = error.Key;
        string razón = error.Value;
        laLista.AñadeItem(new ElementoConEtiqueta(pdi), razón);
      }

      // Activa el menú de Edición si hay elementos en la lista.
      if (errores.Count > 0)
      {
        miMenú.Enabled = true;
      }
      else
      {
        miMenú.Enabled = false;
      }
    }


    private void AñadeAttributo(string elTítulo, string laPregunta, string elAtributo)
    {
      ListView lista = miInterfaseListaConMapaDePdis.InterfaseListaDePdis;

      // Retornamos si no hay PDIs seleccionados.
      int númeroDePdisSeleccionados = lista.SelectedIndices.Count;
      if (númeroDePdisSeleccionados == 0)
      {
        return;
      }

      // Pregunta si se quiere Ignorarque el PDI no tenga coordenadas a Nivel 0.
      DialogResult respuesta = MessageBox.Show(
        string.Format(
          laPregunta,
          númeroDePdisSeleccionados),
          elTítulo,
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      #region Estandarizar el Límite de Velocidad si el usuario dice que si.
      if (respuesta != DialogResult.Yes)
      {
        return;
      }

      // Añade los attributos.
      ManejadorDeMapa.SuspendeEventos();
      IList<Pdi> pdis = miInterfaseListaConMapaDePdis.MenuEditorDePdis.ObtieneElementosSeleccionados<Pdi>();
      foreach (Pdi pdi in pdis)
      {
        pdi.AñadeAtributo(elAtributo);
      }
      ManejadorDeMapa.RestableceEventos();

      // Busca errores otra vez.
      miBuscadorDeErrores.Procesa();
      #endregion
    }
    #endregion
  }
}
