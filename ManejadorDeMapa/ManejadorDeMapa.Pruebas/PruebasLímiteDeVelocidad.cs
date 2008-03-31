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
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas
{
  /// <summary>
  /// Pruebas para la clase LímiteDeVelocidad.
  /// </summary>
  [TestFixture]
  public class PruebasLímiteDeVelocidad
  {
    /// <summary>
    /// Prueba el constructor.
    /// </summary>
    [Test]
    public void PruebaConstructor()
    {
      #region Caso 1: Indice en rango válido.
      {
        // Preparación.
        int índice = 3;

        // Llama al constructor en prueba.
        LímiteDeVelocidad objectoEnPrueba = new LímiteDeVelocidad(índice);

        // Prueba Propiedades.
        Assert.AreEqual(índice, objectoEnPrueba.Indice, "Indice");
      }
      #endregion

      #region Caso 2: Indice fuera de rango.
      {
        // Preparación.
        int índiceFueraDeRango = 40;
        bool lanzóExcepción = false;
        ArgumentOutOfRangeException excepciónEsperada = new ArgumentOutOfRangeException(
          "El índice de la clase de ruta debe ser menor o igual a 7");

        // Llama al constructor en prueba.
        try
        {
          LímiteDeVelocidad objectoEnPrueba = new LímiteDeVelocidad(índiceFueraDeRango);
        }
        catch (Exception e)
        {
          // Prueba las propiedades de la excepción.
          Assert.That(e.GetType(), Is.EqualTo(excepciónEsperada.GetType()), "Tipo de Excepción");
          Assert.That(e.Message, Is.EqualTo(excepciónEsperada.Message), "Excepción.Message");

          lanzóExcepción = true;
        }

        Assert.That(lanzóExcepción, Is.True, "No se lanzó la excepción.");
      }
      #endregion
    }


    /// <summary>
    /// Prueba el método ToString().
    /// </summary>
    [Test]
    public void PruebaToString()
    {
      #region Caso 1: Indice en rango válido.
      {
        // Preparación.
        int índice = 3;
        LímiteDeVelocidad objectoEnPrueba = new LímiteDeVelocidad(índice);
        string resultadoEsperado = "(3) 60 km/h";

        // Llama al método en prueba.
        string resultado = objectoEnPrueba.ToString();

        // Prueba resultado.
        Assert.That(resultado, Is.EqualTo(resultadoEsperado), "Resultado");
      }
      #endregion

      #region Caso 2: Límite de Velocidad nulo.
      {
        // Preparación.
        LímiteDeVelocidad objectoEnPrueba = LímiteDeVelocidad.Nulo;

        // Llama al método en prueba.
        string resultado = objectoEnPrueba.ToString();

        // Prueba resultado.
        Assert.That(resultado, Is.EqualTo(string.Empty), "Resultado");
      }
      #endregion
    }
  }
}
