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
using System.Text;
using System.IO;
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
    /// Esta codificación permite interpretar correctamente los acentos de 
    /// archivos ANSI-8 bytes.
    /// </summary>
    private readonly Encoding miCodificaciónPorDefecto = Encoding.GetEncoding(1252);
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elArchivo">El archivo a abrir.</param>
    /// <param name="losElementosDelMapa">Los elementos del mapa.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public EscritorDeFormatoPolish(string elArchivo, ICollection<ElementoDelMapa> losElementosDelMapa, IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      // Usar el punto para separar decimales.
      miFormatoNumérico.NumberDecimalSeparator = ".";

      try
      {
        // Hacer una copia si el archivo existe.
        if (File.Exists(elArchivo))
        {
          const bool sobreEscribe = true;
          File.Copy(elArchivo, elArchivo + ".bak", sobreEscribe);
        }

        // Reporta estatus.
        elEscuchadorDeEstatus.Estatus = "Escribiendo " + elArchivo + " ...";
        elEscuchadorDeEstatus.Progreso = 0;

        // Establece el límite superior de la barra de progreso.
        int númeroDeElementos = losElementosDelMapa.Count;
        elEscuchadorDeEstatus.ProgresoMáximo = númeroDeElementos;

        using (StreamWriter escritor = new StreamWriter(elArchivo, false, miCodificaciónPorDefecto))
        {
          // Guarda todos los elementos.
          int contadorDeElementos = 0;
          foreach (ElementoDelMapa elemento in losElementosDelMapa)
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
    private static void Guarda(Comentario elComentario, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(";" + elComentario.Texto);
    }


    private void Guarda(ElementoDelMapa elElemento, StreamWriter elEscritor)
    {
      // Inicializa indices. 
      var indiceDeNodoRuteable = 1;

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

      // Guarda los campos del elemento.
      foreach (var campo in elElemento.Campos)
      {
        CampoComentario campoComentario;
        CampoNombre campoNombre;
        CampoCoordenadas campoCoordenadas;
        CampoNodoRuteable campoNodo;
        CampoGenérico campoGenérico;
        CampoTipo campoTipo;
        CampoParámetrosDeRuta campoParámetrosDeRuta;
        CampoAtributo campoAtributo;
        CampoEsCiudad campoCiudad;
        CampoIndiceDeCiudad campoIndiceDeCiudad;
        if ((campoComentario = campo as CampoComentario) != null)
        {
          Guarda(campoComentario, elEscritor);
        }
        else if ((campoNombre = campo as CampoNombre) != null)
        {
          Guarda(campoNombre, elEscritor);
        }
        else if ((campoCoordenadas = campo as CampoCoordenadas) != null)
        {
          Guarda(campoCoordenadas, elEscritor);
        }
        else if ((campoNodo = campo as CampoNodoRuteable) != null)
        {
          Guarda(campoNodo, indiceDeNodoRuteable, elEscritor);

          // Incrementa el índice para el próximo nodo.
          ++indiceDeNodoRuteable;
        }
        else if ((campoGenérico = campo as CampoGenérico) != null)
        {
          Guarda(campoGenérico, elEscritor);
        }
        else if ((campoTipo = campo as CampoTipo) != null)
        {
          Guarda(campoTipo, elEscritor);
        }
        else if ((campoParámetrosDeRuta = campo as CampoParámetrosDeRuta) != null)
        {
          Guarda(campoParámetrosDeRuta, elEscritor);
        }
        else if ((campoAtributo = campo as CampoAtributo) != null)
        {
          Guarda(campoAtributo, elEscritor);
        }
        else if ((campoCiudad = campo as CampoEsCiudad) != null)
        {
          Guarda(campoCiudad, elEscritor);
        }
        else if ((campoIndiceDeCiudad = campo as CampoIndiceDeCiudad) != null)
        {
          Guarda(campoIndiceDeCiudad, elEscritor);
        }
        else
        {
          throw new ArgumentException("Campo desconocido: " + campo.GetType());
        }
      }

      // Escribe el final del elemento.
      elEscritor.WriteLine(finalDeElemento);
    }


    private static void Guarda(CampoIndiceDeCiudad elCampoIndiceDeCiudad, StreamWriter elEscritor)
    {
      string texto = elCampoIndiceDeCiudad.Indice.ToString(CultureInfo.InvariantCulture);
      Guarda(elCampoIndiceDeCiudad, texto, elEscritor);
    }


    private static void Guarda(CampoNodoRuteable elCampoNodo, int elNúmero, StreamWriter elEscritor)
    {
      // Crea el texto.
      string texto = string.Format("{0},{1}", elCampoNodo.IndiceDeCoordenadas, elCampoNodo.IdentificadorGlobal);
      if (elCampoNodo.EsExterno)
      {
        texto += ",1";
      }
      else
      {
        texto += ",0";
      }

      Guarda(elCampoNodo, elNúmero, texto, elEscritor);
    }


    private static void Guarda(CampoTipo elCampoTipo, StreamWriter elEscritor)
    {
      string tipo = elCampoTipo.Tipo.ToString();
      Guarda(elCampoTipo, tipo, elEscritor);
    }


    private static void Guarda(CampoGenérico elCampoGenérico, StreamWriter elEscritor)
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

      Guarda(
        elCampoCoordenadas,
        elCampoCoordenadas.Nivel,
        texto.ToString(),
        elEscritor);
    }


    private static void Guarda(CampoNombre elCampoNombre, StreamWriter elEscritor)
    {
      if (elCampoNombre.Número != null)
      {
        Guarda(elCampoNombre, (int)elCampoNombre.Número, elCampoNombre.Nombre, elEscritor);
      }
      else
      {
        Guarda(elCampoNombre, elCampoNombre.Nombre, elEscritor);
      }
    }


    private static void Guarda(CampoEsCiudad elCampoCiudad, StreamWriter elEscritor)
    {
      string texto;
      if (elCampoCiudad.EsCiudad)
      {
        texto = "Y";
      }
      else
      {
        texto = "N";
      }

      Guarda(elCampoCiudad, texto, elEscritor);
    }


    private static void Guarda(Campo elCampo, string elTexto, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(elCampo.Identificador + "=" + elTexto);
    }


    private static void Guarda(Campo elCampo, IConvertible elNúmero, string elTexto, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(
        elCampo.Identificador + elNúmero.ToString(CultureInfo.InvariantCulture)
        + "=" + elTexto);
    }


    private static void Guarda(CampoComentario elCampoComentario, StreamWriter elEscritor)
    {
      elEscritor.WriteLine(";" + elCampoComentario.Comentario);
    }


    private static void Guarda(CampoParámetrosDeRuta elCampoParámetrosDeRuta, StreamWriter elEscritor)
    {
      // Crea el texto.
      const char separador = ',';
      StringBuilder texto = new StringBuilder();
      texto.Append(elCampoParámetrosDeRuta.LímiteDeVelocidad.Indice);
      texto.Append(separador);
      texto.Append(elCampoParámetrosDeRuta.ClaseDeRuta.Indice);
      foreach (bool valor in elCampoParámetrosDeRuta.OtrosParámetros)
      {
        texto.Append(separador);
        if (valor)
        {
          texto.Append(1);
        }
        else
        {
          texto.Append(0);
        }
      }

      Guarda(elCampoParámetrosDeRuta, elCampoParámetrosDeRuta.ToString(), elEscritor);
    }


    private static void Guarda(CampoAtributo elCampoAtributo, StreamWriter elEscritor)
    {
      Guarda(elCampoAtributo, elCampoAtributo.Atributo, elEscritor);
    }
    #endregion
  }
}
