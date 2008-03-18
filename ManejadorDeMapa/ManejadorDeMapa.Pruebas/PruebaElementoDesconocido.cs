#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GpsYv.ManejadorDeMapa.Pruebas
{
  [TestFixture]
  public class PruebaElementoDesconocido
  {
    [Test]
    public void PruebaConstructor()
    {
      // Inicialización.
      int número = 12;
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(new EscuchadorDeEstatusPorOmisión());
      string clase = "clase";
      string nombre = "Nombre";
      string tipo = "0xc";
      List<Campo> campos = new List<Campo> { 
        new CampoNombre (nombre),
        new CampoComentario ("Comentario"),
        new CampoTipo (tipo)
      };
      
      // Llama al constructor.
      ElementoDesconocido objectoDePrueba = new ElementoDesconocido(manejadorDeMapa, número, clase, campos);

      // Prueba Propiedades.
      Assert.AreEqual(campos, objectoDePrueba.Campos, "Campos");
      Assert.AreEqual(clase, objectoDePrueba.Clase, "Clase");
      Assert.AreEqual(string.Empty, objectoDePrueba.Descripción, "Descripción");
      Assert.AreEqual(false, objectoDePrueba.FuéEliminado, "FuéEliminado");
      Assert.AreEqual(false, objectoDePrueba.FuéModificado, "FuéModificado");
      Assert.AreEqual(nombre, objectoDePrueba.Nombre, "Nombre");
      Assert.AreEqual(número, objectoDePrueba.Número, "Número");
      Assert.AreEqual(null, objectoDePrueba.Original, "Original");
      Assert.AreEqual(string.Empty, objectoDePrueba.RazónParaEliminación, "RazónParaEliminación");
      Assert.AreEqual(new Tipo(tipo), objectoDePrueba.Tipo, "Tipo");
    }


    [Test]
    public void PruebaElimina()
    {
      // Inicialización.
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(new EscuchadorDeEstatusPorOmisión());
      int número = 12;
      string clase = "clase";
      string nombre = "Nombre";
      List<Campo> campos = new List<Campo> { 
        new CampoNombre (nombre),
        new CampoComentario ("Comentario") 
      };
      ElementoDesconocido objectoDePrueba = new ElementoDesconocido(manejadorDeMapa, número, clase, campos);
      string razón = "Razón";

      // Llama el método a probar.
      objectoDePrueba.Elimina(razón);

      // Prueba Propiedades.
      Assert.AreEqual(campos, objectoDePrueba.Campos, "Campos");
      Assert.AreEqual(clase, objectoDePrueba.Clase, "Clase");
      Assert.AreEqual(string.Empty, objectoDePrueba.Descripción, "Descripción");
      Assert.AreEqual(true, objectoDePrueba.FuéEliminado, "FuéEliminado");
      Assert.AreEqual(false, objectoDePrueba.FuéModificado, "FuéModificado");
      Assert.AreEqual(nombre, objectoDePrueba.Nombre, "Nombre");
      Assert.AreEqual(número, objectoDePrueba.Número, "Número");
      Assert.AreEqual(null, objectoDePrueba.Original, "Original");
      Assert.AreEqual(razón, objectoDePrueba.RazónParaEliminación, "RazónParaEliminación");
      Assert.AreEqual(Tipo.TipoVacio, objectoDePrueba.Tipo, "Tipo");
    }


    [Test]
    public void PruebaCambiaNombre()
    {
      // Inicialización.
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(new EscuchadorDeEstatusPorOmisión());
      int número = 12;
      string clase = "clase";
      string nombre = "Nombre";
      List<Campo> campos = new List<Campo> { 
        new CampoNombre (nombre),
        new CampoComentario ("Comentario") 
      };
      ElementoDesconocido objectoDePrueba = new ElementoDesconocido(manejadorDeMapa, número, clase, campos);
      string nuevoNombre = "NuevoNombre";
      Assert.AreNotEqual(nuevoNombre, nombre);
      ElementoDesconocido original = (ElementoDesconocido)objectoDePrueba.Clone();

      // Llama el método a probar.
      objectoDePrueba.CambiaNombre(nuevoNombre, "Razón");

      // Prueba Propiedades.
      Assert.AreEqual(campos, objectoDePrueba.Campos, "Campos");
      Assert.AreEqual(clase, objectoDePrueba.Clase, "Clase");
      Assert.AreEqual(string.Empty, objectoDePrueba.Descripción, "Descripción");
      Assert.AreEqual(false, objectoDePrueba.FuéEliminado, "FuéEliminado");
      Assert.AreEqual(true, objectoDePrueba.FuéModificado, "FuéModificado");
      Assert.AreEqual(nuevoNombre, objectoDePrueba.Nombre, "Nombre");
      Assert.AreEqual(número, objectoDePrueba.Número, "Número");
      AseguraElementoEsEquivalente(original, objectoDePrueba.Original, "Original");
      Assert.AreEqual(string.Empty, objectoDePrueba.RazónParaEliminación, "RazónParaEliminación");
      Assert.AreEqual(Tipo.TipoVacio, objectoDePrueba.Tipo, "Tipo");
    }


    private void AseguraElementoEsEquivalente(ElementoDelMapa elEsperado, ElementoDelMapa elReal, string elPrefijo)
    {
      Assert.AreEqual(elEsperado.Campos, elReal.Campos, elPrefijo + ".Campos");
      Assert.AreEqual(elEsperado.Clase, elReal.Clase, elPrefijo + ".Clase");
      Assert.AreEqual(elEsperado.FuéEliminado, elReal.FuéEliminado, elPrefijo + ".FuéEliminado");
      Assert.AreEqual(elEsperado.FuéModificado, elReal.FuéModificado, elPrefijo + ".FuéModificado");
      Assert.AreEqual(elEsperado.Nombre, elReal.Nombre, elPrefijo + ".Nombre");
      Assert.AreEqual(elEsperado.Número, elReal.Número, elPrefijo + ".Número");
      Assert.AreEqual(elEsperado.RazónParaEliminación, elReal.RazónParaEliminación, elPrefijo + ".RazónParaEliminación");
      Assert.AreEqual(elEsperado.Tipo, elReal.Tipo, elPrefijo + ".Tipo");
    }
  }
}
