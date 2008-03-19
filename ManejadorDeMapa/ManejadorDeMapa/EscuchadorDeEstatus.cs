#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
