#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
// (For English scroll down.)
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
// Visita https://github.com/PatricioVidal/GpsMapTools para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Autor: Patricio Vidal.
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
// Visit https://github.com/PatricioVidal/GpsMapTools for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Author: Patricio Vidal.
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
using System.Globalization;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Buscador de posibles nodos desconectados.
  /// </summary>
  public class BuscadorDePosiblesNodosDesconectados : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos

    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene un filtro con las vías con posible nodos desconectados.
    /// </summary>
    public bool[] FiltroDeVíasConPosiblesNodosDesconectados { get; private set; }

    /// <summary>
    /// Obtiene o pone la distancia máxima de búsqueda.
    /// </summary>
    public int DistanciaMáxima { get; set; }


    /// <summary>
    /// Devuelve los posibles nodos desconectados
    /// </summary>
    public List<InformaciónNodoDesconectado> PosibleNodosDesconectados { get; private set; }


    /// <summary>
    /// Atributo "NodoDesconectado".
    /// </summary>
    /// <remarks>
    /// Este atributo indica que el nodo está desconectado.
    /// </remarks>
    public const string AtributoNodoDesconectado = "NodoDesconectado";

    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public const string Descripción = "Busca posibles nodos desconectados en las Vías.";
    #endregion

    #region Métodos Públicos
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
      DistanciaMáxima = 5;
      PosibleNodosDesconectados = new List<InformaciónNodoDesconectado>();
    }

    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override bool ComenzóAProcesar()
    {
      PosibleNodosDesconectados.Clear();
      FiltroDeVíasConPosiblesNodosDesconectados = new bool[NúmeroDeElementos];

      return base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa una Vía.
    /// </summary>
    /// <param name="laVía">La Vía.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      Coordenadas[] nodos = laVía.Coordenadas;
      int númeroDeNodos = nodos.Length;

      // Crea mapa de nodos desconectados por adelantado para hacerlo
      // afuera del loop.
      bool[] mapaNodoDesconectado = new bool[númeroDeNodos];
      for (int i = 0; i < númeroDeNodos; ++i)
      {
        string atributoNodoDesconectado = AtributoNodoDesconectado
          + ',' + i;
        if (laVía.TieneAtributo(atributoNodoDesconectado))
        {
          mapaNodoDesconectado[i] = true;
        }
      }

      // Busca en todos los nodos de todas las vías (comenzando desde la siguiente
      // para no repetir búsquedas.)
      for (int índiceVíaDestino = IndiceDeElementoProcesándose + 1; índiceVíaDestino < NúmeroDeElementos; ++índiceVíaDestino)
      {
        Vía víaDestino = this[índiceVíaDestino];

        #region Busca todos los nodos con las mismas coordenadas.
        foreach (Nodo nodo in laVía.Nodos)
        {
          // Nos saltamos el nodo si éste tiene el atributo de nodo desconectado.
          if (mapaNodoDesconectado[nodo.Indice])
          {
            continue;
          }

          bool esNodoRuteable = nodo.EsRuteable;

          // Buscamos en todos los nodos destino.
          Coordenadas coordenadasNodo = nodo.Coordenadas;
          foreach (Nodo nodoDestino in víaDestino.Nodos)
          {
            #region Analizamos los nodos si tiene las mismas coordenadas.
            Coordenadas coordenadasNodoDestino = nodoDestino.Coordenadas;
            bool tienenLasMismasCoordenadas = (coordenadasNodo == coordenadasNodoDestino);
            if (tienenLasMismasCoordenadas)
            {
              string error = null;
              const double distancia = 0;
              bool esNodoDestinoRuteable = nodoDestino.EsRuteable;
              if (esNodoRuteable && esNodoDestinoRuteable)
              {
                // Si los identificadores de nodo no son el mismo entonces es un
                // posible nodo desconectado.
                if (nodo.IdentificadorGlobal != nodoDestino.IdentificadorGlobal)
                {
                  error = string.Format("E104: Los nodos tienen Identificadores Globales distintos: {0} != {1}.",
                    nodo.IdentificadorGlobal,
                    nodoDestino.IdentificadorGlobal);
                }
              }
              else if (!esNodoRuteable & !esNodoDestinoRuteable)
              {
                error = "E105: Ambos nodos tienen las mismas coordenadas pero ninguno es ruteable.";
              }
              else 
              {
                error = "E106: Ambos nodos tienen las mismas coordenadas pero solo uno es ruteable.";
              }

              if (error != null)
              {
                InformaciónNodoDesconectado posibleNodoDesconectado = new InformaciónNodoDesconectado(
                  nodo,
                  nodoDestino,
                  distancia,
                  error);

                PosibleNodosDesconectados.Add(posibleNodoDesconectado);
                ++númeroDeProblemasDetectados;

                // Añade las vías involucradas al filtro.
                FiltroDeVíasConPosiblesNodosDesconectados[IndiceDeElementoProcesándose] = true;
                FiltroDeVíasConPosiblesNodosDesconectados[índiceVíaDestino] = true;
              }

              // Nos salimos del ciclo porque si los nodos tienen las mismas
              // coordenadas entonces ya no pueden estar conectados en otro nodo.
              break;
            }
            #endregion
          }
        }
        #endregion

        // Busca nodos desconectados extremos.
        int númeroDeNodosDetectados = BuscaPosibleExtremosDesconectados(laVía, víaDestino);
        númeroDeNodosDetectados += BuscaPosibleExtremosDesconectados(víaDestino, laVía);
        if (númeroDeNodosDetectados > 0)
        {
          // Añade las vías involucradas al filtro.
          FiltroDeVíasConPosiblesNodosDesconectados[IndiceDeElementoProcesándose] = true;
          FiltroDeVíasConPosiblesNodosDesconectados[índiceVíaDestino] = true;
        }

        númeroDeProblemasDetectados += númeroDeNodosDetectados;
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
        #region Crea los nodos extremos.
        int índiceUltimoNodo = númeroDeNodos - 1;
        List<int> índicesNodosExtremos = new List<int>();

        // Nos saltamos el nodo si éste tiene el atributo de índice desconectado.
        string atributoNodoDesconectado = AtributoNodoDesconectado
          + ",0";
        if (!laVía.TieneAtributo(atributoNodoDesconectado))
        {
          índicesNodosExtremos.Add(0);
        }
        atributoNodoDesconectado = AtributoNodoDesconectado
          + ','+ índiceUltimoNodo.ToString(CultureInfo.InvariantCulture);
        if (!laVía.TieneAtributo(atributoNodoDesconectado))
        {
          índicesNodosExtremos.Add(índiceUltimoNodo);
        }
        #endregion

        // Busca la distancia mínima a los nodos de la vía.
        foreach (int índiceNodo in índicesNodosExtremos)
        {
          Coordenadas nodo = laVía.Coordenadas[índiceNodo];
          double distanciaNodoMasCercano = double.MaxValue;
          int índiceNodoMasCercano = 0;
          int númeroDeNodosDestino = laVíaDestino.Coordenadas.Length;
          for (int índiceNodoDestino = 0; índiceNodoDestino < númeroDeNodosDestino; ++índiceNodoDestino)
          {
            Coordenadas nodoDestino = laVíaDestino.Coordenadas[índiceNodoDestino];

            // Nos saltamos este nodo si tiene las mismas coordenadas.
            bool tienenLasMismasCoordenadas = (nodo == nodoDestino);
            if (tienenLasMismasCoordenadas)
            {
              distanciaNodoMasCercano = 0;
              continue;
            }

            // Busca el nodo mas cercano.
            double distancia = Coordenadas.DistanciaEnMetros(nodo, nodoDestino);
            if (distancia < distanciaNodoMasCercano)
            {
              distanciaNodoMasCercano = distancia;
              índiceNodoMasCercano = índiceNodoDestino;
            }
          }

          // Si la distancia al nodo mas cercano es cero quiere decir que tiene las mismas
          // coordenadas.  En este caso nos saltamos este nodo extremo porque
          // nodos con las mismas coordenadas son procesados en otro ciclo. 
          if (distanciaNodoMasCercano == 0)
          {
            continue;
          }

          // Los nodos son posibles nodos desconectados si la distancia entre 
          // ellos es menor que la distancia máxima de búsqueda.
          if (distanciaNodoMasCercano < DistanciaMáxima)
          {
            // Si la vía tiene algun otro nodo conectado al nodo destino
            // entonces nos saltamos este nodo.
            bool laVíaTieneOtroNodoConectadoAlNodoDestino = false;
            foreach (Coordenadas nodoDeLaVía in laVía.Coordenadas)
            {
              Coordenadas nodoMasCercano = laVíaDestino.Coordenadas[índiceNodoMasCercano];
              if (nodoDeLaVía == nodoMasCercano)
              {
                laVíaTieneOtroNodoConectadoAlNodoDestino = true;
                break;
              }
            }
            if (!laVíaTieneOtroNodoConectadoAlNodoDestino)
            {
              string error = string.Format("E107: La distancia es menor que {0:0.0} m: {1:0.0} m.", DistanciaMáxima,
                                           distanciaNodoMasCercano);
              InformaciónNodoDesconectado posibleNodoDesconectado = new InformaciónNodoDesconectado(
                new Nodo(laVía, índiceNodo),
                new Nodo(laVíaDestino, índiceNodoMasCercano),
                distanciaNodoMasCercano,
                error);

              PosibleNodosDesconectados.Add(posibleNodoDesconectado);
              ++númeroDeProblemasDetectados;
            }
          }
        }
      }

      return númeroDeProblemasDetectados;
    }


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected override void TerminoDeProcesar()
    {
      base.TerminoDeProcesar();

      // Reporta estatus.
      Estatus = string.Format("Posibles Nodos Desconectados: {0}", PosibleNodosDesconectados.Count);
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      PosibleNodosDesconectados.Clear();
      FiltroDeVíasConPosiblesNodosDesconectados = null;

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
