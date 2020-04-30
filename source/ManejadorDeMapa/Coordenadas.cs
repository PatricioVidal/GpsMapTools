﻿#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
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
using System.Drawing;
namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un par de coordenadas.
  /// </summary>
  public class Coordenadas
  {
    private readonly double miLatitud;
    private readonly double miLongitud;

    /// <summary>
    /// Latitud.
    /// </summary>
    public double Latitud
    {
       get
       {
         return miLatitud;
       }
    }
    
    /// <summary>
    /// Longitud.
    /// </summary>
    public double Longitud
    {
       get
       {
         return miLongitud;
       }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="laLatitud">La Latitud.</param>
    /// <param name="laLongitud">La Longitud.</param>
    public Coordenadas(double laLatitud, double laLongitud)
    {
      miLatitud = laLatitud;
      miLongitud = laLongitud;
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="lasCoordenadas">Las coordenadas.</param>
    public Coordenadas(PointF lasCoordenadas)
      : this (lasCoordenadas.Y, lasCoordenadas.X)
    {
    }


    /// <summary>
    /// Devuelve la distancia en metros entre dos coordenads.
    /// </summary>
    /// <param name="laPrimeraCoordenada">La primera coordenada.</param>
    /// <param name="laSegundaCoordenada">La segunda coordenada.</param>
    public static double DistanciaEnMetros(
      Coordenadas laPrimeraCoordenada,
      Coordenadas laSegundaCoordenada)
    {
      // Esta fórmula fue provista por Antonio Cincotti D'Orazio
      double diferenciaDeLatitud = laPrimeraCoordenada.Latitud - laSegundaCoordenada.Latitud;
      double diferenciaDeLongitud = laPrimeraCoordenada.Longitud - laSegundaCoordenada.Longitud;

      double diferenciaDeLatitudEnMetros = diferenciaDeLatitud * 60 * 1852;
      double diferenciaDeLongitudEnMetros = (diferenciaDeLongitud
        * Math.Cos(laPrimeraCoordenada.Latitud * Math.PI / 180)) * 60 * 1852;

      double distanciaEnMetros = Math.Sqrt(
        (diferenciaDeLatitudEnMetros * diferenciaDeLatitudEnMetros) +
        (diferenciaDeLongitudEnMetros * diferenciaDeLongitudEnMetros));
      
      return distanciaEnMetros;
    }


    /// <summary>
    /// Conversion de coordenadas a PointF.
    /// </summary>
    /// <param name="lasCoordenadas">Las coordenadas.</param>
    public static implicit operator PointF(Coordenadas lasCoordenadas)
    {
      return new PointF((float)lasCoordenadas.Longitud, (float)lasCoordenadas.Latitud);
    }


    /// <summary>
    /// Conversion de coordenadas a PointF.
    /// </summary>
    /// <param name="lasCoordenadas">Las coordenadas.</param>
    public static implicit operator Coordenadas(PointF lasCoordenadas)
    {
      return new Coordenadas(lasCoordenadas);
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      string textoNorteSur = "S";
      if (Latitud > 0)
      {
        textoNorteSur = "N";
      }
      string textoEsteOeste = "W";
      if (Longitud > 0)
      {
        textoEsteOeste = "E";
      }

      string texto = textoNorteSur + " " + Math.Abs(Latitud).ToString("0.000000")
        + "  " + textoEsteOeste + " " + Math.Abs(Longitud).ToString("0.000000");

      return texto;
    }


    /// <summary>
    /// Operador de igualdad.
    /// </summary>
    /// <param name="elPrimerElemento">El Primer Elemento.</param>
    /// <param name="elSegundoElemento">El Segundo Elemento.</param>
    public static bool operator ==(
      Coordenadas elPrimerElemento,
      Coordenadas elSegundoElemento)
    {
      bool esIgual = ((elPrimerElemento.miLatitud == elSegundoElemento.miLatitud) &&
        (elPrimerElemento.miLongitud == elSegundoElemento.miLongitud));
      return esIgual;
    }


    /// <summary>
    /// Operador de desigualdad.
    /// </summary>
    /// <param name="elPrimerElemento">El Primer Elemento.</param>
    /// <param name="elSegundoElemento">El Segundo Elemento.</param>
    public static bool operator !=(
      Coordenadas elPrimerElemento,
      Coordenadas elSegundoElemento)
    {
      return !(elPrimerElemento == elSegundoElemento);
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">El objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Verifica objectos nulos y compara el tipo.
      if (elObjecto == null || (GetType() != elObjecto.GetType()))
      {
        return false;
      }

      // Compara objecto.
      Coordenadas comparador = (Coordenadas)elObjecto;
      bool esIgual = (comparador == this);

      return esIgual;
    }


    /// <summary>
    /// Obtiene una clave única para este objecto.
    /// </summary>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Método GetHashCode() no está implementado.");
    }
  }
}