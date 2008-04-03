using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Controlador de Pestañas.
  /// </summary>
  public partial class ControladorDePestañas : TabControl
  {
    #region Campos
    private EstadoDePestaña miEstadoMáximoDePestañas = EstadoDePestaña.Nada;
    #endregion

    #region Clases
    /// <summary>
    /// Argumento de evento Cambió Estado Máximo de Pestañas.
    /// </summary>
    public class CambióEstadoMáximoDePestañasEventArgs : EventArgs
    {
      /// <summary>
      /// Obtiene el estado máximo de las pestañas.
      /// </summary>
      public readonly EstadoDePestaña EstadoMáximoDePestañas;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="elEstadoMáximo">El estado máximo de las pestañas.</param>
      public CambióEstadoMáximoDePestañasEventArgs(EstadoDePestaña elEstadoMáximo)
      {
        EstadoMáximoDePestañas = elEstadoMáximo;
      }
    }
    #endregion

    #region Enumeraciones
    /// <summary>
    /// Estado de la Pestaña.
    /// </summary>
    public enum EstadoDePestaña
    {
      /// <summary>
      /// Sin estado.
      /// </summary>
      Nada = -1,

      /// <summary>
      /// Bién.
      /// </summary>
      Bién,

      /// <summary>
      /// No sé.
      /// </summary>
      NoSé,

      /// <summary>
      /// Alerta.
      /// </summary>
      Alerta,

      /// <summary>
      /// Error.
      /// </summary>
      Error
    }
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando cambió el estado máximo de las Pestañas.
    /// </summary>
    public event EventHandler<CambióEstadoMáximoDePestañasEventArgs> CambióEstadoMáximoDePestañas;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public ControladorDePestañas()
    {
      InitializeComponent();
    }


    /// <summary>
    /// Pone el estado de un pestaña.
    /// </summary>
    /// <param name="elIndice">El índice de la Pestaña.</param>
    /// <param name="elEstado">El estado a poner.</param>
    public void PoneEstadoDePestaña(int elIndice, EstadoDePestaña elEstado)
    {
      TabPages[elIndice].ImageIndex = (int)elEstado;

      // Calcula el estado máximo de las pestañas.
      int estadoMáximo = (int)EstadoDePestaña.Nada;
      foreach (TabPage pestaña in TabPages)
      {
        // El estado de la pestaña está representado por el índice de 
        // la imágen.
        int estadoDeLaPestaña = pestaña.ImageIndex;
        if (estadoDeLaPestaña > estadoMáximo)
        {
          estadoMáximo = estadoDeLaPestaña;
        }
      }

      // Lanza el evento si el estado máximo cambió.
      if (estadoMáximo != (int)miEstadoMáximoDePestañas)
      {
        miEstadoMáximoDePestañas = (EstadoDePestaña)estadoMáximo;
        if (CambióEstadoMáximoDePestañas != null)
        {
          CambióEstadoMáximoDePestañas(this, new CambióEstadoMáximoDePestañasEventArgs(miEstadoMáximoDePestañas));
        }
      }
    }
    #endregion
  }
}
