﻿#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Interfase de Errores de PDIs.
  /// </summary>
  public partial class InterfaseDePDIsConErroress : InterfaseBase
  {
    #region Campos
    private List<ListViewItem> misItems = new List<ListViewItem>();
    private ManejadorDePDIs miManejadorDePDIs;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando cambian los PDIs con errores.
    /// </summary>
    public event EventHandler<NúmeroDeElementosEventArgs> CambiaronErrores;
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
        if (miManejadorDePDIs != null)
        {
          miManejadorDePDIs.CambiaronErrores -= EnCambiaronErrores;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miManejadorDePDIs = value.ManejadorDePDIs;

          if (miManejadorDePDIs != null)
          {
            miManejadorDePDIs.CambiaronErrores += EnCambiaronErrores;
          }

          // Pone el manejador de mapa en la interfase de mapa.
          miMapa.ManejadorDeMapa = value;

          // Pone el manejador de PDIs en la interfase de edición de PDIs.
          miMenúEditorDePDI.ManejadorDePDIs = value.ManejadorDePDIs;
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
        miMapa.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Métodos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDePDIsConErroress()
    {
      InitializeComponent();

      // Pone el método llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Escucha el evento de edición de PDIs.
      miMenúEditorDePDI.EditóPDIs += delegate(object elObjecto, EventArgs losArgumentos)
      {
        // Borra los puntos adicionales que estén en el mapa.
        miMapa.PuntosAddicionales.Clear();

        // Busca errores otra vez.
        miManejadorDePDIs.BuscaErrores();
      };
    }

    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnCambiaronErrores(elEnviador, losArgumentos);
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No es necesario hacer nada aqui.
    }


    private void EnCambiaronErrores(object elEnviador, EventArgs losArgumentos)
    {
      miLista.RegeneraLista();

      // Genera el evento.
      if (CambiaronErrores != null)
      {
        CambiaronErrores(this, new NúmeroDeElementosEventArgs(miLista.NúmeroDeElementos));
      }
    }

    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los PDIs.
      IDictionary<PDI, string> errores = ManejadorDeMapa.ManejadorDePDIs.Errores;
      foreach (KeyValuePair<PDI, string> error in errores)
      {
        PDI pdi = error.Key;
        string razón = error.Value;
        laLista.AñadeItem(pdi, razón);
      }

      // Activa el menú de Edición si hay elementos en la lista.
      if (errores.Count > 0)
      {
        miMenúEditorDePDI.Enabled = true;
      }
      else
      {
        miMenúEditorDePDI.Enabled = false;
      }
    }
    #endregion
  }
}
