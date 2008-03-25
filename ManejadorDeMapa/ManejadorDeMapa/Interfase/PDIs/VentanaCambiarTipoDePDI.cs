#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
// (For English, see further down.)
//
// GpsYv.ManejadorDeMapa es una aplicación para manejar Mapas de GPS en el
// formato Polish (.mp).  Esta escrito en C# usando el .NET Framework 3.5. 
//
// Esta programa nació por la necesidad del Grupo GPS de Venezuela, 
// GPS_YV (http://www.gpsyv.net), de analizar y corregir los mapas que el
// grupo genera para la comunidad.  GpsYv.ManejadorDeMapa se distribuye bajo 
// la licencia GPL con la finalidad de que sea útil para otros grupos o
// individuos que hacen mapas, y también para promover la colaboración 
// con este proyecto.
//
// Visita http://www.codeplex.com/GPSYVManejadorDeMapa para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Programador: Patricio Vidal (PatricioV2@hotmail.com)
//
// Este programa es software libre. Puede redistribuirlo y/o modificarlo
// bajo los términos de la Licencia Pública General de GNU según es publicada
// por la Free Software Foundation, bien de la versión 2 de dicha Licencia o 
// bien (según su elección) de cualquier versión posterior. 
//
// Este programa se distribuye con la esperanza de que sea útil, 
// pero SIN NINGUNA GARANTÍA, incluso sin la garantía MERCANTIL
// implícita o sin garantizar la CONVENIENCIA PARA UN PROPÓSITO PARTICULAR.
// Véase la Licencia Pública General de GNU para más detalles. 
//
// Debería haber recibido una copia de la Licencia Pública General 
// junto con este programa. Si no ha sido así, escriba a la 
// Free Software Foundation, Inc., en 675 Mass Ave, 
// Cambridge, MA 02139, EEUU.
//
//-----------------------------------------------------------------------------
//
// GpsYv.ManejadorDeMapa (GPS Map Manager) is an application to Manage 
// GPS Maps in Polish format (.mp).  It is written in C# using the 
// .NET Framework 3.5.
//
// This program was born by the need of the GPS Group of Venezuela,
// GPS_YV (http://www.gpsyv.net), to analyze and fix the maps that
// the group generates for the community. GpsYv.ManejadorDeMapa is 
// distributed under the GPL license with the purpose that it could 
// be useful for other groups or individuals that create maps, and 
// also to promote the collaboration with this project.
//
// Visit http://www.codeplex.com/GPSYVManejadorDeMapa for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Programmer: Patricio Vidal (PatricioV2@hotmail.com)
//
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
#endregion

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
  /// Ventana para cambiar el tipo de PDI.
  /// </summary>
  public partial class VentanaCambiarTipoDePDI : Form
  {
    #region Campos
    private IList<PDI> misPDIs = null;
    private Tipo miTipoNuevo = Tipo.TipoNulo;
    private string miError = null;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone loa lista de PDIs.
    /// </summary>
    public IList<PDI> PDIs
    {
      get
      {
        return misPDIs;
      }

      set
      {
        if (value != null)
        {
          misPDIs = value;
          miTipoNuevo = Tipo.TipoNulo;

          // Pone el texto del PDI y el tipo original.
          if (misPDIs.Count == 1)
          {
            // Un solo PDI.
            // Muestra los atributos.
            PDI pdi = misPDIs[0];
            miTextoNombrePDI.Text = pdi.Nombre;
            miTextoCoordenadasPDI.Text = pdi.Coordenadas.ToString();
            miTextoTipoOriginal.Text = pdi.Tipo.ToString();
            miTextoDescripciónOriginal.Text = CaracterísticasDePDIs.Descripción(pdi.Tipo);
          }
          else if (misPDIs.Count > 1)
          {
            // Multiple PDIs.
            miTextoNombrePDI.Text = "<Múltiple PDIs>";
            miTextoCoordenadasPDI.Text = string.Empty;

            // Buscar si tiene un tipo único.
            Tipo tipoÚnico = misPDIs[0].Tipo;
            bool tieneTipoÚnico = true;
            foreach (PDI pdi in misPDIs)
            {
              if (pdi.Tipo != tipoÚnico)
              {
                tieneTipoÚnico = false;
                tipoÚnico = Tipo.TipoNulo;
                break;
              }
            }

            // Muestra las características del tipo solo si es único.
            if (tieneTipoÚnico)
            {
              miTextoTipoOriginal.Text = tipoÚnico.ToString();
              miTextoDescripciónOriginal.Text = CaracterísticasDePDIs.Descripción(tipoÚnico);
            }
            else
            {
              miTextoTipoOriginal.Text = "....";
              miTextoDescripciónOriginal.Text = "<Múltiple tipos>";
            }
          }
          else
          {
            Inicializa();
          }
        }
        else
        {
          Inicializa();
        }
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
    public VentanaCambiarTipoDePDI()
    {
      InitializeComponent();
    }
    #endregion

    #region Métodos Privados
    private void EnBotónCambiar(object sender, EventArgs e)
    {
      if (EsTipoVálido())
      {
        if (miTipoNuevo != Tipo.TipoNulo)
        {
          DialogResult = DialogResult.OK;
        }
        else
        {
          DialogResult = DialogResult.Cancel;
        }

        // Cierra la ventana.
        Close();
      }
    }


    private void Inicializa()
    {
      // Inicializa los campos.
      misPDIs = null;
      miTipoNuevo = Tipo.TipoNulo;
      miError = null;

      // Inicializa la interfase.
      miTextoNombrePDI.Text = "<No hay PDIs seleccionados>";
      miTextoCoordenadasPDI.Text = string.Empty;
      miTextoTipoOriginal.Text = string.Empty;
      miTextoDescripciónOriginal.Text = string.Empty;
    }

    
    private void EnBotónCancelar(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel; 
      Close();
    }


    private void EnTipoNuevoCambió(object elEnviador, EventArgs losArgumentos)
    {
      // Borra el mensaje de error.
      miError = null;
      miProveedorDeErrorDeTipo.SetError(miTextoTipoNuevo, string.Empty);

      // Si hay texto entonces tratamos de convertirlo a tipo.
      // Si no, entonces dejamos el tipo original.
      if (!string.IsNullOrEmpty(miTextoTipoNuevo.Text))
      {
        string tipoComoTexto = "0x" + miTextoTipoNuevo.Text;
         
        // Tratamos de crear el tipo.
        try
        {
          miTipoNuevo = new Tipo(tipoComoTexto);

          // Pone el texto de descripción.
          bool existe = CaracterísticasDePDIs.Descripciones.ContainsKey(miTipoNuevo);
          if (existe)
          {
            miTextoDescripción.Text = CaracterísticasDePDIs.Descripciones[miTipoNuevo];
          }
          else
          {
            miTextoDescripción.Text = "<desconocido>";
            miError = "Tipo Desconocido." ;
          }
        }
        // Si hay errores entonces ponemos el error.
        catch (Exception e)
        {
          miTextoDescripción.Text = "???";
          miError = "Tipo Inválido: " + e.Message;
          miProveedorDeErrorDeTipo.SetError(miTextoTipoNuevo, miError);
        }
      }
      else
      {
        miTextoDescripción.Text = string.Empty;
        miTipoNuevo = Tipo.TipoNulo;
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
