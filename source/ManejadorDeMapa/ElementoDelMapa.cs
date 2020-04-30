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


using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un Elemento del Mapa
  /// </summary>
  public abstract class ElementoDelMapa: ICloneable
  {
    #region Campos
    private readonly ManejadorDeMapa miManejadorDeMapa;
    private int miNúmero;
    private readonly string miClase = string.Empty;
    private CampoNombre miCampoNombre;
    private CampoTipo miCampoTipo;
    private CampoIndiceDeCiudad miCampoIndiceDeCiudad;
    private string miDescripción = string.Empty;
    private readonly IList<Campo> misCampos;
    private bool miFuéModificado;
    private readonly List<string> misModificaciones = new List<string>();
    private bool miFuéEliminado;
    private string miRazónParaEliminación = string.Empty;
    private ElementoDelMapa miOriginal;
    private readonly IDictionary<Tipo, string> misDescripcionesPorTipo;
    private const string SeparadorDeModificaciones = " -> ";
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el manejador del mapa.
    /// </summary>
    public ManejadorDeMapa ManejadorDeMapa
    {
      get
      {
        return miManejadorDeMapa;
      }
    }


    /// <summary>
    /// Devuelve el número del elemento.
    /// </summary>
    public int Número
    {
      get
      {
        return miNúmero;
      }
    }

    
    /// <summary>
    /// Devuelve la clase de elemento.
    /// </summary>
    public string Clase
    {
      get
      {
        return miClase;
      }
    }


    /// <summary>
    /// Devuelve el nombre del elemento.
    /// </summary>
    public string Nombre
    {
      get
      {
        if (miCampoNombre == null)
        {
          return string.Empty;
        }

        return miCampoNombre.Nombre;
      }
    }


    /// <summary>
    /// Devuelve el tipo del elemento.
    /// </summary>
    public Tipo? Tipo
    {
      get
      {
        if (miCampoTipo == null)
        {
          return null;
        }

        return miCampoTipo.Tipo;
      }
    }


    /// <summary>
    /// Devuelve la descripción del elemento.
    /// </summary>
    public string Descripción
    {
      get
      {
        return miDescripción;
      }
    }


    /// <summary>
    /// Devuelve los campos del elemento.
    /// </summary>
    public IList<Campo> Campos
    {
      get
      {
        return misCampos;
      }
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si el elemento fue modificado.
    /// </summary>
    public bool FuéModificado
    {
      get
      {
        return miFuéModificado;
      }
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si el elemento fue eliminado.
    /// </summary>
    public bool FuéEliminado
    {
      get
      {
        return miFuéEliminado;
      }
    }


    /// <summary>
    /// Obtiene las modificaciones hechas al elemento.
    /// </summary>
    public string Modificaciones
    {
      get
      {
        StringBuilder modificaciones = new StringBuilder();

        // Añade las modificaciones.
        if (misModificaciones.Count > 0)
        {
          foreach (string modificación in misModificaciones)
          {
            modificaciones.Append(modificación);
          }
        }

        return modificaciones.ToString();
      }
    }

    /// <summary>
    /// Devuelve la razón para la eliminación.
    /// </summary>
    public string RazónParaEliminación
    {
      get
      {
        return miRazónParaEliminación;
      }
    }


    /// <summary>
    /// Devuelve el elemento original en caso de que se halla borrado o modificado.
    /// </summary>
    /// <remarks>
    /// Si el elemento no ha sido modificado o borrado se devolverá un valor nulo.
    /// </remarks>
    public ElementoDelMapa Original
    {
      get
      {
        return miOriginal;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del elemento.</param>
    /// <param name="laClase">La clase de elemento.</param>
    /// <param name="lasDescripcionesPorTipo">Las descripciones por tipo.</param>
    /// <param name="losCampos">Los campos del elemento.</param>
    protected ElementoDelMapa(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      IDictionary<Tipo, string> lasDescripcionesPorTipo,
      IList<Campo> losCampos)
     {
      // Guarda variables.
      miManejadorDeMapa = elManejadorDeMapa;
      miNúmero = elNúmero;
      miClase = laClase;
      misCampos = losCampos;
      misDescripcionesPorTipo = lasDescripcionesPorTipo;

      // Busca los campos conocidos.
      foreach (Campo campo in losCampos)
      {
        CampoTipo campoTipo;
        CampoNombre campoNombre;
        CampoIndiceDeCiudad campoIndiceDeCiudad;
        if ((campoTipo = campo as CampoTipo) != null)
        {
          miCampoTipo = campoTipo;
        }
        else if ((campoNombre = campo as CampoNombre) != null)
        {
          if (campoNombre.Número == null)
          {
            miCampoNombre = campoNombre;
          }
        }
        else if ((campoIndiceDeCiudad = campo as CampoIndiceDeCiudad) != null)
        {
          miCampoIndiceDeCiudad = campoIndiceDeCiudad;
        }
      }

      bool existe = (Tipo != null) && (misDescripcionesPorTipo.TryGetValue((Tipo)Tipo, out miDescripción));
      if (!existe)
      {
        miDescripción = string.Empty;
      }
    }


    /// <summary>
    /// Actualiza el nombre del elemento.
    /// </summary>
    /// <param name="elNombreNuevo">El nombre nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public bool ActualizaNombre(string elNombreNuevo, string laRazón)
    {
      CampoNombre nuevoCampoNombre = new CampoNombre(elNombreNuevo);
      return ActualizaCampo(nuevoCampoNombre, ref miCampoNombre, laRazón);
    }

    
    /// <summary>
    /// Actualiza el tipo del elemento.
    /// </summary>
    /// <param name="elTipoNuevo">El tipo nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void ActualizaTipo(Tipo elTipoNuevo, string laRazón)
    {
      CampoTipo nuevoCampoTipo = new CampoTipo(elTipoNuevo);
      ActualizaCampo(nuevoCampoTipo, ref miCampoTipo, laRazón);

      // Actualiza la descripción.
      bool existe = misDescripcionesPorTipo.TryGetValue((Tipo)Tipo, out miDescripción);
      if (!existe)
      {
        miDescripción = string.Empty;
      }
    }


    /// <summary>
    /// Actualiza el Campo de Indice de Ciudad.
    /// </summary>
    /// <param name="elCampoIndiceDeCiudadNuevo">El Campo de Indice de Ciudad nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    /// <returns>Una variable lógica que indica si el elemento se modificó.</returns>
    public bool ActualizaCampoIndiceDeCiudad(CampoIndiceDeCiudad elCampoIndiceDeCiudadNuevo, string laRazón)
    {
      return ActualizaCampo(elCampoIndiceDeCiudadNuevo, ref miCampoIndiceDeCiudad, laRazón);
    }


    /// <summary>
    /// Remueve el campo de índice de ciudad.
    /// </summary>
    /// <param name="laRazón">La razón.</param>
    /// <returns>Una variable lógica que indica si el elemento se modificó.</returns>
    public bool RemueveCampoIndiceDeCiudad(string laRazón)
    {
      bool cambió = false;
      if (miCampoIndiceDeCiudad != null)
      {
        RemueveCampo(miCampoIndiceDeCiudad, laRazón);
        miCampoIndiceDeCiudad = null;
        cambió = true;
      }

      return cambió;
    }


    /// <summary>
    /// Marca el elemento para ser eliminado.
    /// </summary>
    /// <param name="laRazón"></param>
    public void Elimina(string laRazón)
    {
      miRazónParaEliminación = laRazón;
      miFuéEliminado = true;
      miManejadorDeMapa.SeModificóElemento(this);
    }


    /// <summary>
    /// Hace que el elemento se re-genere.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Si el elemento no hay sido modificado o eliminado entonces no pasa nada.
    /// </para>
    /// <para>
    /// Si el elemento ha sido modificado entonces se borra el estado de
    /// modificado y la copia original.
    /// </para>
    /// <para>
    /// Si el elemento ha sido eliminado se genera un excepción.
    /// </para>
    /// </remarks>
    /// <param name="elNuevoNúmeroDelElemento">El nuevo número del Elemento regenerado.</param>
    public void Regenera(int elNuevoNúmeroDelElemento)
    {
      if (miFuéEliminado)
      {
        // Si el elemento ha sido eliminado se genera un excepción.
        throw new InvalidOperationException("No de puede regenerar un elemento que está eliminado: " + ToString());
      }

      if (miFuéModificado)
      {
        // Si el elemento ha sido modificado entonces se borra el estado de
        // modificado y la copia original.
        miFuéModificado = false;
        misModificaciones.Clear();
        miOriginal = null;
        miNúmero = elNuevoNúmeroDelElemento;
      }
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public abstract object Clone();


    /// <summary>
    /// Retorna una variable lógica indicando si el elemento tiene el atributo dado.
    /// </summary>
    /// <param name="elAtributo">El atributo dado.</param>
    public bool TieneAtributo(string elAtributo)
    {
      bool tieneAtributo = false;

      foreach (Campo campo in misCampos)
      {
        CampoAtributo campoAtributo = campo as CampoAtributo;
        if (campoAtributo != null)
        {
          if (campoAtributo.Atributo == elAtributo)
          {
            tieneAtributo = true;
            break;
          }
        }
      }

      return tieneAtributo;
    }




    /// <summary>
    /// Añade el atributo dado al elemento.
    /// </summary>
    /// <param name="elAtributo">El atributo dado.</param>
    public void AñadeAtributo(string elAtributo)
    {
      if (!TieneAtributo(elAtributo))
      {
        // Crea y añade el campo nuevo.
        CampoAtributo campoAtributo = new CampoAtributo(elAtributo);
        AñadeCampo(campoAtributo, elAtributo);
      }
    }
    #endregion

    #region Métodos Protegidos y Privados
    private void SeVaAModificarElemento()
    {
      miFuéModificado = true;

      // Guarda el original si todavía no se ha hecho.
      if (miOriginal == null)
      {
        miOriginal = (ElementoDelMapa)Clone();
      }
    }


    /// <summary>
    /// Inserta un campo dado.
    /// </summary>
    /// <param name="elCampoNuevo">El campo a insertar.</param>
    /// <param name="elIndice">El índice en donde insertar el campo.</param>
    protected void InsertaCampo(Campo elCampoNuevo, int elIndice)
    {
      misCampos.Insert(elIndice, elCampoNuevo);
    }


    /// <summary>
    /// Cambia un campo dado.
    /// </summary>
    /// <param name="elCampoNuevo">El campo nuevo.</param>
    /// <param name="elCampoACambiar">El campo a cambiar.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    protected void CambiaCampo(Campo elCampoNuevo, Campo elCampoACambiar, string laRazón)
    {
      // Asegurarse que los campos son del mismo tipo.
      if (elCampoACambiar.GetType() != elCampoNuevo.GetType())
      {
        throw new ArgumentException(
          string.Format("Los campos deben ser del mismo tipo, pero son '{0}' y '{1}'", 
          elCampoACambiar.GetType(),
          elCampoNuevo.GetType()));
      }

      // Busca y actualiza el campo.
      bool encontróCampo = false;
      for (int i = 0; i < misCampos.Count; ++i)
      {
        if (ReferenceEquals(misCampos[i], elCampoACambiar))
        {
          encontróCampo = true;

          #region Remplaza el campo.
          SeVaAModificarElemento();

          // Añade la razón del cambio.
          misModificaciones.Add(string.Format(
            "[{0}: {1} {2} {3}]", elCampoNuevo.Identificador, elCampoACambiar, SeparadorDeModificaciones, laRazón));

          // Asigna el nuevo campo.
          misCampos[i] = elCampoNuevo;

          // Avísale al manejador de mapa que se cambió un elemento.
          miManejadorDeMapa.SeModificóElemento(this);
          #endregion
        }
      }

      if (!encontróCampo)
      {
        throw new ArgumentException(string.Format("El elemento no tiene el campo: {0}", elCampoACambiar.Identificador));
      }
    }


    /// <summary>
    /// Añade un campo.
    /// </summary>
    /// <param name="elCampoNuevo">El campo nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    protected void AñadeCampo(Campo elCampoNuevo, string laRazón)
    {
      // Avísale al manejador de mapa que se va a cambiar un elemento.
      SeVaAModificarElemento();

      // Añade la razón del cambio.
      misModificaciones.Add(string.Format(
        "[Nuevo {0}: {1}]", elCampoNuevo.Identificador, laRazón));

      // Añade el nuevo campo.
      misCampos.Add(elCampoNuevo);

      // Avísale al manejador de mapa que se cambió un elemento.
      miManejadorDeMapa.SeModificóElemento(this);
    }


    /// <summary>
    /// Remueve un campo dado.
    /// </summary>
    /// <param name="elCampoARemover">El campo a remover.</param>
    /// <param name="laRazón">La razón.</param>
    private void RemueveCampo(Campo elCampoARemover, string laRazón)
    {
      // Avísale al manejador de mapa que se va a cambiar un elemento.
      SeVaAModificarElemento();

      // Añade la razón del cambio.
      misModificaciones.Add(string.Format(
        "[Removido {0}: {1}]", elCampoARemover.Identificador, laRazón));

      // Remueve el campo.
      misCampos.Remove(elCampoARemover);
      miCampoIndiceDeCiudad = null;

      // Avísale al manejador de mapa que se cambió un elemento.
      miManejadorDeMapa.SeModificóElemento(this);
    }


    /// <summary>
    /// Actualiza un campo dado.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="elCampoNuevo">El campo nuevo.</param>
    /// <param name="elCampo">El campo dado.</param>
    /// <param name="laRazón">La razón.</param>
    /// <returns></returns>
    protected bool ActualizaCampo<T>(T elCampoNuevo, ref T elCampo, string laRazón)
      where T : Campo
    {
      bool cambió = false;

      // Si no tiene el Campo entonces le añadimos uno.
      // Si no, se lo cambiamos.
      if (elCampo == null)
      {
        AñadeCampo(elCampoNuevo, laRazón);
        elCampo = elCampoNuevo;
        cambió = true;
      }
      else
      {
        // Cambia el campo si es diferente.
        if (elCampo != elCampoNuevo)
        {
          CambiaCampo(elCampoNuevo, elCampo, laRazón);
          elCampo = elCampoNuevo;
          cambió = true;
        }
      }

      return cambió;
    }
    #endregion
  }
}
