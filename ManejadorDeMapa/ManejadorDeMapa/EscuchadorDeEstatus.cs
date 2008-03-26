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
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Escuchador de Estatus.
  /// </summary>
  public class EscuchadorDeEstatus : IEscuchadorDeEstatus
  {
    #region Campos
    private readonly ToolStripStatusLabel miTextoDeEstatus;
    private readonly ToolStripProgressBar miBarraDeProgreso;
    private readonly ToolStripStatusLabel miTextoDeCoordenadas;
    private readonly Form miFormaPrincipal;
    private readonly string miTextoInicialDeLaFormaPrincipal;
    private string miArchivoActivo = string.Empty;
    private long miÚltimoProgreso = 0;
    private Coordenadas misCoordenadas = new Coordenadas(0, 0);
    private double miMinimaDiferenciaDeProgresoParaReportar = 1;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    public string Estatus
    {
      get
      {
        return miTextoDeEstatus.Text;
      }
      set
      {
        miTextoDeEstatus.Text = value;

        // Actualiza los componentes gráficos.
        Application.DoEvents();
      }
    }


    /// <summary>
    /// Obtiene o pone el texto del archivo activo de la aplicación.
    /// </summary>
    public string ArchivoActivo
    {
      get
      {
        return miArchivoActivo;
      }
      set
      {
        miArchivoActivo = value;
        miFormaPrincipal.Text = Path.GetFileName(miArchivoActivo)
          + " - " + miTextoInicialDeLaFormaPrincipal;
      }
    }


    /// <summary>
    /// Obtiene o pone las coordenadas.
    /// </summary>
    public Coordenadas Coordenadas
    {
      get
      {
        return misCoordenadas;
      }

      set
      {
        misCoordenadas = value;
        miTextoDeCoordenadas.Text = misCoordenadas.ToString();
        miTextoDeCoordenadas.Invalidate();
      }
    }


    /// <summary>
    /// Obtiene o pone el progreso.
    /// </summary>
    public long Progreso
    {
      get
      {
        return miBarraDeProgreso.Value;
      }
      set
      {
        // Nos aseguramos que el Control interno de la barra de 
        // progreso no sea nulo para prevenir una excepción cuando
        // el programa se cierra y todavía se está procesando algo.
        if (miBarraDeProgreso.ProgressBar == null)
        {
          return;
        }

        int progreso = (int)value;
        long diferencia = Math.Abs(progreso - miÚltimoProgreso);

        // El progreso se actualiza cuando:
        //   1) La diferencia es mayor que el minimo.  Con esto se evita
        //      actualizar muy seguido y con un cambio que no se nota.
        //   2) El progreso es cero.  Este es el caso cuando el usuario quiere
        //      que la barra de progreso se borre.
        if ((diferencia > miMinimaDiferenciaDeProgresoParaReportar)
          || (progreso == 0))
        {
          // Protege el progreso a mostrar en la Interfase en caso de
          // que mas de un elemento accese este objeto.
          miBarraDeProgreso.Value = Math.Min(progreso, (int)ProgresoMáximo); ;
          miÚltimoProgreso = progreso;

          // Habilitar la barra de progreso si el progreso es mayor que cero.
          if (progreso > 0)
          {
            if (!miBarraDeProgreso.Enabled)
            {
              miBarraDeProgreso.Enabled = true;
            }
          }
          else
          {
            miBarraDeProgreso.Enabled = false;
          }

          // Actualiza los componentes gráficos.
          miBarraDeProgreso.Invalidate();
        }

      }
    }


    /// <summary>
    /// Obtiene o pone el progreso máximo.
    /// </summary>
    public long ProgresoMáximo
    {
      get
      {
        return miBarraDeProgreso.Maximum;
      }
      set
      {
        miBarraDeProgreso.Maximum = (int)value;

        // Calcular la minima diferencia para actualizar de manera
        // que la actualización sea en intervalos que muestren un
        // cambio visible en la barra de progreso.
        miMinimaDiferenciaDeProgresoParaReportar = value / 200;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="laFormaPrincipal">La Forma principal.</param>
    /// <param name="elComponenteDelTextoDeEstatus">El componente del Texto de Estatus.</param>
    /// <param name="elComponenteDeLaBarraDeProgreso">El componente de la Barra de Progreso.</param>
    /// <param name="elComponenteDelTextoDeCoordenadas">El componente del Texto de Coordenadas.</param>
    public EscuchadorDeEstatus(
      Form laFormaPrincipal,
      ToolStripStatusLabel elComponenteDelTextoDeEstatus,
      ToolStripProgressBar elComponenteDeLaBarraDeProgreso,
      ToolStripStatusLabel elComponenteDelTextoDeCoordenadas)
    {
      miFormaPrincipal = laFormaPrincipal;
      miTextoInicialDeLaFormaPrincipal = miFormaPrincipal.Text;
      miTextoDeEstatus = elComponenteDelTextoDeEstatus;
      miBarraDeProgreso = elComponenteDeLaBarraDeProgreso;
      miTextoDeCoordenadas = elComponenteDelTextoDeCoordenadas;

      // Siempre el progreso empieza en zero.
      miBarraDeProgreso.Minimum = 0;

      // Desabilitar la barra de progreso al comienzo.
      miBarraDeProgreso.Enabled = false;
    }
    #endregion
  }
}
