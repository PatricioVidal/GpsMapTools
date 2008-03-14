using System;
using System.Collections.Generic;
using System.Text;
using Tst;
using System.Collections;

namespace GpsYv.ManejadorDeMapa.PDIs
{
  /// <summary>
  /// Buscador de PDIs duplicados.
  /// </summary>
  public class BuscadorDeDuplicados : ProcesadorBase<ManejadorDePDIs, PDI>
  {
    #region Campos
    private readonly IDictionary<PDI, IList<PDI>> misGruposDeDuplicados;
    private readonly List<PDI> misPDIsDuplicados = new List<PDI>();
    private int miDistanciaMáxima = 30;
    private int miDistanciaHamming = 3;
    private int miNúmeroDeElementosEliminados = 0;
    #endregion

    #region Eventos
    /// <summary>
    /// Evento cuando se encuentran duplicados.
    /// </summary>
    public event EventHandler EncontraronDuplicados;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone la distancia máxima de búsqueda.
    /// </summary>
    public int DistanciaMáxima
    {
      get
      {
        return miDistanciaMáxima;
      }

      set
      {
        miDistanciaMáxima = value;
      }
    }

    /// <summary>
    /// Obtiene o pone la distancia máxima Hamming de búsqueda de
    /// nombres parecidos.
    /// </summary>
    /// <remarks>
    /// Valores típicos:
    ///  1   -> No encuentra.
    ///  2-4 -> Encuentra parecidos.
    ///  6+  -> Es como mucho, todo es parecido a todo. 
    /// </remarks>
    /// <seealso cref="http://en.wikipedia.org/wiki/Hamming_distance"/>
    public int DistanciaHamming
    {
      get
      {
        return miDistanciaHamming;
      }

      set
      {
        miDistanciaHamming = value;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="ManejadorDePDIs">El manejador de PDIs.</param>
    /// <param name="elEscuchadorDeEstatus">El escuchador de estatus.</param>
    public BuscadorDeDuplicados(
      ManejadorDePDIs elManejadorDePDIs,
      IEscuchadorDeEstatus elEscuchadorDeEstatus)
      : base(elManejadorDePDIs, elEscuchadorDeEstatus)
    {
      misGruposDeDuplicados = elManejadorDePDIs.GruposDeDuplicados;
    }
    #endregion

    #region Métodos Protegidos.
    /// <summary>
    /// Este método se llama antes de comenzar a procesar los elementos.
    /// </summary>
    protected override void ComenzóAProcesar()
    {
      misGruposDeDuplicados.Clear();
      misPDIsDuplicados.Clear();
      miNúmeroDeElementosEliminados = 0;

      base.ComenzóAProcesar();
    }


    /// <summary>
    /// Procesa un PDI.
    /// </summary>
    /// <param name="elElemento">El PDI.</param>
    /// <returns>Una variable lógica que indica si se proceso el elemento.</returns>
    protected override bool ProcesaElemento(PDI elPDI)
    {
      // Retorna si el PDI ya ha sido identificado como duplicado.
      if (misPDIsDuplicados.Contains(elPDI))
      {
        return false;
      }

      bool modificóElemento = false;
      List<PDI> duplicados = new List<PDI>();

      // Busca en todos los PDIs desde la posición n + 1 que
      // no estén eliminados.
      for (int i = NúmeroDeElementoProcesándose; i < NúmeroDeElementos; ++i)
      {
        PDI posiblePDIDuplicado = this[i];
        if (!posiblePDIDuplicado.FuéEliminado)
        {
          // Si el PDI es idéntico entonces se puede borrar de una.
          if (posiblePDIDuplicado.TieneLaMismaInformación(elPDI))
          {
            posiblePDIDuplicado.Elimina("PDI idéntico al elemento " + elPDI.Número);
            misPDIsDuplicados.Add(posiblePDIDuplicado);
            modificóElemento = true;
            ++miNúmeroDeElementosEliminados;
          }
          else
          {
            // Si el PDI no es idéntico entonces puede ser duplicado si:
            //  - El tipo es el mismo, y
            //  - La distancia es cercana, y
            //      - El nombre es igual, o
            //      - Uno es las siglas del otro, o
            //      - El nombre es parecido.
            if (elPDI.Tipo == posiblePDIDuplicado.Tipo)
            {
              double distanciaEnMetros = Coordenadas.Distancia(elPDI.Coordenadas, posiblePDIDuplicado.Coordenadas);
              if (distanciaEnMetros <= miDistanciaMáxima)
              {
                bool esDuplicado = false;

                // Nombre es igual?
                if (elPDI.Nombre == posiblePDIDuplicado.Nombre)
                {
                  esDuplicado = true;
                }
                // El PDI es las siglas del posible duplicado?
                else if (PuedeSerSiglas(elPDI.Nombre))
                {
                  string siglasPosibleDuplicado = ObtieneSiglas(posiblePDIDuplicado.Nombre);

                  if (elPDI.Nombre == siglasPosibleDuplicado)
                  {
                    esDuplicado = true;
                  }
                }
                // El posible duplicado es las siglas del PDI?
                else if (PuedeSerSiglas(posiblePDIDuplicado.Nombre))
                {
                  string siglasPdi = ObtieneSiglas(elPDI.Nombre);

                  if (siglasPdi == posiblePDIDuplicado.Nombre)
                  {
                    esDuplicado = true;
                  }
                }
                // El nombre es parecido?
                else
                {
                  TstDictionary diccionario = new TstDictionary();
                  diccionario.Add(elPDI.Nombre, null);
                  ICollection resultados = diccionario.NearNeighbors(posiblePDIDuplicado.Nombre, miDistanciaHamming);

                  // Como solo estamos comparando el nombre del PDI con el nombre del 
                  // posible duplicado, entonces al tener un solo resultado significa
                  // que son parecidos.
                  if (resultados.Count > 0)
                  {
                    esDuplicado = true;
                  }
                }

                if (esDuplicado)
                {
                  duplicados.Add(posiblePDIDuplicado);
                  misPDIsDuplicados.Add(posiblePDIDuplicado);
                }
              }
            }
          }
        }
      }

      if (duplicados.Count > 0)
      {
        misGruposDeDuplicados.Add(elPDI, duplicados);
      }

      return modificóElemento;
    }


    private bool PuedeSerSiglas(string elNombre)
    {
      bool puedeSerSiglas = false;

      // Un nombre puede ser siglas si tiene una sola palabra.
      string[] palabras = elNombre.Split(' ');
      if (palabras.Length == 1)
      {
        puedeSerSiglas = true;
      }

      return puedeSerSiglas;
    }


    private string ObtieneSiglas(string elNombre)
    {
      string siglas = string.Empty;
      string[] palabras = elNombre.Split(' ');

      // Si hay una sola palabra (o ninguna) en el nombre entonces
      // devolvemos el nombre. Si no, entonces devolvemos la primera
      // letra de cada palabra.
      if (palabras.Length <= 1)
      {
        siglas = elNombre;
      }
      else
      {
        foreach (string palabra in palabras)
        {
          if (palabra.Length > 0)
          {
            siglas += palabra[0];
          }
        }
      }

      return siglas;
    }


    /// <summary>
    /// Este método se llama al terminar el procesamiento de los elementos.
    /// </summary>
    protected override void TerminoDeProcesar()
    {
      base.TerminoDeProcesar();

      // Reporta estatus.
      Estatus = "Posibles Duplicados: " + misGruposDeDuplicados.Count + "  Idénticos/Eliminados: " + miNúmeroDeElementosEliminados;

      // Genera el evento.
      if (EncontraronDuplicados != null)
      {
        EncontraronDuplicados(this, new EventArgs());
      }
    }
    #endregion
  }
}
