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

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Buscador de errores en PDIs.
  /// </summary>
  public class BuscadorDeErrores : ProcesadorBase<ManejadorDePdis, Pdi>
  {
    #region Campos
    private readonly IDictionary<Pdi, string> misErrores = new Dictionary<Pdi, string>();
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve los PDIs con errores.
    /// </summary>
    public IDictionary<Pdi, string> Errores
    {
      get
      {
        return misErrores;
      }
    }


    /// <summary>
    /// Atributo "IgnorarNoCoordenadasNivel0".
    /// </summary>
    /// <remarks>
    /// Este atributo indica ignorar que el PDI no tenga coordenadas a nivel zero.
    /// </remarks>
    public const string AtributoIgnorarNoCoordenadasNivel0 = "IgnorarNoCoordenadasNivel0";


    /// <summary>
    /// Atributo "IgnorarCamposCityYCityIdx".
    /// </summary>
    /// <remarks>
    /// Este atributo indica ignorar Campos City y CityIdx.
    /// </remarks>
    public const string AtributoIgnorarCamposCityYCityIdx = "IgnorarCamposCityYCityIdx";
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public const string Descripción = "Busca errores en los PDIs.  Incluye Tipos desconocidos, PDIs sin coordenadas a nivel 0, etc.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePdis">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeErrores(
      ManejadorDePdis elManejadorDePdis,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePdis, elEscuchadorDeEstatus)
    {
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override bool ComenzóAProcesar()
    {
      misErrores.Clear();
      return base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elPdi">El PDI.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Pdi elPdi)
    {
      int númeroDeProblemasDetectados = 0;
      List<string> errores = new List<string>();

      #region Verifica que el tipo de PDI no es vacio y que es conocido.
      Tipo? tipo = elPdi.Tipo;
      if (tipo == null)
      {
        errores.Add(Properties.Recursos.E000);
      }
      else
      {
        bool esConocido = CaracterísticasDePdis.Descripciones.ContainsKey((Tipo)tipo);
        if (!esConocido)
        {
          errores.Add(string.Format(Properties.Recursos.E001, elPdi.Tipo));
        }
      }
      #endregion 

      #region Verifica las coordenadas.
      // El PDI debe tener un campo de coordenadas y además tienen que
      // tener nivel zero.
      CampoCoordenadas campoCoordenadas = null;
      foreach (Campo campo in elPdi.Campos)
      {
        if (campo is CampoCoordenadas)
        {
          campoCoordenadas = (CampoCoordenadas)campo;
          break;
        }
      }
      if (campoCoordenadas == null)
      {
        errores.Add(Properties.Recursos.E002);
      }
      else if ((campoCoordenadas.Nivel != 0) &&
               !elPdi.TieneAtributo(AtributoIgnorarNoCoordenadasNivel0))
      {
        errores.Add(string.Format(Properties.Recursos.E003, campoCoordenadas.Nivel));
      }
      #endregion

      #region Verifica City=Y y CityIdx
      if (!elPdi.TieneAtributo(AtributoIgnorarCamposCityYCityIdx))
      {
        if ((elPdi.Tipo != null) && (TiposDeCiudades.Tipos.Contains(elPdi.Tipo.Value)))
        {
            // Si el tipo de PDI es de ciudad y no es RGN20 entonces debería
            // tener un campo EsCiudad con valor verdadero.
            if (elPdi.Clase != "RGN20")
            {
              // Busca el campo EsCiudad.
              CampoEsCiudad campoEsCiudad = null;
              foreach (Campo campo in elPdi.Campos)
              {
                if (campo is CampoEsCiudad)
                {
                  campoEsCiudad = (CampoEsCiudad) campo;
                  break;
                }
              }

              // Añade el error si no tiene el campo o es falso.
              if ((campoEsCiudad == null) ||
                  !campoEsCiudad.EsCiudad)
              {
                errores.Add(Properties.Recursos.E004);
              }
            }

          // Si el tipo de PDI es de ciudad entonces debería tener el campo
          // Busca el campo EsCiudad.
          CampoIndiceDeCiudad campoIndiceDeCiudad = null;
          foreach (Campo campo in elPdi.Campos)
          {
            if (campo is CampoIndiceDeCiudad)
            {
              campoIndiceDeCiudad = (CampoIndiceDeCiudad)campo;
              break;
            }
          }

          // Añade el error si no tiene el campo.
          if (campoIndiceDeCiudad == null)
          {
            errores.Add(Properties.Recursos.E005);
          }
        }
      }
      #endregion

      // Chequea si hay errores.
      if (errores.Count > 0)
      {
        string todosLosErrores = string.Join("|", errores.ToArray());
        misErrores.Add(elPdi, todosLosErrores);
        ++númeroDeProblemasDetectados;
      }

      return númeroDeProblemasDetectados;
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
