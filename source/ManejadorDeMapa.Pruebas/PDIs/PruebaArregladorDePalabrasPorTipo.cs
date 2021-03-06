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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GpsYv.ManejadorDeMapa.PDIs;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas.PDIs
{
  [TestFixture]
  public class PruebaArregladorDePalabrasPorTipo
  {
    [Test]
    public void PruebaConstructor()
    {
      // Preparación.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePDIs manejadorDePDIs = new ManejadorDePDIs(manejadorDeMapa, new List<PDI>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      BuscadorDeDuplicados objectoDePrueba = new BuscadorDeDuplicados(manejadorDePDIs, escuchadorDeEstatus); 

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementoProcesándose, Is.EqualTo(0), "NúmeroDeElementoProcesándose");
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(0), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(0), "NúmeroDeElementosModificados");
    }

    private struct Caso
    {
      public readonly string Tipo;
      public readonly string NombreOriginal;
      public readonly string NombreCorregido;

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
  

    [Test]
    public void PruebaProcesa()
    {
      #region Preparación.
      // Crea el objeto a probar.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePDIs manejadorDePDIs = new ManejadorDePDIs(manejadorDeMapa, new List<PDI>(), escuchadorDeEstatus);
      ArregladorDePalabrasPorTipo objectoDePrueba = new ArregladorDePalabrasPorTipo(manejadorDePDIs, escuchadorDeEstatus);
     
      // Caso de prueba.
      Caso[] casos = new Caso[] {
        //        Tipo,     Nombre Original, Nombre Corregido
        new Caso ("0x2a06", "RES LA COMIDA", "RESTAURANTE LA COMIDA"), // Cambia Nombre.
        new Caso ("0x2a07", "RES  LA  COMIDA", "RESTAURANTE LA COMIDA"), // Cambia nombre y elimina espacios.
        new Caso ("0x9999", "RES LA COMIDA", "RES LA COMIDA"),  // Este no debería cambiar porque el tipo no está en el rango.
        new Caso ("0x6402", "CONJ RES LAS TORRES", "CONJUNTO RESIDENCIAL LAS TORRES"), // Cambia Nombre.
      };
      int númeroDeProblemasDetectados = 6;

      // Crea los elementos.
      IList<PDI> pdis = manejadorDePDIs.Elementos;
      string clase = "POI";
      for (int i = 0; i < casos.Length; ++i)
      {
        Caso caso = casos[i];
        List<Campo> campos = new List<Campo> {
          new CampoNombre (caso.NombreOriginal),
          new CampoTipo (caso.Tipo)
        };

        PDI pdi = new PDI(manejadorDeMapa, i, clase, campos);
        pdis.Add(pdi);
      }
      #endregion

      // Llama al método bajo prueba.
      objectoDePrueba.Procesa();

      // Prueba propiedades.
      Assert.That(objectoDePrueba.NúmeroDeElementoProcesándose, Is.EqualTo(pdis.Count), "NúmeroDeElementoProcesándose");
      Assert.That(objectoDePrueba.NúmeroDeElementos, Is.EqualTo(pdis.Count), "NúmeroDeElementos");
      Assert.That(objectoDePrueba.NúmeroDeProblemasDetectados, Is.EqualTo(númeroDeProblemasDetectados), "NúmeroDeProblemasDetectados");

      // Prueba los nobres de los PDIs.
      for (int i = 0; i < casos.Length; ++i)
      {
        Assert.That(casos[i].NombreCorregido, Is.EqualTo(pdis[i].Nombre), "PDI[" + i + "].Nombre");
      }
    }
  }
}
