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

namespace GpsYv.ManejadorDeMapa.Vías
{
  /// <summary>
  /// Manejador de Vías.
  /// </summary>
  public class ManejadorDeVías : ManejadorBase<Vía>
  {
    #region Campos
    private readonly BuscadorDeErrores miBuscadorDeErrores;
    private IDictionary<Vía, string> misErrores = new Dictionary<Vía, string>();
    private readonly BuscadorDeIncongruencias miBuscadorDeIncongruencias;
    private readonly IList<IList<ElementoDeIncongruencia>> misIncongruencias = new List<IList<ElementoDeIncongruencia>>();
    #endregion

    #region Clases
    /// <summary>
    /// Representa un elemento de la lista de incongruencias.
    /// </summary>
    public struct ElementoDeIncongruencia
    {
      /// <summary>
      /// Obtiene la Vía asociada.
      /// </summary>
      public readonly Vía Vía;

      /// <summary>
      /// Obtiene el detalle.
      /// </summary>
      public readonly string Detalle;

      /// <summary>
      /// Obtiene una variable lógica que indica si el elemento es un posible error.
      /// </summary>
      public bool EsPosibleError;

      /// <summary>
      /// Constructor.
      /// </summary>
      /// <param name="laVía">La vía.</param>
      /// <param name="elDetalle">El detalle.</param>
      /// <param name="elEsPosibleError">Variable lógica que indica si el elemento es un posible error.</param>
      public ElementoDeIncongruencia(Vía laVía, string elDetalle, bool elEsPosibleError)
      {
        Vía = laVía;
        Detalle = elDetalle;
        EsPosibleError = elEsPosibleError;
      }
    }
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando cambian los errores.
    /// </summary>
    public event EventHandler<NúmeroDeElementosEventArgs> CambiaronErrores;

    /// <summary>
    /// Evento cuando cambian las incongruencias.
    /// </summary>
    public event EventHandler<NúmeroDeElementosEventArgs> CambiaronIncongruencias;
    #endregion

    #region Propiedades
    /// <summary>
    /// Descripción de la acción "Procesar Todo".
    /// </summary>
    public static readonly string DescripciónProcesarTodo =
      "Procesa todo lo relacionado con las Vías. Los pasos se hacen en el orden indicado por el número en el menú.";


    /// <summary>
    /// Devuelve los errores de Vías.
    /// </summary>
    public IDictionary<Vía, string> Errores
    {
      get
      {
        return misErrores;
      }
    }


    /// <summary>
    /// Devuelve las incongruencias de Vías.
    /// </summary>
    public IList<IList<ElementoDeIncongruencia>> Incongruencias
    {
      get
      {
        return misIncongruencias;
      }
    }
    
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El Manejador de Mapa.</param>
    /// <param name="lasVías">Las Vías.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public ManejadorDeVías(
      ManejadorDeMapa elManejadorDeMapa,
      IList<Vía> lasVías,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDeMapa, lasVías, elEscuchadorDeEstatus)
    {
      // Crea los procesadores.
      miBuscadorDeErrores = new BuscadorDeErrores(this, elEscuchadorDeEstatus);
      miBuscadorDeIncongruencias = new BuscadorDeIncongruencias(this, elEscuchadorDeEstatus);
    }


    /// <summary>
    /// Busca Errores.
    /// </summary>
    public void BuscaErrores()
    {
      miBuscadorDeErrores.Procesa();

      // Envía evento.
      if (CambiaronErrores != null)
      {
        CambiaronErrores(this, new NúmeroDeElementosEventArgs(misErrores.Count));
      }
    }


    /// <summary>
    /// Busca Incongruencias.
    /// </summary>
    public void BuscaIncongruencias()
    {
      miBuscadorDeIncongruencias.Procesa();

      EnviaCambiaronIncongruencias();
    }


    /// <summary>
    /// Hace todas las correcciones a PDIs.
    /// </summary>
    /// <returns>El número de Vías modificadas.</returns>
    public int ProcesarTodo()
    {
      // Hacer todos las operaciones en orden.
      int númeroDeVíasModificadas = 0;
      BuscaIncongruencias();
      BuscaErrores();

      // Reporta estatus.
      EscuchadorDeEstatus.Estatus = "Se hicieron " + númeroDeVíasModificadas + " modificaciones a Vías.";

      return númeroDeVíasModificadas;
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
      misErrores.Clear();
      misIncongruencias.Clear();
    }


    private void EnviaCambiaronIncongruencias()
    {
      if (CambiaronIncongruencias != null)
      {
        CambiaronIncongruencias(this, new NúmeroDeElementosEventArgs(misIncongruencias.Count));
      }
    }

    #endregion
  }
}
