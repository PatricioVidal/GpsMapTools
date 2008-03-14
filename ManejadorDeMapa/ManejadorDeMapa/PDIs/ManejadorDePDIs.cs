#region Copyright (c) 2008 GPS_YV (http://www.gpsyv.net)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa.PDIs
{
  public class ManejadorDePDIs : ManejadorBase<PDI>
  {
    #region Campos
    private readonly ArregladorDeLetrasEnPDIs miArregladorDeLetras;
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
      miArregladorDeLetras = new ArregladorDeLetrasEnPDIs(this, elEscuchadorDeEstatus);
      miArregladorDePrefijos = new ArregladorDePalabrasPorTipo(this, elEscuchadorDeEstatus);
      miBuscadorDePDIsDuplicados = new BuscadorDeDuplicados(this, elEscuchadorDeEstatus);
      miBuscadorDeErrores = new BuscadorDeErrores(this, elEscuchadorDeEstatus);
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
    /// Hace todas las correcciones a PDIs.
    /// </summary>
    public void ProcesarTodo()
    {
      // Hacer todos las operaciones en orden.
      int númeroDePDIsModificados = ArreglarLetras();
      númeroDePDIsModificados += ArreglarPalabras();
      númeroDePDIsModificados += BuscadorDeDuplicados.Procesa();
      BuscaErrores();

      // Reporta estatus.
      EscuchadorDeEstatus.Estatus = "Se hicieron " + númeroDePDIsModificados + " modificaciones a PDI(s).";
    }
    #endregion

    #region Métodos Privados
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      // Borra las listas.
      misGruposDeDuplicados.Clear();
      misErrores.Clear();
    }
    #endregion
  }
}
