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
    private string miNombre = string.Empty;
    private Tipo miTipo = Tipo.TipoNulo;
    private CampoIndiceDeCiudad miCampoIndiceDeCiudad;
    private string miDescripción = string.Empty;
    private readonly IList<Campo> misCampos;
    private bool miFuéModificado;
    private readonly List<string> misModificacionesDeNombre = new List<string>();
    private readonly List<string> misModificacionesDeTipo = new List<string>();
    private readonly List<string> misOtrasModificaciones = new List<string>();
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
        return miNombre;
      }
    }


    /// <summary>
    /// Devuelve el tipo del elemento.
    /// </summary>
    public Tipo Tipo
    {
      get
      {
        return miTipo;
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
    /// Devuelve una variable lógica que indica si el elemento fué modificado.
    /// </summary>
    public bool FuéModificado
    {
      get
      {
        return miFuéModificado;
      }
    }


    /// <summary>
    /// Devuelve una variable lógica que indica si el elemento fué eliminado.
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

        // Añade las modificaciones de nombre.
        if (misModificacionesDeNombre.Count > 0)
        {
          modificaciones.Append("[Nombre: " + miOriginal.Nombre);
          foreach (string modificación in misModificacionesDeNombre)
          {
            modificaciones.Append(modificación);
          }
          modificaciones.Append("]");
        }

        // Añade las modificaciones de Tipo.
        if (misModificacionesDeTipo.Count > 0)
        {
          modificaciones.Append("[Tipo: " + miOriginal.Tipo);
          foreach (string modificación in misModificacionesDeTipo)
          {
            modificaciones.Append(modificación);
          }
          modificaciones.Append("]");
        }

        // Añade las otras modificaciones.
        if (misOtrasModificaciones.Count > 0)
        {
          foreach (string modificación in misOtrasModificaciones)
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
          miTipo = campoTipo.Tipo;
        }
        else if ((campoNombre = campo as CampoNombre) != null)
        {
          miNombre = campoNombre.Nombre;
        }
        else if ((campoIndiceDeCiudad = campo as CampoIndiceDeCiudad) != null)
        {
          miCampoIndiceDeCiudad = campoIndiceDeCiudad;
        }
      }

      bool existe = misDescripcionesPorTipo.TryGetValue(miTipo, out miDescripción);
      if (!existe)
      {
        miDescripción = string.Empty;
      }
    }


    /// <summary>
    /// Cambia el nombre del elemento.
    /// </summary>
    /// <param name="elNombreNuevo">El nombre nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaNombre(string elNombreNuevo, string laRazón)
    {
      // Avisa que de va a modificar un elemento.
      SeVaAModificarElemento();

      // Actualiza el nombre.
      miNombre = elNombreNuevo;
      misModificacionesDeNombre.Add(
        SeparadorDeModificaciones + laRazón + 
        SeparadorDeModificaciones + elNombreNuevo);

      // Busca y actualiza el campo del nombre.
      bool encontróCampoNombre = false;
      for (int i = 0; i < misCampos.Count; ++i)
      {
        if (misCampos[i] is CampoNombre)
        {
          encontróCampoNombre = true;

          // Remplaza el campo.
          misCampos[i] = new CampoNombre(miNombre);
        }
      }
      
      // Añade el campo tipo si no se encontró.
      if (!encontróCampoNombre)
      {
        misCampos.Add(new CampoNombre(miNombre));
      }

      // Avisa que se modificó un elemento.
      miManejadorDeMapa.SeModificóElemento(this);
    }

    
    /// <summary>
    /// Cambia el tipo del elemento.
    /// </summary>
    /// <param name="elTipoNuevo">El tipo nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    public void CambiaTipo(Tipo elTipoNuevo, string laRazón)
    {
      // Avisa que se va a modificar un elemento.
      SeVaAModificarElemento();

      // Actualiza el tipo.
      miTipo = elTipoNuevo;
      misModificacionesDeTipo.Add(
        SeparadorDeModificaciones + laRazón +
        SeparadorDeModificaciones + elTipoNuevo);

      // Busca y actualiza el campo del tipo.
      bool encontróCampoTipo = false;
      for (int i = 0; i < misCampos.Count; ++i)
      {
        if (misCampos[i] is CampoTipo)
        {
          encontróCampoTipo = true;

          // Remplaza el campo.
          misCampos[i] = new CampoTipo(miTipo);
        }
      }

      // Añade el campo tipo si no se encontró.
      if (!encontróCampoTipo)
      {
        misCampos.Add(new CampoTipo(miTipo));
      }

      // Actualiza la descripción.
      bool existe = misDescripcionesPorTipo.TryGetValue(miTipo, out miDescripción);
      if (!existe)
      {
        miDescripción = string.Empty;
      }

      // Avisa que se modificó un elemento.
      miManejadorDeMapa.SeModificóElemento(this);
    }


    /// <summary>
    /// Cambia el Campo de Indice de Ciudad.
    /// </summary>
    /// <param name="elCampoIndiceDeCiudadNuevo">El Campo de Indice de Ciudad nuevo.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    /// <returns>Una variable lógica que indica que el PDI se modificó.</returns>
    public bool ActualizaCampoIndiceDeCiudad(CampoIndiceDeCiudad elCampoIndiceDeCiudadNuevo, string laRazón)
    {
      bool cambió = false;

      // Si no tiene el Campo de Indice de Ciudad en tonces le 
      // añadimos uno.
      // Si no, se lo cambiamos.
      if (miCampoIndiceDeCiudad == null)
      {
        AñadeCampo(elCampoIndiceDeCiudadNuevo, laRazón);
        miCampoIndiceDeCiudad = elCampoIndiceDeCiudadNuevo;
        cambió = true;
      }
      else
      {
        // Cambia el campo si es diferente.
        if (elCampoIndiceDeCiudadNuevo != miCampoIndiceDeCiudad)
        {
          CambiaCampo(elCampoIndiceDeCiudadNuevo, miCampoIndiceDeCiudad, laRazón);
          miCampoIndiceDeCiudad = elCampoIndiceDeCiudadNuevo;
          cambió = true;
        }
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
    /// <param name="elNuevoNùmeroDelElemento">El nuevo nùmero del Elemento regenerado.</param>
    public void Regenera(int elNuevoNùmeroDelElemento)
    {
      if (miFuéEliminado)
      {
        // Si el elemento ha sido eliminado se genera un excepción.
        throw new InvalidOperationException("No de puede regenerar un elemento está eliminado: " + ToString());
      }

      if (miFuéModificado)
      {
        // Si el elemento ha sido modificado entonces se borra el estado de
        // modificado y la copia original.
        miFuéModificado = false;
        misModificacionesDeNombre.Clear();
        misModificacionesDeTipo.Clear();
        miOriginal = null;
        miNúmero = elNuevoNùmeroDelElemento;
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
    /// Cambia un campo dado.
    /// </summary>
    /// <param name="elCampoNuevo">El campo nuevo.</param>
    /// <param name="elCampoACambiar">El campo a cambiar.</param>
    /// <param name="laRazón">La razón del cambio.</param>
    protected void CambiaCampo(Campo elCampoNuevo, Campo elCampoACambiar, string laRazón)
    {
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
          misOtrasModificaciones.Add(string.Format(
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
      misOtrasModificaciones.Add(string.Format(
        "[Nuevo {0}: {1}]", elCampoNuevo.Identificador, laRazón));

      // Añade el nuevo campo.
      misCampos.Add(elCampoNuevo);

      // Avísale al manejador de mapa que se cambió un elemento.
      miManejadorDeMapa.SeModificóElemento(this);
    }
    #endregion
  }
}
