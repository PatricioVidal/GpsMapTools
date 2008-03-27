using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Argumento de evento de número de elementos.
  /// </summary>
  public class NúmeroDeElementosEventArgs : EventArgs
  {
    /// <summary>
    /// Obtiene el número de elementos.
    /// </summary>
    public readonly int NúmeroDeElementos;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elNúmeroDeElementos">El número de elementos.</param>
    public NúmeroDeElementosEventArgs(int elNúmeroDeElementos)
    {
      NúmeroDeElementos = elNúmeroDeElementos;
    }
  }
}
