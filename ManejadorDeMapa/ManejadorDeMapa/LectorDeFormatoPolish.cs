#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Globalization;
using System.Drawing;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Lector de formato Polish (.mp, GPSMapEdit).
  /// </summary>
  public class LectorDeFormatoPolish : LectorDeArchivo
  {
    #region Campos
    private readonly ManejadorDeMapa miManejadorDeMapa;
    private IList<ElementoDelMapa> misElementosDelMapa;
    private readonly NumberFormatInfo miFormatoNumérico = new NumberFormatInfo();
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
      misElementosDelMapa = elManejadorDeMapa.Elementos;
      misElementosDelMapa.Clear();

      // Usar el punto para separar decimales.
      miFormatoNumérico.NumberDecimalSeparator = ".";

      // Abre el archivo.
      Abrir(elArchivo);

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

      // Añade la polilínea.
      misElementosDelMapa.Add(new Polilínea(miManejadorDeMapa, ObtieneElNúmeroDelPróximoElemento(), laClase, campos));
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
