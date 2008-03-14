#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Escritor del formato Polish (.mp GPSMapEdit).
  /// </summary>
  public class EscritorDeFormatoPolish
  {
    #region Campos
    private const string FormatoDeCoordenada = "0.00000";
    private readonly NumberFormatInfo miFormatoNumérico = new NumberFormatInfo();

    /// <summary>
    /// Esta codificación permite intepretar correctamente los acentos de 
    /// archivos ANSI-8 bytes.
    /// </summary>
    private readonly Encoding miCodificaciónPorDefecto = Encoding.GetEncoding(1252);
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elArchivo">El archivo a abrir.</param>
    /// <param name="losElementodDelMapa">Los elementos del mapa.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public EscritorDeFormatoPolish(string elArchivo, IList<ElementoDelMapa> losElementodDelMapa, IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      // Usar el punto para separar decimales.
      miFormatoNumérico.NumberDecimalSeparator = ".";

      // Reporta estatus.
      elEscuchadorDeEstatus.Estatus = "Guardando ...";
      elEscuchadorDeEstatus.Progreso = 0;

      // Establece el límite superior de la barra de progreso.
      int númeroDeElementos = losElementodDelMapa.Count;
      elEscuchadorDeEstatus.ProgresoMáximo = númeroDeElementos;

      try
      {
        using (StreamWriter escritor = new StreamWriter(elArchivo, false, miCodificaciónPorDefecto))
        {
          // Guarda todos los elementos.
          int contadorDeElementos = 0;
          foreach (ElementoDelMapa elemento in losElementodDelMapa)
          {
            ++contadorDeElementos;

            // Reportar Progreso
            elEscuchadorDeEstatus.Progreso = contadorDeElementos;

            if (elemento is Comentario)
            {
              Guarda((Comentario)elemento, escritor);
            }
            else
            {
              Guarda(elemento, escritor);

              // Separa los elementos con una línea en blanco.
              escritor.WriteLine();
            }
          }
        }

        elEscuchadorDeEstatus.Estatus = "Listo.";
      }
      catch
      {
        elEscuchadorDeEstatus.Estatus = "Error.";
        throw;
      }
      finally
      {
        // Borra la barra de progreso.
        elEscuchadorDeEstatus.Progreso = 0;
      }
    }
    #endregion

    #region Métodos Protegidos y Privados
    private void Guarda(Comentario elComentario, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(";" + elComentario.Texto);
    }


    private void Guarda(ElementoDelMapa elElemento, StreamWriter elEscritor)
    {
      // Salirse si el elemeto está eliminado.
      if (elElemento.FuéEliminado)
      {
        return;
      }

      // Escribe la clase.
      string clase = elElemento.Clase;
      elEscritor.WriteLine("[" + clase + "]");

      // Final del elemento.
      string finalDeElemento = "[END]";
      switch (clase)
      {
        case "IMG ID":
        case "Countries":
        case "Regions":
        case "Cities":
        case "ZipCodes":
        case "Restrict":
          finalDeElemento = "[END-" + clase + "]";
          break;
      }


      foreach (Campo campo in elElemento.Campos)
      {
        if (campo is CampoComentario)
        {
          Guarda((CampoComentario)campo, elEscritor);
        }
        else if (campo is CampoNombre)
        {
          Guarda((CampoNombre)campo, elEscritor);
        }
        else if (campo is CampoCoordenadas)
        {
          Guarda((CampoCoordenadas)campo, elEscritor);
        }
        else if (campo is CampoGenérico)
        {
          Guarda((CampoGenérico)campo, elEscritor);
        }
        else if (campo is CampoTipo)
        {
          Guarda((CampoTipo)campo, elEscritor);
        }
        else
        {
          throw new ArgumentException("Campo desconocido: " + campo.GetType());
        }
      }

      // Escribe el final del elemento.
      elEscritor.WriteLine(finalDeElemento);
    }


    private void Guarda(CampoTipo elCampoTipo, StreamWriter elEscritor)
    {
      string tipo = "0x" + elCampoTipo.Tipo.ToString("x");
      Guarda(elCampoTipo, tipo, elEscritor);
    }


    private void Guarda(CampoGenérico elCampoGenérico, StreamWriter elEscritor)
    {
      Guarda(elCampoGenérico, elCampoGenérico.Texto, elEscritor);
    }


    private void Guarda(CampoCoordenadas elCampoCoordenadas, StreamWriter elEscritor)
    {
      StringBuilder texto = new StringBuilder();
      bool esPrimeraCoordenada = true;
      foreach (Coordenadas coordenadas in elCampoCoordenadas.Coordenadas)
      {
        if (esPrimeraCoordenada)
        {
          esPrimeraCoordenada = false;
        }
        else
        {
          texto.Append(',');
        }

        texto.Append("("
        + coordenadas.Latitud.ToString(FormatoDeCoordenada, miFormatoNumérico)
        + ","
        + coordenadas.Longitud.ToString(FormatoDeCoordenada, miFormatoNumérico)
        + ")");
      }

      Guarda(elCampoCoordenadas, texto.ToString(), elEscritor);
    }


    private void Guarda(CampoNombre elCampoNombre, StreamWriter elEscritor)
    {
      Guarda(elCampoNombre, elCampoNombre.Nombre, elEscritor);
    }


    private void Guarda(Campo elCampo, string elTexto, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(elCampo.Identificador + "=" + elTexto);
    }


    private void Guarda(CampoComentario elCampoComentario, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(";" + elCampoComentario.Texto);
    }
    #endregion
  }
}
