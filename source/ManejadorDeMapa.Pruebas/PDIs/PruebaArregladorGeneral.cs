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
  /// Prueba de clase <see cref="ArregladorGeneral"/>.
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
      ManejadorDePdis manejadorDePdis = new ManejadorDePdis(manejadorDeMapa, new List<Pdi>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      var objectoDePrueba = new ArregladorGeneral(manejadorDePdis, escuchadorDeEstatus); 

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(0), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(0), "NúmeroDeElementosModificados");
    }

    private class Caso
    {
      public string Tipo { get; private set; }
      public string NombreOriginal { get; private set; }
      public string NombreCorregido { get; private set; }

      public Caso(
        string elTipo,
        string elNombreOriginal,
        string laNombreCorregido)
      {
        Tipo = elTipo;
        NombreOriginal = elNombreOriginal;
        NombreCorregido = laNombreCorregido;
      }
    }
  

    /// <summary>
    /// Prueba el método Procesa().
    /// </summary>
    [Test]
    public void PruebaProcesa()
    {
      #region Preparación.
      // Crea el objeto a probar.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePdis manejadorDePdis = new ManejadorDePdis(manejadorDeMapa, new List<Pdi>(), escuchadorDeEstatus);
      ArregladorGeneral objectoDePrueba = new ArregladorGeneral(manejadorDePdis, escuchadorDeEstatus);
     
      // Caso de prueba.
      Caso[] casos = new[] {
        //        Tipo,     Nombre Original, Nombre Corregido
        new Caso ("0x2a06", "RES LA COMIDA", "RESTAURANTE LA COMIDA"), // Cambia Nombre.
        new Caso ("0x2a07", "RES  LA  COMIDA", "RESTAURANTE LA COMIDA"), // Cambia nombre y elimina espacios.
        new Caso ("0x9999", "RES LA COMIDA", "RES LA COMIDA"),  // Este no debería cambiar porque el tipo no está en el rango.
        new Caso ("0x6402", "CONJ RES LAS TORRES", "CONJUNTO RESIDENCIAL LAS TORRES"), // Cambia Nombre.
      };
      const int númeroDeProblemasDetectados = 6;

      // Crea los elementos.
      IList<Pdi> pdis = manejadorDePdis.Elementos;
      const string clase = "POI";
      for (int i = 0; i < casos.Length; ++i)
      {
        Caso caso = casos[i];
        List<Campo> campos = new List<Campo> {
          new CampoNombre (caso.NombreOriginal),
          new CampoTipo (caso.Tipo)
        };

        Pdi pdi = new Pdi(manejadorDeMapa, i, clase, campos);
        pdis.Add(pdi);
      }
      #endregion

      // Llama al método bajo prueba.
      objectoDePrueba.Procesa();

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(pdis.Count), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(númeroDeProblemasDetectados), "NúmeroDeProblemasDetectados");

      // Prueba los nobres de los PDIs.
      for (int i = 0; i < casos.Length; ++i)
      {
        Assert.That(pdis[i].Nombre, Is.EqualTo(casos[i].NombreCorregido), "PDI[" + i + "].Nombre");
      }
    }
  }
}
