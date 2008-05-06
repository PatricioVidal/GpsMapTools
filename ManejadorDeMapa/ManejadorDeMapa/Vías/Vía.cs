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

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Representa una Vía.
  /// </summary>
  public class Vía : Polilínea
  {
    #region Campos
    private static readonly CampoParámetrosDeRuta miCampoParámetrosDeRutaPorDefecto = new CampoParámetrosDeRuta(
      new LímiteDeVelocidad (0),
      new ClaseDeRuta (0),
      new bool[10]);

    private bool miTieneCampoParámetrosDeRutaEnCampos;
    #endregion 

    #region Propiedades
    /// <summary>
    /// Obtiene los Parámetros de Ruta.
    /// </summary>
    public CampoParámetrosDeRuta CampoParámetrosDeRuta { get; private set; }


    /// <summary>
    /// Obtiene los campos de nodos de ruta indexados por indice de coordenada.
    /// </summary>
    public CampoNodoDeRuta[] CamposNodosDeRuta { get; private set; }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número de la Polilínea.</param>
    /// <param name="laClase">La clase de la Polilínea.</param>
    /// <param name="losCampos">Los campos de la Polilínea.</param>
    public Vía(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IList<Campo> losCampos)
      : base(elManejadorDeMapa,
             elNúmero,
             laClase,
             losCampos)
    {
      CampoParámetrosDeRuta = miCampoParámetrosDeRutaPorDefecto;

      // Busca los campos específicos de las vías.
      foreach (Campo campo in losCampos)
      {
        CampoParámetrosDeRuta campoParámetrosDeRuta = campo as CampoParámetrosDeRuta;
        if (campoParámetrosDeRuta != null)
        {
          CampoParámetrosDeRuta = campoParámetrosDeRuta;
          miTieneCampoParámetrosDeRutaEnCampos = true;
        }
      }

      CreaCamposNodosDeRuta();
    }


    /// <summary>
    /// Cambia el Campo de Parámetros de Ruta.
    /// </summary>
    /// <param name="elCampoParámetrosDeRutaNuevo">El Campo de Parámetros de Ruta nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaCampoParámetrosDeRuta(CampoParámetrosDeRuta elCampoParámetrosDeRutaNuevo, string laRazón)
    {
      // Solo cambia el Límite de Velocidad si es diferente.
      if (elCampoParámetrosDeRutaNuevo.Equals(CampoParámetrosDeRuta))
      {
        return;
      }

      // Si no tiene Campo de Parámetros de Ruta entonces añadimos 
      // un Campo de Parámetros de Ruta con el nuevo Límite de Velocidad y  
      // una Clase de Ruta estándar.
      if (!miTieneCampoParámetrosDeRutaEnCampos)
      {
        // Añade el campo.
        AñadeCampo(elCampoParámetrosDeRutaNuevo, laRazón);
        CampoParámetrosDeRuta = elCampoParámetrosDeRutaNuevo;
        miTieneCampoParámetrosDeRutaEnCampos = true;
      }
      else
      {
        // Cambia el campo.
        CambiaCampo(elCampoParámetrosDeRutaNuevo, CampoParámetrosDeRuta, laRazón);
        CampoParámetrosDeRuta = elCampoParámetrosDeRutaNuevo;
      }
    }


    /// <summary>
    /// Añade un nodo de ruta.
    /// </summary>
    /// <param name="elIndice">El índice del nodo de ruta.</param>
    /// <param name="elIdentificadorGlobal">El identificador global.</param>
    /// <param name="laRazón">La razón.</param>
    public void AñadeNodoDeRuta(int elIndice, int elIdentificadorGlobal, string laRazón)
    {
      // Si ya tiene un nodo de ruta en el índice, pero con un identificador
      // global distinto entonces actualizamos el campo.
      // Si el identificador global es igual entonces no hacemos nada.
      CampoNodoDeRuta campoNodoDeRutaActual = CamposNodosDeRuta[elIndice];
      if (campoNodoDeRutaActual != null)
      {
        if (campoNodoDeRutaActual.IndentificadorGlobal != elIdentificadorGlobal)
        {
          // Actualiza el campo.
          CampoNodoDeRuta nuevoCampoNodoDeRuta = new CampoNodoDeRuta(
            campoNodoDeRutaActual.Identificador,
            campoNodoDeRutaActual.Número,
            elIndice,
            elIdentificadorGlobal,
            campoNodoDeRutaActual.EsExterno);
          CambiaCampo(nuevoCampoNodoDeRuta, campoNodoDeRutaActual, laRazón);
        }
      }
      else
      {
        #region Añade el nodo de ruta en la posición correcta.
        #region Ve si es necesario insertar el nodo de ruta.
        // El nuevo nodo de ruta hay que insertarlo si tiene un
        // índice de coordenadas menor que alguno de los nodos
        // de ruta que ya existen.
        List<CampoNodoDeRuta> camposNodosDeRuta = new List<CampoNodoDeRuta>();
        foreach (CampoNodoDeRuta campo in CamposNodosDeRuta)
        {
          if (campo != null)
          {
            camposNodosDeRuta.Add(campo);
          }
        }
        int númeroDeNodosDeRuta = camposNodosDeRuta.Count;
        int últimoIndice = númeroDeNodosDeRuta - 1;
        bool yaInsertóNodoDeRuta = false;
        for (int i = 0; i < númeroDeNodosDeRuta; ++i)
        {
          CampoNodoDeRuta campo = camposNodosDeRuta[i];
          if (elIndice < campo.IndiceDeCoordenadas)
          {
            // Inserta en nuevo nodo de ruta solo si todavía
            // no se ha insertado.
            if (!yaInsertóNodoDeRuta)
            {
              CampoNodoDeRuta nuevoCampoNodoDeRuta = new CampoNodoDeRuta(
                CampoNodoDeRuta.IdentificadorDeNodo,
                campo.Número,
                elIndice,
                elIdentificadorGlobal,
                false);
              CambiaCampo(nuevoCampoNodoDeRuta, campo, laRazón);
              yaInsertóNodoDeRuta = true;
            }

            #region Mueve los siguientes nodos.

            // Una vez que se insertó el nodo de ruta entonces
            // hay que mover todos los nodos siguientes.
            int siguienteIndice = i + 1;
            int siguienteNúmero = campo.Número + 1;
            CampoNodoDeRuta campoNodoDeRuta = new CampoNodoDeRuta(
              campo.Identificador,
              siguienteNúmero,
              campo.IndiceDeCoordenadas,
              campo.IndentificadorGlobal,
              campo.EsExterno);

            // Si el siguiente índice es válido entonces tenemos que cambiar
            // el nodo.
            // Si no, entonces tenemos que añadir el nodo.
            if (siguienteIndice <= últimoIndice)
            {
              CampoNodoDeRuta campoACambiar = camposNodosDeRuta[siguienteIndice];
              CambiaCampo(campoNodoDeRuta, campoACambiar, laRazón);
            }
            else
            {
              AñadeCampo(campoNodoDeRuta, laRazón);
            }

            #endregion
          }
        }

        #endregion

        // Si no se insertó el nodo de ruta quiere decir que debemos
        // añadirlo de último.
        if (!yaInsertóNodoDeRuta)
        {
          int númeroDeNodoDeRuta = númeroDeNodosDeRuta + 1;
          CampoNodoDeRuta nuevoCampoNodoDeRuta = new CampoNodoDeRuta(
            CampoNodoDeRuta.IdentificadorDeNodo,
            númeroDeNodoDeRuta,
            elIndice,
            elIdentificadorGlobal,
            false);
          AñadeCampo(nuevoCampoNodoDeRuta, laRazón);
        }

        #endregion
      }

      CreaCamposNodosDeRuta();
    }
    #endregion

    #region Métodos Privados
    private void CreaCamposNodosDeRuta()
    {
      CamposNodosDeRuta = new CampoNodoDeRuta[Coordenadas.Length];
      foreach (Campo campo in Campos)
      {
        CampoNodoDeRuta campoNodo = campo as CampoNodoDeRuta;
        if (campoNodo != null)
        {
          CamposNodosDeRuta[campoNodo.IndiceDeCoordenadas] = campoNodo;
        }
      }
    }
    #endregion
  }
}
