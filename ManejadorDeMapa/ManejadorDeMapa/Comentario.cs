#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Representa un comentario.
  /// </summary>
  public class Comentario : ElementoDelMapa
  {
    #region Campos
    private readonly string miTexto = string.Empty;
    private static readonly Dictionary<Tipo, string> misDescripciones = new Dictionary<Tipo, string>() {
      { Tipo.TipoVacio, "Comentario"} 
    };
    #endregion

    #region Propiedades
    /// <summary>
    /// Devuelve el texto del comentario.
    /// </summary>
    public string Texto
    {
      get
      {
        return miTexto;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDeMapa">El manejador del mapa.</param>
    /// <param name="elNúmero">El número del elemento.</param>
    /// <param name="elComentario">El comentario.</param>
    public Comentario(
      ManejadorDeMapa elManejadorDeMapa,
      int elNúmero,
      string elComentario)
      : base(elManejadorDeMapa,
             elNúmero,
             ";",
             misDescripciones,
             GeneraCampos(elComentario))
    {
      miTexto = ((CampoComentario)Campos[0]).Texto;
    }


    /// <summary>
    /// Devuelve una copia de este objeto.
    /// </summary>
    public override object Clone()
    {
      Comentario clone = new Comentario(
        ManejadorDeMapa,
        Número,
        Texto);

      return clone;
    }
    #endregion

    #region Métodos Privados
    private static IList<Campo> GeneraCampos(string elComentario)
    {
      // Crea la lista de campos.
      CampoComentario campoComentario = new CampoComentario(elComentario);
      List<Campo> campos = new List<Campo>(1);
      campos.Add(campoComentario);

      return campos;
    }
    #endregion
  }
}
