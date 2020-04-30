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

using System.Collections.Generic;
using NUnit.Framework;
using GpsYv.ManejadorDeMapa.Pdis;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas.Pdis
{
  ///<summary>
  /// Prueba de clase <see cref="BuscadorDeErrores"/>.
  ///</summary>
  [TestFixture]
  public class PruebaBuscadorDeErrores
  {
    /// <summary>
    /// Prueba el constructor.
    /// </summary>
    [Test]
    public void PruebaConstructor()
    {
      // Preparación.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePdis manejadorDePdis = new ManejadorDePdis(manejadorDeMapa, new List<Pdi>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      var objectoDePrueba = new BuscadorDeErrores(manejadorDePdis, escuchadorDeEstatus);

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(0), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(0), "NúmeroDeProblemasDetectados");
    }


    /// <summary>
    /// Caso 1 de la prueba del método Procesa().
    /// </summary>
    /// <remarks>
    /// Prueba la funcionalidad del los Indices de Ciudad.
    /// </remarks>
    [Test]
    public void PruebaProcesaCaso1()
    {
      #region Preparación.
      // Crea el objeto a probar.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePdis manejadorDePdis = new ManejadorDePdis(manejadorDeMapa, new List<Pdi>(), escuchadorDeEstatus);
      var objectoDePrueba = new BuscadorDeErrores(manejadorDePdis, escuchadorDeEstatus);

      // Crea los elementos.
      IList<Pdi> pdis = manejadorDePdis.Elementos;
      const string clase = "POI";
      var campoCoordenadas = new CampoCoordenadas("Data", 0, new[]
        {
          new Coordenadas(10.16300,-66.00000),
          new Coordenadas(10.16199,-65.99850),
          new Coordenadas(10.16010,-65.99591),
        });

      var pdiNoCiudad = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0x001"),
        campoCoordenadas});
      pdis.Add(pdiNoCiudad);

      var pdiCiudadCorrecta = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0xb00"),
        new CampoEsCiudad(true),
        new CampoIndiceDeCiudad(79),
        campoCoordenadas});
      pdis.Add(pdiCiudadCorrecta);

      var pdiSinCampoEsCiudad = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0xc00"),
        new CampoIndiceDeCiudad(79),
        campoCoordenadas});
      pdis.Add(pdiSinCampoEsCiudad);

      var pdiSinIndiceDeCiudad = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0xd00"),
        new CampoEsCiudad(true),
        campoCoordenadas});
      pdis.Add(pdiSinIndiceDeCiudad);

      var pdiSinIndiceDeCiudadYConAttributo = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0xf00"),
        new CampoAtributo(BuscadorDeErrores.AtributoIgnorarCamposCityYCityIdx),
        campoCoordenadas});
      pdis.Add(pdiSinIndiceDeCiudadYConAttributo);

      var pdiSinIndiceDeCiudadYSinCampoEsCiudad = new Pdi(manejadorDeMapa, 1, clase, new List<Campo> {
        new CampoTipo("0xe00"),
        campoCoordenadas});
      pdis.Add(pdiSinIndiceDeCiudadYSinCampoEsCiudad);

      // Deberian haber 3 errores:
      //   - 1 por el PDI sin campo de Ciudad.
      //   - 1 por el PDI sin índice de Ciudad.
      //   - 2 por el PDI sin campo de Ciudad y sin índice de Ciudad.
      const int númeroDeProblemasDetectados = 4;
      #endregion

      // Llama al método bajo prueba.
      objectoDePrueba.Procesa();

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(pdis.Count), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(númeroDeProblemasDetectados), "NúmeroDeProblemasDetectados");
      Assert.That(objectoDePrueba.Errores.Count, Is.EqualTo(númeroDeProblemasDetectados), "Errores.Count");
      
      Assert.That(
        objectoDePrueba.Errores[pdiSinCampoEsCiudad],
        Text.StartsWith("E004"),
        "Errores[pdiSinCampoEsCiudad]");

      Assert.That(
        objectoDePrueba.Errores[pdiSinIndiceDeCiudad],
        Text.StartsWith("E005"), 
        "Errores[pdiSinIndiceDeCiudad]");

      Assert.That(
       objectoDePrueba.Errores[pdiSinIndiceDeCiudadYSinCampoEsCiudad],
       Text.StartsWith("E004"),
       "Errores[pdiSinIndiceDeCiudadYSinCampoEsCiudad]");

      Assert.That(
       objectoDePrueba.Errores[pdiSinIndiceDeCiudadYSinCampoEsCiudad],
       Text.Contains("E005"),
       "Errores[pdiSinIndiceDeCiudadYSinCampoEsCiudad]");
      
      Assert.That(
       objectoDePrueba.Errores.ContainsKey(pdiSinIndiceDeCiudadYConAttributo),
       Is.False,
       "Errores.ContainsKey(pdiSinIndiceDeCiudadYConAttributo)");
      
      Assert.That(
       objectoDePrueba.Errores.ContainsKey(pdiCiudadCorrecta),
       Is.False,
       "Errores.ContainsKey(pdiCiudadCorrecta)");
    }
  }
}
