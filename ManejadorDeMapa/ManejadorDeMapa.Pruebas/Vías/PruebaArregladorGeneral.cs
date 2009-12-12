#region Copyright (c) GPS_YV (http://www.gpsyv.net)
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
using NUnit.Framework;
using GpsYv.ManejadorDeMapa.Vías;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas.Vías
{
  ///<summary>
  /// Prueba de clase <see cref="ArregladorGenera"/>.
  ///</summary>
  [TestFixture]
  public class PruebaArregladorGeneral
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
      ManejadorDeVías manejadorDePdis = new ManejadorDeVías(manejadorDeMapa, new List<Vía>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      ArregladorGeneral objectoDePrueba = new ArregladorGeneral(manejadorDePdis, escuchadorDeEstatus); 

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(0), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(0), "NúmeroDeElementosModificados");
    }

    private class Caso
    {
      public string Nombre { get; private set; }
      public string IndicadorDeDirección { get; private set; }
      public bool UnSoloSentido { get; private set; }
      public string IndicadorDeDirecciónEsperado { get; private set; }
      public bool UnSoloSentidoEsperado { get; private set; }

      public Caso(
       string elNombre,
       string elIndicadorDeDirección,
       bool elUnSoloSentido,
       string elIndicadorDeDirecciónEsperado,
       bool elUnSoloSentidoEsperado)
      {
        Nombre = elNombre;
        IndicadorDeDirección = elIndicadorDeDirección;
        UnSoloSentido = elUnSoloSentido;
        IndicadorDeDirecciónEsperado = elIndicadorDeDirecciónEsperado;
        UnSoloSentidoEsperado = elUnSoloSentidoEsperado;
      }
    }
  

    /// <summary>
    /// Prueba el método Procesa().
    /// </summary>
    [Test]
    public void PruebaProcesaCaso1()
    {
      #region Preparación.
      // Crea el objeto a probar.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDeVías manejadorDePdis = new ManejadorDeVías(manejadorDeMapa, new List<Vía>(), escuchadorDeEstatus);
      ArregladorGeneral objectoDePrueba = new ArregladorGeneral(manejadorDePdis, escuchadorDeEstatus);
     
      // Caso de prueba.
      Caso[] casos = new[] {
        //         Nombre, IndicadorDeDirección, UnSoloSentido, IndicadorDeDirecciónEsperado, UnSoloSentidoEsperado
        new Caso (    "A",                  "0",         false,                          "0",                 false), // Calle doble sentido.
        new Caso (    "B",                  "0",          true,                          "1",                  true), // Falta indicador de dirección.
        new Caso (    "C",                  "1",         false,                          "1",                  true), // Falta UnSoloSentido.
        new Caso (    "D",                  "1",          true,                          "1",                  true), // Calle un sentido.
      };
      const int númeroDeProblemasDetectados = 2;

      // Crea los elementos.
      // Vía típica:
      //   [POLYLINE]
      //   Type=0x2
      //   Label=TRONCAL 9
      //   EndLevel=3
      //   DirIndicator=1
      //   CityIdx=1
      //   RoadID=47
      //   RouteParam=5,3,1,0,0,0,0,0,0,0,0,0
      //   Data0=(10.16300,-66.00000),(10.16199,-65.99850),(10.16010,-65.99591)
      IList<Vía> vías = manejadorDePdis.Elementos;
      const string clase = "POI";
      for (int i = 0; i < casos.Length; ++i)
      {
        Caso caso = casos[i];
        List<Campo> campos = new List<Campo> {
          new CampoTipo("0x2"),
          new CampoNombre (caso.Nombre),
          new CampoGenérico("DirIndicator", caso.IndicadorDeDirección),
          new CampoGenérico("EndLevel", "3"),
          new CampoParámetrosDeRuta(
            new LímiteDeVelocidad(5), 
            new ClaseDeRuta(3), 
            new[] {caso.UnSoloSentido, false, false, false, false, false, false, false, false, false} ),
          new CampoCoordenadas("Data", 0, new[]
                                            {
                                              new Coordenadas(10.16300,-66.00000),
                                              new Coordenadas(10.16199,-65.99850),
                                              new Coordenadas(10.16010,-65.99591),
                                            })
        };

        Vía vía = new Vía(manejadorDeMapa, i, clase, campos);
        vías.Add(vía);
      }
      #endregion

      // Llama al método bajo prueba.
      objectoDePrueba.Procesa();

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(vías.Count), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(númeroDeProblemasDetectados), "NúmeroDeProblemasDetectados");

      // Prueba los nobres de los PDIs.
      for (int i = 0; i < casos.Length; ++i)
      {
        Assert.That(vías[i].CampoParámetrosDeRuta.OtrosParámetros[0], Is.EqualTo(casos[i].UnSoloSentidoEsperado), "Vía[" + i + "].OtrosParámetros[0]");
        Assert.That(vías[i].CampoIndicadorDeDirección, Is.Not.Null, "Vía[" + i + "].CampoIndicadorDeDirección");
        Assert.That(vías[i].CampoIndicadorDeDirección.Texto, Is.EqualTo(casos[i].IndicadorDeDirecciónEsperado), "Vía[" + i + "].CampoIndicadorDeDirección.Texto");
      }
    }
  }
}
