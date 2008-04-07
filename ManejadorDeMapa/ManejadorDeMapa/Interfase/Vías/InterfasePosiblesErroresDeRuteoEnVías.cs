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
  public partial class InterfasePosiblesErroresDeRuteoEnVías : InterfaseBase
  {
    #region Campos
    private BuscadorDePosiblesErroresDeRuteo miBuscadorDePosiblesErroresDeRuteo;
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
        if (miBuscadorDePosiblesErroresDeRuteo != null)
        {
          miBuscadorDePosiblesErroresDeRuteo.Invalidado -= EnInvalidado;
          miBuscadorDePosiblesErroresDeRuteo.Procesó -= EnSeBuscaronIncongruencias;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;
        //miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDePosiblesErroresDeRuteo = value.ManejadorDeVías.BuscadorDePosiblesErroresDeRuteo;

          if (miBuscadorDePosiblesErroresDeRuteo != null)
          {
            miBuscadorDePosiblesErroresDeRuteo.Invalidado += EnInvalidado;
            miBuscadorDePosiblesErroresDeRuteo.Procesó += EnSeBuscaronIncongruencias;
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
        //miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Constructor.
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfasePosiblesErroresDeRuteoEnVías()
    {
      InitializeComponent();

      // Pone el llenador de items.
      //miInterfaseListaConMapaDeVías.InterfaseListaDeVías.PoneLlenadorDeItems(LlenaItems);

      // Escucha el evento de edición de Vías.
      //miInterfaseListaConMapaDeVías.MenuEditorDeVías.Editó += delegate(object elObjecto, EventArgs losArgumentos)
      //{
      //  // Busca errores otra vez.
      //  miBuscadorDePosiblesErroresDeRuteo.Procesa();
      //};
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      //miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void EnSeBuscaronIncongruencias(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      //miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IDictionary<Vía, IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo>> erroresDeRuteo = miBuscadorDePosiblesErroresDeRuteo.Incongruencias;
      foreach (KeyValuePair<Vía, IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo>> ítem in erroresDeRuteo)
      {
        // Crea el grupo.
        Vía vía = ítem.Key;
        ListViewGroup grupo = new ListViewGroup(vía.Nombre);
        laLista.Groups.Add(grupo);

        // Añade los elementos de las incongruencia a la lista.
        IList<BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo> elementosDePosibleErrorDeRuteo = ítem.Value;
        foreach (BuscadorDePosiblesErroresDeRuteo.ElementoDePosibleErrorDeRuteo elementoDePosibleErrorDeRuteo in elementosDePosibleErrorDeRuteo)
        {
          // Si el elemento es un posible error entonces le ponemos un fondo amarillo.
          if (elementoDePosibleErrorDeRuteo.EsPosibleError)
          {
            laLista.AñadeItem(elementoDePosibleErrorDeRuteo.Vía, Color.PaleGoldenrod, grupo, elementoDePosibleErrorDeRuteo.Detalle);
          }
          else
          {
            laLista.AñadeItem(elementoDePosibleErrorDeRuteo.Vía, grupo, elementoDePosibleErrorDeRuteo.Detalle);
          }
        }
      }
    }
    #endregion
  }
}
