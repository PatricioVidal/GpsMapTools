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
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Pdis;
using System.IO;

namespace GpsYv.ManejadorDeMapa.Interfase.Pdis
{
  /// <summary>
  /// Menú para editar PDIs.
  /// </summary>
  public partial class MenuEditorDePdis : MenúEditorDeElementos
  {
    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de PDIs.
    /// </summary>
    [Browsable (true)]
    public ManejadorDePdis ManejadorDePdis
    {
      get;
      set;
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public MenuEditorDePdis()
    {
      InitializeComponent();

      // Añade los menús.
      AñadeMenúGuardarArchivoPdis();
      AñadeMenuParaCambiarTipo();
    }
    #endregion

    #region Métodos Privados
    private void AñadeMenúGuardarArchivoPdis()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem
      {
        Text = "Guardar archivo para localización de PDI(s)",
        AutoSize = true
      };
      Items.Add(menú);

      menú.Click += EnMenúGuardarArchivoPdis;
    }


    private void AñadeMenuParaCambiarTipo()
    {
      ToolStripMenuItem menuCambiarTipo = new ToolStripMenuItem {Text = "Cambiar Tipo"};
      Items.Add(menuCambiarTipo);

      menuCambiarTipo.Click += EnMenúCambiarTipo;
    }


    private void EnMenúGuardarArchivoPdis(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay PDIs seleccionadas.
      if (Lista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Crea el nombre del archivo de salida.
      string archivo = Path.GetFullPath(this.ManejadorDePdis.ManejadorDeMapa.Archivo);
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileName(archivo);
      string nombreDeSalida = Path.ChangeExtension(nombre, ".PDIs.mp");

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog
      {
        Title = "Guarda archivo de localización de PDI(s)",
        AutoUpgradeEnabled = true,
        AddExtension = true,
        CheckPathExists = true,
        Filter = ManejadorDeMapa.FiltrosDeExtensiones,
        InitialDirectory = directorio,
        FileName = nombreDeSalida,
        OverwritePrompt = true,
        ValidateNames = true
      };
      DialogResult respuesta = ventanaDeGuardar.ShowDialog();
      if (respuesta == DialogResult.OK)
      {
        // El primer elemento de cada lista tiene que ser el encabezado.
        List<ElementoDelMapa> elementos = new List<ElementoDelMapa> { this.ManejadorDePdis.ManejadorDeMapa.Encabezado };

        // Genera la lista de PDIs.
        IList<Pdi> pdis = ObtieneElementosSeleccionados<Pdi>();
        foreach (Pdi pdi in pdis)
        {
          // Crea los campos para el PDI.
          List<Campo> campos = new List<Campo> {
            new CampoNombre("PDI #" + pdi.Número),
            new CampoCoordenadas(
              "Data0",
              0,
              pdi.Coordenadas),
            new CampoTipo("0x1604"),
            new CampoGenérico("EndLevel", "3")
          };

          // Crea el PDI y añadelo a la lista.
          Pdi pdiNuevo = new Pdi(
            this.ManejadorDePdis.ManejadorDeMapa,
            0,
            "POI",
            campos);
          elementos.Add(pdiNuevo);
        }

        // Guarda el archivo.
        new EscritorDeFormatoPolish(
          ventanaDeGuardar.FileName,
          elementos,
          this.ManejadorDePdis.EscuchadorDeEstatus);
      }
    }


    private void EnMenúCambiarTipo(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay PDIs.
      if (Lista.SelectedIndices.Count == 0)
      {
        return;
      }

      List<Pdi> pdis = new List<Pdi>();
      foreach (int indice in Lista.SelectedIndices)
      {
        ListViewItem item = Lista.Items[indice];

        // El Tag del item de la lista tiene que ser un ElementoConEtiqueta con un PDI.
        ElementoConEtiqueta elemento = item.Tag as ElementoConEtiqueta;
        if (elemento == null)
        {
          throw new InvalidOperationException(string.Format("El Tag del item de la lista tiene que ser un ElementoConEtiqueta, pero es: {0}", item.Tag.GetType()));
        }
        Pdi pdi = elemento.ElementoDelMapa as Pdi;
        if (pdi == null)
        {
          throw new InvalidOperationException(string.Format("El elemento del item de la lista tiene que ser un Pdi, pero es: {0}", elemento.ElementoDelMapa.GetType()));
        }

        // Añade el PDI a la lista.
        pdis.Add(pdi);
      }

      // Muestra la ventana para cambiar el Tipo.
      VentanaCambiarTipoDePdi ventanaCambiarTipo = new VentanaCambiarTipoDePdi {
        Pdis = pdis };
      DialogResult resultado = ventanaCambiarTipo.ShowDialog();
      if (resultado == DialogResult.OK)
      {
        // Cambia los tipos evitando que se generen eventos con
        // cada cambio.
        ManejadorDePdis.SuspendeEventos();
        foreach (Pdi pdi in pdis)
        {
          pdi.ActualizaTipo(ventanaCambiarTipo.TipoNuevo, "Cambio Manual");
        }

        // Restablece los eventos en el manejador de mapa.
        ManejadorDePdis.RestableceEventos();

        // Envía el evento indicando que se editaron PDIs.
        EnvíaEventoEditó();
      }
    }
    #endregion
  }
}
