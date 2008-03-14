using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace GpsYv.ManejadorDeMapa.Interface
{
  public partial class InterfaceMapa : InterfaceBase
  {
    #region Campos
    private bool miHayUnMapa = false;

    // Objetos para dibujar los elementos.
    private readonly Brush miPincelDeFondoParaTiempo = new SolidBrush(Color.White);
    private readonly Brush miPincelParaTiempo = new SolidBrush(Color.Gray);
    private readonly Font miLetraParaTiempo = new Font("Arial", 8);
    private readonly Brush miPincelParaPDI = new SolidBrush(Color.Black);
    private readonly Brush miPincelParaPDIModificado = new SolidBrush(Color.Yellow);
    private readonly Brush miPincelParaPDIEliminado = new SolidBrush(Color.Red);
    private Pen miLápizParaBorde = new Pen(Color.LightGray, 2);
    private readonly Font miLetraParaNombre = new Font("Arial", 8);
    private readonly Brush miPincelDeFondoParaNombre = new SolidBrush(Color.White);
    private readonly Brush miPincelParaNombre = new SolidBrush(Color.Black);

    // Parametros para dibujar los elementos.
    private Graphics miGráficador;
    private float miEscalaDeCoordenadasAPixeles;
    private PointF miOrigenEnCoordenadas;
    private Rectangle miAreaDeDibujo;

    private RectangleF miRectánguloDeDataEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private RectangleF miRectánguloVisibleEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private RectangleF miRectánguloVisibleActivoEnCoordenadas = new RectangleF(0, 0, 1, 1);
    private bool miMuestraTodoElMapa = true;

    private List<PuntoAdicional> misPuntosAdicionales = new List<PuntoAdicional>();

    private bool miRatónEstaSobreElMapa = false;
    #endregion

    #region Clases
    /// <summary>
    /// Define un PDI addicinal para dibujar en el mapa.
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
      /// <param name="elPincel">El pincel para dibujar el PDI.</param>
      /// <param name="elTamaño">El tamaño del PDI.</param>
      public PuntoAdicional(PointF lasCoordenadas, Brush elPincel, int elTamaño)
      {
        Coordenadas = lasCoordenadas;
        Pincel = elPincel;
        Tamaño = elTamaño;
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
    /// Esta region solo tiene efecto si <cref = "MuestraTodoElMapa"/>
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
    public InterfaceMapa()
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
    #endregion

    #region Métodos Privados
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

      // Dibuja los elementos si tenemos un manejador de mapa válido.
      if (ManejadorDeMapa != null)
      {
        // El orden de dibujo es:
        //  - Polígonos.
        //  - Polilíneas.
        //  - PDIs.
        //  - Puntos adicionales.

        if (MuestraPolígonos | MuestraTodosLosElementos)
        {
          DibujaPolígonos();
        }

        if (MuestraPolilíneas | MuestraTodosLosElementos)
        {
          DibujaPolilíneas();
        }

        if (MuestraPDIs | MuestraTodosLosElementos)
        {
          DibujaPDIs();
        }

        // Dibuja los puntos adicionales de último para garantizar que se vean.
        DibujaPuntosAdicionales();
      }

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
      float rangoVisibleDeLatitud = rectánguloDelClienteEnPixeles.Height / miEscalaDeCoordenadasAPixeles;
      float rangoVisibleDeLongitud = rectánguloDelClienteEnPixeles.Width / miEscalaDeCoordenadasAPixeles;
      miOrigenEnCoordenadas = new PointF(
        miRectánguloVisibleActivoEnCoordenadas.X - ((rangoVisibleDeLongitud - miRectánguloVisibleActivoEnCoordenadas.Width) / 2),
        miRectánguloVisibleActivoEnCoordenadas.Y - ((rangoVisibleDeLatitud - miRectánguloVisibleActivoEnCoordenadas.Height) / 2));
    }


    private void DibujaPDIs()
    {
      // Dibuja los PDIs sin modificar ni eliminados.
      foreach (ElementoDelMapa elemento in ManejadorDeMapa.Elementos)
      {
        if (elemento is PDI)
        {
          PDI pdi = (PDI)elemento;
          if (!pdi.FuéModificado && !pdi.FuéEliminado)
          {
            DibujaPDI(pdi, miPincelParaPDI, 3);
          }
        }
      }

      // Dibuja los PDIs modificados.
      foreach (ElementoDelMapa elemento in ManejadorDeMapa.Elementos)
      {
        if (elemento is PDI)
        {
          PDI pdi = (PDI)elemento;
          if (pdi.FuéModificado)
          {
            DibujaPDI(pdi, miPincelParaPDIModificado, 5);
          }
        }
      }

      // Dibuja los PDIs eliminados.
      foreach (ElementoDelMapa elemento in ManejadorDeMapa.Elementos)
      {
        if (elemento is PDI)
        {
          PDI pdi = (PDI)elemento;
          if (pdi.FuéEliminado)
          {
            DibujaPDI(pdi, miPincelParaPDIEliminado, 5);
          }
        }
      }
    }


    private void DibujaPolígonos()
    {
      // Dibuja los PDIs sin modificar ni eliminados.
      foreach (ElementoDelMapa elemento in ManejadorDeMapa.Elementos)
      {
        if (elemento is Polígono)
        {
          Polígono polígono = (Polígono)elemento;
          int tipo = polígono.Tipo;
          DibujaPolígono(polígono, CaracterísticasDePolígonos.Pincel(tipo));
        }
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
      // Dibuja los PDIs sin modificar ni eliminados.
      foreach (ElementoDelMapa elemento in ManejadorDeMapa.Elementos)
      {
        if (elemento is Polilínea)
        {
          Polilínea polilínea = (Polilínea)elemento;
          int tipo = polilínea.Tipo;
          DibujaPolilínea(polilínea, CaracterísticasDePolilíneas.Lápiz(tipo));
        }
      }
    }


    private void DibujaPolilínea(
      Polilínea laPolilínea,
      Pen elLapiz)
    {
      Point[] puntos = CoordenadasAPixels(laPolilínea.Coordenadas);
      
      // Nos salimos si la polilínea no es visible.
      if (!SonPuntosVisibles(puntos))
      {
        return;
      }

      // Dibuja la polilínea.
      miGráficador.DrawLines(elLapiz, puntos);
    }


    private void DibujaPuntosAdicionales()
    {
      // Dibuja los PDI adicionales.
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
      float longitud = (losPixeles.X / miEscalaDeCoordenadasAPixeles) + miOrigenEnCoordenadas.X;
      float latitud = (y / miEscalaDeCoordenadasAPixeles) + miOrigenEnCoordenadas.Y;

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
          float zoom = 1.2f;
          ZoomMapa(zoom, losArgumentosDelRatón.Location);
        }
        else if (delta < 0)
        {
          float zoom = 0.8f;
          ZoomMapa(zoom, losArgumentosDelRatón.Location);
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
