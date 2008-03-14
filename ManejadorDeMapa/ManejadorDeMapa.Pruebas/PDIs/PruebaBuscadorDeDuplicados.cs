#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GpsYv.ManejadorDeMapa;

namespace GpsYv.ManejadorDeMapa.PDIs.Pruebas
{
  [TestFixture]
  public class PruebaBuscadorDeDuplicados
  {
    [Test]
    public void PruebaConstructor()
    {
      // Inicialización.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePDIs manejadorDePDIs = new ManejadorDePDIs(manejadorDeMapa, new List<PDI>(), escuchadorDeEstatus);

      // Llama al contructor bajo prueba.
      BuscadorDeDuplicados objectoDePrueba = new BuscadorDeDuplicados(manejadorDePDIs, escuchadorDeEstatus); 

      // Prueba propiedades.
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeElementoProcesándose, "NúmeroDeElementoProcesándose");
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeElementos, "NúmeroDeElementos");
      Assert.AreEqual(0, objectoDePrueba.NúmeroDeElementosModificados, "NúmeroDeElementosModificados");
    }

    private struct Caso
    {
      public string Tipo;
      public string Nombre;
      public double Latitud;
      public double Longitud;
      public int[] IndicesDeLosDuplicados;
      public bool EsEliminado;
      string Descripción;

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
      #region Inicialización.
      // Crea el objeto a probar.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      ManejadorDePDIs manejadorDePDIs = new ManejadorDePDIs(manejadorDeMapa, new List<PDI>(), escuchadorDeEstatus);
      BuscadorDeDuplicados objectoDePrueba = new BuscadorDeDuplicados(manejadorDePDIs, escuchadorDeEstatus);
     
      // Caso de prueba.
      Caso[] casos = new Caso[] {
        //        Tipo,     Nombre         ,  Latitud, Longitud, Indice de Duplicados   , Es Eliminado, Descripción.
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.00000, new int[] { 2, 5, 6, 8 }  , false       , "PDI#1"),
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.00000, null                   , true        , "Mismo PDI#1 nombre, misma coordenada: es eliminado."),
        new Caso ("0x2a06", "El PUNTO"     , 10.00001, 20.00000, null                   , false       , "Mismo PDI#1 nombre, cercano: es duplicado."),
        new Caso ("0x2a06", "El PUNTO"     , 10.00000, 20.02000, null                   , false       , "Mismo PDI#1 nombre, lejano: no es duplicado."),
        new Caso ("0x2a05", "El PUNTO"     , 10.00000, 20.00000, null                   , false       , "Diferente tipo, mismo PDI#1 nombre, misma coordenada: no es duplicado."),
        new Caso ("0x2a06", "EP"           , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1, misma coordenada: es duplicado."),
        new Caso ("0x2a06", "EP"           , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1, cercano: es duplicado."),
        new Caso ("0x2a06", " EP "         , 10.00000, 20.00000, null                   , false       , "Siglas de PDI#1 con espacios en blanco, cercano: no es duplicado."),
        new Caso ("0x2a06", "El PUMTO"     , 10.00000, 20.00000, null                   , false       , "Nombre similar a PDI#1, cercano: es duplicado."),
        new Caso ("0x2a06", "EOP"          , 15.00000, 20.00000, new int[] { 10 }       , false       , "PDI#2"),
        new Caso ("0x2a06", "EL OTRO PUNTO", 15.00000, 20.00000, null                   , false       , "PDI#2 es las siglas, misma coordenadas: es duplicado."),
      };
      int númeroDeElementosEliminados = 1;

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

        PDI pdi = new PDI(manejadorDeMapa, i + 1, clase, campos);
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
      AseguraDuplicadosSonIguales(duplicados, objectoDePrueba.Manejador.GruposDeDuplicados, "Duplicados");
      Assert.AreEqual(elementos.Count, objectoDePrueba.NúmeroDeElementoProcesándose, "NúmeroDeElementoProcesándose");
      Assert.AreEqual(elementos.Count, objectoDePrueba.NúmeroDeElementos, "NúmeroDeElementos");
      Assert.AreEqual(númeroDeElementosEliminados, objectoDePrueba.NúmeroDeElementosModificados, "NúmeroDeElementosModificados");

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
