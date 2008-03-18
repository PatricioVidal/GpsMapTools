using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Menú para editar PDIs.
  /// </summary>
  public partial class MenuEditorDePDI : ContextMenuStrip
  {
    #region Campos
    private PDI miPDI = null;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el PDI a editar.
    /// </summary>
    public PDI PDI
    {
      get
      {
        return miPDI;
      }

      set
      {
        miPDI = value;

        if (miPDI != null)
        {
          foreach (ToolStripMenuItem menu in Items)
          {
            menu.Enabled = true;
          }
        }
        else
        {
          foreach (ToolStripMenuItem menu in Items)
          {
            menu.Enabled = false;
          }
        }
      }
    }


    /// <summary>
    /// Obtiene o pone el manejador de PDIs.
    /// </summary>
    public ManejadorDePDIs ManejadorDePDIs
    {
      get;
      set;
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public MenuEditorDePDI()
    {
      InitializeComponent();

      // Inicialización.
      Name = "miMenuDeContexto";
      Size = new System.Drawing.Size(153, 48);

      // Añade los menús.
      AddMenuParaCambiarTipo();
    }
    #endregion

    #region Métodos Privados
    private void AddMenuParaCambiarTipo()
    {
      ToolStripMenuItem menuCambiarTipo = new ToolStripMenuItem();
      menuCambiarTipo.Text = "Cambiar Tipo";
      menuCambiarTipo.Enabled = false;
      Items.Add(menuCambiarTipo);

      menuCambiarTipo.Click += delegate(object elObjecto, EventArgs losArgumentos)
      {
        VentanaCambiarTipo ventanaCambiarTipo = new VentanaCambiarTipo();
        ventanaCambiarTipo.PDI = miPDI;
        DialogResult resultado = ventanaCambiarTipo.ShowDialog();
        if (resultado == DialogResult.OK)
        {
          // Cambia el tipo.
          PDI.CambiaTipo(ventanaCambiarTipo.TipoNuevo, "Cambio Manual");

          // Busca los errores de nuevo.
          ManejadorDePDIs.BuscaErrores();
        }
      };
    }
    #endregion
  }
}
