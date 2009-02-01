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

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Arregla las cosas  generales de los PDIs que se pueden arreglar automáticamente.
  /// </summary>
  /// <remarks>
  /// Las cosas que son arregladas son:
  ///   - Letras inválidas en los nombres de PDIs.
  ///   - Palabras inválidas en los nombres de PDIs.
  /// </remarks>
  public class ArregladorGeneral : ProcesadorBase<ManejadorDePdis, Pdi>
  {
    #region Campos
    private readonly LectorDeConversiónDeLetras miLectorDeConversiónDeLetras;
    private readonly LectorDeCorrecciónDePalabrasPorTipo miLectorDeCorrecciónDePalabrasPorTipo;
    private readonly LectorDeCaracteresAEliminar miLectorDeCaracteresAEliminar;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción =
      "- Arregla Letras reemplazando letras inválidas en los nombres de PDIs.\n" +
      "- Arregla Palabras reemplazando palabras inválidas en los nombres de PDIs.\n" +
      "- Elimina Caracteres inválidos en los nombres de Pdis.";
    

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePdis">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ArregladorGeneral(
      ManejadorDePdis elManejadorDePdis,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePdis, elEscuchadorDeEstatus)
    {
      miLectorDeConversiónDeLetras = new LectorDeConversiónDeLetras(elEscuchadorDeEstatus);
      miLectorDeCorrecciónDePalabrasPorTipo = new LectorDeCorrecciónDePalabrasPorTipo(elEscuchadorDeEstatus);
      miLectorDeCaracteresAEliminar = new LectorDeCaracteresAEliminar(elEscuchadorDeEstatus);
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPdi">El PDI.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Pdi elPdi)
    {
      int númeroDeProblemasDetectados = 0;

      númeroDeProblemasDetectados += ArreglaLetras(elPdi);
      númeroDeProblemasDetectados += ArreglaPalabrasPorTipo(elPdi);
      númeroDeProblemasDetectados += EliminaCaracteres(elPdi);

      return númeroDeProblemasDetectados;
    }


    private int ArreglaLetras(Pdi elPdi)
    {
      int númeroDeProblemasDetectados = 0;

      #region Arregla el nombre del PDI.
      string nombreACorregir = elPdi.Nombre;

      // Cambia el nombre a mayúsculas.
      string nombreCorregido = nombreACorregir.ToUpper();
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPdi.ActualizaNombre(nombreCorregido, "M002: Cambio a mayúsculas.");
        ++númeroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }

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
        elPdi.ActualizaNombre(nombreCorregido, "M003: Cambio de Letras.");
        ++númeroDeProblemasDetectados;
      }

      return númeroDeProblemasDetectados;
    }


    private int ArreglaPalabrasPorTipo(Pdi elPdi)
    {
      int númeroDeProblemasDetectados = 0;

      #region Arregla el nombre del PDI.
      Tipo tipo = elPdi.Tipo;
      string nombreACorregir = elPdi.Nombre;

      // Remueve los espacios en blanco alrededor.
      string nombreCorregido = nombreACorregir.Trim();

      // Remueve espacios en blanco extra entre medio de las palabras.
      string[] palabras = nombreCorregido.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      nombreCorregido = string.Join(" ", palabras);
      if (nombreCorregido != nombreACorregir)
      {
        elPdi.ActualizaNombre(nombreCorregido, "M004: Eliminados espacios en blanco extra.");
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
              elPdi.ActualizaNombre(nombreCorregido.Trim(), "M005: Cambio de palabra.");
              ++númeroDeProblemasDetectados;
              nombreACorregir = nombreCorregido;
            }
          }
        }
      }
      #endregion

      return númeroDeProblemasDetectados;
    }


    private int EliminaCaracteres(Pdi elPdi)
    {
      int númeroDeItemsDetectados = 0;

      #region Elimina los Caracteres Inválidos.
      string nombreACorregir = elPdi.Nombre;
      string nombreCorregido = nombreACorregir;

      // Arregla la letras no permitidas.
      IList<char> caracteresInválidos = miLectorDeCaracteresAEliminar.ListaDeCaracteresInválidos;
      foreach (char caracterInválido in caracteresInválidos)
      {
        nombreCorregido = nombreACorregir.Replace(caracterInválido.ToString(), string.Empty);
      }
      #endregion

      // Si el nombre cambió entonces actualizar el PDI y reportar el cambio.
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPdi.ActualizaNombre(nombreCorregido, "M007: Eliminación de Caracteres Inválidos.");
        ++númeroDeItemsDetectados;
      }

      return númeroDeItemsDetectados;
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
    #endregion

    #region Clases Privadas
    /// <summary>
    /// Representa un par posibles palabras y la palabra final.
    /// </summary>
    private class CorrecciónDePalabras
    {
      /// <summary>
      /// Tipo.
      /// </summary>
      public Tipo Tipo { get; private set; }

      /// <summary>
      /// Posibles palabras.
      /// </summary>
      public string[] PosiblesPalabras { get; private set; }

      /// <summary>
      /// Palabra final.
      /// </summary>
      public string PalabraFinal { get; private set; }

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


    private class LectorDeCorrecciónDePalabrasPorTipo : LectorDeArchivo
    {
      #region Campos
      private const string miArchivoDeConversionDePalabras = @"PDIs\CorrecciónDePalabrasPorTipo.csv";
      private readonly List<CorrecciónDePalabras> miListaDeCorrecciónDePalabras = new List<CorrecciónDePalabras>();
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


    private class LectorDeConversiónDeLetras : LectorDeArchivo
    {
      #region Campos
      private const string miArchivoDeLetrasAReemplazar = @"PDIs\LetrasAReemplazar.csv";
      private readonly Dictionary<char, char> miDiccionarioDeLetras = new Dictionary<char, char>();
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


    private class LectorDeCaracteresAEliminar : LectorDeArchivo
    {
      #region Campos
      private const string miArchivoDeCaracteresAEliminar = @"PDIs\CaracteresAEliminar.csv";
      private readonly List<char> miListaDeCaracteres = new List<char>();
      #endregion

      /// <summary>
      /// Obtiene la Lista de Caracteres Inválidos.
      /// </summary>
      public IList<char> ListaDeCaracteresInválidos
      {
        get
        {
          return miListaDeCaracteres;
        }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
      public LectorDeCaracteresAEliminar(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Lee(miArchivoDeCaracteresAEliminar);
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
          // Verifica que tenemos una sola letra.
          if (línea.Length != 1)
          {
            throw new ArgumentException("Se encontró más de un caracter en la linea: " + línea);
          }

          // Añade el aaracter a la lista.
          char caracter = línea[0];
          miListaDeCaracteres.Add(caracter);
        }
      }
      #endregion
    }
    #endregion
  }
}
