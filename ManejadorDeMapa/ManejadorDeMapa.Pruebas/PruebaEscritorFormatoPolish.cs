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
using NUnit.Framework;
using System.IO;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas
{
  [TestFixture]
  public class PruebaEscritorFormatoPolish
  {
    [Test]
    public void PruebaConstructor()
    {
      #region Caso 1: Operación Normal
      {
        #region Preparación.
        // Lee un mapa de prueba.
        const string archivoDeEntrada = @"..\..\Data\PruebaEscritorFormatoPolish.mp";
        const ManejadorDeMapa manejadorDeMapa = null;
        IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
        LectorDeFormatoPolish lector = new LectorDeFormatoPolish(manejadorDeMapa, archivoDeEntrada, escuchadorDeEstatus);
        IList<ElementoDelMapa> elementos = lector.ElementosDelMapa;

        const string archivoDeSalida = @"PruebaEscritorFormatoPolish.Caso1.mp";
        #endregion

        // Llama al constructor.
        new EscritorDeFormatoPolish(archivoDeSalida, elementos, escuchadorDeEstatus);

        #region Prueba Archivo de Salida.
        // Los archivos se comparan ignorando las líneas en blanco.
        StreamReader entrada = File.OpenText(archivoDeEntrada);
        StreamReader salida = File.OpenText(archivoDeSalida);
        while (true)
        {
          string lineaDeEntrada = LéePróximaLineaConInformación(entrada);
          string lineaDeSalida = LéePróximaLineaConInformación(salida);

          // Nos salimos si llegamos al final de los dos archivos.
          if ((lineaDeEntrada == null) && (lineaDeSalida == null))
          {
            break;
          }

          Assert.That(lineaDeEntrada, Is.Not.Null, "Linea en archivo de entrada.");
          Assert.That(lineaDeSalida, Is.Not.Null, "Linea en archivo de salida.");
          Assert.That(lineaDeSalida, Is.EqualTo(lineaDeEntrada), "Lineas de salida y entrada:");
        }
        #endregion
      }
      #endregion

      #region Caso 2: Archivo de Salida inválido.
      {
        #region Preparación.
        // Crea elementos.
        IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
        IList<ElementoDelMapa> elementos = new List<ElementoDelMapa>();
        
        // Archivo inválido.
        const string archivoDeSalidaInválido = @"ABC::\\&AAAA";

        // Excepción esperada.
        bool lanzóExcepción = false;
        ArgumentException excepciónEsperada = new ArgumentException(
          "The path is not of a legal form.");
        #endregion

        // Llama al constructor en prueba.
        try
        {
          new EscritorDeFormatoPolish(archivoDeSalidaInválido, elementos, escuchadorDeEstatus);
        }
        catch (Exception e)
        {
          // Prueba las propiedades de la excepción.
          Assert.That(e.GetType(), Is.EqualTo(excepciónEsperada.GetType()), "Tipo de Excepción");
          Assert.That(e.Message, Is.EqualTo(excepciónEsperada.Message), "Excepción.Message");

          lanzóExcepción = true;
        }

        Assert.That(lanzóExcepción, Is.True, "No se lanzó la excepción.");
      }
      #endregion

      #region Caso 3: Elemento con Campo Desconocido.
      {
        #region Preparación.
        // Crea elementos.
        IEscuchadorDeEstatus escuchadorDeEstatus = new EscuchadorDeEstatusPorOmisión();
        IList<ElementoDelMapa> elementos = new List<ElementoDelMapa> {
          new ElementoDesconocido(null, 0, "Clase", new List<Campo> {
            new CampoDesconocido() })};

        const string archivoDeSalida = @"PruebaEscritorFormatoPolish.Caso3.mp";

        // Excepción esperada.
        bool lanzóExcepción = false;
        ArgumentException excepciónEsperada = new ArgumentException(
          "Campo desconocido: GpsYv.ManejadorDeMapa.Pruebas.PruebaEscritorFormatoPolish+CampoDesconocido");
        #endregion

        // Llama al constructor en prueba.
        try
        {
          new EscritorDeFormatoPolish(archivoDeSalida, elementos, escuchadorDeEstatus);
        }
        catch (Exception e)
        {
          // Prueba las propiedades de la excepción.
          Assert.That(e.GetType(), Is.EqualTo(excepciónEsperada.GetType()), "Tipo de Excepción");
          Assert.That(e.Message, Is.EqualTo(excepciónEsperada.Message), "Excepción.Message");

          lanzóExcepción = true;
        }

        Assert.That(lanzóExcepción, Is.True, "No se lanzó la excepción.");
      }
      #endregion

    }

    private static string LéePróximaLineaConInformación(TextReader elStream)
    {
      string linea;
      do
      {
        linea = elStream.ReadLine();
      }
      while ((linea != null) && (linea == string.Empty));

      return linea;
    }

    #region Clases para Pruebas
    private class CampoDesconocido : Campo
    {
      public CampoDesconocido()
        : base ("ID")
      {
      }

      public override bool Equals(object elObjecto)
      {
        throw new NotImplementedException();
      }

      public override int GetHashCode()
      {
        throw new NotImplementedException("Método GetHashCode() no está implementado.");
      }

      public override string ToString()
      {
        throw new NotImplementedException();
      }
    }
    #endregion
  }
}
