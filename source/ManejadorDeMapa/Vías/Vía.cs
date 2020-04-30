#region Copyright (c) Patricio Vidal (http://www.gpsyv.net)
// (For English scroll down.)
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
// Visita https://github.com/PatricioVidal/GpsMapTools para más información.
//
// La lógica de este programa se ha desarrollado con las ideas de los miembros
// del grupo GPS_YV. 
//
// Autor: Patricio Vidal.
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
// Visit https://github.com/PatricioVidal/GpsMapTools for more information.
//
// The logic of this program has been develop with ideas of the members
// of the GPS_YV group.
//
// Author: Patricio Vidal.
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
    #region Constantes
    /// <summary>
    /// Identificador campo Indicador de Dirección.
    /// </summary>
    public const string IdentificadorIndicadorDeDirección = "DirIndicator";
    #endregion

    #region Campos
    private static readonly CampoParámetrosDeRuta miCampoParámetrosDeRutaPorDefecto = new CampoParámetrosDeRuta(
      new LímiteDeVelocidad (0),
      new ClaseDeRuta (0),
      new bool[10]);

    private bool miTieneCampoParámetrosDeRutaEnCampos;
    private CampoNombre miCampoNombreSecundario;
    #endregion 

    #region Propiedades
    /// <summary>
    /// Obtiene los Parámetros de Ruta.
    /// </summary>
    public CampoParámetrosDeRuta CampoParámetrosDeRuta { get; private set; }


    /// <summary>
    /// Obtiene el Campo Indicador de Dirección.
    /// </summary>
    public CampoGenérico CampoIndicadorDeDirección { get; private set; }


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
      CampoIndicadorDeDirección = null;

      // Busca los campos específicos de las vías.
      foreach (Campo campo in losCampos)
      {
        CampoParámetrosDeRuta campoParámetrosDeRuta = campo as CampoParámetrosDeRuta;
        CampoNombre campoNombre;
        CampoGenérico campoGenérico;
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
        else if ((campoGenérico = campo as CampoGenérico) != null)
        {
          if (campoGenérico.Identificador == IdentificadorIndicadorDeDirección)
          {
            CampoIndicadorDeDirección = campoGenérico;
          }
        }
      }

      CreaNodos();
    }


    /// <summary>
    /// Cambia el Campo de Parámetros de Ruta.
    /// </summary>
    /// <param name="elCampoParámetrosDeRutaNuevo">El Campo de Parámetros de Ruta nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    /// <returns>Una variable lógica que indica si se cambió el campo.</returns>
    public bool CambiaCampoParámetrosDeRuta(CampoParámetrosDeRuta elCampoParámetrosDeRutaNuevo, string laRazón)
    {
      // Solo cambia el campo si es diferente.
      if (elCampoParámetrosDeRutaNuevo == CampoParámetrosDeRuta)
      {
        return false;
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

      return true;
    }


    /// <summary>
    /// Cambia el Campo Indicador de Dirección.
    /// </summary>
    /// <param name="elCampoNuevo">El Campo Indicador de Dirección nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    /// <returns>Una variable lógica que indica si se cambió el campo.</returns>
    public bool CambiaCampoIndicadorDeDirección(CampoGenérico elCampoNuevo, string laRazón)
    {
      // Solo cambia el campo si es diferente.
      if (elCampoNuevo == CampoIndicadorDeDirección)
      {
        return false;
      }

      // Si no tiene Campo Indicador de Dirección entonces añadimos 
      // el campo nuevo.
      if (CampoIndicadorDeDirección == null)
      {
        // Añade el campo.
        AñadeCampo(elCampoNuevo, laRazón);
        CampoIndicadorDeDirección = elCampoNuevo;
      }
      else
      {
        // Cambia el campo.
        CambiaCampo(elCampoNuevo, CampoIndicadorDeDirección, laRazón);
        CampoIndicadorDeDirección = elCampoNuevo;
      }

      return true;
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
      // Hacer el nodo ruteable sin importar si ya era ruteable en caso
      // de que el identificador global sea diferente.
      Nodo nodoAHacerRuteable = Nodos[elIndice];

      // Si el nodo es ruteable pero con un identificador
      // global distinto entonces actualizamos el campo.
      // Si el identificador global es igual entonces no tenemos que hacer nada.
      if (nodoAHacerRuteable.EsRuteable)
      {
        if (nodoAHacerRuteable.CampoNodoRuteable.IdentificadorGlobal != elIdentificadorGlobal)
        {
          CampoNodoRuteable nuevoCampoNodoRuteable = new CampoNodoRuteable(
            CampoNodoRuteable.IdentificadorDeNodo,
            elIndice,
            elIdentificadorGlobal,
            false);

          // Actualiza el campo.
          CambiaCampo(nuevoCampoNodoRuteable, nodoAHacerRuteable.CampoNodoRuteable, laRazón);

          // Actualiza el nodo.
          nodoAHacerRuteable.HacerRuteable(nuevoCampoNodoRuteable);

          // TODO: Actualiza tabla global de nodos ruteables.
        }
      }
      // El nodo no era ruteable. Tenemos que insertar el campo de nodo
      // ruteable y hacer el nodo ruteable.
      else
      {
        CampoNodoRuteable nuevoCampoNodoRuteable = new CampoNodoRuteable(
          CampoNodoRuteable.IdentificadorDeNodo,
          elIndice,
          elIdentificadorGlobal,
          false);

        // El nuevo campo nodo ruteable hay que insertarlo si tiene
        // un índice de coordenadas menor que alguno de los nodos
        // ruteables que ya existen.
        CampoNodoRuteable campoEnDondeInsertar = null;
        CampoNodoRuteable últimoCampoNodoRuteable = null;
        foreach (var nodo in Nodos)
        {
          if (nodo.EsRuteable)
          {
            últimoCampoNodoRuteable = nodo.CampoNodoRuteable;

            if (elIndice < nodo.Indice)
            {
              campoEnDondeInsertar = nodo.CampoNodoRuteable;
            }
          }
        }

        // Si existe el campo en donde insertar entonces procedemos.
        if (campoEnDondeInsertar != null)
        {
          int indice = Campos.IndexOf(campoEnDondeInsertar);
          InsertaCampo(nuevoCampoNodoRuteable, indice);
        }
        // Si no existe el campo en donde insertar entonces lo
        // insertamos depues del último campo de nodo ruteable.
        // Si no hay nnigún campo de nodo ruteable entonces
        // insertamos el campo al final de todos los campos.
        else
        {
          if (últimoCampoNodoRuteable != null)
          {
            int indice = Campos.IndexOf(últimoCampoNodoRuteable);
            InsertaCampo(nuevoCampoNodoRuteable, indice);
          }
          else
          {
            int indice = Campos.Count;
            InsertaCampo(nuevoCampoNodoRuteable, indice);
          }
        }

        // Hacer el nodo ruteable.
        nodoAHacerRuteable.HacerRuteable(nuevoCampoNodoRuteable);

        // TODO: Actualiza tabla global de nodos ruteables.
      }
    }


    /// <summary>
    /// Cambia las coordenadas.
    /// </summary>
    /// <param name="lasCoordenadaNuevas">Las coordenadas nuevas.</param>
    /// <param name="elIndice">El índice de la coordenada a cambiar.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public override void CambiaCoordenadas(Coordenadas lasCoordenadaNuevas, int elIndice, string laRazón)
    {
      // TODO: Actualizar las coordenadas de los nodos ruteables.

      base.CambiaCoordenadas(lasCoordenadaNuevas, elIndice, laRazón);
    }
    #endregion

    #region Métodos Privados
    private void CreaNodos()
    {
      // Crea los Nodos.
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
