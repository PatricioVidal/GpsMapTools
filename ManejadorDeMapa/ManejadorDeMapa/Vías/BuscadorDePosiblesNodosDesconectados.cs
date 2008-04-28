﻿#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Buscador de posibles nodos desconectados.
  /// </summary>
  public class BuscadorDePosiblesNodosDesconectados : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly List<PosibleNodoDesconectado> misPosiblesNodosDesconectados = new List<PosibleNodoDesconectado>();
    private readonly List<Vía> misVíasYaProcesadas = new List<Vía>();
    private int miDistanciaMáxima = 5;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la distancia máxima de búsqueda.
    /// </summary>
    public int DistanciaMáxima
    {
      get
      {
        return miDistanciaMáxima;
      }

      set
      {
        miDistanciaMáxima = value;
      }
    }

    
    /// <summary>
    /// Devuelve los posibles nodos desconectados
    /// </summary>
    public IList<PosibleNodoDesconectado> PosibleNodosDesconectados
    {
      get
      {
        return misPosiblesNodosDesconectados;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción = "Busca posibles nodos desconectados en las Vías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDePosiblesNodosDesconectados(
      ManejadorDeVías elManejadorDeVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeVías, elEscuchadorDeEstatus)
    {
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override void ComenzóAProcesar()
    {
      misPosiblesNodosDesconectados.Clear();
      misVíasYaProcesadas.Clear();

      base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa na Vía.
    /// </summary>
    /// <param name="laVía">La Vía.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      Coordenadas[] nodos = laVía.Coordenadas;
      int númeroDeNodos = nodos.Length;

      // Busca en todos los nodos de todas las vías (comenzando desde la siguiente
      // para no repetir búsquedas.)
      for (int i = NúmeroDeElementoProcesándose; i < NúmeroDeElementos; ++i)
      {
        Vía víaDestino = this[i];
        Coordenadas[] nodosDestino = víaDestino.Coordenadas;
        int númeroDeNodosDestino = nodosDestino.Length;

        #region Busca todos los nodos con las mismas coordenadas.
        for (int índiceDeNodo = 0; índiceDeNodo < númeroDeNodos; ++índiceDeNodo)
        {
          Coordenadas nodo = laVía.Coordenadas[índiceDeNodo];
          for (int índiceNodoDestino = 0; índiceNodoDestino < númeroDeNodosDestino; ++índiceNodoDestino)
          {
            // Analizamos el nodo si tiene las mismas coordenadas.
            Coordenadas nodoDestino = nodosDestino[índiceNodoDestino];
            bool tienenLasMismasCoordenadas = (nodo == nodoDestino);
            if (tienenLasMismasCoordenadas)
            {
              string error = null;
              const double distancia = 0;
              bool esNodoDeRuta = EsNodoDeRuta(laVía, índiceDeNodo);
              bool esNodoDestinoDeRuta = EsNodoDeRuta(víaDestino, índiceNodoDestino);
              if (esNodoDeRuta & esNodoDestinoDeRuta)
              {
                // Esto indica que los nodos están conectados.
              }
              else if (!esNodoDeRuta & !esNodoDestinoDeRuta)
              {
                error = "Ambos nodos tienen la mismas coordenadas pero ninguno es ruteable.";
              }
              else
              {
                error = "Ambos nodos tienen la mismas coordenadas pero solo uno es ruteable.";
              }

              if (error != null)
              {
                PosibleNodoDesconectado posibleNodoDesconectado = new PosibleNodoDesconectado(
                  laVía,
                  nodo,
                  víaDestino,
                  nodoDestino,
                  distancia,
                  error);

                misPosiblesNodosDesconectados.Add(posibleNodoDesconectado);
                ++númeroDeProblemasDetectados;
              }

              // Nos salimos del ciclo porque si los nodos tienen las mismas
              // coordenadas entonces ya no pueden estar conectados en otro nodo.
              break;
            }
          }
        }
        #endregion

        númeroDeProblemasDetectados += BuscaPosibleExtremosDesconectados(laVía, víaDestino);
        númeroDeProblemasDetectados += BuscaPosibleExtremosDesconectados(víaDestino, laVía);
      }

      return númeroDeProblemasDetectados;
    }


    private int BuscaPosibleExtremosDesconectados(
      Vía laVía, 
      Vía laVíaDestino)
    {
      int númeroDeProblemasDetectados = 0;

      // Necesitamos al menos dos nodos.
      Coordenadas[] losNodos = laVía.Coordenadas;
      int númeroDeNodos = losNodos.Length;
      if (númeroDeNodos >= 2)
      {
        // Crea los nodos extremos.
        int índiceUltimoNodo = númeroDeNodos - 1;
        Coordenadas[] nodosExtremos = new[] {
              losNodos[0],
              losNodos[índiceUltimoNodo]             
              };

        // Busca la distancia mínima a los nodos de la vía.
        Coordenadas[] nodosDestino = laVíaDestino.Coordenadas;
        foreach (Coordenadas nodo in nodosExtremos)
        {
          double distancíaNodoMasCercano = double.MaxValue;
          Coordenadas nodoMasCercano = null;
          foreach (Coordenadas nodoDestino in nodosDestino)
          {
            // Nos saltamos este nodo si tiene las mismas coordenadas.
            bool tienenLasMismasCoordenadas = (nodo == nodoDestino);
            if (tienenLasMismasCoordenadas)
            {
              distancíaNodoMasCercano = 0;
              continue;
            }

            // Los nodos son posibles nodos desconectados si la distancia entre 
            // ellos es menor que la distancia máxima de búsqueda.
            double distancia = Coordenadas.Distancia(nodo, nodoDestino);
            if (distancia < distancíaNodoMasCercano)
            {
              distancíaNodoMasCercano = distancia;
              nodoMasCercano = nodoDestino;
            }
          }

          // Si la distancia al nodo mas cercano es cero quiere decir que tiene las mismas
          // coordenadas.  En este caso nos saltamos este nodo extremo porque
          // nodos con las mismas coordenadas son procesados en otro ciclo. 
          if (distancíaNodoMasCercano == 0)
          {
            continue;
          }

          if (distancíaNodoMasCercano < miDistanciaMáxima)
          {
            string error = string.Format("La distancia es menor que {0:0.0} m: {1:0.0} m", miDistanciaMáxima,
                                         distancíaNodoMasCercano);
            PosibleNodoDesconectado posibleNodoDesconectado = new PosibleNodoDesconectado(
              laVía,
              nodo,
              laVíaDestino,
              nodoMasCercano,
              distancíaNodoMasCercano,
              error);

            misPosiblesNodosDesconectados.Add(posibleNodoDesconectado);
            ++númeroDeProblemasDetectados;
          }
        }
      }

      return númeroDeProblemasDetectados;
    }


    private static bool EsNodoDeRuta(Vía laVía, int elIndiceDeNodo)
    {
      bool esNodoDeRuta = false;

      foreach(CampoNodo campoNodo in laVía.CamposNodo)
      {
        if (campoNodo.IndiceDeCoordenadas == elIndiceDeNodo)
        {
          esNodoDeRuta = true;
          break;
        }
      }

      return esNodoDeRuta;
    }


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected override void TerminoDeProcesar()
    {
      base.TerminoDeProcesar();

      // Reporta estatus.
      Estatus = string.Format("Posibles Nodos Desconectados: {0}", misPosiblesNodosDesconectados.Count);
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      misPosiblesNodosDesconectados.Clear();
      misVíasYaProcesadas.Clear();

      // Pone al Procesador en estado inválido.
      Invalida();
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Pone al Procesador en estado inválido.
      Invalida();
    }
    #endregion
  }
}
