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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GpsYv.ManejadorDeMapa.Vías;
using System.IO;
using GpsYv.ManejadorDeMapa.Pdis;

namespace GpsYv.ManejadorDeMapa.Interfase.Vías
{
  /// <summary>
  /// Interfase de Posibles Nodos Desconectados.
  /// </summary>
  public partial class InterfasePosiblesNodosDesconectados : InterfaseBase
  {
    #region Campos
    private BuscadorDePosiblesNodosDesconectados miBuscadorDePosiblesNodosDesconectados;
    private readonly Brush miPincelDeBordeDeNodo = Brushes.Black;
    private readonly Brush miPincelDeNodo = Brushes.Yellow;
    private readonly Pen miLápizDeViaConElPosibleNodoDesconectado = new Pen(Color.LightSalmon, 9);
    private readonly Brush miPincelDePosibleNodoDesconectado = Brushes.LightSalmon;
    private readonly Color miColorItemEditado = Color.LightGray;
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
        if (miBuscadorDePosiblesNodosDesconectados != null)
        {
          miBuscadorDePosiblesNodosDesconectados.Invalidado -= EnInvalidado;
          miBuscadorDePosiblesNodosDesconectados.Procesó -= EnSeBuscaronPosiblesNodosDesconectados;
        }

        // Pone el nuevo manejador de mapa.
        base.ManejadorDeMapa = value;

        // Maneja eventos.
        if (value != null)
        {
          miBuscadorDePosiblesNodosDesconectados = value.ManejadorDeVías.BuscadorDePosiblesNodosDesconectados;
          miBuscadorDePosiblesNodosDesconectados.Invalidado += EnInvalidado;
          miBuscadorDePosiblesNodosDesconectados.Procesó += EnSeBuscaronPosiblesNodosDesconectados;
          InicializaDistanciaMáxima();
        }

        // Pone el manejador de mapa en los componentes.
        miInterfaseListaConMapaDeVías.ManejadorDeMapa = value;
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
        miInterfaseListaConMapaDeVías.EscuchadorDeEstatus = value;
      }
    }
    #endregion

    #region Constructor.
    /// <summary>
    /// Constructor.
    /// </summary>
    public InterfasePosiblesNodosDesconectados()
    {
      InitializeComponent();

      // Pone el llenador de items.
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.PoneLlenadorDeItems(LlenaItems);

      miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.DibujóElementos += EnDibujóElementos;

      // Escucha el evento de edición de Vías.
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Editó += delegate(object elObjecto, EventArgs losArgumentos)
      {
        // Marca los Items editados.
        foreach(int i in miInterfaseListaConMapaDeVías.InterfaseListaDeVías.SelectedIndices)
        {
          ListViewItem item = miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Items[i];
          item.BackColor = miColorItemEditado;         
        }
      };


      // Añade menú "Guardar archivo de PDIs para localización de Nodos Desconectados". 
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Items.Add(new ToolStripSeparator());
      ToolStripMenuItem menú = new ToolStripMenuItem("Guardar archivo de PDIs para localización de Nodos Desconectados");
      menú.Click += EnMenúGuardarArchivoPdisParaLocalizarNodosDesconectados;
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Items.Add(menú);

      // Añade menú "Conectar Nodos Desconectados". 
      menú = new ToolStripMenuItem("Conectar Nodos Desconectados");
      menú.Click += EnMenúConectarNodosDesconectados;
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Items.Add(menú);
      // TODO: Habilitar el menú cuando la lógica esté completa.
      menú.Enabled = false;

      // Añade menú "Excluir de búsqueda de Nodos Desconectados". 
      menú = new ToolStripMenuItem("Excluir de búsqueda de Nodos Desconectados");
      menú.Click += EnMenúExcluirDeBúsquedaDeNodosDesconectados;
      miInterfaseListaConMapaDeVías.MenuEditorDeVías.Items.Add(menú);
      // TODO: Habilitar el menú cuando la lógica esté completa.
      menú.Enabled = false;
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();

      // Borra las polilíneas adicionales que pudieran estar dibujadas en el mapa.
      miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas.PolilíneasAdicionales.Clear();
    }


    private void EnSeBuscaronPosiblesNodosDesconectados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miInterfaseListaConMapaDeVías.InterfaseListaDeVías.RegeneraLista();
    }


    private void LlenaItems(InterfaseListaDeElementos laLista)
    {
      // Añade los elementos.
      IList<PosibleNodoDesconectado> posibleNodosDesconectados = miBuscadorDePosiblesNodosDesconectados.PosibleNodosDesconectados;
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodosDesconectados)
      {
        ElementoConEtiqueta elemento = new ElementoConEtiqueta(posibleNodoDesconectado.Vía, posibleNodoDesconectado);
        laLista.AñadeItem(
          elemento,
          posibleNodoDesconectado.Nodo.ToString(),
          posibleNodoDesconectado.Distancia.ToString("0.0"),
          posibleNodoDesconectado.Detalle);
      }
    }
    

    private void EnDibujóElementos(object elEnviador, EventArgs losArgumentos)
    {
      InterfaseMapa mapa = miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas;
      mapa.PuntosAddicionales.Clear();
      RectangleF rectánguloQueEncierra = new RectangleF(
        float.PositiveInfinity,
        float.PositiveInfinity,
        0,
        0);


      // Dibuja los nodos como puntos addicionales para resaltarlos.
      foreach(int i in miInterfaseListaConMapaDeVías.InterfaseListaDeVías.SelectedIndices)
      {
        ElementoConEtiqueta elemento = (ElementoConEtiqueta)miInterfaseListaConMapaDeVías.InterfaseListaDeVías.Items[i].Tag;
        PosibleNodoDesconectado posibleNodoDesconectado = (PosibleNodoDesconectado)elemento.Etiqueta;

        // Dibuja la vía del posible nodo desconectado.
        mapa.PolilíneasAdicionales.Add(new InterfaseMapa.PolilíneaAdicional(
          posibleNodoDesconectado.VíaConElPosibleNodoDesconectado.Coordenadas,
          miLápizDeViaConElPosibleNodoDesconectado));

        // Dibuja el posible nodo desconectado.
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.PosiblesNodoDesconectado,
          miPincelDeBordeDeNodo,
          11));
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.PosiblesNodoDesconectado,
          miPincelDePosibleNodoDesconectado,
          7));

        // Dibuja el nodo.
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Nodo,
          miPincelDeBordeDeNodo,
          13));
        mapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Nodo,
          miPincelDeNodo,
          9));

        InterfaseMapa.ActualizaRectánguloQueEncierra(
          posibleNodoDesconectado.PosiblesNodoDesconectado,
          ref rectánguloQueEncierra);
      }

      const float margen = 0.0001f;
      RectangleF rectánguloVisible = new RectangleF(
        rectánguloQueEncierra.X - margen,
        rectánguloQueEncierra.Y - margen,
        rectánguloQueEncierra.Width + (2 * margen),
        rectánguloQueEncierra.Height + (2 * margen));
      mapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
    }

    
    private void EnCambióBarraDeDistancia(object sender, EventArgs e)
    {
      InicializaDistanciaMáxima();
    }


    private void InicializaDistanciaMáxima()
    {
      int distancia = miBarraDeDistancia.Value * 5;
      miTextoDistancia.Text = distancia + " m";
      miBuscadorDePosiblesNodosDesconectados.DistanciaMáxima = distancia;
    }


    private void EnBotónBuscaPosiblesNodosDesconectados(object sender, EventArgs e)
    {
      miBuscadorDePosiblesNodosDesconectados.Procesa();
    }


    private void EnMenúGuardarArchivoPdisParaLocalizarNodosDesconectados(object elObjecto, EventArgs losArgumentos)
    {
      ListView lista = miInterfaseListaConMapaDeVías.InterfaseListaDeVías;

      // Retornamos si no hay Vías seleccionadas.
      if (lista.SelectedIndices.Count == 0)
      {
        return;
      }

      // Crea el nombre del archivo de salida.
      string archivo = Path.GetFullPath(ManejadorDeMapa.Archivo);
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileName(archivo);
      string nombreDeSalida = Path.ChangeExtension(nombre, ".PDIsDeNodosDesconectados.mp");

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog
      {
        Title = "Guarda archivo de PDIs para localización de Nodos Desconectados",
        AutoUpgradeEnabled = true,
        AddExtension = true,
        CheckPathExists = true,
        Filter = ManejadorDeMapa.FiltrosDeExtensiones,
        InitialDirectory = directorio,
        FileName = nombreDeSalida,
        OverwritePrompt = true,
        ValidateNames = true
      };
      DialogResult respuesta = ventanaDeGuardar.ShowDialog();
      if (respuesta == DialogResult.OK)
      {
        // El primer elemento de cada lista tiene que ser el encabezado.
        List<ElementoDelMapa> elementos = new List<ElementoDelMapa> { ManejadorDeMapa.Encabezado };

        // Genera la lista de PDIs.
        IList<PosibleNodoDesconectado> posibleNodoDesconectados =
          miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
        foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
        {
          // Crea los campos para el PDI.
          List<Campo> campos = new List<Campo> {
            new CampoNombre(string.Format("Nodo Desconectado de Vía # {0}",
              posibleNodoDesconectado.VíaConElPosibleNodoDesconectado.Número)),
            new CampoCoordenadas(
              "Data0",
              0,
              posibleNodoDesconectado.PosiblesNodoDesconectado),
            new CampoTipo("0x1604"),
            new CampoGenérico("EndLevel", "3")
          };

          // Crea el PDI y añadelo a la lista.
          Pdi pdi = new Pdi(
            ManejadorDeMapa,
            0,
            "POI",
            campos);
          elementos.Add(pdi);
        }

        // Guarda el archivo.
        new EscritorDeFormatoPolish(
          ventanaDeGuardar.FileName,
          elementos,
          miInterfaseListaConMapaDeVías.EscuchadorDeEstatus);
      }
    }


    private void EnMenúConectarNodosDesconectados(object elEnviador, EventArgs losArgumentos)
    {
      ListView lista = miInterfaseListaConMapaDeVías.InterfaseListaDeVías;

      // Retornamos si no hay Vías seleccionadas.
      int númeroDeNodosDesconectados = lista.SelectedIndices.Count;
      if (númeroDeNodosDesconectados == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar el Límite de Velocidad.
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere conectar los {0} Nodos seleccionadas de próximas búsquedas de Parámetros de Ruta Estándar?", númeroDeNodosDesconectados),
        "Conectar Nodos Desconectados",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      #region Conectar Nodos Desconectados.
      if (respuesta != DialogResult.Yes)
      {
        return;
      }

      // Cambia las vías.
      ManejadorDeMapa.SuspendeEventos();
      IList<PosibleNodoDesconectado> posibleNodoDesconectados = miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
      {
        // Poner la lógica.
      }
      ManejadorDeMapa.RestableceEventos();
      #endregion
    }


    private void EnMenúExcluirDeBúsquedaDeNodosDesconectados(object elEnviador, EventArgs losArgumentos)
    {
      ListView lista = miInterfaseListaConMapaDeVías.InterfaseListaDeVías;

      // Retornamos si no hay Vías seleccionadas.
      int númeroDeNodosDesconectados = lista.SelectedIndices.Count;
      if (númeroDeNodosDesconectados == 0)
      {
        return;
      }

      // Pregunta si se quiere Estandarizar el Límite de Velocidad.
      DialogResult respuesta = MessageBox.Show(
        string.Format("Está seguro que quiere conectar los {0} Nodos seleccionadas de próximas búsquedas de Parámetros de Ruta Estándar?", númeroDeNodosDesconectados),
        "Conectar Nodos Desconectados",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      #region Conectar Nodos Desconectados.
      if (respuesta != DialogResult.Yes)
      {
        return;
      }

      // Cambia las vías.
      ManejadorDeMapa.SuspendeEventos();
      IList<PosibleNodoDesconectado> posibleNodoDesconectados = miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
      {
        // Poner la lógica.
      }
      ManejadorDeMapa.RestableceEventos();
      #endregion
    }
    #endregion
  }
}
