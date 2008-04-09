namespace GpsYv.ManejadorDeMapa.Interfase
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
          this.components = new System.ComponentModel.Container();
          System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterfaseManejadorDeMapa));
          System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
          System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
          System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
          this.miMenúProcesarTodo = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuProcesarTodoEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúEliminarCaracteresEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuArreglarLetrasEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuArreglarPalabrasEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúBuscaDuplicadosEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúBuscarErroresEnPDIs = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúVías = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúProcesarTodoEnVías = new System.Windows.Forms.ToolStripMenuItem();
          this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
          this.miMenúBuscarErroresEnVías = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuPrincipal = new System.Windows.Forms.MenuStrip();
          this.miMenúMapa = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAbrirArchivo = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAceptarModificaciones = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuGuardar = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuSalir = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenúAyuda = new System.Windows.Forms.ToolStripMenuItem();
          this.acercaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
          this.miMenuAcerca = new System.Windows.Forms.ToolStripMenuItem();
          this.miEstatus = new System.Windows.Forms.StatusStrip();
          this.miTextoDeEstatus = new System.Windows.Forms.ToolStripStatusLabel();
          this.miTextoDeCoordenadas = new System.Windows.Forms.ToolStripStatusLabel();
          this.miBarraDeProgreso = new System.Windows.Forms.ToolStripProgressBar();
          this.miControladorDePestañasPrincipal = new GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas();
          this.miPaginaDeElementos = new System.Windows.Forms.TabPage();
          this.miControladorDePestañas = new GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas();
          this.miPáginaMapa = new System.Windows.Forms.TabPage();
          this.miInterfaseDeMapa = new GpsYv.ManejadorDeMapa.Interfase.InterfaseMapa();
          this.miPáginaTodos = new System.Windows.Forms.TabPage();
          this.miLista = new GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos();
          this.miColumnaClase = new System.Windows.Forms.ColumnHeader();
          this.miPaginaDePDIs = new System.Windows.Forms.TabPage();
          this.miInterfaseManejadorDePDIs = new GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseManejadorDePDIs();
          this.miPáginaDeVías = new System.Windows.Forms.TabPage();
          this.miInterfaseManejadorDeVías = new GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseManejadorDeVías();
          this.menúBuscarPosiblesErroresDeRuteoEnVías = new System.Windows.Forms.ToolStripMenuItem();
          toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
          toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
          toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
          toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
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
          // toolStripMenuItem1
          // 
          toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenúProcesarTodo,
            toolStripSeparator3,
            this.miMenuPDIs,
            this.miMenúVías});
          toolStripMenuItem1.Name = "toolStripMenuItem1";
          resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
          // 
          // miMenúProcesarTodo
          // 
          this.miMenúProcesarTodo.Name = "miMenúProcesarTodo";
          resources.ApplyResources(this.miMenúProcesarTodo, "miMenúProcesarTodo");
          this.miMenúProcesarTodo.Click += new System.EventHandler(this.EnMenúProcesarTodo);
          // 
          // toolStripSeparator3
          // 
          toolStripSeparator3.Name = "toolStripSeparator3";
          resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
          // 
          // miMenuPDIs
          // 
          this.miMenuPDIs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenuProcesarTodoEnPDIs,
            toolStripSeparator6,
            this.miMenúEliminarCaracteresEnPDIs,
            this.miMenuArreglarLetrasEnPDIs,
            this.miMenuArreglarPalabrasEnPDIs,
            this.miMenúBuscaDuplicadosEnPDIs,
            this.miMenúBuscarErroresEnPDIs});
          this.miMenuPDIs.Name = "miMenuPDIs";
          resources.ApplyResources(this.miMenuPDIs, "miMenuPDIs");
          // 
          // miMenuProcesarTodoEnPDIs
          // 
          this.miMenuProcesarTodoEnPDIs.Name = "miMenuProcesarTodoEnPDIs";
          resources.ApplyResources(this.miMenuProcesarTodoEnPDIs, "miMenuProcesarTodoEnPDIs");
          this.miMenuProcesarTodoEnPDIs.Click += new System.EventHandler(this.EnMenuProcesarTodoEnPDIs);
          // 
          // toolStripSeparator6
          // 
          toolStripSeparator6.Name = "toolStripSeparator6";
          resources.ApplyResources(toolStripSeparator6, "toolStripSeparator6");
          // 
          // miMenúEliminarCaracteresEnPDIs
          // 
          this.miMenúEliminarCaracteresEnPDIs.Name = "miMenúEliminarCaracteresEnPDIs";
          resources.ApplyResources(this.miMenúEliminarCaracteresEnPDIs, "miMenúEliminarCaracteresEnPDIs");
          this.miMenúEliminarCaracteresEnPDIs.Click += new System.EventHandler(this.EnMenúEliminarCaracteresEnPDIs);
          // 
          // miMenuArreglarLetrasEnPDIs
          // 
          this.miMenuArreglarLetrasEnPDIs.Name = "miMenuArreglarLetrasEnPDIs";
          resources.ApplyResources(this.miMenuArreglarLetrasEnPDIs, "miMenuArreglarLetrasEnPDIs");
          this.miMenuArreglarLetrasEnPDIs.Click += new System.EventHandler(this.EnMenuArreglarLetrasEnPDIs);
          // 
          // miMenuArreglarPalabrasEnPDIs
          // 
          this.miMenuArreglarPalabrasEnPDIs.Name = "miMenuArreglarPalabrasEnPDIs";
          resources.ApplyResources(this.miMenuArreglarPalabrasEnPDIs, "miMenuArreglarPalabrasEnPDIs");
          this.miMenuArreglarPalabrasEnPDIs.Click += new System.EventHandler(this.EnMenuArreglarPalabrasEnPDIs);
          // 
          // miMenúBuscaDuplicadosEnPDIs
          // 
          this.miMenúBuscaDuplicadosEnPDIs.Name = "miMenúBuscaDuplicadosEnPDIs";
          resources.ApplyResources(this.miMenúBuscaDuplicadosEnPDIs, "miMenúBuscaDuplicadosEnPDIs");
          this.miMenúBuscaDuplicadosEnPDIs.Click += new System.EventHandler(this.EnMenuBuscarDuplicadosEnPDIs);
          // 
          // miMenúBuscarErroresEnPDIs
          // 
          this.miMenúBuscarErroresEnPDIs.Name = "miMenúBuscarErroresEnPDIs";
          resources.ApplyResources(this.miMenúBuscarErroresEnPDIs, "miMenúBuscarErroresEnPDIs");
          this.miMenúBuscarErroresEnPDIs.Click += new System.EventHandler(this.EnMenuBuscarErroresEnPDIs);
          // 
          // miMenúVías
          // 
          this.miMenúVías.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenúProcesarTodoEnVías,
            this.toolStripSeparator5,
            toolStripMenuItem2,
            this.menúBuscarPosiblesErroresDeRuteoEnVías,
            this.miMenúBuscarErroresEnVías});
          this.miMenúVías.Name = "miMenúVías";
          resources.ApplyResources(this.miMenúVías, "miMenúVías");
          // 
          // miMenúProcesarTodoEnVías
          // 
          this.miMenúProcesarTodoEnVías.Name = "miMenúProcesarTodoEnVías";
          resources.ApplyResources(this.miMenúProcesarTodoEnVías, "miMenúProcesarTodoEnVías");
          this.miMenúProcesarTodoEnVías.Click += new System.EventHandler(this.EnMenuProcesarTodoEnVías);
          // 
          // toolStripSeparator5
          // 
          this.toolStripSeparator5.Name = "toolStripSeparator5";
          resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
          // 
          // toolStripMenuItem2
          // 
          toolStripMenuItem2.Name = "toolStripMenuItem2";
          resources.ApplyResources(toolStripMenuItem2, "toolStripMenuItem2");
          toolStripMenuItem2.Click += new System.EventHandler(this.EnMenúBuscarIncongruenciasEnVías);
          // 
          // miMenúBuscarErroresEnVías
          // 
          this.miMenúBuscarErroresEnVías.Name = "miMenúBuscarErroresEnVías";
          resources.ApplyResources(this.miMenúBuscarErroresEnVías, "miMenúBuscarErroresEnVías");
          this.miMenúBuscarErroresEnVías.Click += new System.EventHandler(this.EnMenúBuscarErroresEnVías);
          // 
          // miMenuPrincipal
          // 
          this.miMenuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMenúMapa,
            toolStripMenuItem1,
            this.miMenúAyuda});
          resources.ApplyResources(this.miMenuPrincipal, "miMenuPrincipal");
          this.miMenuPrincipal.Name = "miMenuPrincipal";
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
          resources.ApplyResources(this.miMenúMapa, "miMenúMapa");
          // 
          // miMenuAbrirArchivo
          // 
          this.miMenuAbrirArchivo.Name = "miMenuAbrirArchivo";
          resources.ApplyResources(this.miMenuAbrirArchivo, "miMenuAbrirArchivo");
          this.miMenuAbrirArchivo.Click += new System.EventHandler(this.EnMenuAbrir);
          // 
          // miMenuAceptarModificaciones
          // 
          resources.ApplyResources(this.miMenuAceptarModificaciones, "miMenuAceptarModificaciones");
          this.miMenuAceptarModificaciones.Name = "miMenuAceptarModificaciones";
          this.miMenuAceptarModificaciones.Click += new System.EventHandler(this.EnMenúAceptarModificaciones);
          // 
          // miMenuGuardar
          // 
          resources.ApplyResources(this.miMenuGuardar, "miMenuGuardar");
          this.miMenuGuardar.Name = "miMenuGuardar";
          this.miMenuGuardar.Click += new System.EventHandler(this.EnMenuGuardar);
          // 
          // miMenuGuardarComo
          // 
          resources.ApplyResources(this.miMenuGuardarComo, "miMenuGuardarComo");
          this.miMenuGuardarComo.Name = "miMenuGuardarComo";
          this.miMenuGuardarComo.Click += new System.EventHandler(this.EnMenuGuardarComo);
          // 
          // miMenuSalir
          // 
          this.miMenuSalir.Name = "miMenuSalir";
          resources.ApplyResources(this.miMenuSalir, "miMenuSalir");
          this.miMenuSalir.Click += new System.EventHandler(this.EnMenuSalir);
          // 
          // miMenúAyuda
          // 
          this.miMenúAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.acercaToolStripMenuItem,
            this.miMenuAcerca});
          this.miMenúAyuda.Name = "miMenúAyuda";
          resources.ApplyResources(this.miMenúAyuda, "miMenúAyuda");
          // 
          // acercaToolStripMenuItem
          // 
          resources.ApplyResources(this.acercaToolStripMenuItem, "acercaToolStripMenuItem");
          this.acercaToolStripMenuItem.ForeColor = System.Drawing.SystemColors.HotTrack;
          this.acercaToolStripMenuItem.Name = "acercaToolStripMenuItem";
          this.acercaToolStripMenuItem.Click += new System.EventHandler(this.EnMenuPáginaWeb);
          // 
          // miMenuAcerca
          // 
          this.miMenuAcerca.Name = "miMenuAcerca";
          resources.ApplyResources(this.miMenuAcerca, "miMenuAcerca");
          this.miMenuAcerca.Click += new System.EventHandler(this.EnMenuAcerca);
          // 
          // miEstatus
          // 
          this.miEstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miTextoDeEstatus,
            this.miTextoDeCoordenadas,
            this.miBarraDeProgreso});
          resources.ApplyResources(this.miEstatus, "miEstatus");
          this.miEstatus.Name = "miEstatus";
          // 
          // miTextoDeEstatus
          // 
          resources.ApplyResources(this.miTextoDeEstatus, "miTextoDeEstatus");
          this.miTextoDeEstatus.Name = "miTextoDeEstatus";
          this.miTextoDeEstatus.Spring = true;
          // 
          // miTextoDeCoordenadas
          // 
          resources.ApplyResources(this.miTextoDeCoordenadas, "miTextoDeCoordenadas");
          this.miTextoDeCoordenadas.Name = "miTextoDeCoordenadas";
          // 
          // miBarraDeProgreso
          // 
          resources.ApplyResources(this.miBarraDeProgreso, "miBarraDeProgreso");
          this.miBarraDeProgreso.Name = "miBarraDeProgreso";
          // 
          // miControladorDePestañasPrincipal
          // 
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPaginaDeElementos);
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPaginaDePDIs);
          this.miControladorDePestañasPrincipal.Controls.Add(this.miPáginaDeVías);
          resources.ApplyResources(this.miControladorDePestañasPrincipal, "miControladorDePestañasPrincipal");
          this.miControladorDePestañasPrincipal.Name = "miControladorDePestañasPrincipal";
          this.miControladorDePestañasPrincipal.SelectedIndex = 0;
          // 
          // miPaginaDeElementos
          // 
          this.miPaginaDeElementos.Controls.Add(this.miControladorDePestañas);
          resources.ApplyResources(this.miPaginaDeElementos, "miPaginaDeElementos");
          this.miPaginaDeElementos.Name = "miPaginaDeElementos";
          this.miPaginaDeElementos.UseVisualStyleBackColor = true;
          // 
          // miControladorDePestañas
          // 
          this.miControladorDePestañas.Controls.Add(this.miPáginaMapa);
          this.miControladorDePestañas.Controls.Add(this.miPáginaTodos);
          resources.ApplyResources(this.miControladorDePestañas, "miControladorDePestañas");
          this.miControladorDePestañas.Name = "miControladorDePestañas";
          this.miControladorDePestañas.SelectedIndex = 0;
          // 
          // miPáginaMapa
          // 
          this.miPáginaMapa.Controls.Add(this.miInterfaseDeMapa);
          resources.ApplyResources(this.miPáginaMapa, "miPáginaMapa");
          this.miPáginaMapa.Name = "miPáginaMapa";
          this.miPáginaMapa.UseVisualStyleBackColor = true;
          // 
          // miInterfaseDeMapa
          // 
          this.miInterfaseDeMapa.BackColor = System.Drawing.Color.Transparent;
          resources.ApplyResources(this.miInterfaseDeMapa, "miInterfaseDeMapa");
          this.miInterfaseDeMapa.EscuchadorDeEstatus = null;
          this.miInterfaseDeMapa.ManejadorDeMapa = null;
          this.miInterfaseDeMapa.MuestraPDIs = false;
          this.miInterfaseDeMapa.MuestraPolígonos = false;
          this.miInterfaseDeMapa.MuestraPolilíneas = false;
          this.miInterfaseDeMapa.MuestraTodoElMapa = true;
          this.miInterfaseDeMapa.MuestraTodosLosElementos = true;
          this.miInterfaseDeMapa.MuestraVías = false;
          this.miInterfaseDeMapa.Name = "miInterfaseDeMapa";
          this.miInterfaseDeMapa.RectánguloVisibleEnCoordenadas = ((System.Drawing.RectangleF)(resources.GetObject("miInterfaseDeMapa.RectánguloVisibleEnCoordenadas")));
          // 
          // miPáginaTodos
          // 
          this.miPáginaTodos.Controls.Add(this.miLista);
          resources.ApplyResources(this.miPáginaTodos, "miPáginaTodos");
          this.miPáginaTodos.Name = "miPáginaTodos";
          this.miPáginaTodos.UseVisualStyleBackColor = true;
          // 
          // miLista
          // 
          this.miLista.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.miColumnaClase});
          resources.ApplyResources(this.miLista, "miLista");
          this.miLista.FullRowSelect = true;
          this.miLista.GridLines = true;
          this.miLista.Name = "miLista";
          this.miLista.UseCompatibleStateImageBehavior = false;
          this.miLista.View = System.Windows.Forms.View.Details;
          // 
          // miColumnaClase
          // 
          resources.ApplyResources(this.miColumnaClase, "miColumnaClase");
          // 
          // miPaginaDePDIs
          // 
          this.miPaginaDePDIs.Controls.Add(this.miInterfaseManejadorDePDIs);
          resources.ApplyResources(this.miPaginaDePDIs, "miPaginaDePDIs");
          this.miPaginaDePDIs.Name = "miPaginaDePDIs";
          this.miPaginaDePDIs.UseVisualStyleBackColor = true;
          // 
          // miInterfaseManejadorDePDIs
          // 
          resources.ApplyResources(this.miInterfaseManejadorDePDIs, "miInterfaseManejadorDePDIs");
          this.miInterfaseManejadorDePDIs.EscuchadorDeEstatus = null;
          this.miInterfaseManejadorDePDIs.ManejadorDeMapa = null;
          this.miInterfaseManejadorDePDIs.Name = "miInterfaseManejadorDePDIs";
          // 
          // miPáginaDeVías
          // 
          this.miPáginaDeVías.Controls.Add(this.miInterfaseManejadorDeVías);
          resources.ApplyResources(this.miPáginaDeVías, "miPáginaDeVías");
          this.miPáginaDeVías.Name = "miPáginaDeVías";
          this.miPáginaDeVías.UseVisualStyleBackColor = true;
          // 
          // miInterfaseManejadorDeVías
          // 
          resources.ApplyResources(this.miInterfaseManejadorDeVías, "miInterfaseManejadorDeVías");
          this.miInterfaseManejadorDeVías.EscuchadorDeEstatus = null;
          this.miInterfaseManejadorDeVías.ManejadorDeMapa = null;
          this.miInterfaseManejadorDeVías.Name = "miInterfaseManejadorDeVías";
          // 
          // menúBuscarPosiblesErroresDeRuteoEnVías
          // 
          this.menúBuscarPosiblesErroresDeRuteoEnVías.Name = "menúBuscarPosiblesErroresDeRuteoEnVías";
          resources.ApplyResources(this.menúBuscarPosiblesErroresDeRuteoEnVías, "menúBuscarPosiblesErroresDeRuteoEnVías");
          this.menúBuscarPosiblesErroresDeRuteoEnVías.Click += new System.EventHandler(this.EnMenúBuscarPosiblesErroresDeRuteoEnVías);
          // 
          // InterfaseManejadorDeMapa
          // 
          resources.ApplyResources(this, "$this");
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.Controls.Add(this.miControladorDePestañasPrincipal);
          this.Controls.Add(this.miEstatus);
          this.Controls.Add(this.miMenuPrincipal);
          this.MainMenuStrip = this.miMenuPrincipal;
          this.Name = "InterfaseManejadorDeMapa";
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
        private System.Windows.Forms.ToolStripMenuItem miMenúAyuda;
        private System.Windows.Forms.StatusStrip miEstatus;
        private GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas miControladorDePestañasPrincipal;
        private System.Windows.Forms.TabPage miPaginaDePDIs;
        private System.Windows.Forms.ToolStripStatusLabel miTextoDeEstatus;
        private System.Windows.Forms.ToolStripProgressBar miBarraDeProgreso;
        private System.Windows.Forms.ToolStripStatusLabel miTextoDeCoordenadas;
        private System.Windows.Forms.ToolStripMenuItem acercaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miMenuAcerca;
        private System.Windows.Forms.TabPage miPaginaDeElementos;
        private GpsYv.ManejadorDeMapa.Interfase.PDIs.InterfaseManejadorDePDIs miInterfaseManejadorDePDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuGuardar;
        private GpsYv.ManejadorDeMapa.Interfase.ControladorDePestañas miControladorDePestañas;
        private System.Windows.Forms.TabPage miPáginaMapa;
        private InterfaseMapa miInterfaseDeMapa;
        private System.Windows.Forms.TabPage miPáginaTodos;
        private GpsYv.ManejadorDeMapa.Interfase.InterfaseListaDeElementos miLista;
        private System.Windows.Forms.ColumnHeader miColumnaClase;
        private System.Windows.Forms.ToolStripMenuItem miMenuAceptarModificaciones;
        private System.Windows.Forms.TabPage miPáginaDeVías;
        private GpsYv.ManejadorDeMapa.Interfase.Vías.InterfaseManejadorDeVías miInterfaseManejadorDeVías;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem miMenúProcesarTodo;
        private System.Windows.Forms.ToolStripMenuItem miMenuProcesarTodoEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúEliminarCaracteresEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuArreglarLetrasEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuArreglarPalabrasEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscarErroresEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenuPDIs;
        private System.Windows.Forms.ToolStripMenuItem miMenúVías;
        private System.Windows.Forms.ToolStripMenuItem miMenúProcesarTodoEnVías;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscarErroresEnVías;
        private System.Windows.Forms.ToolStripMenuItem miMenúBuscaDuplicadosEnPDIs;
        private System.Windows.Forms.ToolStripMenuItem menúBuscarPosiblesErroresDeRuteoEnVías;
    }
}

