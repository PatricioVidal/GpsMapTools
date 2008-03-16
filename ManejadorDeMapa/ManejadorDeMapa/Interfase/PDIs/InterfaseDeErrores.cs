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
  public partial class InterfaseDeErrores : InterfaseBase
  {
    #region Campos
    private ManejadorDePDIs miManejadorDePDIs;
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
        }
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
      // Vacia las lista.
      miLista.Items.Clear();

      // Añade los PDIs.
      IDictionary<PDI, string> errores = ManejadorDeMapa.ManejadorDePDIs.Errores;
      foreach (KeyValuePair<PDI, string> error in errores)
      {
        PDI pdi = error.Key;
        string razón = error.Value;

        ListViewItem item = new ListViewItem(
          new string[] { 
                pdi.Número.ToString(),
                pdi.TipoComoTexto(), 
                pdi.Descripción,
                pdi.Nombre, 
                razón});
        miLista.Items.Add(item);
      }

      // Actualiza la Pestaña.
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        int númeroDeErrores = miLista.Items.Count;
        pestaña.Text = "Errores (" + númeroDeErrores + ")";
      }
    }
    #endregion
  }
}
