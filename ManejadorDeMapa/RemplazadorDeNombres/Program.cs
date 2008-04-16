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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa;
using System.IO;
using System.Reflection;

namespace GpsYv.RemplazadorDeNombres
{
  class Program
  {
    [STAThread]
    static void Main(string[] args)
    {
      string directorioDeEntrada;
      string archivoConElDiccionario;
      string directorioDeSalida;

      #region Selecciona el directorio de entrada.
      using (FolderBrowserDialog ventanaDeDirectorios = new FolderBrowserDialog())
      {
        ventanaDeDirectorios.Description = "Selecciona el directorio con las fuentes a corregir:";
        ventanaDeDirectorios.ShowNewFolderButton = false;
        ventanaDeDirectorios.SelectedPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        DialogResult respuesta = ventanaDeDirectorios.ShowDialog();

        // Nos salimos si el usuario cancela.
        if (respuesta != DialogResult.OK)
        {
          Console.WriteLine("Cancelado por el usuario.");
          Environment.Exit(1);
        }

        directorioDeEntrada = ventanaDeDirectorios.SelectedPath;
        Console.WriteLine(string.Format("Directorio con las fuentes: {0}", directorioDeEntrada));
      }
      #endregion

      #region Selecciona archivo de conversión.
      using (OpenFileDialog ventanaParaAbrirArchivo = new OpenFileDialog())
      {
        ventanaParaAbrirArchivo.Title = "Selecciona archivo con el diccionario:";
        ventanaParaAbrirArchivo.CheckFileExists = true;
        ventanaParaAbrirArchivo.AutoUpgradeEnabled = true;
        ventanaParaAbrirArchivo.DefaultExt = "csv";
        ventanaParaAbrirArchivo.Filter = "Formato csv (*.csv)|*.csv|"
                                       + "Formato texto (*.txt)|*.txt|"
                                       + "Todos los archivos (*.*)|*.*";

        DialogResult respuesta = ventanaParaAbrirArchivo.ShowDialog();

        // Nos salimos si el usuario cancela.
        if (respuesta != DialogResult.OK)
        {
          Console.WriteLine("Cancelado por el usuario.");
          Environment.Exit(1);
        }

        archivoConElDiccionario = ventanaParaAbrirArchivo.FileName;
        Console.WriteLine(string.Format("Archivo de Conversión: {0}", archivoConElDiccionario));
      }
      #endregion

      #region Selecciona el directorio de salida.
      using (FolderBrowserDialog ventanaDeDirectorios = new FolderBrowserDialog())
      {
        ventanaDeDirectorios.Description = "Selecciona el directorio de salida:";
        ventanaDeDirectorios.ShowNewFolderButton = true;
        DialogResult respuesta = ventanaDeDirectorios.ShowDialog();

        // Nos salimos si el usuario cancela.
        if (respuesta != DialogResult.OK)
        {
          Console.WriteLine("Cancelado por el usuario.");
          Environment.Exit(1);
        }

        directorioDeSalida = ventanaDeDirectorios.SelectedPath;
        Console.WriteLine(string.Format("Directorio de Salida: {0}", directorioDeSalida));
      }
      #endregion

      // Chequea que los directorios no sean los mismos.
      if (directorioDeEntrada == directorioDeSalida)
      {
        Console.WriteLine("ERROR: El directorio de entrada y salida deben ser diferentes.");
        Environment.Exit(1);
      }

      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa.ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa.ManejadorDeMapa(escuchadorDeEstatus);
      RemplazadorDeNombres remplazadorDeNombres = new RemplazadorDeNombres(
        archivoConElDiccionario,
        manejadorDeMapa.ManejadorDeElementos,
        escuchadorDeEstatus);

      DirectoryInfo informaciónDelDirectorio = new DirectoryInfo(directorioDeEntrada);
      FileInfo[] archivosFuente = informaciónDelDirectorio.GetFiles("*.mp");

      foreach (FileInfo archivo in archivosFuente)
      {
        Console.WriteLine(string.Format("Procesando '{0}' ... ", archivo.FullName));
        Console.WriteLine();

        // Lee mapa.
        Console.Write("Leyendo mapa ... ");
        manejadorDeMapa.Abrir(archivo.FullName);
        Console.WriteLine("listo.");

        // Cambia nombres.
        Console.Write("Cambiando nombres ... ");
        int número = remplazadorDeNombres.Procesa();
        Console.WriteLine(string.Format(" cambiados {0} nombres", número));

        // Verifica que el archivo de salida no existe.
        string archivoDeSalida = Path.Combine(directorioDeSalida, archivo.Name);
        if (File.Exists(archivoDeSalida))
        {
          DialogResult respuesta = MessageBox.Show(
            string.Format("El archivo de salida '{0}' existe. El directorio de salida debe estar vacio. El programa terminará.", archivoDeSalida),
            "Archivo de Salida",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);

          Console.WriteLine("Archivo de salida existe.");
          Environment.Exit(1);
          break;
        }

        // Escribe el archivo de salida.
        Console.Write(string.Format("Guardando mapa '{0}' ... ", archivoDeSalida));
        manejadorDeMapa.GuardaEnFormatoPolish(
          archivoDeSalida,
          string.Format("Generado por {0} @ {1}", Assembly.GetExecutingAssembly().GetName().Name, DateTime.Now));
        Console.WriteLine("listo.");
        Console.WriteLine();
      }
    }
  }
}
