﻿namespace GpsYv.ManejadorDeMapa.Interfase
{
    partial class InterfaseManejadorDeMapa
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDeMapa));
          this.miMenuPrincipal = new System.Windows.Forms.MenuStrip();
          this.miMenúMapa = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAbrirArchivo = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAceptarModificaciones = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuGuardar = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuSalir = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuProcesarTodoEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
          this.miMenúEliminarCaracteresEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuArreglarLetrasEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuArreglarPalabrasDePDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúBuscaDuplicadosEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúBuscarErroresEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúVías = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúProcesarTodoEnVías = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
          this.miMenúBuscarErroresEnVías = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúAyuda = new System.Windows.Forms.ToolStripMenuItem();
          this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAcerca = new System.Windows.Forms.ToolStripMenuItem();
          this.miEstatus = new System.Windows.Forms.StatusStrip();
          this.miTextoDeEstatus = new System.Windows.Forms.ToolStripStatusLabel();
          this.miTextoDeCoordenadas = new System.Windows.Forms.ToolStripStatusLabel();
          this.miBarraDeProgreso = new System.Windows.Forms.ToolStripProgressBar();
          this.miControladorDePestañasPrincipal = new System.Windows.Forms.TabControl();
          this.miPaginaDeElementos = new System.Windows.Forms.TabPage();
          this.miControladorDePestañas = new System.Windows.Forms.TabControl();
          this.miPáginaMapa = new System.Windows.Forms.TabPage();
          this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
          this.miPáginaTodos = new System.Windows.Forms.TabPage();
          this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
          this.miColumnaClase = new System.Windows.Forms.ColumnHeader();
          this.miPaginaDePDIs = new System.Windows.Forms.TabPage();
          this.miInterfaseManejadorDePDIs = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseManejadorDePDIs();
          this.miPáginaDeVías = new System.Windows.Forms.TabPage();
          this.miInterfaseManejadorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseManejadorDeVías();
          this.miMenuPrincipal.SuspendLayout();
          this.miEstatus.SuspendLayout();
          this.miControladorDePestañasPrincipal.SuspendLayout();
          this.miPaginaDeElementos.SuspendLayout();
          this.miControladorDePestañas.SuspendLayout();
          this.miPáginaMapa.SuspendLayout();
          this.miPáginaTodos.SuspendLayout();
          this.miPaginaDePDIs.SuspendLayout();
          this.miPáginaDeVías.SuspendLayout();
          this.SuspendLayout();
          // 
          // miMenuPrincipal
          // 
          this.miMenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenúMapa,
            this.miMenuPDIs,
            this.miMenúVías,
            this.miMenúAyuda});
          this.miMenuPrincipal.Location = new System.Drawing.Point(0, 0);
          this.miMenuPrincipal.Name = "miMenuPrincipal";
          this.miMenuPrincipal.Size = new System.Drawing.Size(784, 24);
          this.miMenuPrincipal.TabIndex = 0;
          this.miMenuPrincipal.Text = "MenuPrincipal";
          // 
          // miMenúMapa
          // 
          this.miMenúMapa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenuAbrirArchivo,
            this.miMenuAceptarModificaciones,
            this.miMenuGuardar,
            this.miMenuGuardarComo,
            this.miMenuSalir});
          this.miMenúMapa.Name = "miMenúMapa";
          this.miMenúMapa.Size = new System.Drawing.Size(49, 20);
          this.miMenúMapa.Text = "Mapa";
          // 
          // miMenuAbrirArchivo
          // 
          this.miMenuAbrirArchivo.Name = "miMenuAbrirArchivo";
          this.miMenuAbrirArchivo.Size = new System.Drawing.Size(199, 22);
          this.miMenuAbrirArchivo.Text = "&Abrir";
          this.miMenuAbrirArchivo.ToolTipText = "Abre un mapa.";
          this.miMenuAbrirArchivo.Click += new System.EventHandler(this.EnMenuAbrir);
          // 
          // miMenuAceptarModificaciones
          // 
          this.miMenuAceptarModificaciones.Enabled = false;
          this.miMenuAceptarModificaciones.Name = "miMenuAceptarModificaciones";
          this.miMenuAceptarModificaciones.Size = new System.Drawing.Size(199, 22);
          this.miMenuAceptarModificaciones.Text = "Aceptar Modificaciones";
          this.miMenuAceptarModificaciones.Click += new System.EventHandler(this.EnMenúAceptarModificaciones);
          // 
          // miMenuGuardar
          // 
          this.miMenuGuardar.Enabled = false;
          this.miMenuGuardar.Name = "miMenuGuardar";
          this.miMenuGuardar.Size = new System.Drawing.Size(199, 22);
          this.miMenuGuardar.Text = "Guardar";
          this.miMenuGuardar.ToolTipText = "Guarda el mapa con el mismo nombre.";
          this.miMenuGuardar.Click += new System.EventHandler(this.EnMenuGuardar);
          // 
          // miMenuGuardarComo
          // 
          this.miMenuGuardarComo.Enabled = false;
          this.miMenuGuardarComo.Name = "miMenuGuardarComo";
          this.miMenuGuardarComo.Size = new System.Drawing.Size(199, 22);
          this.miMenuGuardarComo.Text = "Guardar Como ...";
          this.miMenuGuardarComo.ToolTipText = "Guarda el mapa con el un nombre nuevo.";
          this.miMenuGuardarComo.Click += new System.EventHandler(this.EnMenuGuardarComo);
          // 
          // miMenuSalir
          // 
          this.miMenuSalir.Name = "miMenuSalir";
          this.miMenuSalir.Size = new System.Drawing.Size(199, 22);
          this.miMenuSalir.Text = "&Salir";
          this.miMenuSalir.ToolTipText = "Cierra la aplicación.";
          this.miMenuSalir.Click += new System.EventHandler(this.EnMenuSalir);
          // 
          // miMenuPDIs
          // 
          this.miMenuPDIs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenuProcesarTodoEnPDIs,
            this.toolStripSeparator1,
            this.miMenúEliminarCaracteresEnPDIs,
            this.miMenuArreglarLetrasEnPDIs,
            this.miMenuArreglarPalabrasDePDIs,
            this.miMenúBuscaDuplicadosEnPDIs,
            this.miMenúBuscarErroresEnPDIs});
          this.miMenuPDIs.Name = "miMenuPDIs";
          this.miMenuPDIs.Size = new System.Drawing.Size(42, 20);
          this.miMenuPDIs.Text = "PDIs";
          this.miMenuPDIs.ToolTipText = "Menús para PDIs.";
          // 
          // miMenuProcesarTodoEnPDIs
          // 
          this.miMenuProcesarTodoEnPDIs.Name = "miMenuProcesarTodoEnPDIs";
          this.miMenuProcesarTodoEnPDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenuProcesarTodoEnPDIs.Text = "Procesar Todo";
          this.miMenuProcesarTodoEnPDIs.Click += new System.EventHandler(this.EnMenuProcesarTodoEnPDIs);
          // 
          // toolStripSeparator1
          // 
          this.toolStripSeparator1.Name = "toolStripSeparator1";
          this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
          // 
          // miMenúEliminarCaracteresEnPDIs
          // 
          this.miMenúEliminarCaracteresEnPDIs.Name = "miMenúEliminarCaracteresEnPDIs";
          this.miMenúEliminarCaracteresEnPDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenúEliminarCaracteresEnPDIs.Text = "1. Eliminar Caracteres Inválidos";
          this.miMenúEliminarCaracteresEnPDIs.Click += new System.EventHandler(this.EnMenúEliminarCaracteresEnPDIs);
          // 
          // miMenuArreglarLetrasEnPDIs
          // 
          this.miMenuArreglarLetrasEnPDIs.Name = "miMenuArreglarLetrasEnPDIs";
          this.miMenuArreglarLetrasEnPDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenuArreglarLetrasEnPDIs.Text = "2. Arreglar Letras en Nombres";
          this.miMenuArreglarLetrasEnPDIs.Click += new System.EventHandler(this.EnMenuArreglarLetrasEnPDIs);
          // 
          // miMenuArreglarPalabrasDePDIs
          // 
          this.miMenuArreglarPalabrasDePDIs.Name = "miMenuArreglarPalabrasDePDIs";
          this.miMenuArreglarPalabrasDePDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenuArreglarPalabrasDePDIs.Text = "3. Arreglar Palabras en Nombres";
          this.miMenuArreglarPalabrasDePDIs.Click += new System.EventHandler(this.EnMenuArreglarPalabrasEnPDIs);
          // 
          // miMenúBuscaDuplicadosEnPDIs
          // 
          this.miMenúBuscaDuplicadosEnPDIs.Name = "miMenúBuscaDuplicadosEnPDIs";
          this.miMenúBuscaDuplicadosEnPDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenúBuscaDuplicadosEnPDIs.Text = "4. Buscar Duplicados";
          this.miMenúBuscaDuplicadosEnPDIs.Click += new System.EventHandler(this.EnMenuBuscarDuplicadosEnPDIs);
          // 
          // miMenúBuscarErroresEnPDIs
          // 
          this.miMenúBuscarErroresEnPDIs.Name = "miMenúBuscarErroresEnPDIs";
          this.miMenúBuscarErroresEnPDIs.Size = new System.Drawing.Size(243, 22);
          this.miMenúBuscarErroresEnPDIs.Text = "5. Buscar Errores";
          this.miMenúBuscarErroresEnPDIs.Click += new System.EventHandler(this.EnMenuBuscarErroresEnPDIs);
          // 
          // miMenúVías
          // 
          this.miMenúVías.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenúProcesarTodoEnVías,
            this.toolStripSeparator2,
            this.miMenúBuscarErroresEnVías});
          this.miMenúVías.Name = "miMenúVías";
          this.miMenúVías.Size = new System.Drawing.Size(40, 20);
          this.miMenúVías.Text = "Vías";
          this.miMenúVías.ToolTipText = "Menús para Vías";
          // 
          // miMenúProcesarTodoEnVías
          // 
          this.miMenúProcesarTodoEnVías.Name = "miMenúProcesarTodoEnVías";
          this.miMenúProcesarTodoEnVías.Size = new System.Drawing.Size(160, 22);
          this.miMenúProcesarTodoEnVías.Text = "Procesar Todo";
          this.miMenúProcesarTodoEnVías.Click += new System.EventHandler(this.EnMenuProcesarTodoEnVías);
          // 
          // toolStripSeparator2
          // 
          this.toolStripSeparator2.Name = "toolStripSeparator2";
          this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
          // 
          // miMenúBuscarErroresEnVías
          // 
          this.miMenúBuscarErroresEnVías.Name = "miMenúBuscarErroresEnVías";
          this.miMenúBuscarErroresEnVías.Size = new System.Drawing.Size(160, 22);
          this.miMenúBuscarErroresEnVías.Text = "1. Buscar Errores";
          this.miMenúBuscarErroresEnVías.Click += new System.EventHandler(this.EnMenúBuscarErroresEnVías);
          // 
          // miMenúAyuda
          // 
          this.miMenúAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaToolStripMenuItem,
            this.miMenuAcerca});
          this.miMenúAyuda.Name = "miMenúAyuda";
          this.miMenúAyuda.Size = new System.Drawing.Size(53, 20);
          this.miMenúAyuda.Text = "Ayuda";
          // 
          // acercaToolStripMenuItem
          // 
          this.acercaToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.acercaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
          this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
          this.acercaToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
          this.acercaToolStripMenuItem.Text = "www.gpsyv.net";
          this.acercaToolStripMenuItem.ToolTipText = "Va a www.gpsyv.net";
          this.acercaToolStripMenuItem.Click += new System.EventHandler(this.EnMenuPáginaWeb);
          // 
          // miMenuAcerca
          // 
          this.miMenuAcerca.Name = "miMenuAcerca";
          this.miMenuAcerca.Size = new System.Drawing.Size(154, 22);
          this.miMenuAcerca.Text = "Acerca";
          this.miMenuAcerca.Click += new System.EventHandler(this.EnMenuAcerca);
          // 
          // miEstatus
          // 
          this.miEstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTextoDeEstatus,
            this.miTextoDeCoordenadas,
            this.miBarraDeProgreso});
          this.miEstatus.Location = new System.Drawing.Point(0, 542);
          this.miEstatus.Name = "miEstatus";
          this.miEstatus.Size = new System.Drawing.Size(784, 22);
          this.miEstatus.TabIndex = 1;
          this.miEstatus.Text = "Estatus";
          // 
          // miTextoDeEstatus
          // 
          this.miTextoDeEstatus.AutoSize = false;
          this.miTextoDeEstatus.Name = "miTextoDeEstatus";
          this.miTextoDeEstatus.Size = new System.Drawing.Size(467, 17);
          this.miTextoDeEstatus.Spring = true;
          this.miTextoDeEstatus.Text = "Estatus";
          this.miTextoDeEstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // miTextoDeCoordenadas
          // 
          this.miTextoDeCoordenadas.AutoSize = false;
          this.miTextoDeCoordenadas.Name = "miTextoDeCoordenadas";
          this.miTextoDeCoordenadas.Size = new System.Drawing.Size(150, 17);
          this.miTextoDeCoordenadas.Text = "Coordenadas";
          this.miTextoDeCoordenadas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
          // 
          // miBarraDeProgreso
          // 
          this.miBarraDeProgreso.AutoSize = false;
          this.miBarraDeProgreso.Name = "miBarraDeProgreso";
          this.miBarraDeProgreso.Size = new System.Drawing.Size(150, 16);
          // 
          // miControladorDePestañasPrincipal
          // 
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPaginaDeElementos);
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPaginaDePDIs);
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPáginaDeVías);
          this.miControladorDePestañasPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miControladorDePestañasPrincipal.Location = new System.Drawing.Point(0, 24);
          this.miControladorDePestañasPrincipal.Name = "miControladorDePestañasPrincipal";
          this.miControladorDePestañasPrincipal.SelectedIndex = 0;
          this.miControladorDePestañasPrincipal.Size = new System.Drawing.Size(784, 518);
          this.miControladorDePestañasPrincipal.TabIndex = 3;
          // 
          // miPaginaDeElementos
          // 
          this.miPaginaDeElementos.Controls.Add(this.miControladorDePestañas);
          this.miPaginaDeElementos.Location = new System.Drawing.Point(4, 22);
          this.miPaginaDeElementos.Name = "miPaginaDeElementos";
          this.miPaginaDeElementos.Size = new System.Drawing.Size(776, 492);
          this.miPaginaDeElementos.TabIndex = 1;
          this.miPaginaDeElementos.Text = "Elementos";
          this.miPaginaDeElementos.UseVisualStyleBackColor = true;
          // 
          // miControladorDePestañas
          // 
          this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
          this.miControladorDePestañas.Controls.Add(this.miPáginaTodos);
          this.miControladorDePestañas.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miControladorDePestañas.Location = new System.Drawing.Point(0, 0);
          this.miControladorDePestañas.Name = "miControladorDePestañas";
          this.miControladorDePestañas.SelectedIndex = 0;
          this.miControladorDePestañas.Size = new System.Drawing.Size(776, 492);
          this.miControladorDePestañas.TabIndex = 1;
          // 
          // miPáginaMapa
          // 
          this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
          this.miPáginaMapa.Location = new System.Drawing.Point(4, 22);
          this.miPáginaMapa.Name = "miPáginaMapa";
          this.miPáginaMapa.Padding = new System.Windows.Forms.Padding(3);
          this.miPáginaMapa.Size = new System.Drawing.Size(768, 466);
          this.miPáginaMapa.TabIndex = 0;
          this.miPáginaMapa.Text = "Mapa";
          this.miPáginaMapa.UseVisualStyleBackColor = true;
          // 
          // miInterfaseDeMapa
          // 
          this.miInterfaseDeMapa.BackColor = System.Drawing.Color.Transparent;
          this.miInterfaseDeMapa.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miInterfaseDeMapa.EscuchadorDeEstatus = null;
          this.miInterfaseDeMapa.Location = new System.Drawing.Point(3, 3);
          this.miInterfaseDeMapa.ManejadorDeMapa = null;
          this.miInterfaseDeMapa.MuestraPDIs = false;
          this.miInterfaseDeMapa.MuestraPolígonos = false;
          this.miInterfaseDeMapa.MuestraPolilíneas = false;
          this.miInterfaseDeMapa.MuestraTodoElMapa = true;
          this.miInterfaseDeMapa.MuestraTodosLosElementos = true;
          this.miInterfaseDeMapa.MuestraVías = false;
          this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
          this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
          this.miInterfaseDeMapa.Size = new System.Drawing.Size(762, 460);
          this.miInterfaseDeMapa.TabIndex = 1;
          // 
          // miPáginaTodos
          // 
          this.miPáginaTodos.Controls.Add(this.miLista);
          this.miPáginaTodos.Location = new System.Drawing.Point(4, 22);
          this.miPáginaTodos.Name = "miPáginaTodos";
          this.miPáginaTodos.Padding = new System.Windows.Forms.Padding(3);
          this.miPáginaTodos.Size = new System.Drawing.Size(768, 466);
          this.miPáginaTodos.TabIndex = 1;
          this.miPáginaTodos.Text = "Todos";
          this.miPáginaTodos.UseVisualStyleBackColor = true;
          // 
          // miLista
          // 
          this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaClase});
          this.miLista.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miLista.FullRowSelect = true;
          this.miLista.GridLines = true;
          this.miLista.Location = new System.Drawing.Point(3, 3);
          this.miLista.Name = "miLista";
          this.miLista.Size = new System.Drawing.Size(762, 460);
          this.miLista.TabIndex = 1;
          this.miLista.UseCompatibleStateImageBehavior = false;
          this.miLista.View = System.Windows.Forms.View.Details;
          this.miLista.VirtualMode = true;
          // 
          // miColumnaClase
          // 
          this.miColumnaClase.Text = "Clase";
          this.miColumnaClase.Width = 85;
          // 
          // miPaginaDePDIs
          // 
          this.miPaginaDePDIs.Controls.Add(this.miInterfaseManejadorDePDIs);
          this.miPaginaDePDIs.Location = new System.Drawing.Point(4, 22);
          this.miPaginaDePDIs.Name = "miPaginaDePDIs";
          this.miPaginaDePDIs.Padding = new System.Windows.Forms.Padding(3);
          this.miPaginaDePDIs.Size = new System.Drawing.Size(776, 492);
          this.miPaginaDePDIs.TabIndex = 0;
          this.miPaginaDePDIs.Text = "PDIs";
          this.miPaginaDePDIs.UseVisualStyleBackColor = true;
          // 
          // miInterfaseManejadorDePDIs
          // 
          this.miInterfaseManejadorDePDIs.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miInterfaseManejadorDePDIs.EscuchadorDeEstatus = null;
          this.miInterfaseManejadorDePDIs.Location = new System.Drawing.Point(3, 3);
          this.miInterfaseManejadorDePDIs.ManejadorDeMapa = null;
          this.miInterfaseManejadorDePDIs.Name = "miInterfaseManejadorDePDIs";
          this.miInterfaseManejadorDePDIs.Size = new System.Drawing.Size(770, 486);
          this.miInterfaseManejadorDePDIs.TabIndex = 0;
          // 
          // miPáginaDeVías
          // 
          this.miPáginaDeVías.Controls.Add(this.miInterfaseManejadorDeVías);
          this.miPáginaDeVías.Location = new System.Drawing.Point(4, 22);
          this.miPáginaDeVías.Name = "miPáginaDeVías";
          this.miPáginaDeVías.Size = new System.Drawing.Size(776, 492);
          this.miPáginaDeVías.TabIndex = 2;
          this.miPáginaDeVías.Text = "Vías";
          this.miPáginaDeVías.UseVisualStyleBackColor = true;
          // 
          // miInterfaseManejadorDeVías
          // 
          this.miInterfaseManejadorDeVías.Dock = System.Windows.Forms.DockStyle.Fill;
          this.miInterfaseManejadorDeVías.EscuchadorDeEstatus = null;
          this.miInterfaseManejadorDeVías.Location = new System.Drawing.Point(0, 0);
          this.miInterfaseManejadorDeVías.ManejadorDeMapa = null;
          this.miInterfaseManejadorDeVías.Name = "miInterfaseManejadorDeVías";
          this.miInterfaseManejadorDeVías.Size = new System.Drawing.Size(776, 492);
          this.miInterfaseManejadorDeVías.TabIndex = 0;
          // 
          // InterfaseManejadorDeMapa
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(784, 564);
          this.Controls.Add(this.miControladorDePestañasPrincipal);
          this.Controls.Add(this.miEstatus);
          this.Controls.Add(this.miMenuPrincipal);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.MainMenuStrip = this.miMenuPrincipal;
          this.MinimumSize = new System.Drawing.Size(300, 200);
          this.Name = "InterfaseManejadorDeMapa";
          this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
          this.Load += new System.EventHandler(this.EnCargarForma);
          this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnCerrarForma);
          this.miMenuPrincipal.ResumeLayout(false);
          this.miMenuPrincipal.PerformLayout();
          this.miEstatus.ResumeLayout(false);
          this.miEstatus.PerformLayout();
          this.miControladorDePestañasPrincipal.ResumeLayout(false);
          this.miPaginaDeElementos.ResumeLayout(false);
          this.miControladorDePestañas.ResumeLayout(false);
          this.miPáginaMapa.ResumeLayout(false);
          this.miPáginaTodos.ResumeLayout(false);
          this.miPaginaDePDIs.ResumeLayout(false);
          this.miPáginaDeVías.ResumeLayout(false);
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip miMenuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem miMenúMapa;
        private System.Windows.Forms.ToolStripMenuItem miMenuAbrirArchivo;
        private System.Windows.Forms.ToolStripMenuItem miMenuGuardarComo;
        private System.Windows.Forms.ToolStripMenuItem miMenuSalir;
        private System.Windows.Forms.ToolStripMenuItem miMenuPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúAyuda;
        private System.Windows.Forms.StatusStrip miEstatus;
        private System.Windows.Forms.TabControl miControladorDePestañasPrincipal;
        private System.Windows.Forms.TabPage miPaginaDePDIs;
        private System.Windows.Forms.ToolStripStatusLabel miTextoDeEstatus;
        private System.Windows.Forms.ToolStripProgressBar miBarraDeProgreso;
        private System.Windows.Forms.ToolStripStatusLabel miTextoDeCoordenadas;
        private System.Windows.Forms.ToolStripMenuItem acercaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miMenuAcerca;
        private System.Windows.Forms.ToolStripMenuItem miMenuArreglarLetrasEnPDIs;
        private System.Windows.Forms.TabPage miPaginaDeElementos;
        private System.Windows.Forms.ToolStripMenuItem miMenuArreglarPalabrasDePDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuProcesarTodoEnPDIs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseManejadorDePDIs miInterfaseManejadorDePDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscaDuplicadosEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuGuardar;
        private System.Windows.Forms.TabControl miControladorDePestañas;
        private System.Windows.Forms.TabPage miPáginaMapa;
        private InterfaseMapa miInterfaseDeMapa;
        private System.Windows.Forms.TabPage miPáginaTodos;
        private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
        private System.Windows.Forms.ColumnHeader miColumnaClase;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscarErroresEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúEliminarCaracteresEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuAceptarModificaciones;
        private System.Windows.Forms.ToolStripMenuItem miMenúVías;
        private System.Windows.Forms.ToolStripMenuItem miMenúProcesarTodoEnVías;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscarErroresEnVías;
        private System.Windows.Forms.TabPage miPáginaDeVías;
        private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseManejadorDeVías miInterfaseManejadorDeVías;
    }
}

