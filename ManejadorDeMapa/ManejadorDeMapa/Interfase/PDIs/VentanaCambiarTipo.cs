using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Ventana para cambiar el tipo de PDI.
  /// </summary>
  public partial class VentanaCambiarTipo : Form
  {
    #region Campos
    private PDI miPDI;
    private Tipo miTipoOriginal;
    private Tipo miTipoNuevo;
    private string miError = null;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el tipo de PDI.
    /// </summary>
    /// <remarks>
    /// Si el tipo del PDI es cambiado en la interfase, el nuevo valor
    /// se guarda en ésta propiedad.
    /// </remarks>
    public PDI PDI
    {
      get
      {
        return miPDI;
      }

      set
      {
        miPDI = value;
        miTipoOriginal = miPDI.Tipo;

        // Pone el texto del PDI y el tipo original.
        miTextoNombrePDI.Text = miPDI.Nombre;
        miTextCoordenadasPDI.Text = miPDI.Coordenadas.ToString();
        miTextoTipoOriginal.Text = miTipoOriginal.ToString();
        miTextoDescripciónOriginal.Text = CaracterísticasDePDIs.Descripción(miTipoOriginal);

      }
    }


    /// <summary>
    /// Obtiene el tipo nuevo de PDI.
    /// </summary>
    /// <remarks>
    /// Si el tipo del PDI es cambiado en la interfase, el nuevo valor
    /// se guarda en ésta propiedad.
    /// </remarks>
    public Tipo TipoNuevo
    {
      get
      {
        return miTipoNuevo;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public VentanaCambiarTipo()
    {
      InitializeComponent();
    }
    #endregion

    #region Métodos Privados
    private void EnBotónCambiar(object sender, EventArgs e)
    {
      if (EsTipoVálido())
      {
        if (miTipoNuevo != miTipoOriginal)
        {
          DialogResult = DialogResult.OK;
        }
        else
        {
          DialogResult = DialogResult.Cancel;
        }

        Close();
      }
    }

    
    private void EnBotónCancelar(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel; 
      Close();
    }


    private void EnTipoNuevoCambió(object elEnviador, EventArgs losArgumentos)
    {
      // Borra el mensaje de error.
      miProveedorDeErrorDeTipo.SetError(miTextoTipoNuevo, string.Empty);

      // Si hay texto entonces tratamos de convertirlo a tipo.
      // Si no, entonces dejamos el tipo original.
      if (!string.IsNullOrEmpty(miEtiquetaTipoNuevo.Text))
      {
        string tipoComoTexto = "0x" + miTextoTipoNuevo.Text;
         
        // Tratamos de crear el tipo.
        try
        {
          miTipoNuevo = new Tipo(tipoComoTexto);
          miError = null;

          // Pone el texto de descripción.
          bool existe = CaracterísticasDePDIs.Descripciones.ContainsKey(miTipoNuevo);
          if (existe)
          {
            miTextoDescripción.Text = CaracterísticasDePDIs.Descripciones[miTipoNuevo];
          }
          else
          {
            miTextoDescripción.Text = "<desconocido>";
          }
        }
        // Si hay errores entonces ponemos el error.
        catch (Exception e)
        {
          miTextoDescripción.Text = "???";
          miError = "Tipo Inválido: " + e.Message;
        }
      }
      else
      {
        miTextoDescripción.Text = string.Empty;
        miTipoNuevo = miTipoOriginal;
      }
    }


    private bool EsTipoVálido()
    {
      bool tipoEsVálido = (miError == null);
      return tipoEsVálido;
    }


    private void TipoValidado(object sender, EventArgs e)
    {
      if (!EsTipoVálido())
      {
        miProveedorDeErrorDeTipo.SetError(miTextoTipoNuevo, miError);
      }
      else
      {
        miProveedorDeErrorDeTipo.SetError(miTextoTipoNuevo, string.Empty);
      }
    }
    #endregion
  }
}
