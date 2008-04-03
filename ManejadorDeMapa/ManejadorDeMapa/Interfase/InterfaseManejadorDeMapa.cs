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
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using GpsYv.ManejadorDeMapa.Properties;
using System.Reflection;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Interfase del manejador de mapa.
  /// </summary>
  public partial class InterfaseManejadorDeMapa : Form
  {
    #region Campos
    private ManejadorDeMapa miManejadorDeMapa;
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private readonly Dictionary<TabPage, int> misIndicesDePestañas = new Dictionary<TabPage, int>();
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseManejadorDeMapa()
    {
      InitializeComponent();

      // Pone el nombre.
      this.Text = VentanaDeAcerca.AssemblyDescription + " - " + VentanaDeAcerca.AssemblyCompany;

      #region Asigna los ToolTips de los menús.
      miMenuAceptarModificaciones.ToolTipText = GpsYv.ManejadorDeMapa.ManejadorDeMapa.DescripciónAceptarModificaciones;

      // PDIs.
      miMenuProcesarTodoEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.ManejadorDePDIs.DescripciónProcesarTodo;
      miMenúEliminarCaracteresEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.EliminadorDeCaracteres.Descripción;
      miMenuArreglarLetrasEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.ArregladorDeLetras.Descripción;
      miMenuArreglarPalabrasEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.ArregladorDePalabrasPorTipo.Descripción;
      miMenúBuscaDuplicadosEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.BuscadorDeDuplicados.Descripción;
      miMenúBuscarErroresEnPDIs.ToolTipText = GpsYv.ManejadorDeMapa.PDIs.BuscadorDeErrores.Descripción;

      // Vías.
      miMenúProcesarTodoEnVías.ToolTipText = GpsYv.ManejadorDeMapa.Vías.ManejadorDeVías.DescripciónProcesarTodo;
      miMenúBuscarErroresEnVías.ToolTipText = GpsYv.ManejadorDeMapa.Vías.BuscadorDeErrores.Descripción;
      #endregion

      // Crea y asigna el escuchador de estatus.
      miEscuchadorDeEstatus = new EscuchadorDeEstatus(
        this,
        miTextoDeEstatus,
        miBarraDeProgreso,
        miTextoDeCoordenadas);

      miManejadorDeMapa = new ManejadorDeMapa(miEscuchadorDeEstatus);

      // Maneja eventos de modificación de elementos.
      miManejadorDeMapa.MapaNuevo += EnMapaNuevo;
      miManejadorDeMapa.ElementosModificados += EnElementosModificados;

      // Pone el método llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Asigna las propiedades de la interfase de mapa.
      miInterfaseDeMapa.ManejadorDeMapa = miManejadorDeMapa;
      miInterfaseDeMapa.EscuchadorDeEstatus = miEscuchadorDeEstatus;

      // Asigna las propiedades de la interfase de PDIs.
      miInterfaseManejadorDePDIs.ManejadorDeMapa = miManejadorDeMapa;
      miInterfaseManejadorDePDIs.EscuchadorDeEstatus = miEscuchadorDeEstatus;

      // Asigna las propiedades de la interfase de Vías.
      miInterfaseManejadorDeVías.ManejadorDeMapa = miManejadorDeMapa;
      miInterfaseManejadorDeVías.EscuchadorDeEstatus = miEscuchadorDeEstatus;

      // Crea el diccionario de índices de pestañas.
      TabControl.TabPageCollection pestañas = miControladorDePestañasPrincipal.TabPages;
      for (int i = 0; i < pestañas.Count; ++i)
      {
        misIndicesDePestañas[pestañas[i]] = i;
      }

      // Maneja evento de cambio de Estado Máximo de Pestañas de PDIs.
      miInterfaseManejadorDePDIs.CambióEstadoMáximoDePestañas +=
        delegate(object elEnviador, ControladorDePestañas.CambióEstadoMáximoDePestañasEventArgs losArgumentos)
        {
          miControladorDePestañasPrincipal.PoneEstadoDePestaña(
            misIndicesDePestañas[miPaginaDePDIs], losArgumentos.EstadoMáximoDePestañas);
        };

      // Maneja evento de cambio de Estado Máximo de Pestañas de Vías.
      miInterfaseManejadorDeVías.CambióEstadoMáximoDePestañas +=
        delegate(object elEnviador, ControladorDePestañas.CambióEstadoMáximoDePestañasEventArgs losArgumentos)
        {
          miControladorDePestañasPrincipal.PoneEstadoDePestaña(
            misIndicesDePestañas[miPáginaDeVías], losArgumentos.EstadoMáximoDePestañas);
        };
    }
    #endregion
      
    #region Métodos Privados
    private void EnMenuSalir(object sender, EventArgs e)
    {
      Application.Exit();
    }


    private void EnMenuAbrir(object elRemitente, EventArgs losArgumentos)
    {
      using (OpenFileDialog ventanaParaAbrirArchivo = new OpenFileDialog())
      {
        ventanaParaAbrirArchivo.CheckFileExists = true;
        ventanaParaAbrirArchivo.AutoUpgradeEnabled = true;
        ventanaParaAbrirArchivo.DefaultExt = "txt";
        ventanaParaAbrirArchivo.Filter = ManejadorDeMapa.FiltrosDeExtensiones;

        DialogResult respuesta = ventanaParaAbrirArchivo.ShowDialog();

        switch (respuesta)
        {
          case DialogResult.OK:
            string archivo = ventanaParaAbrirArchivo.FileName;
            try
            {
              miManejadorDeMapa.Abrir(archivo);

              // Deshabilita el menu de Guardar porque el mapa se acaba de leer.
              miMenuGuardar.Enabled = false;

              // Habilita el menú de "Guardar Como ..." porque hay un mapa en memoria.
              miMenuGuardarComo.Enabled = true;
            }
            catch (Exception e)
            {
              Programa.MuestraExcepción("Error abriendo archivo " + archivo, e);
            }
            break;
        }
      }
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    private void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // Deshabilita los menus de Guardar.
      miMenuAceptarModificaciones.Enabled = false;

      // Actualiza la lista de elementos.
      miLista.RegeneraLista();
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    private void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Actualiza la lista de elementos.
      miLista.RegeneraLista();

      // Habilita los menus de Guardar.
      miMenuGuardar.Enabled = true;
      miMenuAceptarModificaciones.Enabled = true;
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<ElementoDelMapa> elementosDelMapa = miManejadorDeMapa.Elementos;
      foreach (ElementoDelMapa elementoDelMapa in elementosDelMapa)
      {
        laLista.AñadeItem(elementoDelMapa, elementoDelMapa.Clase);
      }

      // Actualiza las Pestañas.
      miPaginaDeElementos.Text = "Elementos (" + laLista.NúmeroDeElementos + ")";
      miPaginaDePDIs.Text = "PDIs (" + miManejadorDeMapa.PDIs.Count + ")";
      miPáginaDeVías.Text = "Vías (" + miManejadorDeMapa.Vías.Count + ")";
    }


    private void EnMenuAcerca(object sender, EventArgs e)
    {
      Form ventanaDeAcerca = new Interfase.VentanaDeAcerca();
      ventanaDeAcerca.ShowDialog();
    }


    private void EnMenuGuardarComo(object sender, EventArgs e)
    {
      string archivo = miManejadorDeMapa.Archivo;

      // Verifica que el archivo es válido.
      if (string.IsNullOrEmpty(archivo))
      {
        throw new InvalidOperationException("No se ha abierto ningún archivo.");
      }

      // Crea el nombre del archivo de salida.
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileName(archivo);
      string nombreDeSalida = Path.ChangeExtension(nombre , ".Corregido.mp");

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog();
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
        GuardaMapa(ventanaDeGuardar.FileName);
      }
    }


    private void GuardaMapa(string elArchivo)
    {
      try
      {
        miManejadorDeMapa.GuardaEnFormatoPolish(
          elArchivo, 
          "Generado por " +
          VentanaDeAcerca.AssemblyName + " " + 
          VentanaDeAcerca.AssemblyVersion + " - " +
          VentanaDeAcerca.AssemblyDescription);

        // Deshabilita el menu de Guardar porque se acaba de guardar el mapa.
        miMenuGuardar.Enabled = false;
      }
      catch (Exception e)
      {
        Programa.MuestraExcepción("Error guardando archivo " + elArchivo, e);
      }
    }


    private void EnMenuGuardar(object sender, EventArgs e)
    {
      string archivo = miManejadorDeMapa.Archivo;

      // Verifica que el archivo es válido.
      if (string.IsNullOrEmpty(archivo))
      {
        throw new InvalidOperationException("No se ha abierto ningún archivo.");
      }

      // Asegurarse que el usuario quiere sobre-escribir el archivo.
      DialogResult respuesta = MessageBox.Show(
        "Esta seguro que quiere sobre-escribir " + Path.GetFileName(archivo),
        "Guardar Archivo",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);
      if (respuesta == DialogResult.Yes)
      {
        GuardaMapa(archivo);
      }
    }


    private void EnMenuArreglarPalabrasEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ArreglarPalabras();
    }


    private void EnMenuArreglarLetrasEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ArreglarLetras();
    }


    private void EnMenuProcesarTodoEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ProcesarTodo();
    }


    private void EnMenúEliminarCaracteresEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.EliminaCaracteresInválidos();
    }

    
    private void EnMenuBuscarDuplicadosEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
    }


    private void EnMenuBuscarErroresEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.BuscadorDeErrores.Procesa();
    }


    private void EnMenuPáginaWeb(object elEnviador, EventArgs losArgumentos)
    {
      System.Diagnostics.Process.Start("IExplore.exe", ((ToolStripItem)elEnviador).Text);
    }


    private void EnCargarForma(object sender, EventArgs e)
    {
      // Este código esta basado en http://www.codeproject.com/KB/cs/UserSettings.aspx

      // Lee la posición y el tamaño de la Forma de la configuración
      // del usuario. Estos valores se asignan solo si son válidos.
      if (Settings.Default.PosiciónDeLaFormaPrincipal != null)
      {
        if (
          (Settings.Default.PosiciónDeLaFormaPrincipal.X >= 0) &
          (Settings.Default.PosiciónDeLaFormaPrincipal.Y >= 0))
        {
          this.Location = Settings.Default.PosiciónDeLaFormaPrincipal;
        }
      }
      if (Settings.Default.TamañoDeLaFormaPrincipal != null)
      {
        if (
          (Settings.Default.TamañoDeLaFormaPrincipal.Width >= MinimumSize.Width) &
          (Settings.Default.TamañoDeLaFormaPrincipal.Height >= MinimumSize.Height))
        {
          this.Size = Settings.Default.TamañoDeLaFormaPrincipal;
        }
      }
    }


    private void EnCerrarForma(object sender, FormClosingEventArgs e)
    {
      // Este código esta basado en http://www.codeproject.com/KB/cs/UserSettings.aspx

      // Guarda la posición y el tamaño de la Forma a la configuración
      // del usuario.  
      // Si la Forma está Minimizada o Maximizada entonces hay que
      // guardar los valores de la propiedad "RestoreBounds".
      switch (this.WindowState)
      {
        case FormWindowState.Normal:
          Settings.Default.PosiciónDeLaFormaPrincipal = this.Location;
          Settings.Default.TamañoDeLaFormaPrincipal = this.Size;
          break;
        default:
          Settings.Default.PosiciónDeLaFormaPrincipal = this.RestoreBounds.Location;
          Settings.Default.TamañoDeLaFormaPrincipal = this.RestoreBounds.Size;
          break;
      }

      // Guarda configuración en disco.
      // El archivo es guardado en:
      // <Profile Directory>\<Company Name>\<App Name>_<Evidence Type>_<Evidence Hash>\<Version>\user.config
      // Ver aquí para detalles: http://blogs.msdn.com/rprabhu/articles/433979.aspx 
      Settings.Default.Save();
    }


    private void EnMenúAceptarModificaciones(object sender, EventArgs e)
    {
      miManejadorDeMapa.AceptaModificaciones();
    }


    private void EnMenuProcesarTodoEnVías(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDeVías.ProcesarTodo();
    }


    private void EnMenúBuscarErroresEnVías(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDeVías.BuscadorDeErrores.Procesa();
    }


    private void EnMenúProcesarTodo(object sender, EventArgs e)
    {
      miManejadorDeMapa.ProcesarTodo();
    }


    private void EnMenúBuscarIncongruenciasEnVías(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDeVías.BuscadorDeIncongruencias.Procesa();
    }
    #endregion
  }
}
