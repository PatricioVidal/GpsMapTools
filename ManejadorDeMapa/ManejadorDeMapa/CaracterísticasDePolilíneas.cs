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
using System.Drawing;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Clase que contiene las características de los PDIs.
  /// </summary>
  static class CaracterísticasDePolilíneas
  {
    #region Campos
    private static readonly string miArchivoDeCaracterísticasDePolilíneas = "CaracterísticasDePolilíneas.csv";

    /// <summary>
    /// Diccionario de lápices por tipo de polilinea.
    /// </summary>
    /// <remarks>
    public readonly static IDictionary<Tipo, Pen> misLápices = new Dictionary<Tipo, Pen>();
    #endregion

    #region Campos y Métodos Públicos
    /// <summary>
    /// Diccionario de descripciones por tipo de polilinea.
    /// </summary>
    public readonly static IDictionary<Tipo, string> Descripciones = new Dictionary<Tipo, string>();


    /// <summary>
    /// Devuelve el lápiz para un tipo de polilinea dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polilinea.</param>
    public static Pen Lápiz(Tipo elTipo)
    {
      Pen lápiz;
      bool existe = misLápices.TryGetValue(elTipo, out lápiz);
      if (!existe)
      {
        // Lápiz por defecto.
        lápiz = misLápices[Tipo.TipoVacio];
      }
      return lápiz;
    }


    /// <summary>
    /// Devuelve la descripción para un tipo de polilínea dado.
    /// </summary>
    /// <param name="elTipo">El tipo de polígono.</param>
    public static string Descripción(Tipo elTipo)
    {
      string descripcion;
      bool existe = Descripciones.TryGetValue(elTipo, out descripcion);
      if (!existe)
      {
        // Descripcion por defecto.
        descripcion = Descripciones[Tipo.TipoVacio];
      }
      return descripcion;
    }
    #endregion
    #region Métodos Privados
    private class LectorDeCaracterísticasDePolilíneas : LectorDeArchivo
    {
      public readonly IDictionary<Tipo, Pen> misLápices;
      public readonly IDictionary<Tipo, string> misDescripciones;

      public LectorDeCaracterísticasDePolilíneas(
        string elArchivo,
        IDictionary<Tipo, Pen> losLápices,
        IDictionary<Tipo, string> lasDescripciones)
      {
        misLápices = losLápices;
        misDescripciones = lasDescripciones;

        Abrir(elArchivo);
      }


      protected override void ProcesaLínea(string laLínea)
      {
        // Elimina espacios en blanco.
        string línea = laLínea.Trim();

        // Saltarse lineas en blanco y comentarios.
        bool laLíneaEstaEnBlanco = (línea == string.Empty);
        bool laLíneaEsComentario = línea.StartsWith("//");
        if (!laLíneaEstaEnBlanco & !laLíneaEsComentario)
        {
          // Separa las letras.
          string[] partes = línea.Split(',');

          // Verifica que tenemos dos partes.
          if (partes.Length != 4)
          {
            throw new ArgumentException("No se encontraron 4 partes separadas por coma en la linea: " + línea);
          }

          // Lee las cuatro partes.
          Tipo tipo = new Tipo(partes[0]);
          Color color = Color.FromName(partes[1]);
          int ancho = int.Parse(partes[2]);
          string descripción = partes[3];

          // Llena los diccionarios.
          Pen lápiz = new Pen(color, ancho);
          misLápices[tipo] = lápiz;
          Descripciones[tipo] = descripción;
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    static CaracterísticasDePolilíneas()
    {
      // Lee las características de polígonos.
      LectorDeCaracterísticasDePolilíneas lector = new LectorDeCaracterísticasDePolilíneas(
        miArchivoDeCaracterísticasDePolilíneas,
        misLápices,
        Descripciones);
    }
    #endregion
  }
}
