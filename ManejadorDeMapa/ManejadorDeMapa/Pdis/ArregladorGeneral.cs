#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
// (For English, see further down.)
//
// GpsYv.ManejadorDeMapa es una aplicaci�n para manejar Mapas de GPS en el
// formato Polish (.mp).  Esta escrito en C# usando el .NET Framework 3.5. 
//
// Esta programa naci� por la necesidad del Grupo GPS de Venezuela, 
// GPS_YV (http://www.gpsyv.net), de analizar y corregir los mapas que el
// grupo genera para la comunidad.  GpsYv.ManejadorDeMapa se distribuye bajo 
// la licencia GPL con la finalidad de que sea �til para otros grupos o
// individuos que hacen mapas, y tambi�n para promover la colaboraci�n 
// con este proyecto.
//
// Visita http://www.codeplex.com/GPSYVManejadorDeMapa para m�s informaci�n.
//
// La l�gica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Programador: Patricio Vidal (PatricioV2@hotmail.com)
//
// Este programa es software libre. Puede redistribuirlo y/o modificarlo
// bajo los t�rminos de la Licencia P�blica General de GNU seg�n es publicada
// por la Free Software Foundation, bien de la versi�n 2 de dicha Licencia o 
// bien (seg�n su elecci�n) de cualquier versi�n posterior. 
//
// Este programa se distribuye con la esperanza de que sea �til, 
// pero SIN NINGUNA GARANT�A, incluso sin la garant�a MERCANTIL
// impl�cita o sin garantizar la CONVENIENCIA PARA UN PROP�SITO PARTICULAR.
// V�ase la Licencia P�blica General de GNU para m�s detalles. 
//
// Deber�a haber recibido una copia de la Licencia P�blica General 
// junto con este programa. Si no ha sido as�, escriba a la 
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
  /// Arregla las cosas  generales de los PDIs que se pueden arreglar autom�ticamente.
  /// </summary>
  /// <remarks>
  /// Las cosas que son arregladas son:
  ///   - Letras inv�lidas en los nombres de PDIs.
  ///   - Palabras inv�lidas en los nombres de PDIs.
  /// </remarks>
  public class ArregladorGeneral : ProcesadorBase<ManejadorDePdis, Pdi>
  {
    #region Campos
    private readonly LectorDeConversi�nDeLetras miLectorDeConversi�nDeLetras;
    private readonly LectorDeCorrecci�nDePalabrasPorTipo miLectorDeCorrecci�nDePalabrasPorTipo;
    private readonly LectorDeCaracteresAEliminar miLectorDeCaracteresAEliminar;
    #endregion

    #region M�todos P�blicos
    /// <summary>
    /// Descripci�n de �ste procesador.
    /// </summary>
    public static readonly string Descripci�n =
      "- Arregla Letras reemplazando letras inv�lidas en los nombres de PDIs.\n" +
      "- Arregla Palabras reemplazando palabras inv�lidas en los nombres de PDIs.\n" +
      "- Elimina Caracteres inv�lidos en los nombres de Pdis.";
    

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
      miLectorDeConversi�nDeLetras = new LectorDeConversi�nDeLetras(elEscuchadorDeEstatus);
      miLectorDeCorrecci�nDePalabrasPorTipo = new LectorDeCorrecci�nDePalabrasPorTipo(elEscuchadorDeEstatus);
      miLectorDeCaracteresAEliminar = new LectorDeCaracteresAEliminar(elEscuchadorDeEstatus);
    }
    #endregion

    #region M�todos Protegidos.
    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPdi">El PDI.</param>
    /// <returns>El n�mero de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Pdi elPdi)
    {
      int n�meroDeProblemasDetectados = 0;

      n�meroDeProblemasDetectados += ArreglaLetras(elPdi);
      n�meroDeProblemasDetectados += ArreglaPalabrasPorTipo(elPdi);
      n�meroDeProblemasDetectados += EliminaCaracteres(elPdi);

      return n�meroDeProblemasDetectados;
    }


    private int ArreglaLetras(Pdi elPdi)
    {
      int n�meroDeProblemasDetectados = 0;

      #region Arregla el nombre del PDI.
      string nombreACorregir = elPdi.Nombre;

      // Cambia el nombre a may�sculas.
      string nombreCorregido = nombreACorregir.ToUpper();
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPdi.ActualizaNombre(nombreCorregido, "M002: Cambio a may�sculas.");
        ++n�meroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }

      // Arregla la letras no permitidas.
      IDictionary<char, char> diccionarioDeLetrasReemplazar = miLectorDeConversi�nDeLetras.DiccionarioDeLetrasAReemplazar;
      foreach (KeyValuePair<char, char> par in diccionarioDeLetrasReemplazar)
      {
        char letraOriginal = par.Key;
        char letraArreglada = par.Value;
        nombreCorregido = nombreCorregido.Replace(letraOriginal, letraArreglada);
      }
      #endregion

      // Si el nombre cambi� entonces actualizar el PDI y reportar el cambio.
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPdi.ActualizaNombre(nombreCorregido, "M003: Cambio de Letras.");
        ++n�meroDeProblemasDetectados;
      }

      return n�meroDeProblemasDetectados;
    }


    private int ArreglaPalabrasPorTipo(Pdi elPdi)
    {
      int n�meroDeProblemasDetectados = 0;

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
        ++n�meroDeProblemasDetectados;
        nombreACorregir = nombreCorregido;
      }

      // Corrige las palabras basado en el tipo.
      IList<Correcci�nDePalabras> listaDeCorrecci�nDePalabras = miLectorDeCorrecci�nDePalabrasPorTipo.ListaDeCorrecci�nDePalabras;
      foreach (Correcci�nDePalabras correcci�nDePalabras in listaDeCorrecci�nDePalabras)
      {
        // Si acertamos el tipo de PDI entonces procedemos a
        // buscar las palabras. 
        if (tipo == correcci�nDePalabras.Tipo)
        {
          // A�ade un espacio en blanco alrededor del nombre para asi hacer
          // b�squedas por palabras completas.
          nombreACorregir = " " + nombreACorregir + " ";
          foreach (string posiblePalabra in correcci�nDePalabras.PosiblesPalabras)
          {
            // A�ade un espacio en blanco al final del nombre para hacer
            // b�squedas por palabras completas.
            nombreCorregido = nombreACorregir.Replace(" " + posiblePalabra + " ", " " + correcci�nDePalabras.PalabraFinal + " ");

            if (nombreCorregido != nombreACorregir)
            {
              // Remueve los espacios en blanco que se pudo haber a�adido.
              elPdi.ActualizaNombre(nombreCorregido.Trim(), "M005: Cambio de palabra.");
              ++n�meroDeProblemasDetectados;
              nombreACorregir = nombreCorregido;
            }
          }
        }
      }
      #endregion

      return n�meroDeProblemasDetectados;
    }


    private int EliminaCaracteres(Pdi elPdi)
    {
      int n�meroDeItemsDetectados = 0;

      #region Elimina los Caracteres Inv�lidos.
      string nombreACorregir = elPdi.Nombre;
      string nombreCorregido = nombreACorregir;

      // Arregla la letras no permitidas.
      IList<char> caracteresInv�lidos = miLectorDeCaracteresAEliminar.ListaDeCaracteresInv�lidos;
      foreach (char caracterInv�lido in caracteresInv�lidos)
      {
        nombreCorregido = nombreACorregir.Replace(caracterInv�lido.ToString(), string.Empty);
      }
      #endregion

      // Si el nombre cambi� entonces actualizar el PDI y reportar el cambio.
      if (nombreCorregido != nombreACorregir)
      {
        // Actualiza el campo del nombre.
        elPdi.ActualizaNombre(nombreCorregido, "M007: Eliminaci�n de Caracteres Inv�lidos.");
        ++n�meroDeItemsDetectados;
      }

      return n�meroDeItemsDetectados;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que env�a el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aqu�.
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que env�a el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No necesitamos hacer nada aqu�.
    }
    #endregion

    #region Clases Privadas
    /// <summary>
    /// Representa un par posibles palabras y la palabra final.
    /// </summary>
    private class Correcci�nDePalabras
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
      public Correcci�nDePalabras(Tipo elTipo, string[] lasPosiblesPalabras, string laPalabraFinal)
      {
        Tipo = elTipo;
        PosiblesPalabras = lasPosiblesPalabras;
        PalabraFinal = laPalabraFinal;
      }
    }


    private class LectorDeCorrecci�nDePalabrasPorTipo : LectorDeArchivo
    {
      #region Campos
      private const string miArchivoDeConversionDePalabras = @"PDIs\Correcci�nDePalabrasPorTipo.csv";
      private readonly List<Correcci�nDePalabras> miListaDeCorrecci�nDePalabras = new List<Correcci�nDePalabras>();
      #endregion

      /// <summary>
      /// Obtiene la lista de correcci�n de palabras.
      /// </summary>
      public IList<Correcci�nDePalabras> ListaDeCorrecci�nDePalabras
      {
        get
        {
          return miListaDeCorrecci�nDePalabras;
        }
      }

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
      public LectorDeCorrecci�nDePalabrasPorTipo(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Lee(miArchivoDeConversionDePalabras);
      }


      protected override void ProcesaL�nea(string laL�nea)
      {
        // Elimina espacios en blanco.
        string l�nea = laL�nea.Trim();

        // Saltarse lineas en blanco y comentarios.
        bool laL�neaEstaEnBlanco = (l�nea == string.Empty);
        bool laL�neaEsComentario = l�nea.StartsWith("//");
        if (!laL�neaEstaEnBlanco & !laL�neaEsComentario)
        {
          // Separa las palabras.
          string[] partes = l�nea.Split(',');

          // Verifica que tenemos tres partes.
          if (partes.Length != 3)
          {
            throw new ArgumentException("No se encontraron 3 partes separadas por coma en la linea: " + l�nea);
          }

          // Lee las tres partes.
          string[] tipos = partes[0].Split('-');
          string[] posiblePalabras = partes[1].Split('|');
          string palabraFinal = partes[2];

          // Llena la lista.
          int primerTipo = Convert.ToInt32(tipos[0], 16);
          int �ltimoTipo = primerTipo;
          if (tipos.Length > 1)
          {
            �ltimoTipo = Convert.ToInt32(tipos[1], 16);
          }
          for (int tipo = primerTipo; tipo <= �ltimoTipo; ++tipo)
          {
            miListaDeCorrecci�nDePalabras.Add(new Correcci�nDePalabras(new Tipo(tipo), posiblePalabras, palabraFinal));
          }
        }
      }
    }


    private class LectorDeConversi�nDeLetras : LectorDeArchivo
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
      public LectorDeConversi�nDeLetras(IEscuchadorDeEstatus elEscuchadorDeEstatus)
        : base(elEscuchadorDeEstatus)
      {
        Lee(miArchivoDeLetrasAReemplazar);
      }

      #region M�todos Protegidos y Privados
      protected override void ProcesaL�nea(string laL�nea)
      {
        // Elimina espacios en blanco.
        string l�nea = laL�nea.Trim();

        // Saltarse lineas en blanco y comentarios.
        bool laL�neaEstaEnBlanco = (l�nea == string.Empty);
        bool laL�neaEsComentario = l�nea.StartsWith("//");
        if (!laL�neaEstaEnBlanco & !laL�neaEsComentario)
        {
          // Separa las letras.
          string[] partes = l�nea.Split(',');

          // Verifica que tenemos dos partes.
          if (partes.Length != 2)
          {
            throw new ArgumentException("No se encontraron 2 partes separadas por coma en la linea: " + l�nea);
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
      /// Obtiene la Lista de Caracteres Inv�lidos.
      /// </summary>
      public IList<char> ListaDeCaracteresInv�lidos
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

      #region M�todos Protegidos y Privados
      protected override void ProcesaL�nea(string laL�nea)
      {
        // Elimina espacios en blanco.
        string l�nea = laL�nea.Trim();

        // Saltarse lineas en blanco y comentarios.
        bool laL�neaEstaEnBlanco = (l�nea == string.Empty);
        bool laL�neaEsComentario = l�nea.StartsWith("//");
        if (!laL�neaEstaEnBlanco & !laL�neaEsComentario)
        {
          // Verifica que tenemos una sola letra.
          if (l�nea.Length != 1)
          {
            throw new ArgumentException("Se encontr� m�s de un caracter en la linea: " + l�nea);
          }

          // A�ade el aaracter a la lista.
          char caracter = l�nea[0];
          miListaDeCaracteres.Add(caracter);
        }
      }
      #endregion
    }
    #endregion
  }
}
