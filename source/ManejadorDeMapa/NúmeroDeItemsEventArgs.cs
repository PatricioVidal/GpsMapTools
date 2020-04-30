using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpsYv.ManejadorDeMapa
{
  /// <summary>
  /// Argumento de evento de número de ítems.
  /// </summary>
  public class NúmeroDeItemsEventArgs : EventArgs
  {
    /// <summary>
    /// Obtiene el número de ítems.
    /// </summary>
    public readonly int NúmeroDeItems;
    
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="elNúmeroDeItems">El número de ítems.</param>
    public NúmeroDeItemsEventArgs(int elNúmeroDeItems)
    {
      NúmeroDeItems = elNúmeroDeItems;
    }
  }
}
