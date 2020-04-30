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
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa una Polilínea.
  /// </summary>
  public class Polilínea : ElementoDelMapa
  {
    #region Campos
    private CampoCoordenadas miCampoCoordenadas = CampoCoordenadas.Nulas;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las coordenadas de la Polilínea.
    /// </summary>
    public Coordenadas[] Coordenadas
    {
      get
      {
        return miCampoCoordenadas.Coordenadas;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número de la Polilínea.</param>
    /// <param name="laClase">La clase de la Polilínea.</param>
    /// <param name="losCampos">Los campos de la Polilínea.</param>
    public Polilínea(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa,
             elNúmero,
             laClase,
             CaracterísticasDePolilíneas.Descripciones,
             losCampos)
    {
      // Busca los campos especificos de los Polilíneas.
      foreach (Campo campo in losCampos)
      {
        CampoCoordenadas campoCoordenadas = campo as CampoCoordenadas;
        if (campoCoordenadas != null)
        {
          // Si ya tenemos coordenadas entonces solamente las remplazamos
          // si el nivel es menor.
          if (miCampoCoordenadas != CampoCoordenadas.Nulas)
          {
            if (campoCoordenadas.Nivel < miCampoCoordenadas.Nivel)
            {
              miCampoCoordenadas = campoCoordenadas;
            }
          }
          else
          {
            miCampoCoordenadas = campoCoordenadas;
          }
        }
      }
    }


    /// <summary>
    /// Cambia las coordenadas.
    /// </summary>
    /// <param name="lasCoordenadaNuevas">Las coordenadas nuevas.</param>
    /// <param name="elIndice">El índice de la coordenada a cambiar.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public virtual void CambiaCoordenadas(Coordenadas lasCoordenadaNuevas, int elIndice,  string laRazón)
    {
      // Si no tiene Campo de Coordenadas entonces es un error.
      if (miCampoCoordenadas == CampoCoordenadas.Nulas)
      {
        throw new ArgumentException("Las coordenadas son nulas.");
      }

      // Cambia el campo.
      CampoCoordenadas nuevoCampoCoordenadas = new CampoCoordenadas(
        miCampoCoordenadas.Identificador,
        miCampoCoordenadas.Nivel,
        miCampoCoordenadas.Coordenadas);
      nuevoCampoCoordenadas.Coordenadas[elIndice] = lasCoordenadaNuevas;
      CambiaCampo(nuevoCampoCoordenadas, miCampoCoordenadas, laRazón);
      miCampoCoordenadas = nuevoCampoCoordenadas;
    }    


    /// <summary>
    /// Devuelve una versión de texto del objecto.
    /// </summary>
    public override string ToString()
    {
      StringBuilder coordenadas = new StringBuilder();
      int númeroDeCoordenadasAMostrar = Math.Min(Coordenadas.Length, 5);
      for (int i = 0; i < númeroDeCoordenadasAMostrar; ++i)
      {
        coordenadas.Append(Coordenadas[i].ToString());
      }

      string texto = string.Format("{0} [{1}]", Nombre, coordenadas);

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

      Polilínea clone = new Polilínea(ManejadorDeMapa, Número, Clase, camposNuevos);
      return clone;
    }
    #endregion
  }
}
