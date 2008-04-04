using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Interfase de Vías con incongruencias.
  /// </summary>
  public partial class InterfaseDeVíasConIncongruencias : InterfaseBase
  {
    #region Campos
    private BuscadorDeIncongruencias miBuscadorDeIncongruencias;
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
        if (miBuscadorDeIncongruencias != null)
        {
          miBuscadorDeIncongruencias.Invalidado -= EnInvalidado;
          miBuscadorDeIncongruencias.Procesó -= EnSeBuscaronIncongruencias;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;
        miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDeIncongruencias = value.ManejadorDeVías.BuscadorDeIncongruencias;

          if (miBuscadorDeIncongruencias != null)
          {
            miBuscadorDeIncongruencias.Invalidado += EnInvalidado;
            miBuscadorDeIncongruencias.Procesó += EnSeBuscaronIncongruencias;
          }
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
        miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Constructor.
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDeVíasConIncongruencias()
    {
      InitializeComponent();

      // Pone el llenador de items.
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.PoneLlenadorDeItems(LlenaItems);

      // Escucha el evento de edición de Vías.
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Editó += delegate(object elObjecto, EventArgs losArgumentos)
      {
        // Busca errores otra vez.
        miBuscadorDeIncongruencias.Procesa();
      };
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void EnSeBuscaronIncongruencias(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<IList<BuscadorDeIncongruencias.ElementoDeIncongruencia>> incongruencias = miBuscadorDeIncongruencias.Incongruencias;
      foreach (IList<BuscadorDeIncongruencias.ElementoDeIncongruencia> elementosDeIncongruencia in incongruencias)
      {
        bool esElPrimerElemento = true;
        ListViewGroup grupo = null;
        foreach (BuscadorDeIncongruencias.ElementoDeIncongruencia elementoDeIncongruencia in elementosDeIncongruencia)
        {
          if (esElPrimerElemento)
          {
            grupo = new ListViewGroup(elementoDeIncongruencia.Vía.Nombre);
            laLista.Groups.Add(grupo);
            esElPrimerElemento = false;
          }

          // Si el elemento es un posible error entonces le ponemos un fondo amarillo.
          if (elementoDeIncongruencia.EsPosibleError)
          {
            laLista.AñadeItem(elementoDeIncongruencia.Vía, Color.Yellow, grupo, elementoDeIncongruencia.Detalle);
          }
          else
          {
            laLista.AñadeItem(elementoDeIncongruencia.Vía, grupo, elementoDeIncongruencia.Detalle);
          }
        }
      }
    }
    #endregion
  }
}
