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
using System.Timers;
using System.Windows.Input;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Clase base para procesadores de elementos.
  /// </summary>
  public abstract class ProcesadorBase<T,K> : ICommand
    where T: ManejadorBase<K>
    where K: ElementoDelMapa
  {
    #region Campos
    private readonly T miManejador;
    private readonly string miNombreDeElemento;
    private readonly Timer miTimerParaReportarEstatus = new Timer(25);
    private bool miReportaEstatus = true;
    private static bool miEstáProcesando;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando el estado del procesador puede estar inválido.
    /// </summary>
    /// <remarks>
    /// Esto puede pasar cuando por ejemplo se modifican elementos.
    /// </remarks>
    public event EventHandler Invalidado;

    
    /// <summary>
    /// Evento cuando se comienza el procesamiento.
    /// </summary>
    public event EventHandler Procesando;


    /// <summary>
    /// Evento cuando se completa el procesamiento.
    /// </summary>
    public event EventHandler<NúmeroDeItemsEventArgs> Procesó;
    #endregion

    #region Propiedades

    /// <summary>
    /// Obtiene el manejador de mapa.
    /// </summary>
    protected ManejadorDeMapa ManejadorDeMapa { get; set; }


    /// <summary>
    /// Devuelve el número de ítem detectados.
    /// </summary>
    public int NúmeroDeProblemasDetectados { get; private set; }


    /// <summary>
    /// Obtiene el número de elementos.
    /// </summary>
    public int NúmeroDeElementos { get; private set; }


    /// <summary>
    /// Obtiene el índice del elemento procesándose.
    /// </summary>
    protected int IndiceDeElementoProcesándose { get; set; }


    /// <summary>
    /// Indexador por número índice.
    /// </summary>
    /// <param name="elIndice">El número índice dado.</param>
    /// <returns>El elemento el el índice dado.</returns>
    protected K this[int elIndice]
    {
      get
      {
        return miManejador.Elementos[elIndice];
      }
    }

    /// <summary>
    /// Gets the status listener.
    /// </summary>
    protected IEscuchadorDeEstatus EscuchadorDeEstatus { get; set; }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejador">El manejador.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    protected ProcesadorBase(
      T elManejador,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
    {
      miManejador = elManejador;
      NúmeroDeElementos = miManejador.Elementos.Count;
      ManejadorDeMapa = elManejador.ManejadorDeMapa;
      EscuchadorDeEstatus = elEscuchadorDeEstatus;

      // Pone el manejador de eventos del timer.
      miTimerParaReportarEstatus.Elapsed += EnTimerElapsed;
      
      // Escucha eventos.
      ManejadorDeMapa.MapaNuevo += EnMapaNuevo;
      miManejador.ElementosModificados += EnElementosModificados;

      // Genera el nombre del elemento.
      // El nombre completo es de la forma GpsYv.ManejadorDeMapa.<Nombre>
      // asi que tenemos que extraer la última palabra.
      string nombreCompleto = typeof(K).ToString();
      string[] palabras = nombreCompleto.Split('.');
      int últimoIndice = palabras.Length - 1;
      miNombreDeElemento = palabras[últimoIndice];
    }


    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="elParámetro">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object elParámetro)
    {
      Procesa();
    }


    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <returns>
    /// true if this command can be executed; otherwise, false.
    /// </returns>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    public bool CanExecute(object parameter)
    {
      return true;
    }


    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged;


    /// <summary>
    /// Procesa todos los elementos.
    /// </summary>
    /// <returns>El número de problemas detectados.</returns>
    public int Procesa()
    {
      // Crea un filtro que muestre todos los elementos.
      int númeroDeElementos = miManejador.Elementos.Count; 
      bool[] filtro = new bool[númeroDeElementos];
      for (int i = 0; i < númeroDeElementos; ++i)
      {
        filtro[i] = true;
      }

      return Procesa(filtro);
    }


    /// <summary>
    /// Procesa los elementos seleccionados por un filtro dado..
    /// </summary>
    /// <param name="elFiltroEstáSeleccionado">El filtro dado.</param>
    /// <returns>El número de problemas detectados.</returns>
    public int Procesa(bool[] elFiltroEstáSeleccionado)
    {
      // Nos salimos si estamos procesando algo.
      if (miEstáProcesando)
      {
        return 0;
      }

      // Indica que se van a procesar los elementos.
      bool go = ComenzóAProcesar();

      // Exit early if there is a cancellation.
      if (!go)
      {
        return 0;
      }

      using (ManejadorDeMapa.IndicaProcesoExclusivo())
      {
        // Envia evento.
        if (Procesando != null)
        {
          Procesando(this, new EventArgs());
        }

        // Indica que estamos procesando.
        miEstáProcesando = true;

        NúmeroDeProblemasDetectados = 0;
        IList<K> elementos = miManejador.Elementos;
        NúmeroDeElementos = elementos.Count;
        int númeroDeElementosAProcesar = 0;
        foreach (bool estáSeleccionado in elFiltroEstáSeleccionado)
        {
          if (estáSeleccionado)
          {
            ++númeroDeElementosAProcesar;
          }
        }

        // Comienza el timer.
        miTimerParaReportarEstatus.Start();

        // Reporta estatus.
        EscuchadorDeEstatus.Progreso = 0;

        // Suspende notificaciones.
        miManejador.SuspendeEventos();

        // Procesar todos los elementos.
        EscuchadorDeEstatus.ProgresoMáximo = elementos.Count;
        miReportaEstatus = true;
        int númeroDelElementoProcesándose = 0;
        for (IndiceDeElementoProcesándose = 0;
          IndiceDeElementoProcesándose < elementos.Count;
          ++IndiceDeElementoProcesándose)
        {
          // Ve si necesitamos parar de procesar.
          if (miManejador.ManejadorDeMapa.ParaDeProcesar)
          {
            ManejadorDeMapa.ParaDeProcesar = false;
            break;
          }

          // Nos saltamos el elemento si no está seleccionado.
          if (!elFiltroEstáSeleccionado[IndiceDeElementoProcesándose])
          {
            continue;
          }
            
          // Actualiza estatus.
          ++númeroDelElementoProcesándose;
          EscuchadorDeEstatus.Progreso = númeroDelElementoProcesándose;
          if (miReportaEstatus)
          {
            Estatus = string.Format("Procesando {0} # {1}/{2}: [{3}]",
                                    miNombreDeElemento,
                                    númeroDelElementoProcesándose,
                                    númeroDeElementosAProcesar,
                                    NúmeroDeProblemasDetectados);

            miReportaEstatus = false;
          }

          // Procesa el elemento si no está eliminado.
          K elemento = elementos[IndiceDeElementoProcesándose];
          if (!elemento.FuéEliminado)
          {
            // Procesa el elemento.
            NúmeroDeProblemasDetectados += ProcesaElemento(elemento);
          }
        }

        // Para el timer.
        miTimerParaReportarEstatus.Stop();

        // Reporta estatus.
        EscuchadorDeEstatus.Progreso = 0;
        EscuchadorDeEstatus.Estatus = "Listo.";

        // Restablece notificaciones.
        miManejador.RestableceEventos();

        // Indica que se terminó de procesar los elementos.
        TerminoDeProcesar();

        // Indica que dejamos de procesar.
        miEstáProcesando = false;

        // Envia evento.
        if (Procesó != null)
        {
          Procesó(this, new NúmeroDeItemsEventArgs(NúmeroDeProblemasDetectados));
        }
      }

      return NúmeroDeProblemasDetectados;
    }

    #endregion

    #region Métodos Protegidos y Privados
    private void EnTimerElapsed(object elEnviador, ElapsedEventArgs losArgumentos)
    {
      miReportaEstatus = true;
    }


    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    /// <returns>A boolean indicating whether to continue
    /// processing.</returns>
    protected virtual bool ComenzóAProcesar()
    {
      return true;
    }


    /// <summary>
    /// Procesa un elemento.
    /// </summary>
    /// <param name="elElemento">El Elemento.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected abstract int ProcesaElemento(K elElemento);


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected virtual void TerminoDeProcesar()
    {
      // Reporta estatus.
      Estatus = "Se detectaron " + NúmeroDeProblemasDetectados + " problemas en " + miNombreDeElemento + "(s)";
    }


    /// <summary>
    /// Obtiene o pone el texto del estatus.
    /// </summary>
    protected string Estatus
    {
      get
      {
        return EscuchadorDeEstatus.Estatus;
      }

      set
      {
        EscuchadorDeEstatus.Estatus = value;
      }
    }


    /// <summary>
    /// Pone al Procesador en estado inválido.
    /// </summary>
    protected void Invalida()
    {
      if (Invalidado != null)
      {
        Invalidado(this, new EventArgs());
      }
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected abstract void EnMapaNuevo(object elEnviador, EventArgs losArgumentos);


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected abstract void EnElementosModificados(object elEnviador, EventArgs losArgumentos);
    #endregion
  }
}
