using System;
using System.Collections.Generic;
using System.Text;
using Tst;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Buscador de errores en PDIs.
  /// </summary>
  public class BuscadorDeErrores : ProcesadorBase<ManejadorDePDIs, PDI>
  {
    #region Campos
    private readonly IDictionary<PDI, string> misErrores;
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elManejadorDePDIs">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeErrores(
      ManejadorDePDIs elManejadorDePDIs,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePDIs, elEscuchadorDeEstatus)
    {
      misErrores = elManejadorDePDIs.Errores;
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override void ComenzóAProcesar()
    {
      misErrores.Clear();
      base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elElemento">El PDI.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(PDI elPDI)
    {
      List<string> errores = new List<string>();

      #region Verifica que el tipo de PDI es conocido.
      int tipo = elPDI.Tipo;
      bool esConocido = CaracterísticasDePDIs.Descripciones.ContainsKey(tipo);
      if (!esConocido)
      {
        errores.Add("El tipo (" + elPDI.TipoComoTexto() + ") no es conocido");
      }
      #endregion 

      #region Verifica las coordenadas.
      // El PDI debe tener un campo de coordenadas y además tienen que
      // tener nivel zero.
      CampoCoordenadas campoCoordenadas = null;
      foreach (Campo campo in elPDI.Campos)
      {
        if (campo is CampoCoordenadas)
        {
          campoCoordenadas = (CampoCoordenadas)campo;
          break;
        }
      }
      if (campoCoordenadas == null)
      {
        errores.Add("No tiene coordenadas.");
      }
      else if (campoCoordenadas.Nivel != 0)
      {
        errores.Add("No tiene coordenadas a nivel 0, sino a nivel " + campoCoordenadas.Nivel);
      }
      #endregion

      // Chequea si hay errores.
      if (errores.Count > 0)
      {
        string todosLosErrores = string.Join("|", errores.ToArray());
        misErrores.Add(elPDI, todosLosErrores);
      }

      // Este método nunca modifica elementos.
      bool seModificóElemento = false;
      return seModificóElemento;
    }


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected override void TerminoDeProcesar()
    {
      base.TerminoDeProcesar();

      // Reporta estatus.
      Estatus = "PDIs con Errores: " + misErrores.Count;
    }
    #endregion
  }
}
