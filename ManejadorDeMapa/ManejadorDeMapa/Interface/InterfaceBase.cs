using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interface
{
  public partial class InterfaceBase : UserControl
  {
    #region Campos
    private ManejadorDeMapa miManejadorDeMapa;
    private IEscuchadorDeEstatus miEscuchadorDeEstatus;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public virtual ManejadorDeMapa ManejadorDeMapa
    {
      get
      {
        return miManejadorDeMapa;
      }

      set
      {
        // Deja de manejar los eventos de modificación de elementos.
        if (miManejadorDeMapa != null)
        {
          miManejadorDeMapa.MapaNuevo -= EnMapaNuevo;
          miManejadorDeMapa.ElementosModificados -= EnElementosModificados;
        }

        // Pone el nuevo manejador de mapa.
        miManejadorDeMapa = value;

        // Maneja eventos de modificación de elementos.
        if (miManejadorDeMapa != null)
        {
          miManejadorDeMapa.MapaNuevo += EnMapaNuevo;
          miManejadorDeMapa.ElementosModificados += EnElementosModificados;
        }
      }
    }


    /// <summary>
    /// Obtiene o pone el escuchador de estatus.
    /// </summary>
    public virtual IEscuchadorDeEstatus EscuchadorDeEstatus
    {
      get
      {
        return miEscuchadorDeEstatus;
      }

      set
      {
        miEscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaceBase()
    {
      InitializeComponent();
    }
    #endregion

    #region Métodos Protegidos
    protected virtual void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      throw new InvalidOperationException(this.GetType() + " tiene que implementar método EnMapaNuevo(...)");
    }

    protected virtual void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      throw new InvalidOperationException(this.GetType() + " tiene que implementar método EnElementosModificados(...)");
    }
    #endregion
  }
}
