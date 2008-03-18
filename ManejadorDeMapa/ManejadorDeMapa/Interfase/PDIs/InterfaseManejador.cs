using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  public partial class InterfaseManejador : InterfaseBase
  {
    #region Campos
    private List<ListViewItem> misItemsDeLista = new List<ListViewItem>();
    private readonly InterfaseBase[] misInterfases;
    private const string FormatoDeCoordenada = "0.00000";
    private readonly NumberFormatInfo miFormatoNumérico = new NumberFormatInfo();
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public override ManejadorDeMapa ManejadorDeMapa
    {
      set
      {
        base.ManejadorDeMapa = value;

        foreach (InterfaseBase interfaseBase in misInterfases)
        {
          interfaseBase.ManejadorDeMapa = value;
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

        foreach (InterfaseBase interfaseBase in misInterfases)
        {
          interfaseBase.EscuchadorDeEstatus = value;
        }
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseManejador()
    {
      InitializeComponent();

      // Usar el punto para separar decimales.
      miFormatoNumérico.NumberDecimalSeparator = ".";

      // Crea el vector de interfases.
      misInterfases = new InterfaseBase[] {
        miInterfaseDeMapa,
        miInterfasePDIsDuplicados,
        miInterfasePDIsEliminados,
        miInterfasePDIsErrores,
        miInterfasePDIsModificados};

      // Asignar las propiedades correspondientes.
      miInterfasePDIsDuplicados.Tag = miPáginaPosiblesDuplicados;
      miInterfasePDIsEliminados.Tag = miPáginaEliminados;
      miInterfasePDIsModificados.Tag = miPáginaModificados;
      miInterfasePDIsErrores.Tag = miPáginaErrores;
    }
    #endregion

    #region Métodos Privados
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnElementosModificados(elEnviador, losArgumentos);
    }


    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Vacia la lista.
      misItemsDeLista.Clear();

      // Añade los elementos.
      IList<ElementoDelMapa> elementosDelMapa = ManejadorDeMapa.Elementos;
      misItemsDeLista.Capacity = elementosDelMapa.Count;
      foreach (ElementoDelMapa elementoDelMapa in elementosDelMapa)
      {
        if (elementoDelMapa is PDI)
        {
          PDI puntoDeInterés = (PDI)elementoDelMapa;

          // Añade el PDI a la lista.
          ListViewItem itemParaLaListaDePDIs = new ListViewItem(
            new string[] { 
            puntoDeInterés.Número.ToString(),
            puntoDeInterés.Tipo.ToString(), 
            puntoDeInterés.Descripción,
            puntoDeInterés.Nombre, 
            puntoDeInterés.Coordenadas.Latitud.ToString(FormatoDeCoordenada, miFormatoNumérico),
            puntoDeInterés.Coordenadas.Longitud.ToString(FormatoDeCoordenada, miFormatoNumérico)},
              -1);
          misItemsDeLista.Add(itemParaLaListaDePDIs);
        }
      }

      // Pone el número de elementos virtuales.
      miLista.VirtualListSize = misItemsDeLista.Count;

      // Actualiza las Pestañas.
      miPáginaDeTodos.Text = "Todos (" + miLista.VirtualListSize + ")";
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        pestaña.Text = "PDIs (" + miLista.VirtualListSize + ")";
      }
    }

    private void ObtieneItemDeListaDePDIs(object elEnviador, RetrieveVirtualItemEventArgs elArgumento)
    {
      elArgumento.Item = misItemsDeLista[elArgumento.ItemIndex];
    }
    #endregion
  }
}
