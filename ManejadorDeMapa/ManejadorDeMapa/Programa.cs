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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using GpsYv.ManejadorDeMapa.Properties;

namespace GpsYv.ManejadorDeMapa
{
  static class Programa
  {
    #region Métodos Públicos
    /// <summary>
    /// Punto de entrada de la applicación.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // Actualiza las opciones del usuario si es necesario.
      if (Settings.Default.RequireActualizarOpcionesDelUsuario)
      {
        // Actualiza las opciones del usuario.
        Settings.Default.Upgrade();

        // Previene que se actualizen las opciones del usuario de nuevo.
        Settings.Default.RequireActualizarOpcionesDelUsuario = false;
      }

      // Crea un rastro (Trace)
      TextWriterTraceListener escritorDeRastro = new TextWriterTraceListener("Rastro.log");
      Trace.AutoFlush = true;
      Trace.WriteLine("Comenzando Aplicación");
      Trace.Indent();

      // Pone opciones.
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      // Corre la applicación.
      try
      {
        Application.Run(new Interfase.InterfaseManejadorDeMapa());
      }
      catch (Exception e)
      {
        MuestraExcepción("Error irrecuperable. La aplicación va a cerrar.", e);
      }

      // Finaliza el rastro.
      Trace.Unindent();
      Trace.Flush();
    }


    /// <summary>
    /// Muestra información sobre una excepción dada.
    /// </summary>
    /// <param name="elMensaje">El mensaje.</param>
    /// <param name="laExcepción">La excepción dada.</param>
    public static void MuestraExcepción(string elMensaje, Exception laExcepción)
    {
      // Crea un archivo de registro.
      string archivoDeRegistro = Interfase.VentanaDeAcerca.AssemblyName + ".Error.log";
      archivoDeRegistro = Path.GetFullPath(archivoDeRegistro);
      using (StreamWriter registro = new StreamWriter(archivoDeRegistro, true))
      {
        string encabezado = DateTime.Now + ": " + elMensaje;
        registro.WriteLine(encabezado);
        registro.WriteLine(laExcepción);
        registro.WriteLine();
      }

      // Muestra la excepción al usuario.
      StringBuilder mensaje = new StringBuilder();
      mensaje.AppendLine(elMensaje);
      mensaje.AppendLine(laExcepción.Message);
      Exception innerException = laExcepción.InnerException;
      while (innerException != null)
      {
        mensaje.AppendLine(innerException.Message);
        innerException = innerException.InnerException;
      }
      mensaje.AppendLine();
      mensaje.AppendFormat("NOTA: Vea archivo '{0}' para mas detalles.", archivoDeRegistro);
      MessageBox.Show(
        mensaje.ToString(),
        Interfase.VentanaDeAcerca.AssemblyName,
        MessageBoxButtons.OK,
        MessageBoxIcon.Error);
    }

    #endregion
  }
}
