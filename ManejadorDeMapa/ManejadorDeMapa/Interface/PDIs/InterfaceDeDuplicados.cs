using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interface.PDIs
{
  public partial class InterfaceDeDuplicados : InterfaceBase
  {
    #region Campos
    private ManejadorDePDIs miManejadorDePDIs;
    private List<PDI> misPDIs = new List<PDI>();
    private Dictionary<ListViewGroup, List<PDI>> miPDIsPorGrupo = new Dictionary<ListViewGroup, List<PDI>>();
    private Brush miPincelDePDI = new SolidBrush(Color.Black);
    private Brush miPincelDePDIDuplicado = new SolidBrush(Color.Orange);
    private Color miColorDeFondoOriginal;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public override ManejadorDeMapa ManejadorDeMapa
    {
      set
      {
        // Deja de manejar los eventos.
        if (miManejadorDePDIs != null)
        {
          miManejadorDePDIs.BuscadorDeDuplicados.EncontraronDuplicados -= EnEncontraronDuplicados;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miManejadorDePDIs = value.ManejadorDePDIs;
          miManejadorDePDIs.BuscadorDeDuplicados.EncontraronDuplicados += EnEncontraronDuplicados;
          InicializaDistanciaMáxima();
          InicializaDistanciaHamming();
        }

        // Pone el manejador de mapa en los componentes.
        miMapa.ManejadorDeMapa = value;
      }
    }


    /// <summary>
    /// Obtiene o pone el escuchador de estatus.
    /// </summary>
    public override IEscuchadorDeEstatus EscuchadorDeEstatus
    {
      set
      {
        base.EscuchadorDeEstatus = value;
        miMapa.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaceDeDuplicados()
    {
      InitializeComponent();

      // Inicialización.
      miColorDeFondoOriginal = miLista.BackColor;
    }
    #endregion

    #region Métodos Privados
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnEncontraronDuplicados(elEnviador, losArgumentos);
    }


    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No es necesario hacer nada aqui.
    }


    private void EnEncontraronDuplicados(object elEnviador, EventArgs losArgumentos)
    {
      // Añade los PDIs duplicados.
      miLista.SuspendLayout();
      miLista.Items.Clear();
      miLista.Groups.Clear();
      misPDIs.Clear();
      foreach (KeyValuePair<PDI, IList<PDI>> item in miManejadorDePDIs.GruposDeDuplicados)
      {
        PDI pdiBase = item.Key;
        IList<PDI> duplicados = item.Value;
        List<PDI> pdis = new List<PDI> { pdiBase };
        pdis.AddRange(duplicados);

        // Crea un grupo para cada conjunto de duplicados.
        ListViewGroup grupo = new ListViewGroup(pdiBase.Nombre);
        miLista.Groups.Add(grupo);
        miPDIsPorGrupo[grupo] = pdis;

        // Añade todos los PDIs.
        miLista.Items.Add(CreaItemDeLista(pdiBase, grupo, 0));
        misPDIs.Add(pdiBase);
        foreach (PDI duplicado in duplicados)
        {
          double distancia = Coordenadas.Distancia(pdiBase.Coordenadas, duplicado.Coordenadas);
          miLista.Items.Add(CreaItemDeLista(duplicado, grupo, distancia));
          misPDIs.Add(duplicado);
        }
      }
      miLista.ResumeLayout(false);

      // Actualiza la Pestaña.
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        int númeroDePosiblesDuplicados = miLista.Groups.Count;
        pestaña.Text = "Posibles Duplicados (" + númeroDePosiblesDuplicados + ")";
      }

      // Actualiza el Número de PDIs a Eliminar.
      ActualizaNúmeroDePDIsAEliminar();
    }

    
    private ListViewItem CreaItemDeLista(PDI elPdi, ListViewGroup elGrupo, double laDistancia)
    {
      ListViewItem item = new ListViewItem(
              new string[] { 
                elPdi.Número.ToString(),
                elPdi.TipoComoTexto(), 
                elPdi.Descripción,
                elPdi.Nombre,
                elPdi.Coordenadas.ToString(),
                laDistancia.ToString("0.0")
              },
              elGrupo);

      item.Checked = false;

      return item;
    }


    private void EnItemSeleccionado(object elEnviador, ItemCheckedEventArgs elArgumentoDelEvento)
    {
      ActualizaNúmeroDePDIsAEliminar();
    }


    private void ActualizaNúmeroDePDIsAEliminar()
    {
      int númeroDePDIsAEliminar = 0;
      foreach (ListViewItem item in miLista.Items)
      {
        if (item.Checked)
        {
          ++númeroDePDIsAEliminar;

          item.BackColor = Color.LightPink;
        }
        else
        {
          item.BackColor = miColorDeFondoOriginal;
        }

        if (númeroDePDIsAEliminar > 0)
        {
          miTextoNumeroDePDIsSelecionados.Text = númeroDePDIsAEliminar + " PDIs para Eliminar";
          miBotonEliminarPDIs.Enabled = true;
        }
        else
        {
          miTextoNumeroDePDIsSelecionados.Text = string.Empty;
          miBotonEliminarPDIs.Enabled = false;
        }
      }
    }


    private void EnBotónEliminarPDIs(object sender, EventArgs e)
    {
      // Suspende notificaciones.
      ManejadorDeMapa.SuspendeEventos();

      // Obtiene la lista de PDIs a eliminar.
      List<PDI> pdisAEliminar = new List<PDI>();
      foreach (ListViewItem item in miLista.Items)
      {
        if (item.Checked)
        {
          pdisAEliminar.Add(misPDIs[item.Index]);
        }
      }

      // Pregunta si se quiere eliminar los PDIs.
      StringBuilder texto = new StringBuilder();
      texto.AppendLine("Esta seguro que quiere borrar estos PDIs?");
      foreach (PDI pdi in pdisAEliminar)
      {
        texto.AppendLine("  #" + pdi.Número + ", " + pdi.Nombre);
      }
      DialogResult respuesta = MessageBox.Show(
        texto.ToString(), 
        "Eliminar PDIs", 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Warning);

      // Elimina los PDIs si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        foreach (PDI pdi in pdisAEliminar)
        {
          pdi.Elimina("Manualmente eliminado en la pestaña de 'Posibles Duplicados'");
        }

        // Busca otra vez los PDIs duplicados tomando en cuenta
        // los que se acaban de eliminar.
        ManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
      }

      // Restablece notificaciones.
      ManejadorDeMapa.RestableceEventos();
    }


    private void EnClick(object laLista, MouseEventArgs losArgumentosDelRatón)
    {
      // Obtiene el grupo seleccionado.
      ListView lista = (ListView)laLista;
      ListViewHitTestInfo información = lista.HitTest(losArgumentosDelRatón.Location);
      ListViewGroup grupo = información.Item.Group;
      List<PDI> pdis = miPDIsPorGrupo[grupo];

      // Busca el rango visible para los PDIs.
      IList<ElementoDelMapa> elementos = new List<ElementoDelMapa>(pdis.ToArray());
      RectangleF rectánguloQueEncierra = InterfaceMapa.RectanguloQueEncierra(elementos);
      float margen = 0.0001f;
      RectangleF rectánguloVisible = new RectangleF(
        (float)rectánguloQueEncierra.X - margen,
        (float)rectánguloQueEncierra.Y - margen,
        rectánguloQueEncierra.Width + (2 * margen),
        rectánguloQueEncierra.Height + (2 * margen));

      // Dibuja los PDIs como PDIs adicionales para resaltarlos.
      miMapa.PuntosAddicionales.Clear();
      PDI pdiDeSeleccionado = misPDIs[información.Item.Index];
      miMapa.PuntosAddicionales.Add(new InterfaceMapa.PuntoAdicional(
        pdiDeSeleccionado.Coordenadas, miPincelDePDI, 13));
      foreach (PDI pdi in pdis)
      {
        miMapa.PuntosAddicionales.Add(new InterfaceMapa.PuntoAdicional(
          pdi.Coordenadas, miPincelDePDIDuplicado, 7));
      }

      // Muestra el mapa en la region deseada.
      miMapa.Enabled = true;
      miMapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
      miMapa.MuestraTodoElMapa = false;
      miMapa.Refresh();
    }


    private void EnBotónBuscaDuplicados(object sender, EventArgs e)
    {
      ManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
    }


    private void EnCambióBarraDeDistancia(object sender, EventArgs e)
    {
      InicializaDistanciaMáxima();
    }


    private void InicializaDistanciaMáxima()
    {
      int distancia = miBarraDeDistancia.Value * 10;
      miTextoDistancia.Text = distancia + " m";
      miManejadorDePDIs.BuscadorDeDuplicados.DistanciaMáxima = distancia;
    }


    private void EnCambioBarraDeParecidoDelNombre(object sender, EventArgs e)
    {
      InicializaDistanciaHamming();
    }


    private void InicializaDistanciaHamming()
    {
      int distanciaHamming = miBarraDeParecidoDeNombre.Value;
      string texto = distanciaHamming.ToString();
      switch (distanciaHamming)
      {
        case 0:
          texto += " - Idéntico";
          break;
        case 1:
        case 2:
          texto += " - Muy Parecido";
          break;
        case 3:
          texto += " - Parecido";
          break;
        case 4:
        case 5:
        case 6:
          texto += " - Parecido";
          break;
      }

      miTextoParecidoDelNombre.Text = texto;
      miManejadorDePDIs.BuscadorDeDuplicados.DistanciaHamming = distanciaHamming;
    }
    #endregion
  }
}
