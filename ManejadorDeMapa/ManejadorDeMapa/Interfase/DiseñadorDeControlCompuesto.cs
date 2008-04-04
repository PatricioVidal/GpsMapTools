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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms.Design.Behavior;
using System.Diagnostics;

namespace GpsYv.ManejadorDeMapa.Interfase
{
  /// <summary>
  /// Diseñador de Control Compuesto
  /// </summary>
  /// <typeparam name="T">El tipo de control.</typeparam>
  [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
  public abstract class DiseñadorDeControlCompuesto<T> : ControlDesigner
    where T : Control
  {
    #region Campos
    private BehaviorService miServicioDeComportamiento;
    private ISelectionService miServicioDeSelección;
    private Adorner miAdornador = new Adorner();
    #endregion

    #region Métodos Protegidos
    /// <summary>
    /// Libera los recursos.
    /// </summary>
    /// <param name="elLibera">Variable lógica que indica que se liberen los recursos manejados.</param>
    protected override void Dispose(bool elLibera)
    {
      if (elLibera)
      {
        if (miServicioDeComportamiento != null)
        {
          // Remueve los adornadores añadidos por este diseñador.
          miServicioDeComportamiento.Adorners.Remove(miAdornador);
        }
      }

      base.Dispose(elLibera);
    }


    /// <summary>
    /// Devuelve los controles que se quieren manejar del control compuesto.
    /// </summary>
    /// <param name="elControlCompuesto">El control compuesto.</param>
    protected abstract Control[] ObtieneControlesAManejar(T elControlCompuesto);


    /// <summary>
    /// Inicializa.
    /// </summary>
    /// <param name="elComponente">El componente.</param>
    public override void Initialize(IComponent elComponente)
    {
      base.Initialize(elComponente);
      miServicioDeComportamiento = (BehaviorService)GetService(typeof(BehaviorService));
      miServicioDeSelección = (ISelectionService)GetService(typeof(ISelectionService));

      if (miServicioDeComportamiento != null)
      {
        miServicioDeComportamiento.Adorners.Add(miAdornador);

        // Añade los glifos.
        Control[] controles = ObtieneControlesAManejar(Control as T);
        foreach (Control control in controles)
        {
          this.miAdornador.Glyphs.Add(new GlifoControlCompuesto(
              new ComportamientoControlCompuesto(this, miAdornador, control),
              miServicioDeComportamiento,
              miServicioDeSelección,
              control));
        }
      }
    }
    #endregion

    #region Clases Privadas
    private class GlifoControlCompuesto : Glyph
    {
      private readonly BehaviorService miServicioDeComportamiento;
      private readonly ComportamientoControlCompuesto miComportamiento;
      private readonly ISelectionService miServicioDeSelección;
      private readonly Control miControl;

      public GlifoControlCompuesto(
        ComportamientoControlCompuesto elComportamiento,
        BehaviorService elServicioDeComportamiento,
        ISelectionService elServicioDeSelección,
        Control elControl)
        : base(elComportamiento)
      {
        miServicioDeComportamiento = elServicioDeComportamiento;
        miServicioDeSelección = elServicioDeSelección;
        miComportamiento = elComportamiento;
        miControl = elControl;
      }


      public override Behavior Behavior
      {
        get
        {
          return miComportamiento;
        }
      }


      public override Rectangle Bounds
      {
        get
        {
          if (miControl != null)
          {
            Point controlLocation = this.miServicioDeComportamiento.ControlToAdornerWindow(this.miControl);
            Size size = miControl.Bounds.Size;
            Rectangle bounds = new Rectangle(controlLocation, size);
            return bounds;
          }
          return Rectangle.Empty;
        }
      }


      public override Cursor GetHitTest(Point p)
      {
        if (Bounds.Contains(p))
        {
          return Cursors.Hand;
        }
        return null;
      }


      public override void Paint(PaintEventArgs losArgumentos)
      {
        if (miServicioDeSelección.PrimarySelection == this.miControl)
        {
          Rectangle borde = Bounds;
          borde.Inflate(1, 1);

          ControlPaint.DrawBorder(losArgumentos.Graphics, borde, Color.Blue, ButtonBorderStyle.Dotted);
        }
      }
    }


    private class ComportamientoControlCompuesto : Behavior
    {
      private readonly IDesigner miDiseñador;
      private readonly IServiceProvider miProveedor;
      private readonly ISelectionService miServicioDeSelección;
      private readonly Adorner miAdornador;
      private readonly Control miControl;

      public ComportamientoControlCompuesto(
        IDesigner elDiseñador,
        Adorner elAdornador,
        Control elControl)
        : base()
      {
        miDiseñador = elDiseñador;
        miAdornador = elAdornador;
        miControl = elControl;
        miProveedor = miDiseñador.Component.Site;
        miServicioDeSelección = miProveedor.GetService(typeof(ISelectionService)) as ISelectionService;
        miServicioDeSelección.SelectionChanged += EnSelecciónCambiada;
      }


      void EnSelecciónCambiada(object sender, EventArgs e)
      {
        this.miAdornador.Invalidate();
      }


      public override bool OnMouseDown(Glyph elGlifo, MouseButtons elBotón, Point laPosición)
      {
        base.OnMouseDown(elGlifo, elBotón, laPosición);
        ArrayList selección = new ArrayList();
        selección.Add(miControl);
        miServicioDeSelección.SetSelectedComponents(selección);

        return true;
      }

    }
    #endregion
  }
}