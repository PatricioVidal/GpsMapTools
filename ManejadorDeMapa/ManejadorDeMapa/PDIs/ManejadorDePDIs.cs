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

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Manejados de PDIs.
  /// </summary>
  public class ManejadorDePDIs : ManejadorBase<PDI>
  {
    #region Campos
    private readonly EliminadorDeCaracteres miEliminadorDeCaracteres;
    private readonly ArregladorDeLetras miArregladorDeLetras;
    private readonly ArregladorDePalabrasPorTipo miArregladorDePrefijos;
    private readonly BuscadorDeDuplicados miBuscadorDePDIsDuplicados;
    private readonly BuscadorDeErrores miBuscadorDeErrores;
    private IDictionary<PDI, IList<PDI>> misGruposDeDuplicados = new Dictionary<PDI, IList<PDI>>();
    private IDictionary<PDI, string> misErrores = new Dictionary<PDI, string>();
    #endregion
    
    #region Eventos
    /// <summary>
    /// Evento cuando se encuentran errores.
    /// </summary>
    public event EventHandler EncontraronErrores;
    #endregion

    #region Propiedades
    /// <summary>
    /// Descripción de la acción "Procesar Todo".
    /// </summary>
    public static readonly string DescripciónProcesarTodo =
      "Procesa todo lo relacionado con de PDIs. Los pasos se hacen en el orden indicado por el número en el menú.";


    /// <summary>
    /// Obtiene el Buscador de Duplicados.
    /// </summary>
    public BuscadorDeDuplicados BuscadorDeDuplicados
    {
      get
      {
        return miBuscadorDePDIsDuplicados;
      }
    }


    /// <summary>
    /// Devuelve los grupos de PDIs duplicados.
    /// </summary>
    public IDictionary<PDI, IList<PDI>> GruposDeDuplicados
    {
      get
      {
        return misGruposDeDuplicados;
      }
    }


    /// <summary>
    /// Devuelve los PDIs con errores.
    /// </summary>
    public IDictionary<PDI, string> Errores
    {
      get
      {
        return misErrores;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El Manejador de Mapa.</param>
    /// <param name="losPuntosDeInteres">Los PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorDePDIs(
      ManejadorDeMapa elManejadorDeMapa,
      IList<PDI> losPuntosDeInteres,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base (elManejadorDeMapa, losPuntosDeInteres, elEscuchadorDeEstatus)
    {
      // Crea los procesadores.
      miEliminadorDeCaracteres = new EliminadorDeCaracteres(this, elEscuchadorDeEstatus);
      miArregladorDeLetras = new ArregladorDeLetras(this, elEscuchadorDeEstatus);
      miArregladorDePrefijos = new ArregladorDePalabrasPorTipo(this, elEscuchadorDeEstatus);
      miBuscadorDePDIsDuplicados = new BuscadorDeDuplicados(this, elEscuchadorDeEstatus);
      miBuscadorDeErrores = new BuscadorDeErrores(this, elEscuchadorDeEstatus);
    }


    /// <summary>
    /// Elimina Caracteres Inválidos en los nombres de PDIs.
    /// </summary>
    /// <returns>El número de PDIs modificados.</returns>
    public int EliminaCaracteresInválidos()
    {
      int númeroDePDIsModificados =  miEliminadorDeCaracteres.Procesa();
      return númeroDePDIsModificados;
    }


    /// <summary>
    /// Arreglar las letras en los PDIs.
    /// </summary>
    /// <returns>El número de PDIs modificados.</returns>
    public int ArreglarLetras()
    {
      int númeroDePDIsModificados = miArregladorDeLetras.Procesa();
      return númeroDePDIsModificados;
    }


    /// <summary>
    /// Busca Errores.
    /// </summary>
    public void BuscaErrores()
    {
      miBuscadorDeErrores.Procesa();

      // Envia evento.
      if (EncontraronErrores != null)
      {
        EncontraronErrores(this, new EventArgs());
      }
    }


    /// <summary>
    /// Arreglar las palabras en los PDIs.
    /// </summary>
    /// <returns>El número de PDIs modificados.</returns>
    public int ArreglarPalabras()
    {
      int númeroDePDIsModificados = miArregladorDePrefijos.Procesa();
      return númeroDePDIsModificados;
    }


    /// <summary>
    /// Procesa todas las correcciones a PDIs.
    /// </summary>
    /// <returns>El número de PDIs modificados.</returns>
    public int ProcesarTodo()
    {
      // Hacer todos las operaciones en orden.
      int númeroDePDIsModificados = EliminaCaracteresInválidos();
      númeroDePDIsModificados += ArreglarLetras();
      númeroDePDIsModificados += ArreglarPalabras();
      númeroDePDIsModificados += BuscadorDeDuplicados.Procesa();
      BuscaErrores();

      // Reporta estatus.
      EscuchadorDeEstatus.Estatus = "Se hicieron " + númeroDePDIsModificados + " modificaciones a PDI(s).";

      return númeroDePDIsModificados;
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // Borra las listas.
      misGruposDeDuplicados.Clear();
      misErrores.Clear();
    }
    #endregion
  }
}
