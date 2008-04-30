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

using System.Collections.Generic;

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Representa un Punto De Interés (PDI/POI)
  /// </summary>
  public class Pdi : ElementoDelMapa
  {
    #region Campos
    private readonly static Coordenadas misCoordenadasVacias = new Coordenadas(double.NaN, double.NaN);
    private readonly CampoCoordenadas misCoordenadas = CampoCoordenadas.Nulas;
    private CampoIndiceDeCiudad miCampoIndiceDeCiudad;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las coordenadas del PDI.
    /// </summary>
    public Coordenadas Coordenadas
    {
      get
      {
        if (misCoordenadas.Coordenadas.Length == 0)
        {
          return misCoordenadasVacias;
        }

        return misCoordenadas.Coordenadas[0];
      }
    }


    /// <summary>
    /// Obtiene una variable lógica que indica si el PDI es una ciudad.
    /// </summary>
    public bool EsCiudad { get; private set; }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del PDI.</param>
    /// <param name="laClase">La clase de PDI.</param>
    /// <param name="losCampos">Los campos del PDI.</param>
    /// <param name="elEsCiudad">Variable lógica que indica si el PDI es una ciudad.</param>
    public Pdi(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos,
      bool elEsCiudad)
      : this(elManejadorDeMapa,
             elNúmero,
             laClase,
             losCampos)
    {
      EsCiudad = elEsCiudad;
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del PDI.</param>
    /// <param name="laClase">La clase de PDI.</param>
    /// <param name="losCampos">Los campos del PDI.</param>
    public Pdi(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa, 
             elNúmero, 
             laClase,
             CaracterísticasDePdis.Descripciones,
             losCampos)
    {
      // Busca los campos especificos de los PDIs.
      foreach (Campo campo in losCampos)
      {
        CampoCoordenadas campoCoordenadas;
        CampoEsCiudad campoCiudad;
        CampoIndiceDeCiudad campoIndiceDeCiudad;
        if ((campoCoordenadas  = campo as CampoCoordenadas) != null)
        {
          misCoordenadas = campoCoordenadas;
        }
        else if ((campoCiudad = campo as CampoEsCiudad) != null)
        {
          EsCiudad = campoCiudad.EsCiudad;
        }
        else if ((campoIndiceDeCiudad = campo as CampoIndiceDeCiudad) != null)
        {
          miCampoIndiceDeCiudad = campoIndiceDeCiudad;
        }
      }
    }


    /// <summary>
    /// Cambia el Campo de Indice de Ciudad.
    /// </summary>
    /// <param name="elCampoIndiceDeCiudadNuevo">El Campo de Indice de Ciudad nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    /// <returns>Una variable lógica que indica que el PDI se modificó.</returns>
    public bool ActualizaCampoIndiceDeCiudad(CampoIndiceDeCiudad elCampoIndiceDeCiudadNuevo, string laRazón)
    {
      bool cambió = false;

      // Si no tiene el Campo de Indice de Ciudad en tonces le 
      // añadimos uno.
      // Si no, se lo cambiamos.
      if (miCampoIndiceDeCiudad == null)
      {
        AñadeCampo(elCampoIndiceDeCiudadNuevo, laRazón);
        miCampoIndiceDeCiudad = elCampoIndiceDeCiudadNuevo;
        cambió = true;
      }
      else
      {
        // Cambia el campo si es diferente.
        if (elCampoIndiceDeCiudadNuevo != miCampoIndiceDeCiudad)
        {
          CambiaCampo(elCampoIndiceDeCiudadNuevo, miCampoIndiceDeCiudad, laRazón);
          miCampoIndiceDeCiudad = elCampoIndiceDeCiudadNuevo;
          cambió = true;
        }
      }

      return cambió;
    }


    /// <summary>
    /// Devuelve una version de texto del objecto.
    /// </summary>
    public override string ToString()
    {
      string texto = "#" + Número  + ", " + Nombre + ", " + Tipo + ", " + Coordenadas;

      return texto;
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public override object Clone()
    {
      // Como los campos son invariables entonces no necesitamos
      // hacer copias de ellos.
      List<Campo> camposNuevos = new List<Campo>(Campos.Count);
      foreach (Campo campo in Campos)
      {
        camposNuevos.Add(campo);
      }

      Pdi clone = new Pdi(ManejadorDeMapa, Número, Clase, camposNuevos);
      return clone;
    }


    /// <summary>
    /// Retorna una variable lógica indicando si un PDI dado
    /// tien la misma información que este PDI.
    /// </summary>
    /// <param name="elPdi">El PDI dado.</param>
    public bool TieneLaMismaInformación(Pdi elPdi)
    {
      bool tieneLaMismaInformación = false;

      // El PDI tiene la misma información si:
      //  - El nombre es igual.
      //  - El tipo es igual.
      //  - Las coordenadas son iguales.
      //  - La información de los campos son iguales.
      if ((Nombre == elPdi.Nombre) &&
        (Tipo == elPdi.Tipo) &&
        (Coordenadas == elPdi.Coordenadas) &&
        (Campos.Count == elPdi.Campos.Count))
      {
        // Ahora hay que asegurarse que todos lo campos son iguales.
        tieneLaMismaInformación = true;

        foreach (Campo campo in Campos)
        {
          bool encontróCampoIgual = false;
          foreach (Campo campoAComparar in elPdi.Campos)
          {
            if (campo.Equals(campoAComparar))
            {
              encontróCampoIgual = true;
              break;
            }
          }

          // Si no encontró un campo igual entonces el PDI
          // no contiene la misma información.
          if (!encontróCampoIgual)
          {
            tieneLaMismaInformación = false;
            break;
          }
        }
      }

      return tieneLaMismaInformación;
    }
    #endregion
  }
}
