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

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un Campo de Parámetros de Ruta.
  /// </summary>
  public class CampoParámetrosDeRuta : Campo
  {
    #region Campos
    private readonly ClaseDeRuta miClaseDeRuta;
    private readonly LímiteDeVelocidad miLímiteDeVelocidad;
    private const int NúmeroDeOtrosParámetros = 10;
    private readonly bool[] misOtrosParámetros;
    #endregion

    #region Propiedades
    /// <summary>
    /// Identificador.
    /// </summary>
    public const string IdentificadorDeParámetrosDeRuta = "RouteParam";


    /// <summary>
    /// Obtiene la clase de ruta.
    /// </summary>
    public ClaseDeRuta ClaseDeRuta
    {
      get
      {
        return miClaseDeRuta;
      }
    }


    /// <summary>
    /// Obtiene el Límite de Velocidad.
    /// </summary>
    public LímiteDeVelocidad LímiteDeVelocidad
    {
      get
      {
        return miLímiteDeVelocidad;
      }
    }


    /// <summary>
    /// Obtiene los otros parámetros.
    /// </summary>
    public bool[] OtrosParámetros
    {
      get
      {
        return misOtrosParámetros;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elTextoDeParámetrosDeRuta">El texto de los parámetros de ruta.</param>
    public CampoParámetrosDeRuta(
      string elTextoDeParámetrosDeRuta)
      : base(IdentificadorDeParámetrosDeRuta)
    {
      string[] partes = elTextoDeParámetrosDeRuta.Split(',');

      // Verifica el número de partes.
      const int númeroDePartes = 12;
      if (partes.Length < númeroDePartes)
      {
        throw new ArgumentException(string.Format("Los parámetros de rutas deben tener " +
          "{0} elementos separados por coma, pero es: {1}", númeroDePartes, elTextoDeParámetrosDeRuta));
      }

      // Lée los parametros.
      miLímiteDeVelocidad = new LímiteDeVelocidad(Convert.ToInt32(partes[0]));
      miClaseDeRuta = new ClaseDeRuta(Convert.ToInt32(partes[1]));
      misOtrosParámetros = new bool[NúmeroDeOtrosParámetros];
      int otrosParámetrosIndiceOffset = 2;
      for (int i = 0; i < NúmeroDeOtrosParámetros; ++i)
      {
        int valor = Convert.ToInt32(partes[i + otrosParámetrosIndiceOffset]);
        switch (valor)
        {
          case 0:
            misOtrosParámetros[i] = false;
            break;
          case 1:
            misOtrosParámetros[i] = true;
            break;
          default:
            throw new ArgumentException(
              string.Format("El números de los parámetros de ruta para el tercer elemento en adelante tiene que ser 0 ó 1: {0}", elTextoDeParámetrosDeRuta),
              "elTextoDeParámetrosDeRuta");
        }
      }
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elLímiteDeVelocidad">El Límite de Velocidad</param>
    /// <param name="laClaseDeRuta">La Clase de Ruta</param>
    public CampoParámetrosDeRuta(
      LímiteDeVelocidad elLímiteDeVelocidad,
      ClaseDeRuta laClaseDeRuta,
      bool[] losOtrosParámetros)
      : base (IdentificadorDeParámetrosDeRuta)
    {
      if (losOtrosParámetros.Length != NúmeroDeOtrosParámetros)
      {
        throw new ArgumentException(
          string.Format("El números de Otrós Parámetros debe ser {0} pero es {1}", NúmeroDeOtrosParámetros, losOtrosParámetros.Length),
          "losOtrosParámetros");
      }

      misOtrosParámetros = losOtrosParámetros;
      miLímiteDeVelocidad = elLímiteDeVelocidad;
      miClaseDeRuta = laClaseDeRuta;
    }


    /// <summary>
    /// Devuelve un texto representando el campo.
    /// </summary>
    public override string ToString()
    {
      char separador = ',';
      StringBuilder texto = new StringBuilder();
      texto.Append(miLímiteDeVelocidad.Indice);
      texto.Append(separador);
      texto.Append(miClaseDeRuta.Indice);
      foreach (bool valor in misOtrosParámetros)
      {
        texto.Append(separador);
        if (valor)
        {
          texto.Append(1);
        }
        else
        {
          texto.Append(0);
        }
      }

      return texto.ToString();
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si un objeto
    /// dado es igual.
    /// </summary>
    /// <param name="elObjecto">El objecto dado.</param>
    public override bool Equals(object elObjecto)
    {
      // Si el objeto es nulo entonces no puede ser igual.
      if (elObjecto == null)
      {
        return false;
      }

      // Si el objecto no es del mismo tipo entonces no puede ser igual.
      if (!(elObjecto is CampoParámetrosDeRuta))
      {
        return false;
      }

      // Compara latitud y longitud.
      CampoParámetrosDeRuta comparador = (CampoParámetrosDeRuta)elObjecto;
      bool esIgual = (this.ToString() == comparador.ToString());

      return esIgual;
    }


    /// <summary>
    /// Obtiene una clave única para este objecto.
    /// </summary>
    public override int GetHashCode()
    {
      throw new NotImplementedException("Método GetHashCode() no está implementado.");
    }    
    #endregion
  }
}
