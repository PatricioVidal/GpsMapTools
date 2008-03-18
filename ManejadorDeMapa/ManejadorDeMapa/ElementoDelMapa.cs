﻿#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
    private readonly int miNúmero;
    private readonly string miClase = string.Empty;
    private string miNombre = string.Empty;
    private Tipo miTipo = Tipo.TipoVacio;
    private string miDescripción = string.Empty;
    private readonly IList<Campo> misCampos;
    private bool miFuéModificado = false;
    private List<string> misModificacionesDeNombre = new List<string>();
    private List<string> misModificacionesDeTipo = new List<string>();
    private bool miFuéEliminado = false;
    private string miRazónParaEliminación = string.Empty;
    private ElementoDelMapa miOriginal = null;
    private readonly IDictionary<Tipo, string> misDescripcionesPorTipo;
    private static readonly string SeparadorDeModificaciones = " -> ";
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
        return (IList<Campo>)misCampos;
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
    public ElementoDelMapa(
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
        if (campo is CampoTipo)
        {
          miTipo = ((CampoTipo)campo).Tipo;
        }
        else if (campo is CampoNombre)
        {
          miNombre = ((CampoNombre)campo).Nombre;
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
      IList<Campo> campos = misCampos;
      for (int i = 0; i < misCampos.Count; ++i)
      {
        if (campos[i] is CampoNombre)
        {
          // Remplaza el campo.
          campos[i] = new CampoNombre(miNombre);

          // Solo se cambia el primer campo nombre.
          break;
        }
      }

      // Avisa que se modificó un elemento.
      miManejadorDeMapa.SeModificóUnElemento();
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
      IList<Campo> campos = misCampos;
      for (int i = 0; i < misCampos.Count; ++i)
      {
        if (campos[i] is CampoTipo)
        {
          // Remplaza el campo.
          campos[i] = new CampoTipo(miTipo);

          // Solo se cambia el primer campo tipo.
          break;
        }
      }

      // Actualiza la descripción.
      bool existe = misDescripcionesPorTipo.TryGetValue(miTipo, out miDescripción);
      if (!existe)
      {
        miDescripción = string.Empty;
      }

      // Avisa que se modificó un elemento.
      miManejadorDeMapa.SeModificóUnElemento();
    }


    /// <summary>
    /// Marca el elemento para ser eliminado.
    /// </summary>
    /// <param name="laRazón"></param>
    public void Elimina(string laRazón)
    {
      miRazónParaEliminación = laRazón;
      miFuéEliminado = true;
      miManejadorDeMapa.SeModificóUnElemento();
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public abstract object Clone();
    #endregion

    #region Métodos Protegidos y Privados
    private static string BuscaDescripción(
      IDictionary<Tipo, string> lasDescripcionesPorTipo,
      IList<Campo> losCampos)
    {
      Tipo? tipo = null;
      foreach (Campo campo in losCampos)
      {
        if (campo is CampoTipo)
        {
          tipo = ((CampoTipo)campo).Tipo;
        }
      }

      // Busca la descripción.
      string descripción = string.Empty;
      if (tipo != null)
      {
        bool existe = lasDescripcionesPorTipo.TryGetValue(tipo.Value, out descripción);
        if (!existe)
        {
          descripción = string.Empty;
        }
      }

      return descripción;
    }


    private void SeVaAModificarElemento()
    {
      miFuéModificado = true;

      // Guarda el original si todavíano se ha hecho.
      if (miOriginal == null)
      {
        miOriginal = (ElementoDelMapa)Clone();
      }
    }
    #endregion
  }
}