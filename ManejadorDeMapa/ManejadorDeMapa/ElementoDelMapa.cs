#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
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
    private readonly CampoTipo miTipo = new CampoTipo("0");
    private readonly string miDescripción = string.Empty;
    private readonly IList<Campo> misCampos;
    private bool miFuéModificado = false;
    private bool miFuéEliminado = false;
    private string miRazónParaEliminación = string.Empty;
    private ElementoDelMapa miOriginal = null;
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

      set
      {
        // Cambia el nombre solo si el nuevo valor es distinto.
        if (value != miNombre)
        {
          miOriginal = (ElementoDelMapa)Clone();
          miNombre = value;

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

          miFuéModificado = true;
          miManejadorDeMapa.SeModificóUnElemento();
        }
      }
    }


    /// <summary>
    /// Devuelve el tipo del elemento.
    /// </summary>
    public int Tipo
    {
      get
      {
        return miTipo.Tipo;
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
      IDictionary<int, string> lasDescripcionesPorTipo,
      IList<Campo> losCampos)
      : this (elManejadorDeMapa,
              elNúmero,
              laClase,
              BuscaDescripción(lasDescripcionesPorTipo, losCampos),
              losCampos)
    {
    }


    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del elemento.</param>
    /// <param name="laClase">La clase de elemento.</param>
    /// <param name="laDescripcion">La descripción.</param>
    /// <param name="losCampos">Los campos del elemento.</param>
    public ElementoDelMapa(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string laClase,
      string laDescripcion,
      IList<Campo> losCampos)
    {
      // Guarda variables.
      miManejadorDeMapa = elManejadorDeMapa;
      miNúmero = elNúmero;
      miClase = laClase;
      miDescripción = laDescripcion;
      misCampos = losCampos;

      // Busca los campos conocidos.
      foreach (Campo campo in losCampos)
      {
        if (campo is CampoTipo)
        {
          miTipo = (CampoTipo)campo;
        }
        else if (campo is CampoNombre)
        {
          miNombre = ((CampoNombre)campo).Nombre;
        }
      }
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
    /// Devuelve el tipo del elemento en forma de texto.
    /// </summary>
    public string TipoComoTexto()
    {
      string texto = miTipo.ToString();

      return texto;
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public abstract object Clone();
    #endregion

    #region Métodos Protegidos y Privados
    private static string BuscaDescripción(
      IDictionary<int, string> lasDescripcionesPorTipo,
      IList<Campo> losCampos)
    {
      int tipo = int.MinValue;
      foreach (Campo campo in losCampos)
      {
        if (campo is CampoTipo)
        {
          tipo = ((CampoTipo)campo).Tipo;
        }
      }

      // Busca la descripción.
      string descripción = string.Empty;
      if (tipo != int.MinValue)
      {
        lasDescripcionesPorTipo.TryGetValue(tipo, out descripción);
      }

      return descripción;
    }
    #endregion
  }
}
