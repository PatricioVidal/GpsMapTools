﻿#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
//
// Este
//
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using GpsYv.ManejadorDeMapa.Interfase;

namespace GpsYv.ManejadorDeMapa.Pruebas
{
  [TestFixture]
  public class PruebasDeIntegración
  {
    private string miDirectorioDeData = @"..\..\..\Data\";

    private struct CasoDeProcesamientoDePDIs
    {
      public readonly string Archivo;
      public readonly int Todos;
      public readonly int Modificados;
      public readonly int PosiblesDuplicados;
      public readonly int Eliminados;
      public readonly int Errores;

      public CasoDeProcesamientoDePDIs(
        string elArchivo,
        int losTodos,
        int losModificados,
        int losPosiblesDuplicados,
        int losEliminados,
        int losErrores)
      {
        Archivo = elArchivo;
        Todos = losTodos;
        Modificados = losModificados;
        PosiblesDuplicados = losPosiblesDuplicados;
        Eliminados = losEliminados;
        Errores = losErrores;
      }
    }

    [Test]
    public void PruebaProcesamientoDePDIs()
    {
      #region Inicialización.
      // Comienza applicación.
      InterfaseManejadorDeMapa formaPrincipal = new InterfaseManejadorDeMapa();
      formaPrincipal.Show();
      formaPrincipal.TopMost = true;

      // Crea los probadores de los elementos de la interfase.
      TabControlTester controladorDePestañasPrincipal = new TabControlTester("miControladorDePestañasPrincipal");
      TabControlTester controladorDePestañasDePDIs = new TabControlTester("miControladorDePestañasDePDIs");
      TabControl.TabPageCollection pestañasPDIs = controladorDePestañasDePDIs.Properties.TabPages;
      TabPage pestañaTodos = pestañasPDIs[1];
      TabPage pestañaModificados = pestañasPDIs[2];
      TabPage pestañaPosiblesDuplicados = pestañasPDIs[3];
      TabPage pestañaEliminados = pestañasPDIs[4];
      TabPage pestañaErrores = pestañasPDIs[5];
      #endregion

      CasoDeProcesamientoDePDIs[] casos = new CasoDeProcesamientoDePDIs[] {
        //                                Archivo, Todos, Modificados, Duplicados, Eliminados, Errores
        new CasoDeProcesamientoDePDIs( "58090.mp",  1713,          87,         19,          2,     117),
        new CasoDeProcesamientoDePDIs( "58170.mp",  6837,         202,         10,        189,     400),
        new CasoDeProcesamientoDePDIs( "58220.mp",  6460,         680,         32,         58,     201),
        new CasoDeProcesamientoDePDIs( "58370.mp",  1808,          40,         46,          8,     335),
        new CasoDeProcesamientoDePDIs( "58460.mp",   980,          53,        151,          4,     235),
      };

      foreach (CasoDeProcesamientoDePDIs caso in casos)
      {
        AbreArchivo(caso.Archivo);

        // Verifica el número de PDIs en las pestañas.
        string identificación = "[" + caso.Archivo + "]";
        Assert.AreEqual("Todos (" + caso.Todos + ")", pestañaTodos.Text, identificación + "PDIs.Todos.Text");
        Assert.AreEqual("Modificados (0)", pestañaModificados.Text, identificación + "PDIs.Modificados.Text");
        Assert.AreEqual("Posibles Duplicados (0)", pestañaPosiblesDuplicados.Text, identificación + "PDIs.PosiblesDuplicados.Text");
        Assert.AreEqual("Eliminados (0)", pestañaEliminados.Text, identificación + "PDIs.Eliminados.Text");
        Assert.AreEqual("Errores (0)", pestañaErrores.Text, identificación + "PDIs.Errores.Text");

        // Selecciona la pestaña de PDIs.
        controladorDePestañasPrincipal.SelectTab(2);

        // Manda a procesar todos los PDIs.
        ToolStripMenuItemTester menuArreglarTodoEnPDIs = new ToolStripMenuItemTester("miMenuArreglarTodoEnPDIs");
        menuArreglarTodoEnPDIs.Click();

        // Verifica el número de PDIs en las pestañas.
        Assert.AreEqual("Todos (" + caso.Todos + ")", pestañaTodos.Text, identificación + "PDIs.Todos.Text");
        Assert.AreEqual("Modificados (" + caso.Modificados + ")", pestañaModificados.Text, identificación + "PDIs.Modificados.Text");
        Assert.AreEqual("Posibles Duplicados (" + caso.PosiblesDuplicados + ")", pestañaPosiblesDuplicados.Text, identificación + "PDIs.PosiblesDuplicados.Text");
        Assert.AreEqual("Eliminados (" + caso.Eliminados + ")", pestañaEliminados.Text, identificación + "PDIs.Eliminados.Text");
        Assert.AreEqual("Errores (" + caso.Errores + ")", pestañaErrores.Text, identificación + "PDIs.Errores.Text");
      }

      // Cerrar la applicación.
      ToolStripMenuItemTester menuSalir = new ToolStripMenuItemTester("miMenuSalir");
      menuSalir.Click();
    }


    private void AbreArchivo(string elArchivo)
    {
      // Crea el camino absoluto al archivo.
      string archivoParaAbrir = Path.Combine(miDirectorioDeData, elArchivo);
      archivoParaAbrir = Path.GetFullPath(archivoParaAbrir);

      // Instala un manejador que espere por la ventana de abrir archivo.
      using (ModalFormTester probadorDeForma = new ModalFormTester())
      {
        probadorDeForma.ExpectModal("Open", delegate()
        {
          // Dar tiempo para que aparezca la ventana de abrir el archivo.
          Thread.Sleep(500);

          // Manda a abrir el arhivo.
          OpenFileDialogTester formaAbrirArchivo = new OpenFileDialogTester("Open");
          formaAbrirArchivo.OpenFile(archivoParaAbrir);
        }
        );

        // Selecciona el menu de abrir archivo.
        ToolStripMenuItemTester menuAbrirArchivo = new ToolStripMenuItemTester("miMenuAbrirArchivo");
        menuAbrirArchivo.Click();
      }
    }
  }
}
