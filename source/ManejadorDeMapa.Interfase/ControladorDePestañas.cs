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

    #region Métodos
    /// <summary>
    /// Constructor.
    /// </summary>
    public ControladorDePestañas()
    {
      InitializeComponent();
    }


    /// <summary>
    /// Actualiza la pestaña en una página dada.
    /// </summary>
    /// <param name="laPágina">La página dada.</param>
    /// <param name="elNúmeroDeItems">El número de items.</param>
    /// <param name="elEstadoPositivo">El estado a poner cuando es positivo.</param>
    public void ActualizaPestaña(
      TabPage laPágina,
      int elNúmeroDeItems,
      EstadoDePestaña elEstadoPositivo)
    {
      // Cambia el texto de la pestaña.
      laPágina.Text = string.Format("{0} ({1})", TextoBase(laPágina), elNúmeroDeItems);

      // Si el número de items es positivo entonces cambia
      // el estado de la pestaña al estado positivo.
      // Si no, entonces cambia el estado de la pestaña a estado Bíen.
      if (elNúmeroDeItems > 0)
      {
        PoneEstadoDePestaña(
          laPágina,
          elEstadoPositivo);
      }
      else
      {
        if (elEstadoPositivo == EstadoDePestaña.Nada)
        {
          PoneEstadoDePestaña(
            laPágina,
            ControladorDePestañas.EstadoDePestaña.Nada);
        }
        else
        {
          PoneEstadoDePestaña(
            laPágina,
            ControladorDePestañas.EstadoDePestaña.Bién);
        }
      }
    }


    /// <summary>
    /// Pone el estado de la pestaña en una página dada.
    /// </summary>
    /// <param name="laPágina">La página dada.</param>
    /// <param name="elEstado">El estado a poner.</param>
    public void PoneEstadoDePestaña(TabPage laPágina, EstadoDePestaña elEstado)
    {
      laPágina.ImageIndex = (int)elEstado;

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


    /// <summary>
    /// Invalida la pestaña de una página dada.
    /// </summary>
    /// <param name="laPágina">La página dada.</param>
    public void InvalidaPestaña(TabPage laPágina)
    {
      // Pone las pestañas en estado de "No Sé" para indicar que
      // no se sabe si hay errores.
      PoneEstadoDePestaña(
        laPágina,
        ControladorDePestañas.EstadoDePestaña.NoSé);

      // Cambia el texto de la pestaña.
      laPágina.Text = TextoBase(laPágina);
    }
    #endregion

    #region Métodos Privados
    private string TextoBase(TabPage laPágina)
    {
      // Busca el texto base guiandose por el primer parentesis.
      string textoBase = null;
      int indiceParentesis = laPágina.Text.IndexOf(" (");
      if (indiceParentesis > 0)
      {
        textoBase = laPágina.Text.Substring(0, indiceParentesis);
      }
      else
      {
        textoBase = laPágina.Text;
      }

      return textoBase;
    }
    #endregion
  }
}
