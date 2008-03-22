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

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Arreglador de letras en PDIs.
  /// </summary>
  public class ArregladorDeLetras : ProcesadorBase<ManejadorDePDIs, PDI>
  {
    #region Campos
    private readonly LectorDeConversiónDeLetras miLectorDeConversiónDeLetras;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción =
      "Arregla Letras reemplazando letras inválidas en los nombres de PDIs.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePDIs">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorDeLetras(
      ManejadorDePDIs elManejadorDePDIs,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePDIs, elEscuchadorDeEstatus)
    {
      miLectorDeConversiónDeLetras = new LectorDeConversiónDeLetras(elEscuchadorDeEstatus);
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elElemento">El PDI.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(PDI elPDI)
    {
      bool modificóElemento = false;

      #region Arregla el nombre del PDI.
      string nombreACorregir = elPDI.Nombre;

      // Cambia el nombre a mayúsculas.
      string nombreCorregido = nombreACorregir.ToUpper();

      // Arregla la letras no permitidas.
      IDictionary<char, char> diccionarioDeLetrasReemplazar = miLectorDeConversiónDeLetras.DiccionarioDeLetrasAReemplazar;
      foreach (KeyValuePair<char, char> par in diccionarioDeLetrasReemplazar)
      {
        char letraOriginal = par.Key;
        char letraArreglada = par.Value;
        nombreCorregido = nombreCorregido.Replace(letraOriginal, letraArreglada);
      }
      #endregion

      // Si el nombre cambió entonces actualizar el PDI y reportar el cambio.
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPDI.CambiaNombre(nombreCorregido, "Cambio de Letras");
        modificóElemento = true;
      }

      return modificóElemento;
    }
    #endregion

    #region Clases Privadas
    private class LectorDeConversiónDeLetras : LectorDeArchivo
    {
      #region Campos
      private static readonly string miArchivoDeLetrasAReemplazar = @"PDIs\LetrasAReemplazar.csv";
      private Dictionary<char, char> miDiccionarioDeLetras = new Dictionary<char, char>();
      #endregion

      /// <summary>
      /// Obtiene el diccionario de letras a reemplazar.
      /// </summary>
      public IDictionary<char, char> DiccionarioDeLetrasAReemplazar
      {
        get
        {
          return miDiccionarioDeLetras;
        }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
      public LectorDeConversiónDeLetras(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Lee(miArchivoDeLetrasAReemplazar);
      }

      #region Métodos Protegidos y Privados
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
          if (partes.Length != 2)
          {
            throw new ArgumentException("No se encontraron 2 partes separadas por coma en la linea: " + línea);
          }

          // Lee las dos partes como la letra y la conversion.
          char letra = Convert.ToChar(partes[0]);
          char conversion = Convert.ToChar(partes[1]);
          miDiccionarioDeLetras[letra] = conversion;
        }
      }
      #endregion
    }
    #endregion
  }
}

