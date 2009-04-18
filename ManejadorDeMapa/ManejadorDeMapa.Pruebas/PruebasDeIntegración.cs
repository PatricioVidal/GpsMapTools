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

using System.Windows.Forms;
using System.Threading;
using System.IO;
using NUnit.Framework;
using NUnit.Extensions.Forms;
using GpsYv.ManejadorDeMapa.Interfase;

namespace GpsYv.ManejadorDeMapa.Pruebas
{
  /// <summary>
  /// Pruebas de integración.
  /// </summary>
  [TestFixture]
  public class PruebasDeIntegración
  {
    private const string miDirectorioDeData = @"..\..\Data\";

    private struct CasoDeProcesamientoDePdis
    {
      public string Archivo { get; private set; }
      public int Todos { get; private set; }
      public int Modificados { get; private set; }
      public int PosiblesDuplicados { get; private set; }
      public int Eliminados { get; private set; }
      public int Alertas { get; private set; }
      public int Errores { get; private set; }

      public CasoDeProcesamientoDePdis(
        string elArchivo,
        int losTodos,
        int losModificados,
        int losPosiblesDuplicados,
        int losEliminados,
        int lasAlertas,
        int losErrores)
        : this()
      {
        Archivo = elArchivo;
        Todos = losTodos;
        Modificados = losModificados;
        PosiblesDuplicados = losPosiblesDuplicados;
        Eliminados = losEliminados;
        Alertas = lasAlertas;
        Errores = losErrores;
      }
    }


    /// <summary>
    /// Prueba el menú "Procesar Todo".
    /// </summary>
    [Test]
    public void PruebaProcesamientoDeTodo()
    {
      #region Preparación.
      // Comienza applicación.
      InterfaseManejadorDeMapa interfaseManejadorDeMapa = new InterfaseManejadorDeMapa();
      interfaseManejadorDeMapa.Show();
      interfaseManejadorDeMapa.TopMost = true;

      // Crea los probadores de los elementos de la interfase.
      TabControlTester controladorDePestañasPrincipal = new TabControlTester("miControladorDePestañasPrincipal");
      TabControlTester controladorDePestañasDePdis = new TabControlTester("miInterfaseManejadorDePdis.miControladorDePestañas");
      TabControl.TabPageCollection pestañasPdis = controladorDePestañasDePdis.Properties.TabPages;
      TabPage pestañaTodos = pestañasPdis[1];
      TabPage pestañaModificados = pestañasPdis[2];
      TabPage pestañaEliminados = pestañasPdis[3];
      TabPage pestañaPosiblesDuplicados = pestañasPdis[4];
      TabPage pestañaAlertas = pestañasPdis[5];
      TabPage pestañaErrores = pestañasPdis[6];
      #endregion

      CasoDeProcesamientoDePdis[] casos = new[] {
        //                                Archivo, Todos, Modificados, Duplicados, Eliminados, Alertas, Errores
        new CasoDeProcesamientoDePdis( "58090.mp",  1713,         177,         20,          2,       0,      80),
        new CasoDeProcesamientoDePdis( "58170.mp",  6837,         545,         12,        189,       0,     230),
        new CasoDeProcesamientoDePdis( "58220.mp",  6460,         932,         34,         58,       0,     191),
        new CasoDeProcesamientoDePdis( "58370.mp",  1808,         235,         47,          8,       0,     252),
        new CasoDeProcesamientoDePdis( "58460.mp",   980,          85,        151,          4,       0,     225),
      };

      foreach (CasoDeProcesamientoDePdis caso in casos)
      {
        AbreArchivo(caso.Archivo);

        // Verifica el número de PDIs en las pestañas.
        string identificación = "[" + caso.Archivo + "]";
        Assert.AreEqual("Todos (" + caso.Todos + ")", pestañaTodos.Text, identificación + "PDIs.Todos.Text");
        Assert.AreEqual("Modificados (0)", pestañaModificados.Text, identificación + "PDIs.Modificados.Text");
        Assert.AreEqual("Eliminados (0)", pestañaEliminados.Text, identificación + "PDIs.Eliminados.Text");
        Assert.AreEqual("Posibles Duplicados", pestañaPosiblesDuplicados.Text, identificación + "PDIs.PosiblesDuplicados.Text");
        Assert.AreEqual("Alertas", pestañaAlertas.Text, identificación + "PDIs.Alertas.Text");
        Assert.AreEqual("Errores", pestañaErrores.Text, identificación + "PDIs.Errores.Text");

        // Selecciona la pestaña de PDIs.
        controladorDePestañasPrincipal.SelectTab(2);

        // Manda a procesar todo.
        ToolStripMenuItemTester menuProcesarTodo = new ToolStripMenuItemTester("miMenúProcesarTodo");
        menuProcesarTodo.Click();

        // Verifica el número de PDIs en las pestañas.
        Assert.AreEqual("Todos (" + caso.Todos + ")", pestañaTodos.Text, identificación + "PDIs.Todos.Text");
        Assert.AreEqual("Modificados (" + caso.Modificados + ")", pestañaModificados.Text, identificación + "PDIs.Modificados.Text");
        Assert.AreEqual("Eliminados (" + caso.Eliminados + ")", pestañaEliminados.Text, identificación + "PDIs.Eliminados.Text");
        Assert.AreEqual("Posibles Duplicados (" + caso.PosiblesDuplicados + ")", pestañaPosiblesDuplicados.Text, identificación + "PDIs.PosiblesDuplicados.Text");
        Assert.AreEqual("Alertas (" + caso.Alertas + ")", pestañaAlertas.Text, identificación + "PDIs.Alertas.Text");
        Assert.AreEqual("Errores (" + caso.Errores + ")", pestañaErrores.Text, identificación + "PDIs.Errores.Text");
      }

      // Cerrar la applicación.
      ToolStripMenuItemTester menúSalir = new ToolStripMenuItemTester("miMenuSalir");
      menúSalir.Click();
    }


    private static void AbreArchivo(string elArchivo)
    {
      // Crea el camino absoluto al archivo.
      string archivoParaAbrir = Path.Combine(miDirectorioDeData, elArchivo);
      archivoParaAbrir = Path.GetFullPath(archivoParaAbrir);

      // Instala un manejador que espere por la ventana de abrir archivo.
      using (ModalFormTester probadorDeForma = new ModalFormTester())
      {
        probadorDeForma.ExpectModal("Open", delegate 
          {
            OpenFileDialogTester formaAbrirArchivo = new OpenFileDialogTester("Open");

            // Manda a abrir el arhivo.
            // Por alguna razón desconocida, la llamada a OpenFile a veces no 
            // funciona. Llamamos la misma función 10 veces para 
            // asegurarnos que funciona.
            for (int i = 0; i < 10; ++i)
            {
              formaAbrirArchivo.OpenFile(archivoParaAbrir);
              Thread.Sleep(100);
            }
          }
        );

        // Selecciona el menú de abrir archivo.
        ToolStripMenuItemTester menuAbrirArchivo = new ToolStripMenuItemTester("miMenuAbrirArchivo");
        menuAbrirArchivo.Click();
      }
    }
  }
}
