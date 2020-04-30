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
using System.ComponentModel;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;
using System.IO;
using GpsYv.ManejadorDeMapa.Pdis;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Menú para editar Vías.
  /// </summary>
  public partial class MenuEditorDeVías : MenúEditorDeElementos
  {
    #region Propiedades
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

      // Añade los menús.
      AñadeMenúGuardarArchivoPdis();
      AñadeMenúEstandarizarLímiteDeVelocidad();
      AñadeMenúEstandarizarClaseDeRuta();
      AñadeMenúEstandarizarLímiteDeVelocidadYClaseDeRuta();
      AñadeMenúEliminarVías();
    }
    #endregion

    #region Métodos Privados
    private void AñadeMenúGuardarArchivoPdis()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem {
        Text = Properties.Recursos.MenuEditorDeViasGuardarArchivo,
        AutoSize = true
      };
      Items.Add(menú);

      menú.Click += EnMenúGuardarArchivoPdis;
    }


    private void AñadeMenúEstandarizarLímiteDeVelocidad()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem {Text = "Estandarizar Límite de Velocidad", AutoSize = true};
      Items.Add(menú);

      menú.Click += EnMenúEstandarizarLímiteDeVelocidad;
    }


    private void AñadeMenúEstandarizarClaseDeRuta()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem {
        Text = "Estandarizar Clase de Ruta",
        AutoSize = true
      };
      Items.Add(menú);

      menú.Click += EnMenúEstandarizarClaseDeRuta;
    }


    private void AñadeMenúEstandarizarLímiteDeVelocidadYClaseDeRuta()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem {
        Text = "Estandarizar Límite de Velocidad y Clase de Ruta",
        AutoSize = true
      };
      Items.Add(menú);

      menú.Click += EnMenúEstandarizarLímiteDeVelocidadYClaseDeRuta;
    }


    private void AñadeMenúEliminarVías()
    {
      ToolStripMenuItem menú = new ToolStripMenuItem {
        Text = "Eliminar Vías",
        AutoSize = true
      };
      Items.Add(menú);

      menú.Click += EnMenúEliminarVías;
    }


    private void EnMenúGuardarArchivoPdis(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      if (Lista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Crea el nombre del archivo de salida.
      string archivo = Path.GetFullPath(ManejadorDeVías.ManejadorDeMapa.Archivo);
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileName(archivo);
      string nombreDeSalida = Path.ChangeExtension(nombre, ".PDIsDeVías.mp");

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog {
        Title = "Guarda archivo de PDIs para localización de Vía(s)",
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
        List<ElementoDelMapa> elementos = new List<ElementoDelMapa> { ManejadorDeVías.ManejadorDeMapa.Encabezado };

        // Genera la lista de PDIs.
        IList<Vía> vías = ObtieneElementosSeleccionados<Vía>();
        foreach (Vía vía in vías)
        {
          // Crea los campos para el PDI.
          int índiceDeLaCoordenadaCentral = vía.Coordenadas.Length / 2;
          List<Campo> campos = new List<Campo> {
            new CampoNombre("Vía #" + vía.Número),
            new CampoCoordenadas(
              "Data0",
              0,
              vía.Coordenadas[índiceDeLaCoordenadaCentral]),
            new CampoTipo("0x1604"),
            new CampoGenérico("EndLevel", "3")
          };

          // Crea el PDI y añadelo a la lista.
          Pdi pdi = new Pdi(
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


    private void EnMenúEstandarizarLímiteDeVelocidad(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeVíasSeleccionadas = Lista.SelectedIndices.Count;
      if (númeroDeVíasSeleccionadas == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar el Límite de Velocidad.
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere Estandarizar el Límite de Velocidad en las {0} Vías seleccionadas?", númeroDeVíasSeleccionadas), 
        "Estandarizar Límite de Velocidad", 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Warning);

      // Estandarizar el Límite de Velocidad si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        // Cambia las vías.
        ManejadorDeVías.SuspendeEventos();
        IList<Vía> vías = ObtieneElementosSeleccionados<Vía>();
        foreach (Vía vía in vías)
        {
          if (vía.Tipo != null)
          {
            CampoParámetrosDeRuta campo = new CampoParámetrosDeRuta(
              RestriccionesDeParámetrosDeRuta.LímitesDeVelocidad[(Tipo)vía.Tipo],
              vía.CampoParámetrosDeRuta.ClaseDeRuta,
              vía.CampoParámetrosDeRuta.OtrosParámetros);
            vía.CambiaCampoParámetrosDeRuta(campo, "Cambiado a Límite de Velocidad Estandar");
          }
        }
        ManejadorDeVías.RestableceEventos();

        // Notifica la edición.
        EnvíaEventoEditó();
      }
    }


    private void EnMenúEstandarizarClaseDeRuta(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeVíasSeleccionadas = Lista.SelectedIndices.Count;
      if (Lista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar la Clase de Ruta
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere Estandarizar la Clase de Ruta en las {0} Vías seleccionadas?", númeroDeVíasSeleccionadas),
        "Estandarizar Clase de Ruta",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      // Estandarizar la Clase de Ruta si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        // Cambia las vías.
        ManejadorDeVías.SuspendeEventos();
        IList<Vía> vías = ObtieneElementosSeleccionados<Vía>();
        foreach (Vía vía in vías)
        {
          if (vía.Tipo != null)
          {
            CampoParámetrosDeRuta campo = new CampoParámetrosDeRuta(
              vía.CampoParámetrosDeRuta.LímiteDeVelocidad,
              RestriccionesDeParámetrosDeRuta.ClasesDeRuta[(Tipo)vía.Tipo],
              vía.CampoParámetrosDeRuta.OtrosParámetros);
            vía.CambiaCampoParámetrosDeRuta(campo, "Cambiado a Clase de Ruta Estandar");
          }
        }
        ManejadorDeVías.RestableceEventos();

        // Notifica la edición.
        EnvíaEventoEditó();
      }
    }


    private void EnMenúEstandarizarLímiteDeVelocidadYClaseDeRuta(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeVíasSeleccionadas = Lista.SelectedIndices.Count;
      if (númeroDeVíasSeleccionadas == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar el Límite de Velocidad y la Clase de Ruta.
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere Estandarizar el Límite de Velocidad y la Clase de Ruta en las {0} Vías seleccionadas?", númeroDeVíasSeleccionadas),
        "Estandarizar Límite de Velocidad y Clase de Ruta",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      // Estandarizar el Límite de Velocidad y la Clase de Ruta si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        // Cambia las vías.
        ManejadorDeVías.SuspendeEventos();
        IList<Vía> vías = ObtieneElementosSeleccionados<Vía>();
        foreach (Vía vía in vías)
        {
          Tipo? tipo = vía.Tipo;
          if (tipo != null)
          {
            CampoParámetrosDeRuta campo = new CampoParámetrosDeRuta(
              RestriccionesDeParámetrosDeRuta.LímitesDeVelocidad[(Tipo)tipo],
              RestriccionesDeParámetrosDeRuta.ClasesDeRuta[(Tipo)tipo],
              vía.CampoParámetrosDeRuta.OtrosParámetros);
            vía.CambiaCampoParámetrosDeRuta(campo, "Cambiado a Límite de Velocidad y Clase de Ruta Estándar");
          }
        }
        ManejadorDeVías.RestableceEventos();

        // Notifica la edición.
        EnvíaEventoEditó();
      }
    }


    private void EnMenúEliminarVías(object elObjecto, EventArgs losArgumentos)
    {
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeVíasSeleccionadas = Lista.SelectedIndices.Count;
      if (númeroDeVíasSeleccionadas == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar el Límite de Velocidad y la Clase de Ruta.
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere Eliminar las {0} Vías seleccionadas?", númeroDeVíasSeleccionadas),
        "Eliminar Vías",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      // Eliminar las Vías si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        // Elimina las vías.
        ManejadorDeVías.SuspendeEventos();
        IList<Vía> vías = ObtieneElementosSeleccionados<Vía>();
        foreach (Vía vía in vías)
        {
          vía.Elimina("Eliminada Manualmente.");
        }
        ManejadorDeVías.RestableceEventos();

        // Notifica la edición.
        EnvíaEventoEditó();
      }
    }
    #endregion
  }
}
