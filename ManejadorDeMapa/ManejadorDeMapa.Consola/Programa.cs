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
using System.Reflection;
using Genghis;
using System.IO;

class Argumentos : CommandLineParser
{
  [ValueUsage("Directorio de Entrada.", Name = "de", ValueName = "directorio")]
  public string DirectorioDeEntrada { get; set; }

  [ValueUsage("Directorio de Salida.", Name = "ds", ValueName = "directorio")]
  public string DirectorioDeSalida { get; set; }

  [ValueUsage("Procesamientos: ProcesarTodo, ArreglaIndices", 
    Name = "p", ValueName = "procesamiento")]
  public IList<string> Procesamientos = new List<string>();
}

namespace GpsYv.ManejadorDeMapa.Consola
{
  class Programa
  {
    static void Main(string[] losArgumentos)
    {
      // Procesa los argumentos.
      Argumentos argumentos = new Argumentos();
      if (!argumentos.ParseAndContinue(losArgumentos))
      {
        return;
      }

      if (argumentos.DirectorioDeEntrada == null)
      {
        Console.WriteLine(
          argumentos.GetUsage("ERROR: Falta directorio de entrada."));
        Environment.Exit(1);
      }

      if (argumentos.DirectorioDeSalida == null)
      {
        Console.WriteLine(
          argumentos.GetUsage("ERROR: Falta directorio de salida."));
        Environment.Exit(1);
      }
      
      // Chequea que los directorios no sean los mismos.
      if (argumentos.DirectorioDeEntrada == argumentos.DirectorioDeSalida)
      {
        Console.WriteLine(
          argumentos.GetUsage("ERROR: El directorio de entrada y salida deben ser diferentes."));
        Environment.Exit(1);
      }

      // Procesa cada archivo en el directorio fuente.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      DirectoryInfo informaciónDelDirectorio = new DirectoryInfo(argumentos.DirectorioDeEntrada);
      FileInfo[] archivosFuente = informaciónDelDirectorio.GetFiles("*.mp");
      foreach (FileInfo archivo in archivosFuente)
      {
        // Verifica que el archivo de salida no existe.
        string archivoDeSalida = Path.Combine(argumentos.DirectorioDeSalida, archivo.Name);
        if (File.Exists(archivoDeSalida))
        {
          Console.WriteLine(string.Format("ERROR: Archivo de salida '{0}' ya existe.", archivoDeSalida));
          Environment.Exit(1);
          break;
        }

        // Lee mapa.
        Console.Write(string.Format("Leyendo '{0}' ... ", archivo.FullName));
        manejadorDeMapa.Abrir(archivo.FullName);
        Console.WriteLine("listo.");

        // Procesa cada uno de los 'procesamientos'.
        Console.WriteLine("Procesando ... ");
        foreach (string procesamiento in argumentos.Procesamientos)
        {
          Console.Write(string.Format(" -> {0} ...", procesamiento));
          int númeroDeProblemas = 0;
          switch (procesamiento)
          {
            case "ProcesarTodo":
              {
                númeroDeProblemas += manejadorDeMapa.ProcesarTodo();
              }
              break;
            case "ArreglaIndices":
              {
                númeroDeProblemas += manejadorDeMapa.ManejadorDePdis.ArregladorDeIndicesDeCiudad.Procesa();
                númeroDeProblemas += manejadorDeMapa.ManejadorDeVías.ArregladorDeIndicesDeCiudad.Procesa();
              }
              break;
            default:
              Console.WriteLine(
                argumentos.GetUsage(string.Format("ERROR: Procesamiento '{0}' es desconocido.", procesamiento)));
              Environment.Exit(1);
              break;
          }

          // Imprime el número de problemas encontrados.
          Console.WriteLine(string.Format(" {0} problemas.", númeroDeProblemas));

          // Imprime cambios.
          ImprimeCambios(manejadorDeMapa.ManejadorDePdis.Elementos, "PDI");
          ImprimeCambios(manejadorDeMapa.ManejadorDeVías.Elementos, "Via");
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


    private static void ImprimeCambios<T>(IEnumerable<T> losElementos, string elTipoDeElemento) where T: ElementoDelMapa
    {
      int numeroDeModificaciones = (from e in losElementos
                                    where e.FuéModificado && !e.FuéEliminado
                                    select e.Número).Count();
      Console.WriteLine(string.Format(" {0} {1}(s) modificado(a)(s).", numeroDeModificaciones, elTipoDeElemento));
      int numeroDeEliminaciones = (from e in losElementos
                                   where e.FuéEliminado
                                   select e.Número).Count();
      Console.WriteLine(string.Format(" {0} {1}(s) eliminado(a)(s).", numeroDeEliminaciones, elTipoDeElemento));
    }
  }
}
