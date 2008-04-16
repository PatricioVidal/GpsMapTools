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
using System.Text;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using GpsYv.ManejadorDeMapa;

namespace GpsYv.RemplazadorDeNombres
{
  /// <summary>
  /// Remplazador de nombres de elementos.
  /// </summary>
  public class RemplazadorDeNombres : ProcesadorBase<ManejadorDeElementos, ElementoDelMapa>
  {
    #region Campos
    private readonly LectorDeDiccionarioDeNombres miLectorDeCorrecciónDeNombres;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elArchivoConElDiccionario">El Archivo con el Diccionario.</param>
    /// <param name="elManejadorDeElementos">El manejador de Elementos.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public RemplazadorDeNombres(
      string elArchivoConElDiccionario,
      ManejadorDeElementos elManejadorDeElementos,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeElementos, elEscuchadorDeEstatus)
    {
      miLectorDeCorrecciónDeNombres = new LectorDeDiccionarioDeNombres(
        elArchivoConElDiccionario,
        elEscuchadorDeEstatus);
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPDI">El PDI.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(ElementoDelMapa elElemento)
    {
      int númeroDeProblemasDetectados = 0;
      IDictionary<string, string> diccionario = miLectorDeCorrecciónDeNombres.DiccionarioDeNombres;
      string nombreOriginal = elElemento.Nombre;
      if (diccionario.ContainsKey(nombreOriginal))
      {
        ++númeroDeProblemasDetectados;
        elElemento.CambiaNombre(diccionario[nombreOriginal], "Cambiado según el diccionario.");
      }

      return númeroDeProblemasDetectados;
    }
    #endregion

    #region Clases y Métodos Privadas
    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aquí.
    }


    private class LectorDeDiccionarioDeNombres : LectorDeArchivo
    {
      private readonly string miArchivo;
      private readonly Dictionary<string, string> miDiccionarioDeNombres = new Dictionary<string, string>();

      
      public IDictionary<string, string> DiccionarioDeNombres
      {
        get
        {
          return miDiccionarioDeNombres;
        }
      }


      public LectorDeDiccionarioDeNombres(
        string elArchivo,
        IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        miArchivo = elArchivo;
        Lee(miArchivo);
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
          // Separa las palabras.
          string[] partes = línea.Split('|');

          // Verifica que tenemos dos partes.
          if (partes.Length != 2)
          {
            throw new ArgumentException(string.Format("No se encontraron 2 partes separadas por | en la linea: {0}", línea));
          }

          // Lee las dos partes.
          string nombreOriginal = partes[0];
          string nombreFinal = partes[1];

          // Llena el diccionario.
          if (miDiccionarioDeNombres.ContainsKey(nombreOriginal))
          {
            throw new ArgumentException(string.Format("El archivo de entrada '{0}' tiene el siguiente nombre repetido: {1}", miArchivo, nombreOriginal));
          }

          miDiccionarioDeNombres.Add(nombreOriginal, nombreFinal);
        }
      }
    }
    #endregion
  }
}
