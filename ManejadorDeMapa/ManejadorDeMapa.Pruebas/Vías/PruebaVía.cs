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
using GpsYv.ManejadorDeMapa.Vías;
using NUnit.Framework.SyntaxHelpers;

namespace GpsYv.ManejadorDeMapa.Pruebas.Vías
{
  [TestFixture]
  public class PruebaVía
  {
    /// <summary>
    /// Prueba la clase Vía.
    /// </summary>
    [Test]
    public void PruebaConstructor()
    {
      // Preparación.
      int número = 12;
      ManejadorDeMapa manejadorDeMapa = new ManejadorDeMapa(new EscuchadorDeEstatusPorOmisión());
      string clase = "clase";
      string nombre = "Nombre";
      string tipo = "0xc";
      string descripción = "Roundabout";
      LímiteDeVelocidad límiteDeVelocidad = new LímiteDeVelocidad(2);
      ClaseDeRuta claseDeRuta = new ClaseDeRuta(3);
      List<Campo> campos = new List<Campo> { 
        new CampoNombre (nombre),
        new CampoComentario ("Comentario"),
        new CampoTipo (tipo),
        new CampoParámetrosDeRuta(límiteDeVelocidad, claseDeRuta)
      };

      // Llama al constructor.
      Vía objectoEnPrueba = new Vía(manejadorDeMapa, número, clase, campos);

      // Prueba Propiedades.
      Assert.That(campos, Is.EqualTo(objectoEnPrueba.Campos), "Campos");
      Assert.That(clase, Is.EqualTo(objectoEnPrueba.Clase), "Clase");
      Assert.That(descripción, Is.EqualTo(objectoEnPrueba.Descripción), "Descripción");
      Assert.That(objectoEnPrueba.FuéEliminado, Is.False, "FuéEliminado");
      Assert.That(objectoEnPrueba.FuéModificado, Is.False, "FuéModificado");
      Assert.That(nombre, Is.EqualTo(objectoEnPrueba.Nombre), "Nombre");
      Assert.That(número, Is.EqualTo(objectoEnPrueba.Número), "Número");
      Assert.That(objectoEnPrueba.Original, Is.Null, "Original");
      Assert.That(string.Empty, Is.EqualTo(objectoEnPrueba.RazónParaEliminación), "RazónParaEliminación");
      Assert.That(new Tipo(tipo), Is.EqualTo(objectoEnPrueba.Tipo), "Tipo");
      Assert.That(objectoEnPrueba.ClaseDeRuta.Indice, Is.EqualTo(claseDeRuta.Indice), "ClaseDeRuta.Indice");
      Assert.That(objectoEnPrueba.LímiteDeVelocidad.Indice, Is.EqualTo(límiteDeVelocidad.Indice), "LímiteDeVelocidad.Indice");
    }
  }
}
