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

using System.Collections.Generic;

namespace GpsYv.ManejadorDeMapa.Pdis
{
  /// <summary>
  /// Manejados de PDIs.
  /// </summary>
  public class ManejadorDePdis : ManejadorBase<Pdi>
  {
    #region Campos
    private readonly EliminadorDeCaracteres miEliminadorDeCaracteres;
    private readonly ArregladorDeLetras miArregladorDeLetras;
    private readonly ArregladorDePalabrasPorTipo miArregladorDePalabrasPorTipo;
    private readonly BuscadorDeDuplicados miBuscadorDeDuplicados;
    private readonly BuscadorDeErrores miBuscadorDeErrores;
    #endregion
    
    #region Propiedades
    /// <summary>
    /// Descripción de la acción "Procesar Todo".
    /// </summary>
    public static readonly string DescripciónProcesarTodo =
      "Procesa todo lo relacionado con de PDIs. Los pasos se hacen en el orden indicado por el número en el menú.";


    /// <summary>
    /// Obtiene el Eliminador de Caracteres.
    /// </summary>
    public EliminadorDeCaracteres EliminadorDeCaracteres
    {
      get
      {
        return miEliminadorDeCaracteres;
      }
    }


    /// <summary>
    /// Obtiene el Arreglador de Letras.
    /// </summary>
    public ArregladorDeLetras ArregladorDeLetras
    {
      get
      {
        return miArregladorDeLetras;
      }
    }


    /// <summary>
    /// Obtiene el Arreglador de Palabras por Tipo.
    /// </summary>
    public ArregladorDePalabrasPorTipo ArregladorDePalabrasPorTipo
    {
      get
      {
        return miArregladorDePalabrasPorTipo;
      }
    }


    /// <summary>
    /// Obtiene el Buscador de Duplicados.
    /// </summary>
    public BuscadorDeDuplicados BuscadorDeDuplicados
    {
      get
      {
        return miBuscadorDeDuplicados;
      }
    }


    /// <summary>
    /// Obtiene el Buscador de Errores.
    /// </summary>
    public BuscadorDeErrores BuscadorDeErrores
    {
      get
      {
        return miBuscadorDeErrores;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El Manejador de Mapa.</param>
    /// <param name="losPuntosDeInteres">Los PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorDePdis(
      ManejadorDeMapa elManejadorDeMapa,
      IList<Pdi> losPuntosDeInteres,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base (elManejadorDeMapa, losPuntosDeInteres, elEscuchadorDeEstatus)
    {
      // Crea los procesadores.
      miEliminadorDeCaracteres = new EliminadorDeCaracteres(this, elEscuchadorDeEstatus);
      miArregladorDeLetras = new ArregladorDeLetras(this, elEscuchadorDeEstatus);
      miArregladorDePalabrasPorTipo = new ArregladorDePalabrasPorTipo(this, elEscuchadorDeEstatus);
      miBuscadorDeDuplicados = new BuscadorDeDuplicados(this, elEscuchadorDeEstatus);
      miBuscadorDeErrores = new BuscadorDeErrores(this, elEscuchadorDeEstatus);

      // Escucha eventos.
      elManejadorDeMapa.PdisModificados += EnElementosModificados;
    }


    /// <summary>
    /// Procesa todas las correcciones a PDIs.
    /// </summary>
    /// <returns>El número de PDIs modificados.</returns>
    public int ProcesarTodo()
    {
      // Hacer todos las operaciones en orden.
      int númeroDeProblemasEnPdis = 0;
      númeroDeProblemasEnPdis += miEliminadorDeCaracteres.Procesa();
      númeroDeProblemasEnPdis += miArregladorDeLetras.Procesa();
      númeroDeProblemasEnPdis += miArregladorDePalabrasPorTipo.Procesa();
      númeroDeProblemasEnPdis += miBuscadorDeDuplicados.Procesa();
      númeroDeProblemasEnPdis += miBuscadorDeErrores.Procesa();

      // Reporta estatus.
      EscuchadorDeEstatus.Estatus = "Se detectaron " + númeroDeProblemasEnPdis + " problemas en PDIs.";

      return númeroDeProblemasEnPdis;
    }
    #endregion
  }
}