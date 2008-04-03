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
using Tst;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Buscador de incongruencias en Vías.
  /// </summary>
  public class BuscadorDeIncongruencias : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly IList<IList<ElementoDeIncongruencia>> misIncongruencias = new List<IList<ElementoDeIncongruencia>>();
    private readonly List<Vía> misVíasYaProcesadas = new List<Vía>();
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las incongruencias de Vías.
    /// </summary>
    public IList<IList<ElementoDeIncongruencia>> Incongruencias
    {
      get
      {
        return misIncongruencias;
      }
    }
    #endregion

    #region Clases
    /// <summary>
    /// Representa un elemento de la lista de incongruencias.
    /// </summary>
    public struct ElementoDeIncongruencia
    {
      /// <summary>
      /// Obtiene la Vía asociada.
      /// </summary>
      public readonly Vía Vía;

      /// <summary>
      /// Obtiene el detalle.
      /// </summary>
      public readonly string Detalle;

      /// <summary>
      /// Obtiene una variable lógica que indica si el elemento es un posible error.
      /// </summary>
      public bool EsPosibleError;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="laVía">La vía.</param>
      /// <param name="elDetalle">El detalle.</param>
      /// <param name="elEsPosibleError">Variable lógica que indica si el elemento es un posible error.</param>
      public ElementoDeIncongruencia(Vía laVía, string elDetalle, bool elEsPosibleError)
      {
        Vía = laVía;
        Detalle = elDetalle;
        EsPosibleError = elEsPosibleError;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción = "Busca incongruencias en las Vías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeIncongruencias(
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
      misIncongruencias.Clear();
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
      int númeroDeItemsDetectados = 0;

      // Retorna si la Vía ya ha sido identificado como incongruencia.
      if (misVíasYaProcesadas.Contains(laVía))
      {
        return númeroDeItemsDetectados;
      }

      // Retorna si la Vía no tiene nombre.
      if (laVía.Nombre == string.Empty)
      {
        return númeroDeItemsDetectados;
      }

      #region Busca las Vías que tengan el mismo nombre.
      // Busca las Vías que tengan el mismo nombre desde la posición n + 1 y que
      // no estén eliminadas.
      List<Vía> víasConElMismoNombre = new List<Vía> { laVía };
      bool hayIncongruencias = false;
      for (int i = NúmeroDeElementoProcesándose; i < NúmeroDeElementos; ++i)
      {
        Vía vía = this[i];
        if (!vía.FuéEliminado &&
          (vía.Nombre == laVía.Nombre))
        {
          víasConElMismoNombre.Add(vía);
          misVíasYaProcesadas.Add(vía);
        }
      }
      #endregion

      #region Busca los extremos del Límite de Velocidad.
      List<int> índicesDeLímitesDeVelocidad = new List<int> ();
      int índiceMínimoDelLímiteDeVelocidad = int.MaxValue;
      int índiceMáximoDelLímiteDeVelocidad = int.MinValue;
      int sumaDelIndiceDelLímiteDeVelocidad = 0;
      foreach (Vía vía in víasConElMismoNombre)
      {
        if (!vía.LímiteDeVelocidad.EsNulo())
        {
          int índice = vía.LímiteDeVelocidad.Indice;
          índicesDeLímitesDeVelocidad.Add(índice);
          índiceMínimoDelLímiteDeVelocidad = Math.Min(índiceMínimoDelLímiteDeVelocidad, índice);
          índiceMáximoDelLímiteDeVelocidad = Math.Max(índiceMáximoDelLímiteDeVelocidad, índice);
          sumaDelIndiceDelLímiteDeVelocidad += índice;
        }
      }
      double promedioDelIndiceDelLímiteDeVelocidad = (double)sumaDelIndiceDelLímiteDeVelocidad / índicesDeLímitesDeVelocidad.Count;
      #endregion

      #region Busca los extremos de la Clase de Ruta.
      List<int> índicesDeLaClaseDeRuta = new List<int> ();
      int índiceMínimoDeLaClaseDeRuta = int.MaxValue;
      int índiceMáximoDeLaClaseDeRuta = int.MinValue;
      int sumaDelIndiceDeLaClaseDeRuta = 0;
      foreach (Vía vía in víasConElMismoNombre)
      {
        if (!vía.ClaseDeRuta.EsNula())
        {
          int índice = vía.ClaseDeRuta.Indice;
          índicesDeLaClaseDeRuta.Add(índice);
          índiceMínimoDeLaClaseDeRuta = Math.Min(índiceMínimoDeLaClaseDeRuta, índice);
          índiceMáximoDeLaClaseDeRuta = Math.Max(índiceMáximoDeLaClaseDeRuta, índice);
          sumaDelIndiceDeLaClaseDeRuta += índice;
        }
      }
      double promedioDelIndiceDeLaClaseDeRuta = (double)sumaDelIndiceDeLaClaseDeRuta / índicesDeLaClaseDeRuta.Count;
      #endregion

      #region Verifica si la diferencia entre los extremos de los indices es muy grande.
      string detallePorDefecto = string.Empty;
      if (índicesDeLímitesDeVelocidad.Count > 0) 
      {
        // Calcula la diferencia del índice de Límite de Velocidad.
        int diferenciaDelIndice = índiceMáximoDelLímiteDeVelocidad - índiceMínimoDelLímiteDeVelocidad;

        // Si las diferencia es mayor del límite entonces
        // hay incongruencias.
        const int MáximaDifferenciaDelIndiceDeLímiteDeVelocidad = 2;
        if (diferenciaDelIndice > MáximaDifferenciaDelIndiceDeLímiteDeVelocidad)
        {
          detallePorDefecto = "Differencia de los Indices de Límite de Velocidad es " +
            diferenciaDelIndice + ", y la máxima diferencia permitida es " +  MáximaDifferenciaDelIndiceDeLímiteDeVelocidad;
          hayIncongruencias = true;
        }
      }

      if (índicesDeLaClaseDeRuta.Count > 0) 
      {
        // Calcula la diferencia del índice de Límite de Velocidad.
        int diferenciaDelIndice = índiceMáximoDeLaClaseDeRuta - índiceMínimoDeLaClaseDeRuta;

        // Si las diferencia es mayor del límite entonces
        // hay incongruencias.
        const int MáximaDifferenciaDeLaClaseDeRuta = 2;
        if (diferenciaDelIndice > MáximaDifferenciaDeLaClaseDeRuta)
        {
          detallePorDefecto = "Differencia de los Indices de la Clase de Ruta es " +
            diferenciaDelIndice + ", y la máxima deferencia permitida es " + MáximaDifferenciaDeLaClaseDeRuta;
          hayIncongruencias = true;
        }
      }
      #endregion

      #region Crea la lista de incongruencias.
      List<ElementoDeIncongruencia> elementosDeIncongruencia = new List<ElementoDeIncongruencia>();
      foreach(Vía vía in víasConElMismoNombre)
      {
        if (vía.LímiteDeVelocidad.EsNulo())
        {
          elementosDeIncongruencia.Add(new ElementoDeIncongruencia(vía, "Límite de Velocidad Nulo", true));
          hayIncongruencias = true;
        }
        else if(vía.ClaseDeRuta.EsNula())
        {
          elementosDeIncongruencia.Add(new ElementoDeIncongruencia(vía, "Clase de Ruta es Nula", true));
          hayIncongruencias = true;
          break;
        }
        else
        {
          bool posibleError = false;

          // Si el Límite de Velocidad está muy lejos del promedio entonces puede ser un error.
          double diferenciaDelIndiceDeLímiteDeVelocidad = Math.Abs(vía.LímiteDeVelocidad.Indice - promedioDelIndiceDelLímiteDeVelocidad);
          if (diferenciaDelIndiceDeLímiteDeVelocidad > 1.5)
          {
            posibleError = true;
          }

          // Si la Clase de Ruta está muy lejos del promedio entonces puede ser un error.
          double diferenciaDelIndiceDeLaClaseDeRuta = Math.Abs(vía.ClaseDeRuta.Indice - promedioDelIndiceDeLaClaseDeRuta);
          if (diferenciaDelIndiceDeLaClaseDeRuta > 1.5)
          {
            posibleError = true;
          }

          // Añade la vía a la lista.
          elementosDeIncongruencia.Add(new ElementoDeIncongruencia(vía, detallePorDefecto, posibleError));
        }
      }
      #endregion

      // Si se detectaron incongruencias entonces las añadimos a la lista.
      if (hayIncongruencias)
      {
        misIncongruencias.Add(elementosDeIncongruencia);
        ++númeroDeItemsDetectados;
      }

      return númeroDeItemsDetectados;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      misIncongruencias.Clear();
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
