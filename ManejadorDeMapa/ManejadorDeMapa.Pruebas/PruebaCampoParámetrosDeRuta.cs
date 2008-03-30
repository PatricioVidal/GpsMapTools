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
  public class PruebaCampoParámetrosDeRuta
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
        int índiceLímiteDeVelocidad = 2;
        int índiceDeClaseDeRuta = 3;
        string parámetrosDeRuta = "2,3,0";
        string identificador = "ID";

        // Llama al constructor en prueba.
        CampoParámetrosDeRuta objectoEnPrueba = new CampoParámetrosDeRuta(identificador, parámetrosDeRuta);

        // Prueba Propiedades.
        Assert.AreEqual(identificador, objectoEnPrueba.Identificador, "Identificador");
        Assert.AreEqual(índiceDeClaseDeRuta, objectoEnPrueba.ClaseDeRuta.Indice, "ClaseDeRuta.Indice");
        Assert.AreEqual(índiceLímiteDeVelocidad, objectoEnPrueba.LímiteDeVelocidad.Indice, "LímiteDeVelocidad.Indice");
      }
      #endregion

      #region Caso 2: Parametros de ruta inválidos.
      {
        // Preparación.
        string parametrosDeRutaInválidos = "2";
        bool lanzóExcepción = false;
        ArgumentException excepciónEsperada = new ArgumentException(
          "Los parámetros de rutas deben tener al menos 2 elementos separados por coma, pero es: 2");

        // Llama al constructor en prueba.
        try
        {
          CampoParámetrosDeRuta objectoEnPrueba = new CampoParámetrosDeRuta("ID", parametrosDeRutaInválidos);
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
        string resultadoEsperado = "(3) 35mph / 60kph";

        // Llama al constructor en prueba.
        string resultado = objectoEnPrueba.ToString();

        // Prueba resultado.
        Assert.That(resultado, Is.EqualTo(resultadoEsperado), "Resultado");
      }
      #endregion

      #region Caso 2: Límite de Velocidad nulo.
      {
        // Preparación.
        CampoParámetrosDeRuta objectoEnPrueba = CampoParámetrosDeRuta.Nulo;

        // Llama al método en prueba.
        string resultado = objectoEnPrueba.ToString();

        // Prueba resultado.
        Assert.That(resultado, Is.EqualTo(string.Empty), "Resultado");
      }
      #endregion
    }
  }
}
