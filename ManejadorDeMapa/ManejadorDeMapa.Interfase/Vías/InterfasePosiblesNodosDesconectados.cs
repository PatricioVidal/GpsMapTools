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
    private readonly InterfaseMapaDeVíasSeleccionadas miMapa;
    private readonly InterfaseListaDeElementos miLista;
    private readonly MenuEditorDeVías miMenú;
    private readonly Brush miPincelDeBordeDeNodo = Brushes.Black;
    private readonly Pen miLápizDeViaDestino = new Pen(Color.LightSalmon, 11);
    private readonly Brush miPincelDePosibleNodoDesconectado = Brushes.Red;
    private readonly Brush miPincelDeNodoDeRuta = Brushes.Cyan;
    private readonly Brush miPincelDeNodo = Brushes.White;
    private readonly Color miColorItemEditado = Color.LightGreen;
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

      // Inicializa campos.
      miMapa = miInterfaseListaConMapaDeVías.InterfaseMapaDeVíasSeleccionadas;
      miLista = miInterfaseListaConMapaDeVías.InterfaseListaDeVías;
      miMenú = miInterfaseListaConMapaDeVías.MenuEditorDeVías;

      // Pone el llenador de items.
      miLista.PoneLlenadorDeItems(LlenaItems);

      // Escucha eventos.
      miMapa.DibujóElementos += EnDibujóElementos;
      miMenú.Editó += delegate
        {
          // Marca los Items editados.
          foreach(int i in miLista.SelectedIndices)
          {
            ListViewItem item = miLista.Items[i];
            item.BackColor = miColorItemEditado;         
          }

          // Regenera el mapa.
          miMapa.DibujaElementos();
        };

      // Añade menú "Guardar archivo de PDIs para localización de Nodos Desconectados". 
      miMenú.Items.Add(new ToolStripSeparator());
      ToolStripMenuItem menú = new ToolStripMenuItem("Guardar archivo de PDIs para localización de Nodos Desconectados");
      menú.Click += EnMenúGuardarArchivoPdisParaLocalizarNodosDesconectados;
      miMenú.Items.Add(menú);

      // Añade menú "Conectar Nodos Desconectados". 
      menú = new ToolStripMenuItem("Conectar Nodos Desconectados");
      menú.Click += EnMenúConectarNodosDesconectados;
      miMenú.Items.Add(menú);

      // Añade menú "Excluir de búsqueda de Nodos Desconectados". 
      menú = new ToolStripMenuItem("Marcar como Nodos Desconectados");
      menú.Click += EnMenúMarcarComoNodosDesconectados;
      miMenú.Items.Add(menú);
    }
    #endregion

    #region Métodos Privados
    private void EnInvalidado(object elEnviador, EventArgs losArgumentos)
    {
      // Borra las polilíneas y puntos adicionales que pudieran estar dibujadas en el mapa.
      miMapa.PolilíneasAdicionales.Clear();
      miMapa.PuntosAddicionales.Clear();
    }


    private void EnSeBuscaronPosiblesNodosDesconectados(object elEnviador, NúmeroDeItemsEventArgs losArgumentos)
    {
      miLista.RegeneraLista();

      // Borra las polilíneas y puntos adicionales que pudieran estar dibujadas en el mapa.
      miMapa.PolilíneasAdicionales.Clear();
      miMapa.PuntosAddicionales.Clear();
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
          posibleNodoDesconectado.Coordenadas.ToString(),
          posibleNodoDesconectado.Distancia.ToString("0.0"),
          posibleNodoDesconectado.Detalle);
      }
    }
    

    private void EnDibujóElementos(object elEnviador, EventArgs losArgumentos)
    {
      miMapa.PuntosAddicionales.Clear();
      double mínimaLatitud = double.PositiveInfinity;
      double máximaLatitud = double.NegativeInfinity;
      double mínimaLongitud = double.PositiveInfinity;
      double máximaLongitud = double.NegativeInfinity;

      // Dibuja los nodos como puntos addicionales para resaltarlos.
      foreach(int índiceSeleccionado in miLista.SelectedIndices)
      {
        ElementoConEtiqueta elemento = (ElementoConEtiqueta)miLista.Items[índiceSeleccionado].Tag;
        PosibleNodoDesconectado posibleNodoDesconectado = (PosibleNodoDesconectado)elemento.Etiqueta;

        // Dibuja la vía destino.
        miMapa.PolilíneasAdicionales.Add(new InterfaseMapa.PolilíneaAdicional(
          posibleNodoDesconectado.VíaDestino.Coordenadas,
          miLápizDeViaDestino));

        // Dibuja los nodos y los nodos de ruta
        DibujaNodos(posibleNodoDesconectado.Vía);
        DibujaNodos(posibleNodoDesconectado.VíaDestino);

        // Dibuja el nodo destino.
        miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.CoordenadasNodoDestino,
          miPincelDeBordeDeNodo,
          11));

        // Dibuja el posible nodo desconectado.
        miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Coordenadas,
          miPincelDeBordeDeNodo,
          13));
        miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
          posibleNodoDesconectado.Coordenadas,
          miPincelDePosibleNodoDesconectado,
          9));

        InterfaseMapa.ActualizaRectánguloQueEncierra(
          posibleNodoDesconectado.Coordenadas,
          ref mínimaLatitud,
          ref máximaLatitud,
          ref mínimaLongitud,
          ref máximaLongitud);
      }

      // Muestra los nodos desconectados.
      const float margen = 0.0001f;
      RectangleF rectánguloVisible = new RectangleF(
        (float)mínimaLongitud - margen,
        (float)mínimaLatitud - margen,
        (float)(máximaLongitud - mínimaLongitud) + (2 * margen),
        (float)(máximaLatitud - mínimaLatitud) + (2 * margen));
      miMapa.RectánguloVisibleEnCoordenadas = rectánguloVisible;
    }


    private void DibujaNodos(Vía laVía)
    {
      for (int i = 0; i < laVía.Coordenadas.Length; ++i)
      {
        if (laVía.CamposNodosDeRuta[i] != null)
        {
          miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
            laVía.Coordenadas[i],
            miPincelDeNodoDeRuta,
            11));
        }
        else
        {
          miMapa.PuntosAddicionales.Add(new InterfaseMapa.PuntoAdicional(
            laVía.Coordenadas[i],
            miPincelDeNodo,
            5));
        }
      }
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
      // Retornamos si no hay Vías seleccionadas.
      if (miLista.SelectedIndices.Count == 0)
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
          miMenú.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
        foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
        {
          // Crea los campos para el PDI.
          List<Campo> campos = new List<Campo> {
            new CampoNombre(string.Format("Nodo Desconectado de Vía # {0}",
              posibleNodoDesconectado.Vía.Número)),
            new CampoCoordenadas(
              "Data0",
              0,
              posibleNodoDesconectado.Coordenadas),
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
      // Retornamos si no hay Vías seleccionadas.
      int númeroDeNodosDesconectados = miLista.SelectedIndices.Count;
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

      // Conecta los nodos.
      ManejadorDeMapa.SuspendeEventos();
      IList<PosibleNodoDesconectado> posibleNodoDesconectados = miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
      const string razón = "Nodo conectado manualmente.";
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
      {
        Vía vía = posibleNodoDesconectado.Vía;
        int índice = posibleNodoDesconectado.Indice;
        Vía víaDestino = posibleNodoDesconectado.VíaDestino;
        int índiceNodoDestino = posibleNodoDesconectado.IndiceNodoDestino;
        bool laVíaTieneOtroNodoConLasMismasCoordenadas = false;

        #region Cambia las coordenadas del nodo desconectado si no son iguales.
        Coordenadas coordenadasNodo = posibleNodoDesconectado.Coordenadas;
        Coordenadas coordenadasNodoDestino = posibleNodoDesconectado.CoordenadasNodoDestino;
        if (coordenadasNodo != coordenadasNodoDestino)
        {
          // Antes the cambiar las coordenadas tenemos que asegurarnos que la vía no tiene
          // otro nodo con esas coordenadas.
          foreach (Coordenadas coordenadasNodoDeLaVía in vía.Coordenadas)
          {
            if (coordenadasNodoDeLaVía == coordenadasNodoDestino)
            {
              laVíaTieneOtroNodoConLasMismasCoordenadas = true;
              break;
            }
          }

          // Si la vía no tiene otro nodo con las mismas coordenadas
          // entonces lo conectamos.
          if (!laVíaTieneOtroNodoConLasMismasCoordenadas)
          {
            vía.CambiaCoordenadas(
              coordenadasNodoDestino,
              índice,
              razón);
          }
        }
        #endregion

        // Si la vía tiene otro nodo con las mismas coordenadas
        // entonces avisamos al usuario y no lo conectamos.
        if (laVíaTieneOtroNodoConLasMismasCoordenadas)
        {
          MessageBox.Show(
            string.Format("La Vía # {0} tiene otro nodo con las mismas coordenadas ({1}).\nEl nodo no se vá a conectar.", vía.Número, coordenadasNodo),
            "Conecta Nodos Desconectados",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
          continue;
        }

        #region Asegurarse que ambos nodos son ruteables.
        CampoNodoDeRuta campoNodoDeRuta = vía.CamposNodosDeRuta[índice];
        CampoNodoDeRuta campoNodoDeRutaDestino = víaDestino.CamposNodosDeRuta[índiceNodoDestino];

        // Si el posible nodo desconectado es de ruta y el nodo destino
        // no es de ruta entonces usamos el identificador global del
        // posible nodo desconectado para el nodo destino.
        if ((campoNodoDeRuta != null) && (campoNodoDeRutaDestino == null))
        {
          // Añade el nodo de ruta en la vía destino.
          víaDestino.AñadeNodoDeRuta(
            índiceNodoDestino,
            campoNodoDeRuta.IndentificadorGlobal,
            razón);
        }
        // Si el nodo destino es de ruta entonces usamos el identificador
        // global del nodo destino para el nodo desconectado.
        else if (campoNodoDeRutaDestino != null)
        {
          // Añade el nodo de ruta en la vía.
          vía.AñadeNodoDeRuta(
            índice,
            campoNodoDeRutaDestino.IndentificadorGlobal,
            razón);
        }
        // Si ninguno de los nodos es de ruta entonces generamos un nuevo
        // índentificador global.
        else
        {
          // Genera un nuevo identificador global.
          int nuevoIndentificadorGlobal = GeneraNuevoIdentificadorGlobal();

          // Añade el nodo de ruta a ambas vías.
          vía.AñadeNodoDeRuta(
            índice,
            nuevoIndentificadorGlobal,
            razón);
          víaDestino.AñadeNodoDeRuta(
            índiceNodoDestino,
            nuevoIndentificadorGlobal,
            razón);
        }
        #endregion
      }
      ManejadorDeMapa.RestableceEventos();

      // Notifica la edición.
      miMenú.EnvíaEventoEditó();
      #endregion
    }


    private int GeneraNuevoIdentificadorGlobal()
    {
      int máximoIndentificadorGlobal = 0;
      foreach (Vía víaDelMapa in ManejadorDeMapa.ManejadorDeVías.Elementos)
      {
        foreach (CampoNodoDeRuta campo in víaDelMapa.CamposNodosDeRuta)
        {
          if (campo != null)
          {
            máximoIndentificadorGlobal = Math.Max(máximoIndentificadorGlobal, campo.IndentificadorGlobal);
          }
        }
      }
      int nuevoIndentificadorGlobal = máximoIndentificadorGlobal + 1;
      return nuevoIndentificadorGlobal;
    }


    private void EnMenúMarcarComoNodosDesconectados(object elEnviador, EventArgs losArgumentos)
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
        string.Format("Está seguro que quiere marcar los {0} Nodos seleccionados como nodos desconectados?", númeroDeNodosDesconectados),
        "Marcar como Nodos Desconectados",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

      #region Marca Nodos como Desconectados.
      if (respuesta != DialogResult.Yes)
      {
        return;
      }

      // Añade el attributo a las vías.
      ManejadorDeMapa.SuspendeEventos();
      IList<PosibleNodoDesconectado> posibleNodoDesconectados = miInterfaseListaConMapaDeVías.MenuEditorDeVías.ObtieneEtiquetasSeleccionadas<PosibleNodoDesconectado>();
      foreach (PosibleNodoDesconectado posibleNodoDesconectado in posibleNodoDesconectados)
      {
        string atributo = BuscadorDePosiblesNodosDesconectados.AtributoNodoDesconectado
                          + ',' + posibleNodoDesconectado.Indice;
        posibleNodoDesconectado.Vía.AñadeAtributo(atributo);
      }
      ManejadorDeMapa.RestableceEventos();

      // Notifica la edición.
      miMenú.EnvíaEventoEditó();
      #endregion
    }
    #endregion
  }
}
