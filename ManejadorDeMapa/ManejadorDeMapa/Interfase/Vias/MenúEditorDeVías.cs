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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;
using System.IO;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Menú para editar Vías.
  /// </summary>
  public partial class MenuEditorDeVías : ContextMenuStrip
  {
    #region Campos
    private ListView miLista = null;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando se editan Vías.
    /// </summary>
    public event EventHandler EditóVías;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la lista con los elementos del mapa.
    /// </summary>
    /// <remarks>
    /// Cada Tag de los items de la lista tiene que ser un PDI.
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

          // Esta clase es muy lenta con listas que no están en modo virtual. 
          // Entonces solo permitimos listas virtuales.
          miLista = value;
          if (!miLista.VirtualMode)
          {
            throw new ArgumentException("La InterfaseMapaDeVíasSeleccionadas solo se puede conectar con listas virtuales.");
          }

          // Conecta el menú a la lista.
          miLista.ContextMenuStrip = this;
        }
      }
    }


    /// <summary>
    /// Obtiene o pone el manejador de Vías.
    /// </summary>
    [Browsable(true)]
    public ManejadorDeVías ManejadorDeVías
    {
      get;
      set;
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public MenuEditorDeVías()
    {
      InitializeComponent();

      // Inicialización.
      Name = "miMenuDeContexto";
      Size = new System.Drawing.Size(153, 48);
      AutoSize = true;

      // Añade los menús.
      AñadeMenúGuardarArchivoPDIs();
      AñadeMenúCambiarCoordenadasANivel0();
    }
    #endregion

    #region Métodos Privados
    private void AñadeMenúCambiarCoordenadasANivel0()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem();
      menú.Text = "Cambiar coordenadas a Nivel 0";
      menú.AutoSize = true;
      Items.Add(menú);

      menú.Click += EnMenúCambiarCoordenadasANivel0;
    }


    private void AñadeMenúGuardarArchivoPDIs()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem();
      menú.Text = "Guarda archivo de PDIs para localización de Vía(s)";
      menú.AutoSize = true;
      Items.Add(menú);

      menú.Click += EnMenúGuardarArchivoPDIs;
    }


    private void EnMenúCambiarCoordenadasANivel0(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      if (miLista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Muestra la ventana de confirmación.
      DialogResult resultado = MessageBox.Show(
        "Esta seguro de que quiere cambiar las coordenadas a nivel 0?",
        "Cambiar Vía(s)",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);
      if (resultado == DialogResult.Yes)
      {
        // Cambia las coordenadas evitando que se generen eventos con
        // cada cambio.
        ManejadorDeVías.SuspendeEventos();
        IList<Vía> vías = ObtieneVíasSeleccionadas();
        foreach (Vía vía in vías)
        {
          int númeroDeCampos = vía.Campos.Count;
          for (int i = 0; i < númeroDeCampos; ++i)
          {
            Campo campo = vía.Campos[i];
            if (campo is CampoCoordenadas)
            {
              CampoCoordenadas campoACambiar = (CampoCoordenadas)campo;

              // Cambia el campo si no está a nivel cero.
              int nivel = 0;
              if (campoACambiar.Nivel != nivel)
              {
                // Genera el nuevo campo.
                CampoCoordenadas campoNuevo = new CampoCoordenadas(
                  campoACambiar.Identificador,
                  nivel,
                  campoACambiar.Coordenadas);

                // Cambia el campo.
                vía.CambiaCampo(campoNuevo, campoACambiar, "Cambio a nivel 0");
              }
            }
          }
        }

        // Envía el evento indicando que se editaron PDIs.
        if (EditóVías != null)
        {
          EditóVías(this, new EventArgs());
        }

        // Restablece los eventos.
        ManejadorDeVías.RestableceEventos();
      }
    }


    private IList<Vía> ObtieneVíasSeleccionadas()
    {
      List<Vía> vías = new List<Vía>();
      foreach (int indice in miLista.SelectedIndices)
      {
        ListViewItem item = miLista.Items[indice];

        // El Tag del item de la lista tiene que ser una vía.
        Vía vía = item.Tag as Vía;
        if (vía == null)
        {
          throw new InvalidOperationException("El Tag del item de la lista tiene que ser una Vía, pero es nulo");
        }

        // Añade la vía a la lista.
        vías.Add(vía);
      }
      return vías;
    }


    private void EnMenúGuardarArchivoPDIs(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      if (miLista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Crea el nombre del archivo de salida.
      string archivo = Path.GetFullPath(ManejadorDeVías.ManejadorDeMapa.Archivo);
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileName(archivo);
      string nombreDeSalida = Path.ChangeExtension(nombre, ".PDIsDeVías.mp");

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog();
      ventanaDeGuardar.Title = "Guarda archivo de PDIs para localización de Vía(s)";
      ventanaDeGuardar.AutoUpgradeEnabled = true;
      ventanaDeGuardar.AddExtension = true;
      ventanaDeGuardar.CheckPathExists = true;
      ventanaDeGuardar.Filter = ManejadorDeMapa.FiltrosDeExtensiones;
      ventanaDeGuardar.InitialDirectory = directorio;
      ventanaDeGuardar.FileName = nombreDeSalida;
      ventanaDeGuardar.OverwritePrompt = true;
      ventanaDeGuardar.ValidateNames = true;
      DialogResult respuesta = ventanaDeGuardar.ShowDialog();
      if (respuesta == DialogResult.OK)
      {
        // El primer elemento de cada lista tiene que ser el encabezado.
        List<ElementoDelMapa> elementos = new List<ElementoDelMapa> { ManejadorDeVías.ManejadorDeMapa.Encabezado };

        // Genera la lista de PDIs.
        IList<Vía> vías = ObtieneVíasSeleccionadas();
        foreach (Vía vía in vías)
        {
          // Crea los campos para el PDI.
          List<Campo> campos = new List<Campo> {
            new CampoNombre("Vía #" + vía.Número),
            new CampoCoordenadas(
              "Data0",
              0,
              vía.Coordenadas[0]),
            new CampoTipo("0x1604"),
            new CampoGenérico("EndLevel", "3")
          };

          // Crea el PDI y añadelo a la lista.
          PDI pdi = new PDI(
            ManejadorDeVías.ManejadorDeMapa,
            0,
            "POI",
            campos);
          elementos.Add(pdi);  
        }

        // Guarda el archivo.
        new EscritorDeFormatoPolish(
          ventanaDeGuardar.FileName,
          elementos,
          ManejadorDeVías.EscuchadorDeEstatus);
      }


    }
    #endregion
  }
}
