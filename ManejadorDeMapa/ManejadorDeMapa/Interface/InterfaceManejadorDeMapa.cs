using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace GpsYv.ManejadorDeMapa.Interface
{
  public partial class InterfaceManejadorDeMapa : Form
  {
    #region Campos
    private ManejadorDeMapa miManejadorDeMapa;
    private readonly IEscuchadorDeEstatus miEscuchadorDeEstatus;
    private List<ListViewItem> misItemsDeListaDeElementos = new List<ListViewItem>();
    #endregion

    #region Métodos Públicos
    public InterfaceManejadorDeMapa()
    {
      InitializeComponent();

      // Pone el nombre.
      this.Text = VentanaDeAcerca.AssemblyDescription + " - " + VentanaDeAcerca.AssemblyCompany;

      // Crea y asigna el escuchador de estatus.
      miEscuchadorDeEstatus = new EscuchadorDeEstatus(
        this,
        miTextoDeEstatus,
        miBarraDeProgreso,
        miTextoDeCoordenadas);

      miManejadorDeMapa = new ManejadorDeMapa(miEscuchadorDeEstatus);

      // Maneja eventos de modificación de elementos.
      miManejadorDeMapa.MapaNuevo += EnElementosModificados;
      miManejadorDeMapa.ElementosModificados += EnElementosModificados;

      // Asigna las propiedades de la interface de PDIs.
      miInterfaceManejadorDePDIs.ManejadorDeMapa = miManejadorDeMapa;
      miInterfaceManejadorDePDIs.EscuchadorDeEstatus = miEscuchadorDeEstatus;
      miInterfaceManejadorDePDIs.Tag = miPaginaDePDIs;

      // Asigna las propiedades de la interface de mapa.
      miInterfaceDeMapa.ManejadorDeMapa = miManejadorDeMapa;
      miInterfaceDeMapa.EscuchadorDeEstatus = miEscuchadorDeEstatus;
    }
    #endregion
      
    #region Métodos Privados
    private void EnMenuSalir(object sender, EventArgs e)
    {
      Application.Exit();
    }


    private void EnMenuAbrir(object elRemitente, EventArgs losArgumentos)
    {
      using (OpenFileDialog ventanaParaAbrirArchivo = new OpenFileDialog())
      {
        ventanaParaAbrirArchivo.CheckFileExists = true;
        ventanaParaAbrirArchivo.AutoUpgradeEnabled = true;
        ventanaParaAbrirArchivo.DefaultExt = "txt";
        ventanaParaAbrirArchivo.Filter = ManejadorDeMapa.FiltrosDeExtensiones;

        DialogResult respuesta = ventanaParaAbrirArchivo.ShowDialog();

        switch (respuesta)
        {
          case DialogResult.OK:
            string archivo = ventanaParaAbrirArchivo.FileName;
            try
            {
              miManejadorDeMapa.Abrir(archivo);
            }
            catch (Exception e)
            {
              Programa.MuestraExcepción(e);
            }
            break;
        }
      }
    }


    private void EnElementosModificados(object elEnviador, EventArgs losArgumentos)
    {
      // Vacia la lista.
      misItemsDeListaDeElementos.Clear();

      // Añade los elementos.
      IList<ElementoDelMapa> elementosDelMapa = miManejadorDeMapa.Elementos;
      misItemsDeListaDeElementos.Capacity = elementosDelMapa.Count;
      foreach (ElementoDelMapa elementoDelMapa in elementosDelMapa)
      {
        ListViewItem item = new ListViewItem(
          new string[] { 
            elementoDelMapa.Número.ToString(),
            elementoDelMapa.Clase,
            elementoDelMapa.TipoComoTexto(),
            elementoDelMapa.Descripción,
            elementoDelMapa.Nombre},
            -1);
        misItemsDeListaDeElementos.Add(item);
      }

      // Pone el número de elementos virtuales.
      miListaDeElementos.VirtualListSize = misItemsDeListaDeElementos.Count;

      // Actualiza la Pestaña.
      this.miPaginaDeElementos.Text = "Elementos (" + miListaDeElementos.VirtualListSize + ")";

      // Habilita los menus de Guardar.
      miMenuGuardar.Enabled = true;
      miMenuGuardarComo.Enabled = true;
    }


    private void EnMenuAcerca(object sender, EventArgs e)
    {
      Form ventanaDeAcerca = new Interface.VentanaDeAcerca();
      ventanaDeAcerca.ShowDialog();
    }


    private void EnMenuGuardarComo(object sender, EventArgs e)
    {
      string archivo = miManejadorDeMapa.Archivo;

      // Verifica que el archivo es válido.
      if (string.IsNullOrEmpty(archivo))
      {
        throw new InvalidOperationException("No se ha abierto ningún archivo.");
      }

      // Crea el nombre del archivo de salida.
      string directorio = Path.GetDirectoryName(archivo);
      string nombre = Path.GetFileNameWithoutExtension(archivo) + ".Corregido";
      string extensión = Path.GetExtension(archivo);
      string archivoDeSalida = nombre + extensión;

      // Ventana de guardar.
      SaveFileDialog ventanaDeGuardar = new SaveFileDialog();
      ventanaDeGuardar.AddExtension = true;
      ventanaDeGuardar.CheckPathExists = true;
      ventanaDeGuardar.Filter = ManejadorDeMapa.FiltrosDeExtensiones;
      ventanaDeGuardar.InitialDirectory = directorio;
      ventanaDeGuardar.FileName = archivoDeSalida;
      ventanaDeGuardar.OverwritePrompt = true;
      ventanaDeGuardar.ValidateNames = true;
      DialogResult respuesta = ventanaDeGuardar.ShowDialog();
      if (respuesta == DialogResult.OK)
      {
        miManejadorDeMapa.Guarda(ventanaDeGuardar.FileName);
      }
    }


    private void EnMenuGuardar(object sender, EventArgs e)
    {
      string archivo = miManejadorDeMapa.Archivo;

      // Verifica que el archivo es válido.
      if (string.IsNullOrEmpty(archivo))
      {
        throw new InvalidOperationException("No se ha abierto ningún archivo.");
      }

      // Asegurarse que el usuario quiere sobre-escribir el archivo.
      DialogResult respuesta = MessageBox.Show(
        "Esta seguro que quiere sobre-escribir " + Path.GetFileName(archivo),
        "Guardar Archivo",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);
      if (respuesta == DialogResult.Yes)
      {
        miManejadorDeMapa.Guarda(archivo);
      }
    }


    private void ObtieneItemDeListaDeElementos(object elEnviador, RetrieveVirtualItemEventArgs elArgumento)
    {
      elArgumento.Item = misItemsDeListaDeElementos[elArgumento.ItemIndex];
    }


    private void EnMenuArreglarPalabrasEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ArreglarPalabras();
    }


    private void EnMenuArreglarLetrasEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ArreglarLetras();
    }


    private void EnMenuProcesarTodoEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.ProcesarTodo();
    }

    private void EnMenuBuscarDuplicadosEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.BuscadorDeDuplicados.Procesa();
    }


    private void EnMenuBuscarErroresEnPDIs(object sender, EventArgs e)
    {
      miManejadorDeMapa.ManejadorDePDIs.BuscaErrores();
    }


    private void EnMenuPáginaWeb(object elEnviador, EventArgs losArgumentos)
    {
      System.Diagnostics.Process.Start("IExplore.exe", ((ToolStripItem)elEnviador).Text);
    }
    #endregion
  }
}
