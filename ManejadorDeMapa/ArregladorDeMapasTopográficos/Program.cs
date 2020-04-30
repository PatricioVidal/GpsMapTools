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

using GpsYv.ManejadorDeMapa;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GpsYv.ArregladorDeMapasTopográficos
{
  class Program
  {
    [STAThread]
    static void Main()
    {
      string directorioDeEntrada;
      string directorioDeSalida;

      #region Selecciona el directorio de entrada.
      using (FolderBrowserDialog ventanaDeDirectorios = new FolderBrowserDialog())
      {
        ventanaDeDirectorios.Description = "Selecciona el directorio con las fuentes:";
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

        // Arreglands Mapa Topográfico.
        Console.WriteLine("Arreglando Mapa Topográfico ... ");
        ArreglaMapaTopografico(manejadorDeMapa);

        // Verifica que el archivo de salida no existe.
        string archivoDeSalida = Path.Combine(directorioDeSalida, archivo.Name);
        if (File.Exists(archivoDeSalida))
        {
          MessageBox.Show(
            string.Format("El archivo de salida '{0}' existe. El directorio de salida debe estar vacío. El programa terminará.", archivoDeSalida),
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


    private static void ArreglaMapaTopografico(ManejadorDeMapa.ManejadorDeMapa elManejadorDeMapa)
    {
      #region Arregla el encabezado.
      ElementoDelMapa encabezado = elManejadorDeMapa.Encabezado;
      IList<Campo> camposDelEncabezado = encabezado.Campos;
      CampoGenérico campoNombre = (CampoGenérico)camposDelEncabezado[1];

      // Borra los campos para re-generarlos.
      camposDelEncabezado.Clear();
      camposDelEncabezado.Add(new CampoGenérico("CodePage", "1252"));
      camposDelEncabezado.Add(new CampoGenérico("LblCoding", "9"));
      camposDelEncabezado.Add(new CampoGenérico("ID", campoNombre.Texto));
      camposDelEncabezado.Add(campoNombre);

      // Añade los campos nuevos.
      Campo[] camposNuevos = new Campo[] {
        new CampoGenérico("Elevation", "M"),
        new CampoGenérico("Preprocess", "F"),
        new CampoGenérico("TreSize", "5000"),
        new CampoGenérico("TreMargin", "0.00000"),
        new CampoGenérico("RgnLimit", "1024"),
        new CampoGenérico("Transparent", "Y"),
        new CampoGenérico("Copyright", "www.gpsyv.net"),
        new CampoGenérico("Levels", "5"),
        new CampoGenérico("Level0", "24"),
        new CampoGenérico("Level1", "22"),
        new CampoGenérico("Level2", "20"),
        new CampoGenérico("Level3", "19"),
        new CampoGenérico("Level4", "18"),
        new CampoGenérico("Zoom0", "0"),
        new CampoGenérico("Zoom1", "1"),
        new CampoGenérico("Zoom2", "2"),
        new CampoGenérico("Zoom3", "3"),
        new CampoGenérico("Zoom4", "4"),
        };
      foreach (Campo campo in camposNuevos)
      {
        camposDelEncabezado.Add(campo);
      }
      Console.WriteLine("  arreglado encabezado...");
      #endregion

      #region Arregla las Polilíneas
      foreach (var polilínea in elManejadorDeMapa.Polilíneas)
      {
        IList<Campo> campos = polilínea.Campos;

        // Busca el campo de tipo.
        int? índiceCampoTipo = null;
        for (int i = 0; i < campos.Count; ++i)
        {
          if (campos[i] is CampoTipo)
          {
            índiceCampoTipo = i;
            break;
          }
        }

        // Add EndLevel después del campo tipo.
        if (índiceCampoTipo != null)
        {
          int i = (int)índiceCampoTipo + 1;
          campos.Insert(i, new CampoGenérico("EndLevel", "3"));
        }
      }
      Console.WriteLine("  arregladas polilíneas...");
      #endregion

      #region Elimina los elementos con Data1 y Data2, y los polígonos.
      IList<ElementoDelMapa> elementos = elManejadorDeMapa.ManejadorDeElementos.Elementos;
      int últimoIndex = elementos.Count - 1;
      for (int i = últimoIndex; i >= 0; --i)
      {
        ElementoDelMapa elemento = elementos[i];

        // Remueve los polígonos.
        if (elemento is Polígono)
        {
          elementos.RemoveAt(i);
        }
        // Si no es polígono entonces vemos si es una polilínea
        // y la removemos si no tiene data0.
        else if (elemento is Polilínea)
        {
          // Buscamos si tiene data0.
          bool tieneData0 = false;
          foreach (var campo in elemento.Campos)
          {
            CampoCoordenadas campoCoordenadas = campo as CampoCoordenadas;
            if (campoCoordenadas != null)
            {
              if (campoCoordenadas.Nivel == 0)
              {
                tieneData0 = true;
                break;
              }
            }
          }
          
          // Removemos el elemento si no tiene data0.
          if (!tieneData0)
          {
            elementos.RemoveAt(i);
          }
        }
      }
      Console.WriteLine("  eliminados polilíneas sin data 0, y polígonos...");
      #endregion
    }
  }
}
