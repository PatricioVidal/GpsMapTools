using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Ordenador de columnas para Listas tipo <see cref="ListView"/>.
  /// </summary>
  public partial class OrdenadorDeColumnaDeLista : Component
  {
    #region Campos
    private readonly ComparadorDeItems miComparadorDeItems = new ComparadorDeItems();
    #endregion

    #region Clases
    private class ComparadorDeItems : IComparer, IComparer<ListViewItem>
    {
      #region Campos
      private int miColumnaAOrdenar = -1;
      private ListView miLista = null;
      private List<ListViewItem> misItemsDeLaListaVirtual = null;
      #endregion

      #region Propiedades
      /// <summary>
      /// Obtiene o pone los items.
      /// </summary>
      /// <remarks>
      /// Esto solo es necesario para listas virtuales.
      /// </remarks>
      public List<ListViewItem> ItemsDeLaListaVirtual
      {
        get
        {
          return misItemsDeLaListaVirtual;
        }

        set
        {
          misItemsDeLaListaVirtual = value;
        }
      }


      /// <summary>
      /// Obtiene o pone la lista a ordenar.
      /// </summary>
      public ListView Lista
      {
        get
        {
          return miLista;
        }

        set
        {
          if (miLista != null)
          {
            miLista.ColumnClick -= EnClickDeLaColumna;
          }

          miLista = value;

          if (miLista != null)
          {
            if (miLista.VirtualMode)
            {
              miLista.ColumnClick += EnClickDeLaColumna;
            }
            else
            {
              miLista.ListViewItemSorter = this;
              miLista.ColumnClick += EnClickDeLaColumna;
            }
          }
        }
      }
      #endregion

      #region Métodos Públicos
      public int Compare(object elPrimerObjeto, object elSegundoObjecto)
      {
        ListViewItem primerItem = (ListViewItem)elPrimerObjeto;
        ListViewItem segundoItem = (ListViewItem)elSegundoObjecto;

        return Compare(primerItem, segundoItem);
      }

      public int Compare(ListViewItem elPrimerItem, ListViewItem elSegundoItem)
      {
        // No ordenamos hasta que el usuario click en una columna.
        if (miColumnaAOrdenar < 0)
        {
          return 0;
        }

        // Compara los texto de la columna a ordenar.
        int comparasión = String.Compare(
          elPrimerItem.SubItems[miColumnaAOrdenar].Text,
          elSegundoItem.SubItems[miColumnaAOrdenar].Text);

        // El signo del resultado depende de como queremos ordenar la lista.
        int resultado = 0;
        switch (miLista.Sorting)
        {
          case SortOrder.Ascending:
            resultado = comparasión;
            break;
          case SortOrder.Descending:
            resultado = -comparasión;
            break;
        }

        return resultado;
      }
      #endregion

      #region Métodos Privados
      private void EnClickDeLaColumna(object elEnviador, ColumnClickEventArgs losArgumentos)
      {
        int columnaSeleccionada = losArgumentos.Column;

        // Si es la misma columna entonces cambiamos el sentido del orden.
        // Si no, entonces ordemans de menor a mayor.
        if (miColumnaAOrdenar == columnaSeleccionada)
        {
          // Cambiamos el sentido.
          switch (miLista.Sorting)
          {
            case SortOrder.Ascending:
              miLista.Sorting = SortOrder.Descending;
              break;
            case SortOrder.Descending:
              miLista.Sorting = SortOrder.Ascending;
              break;
          }
        }
        else
        {
          miColumnaAOrdenar = columnaSeleccionada;
          miLista.Sorting = SortOrder.Ascending;
        }

        // Ordena la lista.
        if (miLista.VirtualMode)
        {
          if (misItemsDeLaListaVirtual == null)
          {
            throw new InvalidOperationException("Para Listas Virtuales se tiene que pones la propiedad ItemsDeLaListaVirtual.");
          }

          misItemsDeLaListaVirtual.Sort(this);
          miLista.Refresh();
        }
        else
        {
          miLista.Sort();
        }
      }
      #endregion
    }
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la lista a ordenar.
    /// </summary>
    [Browsable(true)]
    [Description("Lista a Ordenar.")]
    public ListView Lista
    {
      get
      {
        return miComparadorDeItems.Lista;
      }

      set
      {
        miComparadorDeItems.Lista = value;
      }
    }


    /// <summary>
    /// Obtiene o pone los items.
    /// </summary>
    /// <remarks>
    /// Esto solo es necesario para listas virtuales.
    /// </remarks>
    [Browsable(true)]
    [Description("Items de la lista virtual.")]
    public List<ListViewItem> ItemsDeLaListaVirtual
    {
      get
      {
        return miComparadorDeItems.ItemsDeLaListaVirtual;
      }

      set
      {
        miComparadorDeItems.ItemsDeLaListaVirtual = value;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrdenadorDeColumnaDeLista()
    {
      InitializeComponent();
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="container">Contenedor.</param>
    public OrdenadorDeColumnaDeLista(IContainer container)
    {
      container.Add(this);

      InitializeComponent();
    }
    #endregion
  }
}
