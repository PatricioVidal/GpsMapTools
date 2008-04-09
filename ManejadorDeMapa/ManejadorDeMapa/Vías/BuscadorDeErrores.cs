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
  /// Buscador de errores en Vías.
  /// </summary>
  public class BuscadorDeErrores : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly IDictionary<Vía, string> misErrores = new Dictionary<Vía, string>();
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve los errores de Vías.
    /// </summary>
    public IDictionary<Vía, string> Errores
    {
      get
      {
        return misErrores;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción = "Busca errores en las Vías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeErrores(
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
      misErrores.Clear();
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
      List<string> errores = new List<string>();

      #region Verifica que el tipo de PDI no es vacio.
      Tipo tipo = laVía.Tipo;
      bool esVacio = (tipo == Tipo.TipoNulo);
      if (esVacio)
      {
        errores.Add("El tipo está vacío.");
      }
      #endregion

      #region Verifica que el tipo de PDI es conocido.
      bool esConocido = CaracterísticasDePolilíneas.Descripciones.ContainsKey(tipo);
      if (!esConocido)
      {
        errores.Add("El tipo (" + laVía.Tipo.ToString() + ") no es conocido");
      }
      #endregion

      #region Verifica las coordenadas.
      // La Vía debe tener un campo de coordenadas y además tienen que
      // tener nivel zero.
      CampoCoordenadas campoCoordenadas = null;
      foreach (Campo campo in laVía.Campos)
      {
        if (campo is CampoCoordenadas)
        {
          campoCoordenadas = (CampoCoordenadas)campo;
          break;
        }
      }
      if (campoCoordenadas == null)
      {
        errores.Add("No tiene coordenadas.");
      }
      else
      {
        // Si la Vía tiene coordenadas entonces:
        //  - Deben ser a nivel zero.
        //  - Tienen que ser más de un par.
        if (campoCoordenadas.Nivel != 0)
        {
          errores.Add("No tiene coordenadas a nivel 0, sino a nivel " + campoCoordenadas.Nivel);
        }
        if (!(campoCoordenadas.Coordenadas.Length > 1))
        {
          errores.Add(string.Format("Debe tener más de un par de coordenadas, pero tiene {0}", campoCoordenadas.Coordenadas.Length));
        }
      }
      #endregion

      // Chequea si hay errores.
      if (errores.Count > 0)
      {
        string todosLosErrores = string.Join("|", errores.ToArray());
        misErrores.Add(laVía, todosLosErrores);
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
      misErrores.Clear();

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
