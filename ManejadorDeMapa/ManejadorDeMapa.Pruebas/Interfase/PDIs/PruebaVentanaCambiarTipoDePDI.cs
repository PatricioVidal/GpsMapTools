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

using System.Collections.Generic;
using NUnit.Framework;
using GpsYv.ManejadorDeMapa.Interfase.Pdis;
using GpsYv.ManejadorDeMapa;
using System.Windows.Forms;
using NUnit.Extensions.Forms;
using GpsYv.ManejadorDeMapa.Pdis;

namespace GpsYv.ManejadorDeMapa.Pruebas.Interfase.Pdis
{
  /// <summary>
  /// Clase para probar la Venatana de Cambiar el Tipo de PDI.
  /// </summary>
  [TestFixture]
  public class PruebaVentanaCambiarTipoDePdi
  {
    /// <summary>
    /// Prueba el método ShowDialog().
    /// </summary>
    [Test]
    public void PruebaShowDialog()
    {
      #region Preparación general.
      // Crea un PDI para las pruebas.
      IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(escuchadorDeEstatus);
      string nombre = "Nombre";
      Tipo tipo = new Tipo("0xaaaa");
      const string clase = "POI";
      Coordenadas coordenadas = new Coordenadas(20, 30);
      List<Campo> campos = new List<Campo> {
          new CampoNombre (nombre),
          new CampoTipo (tipo),
          new CampoCoordenadas (CampoCoordenadas.IdentificadorDeCoordenadas, 0, coordenadas)
        };
      List<Pdi> pdis = new List<Pdi>{new Pdi(manejadorDeMapa, 1, clase, campos)};
      #endregion

      #region Caso 1: Apenas aparece la ventana el usuario apreta "Cambiar"
      {
        // En este caso el método Show() debería retornar None,
        // Y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Apreta el botón de "Cambiar". 
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.None, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 2: Apenas aparece la ventana el usuario apreta "Cancelar"
      {
        // En este caso el método Show() debería retornar Cancel,
        // Y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCancelar = new ButtonTester("miBotónCancelar", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Apreta el botón de "Cancelar". 
        botónCancelar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.Cancel, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 3: El usuario cambia el tipo inicial por uno válido.
      {
        // En este caso el método Show() debería retornar OK,
        // y la propiedad TipoNuevo debería ser el tipo nuevo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea un tipo válido.
        string númeroTipoNuevoVálido = "100";
        Tipo tipoNuevoVálido = new Tipo("0x" + númeroTipoNuevoVálido);
        Assert.IsTrue(CaracterísticasDePdis.Descripciones.ContainsKey(tipoNuevoVálido), "El tipo nuevo debe ser conocido.");

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario poniendo un tipo válido.
        textoTipoNuevo.Properties.Focus();
        textoTipoNuevo.Enter(númeroTipoNuevoVálido);

        // Apreta el botón de "Cambiar". 
        botónCambiar.Properties.Focus();
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.OK, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(tipoNuevoVálido, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 4: El usuario cambia el tipo inicial por uno desconocido.
      {
        // En este caso el método Show() debería retornar None,
        // y la propiedad TipoNuevo debería ser el tipo desconocido.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea un tipo desconocido.
        string númeroTipoNuevoDesconocido = "bbbb";
        Tipo tipoNuevoDesconocido = new Tipo("0x" + númeroTipoNuevoDesconocido);
        Assert.IsFalse(CaracterísticasDePdis.Descripciones.ContainsKey(tipoNuevoDesconocido), "El tipo nuevo debe ser desconocido.");

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario poniendo un tipo desconocido.
        textoTipoNuevo.Properties.Focus();
        textoTipoNuevo.Enter(númeroTipoNuevoDesconocido);

        // Apreta el botón de "Cambiar". 
        botónCambiar.Properties.Focus();
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.None, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(tipoNuevoDesconocido, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 5: El usuario cambia el tipo inicial por uno inválido.
      {
        // En este caso el método Show() debería retornar None,
        // y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea un tipo inválido.
        // Un tipo inválido es aquel que genera una excepción cuando
        // se trata de construir una clase Tipo con el.
        string númeroTipoNuevoInválido = "ww";
        bool tipoEsInválido = false;
        try
        {
          new Tipo("0x" + númeroTipoNuevoInválido);
        }
        catch
        {
          tipoEsInválido = true;
        }
        Assert.IsTrue(tipoEsInválido, "El tipo nuevo debe ser inválido.");

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario poniendo un tipo inválido.
        textoTipoNuevo.Properties.Focus();
        textoTipoNuevo.Enter(númeroTipoNuevoInválido);

        // Apreta el botón de "Cambiar". 
        botónCambiar.Properties.Focus();
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.None, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 6: El usuario empiezar a cambiar el tipo, pero al final lo borra y lo deja en blanco.
      {
        // En este caso el método Show() debería retornar None,
        // y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario empezando a cambiar el tipo y despues dejandolo en blanco.
        textoTipoNuevo.Properties.Focus();
        textoTipoNuevo.Enter("123");
        textoTipoNuevo.Properties.Text = string.Empty;

        // Apreta el botón de "Cambiar". 
        botónCambiar.Properties.Focus();
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.None, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");
        Assert.AreEqual(pdis, objectoDePrueba.Pdis, "PDIs");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 7: El usuario empiezar a cambiar el tipo, pero al final cancela.
      {
        // En este caso el método Show() debería retornar Cancel,
        // y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = pdis;

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCancelar = new ButtonTester("miBotónCancelar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario empezando a cambiar el tipo y despues dejandolo en blanco.
        textoTipoNuevo.Enter("123");
        textoTipoNuevo.Properties.Text = string.Empty;

        // Apreta el botón de "Cancelar". 
        botónCancelar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.Cancel, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion

      #region Caso 8: Se usa con un PDI nulo.
      {
        // En este caso el método Show() debería retornar None,
        // y la propiedad TipoNuevo debería ser el tipo nulo.

        #region Preparación.
        // Inicializa objecto de prueba.
        VentanaCambiarTipoDePdi objectoDePrueba = new VentanaCambiarTipoDePdi();
        objectoDePrueba.Pdis = null;

        // Crea los probadores de los elementos de la interfase.
        ButtonTester botónCambiar = new ButtonTester("miBotónCambiar", objectoDePrueba);
        TextBoxTester textoTipoNuevo = new TextBoxTester("miTextoTipoNuevo", objectoDePrueba);
        #endregion

        // Llama al método a probar.
        objectoDePrueba.Show();

        // Simula el usuario empezando a cambiar el tipo y despues dejandolo en blanco.
        textoTipoNuevo.Properties.Focus();
        textoTipoNuevo.Enter("100");
        textoTipoNuevo.Properties.Text = string.Empty;

        // Apreta el botón de "Cambiar". 
        botónCambiar.Properties.Focus();
        botónCambiar.Click();

        // Probar propiedades.
        Assert.AreEqual(DialogResult.None, objectoDePrueba.DialogResult, "DialogResult");
        Assert.AreEqual(Tipo.TipoNulo, objectoDePrueba.TipoNuevo, "TipoNuevo");

        // Cierra la ventana.
        objectoDePrueba.Close();
      }
      #endregion
    }
  }
}
