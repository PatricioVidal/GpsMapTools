using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Interfase de Errores de PDIs.
  /// </summary>
  public partial class InterfaseDeErrores : InterfaseBase
  {
    #region Campos
    private ManejadorDePDIs miManejadorDePDIs;
    private List<PDI> misPDIs = new List<PDI>();
    private Brush miPincelDePDI = new SolidBrush(Color.Orange);
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
          miManejadorDePDIs.EncontraronErrores -= EnEncontraronErrores;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miManejadorDePDIs = value.ManejadorDePDIs;

          if (miManejadorDePDIs != null)
          {
            miManejadorDePDIs.EncontraronErrores += EnEncontraronErrores;
          }

          // Pone el manejador de mapa en la interfase de mapa.
          miMapa.ManejadorDeMapa = value;

          // Pone el manejador de PDIs en la interfase de edición de PDIs.
          miMenúEditorDePDI.ManejadorDePDIs = value.ManejadorDePDIs;
        }
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

    #region Métodos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDeErrores()
    {
      InitializeComponent();
    }

    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnEncontraronErrores(elEnviador, losArgumentos);
    }


    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No es necesario hacer nada aqui.
    }

    
    private void EnEncontraronErrores(object elEnviador, EventArgs losArgumentos)
    {
      // Vacia las listas.
      miLista.Items.Clear();
      misPDIs.Clear();

      // Añade los PDIs.
      IDictionary<PDI, string> errores = ManejadorDeMapa.ManejadorDePDIs.Errores;
      foreach (KeyValuePair<PDI, string> error in errores)
      {
        PDI pdi = error.Key;
        string razón = error.Value;

        ListViewItem item = new ListViewItem(
          new string[] { 
                pdi.Número.ToString(),
                pdi.Tipo.ToString(), 
                pdi.Descripción,
                pdi.Nombre, 
                pdi.Coordenadas.ToString(),
                razón});
        miLista.Items.Add(item);
        misPDIs.Add(pdi);
      }

      // Actualiza la Pestaña.
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        int númeroDeErrores = miLista.Items.Count;
        pestaña.Text = "Errores (" + númeroDeErrores + ")";
      }
    }


    private void EnClick(object laLista, MouseEventArgs losArgumentosDelRatón)
    {
      // Obtiene el grupo seleccionado.
      ListView lista = (ListView)laLista;
      ListViewHitTestInfo información = lista.HitTest(losArgumentosDelRatón.Location);
      ListViewItem item = información.Item;
      PDI pdi = misPDIs[item.Index];

      // Pone el PDI para el menu de edición.
      miMenúEditorDePDI.PDI = pdi;

      // Busca el rango visible para el PDI.
      float margen = 0.0005f;
      RectangleF rectánguloVisible = new RectangleF(
        (float)pdi.Coordenadas.Longitud - margen,
        (float)pdi.Coordenadas.Latitud - margen,
        (2 * margen),
        (2 * margen));

      // Dibuja los PDIs como PDIs adicionales para resaltarlos.
      miMapa.PuntosAddicionales.Clear();
      miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
        pdi.Coordenadas, miPincelDePDI, 13));

      // Muestra el mapa en la region deseada.
      miMapa.Enabled = true;
      miMapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
      miMapa.MuestraTodoElMapa = false;
      miMapa.Refresh();
    }
    #endregion
  }
}
