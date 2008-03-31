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
    private ManejadorDeVías miManejadorDeVías;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando hay Vías con incongruencias.
    /// </summary>
    public event EventHandler<NúmeroDeElementosEventArgs> SeEncontraronIncongruencias;
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
        if (miManejadorDeVías != null)
        {
          miManejadorDeVías.CambiaronIncongruencias -= EnCambiaronIncongruencias;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;
        miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miManejadorDeVías = value.ManejadorDeVías;

          if (miManejadorDeVías != null)
          {
            miManejadorDeVías.CambiaronIncongruencias += EnCambiaronIncongruencias;
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
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.EditóVías += delegate(object elObjecto, EventArgs losArgumentos)
      {
        // Busca errores otra vez.
        miManejadorDeVías.BuscaErrores();
      };
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
      EnCambiaronIncongruencias(elEnviador, losArgumentos);
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No es necesario hacer nada aqui.
    }


    private void EnCambiaronIncongruencias(object elEnviador, EventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();

      // Genera el evento.
      if (SeEncontraronIncongruencias != null)
      {
        SeEncontraronIncongruencias(this, new NúmeroDeElementosEventArgs(miInterfaseListaConMapaDeVías.InterfaseListaDeVías.NúmeroDeElementos));
      }
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade las Vías.
      IList<IList<Vía>> incongruencias = miManejadorDeVías.Incongruencias;
      foreach (IList<Vía> vías in incongruencias)
      {
        foreach (Vía vía in vías)
        {
          laLista.AñadeItem(vía);
        }
      }
    }
    #endregion
  }
}
