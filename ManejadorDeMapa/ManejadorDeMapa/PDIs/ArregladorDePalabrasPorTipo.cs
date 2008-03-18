#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
    /// <param name="elElemento">El PDI.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(PDI elPDI)
    {
      bool modificóElemento = false;

      #region Arregla el nombre del PDI.
      int tipo = elPDI.Tipo;
      string nombreACorregir = elPDI.Nombre;

      // Remueve los espacios en blanco alrededor.
      string nombreCorregido = nombreACorregir.Trim();

      // Remueve espacios en blanco extra entre medio de las palabras.
      string[] palabras = nombreCorregido.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      nombreCorregido = string.Join(" ", palabras);
      if (nombreCorregido != nombreACorregir)
      {
        elPDI.CambiaNombre(nombreCorregido, "Eliminados espacios en blanco extra");
        modificóElemento = true;
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
              modificóElemento = true;
              nombreACorregir = nombreCorregido;
            }
          }
        }
      }
      #endregion

      return modificóElemento;
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
      public int Tipo;

      /// <summary>
      /// Posibles palabras.
      /// </summary>
      public string[] PosiblesPalabras;

      /// <summary>
      /// Palabra final.
      /// </summary>
      public string PalabraFinal;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elTipo">El tipo.</param>
      /// <param name="lasPosiblesPalabras">Las posibles palabras.</param>
      /// <param name="laPalabraFinal">La palabra Final.</param>
      public CorrecciónDePalabras(int elTipo, string[] lasPosiblesPalabras, string laPalabraFinal)
      {
        Tipo = elTipo;
        PosiblesPalabras = lasPosiblesPalabras;
        PalabraFinal = laPalabraFinal;
      }
    }


    private class LectorDeCorrecciónDePalabrasPorTipo : LectorDeArchivo
    {
      #region Campos
      private static readonly string miArchivoDeConversionDePalabras = "CorrecciónDePalabrasPorTipo.csv";
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
        Abrir(miArchivoDeConversionDePalabras);
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
            miListaDeCorrecciónDePalabras.Add(new CorrecciónDePalabras(tipo, posiblePalabras, palabraFinal));
          }
        }
      }
    }
    #endregion
  }
}
