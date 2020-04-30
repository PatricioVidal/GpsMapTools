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

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Contiene las Restricciones de Parámetros de Ruta de las vías.
  /// </summary>
  public class RestriccionesDeParámetrosDeRuta
  {
    #region Campos
    private const string miArchivoDeRestriccionesDeParámetrosDeRuta = @"Vías\RestriccionesDeParámetrosDeRuta.csv";
    #endregion

    #region Propiedades
    /// <summary>
    /// Diccionario con los límites de velocidad de vías.
    /// </summary>
    public readonly static IDictionary<Tipo, LímiteDeVelocidad> LímitesDeVelocidad = new Dictionary<Tipo, LímiteDeVelocidad>();


    /// <summary>
    /// Diccionario con las clases de ruta de vías.
    /// </summary>
    public readonly static IDictionary<Tipo, ClaseDeRuta> ClasesDeRuta = new Dictionary<Tipo, ClaseDeRuta>();
    #endregion

    #region Métodos Privados
    private class LectorRestriccionesDeParámetrosDeRuta : LectorDeArchivo
    {
      private readonly IDictionary<Tipo, LímiteDeVelocidad> misLímitesDeVelocidad;
      private readonly IDictionary<Tipo, ClaseDeRuta> misClasesDeRuta;

      public LectorRestriccionesDeParámetrosDeRuta(
        string elArchivo,
        IDictionary<Tipo, LímiteDeVelocidad> losLímitesDeVelocidad,
        IDictionary<Tipo, ClaseDeRuta> lasClasesDeRuta)
      {
        misLímitesDeVelocidad = losLímitesDeVelocidad;
        misClasesDeRuta = lasClasesDeRuta;

        Lee(elArchivo);
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

          // Verifica que tenemos a menos 3 partes.
          if (partes.Length < 3)
          {
            throw new ArgumentException("No se encontraron 3 partes separadas por coma en la linea: " + línea);
          }

          // Lee las tres partes.
          Tipo tipo = new Tipo(partes[0]);
          LímiteDeVelocidad límiteDeVelocidad = new LímiteDeVelocidad(Convert.ToInt32(partes[1]));
          ClaseDeRuta claseDeRuta = new ClaseDeRuta(Convert.ToInt32(partes[2]));

          // Asegura que el tipo es válido.
          if (!TiposDeVías.Tipos.Contains(tipo))
          {
            throw new ArgumentException("El tipo de vía no es válido: " + tipo);
          }

          // Llena los diccionarios.
          misLímitesDeVelocidad.Add(tipo, límiteDeVelocidad);
          misClasesDeRuta.Add(tipo, claseDeRuta);
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    static RestriccionesDeParámetrosDeRuta()
    {
      // Lee las características de polígonos.
      new LectorRestriccionesDeParámetrosDeRuta(
        miArchivoDeRestriccionesDeParámetrosDeRuta,
        LímitesDeVelocidad,
        ClasesDeRuta);
    }
    #endregion
  }
}
