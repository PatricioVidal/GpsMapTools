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
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Pruebas.PDIs
{
  [TestFixture]
  public class PruebaBuscadorDeDuplicados
  {
    [Test]
    public void PruebaConstructor()
    {
      // Preparación.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePDIs manejadorDePDIs = new ManejadorDePDIs(manejadorDeMapa, new List<PDI>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      ArregladorDePalabrasPorTipo objectoDePrueba = new ArregladorDePalabrasPorTipo(manejadorDePDIs, escuchadorDeEstatus); 

      // Prueba propiedades.
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeElementoProcesándose, "NúmeroDeElementoProcesándose");
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeElementos, "NúmeroDeElementos");
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeProblemasDetectados, "NúmeroDeProblemasDetectados");
    }

    private struct Caso
    {
      public readonly string Tipo;
      public readonly string Nombre;
      public readonly double Latitud;
      public readonly double Longitud;
      public readonly int[] IndicesDeLosDuplicados;
      public readonly bool EsEliminado;
      public readonly string Descripción;

      public Caso(
        string elTipo,
        string elNombre,
        double laLatitud,
        double laLongitud,
        int[] losIndicesDeLosDuplicados,
        bool elEsEliminado,
        string laDescripción)
      {
        Tipo = elTipo;
        Nombre = elNombre;
        Latitud = laLatitud;
        Longitud = laLongitud;
        IndicesDeLosDuplicados = losIndicesDeLosDuplicados;
        EsEliminado = elEsEliminado;
        Descripción = laDescripción;
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
      BuscadorDeDuplicados objectoDePrueba = new BuscadorDeDuplicados(manejadorDePDIs, escuchadorDeEstatus);
     
      // Caso de prueba.
      Caso[] casos = new Caso[] {
        //        Tipo,     Nombre         ,  Latitud, Longitud, Indice de Duplicados   , Es Eliminado, Descripción.
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.00000, new int[] {2,3,6,7,9}  , false       , "PDI#1"),
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.00000, null                   , true        , "Idéntico: Mismos nombre, tipo, y coordenada de PDI#1 : es eliminado."),
        new Caso ("0x2a05", "El PUNTO"     , 10.00000, 20.00000, null                   , false       , "Mismo tipo principal: Mismo nombre, tipo principal, y coordenada de PDI#1: es duplicado."),
        new Caso ("0x2a06", "El PUNTO"     , 10.00001, 20.00000, null                   , false       , "Cercano: Mismo nombre y tipo de PDI#1, coordenadas cercanas: es duplicado."),
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.02000, null                   , false       , "Lejano: Mismo nombre y tipo de PDI#1, coordenadas lejanas: no es duplicado."),
        new Caso ("0x2b06", "El PUNTO"     , 10.00000, 20.00000, null                   , false       , "Diferente tipo principal: Mismo nombre y coordenada de PDI#1, diferente tipo principal: no es duplicado."),
        new Caso ("0x2a06", "EP"           , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1, misma coordenada de PDI#1: es duplicado."),
        new Caso ("0x2a06", "EP"           , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1, coordenadas cercanas: es duplicado."),
        new Caso ("0x2a06", " EP "         , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1 con espacios en blanco, coordenadas cercanas: no es duplicado."),
        new Caso ("0x2a06", "El PUMTO"     , 10.00000, 20.00000, null                   , false       , "Nombre similar a PDI#1, coordenadas cercanas: es duplicado."),
        new Caso ("0x2a06", "EOP"          , 15.00000, 20.00000, new int[] { 11 }       , false       , "PDI#2"),
        new Caso ("0x2a06", "EL OTRO PUNTO", 15.00000, 20.00000, null                   , false       , "PDI#2 es las siglas, misma coordenadas: es duplicado."),
      };
      int númeroDeDuplicadosDetectados = 2;

      // Crea los elementos.
      IList<PDI> elementos = manejadorDePDIs.Elementos;
      string clase = "POI";
      for (int i = 0; i < casos.Length; ++i)
      {
        Caso caso = casos[i];
        List<Campo> campos = new List<Campo> {
          new CampoNombre (caso.Nombre),
          new CampoTipo (caso.Tipo),
          new CampoCoordenadas (
            CampoCoordenadas.IdentificadorDeCoordenadas,
            0,
            new Coordenadas (caso.Latitud, caso.Longitud))
        };

        PDI pdi = new PDI(manejadorDeMapa, i, clase, campos);
        elementos.Add(pdi);
      }

      // Crea los duplicados.
      Dictionary<PDI, IList<PDI>> duplicados = new Dictionary<PDI, IList<PDI>>();
      for (int i = 0; i < casos.Length; ++i)
      {
        Caso caso = casos[i];
        if (caso.IndicesDeLosDuplicados != null)
        {
          List<PDI> pdisDuplicados = new List<PDI>();
          foreach (int j in caso.IndicesDeLosDuplicados)
          {
            pdisDuplicados.Add((PDI)elementos[j]);
          }

          PDI pdi = (PDI)elementos[i];
          duplicados.Add(pdi, pdisDuplicados);
        }
      }
      #endregion

      // Llama al método bajo prueba.
      objectoDePrueba.Procesa();

      // Prueba propiedades.
      AseguraDuplicadosSonIguales(duplicados, objectoDePrueba.GruposDeDuplicados, "GruposDeDuplicados");
      Assert.AreEqual(elementos.Count, objectoDePrueba.NúmeroDeElementoProcesándose, "NúmeroDeElementoProcesándose");
      Assert.AreEqual(elementos.Count, objectoDePrueba.NúmeroDeElementos, "NúmeroDeElementos");
      Assert.AreEqual(númeroDeDuplicadosDetectados, objectoDePrueba.NúmeroDeProblemasDetectados, "NúmeroDeProblemasDetectados");

      // Prueba que se hayan eliminados los PDIs idénticos.
      for (int i = 0; i < casos.Length; ++i)
      {
        Assert.AreEqual(casos[i].EsEliminado, elementos[i].FuéEliminado, "Elemento[" + i + "].FuéEliminado");
      }
    }


    private void AseguraDuplicadosSonIguales(
      IDictionary<PDI, IList<PDI>> elEsperado,
      IDictionary<PDI, IList<PDI>> elReal,
      string elPrefijo)
    {
      Assert.AreEqual(elEsperado.Count, elReal.Count, elPrefijo + ".Count");
      Assert.AreEqual(elEsperado.Keys, elReal.Keys, elPrefijo + ".Keys");

      foreach (PDI pdi in elEsperado.Keys)
      {
        Assert.AreEqual(elEsperado[pdi], elReal[pdi], elPrefijo + "['" + pdi + "']");
      }
    }
  }
}
