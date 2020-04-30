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

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Arreglador de letras en PDIs.
  /// </summary>
  public class ArregladorDePalabrasPorTipo : ProcesadorBase<ManejadorDePDIs, PDI>
  {
    #region Campos
    private readonly LectorDeCorrecciónDePalabrasPorTipo miLectorDeCorrecciónDePalabrasPorTipo;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción =
      "Arregla Palabras reemplazando palabras inválidas en los nombres de PDIs.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePDIs">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorDePalabrasPorTipo(
      ManejadorDePDIs elManejadorDePDIs, 
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePDIs, elEscuchadorDeEstatus)
    {
      miLectorDeCorrecciónDePalabrasPorTipo = new LectorDeCorrecciónDePalabrasPorTipo(elEscuchadorDeEstatus);
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPDI">El PDI.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(PDI elPDI)
    {
      int númeroDeProblemasDetectados = 0;

      #region Arregla el nombre del PDI.
      Tipo tipo = elPDI.Tipo;
      string nombreACorregir = elPDI.Nombre;

      // Remueve los espacios en blanco alrededor.
      string nombreCorregido = nombreACorregir.Trim();

      // Remueve espacios en blanco extra entre medio de las palabras.
      string[] palabras = nombreCorregido.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      nombreCorregido = string.Join(" ", palabras);
      if (nombreCorregido != nombreACorregir)
      {
        elPDI.CambiaNombre(nombreCorregido, "Eliminados espacios en blanco extra");
        ++númeroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }

      // Corrige las palabras basado en el tipo.
      IList<CorrecciónDePalabras> listaDeCorrecciónDePalabras = miLectorDeCorrecciónDePalabrasPorTipo.ListaDeCorrecciónDePalabras;
      foreach (CorrecciónDePalabras correcciónDePalabras in listaDeCorrecciónDePalabras)
      {
        // Si acertamos el tipo de PDI entonces procedemos a
        // buscar las palabras. 
        if (tipo == correcciónDePalabras.Tipo)
        {
          // Añade un espacio en blanco alrededor del nombre para asi hacer
          // búsquedas por palabras completas.
          nombreACorregir = " " + nombreACorregir + " ";
          foreach (string posiblePalabra in correcciónDePalabras.PosiblesPalabras)
          {
            // Añade un espacio en blanco al final del nombre para hacer
            // búsquedas por palabras completas.
            nombreCorregido = nombreACorregir.Replace(" " + posiblePalabra + " ", " " + correcciónDePalabras.PalabraFinal + " ");

            if (nombreCorregido != nombreACorregir)
            {
              // Remueve los espacios en blanco que se pudo haber añadido.
              elPDI.CambiaNombre(nombreCorregido.Trim(), "Cambio de palabra");
              ++númeroDeProblemasDetectados;
              nombreACorregir = nombreCorregido;
            }
          }
        }
      }
      #endregion

      return númeroDeProblemasDetectados;
    }
    #endregion

    #region Clases Privadas
    /// <summary>
    /// Representa un par posibles palabras y la palabra final.
    /// </summary>
    private struct CorrecciónDePalabras
    {
      /// <summary>
      /// Tipo.
      /// </summary>
      public readonly Tipo Tipo;

      /// <summary>
      /// Posibles palabras.
      /// </summary>
      public readonly string[] PosiblesPalabras;

      /// <summary>
      /// Palabra final.
      /// </summary>
      public readonly string PalabraFinal;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elTipo">El tipo.</param>
      /// <param name="lasPosiblesPalabras">Las posibles palabras.</param>
      /// <param name="laPalabraFinal">La palabra Final.</param>
      public CorrecciónDePalabras(Tipo elTipo, string[] lasPosiblesPalabras, string laPalabraFinal)
      {
        Tipo = elTipo;
        PosiblesPalabras = lasPosiblesPalabras;
        PalabraFinal = laPalabraFinal;
      }
    }



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


    private class LectorDeCorrecciónDePalabrasPorTipo : LectorDeArchivo
    {
      #region Campos
      private static readonly string miArchivoDeConversionDePalabras = @"PDIs\CorrecciónDePalabrasPorTipo.csv";
      private List<CorrecciónDePalabras> miListaDeCorrecciónDePalabras = new List<CorrecciónDePalabras>();
      #endregion

      /// <summary>
      /// Obtiene la lista de corrección de palabras.
      /// </summary>
      public IList<CorrecciónDePalabras> ListaDeCorrecciónDePalabras
      {
        get
        {
          return miListaDeCorrecciónDePalabras;
        }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
      public LectorDeCorrecciónDePalabrasPorTipo(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Lee(miArchivoDeConversionDePalabras);
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
          string[] partes = línea.Split(',');

          // Verifica que tenemos tres partes.
          if (partes.Length != 3)
          {
            throw new ArgumentException("No se encontraron 3 partes separadas por coma en la linea: " + línea);
          }

          // Lee las tres partes.
          string[] tipos = partes[0].Split('-');
          string[] posiblePalabras = partes[1].Split('|');
          string palabraFinal = partes[2];

          // Llena la lista.
          int primerTipo = Convert.ToInt32(tipos[0], 16);
          int últimoTipo = primerTipo;
          if (tipos.Length > 1)
          {
            últimoTipo = Convert.ToInt32(tipos[1], 16);
          }
          for (int tipo = primerTipo; tipo <= últimoTipo; ++tipo)
          {
            miListaDeCorrecciónDePalabras.Add(new CorrecciónDePalabras(new Tipo(tipo), posiblePalabras, palabraFinal));
          }
        }
      }
    }
    #endregion
  }
}
