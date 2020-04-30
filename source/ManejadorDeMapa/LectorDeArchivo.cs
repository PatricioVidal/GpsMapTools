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
using System.Text;
using System.IO;
using System.Reflection;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Lector de archivo.
  /// </summary>
  public abstract class LectorDeArchivo
  {
    #region Campos
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private StreamReader miLector;
    private long miNúmeroDeLínea;
    private string miLínea = string.Empty;

    /// <summary>
    /// Esta codificación permite intepretar correctamente los acentos de 
    /// archivos ANSI-8 bytes.
    /// </summary>
    private readonly Encoding miCodificaciónPorDefecto = Encoding.GetEncoding(1252);
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene el número de línea en el archivo.
    /// </summary>
    public long NúmeroDeLínea
    {
      get
      {
        return miNúmeroDeLínea;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    protected LectorDeArchivo()
      : this (new EscuchadorDeEstatusPorOmisión())
    {
    }

    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    protected LectorDeArchivo(IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      miEscuchadorDeEstatus = elEscuchadorDeEstatus;

      // Reporta estatus.
      miEscuchadorDeEstatus.Progreso = 0;
    }


    /// <summary>
    /// Lee un archivo.
    /// </summary>
    /// <param name="elArchivo">El archivo a abrir.</param>
    public void Lee(string elArchivo)
    {
      // Abre el archivo en modo de texto y empieza a leerlo
      // linea por linea.
      string línea = string.Empty;
      try
      {
        // Crea el camino absoluto al archivo.
        string caminoAbsolutoAlArchivo = elArchivo;
        if (!Path.IsPathRooted(elArchivo))
        {
          string directorio = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
          caminoAbsolutoAlArchivo = Path.Combine(directorio, elArchivo);
        }

        // Abre el archivo.
        using (miLector = new StreamReader(caminoAbsolutoAlArchivo, miCodificaciónPorDefecto))
        {
          // Establece el límite superior de la barra de progreso.
          miEscuchadorDeEstatus.ProgresoMáximo = miLector.BaseStream.Length;

          // Procesa todas las líneas del archivo.
          línea = LeeLaPróximaLínea();
          while (línea != null)
          {
            // Reportar Progreso
            miEscuchadorDeEstatus.Progreso = miLector.BaseStream.Position;

            ProcesaLínea(línea);

            // Lee la próxima línea.
            línea = LeeLaPróximaLínea();
          }
        }
      }
      catch (Exception e)
      {
        string mensaje = "Error leyendo archivo '" + elArchivo + "' en la línea " + miNúmeroDeLínea + ":\n"
          + miLínea + "\n";

        throw new ArgumentException(mensaje, e);
      }
      finally
      {
        // Borra la barra de progreso.
        miEscuchadorDeEstatus.Progreso = 0;
      }
    }
    #endregion

    #region Métodos Protegidos y Privados
    /// <summary>
    /// Procesa una línea.
    /// </summary>
    /// <param name="laLínea">La línea.</param>
    protected abstract void ProcesaLínea(string laLínea);


    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    protected string Estatus
    {
      get
      {
        return miEscuchadorDeEstatus.Estatus;
      }

      set
      {
        miEscuchadorDeEstatus.Estatus = value;
      }
    }


    /// <summary>
    /// Devuelve la próxima línea.
    /// </summary>
    protected string LeeLaPróximaLínea()
    {
      miLínea = miLector.ReadLine();
      ++miNúmeroDeLínea;

      return miLínea;
    }
    #endregion
  }
}
