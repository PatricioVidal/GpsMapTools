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
using System.Globalization;
using System.Drawing;
using GpsYv.ManejadorDeMapa.PDIs;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Lector de formato Polish (.mp, GPSMapEdit).
  /// </summary>
  public class LectorDeFormatoPolish : LectorDeArchivo
  {
    #region Campos
    private readonly ManejadorDeMapa miManejadorDeMapa;
    private readonly List<ElementoDelMapa> misElementosDelMapa = new List<ElementoDelMapa>();
    private readonly NumberFormatInfo miFormatoNumérico = new NumberFormatInfo();
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene los elementos del mapa leídos.
    /// </summary>
    public IList<ElementoDelMapa> ElementosDelMapa
    {
      get
      {
        return misElementosDelMapa;
      }
    }

    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elArchivo">El archivo a abrir.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public LectorDeFormatoPolish(ManejadorDeMapa elManejadorDeMapa, string elArchivo, IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elEscuchadorDeEstatus)
    {
      miManejadorDeMapa = elManejadorDeMapa;

      // Usar el punto para separar decimales.
      miFormatoNumérico.NumberDecimalSeparator = ".";

      // Abre el archivo.
      Lee(elArchivo);

      // Reporta el número de elementos leídos.
      int númeroDeElementos = misElementosDelMapa.Count;
      Estatus = "Leídos " + númeroDeElementos + " elementos";
    }
    #endregion

    #region Métodos Privados
    protected override void ProcesaLínea(string laLínea)
    {
      // Elimina espacios en blanco.
      string línea = laLínea;

      if (línea != string.Empty)
      {
        if (línea.StartsWith(";"))
        {
          LeeComentario(línea);
        }
        else
        {
          // La clase corresponde al encabezado sin los corchetes.
          string encabezado = línea.Trim();
          if (!encabezado.StartsWith("[") & !encabezado.EndsWith("]"))
          {
            throw new ArgumentException("Elemento del Mapa desconocido: " + línea);
          }
          string clase = encabezado.Substring(1, encabezado.Length - 2);

          switch (clase)
          {
            case "POI":
            case "RGN10":
            case "RGN20":
              LeePDI(clase);
              break;
            case "POLYGON":
            case "RGN80":
              LeePolígono(clase);
              break;
            case "POLYLINE":
            case "RGN40":
              LeePolilínea(clase);
              break;
            default:
              LeeElementoDesconocido(clase);
              break;
          }
        }
      }

      // Reportar estatus cada 100 elementos.
      int númeroDeElementos = misElementosDelMapa.Count;
      if ((númeroDeElementos % 100) == 0)
      {
        Estatus = "Leyendo Elemento #" + númeroDeElementos;
      }
    }


    private int ObtieneElNúmeroDelPróximoElemento()
    {
      int númeroDelPróximoElemento = misElementosDelMapa.Count + 1;

      return númeroDelPróximoElemento;
    }


    private void LeeComentario(string laLínea)
    {
      // Añade el comentario.
      misElementosDelMapa.Add(new Comentario(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laLínea));
    }

    
    private void LeePDI(string laClase)
    {
      IList<Campo> campos = LeeCampos();

      // Añade el PDI.
      misElementosDelMapa.Add(new PDI(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laClase, campos));
    }


    private void LeePolígono(string laClase)
    {
      IList<Campo> campos = LeeCampos();

      // Añade el polígono.
      misElementosDelMapa.Add(new Polígono(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laClase, campos));
    }


    private void LeePolilínea(string laClase)
    {
      IList<Campo> campos = LeeCampos();


      // Busca el tipo de polilínea.
      Tipo tipo = Tipo.TipoNulo;
      foreach (Campo campo in campos)
      {
        if (campo is CampoTipo)
        {
          tipo = ((CampoTipo)campo).Tipo;
        }
      }

      // Si el tipo es de Vía entonces crea una Vía.
      // Si no, entonces crea una Polilínea.
      if ((tipo != Tipo.TipoNulo) && (TiposDeVías.Tipos.Contains(tipo)))
      {
        // Añade la Vía.
        misElementosDelMapa.Add(new Vía(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laClase, campos));
      }
      else
      {
        // Añade la polilínea.
        misElementosDelMapa.Add(new Polilínea(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laClase, campos));
      }
    }


    private void LeeElementoDesconocido(string laClase)
    {
      IList<Campo> campos = LeeCampos();

      // Añade el elemento.
      misElementosDelMapa.Add(new ElementoDesconocido(
        miManejadorDeMapa, 
        ObtieneElNúmeroDelPróximoElemento(), 
        laClase,
        campos));
    }


    private IList<Campo> LeeCampos()
    {
      List<Campo> campos = new List<Campo>();

      // Lee linea por linea hasta que se consiga el final del elemento.
      string línea = LeeLaPróximaLínea();
      while (!línea.StartsWith("[END"))
      {
        // La línea debería ser un comentario or un campo.
        if (línea.StartsWith(";"))
        {
          // Es un comentario.
          campos.Add(new CampoComentario(línea));
        }
        else
        {
          // Debe ser un campo. Separa la linea en secciones usando '=' como separador.
          int separador = línea.IndexOf('=');
          if (separador > 1)
          {
            // Obtiene el identificador con nivel (Data0, Data1, etc)
            // y texto del campo.
            string identificadorConNivel = línea.Substring(0, separador);
            string texto = línea.Substring(separador + 1);
            
            // Separa el identificador del nivel.
            int indiceDelNúmero = identificadorConNivel.IndexOfAny("0123456789".ToCharArray());
            int? nivel = null;
            string identificador = identificadorConNivel;
            if (indiceDelNúmero >= 0)
            {
              int número;
              bool convirtió = int.TryParse(identificadorConNivel.Substring(indiceDelNúmero), out número);
              if (convirtió)
              {
                nivel = número;
                identificador = identificadorConNivel.Substring(0, indiceDelNúmero);
              }
            }
            
            // Construye el campo basado en el identificador.
            switch (identificador)
            {
              case CampoNombre.IdentificadorDeEtiqueta:
                campos.Add(new CampoNombre(texto));
                break;
              case CampoTipo.IdentificadorDeTipo:
                campos.Add(new CampoTipo(texto));
                break;
              case CampoCoordenadas.IdentificadorDeCoordenadas:
              case CampoCoordenadas.IdentificadorDeCoordenadasAlterno:
                CampoCoordenadas coordenadas = ExtraeCoordenadas(identificadorConNivel, nivel.Value, texto);
                campos.Add(coordenadas);
                break;
              default:
                campos.Add(new CampoGenérico(identificadorConNivel, texto));
                break;
            }
          }
          else
          {
            throw new ArgumentException("Error buscando '=' en: " + línea);
          }
        }

        // Lee la próxima linea.
        línea = LeeLaPróximaLínea();

        // Si llegamos al final del archivo entonces hay un error.
        if (línea == null)
        {
          throw new ArgumentException("Se encontró el final del archivo estando dentro de un elemento.");
        }
      }

      return campos;
    }


    private CampoCoordenadas ExtraeCoordenadas(string elIdentificador, int elNivel, string elTexto)
    {
      // Extrae los pares de coordenadas.
      string[] paresDeCoordenadas = elTexto.Split(new string[] {"),("}, StringSplitOptions.RemoveEmptyEntries);

      // Procesa cada par de coordenadas.
      int númeroDeParesDeCoordenadas = paresDeCoordenadas.Length;
      List<Coordenadas> coordenadas = new List<Coordenadas>();
      foreach (string parDeCoordenadas in paresDeCoordenadas)
      {
        // Elimina los parentesis.
        string textoConCoordenadas = parDeCoordenadas.Replace('(', ' ');
        textoConCoordenadas = textoConCoordenadas.Replace(')', ' ');

        #region Separa latitud y longitud.
        string[] partes = textoConCoordenadas.Split(',');

        // Verifica que tenemos dos partes.
        if (partes.Length != 2)
        {
          throw new ArgumentException("No se encontraron 2 partes separadas por coma en: " + elTexto);
        }

        // Lee las dos partes como latitud y longitud.
        float latitud = (float)Convert.ToDouble(partes[0].Trim(), miFormatoNumérico);
        float longitud = (float)Convert.ToDouble(partes[1].Trim(), miFormatoNumérico);
        #endregion

        // Añade las coordenadas a la lista.
        coordenadas.Add(new Coordenadas(latitud, longitud));
      }

      return new CampoCoordenadas(elIdentificador, elNivel, coordenadas.ToArray());
    }
    #endregion
  }
}
