﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Interfase de PDIs modificados.
  /// </summary>
  public partial class InterfaseDeModificados : InterfaseBase
  {
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDeModificados()
    {
      InitializeComponent();
    }


    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnElementosModificados(elEnviador, losArgumentos);
    }


    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Vacia las lista.
      miLista.Items.Clear();

      // Añade los PDIs.
      IList<PDI> pdis = ManejadorDeMapa.ManejadorDePDIs.Elementos;
      foreach (PDI pdi in pdis)
      {
        // Si el PDI fué cambiado y no eliminado entonces añadelo a la lista de cambios.
        if (pdi.FuéModificado && !pdi.FuéEliminado)
        {
          ListViewItem itemParaLaListaDePDIsModificados = new ListViewItem(
            new string[] { 
                pdi.Número.ToString(),
                pdi.Tipo.ToString(), 
                pdi.Descripción,
                pdi.Nombre, 
                pdi.Modificaciones});
          miLista.Items.Add(itemParaLaListaDePDIsModificados);
        }
      }

      // Actualiza la Pestaña.
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        int númeroDeModificados = miLista.Items.Count;
        pestaña.Text = "Modificados (" + númeroDeModificados + ")";
      }
    }
  }
}
