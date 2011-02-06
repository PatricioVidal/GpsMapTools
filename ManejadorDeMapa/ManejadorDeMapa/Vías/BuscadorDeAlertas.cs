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
using System.Text;
using Tst;
using System.Collections;
using SWallTech.Drawing.Shapes;

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Buscador de alertas en Vías.
  /// </summary>
  public class BuscadorDeAlertas : ProcesadorBase<ManejadorDeVías, Vía>
  {
    #region Campos
    private readonly IDictionary<Vía, IList<string>> misAlertas = new Dictionary<Vía, IList<string>>();
    private readonly List<Vía> misVíasYaProcesadas = new List<Vía>();
    private PolygonF misLímitesDelMapa = null;
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve las alertas de Vías.
    /// </summary>
    public IDictionary<Vía, IList<string>> Alertas
    {
      get
      {
        return misAlertas;
      }
    }


    /// <summary>
    /// Atributo "NoParametrosDeRutaStandard".
    /// </summary>
    /// <remarks>
    /// Este atributo indica que la vía debe excluirse de la regla de 
    /// Parámetros de Ruta estándar.
    /// </remarks>
    public const string AtributoNoParámetrosDeRutaEstándar = "NoParámetrosDeRutaEstándar";
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Descripción de éste procesador.
    /// </summary>
    public static readonly string Descripción = "Busca alertas en las Vías.";


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeVías">El manejador de Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeAlertas(
      ManejadorDeVías elManejadorDeVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeVías, elEscuchadorDeEstatus)
    {
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override void ComenzóAProcesar()
    {
      misAlertas.Clear();
      misVíasYaProcesadas.Clear();

      // Obtiene los límites del mapa.
      if (ManejadorDeMapa.LímitesDelMapa != null)
      {
        misLímitesDelMapa = new PolygonF(ManejadorDeMapa.LímitesDelMapa);
      }
      else
      {
        misLímitesDelMapa = null;
      }
      
      base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa una Vía.
    /// </summary>
    /// <param name="laVía">La Vía.</param>
    /// <returns>El número de problemas detectados al procesar el elemento.</returns>
    protected override int ProcesaElemento(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      númeroDeProblemasDetectados += BuscaVíasConParámetrosDeRutaInválidos(laVía);
      númeroDeProblemasDetectados += BuscaVíaFueraDeLímites(laVía);

      return númeroDeProblemasDetectados;
    }


    private int BuscaVíaFueraDeLímites(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      // Retorna si el mapa no tiene límites.
      if (misLímitesDelMapa == null)
      {
        return númeroDeProblemasDetectados;
      }

      // Busca si alguna de las coordenadas de la vía está fuera de los límites del mapa.
      foreach (Coordenadas coordenadas in laVía.Coordenadas)
      {
        if (!misLímitesDelMapa.Contains(coordenadas))
        {
          ++númeroDeProblemasDetectados;
          misAlertas.Add(laVía, new List<string>(){"A101: Vía fuera de límites del mapa."});
          break;
        }
      }

      return númeroDeProblemasDetectados;
    }


    private int BuscaVíasConParámetrosDeRutaInválidos(Vía laVía)
    {
      int númeroDeProblemasDetectados = 0;

      // Excluye la vía si tiene el Atributo "NoParámetrosDeRutaEstándar".
      if (laVía.TieneAtributo(AtributoNoParámetrosDeRutaEstándar))
      {
        return númeroDeProblemasDetectados;
      }

      IList<string> alertas = new List<string>();

      #region Valida el límite de velocidad.
      // El límite de velocidad debe ser el esperado.
      Tipo? tipo = laVía.Tipo;
      if (tipo != null)
      {
        if (RestriccionesDeParámetrosDeRuta.LímitesDeVelocidad.ContainsKey((Tipo)tipo))
        {
          LímiteDeVelocidad límiteDeVelocidad = laVía.CampoParámetrosDeRuta.LímiteDeVelocidad;
          LímiteDeVelocidad límiteDeVelocidadEsperado = RestriccionesDeParámetrosDeRuta.LímitesDeVelocidad[(Tipo)tipo];
          if (límiteDeVelocidad != límiteDeVelocidadEsperado)
          {
            alertas.Add(string.Format("A102: Límite de Velocidad debería ser {0}, pero es {1}.", límiteDeVelocidadEsperado, límiteDeVelocidad));
          }
        }
      }
      #endregion

      #region Valida la clase de ruta.
      // La clase de ruta debe ser la esperado.
      if (tipo != null)
      {
        if (RestriccionesDeParámetrosDeRuta.ClasesDeRuta.ContainsKey((Tipo)tipo))
        {
          ClaseDeRuta claseDeRuta = laVía.CampoParámetrosDeRuta.ClaseDeRuta;
          ClaseDeRuta claseDeRutaEsperada = RestriccionesDeParámetrosDeRuta.ClasesDeRuta[(Tipo)tipo];
          if (claseDeRuta != claseDeRutaEsperada)
          {
            alertas.Add(string.Format("A103: Clase de Ruta debería ser {0}, pero es {1}.", claseDeRutaEsperada, claseDeRuta));
          }
        }
      }
      #endregion

      if (alertas.Count > 0)
      {
        ++númeroDeProblemasDetectados;
        misAlertas.Add(laVía, alertas);
      }

      return númeroDeProblemasDetectados;
    }


    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      misAlertas.Clear();
      misVíasYaProcesadas.Clear();

      // Pone al Procesador en estado inválido.
      Invalida();
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Pone al Procesador en estado inválido.
      Invalida();
    }
    #endregion
  }
}
