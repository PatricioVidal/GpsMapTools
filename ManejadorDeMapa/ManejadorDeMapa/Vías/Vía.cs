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
    private CampoNodoRuteable[] misCamposNodosRuteables;
    private CampoNombre miCampoNombreSecundario;
    #endregion 

    #region Propiedades
    /// <summary>
    /// Obtiene los Parámetros de Ruta.
    /// </summary>
    public CampoParámetrosDeRuta CampoParámetrosDeRuta { get; private set; }


    /// <summary>
    /// Obtiene los nodos.
    /// </summary>
    public Nodo[] Nodos { get; private set; }


    /// <summary>
    /// Obtiene el nombre secundario.
    /// </summary>
    public string NombreSecundario
    {
       get
       {
         // Devuelve nulo si no tenemos el campo.
         if (miCampoNombreSecundario == null)
         {
           return null;
         }

         return miCampoNombreSecundario.Nombre;
       }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número de la <see cref="Polilínea"/>.</param>
    /// <param name="laClase">La clase de la <see cref="Polilínea"/>.</param>
    /// <param name="losCampos">Los campos de la <see cref="Polilínea"/>.</param>
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
        CampoNombre campoNombre;
        if (campoParámetrosDeRuta != null)
        {
          CampoParámetrosDeRuta = campoParámetrosDeRuta;
          miTieneCampoParámetrosDeRutaEnCampos = true;
        }
        else if ((campoNombre = campo as CampoNombre) != null)
        {
          if (campoNombre.Número == 2)
          {
            miCampoNombreSecundario = campoNombre;
          }
        }
      }

      CreaCamposNodosRuteables();
    }


    /// <summary>
    /// Cambia el Campo de Parámetros de Ruta.
    /// </summary>
    /// <param name="elCampoParámetrosDeRutaNuevo">El Campo de Parámetros de Ruta nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaCampoParámetrosDeRuta(CampoParámetrosDeRuta elCampoParámetrosDeRutaNuevo, string laRazón)
    {
      // Solo cambia el campo si es diferente.
      if (elCampoParámetrosDeRutaNuevo == CampoParámetrosDeRuta)
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
    /// Actualiza el nombre secundario.
    /// </summary>
    /// <param name="elNombreSecundario">El Nombre Secundario</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void ActualizaNombreSecundario(string elNombreSecundario, string laRazón)
    {
      CampoNombre nuevoCampoNombreSecundario = new CampoNombre(elNombreSecundario, 2);
      ActualizaCampo(nuevoCampoNombreSecundario, ref miCampoNombreSecundario, laRazón);
    }

    
    /// <summary>
    /// Añade un nodo ruteable.
    /// </summary>
    /// <param name="elIndice">El índice del nodo ruteable.</param>
    /// <param name="elIdentificadorGlobal">El identificador global.</param>
    /// <param name="laRazón">La razón.</param>
    public void AñadeNodoRuteable(int elIndice, int elIdentificadorGlobal, string laRazón)
    {
      // TODO: Cambiar la lógica para insertar campos en vez de cambiarlos.

      // Si ya tiene un nodo ruteable en el índice, pero con un identificador
      // global distinto entonces actualizamos el campo.
      // Si el identificador global es igual entonces no hacemos nada.
      CampoNodoRuteable campoNodoRuteableActual = misCamposNodosRuteables[elIndice];
      if (campoNodoRuteableActual != null)
      {
        if (campoNodoRuteableActual.IdentificadorGlobal != elIdentificadorGlobal)
        {
          // Actualiza el campo.
          CampoNodoRuteable nuevoCampoNodoRuteable = new CampoNodoRuteable(
            campoNodoRuteableActual.Identificador,
            campoNodoRuteableActual.Número,
            elIndice,
            elIdentificadorGlobal,
            campoNodoRuteableActual.EsExterno);
          CambiaCampo(nuevoCampoNodoRuteable, campoNodoRuteableActual, laRazón);

          // TODO: Actualiza tabla global de nodos ruteables.
        }
      }
      else
      {
        #region Añade el nodo ruteable en la posición correcta.
        #region Ve si es necesario insertar el nodo ruteable.
        // El nuevo nodo ruteable hay que insertarlo si tiene un
        // índice de coordenadas menor que alguno de los nodos
        // ruteables que ya existen.
        List<CampoNodoRuteable> camposNodosRuteables = new List<CampoNodoRuteable>();
        foreach (CampoNodoRuteable campo in misCamposNodosRuteables)
        {
          if (campo != null)
          {
            camposNodosRuteables.Add(campo);

            // TODO: Actualiza tabla global de nodos ruteables.
          }
        }
        int númeroDeNodosRuteables = camposNodosRuteables.Count;
        int últimoIndice = númeroDeNodosRuteables - 1;
        bool yaInsertóNodoRuteable = false;
        for (int i = 0; i < númeroDeNodosRuteables; ++i)
        {
          CampoNodoRuteable campo = camposNodosRuteables[i];
          if (elIndice < campo.IndiceDeCoordenadas)
          {
            // Inserta en nuevo nodo ruteable solo si todavía
            // no se ha insertado.
            if (!yaInsertóNodoRuteable)
            {
              CampoNodoRuteable nuevoCampoNodoRuteable = new CampoNodoRuteable(
                CampoNodoRuteable.IdentificadorDeNodo,
                campo.Número,
                elIndice,
                elIdentificadorGlobal,
                false);
              CambiaCampo(nuevoCampoNodoRuteable, campo, laRazón);
              yaInsertóNodoRuteable = true;

              // TODO: Actualiza tabla global de nodos ruteables.
            }

            #region Mueve los siguientes nodos.

            // Una vez que se insertó el nodo ruteable entonces
            // hay que mover todos los nodos siguientes.
            int siguienteIndice = i + 1;
            int siguienteNúmero = campo.Número + 1;
            CampoNodoRuteable campoNodoRuteable = new CampoNodoRuteable(
              campo.Identificador,
              siguienteNúmero,
              campo.IndiceDeCoordenadas,
              campo.IdentificadorGlobal,
              campo.EsExterno);

            // Si el siguiente índice es válido entonces tenemos que cambiar
            // el nodo.
            // Si no, entonces tenemos que añadir el nodo.
            if (siguienteIndice <= últimoIndice)
            {
              CampoNodoRuteable campoACambiar = camposNodosRuteables[siguienteIndice];
              CambiaCampo(campoNodoRuteable, campoACambiar, laRazón);
            }
            else
            {
              AñadeCampo(campoNodoRuteable, laRazón);
            }

            #endregion
          }
        }

        #endregion

        // Si no se insertó el nodo ruteable quiere decir que debemos
        // añadirlo de último.
        if (!yaInsertóNodoRuteable)
        {
          int númeroDeNodoRuteable = númeroDeNodosRuteables + 1;
          CampoNodoRuteable nuevoCampoNodoRuteable = new CampoNodoRuteable(
            CampoNodoRuteable.IdentificadorDeNodo,
            númeroDeNodoRuteable,
            elIndice,
            elIdentificadorGlobal,
            false);
          AñadeCampo(nuevoCampoNodoRuteable, laRazón);
        }

        #endregion
      }
    }
    #endregion

    #region Métodos Privados
    private void CreaCamposNodosRuteables()
    {
      misCamposNodosRuteables = new CampoNodoRuteable[Coordenadas.Length];
      foreach (Campo campo in Campos)
      {
        CampoNodoRuteable campoNodo = campo as CampoNodoRuteable;
        if (campoNodo != null)
        {
          misCamposNodosRuteables[campoNodo.IndiceDeCoordenadas] = campoNodo;
        }
      }

      int númeroDeNodos = Coordenadas.Length;
      Nodos = new Nodo[númeroDeNodos];
      for (int i = 0; i < númeroDeNodos; ++i)
      {
        Nodos[i] = new Nodo(this, i);
      }
      foreach (Campo campo in Campos)
      {
        CampoNodoRuteable campoNodoRuteable = campo as CampoNodoRuteable;
        if (campoNodoRuteable != null)
        {
          Nodos[campoNodoRuteable.IndiceDeCoordenadas].HacerRuteable(campoNodoRuteable);
        }
      }
    }
    #endregion
  }
}
