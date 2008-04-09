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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using GpsYv.ManejadorDeMapa.PDIs;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Interfase de Mapa gráfico.
  /// </summary>
  public partial class InterfaseMapa : InterfaseBase
  {
    #region Campos
    private bool miHayUnMapa = false;

    // Objetos para dibujar los elementos.
    private readonly Brush miPincelDeFondoParaTiempo = Brushes.White;
    private readonly Brush miPincelParaTiempo = Brushes.Gray;
    private readonly Font miLetraParaTiempo = new Font("Arial", 8);
    private readonly Brush miPincelParaPDI = Brushes.Black;
    private readonly Brush miPincelParaVíaConUnaCoordenada = Brushes.Red;
    private readonly Brush miPincelParaPDIModificado = Brushes.Yellow;
    private readonly Brush miPincelParaPDIEliminado = Brushes.Red;
    private Pen miLápizParaBorde = Pens.LightGray;
    private readonly Font miLetraParaNombre = new Font("Arial", 8);
    private readonly Brush miPincelDeFondoParaNombre = Brushes.White;
    private readonly Brush miPincelParaNombre = Brushes.Black;

    // Parametros para dibujar los elementos.
    private Graphics miGráficador;
    private double miEscalaDeCoordenadasAPixeles;
    private PointF miOrigenEnCoordenadas;
    private Rectangle miAreaDeDibujo;

    // Manejo del area visible.
    private RectangleF miRectánguloDeDataEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private RectangleF miRectánguloVisibleEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private RectangleF miRectánguloVisibleActivoEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private bool miMuestraTodoElMapa = true;

    private List<PuntoAdicional> misPuntosAdicionales = new List<PuntoAdicional>();
    private List<PolilíneaAdicional> misPolilíneasAdicionales = new List<PolilíneaAdicional>();
    private bool miRatónEstaSobreElMapa = false;

    #region Escala.
    private static readonly Point miOffsetDeEscalaEnPixels = new Point(5, 10);
    private readonly Font miLetraParaEscala = new Font("Arial", 8);
    private readonly Brush miPincelDeFondoParaEscala = Brushes.White;
    private readonly Brush miPincelParaEscala = Brushes.Black;
    private readonly Pen miLápizDeFondoParaEscala = new Pen(Color.White, 3);
    private readonly Pen miLápizParaEscala = Pens.Black;

    /// <summary>
    /// Largo mínimo en pixels del segmento de la escala.
    /// </summary>
    /// <remarks>
    /// Este valor garantiza que la distancia entre los marcadores de la escala
    /// sea suficiente para dibujar la distancia.
    /// </remarks>
    private static readonly int miLargoDelSegmentoMínimoEnPixels = 50;

    /// <summary>
    ///  Largo mínimo en metros del segmento de la escala.
    /// </summary>
    private double miLargoDelSegmentoMínimoEnMetros = 0;
    #endregion
    #endregion

    #region Clases
    /// <summary>
    /// Define un punto adicional para dibujar en el mapa.
    /// </summary>
    public class PuntoAdicional
    {
      /// <summary>
      /// Obtiene las coordenadas.
      /// </summary>
      public readonly PointF Coordenadas;

      /// <summary>
      /// Obtiene el pincel.
      /// </summary>
      public readonly Brush Pincel;

      /// <summary>
      /// Obtiene el tamaño.
      /// </summary>
      public readonly int Tamaño;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="lasCoordenadas">Las Coordenadas.</param>
      /// <param name="elPincel">El pincel para dibujar el punto.</param>
      /// <param name="elTamaño">El tamaño del punto.</param>
      public PuntoAdicional(PointF lasCoordenadas, Brush elPincel, int elTamaño)
      {
        Coordenadas = lasCoordenadas;
        Pincel = elPincel;
        Tamaño = elTamaño;
      }
    }
    
    
    /// <summary>
    /// Define una polilínea adicional para dibujar en el mapa.
    /// </summary>
    public class PolilíneaAdicional
    {
      /// <summary>
      /// Obtiene las coordenadas.
      /// </summary>
      public readonly Coordenadas[] Coordenadas;

      /// <summary>
      /// Obtiene el lápiz.
      /// </summary>
      public readonly Pen Lápiz;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="lasCoordenadas">Las Coordenadas.</param>
      /// <param name="elLápiz">El lápiz para dibujar la polilínea.</param>
      public PolilíneaAdicional(Coordenadas[] lasCoordenadas, Pen elLápiz)
      {
        Coordenadas = lasCoordenadas;
        Lápiz = elLápiz;
      }
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestre todo el mapa.
    /// </summary>
    /// <remarks>
    /// Si esta propiedad es verdadera entonces se supercede
    /// la propiedad de region visible.
    /// </remarks>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra Todo el Mapa")]
    public bool MuestraTodoElMapa
    {
      get
      {
        return miMuestraTodoElMapa;
      }

      set
      {
        if (miMuestraTodoElMapa != value)
        {
          miMuestraTodoElMapa = value;
          InicializaRegionVisibleActiva(true);
        }
      }
    }


    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestren los PDIs.
    /// </summary>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra los PDIs")]
    public bool MuestraPDIs { get; set; }


    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestren las Vías.
    /// </summary>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra las Vías")]
    public bool MuestraVías { get; set; }


    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestren los Polígonos.
    /// </summary>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra los Polígonos")]
    public bool MuestraPolígonos { get; set; }


    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestren las Polilineas.
    /// </summary>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra las Polilíneas")]
    public bool MuestraPolilíneas { get; set; }


    /// <summary>
    /// Obtiene o pone una variable lógica que indica que se
    /// muestren todos los elementos.
    /// </summary>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Muestra todos los elementos.")]
    public bool MuestraTodosLosElementos { get; set; }


    /// <summary>
    /// Obtiene o pone la region visible.
    /// </summary>
    /// <remarks>
    /// Esta region solo tiene efecto si <see cref="MuestraTodoElMapa"/>
    /// es falsa.
    /// </remarks>
    [Browsable(true)]
    [Bindable(true)]
    [Description("Rectángulo Visible en Coordenadas")]
    public RectangleF RectánguloVisibleEnCoordenadas 
    {
      get
      {
        return miRectánguloVisibleEnCoordenadas;
      }

      set
      {
        if (miRectánguloVisibleEnCoordenadas != value)
        {
          miRectánguloVisibleEnCoordenadas = value;
          InicializaRegionVisibleActiva(true);
        }
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseMapa()
    {
      InitializeComponent();

      // Abilita el Double Buffer para dibujar sin
      // parpadeo.
      this.SetStyle(
        ControlStyles.AllPaintingInWmPaint |
        ControlStyles.UserPaint |
        ControlStyles.OptimizedDoubleBuffer, true);

      // Conecta el evento de la rueda del ratón.
      miGráficoDelMapa.MouseWheel += EnRuedaDelRatón;

      InicializaRegionVisibleActiva(false);
    }


    /// <summary>
    /// Retorna las coordenadas del rectángulo que encierra los elementos dados.
    /// </summary>
    /// <param name="losElementos">Los elementos dados.</param>
    public static RectangleF RectanguloQueEncierra(IList<ElementoDelMapa> losElementos)
    {
      double mínimaLatitud = double.PositiveInfinity;
      double máximaLatitud = double.NegativeInfinity;
      double mínimaLongitud = double.PositiveInfinity;
      double máximaLongitud = double.NegativeInfinity;
      foreach (ElementoDelMapa elemento in losElementos)
      {
        if (elemento is PDI)
        {
          PDI pdi = (PDI)elemento;

          Coordenadas coordenadas = pdi.Coordenadas;
          BuscaCoordenadasQueEncierran(
            coordenadas,
            ref mínimaLatitud,
            ref máximaLatitud,
            ref mínimaLongitud,
            ref máximaLongitud);
        }
        else if (elemento is Polígono)
        {
          Polígono polígono = (Polígono)elemento;

          foreach (PointF coordenadas in polígono.Coordenadas)
          {
            BuscaCoordenadasQueEncierran(
              coordenadas, 
              ref mínimaLatitud, 
              ref máximaLatitud,
              ref mínimaLongitud,
              ref máximaLongitud);
          }
        }
        else if (elemento is Polilínea)
        {
          Polilínea polilínea = (Polilínea)elemento;

          foreach (PointF coordenadas in polilínea.Coordenadas)
          {
            BuscaCoordenadasQueEncierran(
              coordenadas,
              ref mínimaLatitud,
              ref máximaLatitud,
              ref mínimaLongitud,
              ref máximaLongitud);
          }
        }
      }

      RectangleF rectángulo = new RectangleF(
        (float)mínimaLongitud,
        (float)mínimaLatitud,
        (float)(máximaLongitud - mínimaLongitud),
        (float)(máximaLatitud - mínimaLatitud));

      return rectángulo;
    }


    /// <summary>
    /// Obtiene la lista de puntos addicionales para pintar.
    /// </summary>
    public IList<PuntoAdicional> PuntosAddicionales
    {
      get
      {
        return misPuntosAdicionales;
      }
    }


    /// <summary>
    /// Obtiene la lista de polilíneas addicionales para pintar.
    /// </summary>
    public IList<PolilíneaAdicional> PolilíneasAdicionales
    {
      get
      {
        return misPolilíneasAdicionales;
      }
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // Indicamos que hay un mapa presente.
      miHayUnMapa = true;

      // Borra los puntos adicionales.
      misPuntosAdicionales.Clear();

      // Calcula el rectángulo en coordenadas que encierra al mapa.
      miRectánguloDeDataEnCoordenadas = RectanguloQueEncierra(ManejadorDeMapa.Elementos);

      // Con un mapa nuevo hay que forzar el mostrar todo el 
      // mapa porque la region visible puede ser inválida
      // para en mapa nuevo.
      // Para evitar un doble refrescamiento se llama
      // InicializaRegionVisibleActiva() sin permitir el
      // refrescamiento y luego se llama a Refresh().
      miMuestraTodoElMapa = true;
      miRectánguloVisibleEnCoordenadas = miRectánguloDeDataEnCoordenadas;
      InicializaRegionVisibleActiva(false);
      Refresh();
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      miRectánguloDeDataEnCoordenadas = RectanguloQueEncierra(ManejadorDeMapa.Elementos);

      // Si los elementos del mapa se modificaron entonces hay que
      // forzar un refrescamiento del mapa.
      Refresh();
    }


    private static void BuscaCoordenadasQueEncierran(
      PointF lasCoordenadas,
      ref double laMínimaLatitud,
      ref double laMáximaLatitud,
      ref double laMínimaLongitud,
      ref double laMáximaLongitud)
    {
      // Solo procesa coordenadas válidas.
      if (!double.IsNaN(lasCoordenadas.X) &&
        !double.IsNaN(lasCoordenadas.Y))
      {
        laMínimaLatitud = Math.Min(laMínimaLatitud, lasCoordenadas.Y);
        laMáximaLatitud = Math.Max(laMáximaLatitud, lasCoordenadas.Y);
        laMínimaLongitud = Math.Min(laMínimaLongitud, lasCoordenadas.X);
        laMáximaLongitud = Math.Max(laMáximaLongitud, lasCoordenadas.X);
      }
    }

    
    private void EnPintar(object elEnviador, PaintEventArgs losArgumentosDePintar)
    {
      // Nos salimos si no hay un mapa cargado.
      if (!miHayUnMapa)
      {
        return;
      }

      // Empieza a medir el tiempo.
      Stopwatch timer = new Stopwatch();
      timer.Start();

      InicializaParámetrosGráficos(losArgumentosDePintar);

      // Dibuja el borde del mapa.
      Rectangle borde = CoordenadasAPixels(miRectánguloDeDataEnCoordenadas);
      miGráficador.DrawRectangle(miLápizParaBorde, borde);

      // El orden de dibujo es:
      //  - Polígonos.
      //  - Polilíneas adicionales.
      //  - Polilíneas.
      //  - Vías.
      //  - Puntos adicionales.
      //  - PDIs.

      // Dibuja los elementos si tenemos un manejador de mapa válido.
      if (ManejadorDeMapa != null)
      {
        if (MuestraPolígonos | MuestraTodosLosElementos)
        {
          DibujaPolígonos();
        }

        DibujaPolilíneasAdicionales();

        if (MuestraPolilíneas | MuestraTodosLosElementos)
        {
          DibujaPolilíneas();
        }

        if (MuestraVías | MuestraTodosLosElementos)
        {
          DibujaVías();
        }

        DibujaPuntosAdicionales();

        if (MuestraPDIs | MuestraTodosLosElementos)
        {
          DibujaPDIs();
        }
      }

      DibujaEscala();

      // Muestra el tiempo que tomo pintar el mapa.
      timer.Stop();
      DibujaTextoConFondo(
        timer.ElapsedMilliseconds.ToString() + " ms.",
        0, 0,
        miLetraParaTiempo,
        miPincelParaTiempo,
        miPincelDeFondoParaTiempo);
    }


    private void InicializaParámetrosGráficos(PaintEventArgs losArgumentosDePintar)
    {
      miAreaDeDibujo = losArgumentosDePintar.ClipRectangle;

      miGráficador = losArgumentosDePintar.Graphics;

      Rectangle rectánguloDelClienteEnPixeles = this.ClientRectangle;

      // Calcula la escala del gráfico en el rango del mapa.
      float escalaEnLongitud = rectánguloDelClienteEnPixeles.Width / miRectánguloVisibleActivoEnCoordenadas.Width;
      float escalaEnLatitud = rectánguloDelClienteEnPixeles.Height / miRectánguloVisibleActivoEnCoordenadas.Height;
      miEscalaDeCoordenadasAPixeles = Math.Min(escalaEnLatitud, escalaEnLongitud);

      // Calcula origen para que el mapa esté centrado.
      float rangoVisibleDeLatitud = (float)(rectánguloDelClienteEnPixeles.Height / miEscalaDeCoordenadasAPixeles);
      float rangoVisibleDeLongitud = (float)(rectánguloDelClienteEnPixeles.Width / miEscalaDeCoordenadasAPixeles);
      miOrigenEnCoordenadas = new PointF(
        miRectánguloVisibleActivoEnCoordenadas.X - ((rangoVisibleDeLongitud - miRectánguloVisibleActivoEnCoordenadas.Width) / 2),
        miRectánguloVisibleActivoEnCoordenadas.Y - ((rangoVisibleDeLatitud - miRectánguloVisibleActivoEnCoordenadas.Height) / 2));
    }


    private void DibujaPDIs()
    {
      // Primero dibuja los PDIs sin modificar ni eliminados.
      foreach (PDI pdi in ManejadorDeMapa.PDIs)
      {
        if (!pdi.FuéModificado && !pdi.FuéEliminado)
        {
          DibujaPDI(pdi, miPincelParaPDI, 3);
        }
      }

      // Después dibuja los PDIs modificados para que estén
      // sobre los PDI sin modificar.
      foreach (PDI pdi in ManejadorDeMapa.PDIs)
      {
        if (pdi.FuéModificado)
        {
          DibujaPDI(pdi, miPincelParaPDIModificado, 5);
        }
      }

      // Finalmente dibuja los PDIs eliminados.
      foreach (PDI pdi in ManejadorDeMapa.PDIs)
      {
        if (pdi.FuéEliminado)
        {
          DibujaPDI(pdi, miPincelParaPDIEliminado, 5);
        }
      }
    }


    private void DibujaPolígonos()
    {
      // Dibuja los Polígonos.
      foreach (Polígono polígono in ManejadorDeMapa.Polígonos)
      {
        Tipo tipo = polígono.Tipo;
        DibujaPolígono(polígono, CaracterísticasDePolígonos.Pincel(tipo));
      }
    }


    private void DibujaPolígono(Polígono elPolígono, Brush elPincel)
    {
      Point[] puntos = CoordenadasAPixels(elPolígono.Coordenadas);

      // Nos salimos si el polígono no es visible.
      if (!SonPuntosVisibles(puntos))
      {
        return;
      }

      // Dibuja el polígono.
      if (puntos.Length != 0)
      {
        miGráficador.FillPolygon(elPincel, puntos);
      }
    }


    private bool SonPuntosVisibles(Point[] losPuntos)
    {
      int mínimoX = int.MaxValue;
      int máximoX = int.MinValue;
      int mínimoY = int.MaxValue;
      int máximoY = int.MinValue;
      foreach (Point punto in losPuntos)
      {
        mínimoX = Math.Min(mínimoX, punto.X);
        máximoX = Math.Max(máximoX, punto.X);
        mínimoY = Math.Min(mínimoY, punto.Y);
        máximoY = Math.Max(máximoY, punto.Y);
      }
      Rectangle rectángulo = new Rectangle(
        mínimoX, mínimoY,
        (máximoX - mínimoX), (máximoY - mínimoY));
      bool esVisible = miAreaDeDibujo.IntersectsWith(rectángulo);
      return esVisible;
    }


    private void DibujaPolilíneas()
    {
      // Dibuja las Polilíneas.
      foreach (Polilínea polilínea in ManejadorDeMapa.Polilíneas)
      {
        Tipo tipo = polilínea.Tipo;
        DibujaPolilínea(polilínea.Coordenadas, CaracterísticasDePolilíneas.Lápiz(tipo));
      }
    }


    private void DibujaVías()
    {
      // Dibuja las Vías.
      foreach (Vía vía in ManejadorDeMapa.Vías)
      {
        Tipo tipo = vía.Tipo;
        DibujaPolilínea(vía.Coordenadas, CaracterísticasDePolilíneas.Lápiz(tipo));
      }
    }


    private void DibujaPolilínea(
      Coordenadas[] laPolilínea,
      Pen elLapiz)
    {
      Point[] puntos = CoordenadasAPixels(laPolilínea);
      
      // Nos salimos si la polilínea no es visible.
      if (!SonPuntosVisibles(puntos))
      {
        return;
      }

      // Nos salimos si no tenemos suficientes coordenadas.
      int númeroDeCoordenadas = puntos.Length;
      if (númeroDeCoordenadas < 1)
      {
        return;
      }
      else if (númeroDeCoordenadas == 1)
      {
        // Dibuja un punto.
        DibujaPunto(laPolilínea[0], miPincelParaVíaConUnaCoordenada, 11);
      }
      else
      {
        // Dibuja la polilínea.
        miGráficador.DrawLines(elLapiz, puntos);
      }
    }


    private void DibujaPolilíneasAdicionales()
    {
      // Dibuja las polilíneas adicionales.
      foreach (PolilíneaAdicional polilínea in misPolilíneasAdicionales)
      {
        DibujaPolilínea(polilínea.Coordenadas, polilínea.Lápiz);
      }
    }


    private void DibujaPuntosAdicionales()
    {
      // Dibuja los puntos adicionales.
      foreach (PuntoAdicional punto in misPuntosAdicionales)
      {
        DibujaPunto(punto.Coordenadas, punto.Pincel, punto.Tamaño);
      }
    }


    private void DibujaPDI(PDI elPDI, Brush elPincel, int elTamaño)
    {
      // Obtiene las coordenadas gráficas del PDI.
      Point punto = CoordenadasAPixels(new PointF(
        (float)elPDI.Coordenadas.Longitud,
        (float)elPDI.Coordenadas.Latitud));

      // Nos salimos si el PDI no es visible.
      if (!miAreaDeDibujo.Contains(punto))
      {
        return;
      }

      // Dibuja el PDI centrado.
      DibujaPunto(punto, elPincel, elTamaño);

      // Dibuja el nombre si la escala es lo suficentemente grande.
      if (miEscalaDeCoordenadasAPixeles > 100000)
      {
        DibujaTextoConFondo(
          elPDI.Nombre,
          punto.X, punto.Y +5,
          miLetraParaNombre, 
          miPincelParaNombre,
          miPincelDeFondoParaNombre);
      }
    }


    private void DibujaTextoConFondo(string elTexto, int laX, int laY, Font laLetra, Brush elPincel, Brush elPincelParaFondo)
    {
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX - 1, laY - 1);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX - 1, laY);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX - 1, laY + 1);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX, laY - 1);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX, laY + 1);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX + 1, laY - 1);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX + 1, laY);
      miGráficador.DrawString(elTexto, laLetra, elPincelParaFondo, laX + 1, laY + 1);
      miGráficador.DrawString(elTexto, laLetra, elPincel, laX, laY);
    }


    private void DibujaPunto(PointF elPunto, Brush elPincel, int elTamaño)
    {
      // Obtiene las coordenadas gráficas del punto.
      Point punto = CoordenadasAPixels(elPunto);

      DibujaPunto(punto, elPincel, elTamaño);
    }


    private void DibujaPunto(Point elPunto, Brush elPincel, int elTamaño)
    {
      // Nos salimos si el punto no es visible.
      if (!miAreaDeDibujo.Contains(elPunto))
      {
        return;
      }

      // Dibuja el punto centrado.
      int offset = elTamaño / 2;
      int x = elPunto.X - offset;
      int y = elPunto.Y - offset;
      miGráficador.FillRectangle(elPincel, x, y, elTamaño, elTamaño);
    }

    
    private Point CoordenadasAPixels(PointF lasCoordenadas)
    {
      int abajo = this.ClientRectangle.Bottom;

      int x = (int)Math.Round((lasCoordenadas.X - miOrigenEnCoordenadas.X) * miEscalaDeCoordenadasAPixeles) - 1;
      int y = (int)Math.Round((lasCoordenadas.Y - miOrigenEnCoordenadas.Y) * miEscalaDeCoordenadasAPixeles) + 1;
      y = abajo - y;

      return new Point(x, y);
    }


    private Point[] CoordenadasAPixels(Coordenadas[] lasCoordenadas)
    {
      int largo = lasCoordenadas.Length;
      Point[] puntos = new Point[largo];

      for (int i = 0; i < largo; ++i)
      {
        puntos[i] = CoordenadasAPixels(lasCoordenadas[i]);
      }

      return puntos;
    }


    private Rectangle CoordenadasAPixels(RectangleF lasCoordenadas)
    {
      // Calcula el tamaño.
      Size tamaño = new Size(
        (int)Math.Round(lasCoordenadas.Width * miEscalaDeCoordenadasAPixeles),
        (int)Math.Round(lasCoordenadas.Height * miEscalaDeCoordenadasAPixeles));

      // Calcula el origen.
      Point origenAbajoIzquierda = CoordenadasAPixels(lasCoordenadas.Location);
      Point origenArribaIzquierda = new Point(
        origenAbajoIzquierda.X,
        origenAbajoIzquierda.Y - tamaño.Height);

      return new Rectangle(origenArribaIzquierda, tamaño);
    }

    
    private PointF PixelsACoordenadas(Point losPixeles)
    {
      int abajo = this.ClientRectangle.Bottom;
      int y = abajo - losPixeles.Y;
      float longitud = (float)(losPixeles.X / miEscalaDeCoordenadasAPixeles) + miOrigenEnCoordenadas.X;
      float latitud = (float)(y / miEscalaDeCoordenadasAPixeles) + miOrigenEnCoordenadas.Y;

      return new PointF(longitud, latitud);
    }


    private void InicializaRegionVisibleActiva(bool elRefrescaSiHayCambios)
    {
      RectangleF rectánguloOriginal = miRectánguloVisibleActivoEnCoordenadas;

      if (miMuestraTodoElMapa)
      {
        miRectánguloVisibleActivoEnCoordenadas = miRectánguloDeDataEnCoordenadas;
      }
      else
      {
        miRectánguloVisibleActivoEnCoordenadas = miRectánguloVisibleEnCoordenadas;
      }

      if (elRefrescaSiHayCambios)
      {
        if (rectánguloOriginal != miRectánguloVisibleActivoEnCoordenadas)
        {
          Refresh();
        }
      }
    }


    private void DibujaEscala()
    {
      // Calcula el largo del segmento de la escala en metros.
      // Para eso calculamos las coordenadas de los extremos del segmento 
      // y con las coordenadas calculamos la distancia en metros.
      int bordeDeAbajoEnPixels = this.ClientRectangle.Bottom;
      Point origenDelSegmentoEnPixels = new Point(miOffsetDeEscalaEnPixels.X, bordeDeAbajoEnPixels - miOffsetDeEscalaEnPixels.Y);
      PointF origenDelSegmentoEnCoordenadas = PixelsACoordenadas(origenDelSegmentoEnPixels);
      Point finalDelSegmentoMínimoEnPixels = new Point(origenDelSegmentoEnPixels.X + miLargoDelSegmentoMínimoEnPixels, origenDelSegmentoEnPixels.Y);
      PointF finalDelSegmentoMínimoEnCoordenadas = PixelsACoordenadas(finalDelSegmentoMínimoEnPixels);
      miLargoDelSegmentoMínimoEnMetros = Coordenadas.Distancia(origenDelSegmentoEnCoordenadas, finalDelSegmentoMínimoEnCoordenadas);

      // Descompone el largo del segmento mínimo en exponente y mantisa.
      int exponenteDelSegmentoMínimo = (int)Math.Log10(miLargoDelSegmentoMínimoEnMetros);
      double mantisaDelSegmentoMínimo = miLargoDelSegmentoMínimoEnMetros / Math.Pow(10, exponenteDelSegmentoMínimo);

      // Como queremos mostrar la escala en números enteros tenemos que hacer
      // que la mantisa de la escala sea el menor número entero que sea mayor 
      // o igual que la mantisa del segmento mínimo.
      // Por ejemplo, si el largo del segmento mínimo es 93m, entonces el 
      // exponente es 1 y la mantisa es 9,3 (93 = 9,3 * 10^1). Como en la
      // escala queremos mostar 100m en vez de 93m, entonces usamos una
      // mantisa de 10.
      int mantisaDeLaEscala = (int)Math.Ceiling(mantisaDelSegmentoMínimo);
      int largoDelSegmentoEnMetros = mantisaDeLaEscala * (int)Math.Pow(10, exponenteDelSegmentoMínimo);

      // Como la escala que vamos a mostrar puede ser mayor que la escala
      // del segmento mínimo (por ejemplo: 100m en vez de 93m), entonces
      // tenemos que ajustar el largo del segmento a dibujar.
      double factor = largoDelSegmentoEnMetros / miLargoDelSegmentoMínimoEnMetros;
      int intervaloEnPixels = (int)Math.Round(miLargoDelSegmentoMínimoEnPixels * factor);

      // Pone la unidad de la escala (metros o kilómetros).
      string unidad;
      int exponenteDeLaEscala;
      if (exponenteDelSegmentoMínimo < 3)
      {
        unidad = "m";
        exponenteDeLaEscala = 0;
      }
      else
      {
        unidad = "km";
        exponenteDeLaEscala = 3;
      }
      int largoDelSegmentoEnMetrosOKilómetros = (int)Math.Round(largoDelSegmentoEnMetros / Math.Pow(10, exponenteDeLaEscala)); 

      #region Dibuja la Escala
      // Construye los puntos del segmento y los marcadores.
      int largoMarcador = 5;
      Point marcadorIzquierdo = new Point(origenDelSegmentoEnPixels.X, origenDelSegmentoEnPixels.Y - largoMarcador);
      Point finalDelSegmento = new Point(origenDelSegmentoEnPixels.X + intervaloEnPixels, origenDelSegmentoEnPixels.Y);
      Point marcadorDerecho = new Point(origenDelSegmentoEnPixels.X + intervaloEnPixels, origenDelSegmentoEnPixels.Y - largoMarcador);
      Point[] líneas = new Point[] {
        marcadorIzquierdo,
        origenDelSegmentoEnPixels,
        finalDelSegmento,
        marcadorDerecho};

      // Dibuja el segmento y los marcadores con un fondo para que
      // tenga un buen contraste.
      miGráficador.DrawLines(miLápizDeFondoParaEscala, líneas);
      miGráficador.DrawLines(miLápizParaEscala, líneas);

      // Dibuja el texto.
      string texto = largoDelSegmentoEnMetrosOKilómetros + " " + unidad;
      DibujaTextoConFondo(texto, origenDelSegmentoEnPixels.X + 2, origenDelSegmentoEnPixels.Y - 14, miLetraParaEscala, miPincelParaEscala, miPincelDeFondoParaEscala);
      #endregion
    }

    
    private void EnDobleClick(object sender, EventArgs e)
    {
      InicializaRegionVisibleActiva(true);
    }


    private void EnRuedaDelRatón(object sender, MouseEventArgs losArgumentosDelRatón)
    {
      if (miRatónEstaSobreElMapa)
      {
        int delta = losArgumentosDelRatón.Delta;
        if (delta > 0)
        {
          // Acerca el mapa si el segmento de la escala es más de 1m.
          if (miLargoDelSegmentoMínimoEnMetros > 1)
          {
            float zoom = 1.2f;
            ZoomMapa(zoom, losArgumentosDelRatón.Location);
          }
        }
        else if (delta < 0)
        {
          // Aleja el mapa si el segmento de la escala es menos de 500km.
          if (miLargoDelSegmentoMínimoEnMetros < 500000)
          {
            float zoom = 0.8f;
            ZoomMapa(zoom, losArgumentosDelRatón.Location);
          }
        }
      }
    }


    private void ZoomMapa(float elFactor, Point laPosiciónDelRatón)
    {
      // Calcula en nuevo tamaño de la region visible en coordenadas.
      SizeF nuevoTamaño = new SizeF(
        miRectánguloVisibleActivoEnCoordenadas.Width / elFactor,
        miRectánguloVisibleActivoEnCoordenadas.Height / elFactor);

      // Calcula el nuevo origen de la region visible en coordenadas.
      PointF origenActivo = miRectánguloVisibleActivoEnCoordenadas.Location;
      PointF posiciónDelMouseEnCoordenadas = PixelsACoordenadas(laPosiciónDelRatón);
      float deltaX = posiciónDelMouseEnCoordenadas.X - origenActivo.X;
      float deltaY = posiciónDelMouseEnCoordenadas.Y - origenActivo.Y;
      PointF nuevoOrigen = new PointF(
        posiciónDelMouseEnCoordenadas.X - (deltaX / elFactor),
        posiciónDelMouseEnCoordenadas.Y - (deltaY / elFactor));

      // Actualiza la region visible.
      miRectánguloVisibleActivoEnCoordenadas = new RectangleF(nuevoOrigen, nuevoTamaño);
      Refresh();
    }


    private void EnRatónEntrando(object elObjeto, EventArgs e)
    {
      // Toma el foco cuando el ratón esta sobre el mapa.
      miRatónEstaSobreElMapa = true;
      Control control = (Control)elObjeto;
      control.Focus();
    }


    private void EnRatónSaliendo(object elObjeto, EventArgs e)
    {
      // Suelta el foco cuando el ratón esta sobre el mapa.
      miRatónEstaSobreElMapa = false;
    }


    private void EnRatónMoviendo(object sender, MouseEventArgs losArgumentosDelRatón)
    {
      if (miHayUnMapa)
      {
        PointF puntoDeCoordenadas = PixelsACoordenadas(losArgumentosDelRatón.Location);
        Coordenadas coordenadas = new Coordenadas(puntoDeCoordenadas.Y, puntoDeCoordenadas.X);

        // Muestra las coordenadas.
        EscuchadorDeEstatus.Coordenadas = coordenadas;
      }
    }
    #endregion
  }
}
