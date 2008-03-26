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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.PDIs;

namespace GpsYv.ManejadorDeMapa.Interfase.PDIs
{
  /// <summary>
  /// Interfase para los PDIs duplicados.
  /// </summary>
  public partial class InterfaseDePDIsDuplicados : InterfaseBase
  {
    #region Campos
    private ManejadorDePDIs miManejadorDePDIs;
    private Brush miPincelDePDI = new SolidBrush(Color.Black);
    private Brush miPincelDePDIDuplicado = new SolidBrush(Color.Yellow);
    private Color miColorDeFondoOriginal;
    #endregion

    #region Propiedades
    /// <summary>
    /// Obtiene o pone el manejador de mapa.
    /// </summary>
    public override ManejadorDeMapa ManejadorDeMapa
    {
      set
      {
        // Deja de manejar los eventos.
        if (miManejadorDePDIs != null)
        {
          miManejadorDePDIs.BuscadorDeDuplicados.EncontraronDuplicados -= EnEncontraronDuplicados;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miManejadorDePDIs = value.ManejadorDePDIs;
          miManejadorDePDIs.BuscadorDeDuplicados.EncontraronDuplicados += EnEncontraronDuplicados;
          InicializaDistanciaMáxima();
          InicializaDistanciaHamming();
        }

        // Pone el manejador de mapa en los componentes.
        miMapa.ManejadorDeMapa = value;
      }
    }


    /// <summary>
    /// Obtiene o pone el escuchador de estatus.
    /// </summary>
    public override IEscuchadorDeEstatus EscuchadorDeEstatus
    {
      set
      {
        base.EscuchadorDeEstatus = value;
        miMapa.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Métodos Públicos
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfaseDePDIsDuplicados()
    {
      InitializeComponent();

      // Inicialización.
      miColorDeFondoOriginal = miLista.BackColor;
    }
    #endregion

    #region Métodos Privados
    /// <summary>
    /// Maneja el evento cuando hay un mapa nuevo.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnMapaNuevo(object elEnviador, EventArgs losArgumentos)
    {
      EnEncontraronDuplicados(elEnviador, losArgumentos);
    }


    /// <summary>
    /// Maneja el evento cuando hay elementos modificados en el mapa.
    /// </summary>
    /// <param name="elEnviador">El objecto que envía el evento.</param>
    /// <param name="losArgumentos">Los argumentos del evento.</param>
    protected override void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // No es necesario hacer nada aqui.
    }


    private void EnEncontraronDuplicados(object elEnviador, EventArgs losArgumentos)
    {
      // Añade los PDIs duplicados.
      miLista.SuspendLayout();
      miLista.Items.Clear();
      miLista.Groups.Clear();
      foreach (KeyValuePair<PDI, IList<PDI>> item in miManejadorDePDIs.GruposDeDuplicados)
      {
        PDI pdiBase = item.Key;
        IList<PDI> duplicados = item.Value;
        List<PDI> pdis = new List<PDI> { pdiBase };
        pdis.AddRange(duplicados);

        // Crea un grupo para cada conjunto de duplicados.
        ListViewGroup grupo = new ListViewGroup(pdiBase.Nombre);
        grupo.Tag = pdis;
        miLista.Groups.Add(grupo);

        // Añade todos los PDIs.
        miLista.Items.Add(CreaItemDeLista(pdiBase, grupo, 0));
        foreach (PDI duplicado in duplicados)
        {
          double distancia = Coordenadas.Distancia(pdiBase.Coordenadas, duplicado.Coordenadas);
          miLista.Items.Add(CreaItemDeLista(duplicado, grupo, distancia));
        }
      }
      miLista.ResumeLayout(false);

      // Actualiza la Pestaña.
      if ((Tag != null) && (Tag is TabPage))
      {
        TabPage pestaña = (TabPage)Tag;
        int númeroDePosiblesDuplicados = miLista.Groups.Count;
        pestaña.Text = "Posibles Duplicados (" + númeroDePosiblesDuplicados + ")";
      }

      // Actualiza el Número de PDIs a Eliminar.
      ActualizaNúmeroDePDIsAEliminar();
    }

    
    private ListViewItem CreaItemDeLista(PDI elPdi, ListViewGroup elGrupo, double laDistancia)
    {
      ListViewItem item = new ListViewItem(
              new string[] { 
                elPdi.Número.ToString(),
                elPdi.Tipo.ToString(), 
                elPdi.Descripción,
                elPdi.Nombre,
                elPdi.Coordenadas.ToString(),
                laDistancia.ToString("0.0")
              },
              elGrupo);

      item.Tag = elPdi;
      item.Checked = false;

      return item;
    }


    private void EnItemSeleccionado(object elEnviador, ItemCheckedEventArgs elArgumentoDelEvento)
    {
      ActualizaNúmeroDePDIsAEliminar();
    }


    private void ActualizaNúmeroDePDIsAEliminar()
    {
      int númeroDePDIsAEliminar = 0;
      foreach (ListViewItem item in miLista.Items)
      {
        if (item.Checked)
        {
          ++númeroDePDIsAEliminar;

          item.BackColor = Color.LightPink;
        }
        else
        {
          item.BackColor = miColorDeFondoOriginal;
        }

        if (númeroDePDIsAEliminar > 0)
        {
          miTextoNumeroDePDIsSelecionados.Text = númeroDePDIsAEliminar + " PDIs para Eliminar";
          miBotonEliminarPDIs.Enabled = true;
        }
        else
        {
          miTextoNumeroDePDIsSelecionados.Text = string.Empty;
          miBotonEliminarPDIs.Enabled = false;
        }
      }
    }


    private void EnBotónEliminarPDIs(object sender, EventArgs e)
    {
      // Suspende notificaciones.
      ManejadorDeMapa.SuspendeEventos();

      // Obtiene la lista de PDIs a eliminar.
      List<PDI> pdisAEliminar = new List<PDI>();
      foreach (ListViewItem item in miLista.Items)
      {
        if (item.Checked)
        {
          pdisAEliminar.Add((PDI)item.Tag);
        }
      }

      // Pregunta si se quiere eliminar los PDIs.
      StringBuilder texto = new StringBuilder();
      texto.AppendLine("Esta seguro que quiere borrar estos PDIs?");
      foreach (PDI pdi in pdisAEliminar)
      {
        texto.AppendLine("  #" + pdi.Número + ", " + pdi.Nombre);
      }
      DialogResult respuesta = MessageBox.Show(
        texto.ToString(), 
        "Eliminar PDIs", 
        MessageBoxButtons.YesNo, 
        MessageBoxIcon.Warning);

      // Elimina los PDIs si el usuario dice que si.
      if (respuesta == DialogResult.Yes)
      {
        foreach (PDI pdi in pdisAEliminar)
        {
          pdi.Elimina("Manualmente eliminado en la pestaña de 'Posibles Duplicados'");
        }

        // Busca otra vez los PDIs duplicados tomando en cuenta
        // los que se acaban de eliminar.
        ManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
      }

      // Restablece notificaciones.
      ManejadorDeMapa.RestableceEventos();
    }


    private void EnClick(object laLista, MouseEventArgs losArgumentosDelRatón)
    {
      // Obtiene el grupo seleccionado.
      ListView lista = (ListView)laLista;
      ListViewHitTestInfo información = lista.HitTest(losArgumentosDelRatón.Location);
      ListViewGroup grupo = información.Item.Group;
      List<PDI> pdis = (List<PDI>)grupo.Tag;

      // Busca el rango visible para los PDIs.
      IList<ElementoDelMapa> elementos = new List<ElementoDelMapa>(pdis.ToArray());
      RectangleF rectánguloQueEncierra = InterfaseMapa.RectanguloQueEncierra(elementos);
      float margen = 0.0001f;
      RectangleF rectánguloVisible = new RectangleF(
        (float)rectánguloQueEncierra.X - margen,
        (float)rectánguloQueEncierra.Y - margen,
        rectánguloQueEncierra.Width + (2 * margen),
        rectánguloQueEncierra.Height + (2 * margen));

      // Dibuja los PDIs como PDIs adicionales para resaltarlos.
      miMapa.PuntosAddicionales.Clear();
      PDI pdiSeleccionado = (PDI)información.Item.Tag;
      miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
        pdiSeleccionado.Coordenadas, miPincelDePDI, 13));
      foreach (PDI pdi in pdis)
      {
        miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          pdi.Coordenadas, miPincelDePDIDuplicado, 7));
      }

      // Muestra el mapa en la region deseada.
      miMapa.Enabled = true;
      miMapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
      miMapa.MuestraTodoElMapa = false;
      miMapa.Refresh();
    }


    private void EnBotónBuscaDuplicados(object sender, EventArgs e)
    {
      ManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
    }


    private void EnCambióBarraDeDistancia(object sender, EventArgs e)
    {
      InicializaDistanciaMáxima();
    }


    private void InicializaDistanciaMáxima()
    {
      int distancia = miBarraDeDistancia.Value * 10;
      miTextoDistancia.Text = distancia + " m";
      miManejadorDePDIs.BuscadorDeDuplicados.DistanciaMáxima = distancia;
    }


    private void EnCambioBarraDeParecidoDelNombre(object sender, EventArgs e)
    {
      InicializaDistanciaHamming();
    }


    private void InicializaDistanciaHamming()
    {
      int distanciaHamming = miBarraDeParecidoDeNombre.Value;
      string texto = distanciaHamming.ToString();
      switch (distanciaHamming)
      {
        case 0:
          texto += " - Idéntico";
          break;
        case 1:
        case 2:
          texto += " - Muy Parecido";
          break;
        case 3:
          texto += " - Parecido";
          break;
        case 4:
        case 5:
        case 6:
          texto += " - Parecido";
          break;
      }

      miTextoParecidoDelNombre.Text = texto;
      miManejadorDePDIs.BuscadorDeDuplicados.DistanciaHamming = distanciaHamming;
    }
    #endregion
  }
}
